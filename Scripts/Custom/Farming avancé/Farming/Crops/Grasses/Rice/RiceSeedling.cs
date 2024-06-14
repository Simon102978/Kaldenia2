namespace Server.Items.Crops
{
	public class RiceSeedling : BaseSeedling
	{
		[Constructable]
		public RiceSeedling( Mobile sower ) : base( Utility.RandomList ( 0x1EBE, 0x1EBF ) )
		{
			Movable = false;
			Name = "Rice Seedling";
			Hue = Utility.RandomList ( 0xC5A, 0xC5B );
			Sower = sower;
			Init(this, typeof(RiceCrop));
		}
		
		public RiceSeedling(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(RiceCrop));
		}
	}
}