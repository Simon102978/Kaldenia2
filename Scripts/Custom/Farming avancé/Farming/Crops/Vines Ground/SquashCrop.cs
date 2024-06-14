namespace Server.Items.Crops
{
	public class SquashSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public SquashSeed() : this( 1 ) { }

		[Constructable]
		public SquashSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Squash Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(SquashSeedling));
		}

		public SquashSeed( Serial serial ) : base( serial ) { }

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

	public class SquashSeedling : BaseSeedling
	{
		[Constructable]
		public SquashSeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Squash Seedling";
			Sower = sower;
			Init(this, typeof(SquashCrop));
		}
		
		public SquashSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SquashCrop));
		}
	}

	public class SquashCrop : BaseCrop
	{
		[Constructable]
		public SquashCrop() : this(null) { }

		[Constructable]
		public SquashCrop( Mobile sower ) : base( 3164 )
		{
			Movable = false;
			Name = "Squash Plant";
			Hue = 2995;
			Sower = sower;
			Init(this, 2, 3164, 3164, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Squash));
		}

		public SquashCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 2, 3164, 3164, true);
		}
	}
}