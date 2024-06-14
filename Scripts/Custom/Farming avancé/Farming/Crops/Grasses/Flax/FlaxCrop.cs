namespace Server.Items.Crops
{
    public class FlaxCrop : BaseCrop
    {
        [Constructable]
        public FlaxCrop() : this(null) { }

        [Constructable]
        public FlaxCrop(Mobile sower) : base(0x1A9B)
        {
            Movable = false;
            Name = "Flax Plant";
            Hue = 0x000;
			Sower = sower;
			Init(this, 5, 0x1A9A, 0x1A9B, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Flax));
		}

		public FlaxCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 5, 0x1A9A, 0x1A9B, true);
		}
	}
}