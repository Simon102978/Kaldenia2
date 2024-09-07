using System;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Sixth
{
	public class DispelSpell : MagerySpell
	{
		private static readonly SpellInfo m_Info = new SpellInfo(
			"Dispel", "An Ort",
			218,
			9002,
			Reagent.Garlic,
			Reagent.MandrakeRoot,
			Reagent.SulfurousAsh);

		public DispelSpell(Mobile caster, Item scroll)
			: base(caster, scroll, m_Info)
		{
		}

		public override MagicAptitudeRequirement[] AffinityRequirements { get { return new MagicAptitudeRequirement[] { new MagicAptitudeRequirement(MagieType.Obeissance, 10) }; } }

		public override SpellCircle Circle => SpellCircle.Sixth;

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		public class InternalTarget : Target
		{
			private readonly DispelSpell m_Owner;

			public InternalTarget(DispelSpell owner)
				: base(10, false, TargetFlags.Harmful)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is Mobile)
				{
					Mobile m = (Mobile)o;
					BaseCreature bc = m as BaseCreature;

					if (!from.CanSee(m))
					{
						from.SendLocalizedMessage(500237); // Target can not be seen.
					}
					else if (bc == null || !bc.Summoned)
					{
						from.SendLocalizedMessage(1005049); // That cannot be dispelled.
					}
					else if (m_Owner.CheckHSequence(m))
					{
						SpellHelper.Turn(from, m);

						double dispelChance = CalculateDispelChance(from, bc);

						if (dispelChance > Utility.RandomDouble())
						{
							Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
							Effects.PlaySound(m, m.Map, 0x201);

							int damage = bc.Hits / 2;
							bc.Hits -= damage;

							from.SendMessage($"Vous avez réussi à affaiblir la créature invoquée de {damage} points de vie.");

							if (bc.Hits <= 0)
							{
								bc.Kill();
							}
						}
						else
						{
							m.FixedEffect(0x3779, 10, 20);
							from.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
						}
					}
				}
			}

			private double CalculateDispelChance(Mobile from, BaseCreature target)
			{
				double dispelChance;

				if (from is PlayerMobile) // Si le lanceur est un joueur
				{
					dispelChance = (50.0 + ((100 * (from.Skills.Magery.Value - target.GetDispelDifficulty())) / (target.DispelFocus * 2))) / 100;
				}
				else // Si le lanceur est un monstre
				{
					// Réduisez considérablement la chance de dissipation pour les monstres
					dispelChance = (25.0 + ((100 * (from.Skills.Magery.Value - target.GetDispelDifficulty())) / (target.DispelFocus * 4))) / 100;
				}

				// Skill Masteries
				dispelChance -= ((double)SkillMasteries.MasteryInfo.EnchantedSummoningBonus(target) / 100);

				// Assurez-vous que la chance ne soit pas négative ou supérieure à 1
				return Math.Max(0.05, Math.Min(dispelChance, 0.80));
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
