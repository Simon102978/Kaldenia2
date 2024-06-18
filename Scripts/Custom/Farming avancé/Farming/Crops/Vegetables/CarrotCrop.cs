namespace Server.Items.Crops
{
	public class CarrotSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public CarrotSeed() : this( 1 ) { }

		[Constructable]
		public CarrotSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Carrot Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(CarrotSeedling));
		}

		public CarrotSeed( Serial serial ) : base( serial ) { }

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

	public class CarrotSeedling : BaseSeedling
	{
		[Constructable]
		public CarrotSeedling( Mobile sower ) : base( 0xC68 )
		{
			Movable = false;
			Name = "Carrot Seedling";
			Sower = sower;
			Init(this, typeof(CarrotCrop));
		}
		
		public CarrotSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(CarrotCrop));
		}
	}

	public class CarrotCrop : BaseCrop
	{
		[Constructable]
		public CarrotCrop() : this(null) { }

		[Constructable]
		public CarrotCrop( Mobile sower ) : base( 0xC76 )
		{
			Movable = false;
			Name = "Carrot Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 1, 0xC61, 0xC7C, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Carrot));
		}

		public CarrotCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 1, 0xC61, 0xC7C, true);
		}
	}
}