using Server.Targeting;
using System;

namespace Server.Spells.Second
{
	public class StrengthSpell : MagerySpell
	{
		private static readonly SpellInfo m_Info = new SpellInfo(
			"Strength", "Uus Mani",
			212,
			9061,
			Reagent.MandrakeRoot,
			Reagent.Nightshade);

		public override MagicAptitudeRequirement[] AffinityRequirements { get { return new MagicAptitudeRequirement[] { new MagicAptitudeRequirement(MagieType.Arcane, 3) }; } }

		public StrengthSpell(Mobile caster, Item scroll)
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

				int strBonus = (magery + evalInt) / 10; // Ajustez ce diviseur selon vos besoins
				TimeSpan duration = TimeSpan.FromSeconds(magery + evalInt); // Ajustez cette durée selon vos besoins

				// Appliquer le bonus manuellement
				m.AddStatMod(new StatMod(StatType.Str, "Strength_Str", strBonus, duration));

				BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Strength, 1075845, duration, m, strBonus.ToString()));

				m.FixedParticles(0x375A, 10, 15, 5017, EffectLayer.Waist);
				m.PlaySound(0x1EE);

				m.SendLocalizedMessage(1075845); // Your strength has been increased.
				if (m != Caster)
				{
					Caster.SendLocalizedMessage(1075846); // You have increased their strength.
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private readonly StrengthSpell m_Owner;
			public InternalTarget(StrengthSpell owner)
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
