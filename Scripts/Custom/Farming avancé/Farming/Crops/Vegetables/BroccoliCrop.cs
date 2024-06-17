namespace Server.Items.Crops
{
	public class BroccoliSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public BroccoliSeed() : this( 1 ) { }

		[Constructable]
		public BroccoliSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Broccoli Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(BroccoliSeedling));
		}

		public BroccoliSeed( Serial serial ) : base( serial ) { }

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

	public class BroccoliSeedling : BaseSeedling
	{
		[Constructable]
		public BroccoliSeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Broccoli Seedling";
			Sower = sower;
			Init(this, typeof(BroccoliCrop));
		}
		
		public BroccoliSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(BroccoliCrop));
		}
	}

	public class BroccoliCrop : BaseCrop
	{
		[Constructable]
		public BroccoliCrop() : this(null) { }

		[Constructable]
		public BroccoliCrop( Mobile sower ) : base( 0xCC7 )
		{
			Movable = false;
			Name = "Broccoli Plant";
			Hue = 0x48F;
			Sower = sower;
			Init(this, 1, 0xC62, 0xCC7, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Broccoli));
		}

		public BroccoliCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 1, 0xC62, 0xCC7, true);
		}
	}
}