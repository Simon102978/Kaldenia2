namespace Server.Items.Crops
{
	public class SugarcaneSeedling : BaseSeedling
	{
		[Constructable]
		public SugarcaneSeedling( Mobile sower ) : base( Utility.RandomList ( 0x1EBE, 0x1EBF ) )
		{
			Movable = false;
			Name = "Sugar Cane Seedling";
			Hue = 0x238;
			Sower = sower;
			Init(this, typeof(SugarcaneCrop));
		}
		
		public SugarcaneSeedling(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SugarcaneCrop));
		}
	}
}