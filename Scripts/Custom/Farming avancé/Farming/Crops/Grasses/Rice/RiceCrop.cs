namespace Server.Items.Crops
{
	public class RiceCrop : BaseCrop
	{
		[Constructable]
		public RiceCrop() : this(null) { }

		[Constructable]
		public RiceCrop( Mobile sower ) : base( Utility.RandomList ( 0xC5A, 0xC5B ) )
		{
			Movable = false;
			Name = "Rice Plant";
			Hue = 0x303;
			Sower = sower;
			Init(this, 2, Utility.RandomList(0xC55, 0xC56, 0xC57, 0xC59), Utility.RandomList(0xC58, 0xC5A, 0xC5B), false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(RiceSheath));
		}

		public RiceCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 2, Utility.RandomList(0xC55, 0xC56, 0xC57, 0xC59), Utility.RandomList(0xC58, 0xC5A, 0xC5B), true);
		}
	}
}