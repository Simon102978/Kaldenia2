namespace Server.Items.Crops
{
	public class VanillaSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public VanillaSeed() : this( 1 ) { }

		[Constructable]
		public VanillaSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Vanilla Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(VanillaSeedling));
		}

        public VanillaSeed(Serial serial) : base(serial) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class VanillaSeedling : BaseSeedling
	{
		[Constructable]
		public VanillaSeedling( Mobile sower ) : base( 3973 )
		{
			Movable = false;
            Name = "Vanilla Seedling";
			Sower = sower;
			Init(this, typeof(VanillaCrop));
		}
		
        public VanillaSeedling(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(VanillaCrop));
		}
	}

	public class VanillaCrop : BaseCrop
	{
		[Constructable]
		public VanillaCrop() : this(null) { }

		[Constructable]
		public VanillaCrop( Mobile sower ) : base( 0x2BE3 )
		{
			Movable = false;
			Name = "Vanilla Plant";
			Hue = 0;
			Sower = sower;
			Init(this, 2, 0x2BE3, 0x2BE3, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Vanilla));
		}

		public VanillaCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 2, 0x2BE3, 0x2BE3, true);
		}
	}
}