namespace Server.Items.Crops
{
	public class GreenSquashSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public GreenSquashSeed() : this( 1 ) { }

		[Constructable]
		public GreenSquashSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Green Squash Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(GreenSquashSeedling));
		}

		public GreenSquashSeed( Serial serial ) : base( serial ) { }

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

	public class GreenSquashSeedling : BaseSeedling
	{
		[Constructable]
		public GreenSquashSeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Green Squash Seedling";
			Sower = sower;
			Init(this, typeof(GreenSquashCrop));
		}
		
		public GreenSquashSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(GreenSquashCrop));
		}
	}

	public class GreenSquashCrop : BaseCrop
	{
		[Constructable]
		public GreenSquashCrop() : this(null) { }

		[Constructable]
		public GreenSquashCrop( Mobile sower ) : base( 3164 )
		{
			Movable = false;
			Name = "Green Squash Plant";
			Hue = 2979;
			Sower = sower;
			Init(this, 2, 3164, 3164, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(GreenSquash));
		}

		public GreenSquashCrop(Serial serial) : base(serial) { }

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