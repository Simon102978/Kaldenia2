namespace Server.Items.Crops
{
	public class StrawberrySeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public StrawberrySeed() : this( 1 ) { }

		[Constructable]
		public StrawberrySeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Strawberry Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(StrawberrySeedling));
		}

		public StrawberrySeed( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class StrawberrySeedling : BaseSeedling
	{
		[Constructable]
		public StrawberrySeedling( Mobile sower ) : base( Utility.RandomList ( 0x1A99, 0x1A9A ) )
		{
			Movable = false;
			Name = "Strawberry Seedling";
			Sower = sower;
			Init(this, typeof(StrawberryCrop));
		}
		
		public StrawberrySeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(StrawberryCrop));
		}
	}

	public class StrawberryCrop : BaseCrop
	{
		[Constructable]
		public StrawberryCrop() : this(null) { }

		[Constructable]
		public StrawberryCrop( Mobile sower ) : base( 0x1A9B )
		{
			Movable = false;
			Name = "Strawberry Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, Utility.RandomList(0xD09, 0xD0A), 0xD0B, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Strawberry));
		}

		public StrawberryCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 10, Utility.RandomList(0x1A99, 0x1A9A), 0x1A9B, true);
		}
	}
}