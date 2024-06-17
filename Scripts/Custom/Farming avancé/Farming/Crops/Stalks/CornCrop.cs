namespace Server.Items.Crops
{
	public class CornSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public CornSeed() : this( 1 ) { }

		[Constructable]
		public CornSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Corn Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(CornSeedling));
		}

		public CornSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CornSeedling : BaseSeedling
	{
		[Constructable]
		public CornSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Corn Seedling";
			Sower = sower;
			Init(this, typeof(CornCrop));
		}
		
		public CornSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(CornCrop));
		}
	}

	public class CornCrop : BaseCrop
	{
		[Constructable]
		public CornCrop() : this(null) { }

		[Constructable]
		public CornCrop( Mobile sower ) : base( 0xC7D )
		{
			Movable = false;
			Name = "Corn Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 2, 0xC7E, 0xC7D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Corn));
		}

		public CornCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 2, 0xC7E, 0xC7D, true);
		}
	}
}