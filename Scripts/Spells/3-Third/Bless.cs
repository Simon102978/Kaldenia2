using Server.Targeting;
using System;
using System.Collections.Generic;

namespace Server.Spells.Third
{
	public class BlessSpell : MagerySpell
	{
		private static readonly SpellInfo m_Info = new SpellInfo(
			"Bless", "Rel Sanct",
			203,
			9061,
			Reagent.Garlic,
			Reagent.MandrakeRoot);

		private static Dictionary<Mobile, InternalTimer> _Table;

		public override MagicAptitudeRequirement[] AffinityRequirements { get { return new MagicAptitudeRequirement[] { new MagicAptitudeRequirement(MagieType.Vie, 4) }; } }

		public static bool IsBlessed(Mobile m)
		{
			return _Table != null && _Table.ContainsKey(m);
		}

		public static void AddBless(Mobile m, TimeSpan duration)
		{
			if (_Table == null)
				_Table = new Dictionary<Mobile, InternalTimer>();

			if (_Table.ContainsKey(m))
			{
				_Table[m].Stop();
			}

			_Table[m] = new InternalTimer(m, duration);
		}

		public static void RemoveBless(Mobile m, bool early = false)
		{
			if (_Table != null && _Table.ContainsKey(m))
			{
				_Table[m].Stop();
				m.Delta(MobileDelta.Stat);

				_Table.Remove(m);
			}
		}

		public BlessSpell(Mobile caster, Item scroll)
			: base(caster, scroll, m_Info)
		{
		}

		public override SpellCircle Circle => SpellCircle.Third;

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

				int statBonus = (magery + evalInt) / 13;
				TimeSpan duration = TimeSpan.FromSeconds(magery + evalInt);

				// Appliquer les bonus manuellement
				m.AddStatMod(new StatMod(StatType.Str, "Bless_Str", statBonus, duration));
				m.AddStatMod(new StatMod(StatType.Dex, "Bless_Dex", statBonus, duration));
				m.AddStatMod(new StatMod(StatType.Int, "Bless_Int", statBonus, duration));

				string args = string.Format("{0}\t{0}\t{0}", statBonus);
				BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Bless, 1075847, 1075848, duration, m, args));

				m.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
				m.PlaySound(0x1EA);

				AddBless(m, duration);

				m.SendLocalizedMessage(1075847); // You are blessed with enhanced potential.
				if (m != Caster)
				{
					Caster.SendLocalizedMessage(1075848); // You have blessed them with enhanced potential.
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private readonly BlessSpell m_Owner;
			public InternalTarget(BlessSpell owner)
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

		private class InternalTimer : Timer
		{
			public Mobile Mobile { get; set; }

			public InternalTimer(Mobile m, TimeSpan duration)
				: base(duration)
			{
				Mobile = m;
				Start();
			}

			protected override void OnTick()
			{
				RemoveBless(Mobile);
			}
		}
	}
}
