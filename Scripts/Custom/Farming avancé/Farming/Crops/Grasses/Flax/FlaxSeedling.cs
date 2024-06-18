namespace Server.Items.Crops
{
    public class FlaxSeedling : BaseSeedling
    {
        [Constructable]
        public FlaxSeedling(Mobile sower)
            : base(0x1A99)
        {
            Movable = false;
            Name = "Flax Seedling";
			Sower = sower;
			Init(this, typeof(FlaxCrop));
		}
		
		public FlaxSeedling(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(FlaxCrop));
		}
	}
}