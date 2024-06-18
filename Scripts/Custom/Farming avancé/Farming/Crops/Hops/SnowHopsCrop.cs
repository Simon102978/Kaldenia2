namespace Server.Items.Crops
{
	public class SnowHopsSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public SnowHopsSeed() : this( 1 ) { }

		[Constructable]
		public SnowHopsSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Snow Hops Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(SnowHopsSeedling));
		}

		public SnowHopsSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SnowHopsSeedling : BaseSeedling
	{
		[Constructable]
		public SnowHopsSeedling( Mobile sower ) : base( 0xC7E )
		{
			Movable = false;
			Name = "Snow Hops Seedling";
			Hue = 0x481;
			Sower = sower;
			Init(this, typeof(SnowHopsCrop));
		}
		
		public SnowHopsSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SnowHopsCrop));
		}
	}

	public class SnowHopsCrop : BaseCrop
	{
		[Constructable]
		public SnowHopsCrop() : this(null) { }

		[Constructable]
		public SnowHopsCrop( Mobile sower ) : base( 0x1AA1 )
		{
			Movable = false;
			Name = "Snow Hops Plant";
			Hue = 0x481;
			Sower = sower;
			Init(this, 20, 0x1AA1, 0x1AA1, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(SnowHops));
		}

		public SnowHopsCrop(Serial serial) : base(serial) { }

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