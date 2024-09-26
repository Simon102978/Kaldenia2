using System;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Items
{
	public class CelestialManaPotion : BaseManaPotion
	{
		private const int TickDuration = 20; // Durée totale en secondes
		private const int TickInterval = 3; // Intervalle entre chaque tick en secondes
		private const int TotalManaRestore = 45; // Montant total de mana restauré (comme une Greater Mana Potion)

		[Constructable]
		public CelestialManaPotion()
			: base(PotionEffect.CelesteManaPotion)
		{
			Name = "Potion de mana céleste";
			Hue = 2079; // Vous pouvez ajuster la couleur si nécessaire
		}

		public CelestialManaPotion(Serial serial)
			: base(serial)
		{
		}

		public override int MinManaHeal => Utility.RandomMinMax(3, 7); // Minimum par tick
		public override int MaxManaHeal => TotalManaRestore / (TickDuration / TickInterval); // Maximum par tick
		public override double Delay => 0.0; // Pas de délai initial

		public override void Drink(Mobile from)
		{
			if (from.Alive)
			{
				from.AddToBackpack(new Bottle());

				PlayDrinkEffect(from);
				Consume();

				Timer manaTimer = new CelestialManaTimer(from, this);
				manaTimer.Start();
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

		private class CelestialManaTimer : Timer
		{
			private Mobile m_Mobile;
			private CelestialManaPotion m_Potion;
			private int m_TickCount;

			public CelestialManaTimer(Mobile m, CelestialManaPotion potion)
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
					int manaAmount = m_Potion.MaxManaHeal;
					m_Mobile.Mana += manaAmount;
					m_Mobile.SendMessage("Vous ressentez une vague de mana céleste.");

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
