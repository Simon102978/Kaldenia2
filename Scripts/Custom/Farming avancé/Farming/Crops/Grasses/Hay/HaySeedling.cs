namespace Server.Items.Crops
{
	public class HaySeedling : BaseSeedling
	{
		[Constructable]
		public HaySeedling( Mobile sower ) : base( Utility.RandomList ( 0xDAE, 0xDAF ) )
		{
			Movable = false;
			Name = "Hay Seedling";
			Sower = sower;
			Init(this, typeof(HayCrop));
		}
		
		public HaySeedling(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(HayCrop));
		}
	}
}