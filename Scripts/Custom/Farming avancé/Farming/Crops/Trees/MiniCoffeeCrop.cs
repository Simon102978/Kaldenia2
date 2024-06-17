namespace Server.Items.Crops
{
	public class MiniCoffeeSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public MiniCoffeeSeed() : this( 1 ) { }

		[Constructable]
		public MiniCoffeeSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Miniture Coffee Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(MiniCoffeeSeedling));
		}

		public MiniCoffeeSeed( Serial serial ) : base( serial ) { }

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

	public class MiniCoffeeSeedling : BaseSeedling
	{
		[Constructable]
		public MiniCoffeeSeedling( Mobile sower ) : base( Utility.RandomList ( 0xCE9, 0xCEA ) )
		{
			Movable = false;
			Name = "Miniture Coffee Tree Seedling";
			Sower = sower;
			Init(this, typeof(MiniCoffeeCrop));
		}
		
		public MiniCoffeeSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(MiniCoffeeCrop));
		}
	}

	public class MiniCoffeeCrop : BaseCrop
	{
		[Constructable]
		public MiniCoffeeCrop() : this(null) { }

		[Constructable]
		public MiniCoffeeCrop( Mobile sower ) : base( 3273 )
		{
			Movable = false;
			Name = "Miniture Coffee Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 3273, 3273, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(CoffeeBean));
		}

		public MiniCoffeeCrop(Serial serial) : base(serial) { }

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