namespace Server.Items.Crops
{
	public class MiniLemonSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public MiniLemonSeed() : this( 1 ) { }

		[Constructable]
		public MiniLemonSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Miniture Lemon Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(MiniLemonSeedling));
		}

		public MiniLemonSeed( Serial serial ) : base( serial ) { }

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

	public class MiniLemonSeedling : BaseSeedling
	{
		[Constructable]
		public MiniLemonSeedling( Mobile sower ) : base( Utility.RandomList ( 0xCE9, 0xCEA ) )
		{
			Movable = false;
			Name = "Miniture Lemon Tree Seedling";
			Sower = sower;
			Init(this, typeof(MiniLemonCrop));
		}
		
		public MiniLemonSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(MiniLemonCrop));
		}
	}

	public class MiniLemonCrop : BaseCrop
	{
		[Constructable]
		public MiniLemonCrop() : this(null) { }

		[Constructable]
		public MiniLemonCrop( Mobile sower ) : base( 3273 )
		{
			Movable = false;
			Name = "Miniture Lemon Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 3273, 3273, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Lemon));
		}

		public MiniLemonCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 10, 3273, 3273, true);
		}
	}
}