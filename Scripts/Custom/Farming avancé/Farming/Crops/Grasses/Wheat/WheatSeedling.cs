namespace Server.Items.Crops
{
	public class WheatSeedling : BaseSeedling
	{
		[Constructable]
		public WheatSeedling( Mobile sower ) : base( Utility.RandomList ( 0xDAE, 0xDAF ) )
		{
            Name = "Wheat Seedling";
			Movable = false;
			Sower = sower;
			Init(this, typeof(WheatCrop));
		}
		
		public WheatSeedling(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(WheatCrop));
		}
	}
}