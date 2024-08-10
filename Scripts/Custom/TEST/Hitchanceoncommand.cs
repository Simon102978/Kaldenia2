using System;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Commands;
using Server.Items;
using Server.Spells.SkillMasteries;

namespace Server.Commands
{
	public class HitChanceCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register("hitchance", AccessLevel.Player, new CommandEventHandler(HitChance_OnCommand));
		}

		[Usage("HitChance")]
		[Description("Calcule votre chance de toucher une cible PvE.")]
		public static void HitChance_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;
			from.SendMessage("Sélectionnez une cible PvE pour calculer votre chance de la toucher.");
			from.Target = new HitChanceTarget();
		}

		private class HitChanceTarget : Target
		{
			public HitChanceTarget() : base(12, false, TargetFlags.None) { }

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is Mobile defender)
				{
					double hitchance = CalculateHitChance(from, defender);
					from.SendMessage($"Votre chance de toucher {defender.Name} est d'environ {hitchance:F2}%.");
				}
				else
				{
					from.SendMessage("Vous devez cibler un mobile.");
				}
			}

			private double CalculateHitChance(Mobile attacker, Mobile defender)
			{
				BaseWeapon atkWeapon = attacker.Weapon as BaseWeapon;
				BaseWeapon defWeapon = defender.Weapon as BaseWeapon;

				if (atkWeapon == null || defWeapon == null)
					return 0;

				double atkValue = atkWeapon.GetAttackSkillValue(attacker, defender);
				double defValue = defWeapon.GetDefendSkillValue(attacker, defender);

				atkValue = Math.Max(atkValue, -19.9);
				defValue = Math.Max(defValue, -19.9);

				int bonus = AosAttributes.GetValue(attacker, AosAttribute.AttackChance);

				if (attacker is BaseCreature bc && !bc.Controlled && defender is BaseCreature bc2 && bc2.Controlled)
				{
					bonus = Math.Max(bonus, 45);
				}

				bonus = Math.Min(45, bonus);

				double ourValue = (atkValue + 20.0) * (100 + bonus);

				bonus = AosAttributes.GetValue(defender, AosAttribute.DefendChance);

				ForceArrow.ForceArrowInfo info = ForceArrow.GetInfo(attacker, defender);

				if (info != null && info.Defender == defender)
					bonus -= info.DefenseChanceMalus;

				int max = 45 + BaseArmor.GetRefinedDefenseChance(defender) + WhiteTigerFormSpell.GetDefenseCap(defender);

				if (bonus > max)
					bonus = max;

				double theirValue = (defValue + 20.0) * (100 + bonus);

				double chance = ourValue / (theirValue * 1.8); // Changed from 2.0 to 1.8

				if (atkWeapon is BaseThrown)
				{
					// Distance malus
					if (attacker.InRange(defender, 1))  // Close Quarters
					{
						chance -= (.12 - Math.Min(12, (attacker.Skills[SkillName.Throwing].Value + attacker.RawDex) / 20) / 10);
					}
					else if (attacker.GetDistanceToSqrt(defender) < ((BaseThrown)atkWeapon).MinThrowRange)  // Too close
					{
						chance -= .12;
					}

					// Shield penalty
					BaseShield shield = attacker.FindItemOnLayer(Layer.TwoHanded) as BaseShield;

					if (shield != null)
					{
						double malus = Math.Min(90, 1200 / Math.Max(1.0, attacker.Skills[SkillName.Parry].Value));
						chance = chance - (chance * (malus / 100));
					}
				}

				if (defWeapon is BaseThrown)
				{
					BaseShield shield = defender.FindItemOnLayer(Layer.TwoHanded) as BaseShield;

					if (shield != null)
					{
						double malus = Math.Min(90, 1200 / Math.Max(1.0, defender.Skills[SkillName.Parry].Value));
						chance = chance + (chance * (malus / 100));
					}
				}

				chance = Math.Max(chance, 0.02);

				// Ajout du 15% de chance de toucher supplémentaire
				chance += 0.15;

				// Assurez-vous que la chance ne dépasse pas 100%
				chance = Math.Min(chance, 1.0);

				// Convert to percentage
				return chance * 100;
			}
		}
	}
}
