namespace Server.Items.Crops
{
	public class TeaSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public TeaSeed() : this( 1 ) { }

		[Constructable]
		public TeaSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Tea Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(TeaSeedling));
		}

		public TeaSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class TeaSeedling : BaseSeedling
	{
		[Constructable]
		public TeaSeedling( Mobile sower ) : base( 0xCB6 )
		{
			Movable = false;
			Name = "Tea Seedling";
			Sower = sower;
			Init(this, typeof(TeaCrop));
		}
		
		public TeaSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(TeaCrop));
		}
	}

	public class TeaCrop : BaseCrop
	{
		[Constructable]
		public TeaCrop() : this(null) { }

		[Constructable]
		public TeaCrop( Mobile sower ) : base( 0xCE9 )
		{
			Movable = false;
			Name = "Tea Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 0xCE9, 0xCE9, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(TeaLeaves));
		}

		public TeaCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 10, 0xCE9, 0xCE9, true);
		}
	}
}