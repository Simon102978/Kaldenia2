using Server.Targeting;
using System;

namespace Server.Spells.Second
{
	public class AgilitySpell : MagerySpell
	{
		private static readonly SpellInfo m_Info = new SpellInfo(
			"Agility", "Ex Uus",
			212,
			9061,
			Reagent.Bloodmoss,
			Reagent.MandrakeRoot);

		public override MagicAptitudeRequirement[] AffinityRequirements { get { return new MagicAptitudeRequirement[] { new MagicAptitudeRequirement(MagieType.Arcane, 3) }; } }

		public AgilitySpell(Mobile caster, Item scroll)
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

				int dexBonus = (magery + evalInt) / 10; // Ajustez ce diviseur selon vos besoins
				TimeSpan duration = TimeSpan.FromSeconds(magery + evalInt); // Ajustez cette durée selon vos besoins

				// Appliquer le bonus manuellement
				m.AddStatMod(new StatMod(StatType.Dex, "Agility_Dex", dexBonus, duration));

				BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Agility, 1075841, duration, m, dexBonus.ToString()));

				m.FixedParticles(0x375A, 10, 15, 5010, EffectLayer.Waist);
				m.PlaySound(0x1e7);

				m.SendLocalizedMessage(1075841); // Your agility has been increased.
				if (m != Caster)
				{
					Caster.SendLocalizedMessage(1075842); // You have increased their agility.
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private readonly AgilitySpell m_Owner;
			public InternalTarget(AgilitySpell owner)
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
