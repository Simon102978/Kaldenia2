namespace Server.Items.Crops
{
	public class LettuceSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public LettuceSeed() : this( 1 ) { }

		[Constructable]
		public LettuceSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Lettuce Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(LettuceSeedling));
		}

		public LettuceSeed( Serial serial ) : base( serial ) { }

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

	public class LettuceSeedling : BaseSeedling
	{
		[Constructable]
		public LettuceSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Lettuce Seedling";
			Sower = sower;
			Init(this, typeof(LettuceCrop));
		}
		
		public LettuceSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(LettuceCrop));
		}
	}

	public class LettuceCrop : BaseCrop
	{
		[Constructable]
		public LettuceCrop() : this(null) { }

		[Constructable]
		public LettuceCrop( Mobile sower ) : base( 0xC70 )
		{
			Movable = false;
			Name = "Lettuce Plant";
			Hue = 0x1D3;
			Sower = sower;
			Init(this, 1, 0xC61, 0xC70, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Lettuce));
		}

		public LettuceCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 1, 0xC61, 0xC70, true);
		}
	}
}