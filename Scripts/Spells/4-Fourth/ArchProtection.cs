using Server.Engines.PartySystem;
using System;
using System.Collections.Generic;

namespace Server.Spells.Fourth
{
	public class ArchProtectionSpell : MagerySpell
	{
		private static readonly SpellInfo m_Info = new SpellInfo(
			"Arch Protection", "Vas Uus Sanct",
			239,
			9011,
			Reagent.Garlic,
			Reagent.Ginseng,
			Reagent.MandrakeRoot);

		public ArchProtectionSpell(Mobile caster, Item scroll)
			: base(caster, scroll, m_Info)
		{
		}

		public override MagicAptitudeRequirement[] AffinityRequirements { get { return new MagicAptitudeRequirement[] { new MagicAptitudeRequirement(MagieType.Obeissance, 8) }; } }
		public override SpellCircle Circle => SpellCircle.Fourth;

		public override void OnCast()
		{
			if (CheckSequence())
			{
				List<Mobile> targets = new List<Mobile>();
				Map map = Caster.Map;

				if (map != null)
				{
					IPooledEnumerable eable = map.GetMobilesInRange(Caster.Location, 10);

					foreach (Mobile m in eable)
					{
						if (Caster.CanBeBeneficial(m, false))
							targets.Add(m);
					}

					eable.Free();
				}

				Party party = Party.Get(Caster);

				for (int i = 0; i < targets.Count; ++i)
				{
					Mobile m = targets[i];

					if (m == Caster || (party != null && party.Contains(m)))
					{
						Caster.DoBeneficial(m);
						Second.ProtectionSpell.Toggle(Caster, m, true);
						AddEntry(m, Caster);
					}
				}

				Caster.PlaySound(0x299);
				Caster.FixedParticles(0x3779, 10, 20, 5002, EffectLayer.Waist);
			}

			FinishSequence();
		}

		private static readonly Dictionary<Mobile, Timer> _Table = new Dictionary<Mobile, Timer>();

		private static void AddEntry(Mobile m, Mobile caster)
		{
			if (_Table.ContainsKey(m))
			{
				_Table[m].Stop();
			}

			Timer t = new InternalTimer(m, caster);
			t.Start();
			_Table[m] = t;
		}

		public static void RemoveEntry(Mobile m)
		{
			if (_Table.ContainsKey(m))
			{
				_Table[m].Stop();
				_Table.Remove(m);
				m.EndAction(typeof(ArchProtectionSpell));
				m.SendLocalizedMessage(1005587); // The protection spell has expired.
			}
		}

		private class InternalTimer : Timer
		{
			private readonly Mobile m_Owner;

			public InternalTimer(Mobile target, Mobile caster)
				: base(TimeSpan.FromSeconds(0))
			{
				double time = caster.Skills[SkillName.Magery].Value * 1.2;
				if (time > 144)
					time = 144;
				Delay = TimeSpan.FromSeconds(time);
				Priority = TimerPriority.OneSecond;

				m_Owner = target;
			}

			protected override void OnTick()
			{
				RemoveEntry(m_Owner);
			}
		}
	}
}
