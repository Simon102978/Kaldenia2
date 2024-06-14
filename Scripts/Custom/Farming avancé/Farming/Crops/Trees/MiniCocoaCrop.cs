namespace Server.Items.Crops
{
	public class MiniCocoaSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public MiniCocoaSeed() : this( 1 ) { }

		[Constructable]
		public MiniCocoaSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Miniture Cocoa Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(MiniCocoaSeedling));
		}

		public MiniCocoaSeed( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class MiniCocoaSeedling : BaseSeedling
	{
		[Constructable]
		public MiniCocoaSeedling( Mobile sower ) : base( Utility.RandomList ( 0xCE9, 0xCEA ) )
		{
			Movable = false;
			Name = "Miniture Cocoa Tree Seedling";
			Sower = sower;
			Init(this, typeof(MiniCocoaCrop));
		}
		
		public MiniCocoaSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(MiniCocoaCrop));
		}
	}

	public class MiniCocoaCrop : BaseCrop
	{
		[Constructable]
		public MiniCocoaCrop() : this(null) { }

		[Constructable]
		public MiniCocoaCrop( Mobile sower ) : base( 3273 )
		{
			Movable = false;
			Name = "Miniture Cocoa Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 3273, 3273, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(CocoaBean));
		}

		public MiniCocoaCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 10, 3273, 3273, true);
		}
	}
}