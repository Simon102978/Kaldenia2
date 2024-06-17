namespace Server.Items.Crops
{
	public class NightshadeSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public NightshadeSeed() : this( 1 ) { }

		[Constructable]
		public NightshadeSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Nightshade Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(NightshadeSeedling));
		}

		public NightshadeSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class NightshadeSeedling : BaseSeedling
	{
		[Constructable]
		public NightshadeSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Nightshade Seedling";
			Sower = sower;
			Init(this, typeof(NightshadeCrop));
		}
		
		public NightshadeSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(NightshadeCrop));
		}
	}

	public class NightshadeCrop : BaseCrop
	{
		[Constructable]
		public NightshadeCrop() : this(null) { }

		[Constructable]
		public NightshadeCrop( Mobile sower ) : base( 0x18E5 )
		{
			Movable = false;
			Name = "Nightshade Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 6, 0xCB5, 0x18E5, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Nightshade));
		}

		public NightshadeCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 6, 0xCB5, 0x18E5, true);
		}
	}
}