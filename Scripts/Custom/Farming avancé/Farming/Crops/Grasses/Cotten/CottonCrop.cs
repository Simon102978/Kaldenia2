namespace Server.Items.Crops
{
    public class CottonCrop : BaseCrop
    {
        [Constructable]
        public CottonCrop() : this(null)
        {
        }

        [Constructable]
        public CottonCrop(Mobile sower) : base(Utility.RandomList(0xC4F, 0xC50))
        {
            Movable = false;
            Name = "Cotton Plant";
            Hue = 0x000;
			Sower = sower;
			Init(this, 5, Utility.RandomList(0xC53, 0xC54), Utility.RandomList(0xC4F, 0xC50), false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Cotton));
		}

		public CottonCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 5, Utility.RandomList(0xC53, 0xC54), Utility.RandomList(0xC4F, 0xC50), true);
		}
    }
}