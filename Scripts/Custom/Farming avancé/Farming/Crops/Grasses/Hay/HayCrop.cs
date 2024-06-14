namespace Server.Items.Crops
{
    public class HayCrop : BaseCrop
    {
        [Constructable]
        public HayCrop() : this(null) { }

        [Constructable]
        public HayCrop(Mobile sower) : base(Utility.RandomList(0xC58, 0xC5A, 0xC5B))
        {
            Movable = false;
            Name = "Hay Plant";
            Hue = 0x000;
			Sower = sower;
			Init(this, 2, Utility.RandomList(0xC55, 0xC56, 0xC57, 0xC59), Utility.RandomList(0xC58, 0xC5A, 0xC5B), false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Hay));
		}

		public HayCrop(Serial serial) : base(serial) { }

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