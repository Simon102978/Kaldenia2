namespace Server.Items.Crops
{
	public class TanGingerSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public TanGingerSeed() : this( 1 ) { }

		[Constructable]
		public TanGingerSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Tan Ginger Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(TanGingerSeedling));
		}

		public TanGingerSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class TanGingerSeedling : BaseSeedling
	{
		[Constructable]
		public TanGingerSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Tan Ginger Seedling";
			Sower = sower;
			Init(this, typeof(TanGingerCrop));
		}
		
		public TanGingerSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(TanGingerCrop));
		}
	}

	public class TanGingerCrop : BaseCrop
	{
		[Constructable]
		public TanGingerCrop() : this(null) { }

		[Constructable]
		public TanGingerCrop( Mobile sower ) : base( 0xCC7 )
		{
			Movable = false;
			Name = "Tan Ginger Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 6, 0xCB5, 0xCC7, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(TanGinger));
		}

		public TanGingerCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 6, 0xCB5, 0xCC7, true);
		}
	}
}