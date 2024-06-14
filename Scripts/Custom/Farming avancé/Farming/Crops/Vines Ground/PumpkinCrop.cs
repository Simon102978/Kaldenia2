namespace Server.Items.Crops
{
	public class PumpkinSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public PumpkinSeed() : this( 1 ) { }

		[Constructable]
		public PumpkinSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Pumpkin Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(PumpkinSeedling));
		}

		public PumpkinSeed( Serial serial ) : base( serial ) { }

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

	public class PumpkinSeedling : BaseSeedling
	{
		[Constructable]
		public PumpkinSeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Pumpkin Seedling";
			Sower = sower;
			Init(this, typeof(PumpkinCrop));
		}
		
		public PumpkinSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(PumpkinCrop));
		}
	}

	public class PumpkinCrop : BaseCrop
	{
		[Constructable]
		public PumpkinCrop() : this(null) { }

		[Constructable]
		public PumpkinCrop( Mobile sower ) : base( 0x0C5E )
		{
			Movable = false;
			Name = "Pumpkin Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 2, 0x0C5E, 0x0C6A, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Pumpkin));
		}

		public PumpkinCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 2, 0x0C5E, 0x0C6A, true);
		}
	}
}