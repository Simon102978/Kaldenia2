using Server.ContextMenus;
using System.Collections.Generic;
using Server.Custom.Packaging.Packages;

namespace Server.Items
{
	public class RecycleBag : Bag
	{
		[Constructable]
		public RecycleBag() : this(Utility.RandomBlueHue())
		{
		}

		[Constructable]
		public RecycleBag(int hue)
		{
			Name = "Sac de recyclage";
			Weight = 2.0;
			Hue = hue;
		}

		public RecycleBag(Serial s) : base(s)
		{
		}

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			base.GetContextMenuEntries(from, list);

			if (from.Alive)
				list.Add(new RecycleurEntry(this));
		}

		private class RecycleurEntry : ContextMenuEntry
		{
			private readonly RecycleBag m_Bag;

			public RecycleurEntry(RecycleBag bag) : base(6276)
			{
				m_Bag = bag;
			}

			public override void OnClick()
			{
				if (m_Bag.Deleted)
					return;

				Mobile from = Owner.From;

				if (from.CheckAlive())
					Recycle(from, m_Bag);
			}
		}

		public static void Recycle(Mobile from, Item item)
		{
			var scalar = 1.0;

			if (item is Gold)
			{
				from.SendMessage("Vous ne pouvez pas recycler des pièces d'or.");
				return;
			}
			else if (item is PieceArgent)
			{
				from.SendMessage("Vous ne pouvez pas recycler des pièces d'argent.");
				return;
			}
			else if (item.CheckNewbied() || item.Insured || item.PayedInsurance)
			{
				from.SendMessage($"Vous ne pouvez pas recycler {item.Name}, car cet item est assuré ou béni.");
				return;
			}
			else if ((item is BaseArmor && ((BaseArmor)item).PlayerConstructed) || (item is BaseWeapon && ((BaseWeapon)item).PlayerConstructed) || (item is BaseClothing && ((BaseClothing)item).PlayerConstructed))
				scalar = 0.1;
			else if (item is BaseContainer bag)
			{ 
				for (int i = bag.Items.Count - 1; i >=0; i--)
					Recycle(from, bag.Items[i]);
			}

			if (!(item is BaseContainer))
			{
				var newWeight = item.Weight * item.Amount * scalar;

				if (newWeight < 1)
				{
					from.SendMessage($"Vous ne pouvez pas recycler {item.Name}, car cet item n'est pas assez lourd.");
					return;
				}

				item.Delete();

				Item newItem = new Materiaux();
				newItem.Amount = (int)(newWeight);
				from.AddToBackpack(newItem);
				from.UpdateTotals();
			}
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
