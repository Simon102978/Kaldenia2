namespace Server.Items.Crops
{
	public class SweetHopsSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public SweetHopsSeed() : this( 1 ) { }

		[Constructable]
		public SweetHopsSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Sweet Hops Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(SweetHopsSeedling));
		}

		public SweetHopsSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SweetHopsSeedling : BaseSeedling
	{
		[Constructable]
		public SweetHopsSeedling( Mobile sower ) : base( 0xC7E )
		{
			Movable = false;
			Name = "Sweet Hops Seedling";
			Hue = 0x30;
			Sower = sower;
			Init(this, typeof(SweetHopsCrop));
		}
		
		public SweetHopsSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SweetHopsCrop));
		}
	}

	public class SweetHopsCrop : BaseCrop
	{
		[Constructable]
		public SweetHopsCrop() : this(null) { }

		[Constructable]
		public SweetHopsCrop( Mobile sower ) : base( 0x1AA1 )
		{
			Movable = false;
			Name = "Sweet Hops Plant";
			Hue = 0x30;
			Sower = sower;
			Init(this, 20, 0x1AA1, 0x1AA1, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(SweetHops));
		}

		public SweetHopsCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 20, 0x1AA1, 0x1AA1, true);
		}
	}
}