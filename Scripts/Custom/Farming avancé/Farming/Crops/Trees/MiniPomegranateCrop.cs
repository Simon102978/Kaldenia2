namespace Server.Items.Crops
{
	public class MiniPomegranateSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public MiniPomegranateSeed() : this( 1 ) { }

		[Constructable]
		public MiniPomegranateSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Miniture Pomegranate Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(MiniPomegranateSeedling));
		}

		public MiniPomegranateSeed( Serial serial ) : base( serial ) { }

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

	public class MiniPomegranateSeedling : BaseSeedling
	{
		[Constructable]
		public MiniPomegranateSeedling( Mobile sower ) : base( Utility.RandomList ( 0xCE9, 0xCEA ) )
		{
			Movable = false;
			Name = "Miniture Pomegranate Tree Seedling";
			Sower = sower;
			Init(this, typeof(MiniPomegranateCrop));
		}
		
		public MiniPomegranateSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(MiniPomegranateCrop));
		}
	}

	public class MiniPomegranateCrop : BaseCrop
	{
		[Constructable]
		public MiniPomegranateCrop() : this(null) { }

		[Constructable]
		public MiniPomegranateCrop( Mobile sower ) : base( 3273 )
		{
			Movable = false;
			Name = "Miniture Pomegranate Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 3273, 3273, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Pomegranate));
		}

		public MiniPomegranateCrop(Serial serial) : base(serial) { }

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