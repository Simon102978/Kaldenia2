namespace Server.Items.Crops
{
	public class TanMushroomSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public TanMushroomSeed() : this( 1 ) { }

		[Constructable]
		public TanMushroomSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Tan Mushroom Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(TanMushroomSeedling));
		}

		public TanMushroomSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class TanMushroomSeedling : BaseSeedling
	{
		[Constructable]
		public TanMushroomSeedling( Mobile sower ) : base( 0xD12 )
		{
			Movable = false;
			Name = "Tan Mushroom Seedling";
			Sower = sower;
			Init(this, typeof(TanMushroomCrop));
		}
		
		public TanMushroomSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(TanMushroomCrop));
		}
	}

	public class TanMushroomCrop : BaseCrop
	{
		[Constructable]
		public TanMushroomCrop() : this(null) { }

		[Constructable]
		public TanMushroomCrop( Mobile sower ) : base( 0xD13 )
		{
			Movable = false;
			Name = "Tan Mushroom Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 6, 0xD13, 0xD13, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(TanMushroom));
		}

		public TanMushroomCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 6, 0xD13, 0xD13, true);
		}
	}
}