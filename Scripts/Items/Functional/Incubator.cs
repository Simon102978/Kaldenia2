using Server.Commands;
using Server.Gumps;
using Server.Multis;
using System;

namespace Server.Items
{
	[Flipable(0x407C, 0x407D)]
	public class Incubator : Container, ISecurable
	{
		//public override int LabelNumber => 1112479;  // un incubateur

		private SecureLevel m_Level;

		[CommandProperty(AccessLevel.GameMaster)]
		public SecureLevel Level
		{
			get { return m_Level; }
			set { m_Level = value; }
		}

		public override int DefaultGumpID => 1156;
		public override int DefaultDropSound => 66;

		[Constructable]
		public Incubator()
			: base(0x407C)
		{
			Name = "Une Vitrine";
			m_Level = SecureLevel.CoOwners;
		}

		public override bool OnDragDropInto(Mobile from, Item item, Point3D p)
		{
			return base.OnDragDropInto(from, item, p);
		}

		public override bool OnDragDrop(Mobile from, Item item)
		{
			return base.OnDragDrop(from, item);
		}

		public override bool CheckHold(Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight)
		{
			if (!BaseHouse.CheckSecured(this))
			{
				m.SendLocalizedMessage(1113711); // L'incubateur doit être sécurisé pour être utilisé, pas verrouillé.
				return false;
			}

			return base.CheckHold(m, item, message, checkItems, plusItems, plusWeight);
		}

		public Incubator(Serial serial)
			: base(serial)
		{
		}


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(1); // version 1

			writer.Write((int)m_Level);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			m_Level = (SecureLevel)reader.ReadInt();

			if (version == 0)
			{
				if (Items.Count > 0)
				{
		}
	}
		}
	}
}



