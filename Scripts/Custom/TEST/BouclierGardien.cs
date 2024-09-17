using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Mobiles;

namespace Server.Scripts.Commands
{
	public class BouclierGardien
	{
		private static Dictionary<Mobile, Timer> m_Timers = new Dictionary<Mobile, Timer>();
		private static Dictionary<Mobile, BouclierEffects> m_Effects = new Dictionary<Mobile, BouclierEffects>();

		public static void Initialize()
		{
			//CommandSystem.Register("BouclierGardien", AccessLevel.Player, new CommandEventHandler(BouclierGardien_OnCommand));
		}

		[Usage("BouclierGardien")]
		[Description("Permet au personnage d'invoquer un bouclier \"magique\" qui offre une protection supplémentaire pour 20 points de mana")]
		public static void BouclierGardien_OnCommand(CommandEventArgs e)
		{
			if (!(e.Mobile is PlayerMobile from) || !from.Alive)
				return;

			if (from.Skills.Parry.Base < 10)
			{
				from.SendMessage("Vous devez avoir au moins 10 en Parry pour utiliser le Bouclier Gardien.");
				return;
			}

			int bc = (int)(from.Skills.Parry.Base / 10);

			if (from.Mana < 20)
			{
				from.SendMessage("Vous n'avez pas assez de mana.");
				return;
			}

			StopTimer(from);

			from.Mana -= 20;
			from.Stam -= 40;

			int value = bc * 3;
			from.VirtualArmor += value;

			BouclierEffects effects = new BouclierEffects(value);
			ApplyEffects(from, effects);

			Timer t = new InternalTimer(from, effects, TimeSpan.FromSeconds(12));
			m_Timers[from] = t;
			t.Start();

			m_Effects[from] = effects;

			from.FixedParticles(0x375A, 9, 20, 5016, EffectLayer.Waist);
			from.PlaySound(0x212);
			from.PlaySound(0x206);

			from.SendMessage("Vous activez le bouclier gardien.");
		}

		public static void StopTimer(Mobile m)
		{
			if (m_Timers.TryGetValue(m, out Timer t))
			{
				t.Stop();
				m_Timers.Remove(m);

				if (m_Effects.TryGetValue(m, out BouclierEffects effects))
				{
					RemoveEffects(m, effects);
					m_Effects.Remove(m);
				}
			}
		}

		private static void ApplyEffects(Mobile m, BouclierEffects effects)
		{
			m.AddResistanceMod(new ResistanceMod(ResistanceType.Physical, effects.PhysicalResist));
			m.AddResistanceMod(new ResistanceMod(ResistanceType.Fire, effects.FireResist));
			m.AddResistanceMod(new ResistanceMod(ResistanceType.Cold, effects.ColdResist));
			m.AddResistanceMod(new ResistanceMod(ResistanceType.Poison, effects.PoisonResist));
			m.AddResistanceMod(new ResistanceMod(ResistanceType.Energy, effects.EnergyResist));
			m.MagicDamageAbsorb += effects.MagicAbsorb;
			m.MeleeDamageAbsorb += effects.MeleeAbsorb;
		}

		private static void RemoveEffects(Mobile m, BouclierEffects effects)
		{
			m.VirtualArmor -= effects.VirtualArmor;
			m.RemoveResistanceMod(new ResistanceMod(ResistanceType.Physical, effects.PhysicalResist));
			m.RemoveResistanceMod(new ResistanceMod(ResistanceType.Fire, effects.FireResist));
			m.RemoveResistanceMod(new ResistanceMod(ResistanceType.Cold, effects.ColdResist));
			m.RemoveResistanceMod(new ResistanceMod(ResistanceType.Poison, effects.PoisonResist));
			m.RemoveResistanceMod(new ResistanceMod(ResistanceType.Energy, effects.EnergyResist));
			m.MagicDamageAbsorb -= effects.MagicAbsorb;
			m.MeleeDamageAbsorb -= effects.MeleeAbsorb;
		}

		private class BouclierEffects
		{
			public int VirtualArmor { get; }
			public int PhysicalResist { get; }
			public int FireResist { get; }
			public int ColdResist { get; }
			public int PoisonResist { get; }
			public int EnergyResist { get; }
			public int MagicAbsorb { get; }
			public int MeleeAbsorb { get; }

			public BouclierEffects(int value)
			{
				VirtualArmor = value;
				PhysicalResist = value;
				FireResist = value / 3;
				ColdResist = value / 3;
				PoisonResist = value / 3;
				EnergyResist = value / 3;
				MagicAbsorb = value / 2;
				MeleeAbsorb = value / 2;
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Owner;
			private BouclierEffects m_Effects;

			public InternalTimer(Mobile owner, BouclierEffects effects, TimeSpan duration) : base(duration)
			{
				Priority = TimerPriority.OneSecond;
				m_Owner = owner;
				m_Effects = effects;
			}

			protected override void OnTick()
			{
				RemoveEffects(m_Owner, m_Effects);
				BouclierGardien.m_Effects.Remove(m_Owner);
				BouclierGardien.m_Timers.Remove(m_Owner);

				m_Owner.SendMessage("Votre bouclier gardien est maintenant inactif.");
			}
		}

	}
}
