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

				Skill atkSkill = attacker.Skills[atkWeapon.Skill];
				Skill defSkill = defender.Skills[defWeapon.Skill];

				double atkValue = atkWeapon.GetAttackSkillValue(attacker, defender);
				double defValue = defWeapon.GetDefendSkillValue(attacker, defender);

				atkValue = Math.Max(atkValue, -19.9);
				defValue = Math.Max(defValue, -19.9);

				int bonus = AosAttributes.GetValue(attacker, AosAttribute.AttackChance);
				bonus = Math.Min(45, bonus);

				double ourValue = (atkValue + 20.0) * (100 + bonus);

				bonus = AosAttributes.GetValue(defender, AosAttribute.DefendChance);
				int max = 45 + BaseArmor.GetRefinedDefenseChance(defender) + WhiteTigerFormSpell.GetDefenseCap(defender);
				bonus = Math.Min(bonus, max);

				double theirValue = (defValue + 20.0) * (100 + bonus);

				double chance = ourValue / (theirValue * 2.0);

				// Ajustements pour les armes de lancer
				if (atkWeapon is BaseThrown)
				{
					// Ajoutez ici les ajustements pour les armes de lancer
				}

				chance = Math.Max(chance, 0.02);

				// Convertir en pourcentage
				return chance * 100;
			}
		}
	}
}
