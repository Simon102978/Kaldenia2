namespace Server.Items.Crops
{
	public class CoconutSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public CoconutSeed() : this( 1 ) { }

		[Constructable]
		public CoconutSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Coconut Palm Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(CoconutSeedling));
		}

		public CoconutSeed( Serial serial ) : base( serial ) { }

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

	public class CoconutSeedling : BaseSeedling
	{
		[Constructable]
		public CoconutSeedling( Mobile sower ) : base( 0xC95 )
		{
			Movable = false;
			Name = "Coconut Palm Seedling";
			Sower = sower;
			Init(this, typeof(CoconutCrop));
		}
		
		public CoconutSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(CoconutCrop));
		}
	}

	public class CoconutCrop : BaseCrop
	{
		[Constructable]
		public CoconutCrop() : this(null) { }

		[Constructable]
		public CoconutCrop( Mobile sower ) : base( 0xC95 )
		{
			Movable = false;
			Name = "Coconut Palm Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 3, 0xC95, 0xC95, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Coconut));
		}

		public CoconutCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 3, 0xC95, 0xC95, true);
		}
	}
}