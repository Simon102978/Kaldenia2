using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class MapleTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed => new MapleTreeDeed();

		[Constructable]
		public MapleTreeAddon(bool trunk)
		{
			int[] possibleItemIDs = { 0x2478, 0x2479, 0x247A, 0x247B, 0x247C };
			int randomItemID = possibleItemIDs[Utility.Random(possibleItemIDs.Length)];

			AddComponent(new LocalizedAddonComponent(0x247D, 1071104), 0, 0, 0);

			if (!trunk)
				AddComponent(new LocalizedAddonComponent(randomItemID, 1071104), 0, 0, 0);
		}

		public MapleTreeAddon(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.WriteEncodedInt(0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadEncodedInt();
		}
	}

	public class MapleTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon => new MapleTreeAddon(m_Trunk);
		public override int LabelNumber => 1071104;  // Maple Tree

		private bool m_Trunk;

		[Constructable]
		public MapleTreeDeed()
		{
			Name = "un érable";
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (IsChildOf(from.Backpack))
			{
				from.CloseGump(typeof(InternalGump));
				from.SendGump(new InternalGump(this));
			}
			else
				from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.
		}
		private void SendTarget(Mobile m)
		{
			base.OnDoubleClick(m);
		}
		public MapleTreeDeed(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.WriteEncodedInt(0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadEncodedInt();
		}

		private class InternalGump : Gump
		{
			private readonly MapleTreeDeed m_Deed;

			public InternalGump(MapleTreeDeed deed) : base(60, 36)
			{
				m_Deed = deed;

				AddPage(0);

				AddBackground(0, 0, 273, 324, 0x13BE);
				AddImageTiled(10, 10, 253, 20, 0xA40);
				AddImageTiled(10, 40, 253, 244, 0xA40);
				AddImageTiled(10, 294, 253, 20, 0xA40);
				AddAlphaRegion(10, 10, 253, 304);
				AddButton(10, 294, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
				AddHtmlLocalized(45, 296, 450, 20, 1060051, 0x7FFF, false, false); // CANCEL
				AddHtmlLocalized(14, 12, 273, 20, 1076744, 0x7FFF, false, false); // Please select your vanity position.

				AddPage(1);

				AddButton(19, 49, 0x845, 0x846, 1, GumpButtonType.Reply, 0);
				AddHtmlLocalized(44, 47, 213, 20, 1075386, 0x7FFF, false, false); // South
				AddButton(19, 73, 0x845, 0x846, 2, GumpButtonType.Reply, 0);
				AddHtmlLocalized(44, 71, 213, 20, 1075387, 0x7FFF, false, false); // East
			}

			public override void OnResponse(NetState sender, RelayInfo info)
			{
				if (m_Deed == null || m_Deed.Deleted || info.ButtonID == 0)
					return;

				m_Deed.m_Trunk = (info.ButtonID != 1);
				m_Deed.SendTarget(sender.Mobile);
			}
		}
	}
}
