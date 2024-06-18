namespace Server.Items.Crops
{
	public class BitterHopsSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public BitterHopsSeed() : this( 1 ) { }

		[Constructable]
		public BitterHopsSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Bitter Hops Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(BitterHopsSeedling));
		}

		public BitterHopsSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BitterHopsSeedling : BaseSeedling
	{
		[Constructable]
		public BitterHopsSeedling( Mobile sower ) : base( 0xC7E )
		{
			Movable = false;
			Name = "Bitter Hops Seedling";
			Sower = sower;
			Init(this, typeof(BitterHopsCrop));
		}
		
		public BitterHopsSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(BitterHopsCrop));
		}
	}

	public class BitterHopsCrop : BaseCrop
	{
		[Constructable]
		public BitterHopsCrop() : this(null) { }

		[Constructable]
		public BitterHopsCrop( Mobile sower ) : base( 0x1AA1 )
		{
			Movable = false;
			Name = "Bitter Hops Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 20, 0x1AA1, 0x1AA1, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(BitterHops));
		}

		public BitterHopsCrop(Serial serial) : base(serial) { }

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