using Server.Network;
using System;

namespace Server.Items
{
	public abstract class BaseManaPotion : BasePotion
	{
		public BaseManaPotion(PotionEffect effect)
			: base(0xF0C, effect)
		{
		}

		public BaseManaPotion(Serial serial)
			: base(serial)
		{
		}

		public abstract int MinManaHeal { get; }
		public abstract int MaxManaHeal { get; }
		public abstract double Delay { get; }

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

		public void DoManaHeal(Mobile from)
		{
			var toHeal = Utility.RandomMinMax(Scale(from, MinManaHeal), Scale(from, MaxManaHeal));
			from.Mana += toHeal;
			from.SendMessage($"Vous avez régénérez {0} mana.", toHeal.ToString()); // You have had ~1_MANA_RESTORED~ mana restored.
		}

		public override void Drink(Mobile from)
		{
			if (from.Mana < from.ManaMax)
			{
				if (from.Poisoned || MortalStrike.IsWounded(from))
				{
					from.LocalOverheadMessage(MessageType.Regular, 0x22, 1005000); // You can not heal yourself in your current state.
				}
				else if (from.CanBeginAction(typeof(BaseManaPotion)))
				{
					from.BeginAction(typeof(BaseManaPotion));

					DoManaHeal(from);
					PlayDrinkEffect(from);
					Consume();

					Timer.DelayCall(TimeSpan.FromSeconds(Delay), m => m.EndAction(typeof(BaseManaPotion)), from);
				}
				else
				{
					from.SendMessage("Vous devez attendre 10 secondes avant de reprendre une potion de Mana."); // You must wait 10 seconds before using another healing potion.
				}
			}
			else
			{
				from.SendLocalizedMessage(1049546); // You decide against drinking this potion, as you are already at full mana.
			}
		}
	}
}
