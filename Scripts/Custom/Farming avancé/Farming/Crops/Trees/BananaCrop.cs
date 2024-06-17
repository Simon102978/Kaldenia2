namespace Server.Items.Crops
{
	public class BananaSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public BananaSeed() : this( 1 ) { }

		[Constructable]
		public BananaSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Banana Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(BananaSeedling));
		}

		public BananaSeed( Serial serial ) : base( serial ) { }

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

	public class BananaSeedling : BaseSeedling
	{
		[Constructable]
		public BananaSeedling( Mobile sower ) : base( 0xCAA )
		{
			Movable = false;
			Name = "Banana Tree Seedling";
			Sower = sower;
			Init(this, typeof(BananaCrop));
		}
		
		public BananaSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(BananaCrop));
		}
	}

	public class BananaCrop : BaseCrop
	{
		[Constructable]
		public BananaCrop() : this(null) { }

		[Constructable]
		public BananaCrop( Mobile sower ) : base( 0xCAA )
		{
			Movable = false;
			Name = "Banana Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 12, 0xCAA, 0xCAA, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Banana));
		}

		public BananaCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 12, 0xCAA, 0xCAA, true);
		}
	}
}