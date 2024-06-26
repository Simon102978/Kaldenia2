using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	public class TestStone : Item
	{
		[Constructable]
		public TestStone() : base(3796)
		{
			Movable = false;
			Name = "Test";
		}

		public override void OnDoubleClickDead(Mobile from)
		{
			OnDoubleClick(from);
		}

		public override void OnDoubleClick(Mobile from)
		{
			var pm = from as CustomPlayerMobile;
			from.CloseGump(typeof(TestStoneGump));
			from.SendGump(new TestStoneGump(pm));
		}

		public TestStone(Serial serial) : base(serial)
		{
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

