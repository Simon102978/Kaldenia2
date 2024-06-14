namespace Server.Items.Crops
{
	public class GinsengSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public GinsengSeed() : this( 1 ) { }

		[Constructable]
		public GinsengSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Ginseng Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(GinsengSeedling));
		}

		public GinsengSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GinsengSeedling : BaseSeedling
	{
		[Constructable]
		public GinsengSeedling( Mobile sower ) : base( 0xC7E )
		{
			Movable = false;
			Name = "Ginseng Seedling";
			Sower = sower;
			Init(this, typeof(GinsengCrop));
		}
		
		public GinsengSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(GinsengCrop));
		}
	}

	public class GinsengCrop : BaseCrop
	{
		[Constructable]
		public GinsengCrop() : this(null) { }

		[Constructable]
		public GinsengCrop( Mobile sower ) : base( 0x18EA )
		{
			Movable = false;
			Name = "Ginseng Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 6, 0xC7E, 0x18EA, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Ginseng));
		}

		public GinsengCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 6, 0xC7E, 0x18EA, true);
		}
	}
}