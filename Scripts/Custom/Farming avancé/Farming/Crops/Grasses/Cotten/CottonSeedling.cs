namespace Server.Items.Crops
{
    public class CottonSeedling : BaseSeedling
    {
        [Constructable]
        public CottonSeedling(Mobile sower)
            : base(Utility.RandomList(0xC51, 0xC52))
        {
            Movable = false;
            Name = "Cotton Seedling";
			Sower = sower;
            Init(this, typeof(CottonCrop));
        }
        
        public CottonSeedling(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
			writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            Init(this, typeof(CottonCrop));
		}
    }
}