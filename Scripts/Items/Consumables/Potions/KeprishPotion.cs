using System;
using Server;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;

namespace Server.Items
{
	public class KeprishPotion : BasePotion
	{
		private static readonly TimeSpan TransformationDuration = TimeSpan.FromMinutes(5);

		[Constructable]
		public KeprishPotion() : base(0xF0E, PotionEffect.Keprish)
		{
			Hue = 0x8FD; // Couleur personnalisée pour la potion Keprish
			Name = "Potion Keprish";
		}

		public KeprishPotion(Serial serial) : base(serial)
		{
		}

		public override void Drink(Mobile from)
		{
			if (from is CustomPlayerMobile customPlayer)
			{
				if (customPlayer.RaceRestreinte)
				{
					TransformToBloodElemental(customPlayer);
				}
				else
				{
					ApplyDeadlyPoison(customPlayer);
				}
			}
			else
			{
				from.SendMessage("Cette potion n'a aucun effet sur vous.");
			}

			this.Consume();
		}

		private void TransformToBloodElemental(Mobile from)
		{
			if (!from.CanBeginAction(typeof(KeprishPotion)))
			{
				from.SendLocalizedMessage(1005559); // Cette transformation est déjà active.
				return;
			}

			from.BodyMod = 159; // ID du corps du Blood Elemental
			from.HueMod = 0x21; // Hue du Blood Elemental

			from.SendMessage("Vous vous transformez en Blood Elemental pour 5 minutes!");

			Timer.DelayCall(TransformationDuration, () =>
			{
				from.BodyMod = 0;
				from.HueMod = -1;
				from.SendMessage("Vous reprenez votre forme normale.");
				from.EndAction(typeof(KeprishPotion));
			});

			from.BeginAction(typeof(KeprishPotion));
		}

		private void ApplyDeadlyPoison(Mobile from)
		{
			from.ApplyPoison(from, Poison.Deadly);
			from.SendLocalizedMessage(1010512); // Un poison mortel coule dans vos veines!
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}
