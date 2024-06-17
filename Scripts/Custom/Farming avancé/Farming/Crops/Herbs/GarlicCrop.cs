namespace Server.Items.Crops
{
	public class GarlicSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public GarlicSeed() : this( 1 ) { }

		[Constructable]
		public GarlicSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Garlic Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(GarlicSeedling));
		}

		public GarlicSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GarlicSeedling : BaseSeedling
	{
		[Constructable]
		public GarlicSeedling( Mobile sower ) : base( 0xC68 )
		{
			Movable = false;
			Name = "Garlic Seedling";
			Sower = sower;
			Init(this, typeof(GarlicCrop));
		}
		
		public GarlicSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(GarlicCrop));
		}
	}

	public class GarlicCrop : BaseCrop
	{
		[Constructable]
		public GarlicCrop() : this(null) { }

		[Constructable]
		public GarlicCrop( Mobile sower ) : base( 0xC6F )
		{
			Movable = false;
			Name = "Garlic Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 6, 0xC69, 0xC6F, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Garlic));
		}

		public GarlicCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 6, 0xC69, 0xC6F, true);
		}
	}
}