using Server.Targeting;
using System;

namespace Server.Spells.Second
{
	public class CunningSpell : MagerySpell
	{
		private static readonly SpellInfo m_Info = new SpellInfo(
			"Cunning", "Uus Wis",
			212,
			9061,
			Reagent.MandrakeRoot,
			Reagent.Nightshade);

		public override MagicAptitudeRequirement[] AffinityRequirements { get { return new MagicAptitudeRequirement[] { new MagicAptitudeRequirement(MagieType.Arcane, 3) }; } }

		public CunningSpell(Mobile caster, Item scroll)
			: base(caster, scroll, m_Info)
		{
		}

		public override SpellCircle Circle => SpellCircle.Second;

		public override void OnCast()
		{
			Caster.Target = new InternalTarget(this);
		}

		public void Target(Mobile m)
		{
			if (!Caster.CanSee(m))
			{
				Caster.SendLocalizedMessage(500237); // Target can not be seen.
			}
			else if (CheckBSequence(m))
			{
				SpellHelper.Turn(Caster, m);

				int magery = (int)Caster.Skills[SkillName.Magery].Value;
				int evalInt = (int)Caster.Skills[SkillName.EvalInt].Value;

				int intBonus = (magery + evalInt) / 10; // Ajustez ce diviseur selon vos besoins
				TimeSpan duration = TimeSpan.FromSeconds(magery + evalInt); // Ajustez cette durée selon vos besoins

				// Appliquer le bonus manuellement
				m.AddStatMod(new StatMod(StatType.Int, "Cunning_Int", intBonus, duration));

				BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Cunning, 1075845, duration, m, intBonus.ToString()));

				m.FixedParticles(0x375A, 10, 15, 5011, EffectLayer.Head);
				m.PlaySound(0x1EB);

				m.SendLocalizedMessage(1075845); // Your intelligence has been increased.

				if (m != Caster)
				{
					Caster.SendLocalizedMessage(1075846); // You have increased their intelligence.
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private readonly CunningSpell m_Owner;

			public InternalTarget(CunningSpell owner)
				: base(10, false, TargetFlags.Beneficial)
			{
				m_Owner = owner;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (o is Mobile)
				{
					m_Owner.Target((Mobile)o);
				}
			}

			protected override void OnTargetFinish(Mobile from)
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
