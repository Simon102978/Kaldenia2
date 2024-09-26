using System;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Items
{
	public class CelestialHealPotion : BaseHealPotion
	{
		private const int TickDuration = 20; // Durée totale en secondes
		private const int TickInterval = 3; // Intervalle entre chaque tick en secondes
		private const int TotalHealing = 60; // Montant total de guérison (comme une Greater Heal Potion)

		[Constructable]
		public CelestialHealPotion()
			: base(PotionEffect.CelesteHealPotion)
		{
			Name = "Potion de soins célestes";
			Hue = 1999;
		}

		public CelestialHealPotion(Serial serial)
			: base(serial)
		{
		}

		public override int MinHeal => Utility.RandomMinMax(3, 7); // Minimum par tick
		public override int MaxHeal => TotalHealing / (TickDuration / TickInterval); // Maximum par tick
		public override double Delay => 0.0; // Pas de délai initial

		public override void Drink(Mobile from)
		{
			if (from.Alive)
			{
				from.AddToBackpack(new Bottle());

				PlayDrinkEffect(from);
				Consume();

				Timer healingTimer = new CelestialHealingTimer(from, this);
				healingTimer.Start();
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		private class CelestialHealingTimer : Timer
		{
			private Mobile m_Mobile;
			private CelestialHealPotion m_Potion;
			private int m_TickCount;

			public CelestialHealingTimer(Mobile m, CelestialHealPotion potion)
				: base(TimeSpan.FromSeconds(TickInterval), TimeSpan.FromSeconds(TickInterval))
			{
				m_Mobile = m;
				m_Potion = potion;
				m_TickCount = 0;
			}

			protected override void OnTick()
			{
				if (m_Mobile.Alive)
				{
					int healAmount = m_Potion.MaxHeal;
					m_Mobile.Heal(healAmount);
					m_Mobile.SendMessage("Vous ressentez une vague de guérison céleste.");

					m_TickCount++;
					if (m_TickCount >= TickDuration / TickInterval)
					{
						Stop();
					}
				}
				else
				{
					Stop();
				}
			}
		}
	}
}
