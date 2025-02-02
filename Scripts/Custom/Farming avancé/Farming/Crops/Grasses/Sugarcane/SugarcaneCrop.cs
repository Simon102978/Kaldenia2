namespace Server.Items.Crops
{
	public class SugarcaneCrop : BaseCrop
	{

		public override double MinSkill{ get { return 10.0; } }

		public override double MaxSkill{ get { return 40.0; } }
		[Constructable]
		public SugarcaneCrop() : this(null) { }

		[Constructable]
		public SugarcaneCrop( Mobile sower ) : base( Utility.RandomList ( 0xC5A, 0xC5B ) )
		{
			Movable = false;
			Name = "Sugar Cane Plant";
			Hue = 0x237;
			Sower = sower;
			Init(this, 2, Utility.RandomList(0xC55, 0xC56, 0xC57, 0xC59), Utility.RandomList(0xC5A, 0xC5B), false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Sugarcane));
		}

		public SugarcaneCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 2, Utility.RandomList(0xC55, 0xC56, 0xC57, 0xC59), Utility.RandomList(0xC5A, 0xC5B), true);
		}
	}
}