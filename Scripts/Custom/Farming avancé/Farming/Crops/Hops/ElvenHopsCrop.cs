namespace Server.Items.Crops
{
	public class ElvenHopsSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public ElvenHopsSeed() : this( 1 ) { }

		[Constructable]
		public ElvenHopsSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Elven Hops Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(ElvenHopsSeedling));
		}

		public ElvenHopsSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class ElvenHopsSeedling : BaseSeedling
	{
		[Constructable]
		public ElvenHopsSeedling( Mobile sower ) : base( 0xC7E )
		{
			Movable = false;
			Name = "Elven Hops Seedling";
			Hue = 0x17;
			Sower = sower;
			Init(this, typeof(ElvenHopsCrop));
		}
		
		public ElvenHopsSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(ElvenHopsCrop));
		}
	}

	public class ElvenHopsCrop : BaseCrop
	{
		[Constructable]
		public ElvenHopsCrop() : this(null) { }

		[Constructable]
		public ElvenHopsCrop( Mobile sower ) : base( 0x1AA1 )
		{
			Movable = false;
			Name = "Elven Hops Plant";
			Hue = 0x17;
			Sower = sower;
			Init(this, 20, 0x1AA1, 0x1AA1, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(ElvenHops));
		}

		public ElvenHopsCrop(Serial serial) : base(serial) { }

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