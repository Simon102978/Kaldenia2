namespace Server.Items.Crops
{
	public class OatsSeedling : BaseSeedling
	{
		[Constructable]
		public OatsSeedling( Mobile sower ) : base( Utility.RandomList ( 0xDAE, 0xDAF ) )
		{
			Movable = false;
			Name = "Oats Seedling";
			Sower = sower;
			Init(this, typeof(OatsCrop));
		}
		
		public OatsSeedling(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(OatsCrop));
		}
	}
}