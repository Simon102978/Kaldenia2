using System;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class SleepPotion : BasePotion
	{
		private const int SleepDuration = 300; // 5 minutes en secondes

		[Constructable]
		public SleepPotion() : base(0xF0B, PotionEffect.SleepPotion)
		{
			Name = "Potion de sommeil";
			Hue = 2021; // Couleur violette foncée, ajustez selon vos préférences
		}

		public SleepPotion(Serial serial) : base(serial)
		{
		}

		public override void Drink(Mobile from)
		{
			if (from.Alive)
			{
				from.AddToBackpack(new Bottle());

				PlayDrinkEffect(from);
				Consume();

				from.SendMessage("Vous vous sentez soudainement très somnolent...");

				// Appliquer l'effet de sommeil
				ApplySleepEffect(from);
			}
		}

		private void ApplySleepEffect(Mobile from)
		{
			// Sauvegarder le BodyValue original
			int originalBodyValue = from.BodyValue;

			// Changer le BodyValue en fonction du sexe
			from.BodyValue = from.Female ? 403 : 402;

			// Geler le joueur
			from.Frozen = true;


			// Créer un timer pour réveiller le joueur après la durée spécifiée
			Timer.DelayCall(TimeSpan.FromSeconds(SleepDuration), () =>
			{
				WakeUp(from, originalBodyValue);
			});
		}

		private void WakeUp(Mobile from, int originalBodyValue)
		{
			// Restaurer le BodyValue original
			from.BodyValue = originalBodyValue;

			// Dégeler le joueur
			from.Frozen = false;

			from.SendMessage("Vous vous réveillez lentement.");
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
	}
}
