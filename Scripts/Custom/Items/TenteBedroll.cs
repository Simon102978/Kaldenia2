using System.Linq;
using Server.Custom;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class BedrollTent : Item, IDyable
	{
		private int m_Hue;
		private const int MAX_TENTS = 1;
		private static int m_TentCount = 0;

		[Constructable]
		public BedrollTent() : this(0)
		{
		}

		[Constructable]
		public BedrollTent(int hue) : base(0xA57)
		{
			Name = "Tente";
			Movable = true;
			m_Hue = Hue = hue;
		}

		public BedrollTent(Serial serial) : base(serial)
		{
		}
		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;

			return true;
		}
		public override void OnDoubleClick(Mobile from)
		{
			if (from.InRange(GetWorldLocation(), 1))
			{
				if (from.Backpack != null && this.IsChildOf(from.Backpack))
				{
					if (from.HasGump(typeof(BedrollGump)))
					{
						from.CloseGump(typeof(BedrollGump));
					}
					else
					{
						from.SendGump(new BedrollGump(this));
					}
				}
				else
				{
					from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
				}
			}
			else
			{
				from.SendLocalizedMessage(500446); // That is too far away.
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write(m_Hue);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Hue = reader.ReadInt();
		}

		private class BedrollGump : BaseProjectMGump
		{
			private SmallTent m_Tent;
			
			private BedrollTent m_Bedroll;

			public BedrollGump(BedrollTent bedroll) : base("Menu Tente", 100, 60, false)
			{
				m_Bedroll = bedroll;

				Closable = true;
				Disposable = true;
				Dragable = true;
				Resizable = false;
				AddPage(0);

				AddButton(65, 85, 4005, 4007, 1, GumpButtonType.Reply, 0);
				AddLabel(105, 85, 0, "Ajouter une Tente");

				AddButton(65, 135, 4005, 4007, 2, GumpButtonType.Reply, 0);
				AddLabel(105, 135, 0, "Retirer une Tente");
			}

			public override void OnResponse(NetState sender, RelayInfo info)
			{
				Mobile from = sender.Mobile;

				if (from == null || info.ButtonID == 0)
					return;

				switch (info.ButtonID)
				{
					case 1:
						AddTent(from, m_Bedroll.m_Hue); // Utilisation du hue de BedrollTent courant
						break;
					case 2:
						RemoveTent(from);
						break;
				}
			}

			private void AddTent(Mobile from, int hue)
			{
		//		if (CustomUtility.IsInDungeonRegion(from.Location))
		//		{
		//			from.SendMessage("Vous ne pouvez pas poser de tente dans les donjons.");
		//			return;
		//		}

				if (m_Tent != null)
				{
					from.SendMessage("Vous ne pouvez poser qu'une seule tente à la fois.");
					return;
				}

				if (m_TentCount >= MAX_TENTS)
				{
					from.SendMessage("Vous avez atteint le nombre maximum de tentes.");
					return;
				}

				m_Tent = new SmallTent(); // Création d'une tente sans couleur
				m_Tent.Hue = m_Bedroll.Hue; // Définition de la couleur de la tente avec la couleur du Bedroll
				m_Tent.MoveToWorld(from.Location, from.Map);
				from.SendMessage("Vous ajoutez une tente.");
				m_TentCount++;
			}

			private void RemoveTent(Mobile from)
			{
				Item[] items = from.GetItemsInRange(15).ToArray();

				foreach (Item item in items)
				{
					if (item is SmallTent)
					{
						item.Delete();
						from.SendMessage("Vous retirez la tente.");
						m_TentCount--;
						return;
					}
				}

				from.SendMessage("Il n'y a plus de tente.");
			}
		}
	}
}
