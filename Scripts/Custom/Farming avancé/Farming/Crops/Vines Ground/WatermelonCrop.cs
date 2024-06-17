namespace Server.Items.Crops
{
	public class WatermelonSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public WatermelonSeed() : this( 1 ) { }

		[Constructable]
		public WatermelonSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Watermelon Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(WatermelonSeedling));
		}

		public WatermelonSeed( Serial serial ) : base( serial ) { }

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

	public class WatermelonSeedling : BaseSeedling
	{
		[Constructable]
		public WatermelonSeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Watermelon Seedling";
			Sower = sower;
			Init(this, typeof(WatermelonCrop));
		}
		
		public WatermelonSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(WatermelonCrop));
		}
	}

	public class WatermelonCrop : BaseCrop
	{
		[Constructable]
		public WatermelonCrop() : this(null) { }

		[Constructable]
		public WatermelonCrop( Mobile sower ) : base( 3164 )
		{
			Movable = false;
			Name = "Watermelon Plant";
			Hue = 2955;
			Sower = sower;
			Init(this, 2, 3164, 3164, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Watermelon));
		}

		public WatermelonCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 2, 3164, 3164, true);
		}
	}
}