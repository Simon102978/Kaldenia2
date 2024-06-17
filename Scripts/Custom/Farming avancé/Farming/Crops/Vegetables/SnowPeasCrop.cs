namespace Server.Items.Crops
{
	public class SnowPeasSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public SnowPeasSeed() : this( 1 ) { }

		[Constructable]
		public SnowPeasSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Snow Peas Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(SnowPeasSeedling));
		}

		public SnowPeasSeed( Serial serial ) : base( serial ) { }

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

	public class SnowPeasSeedling : BaseSeedling
	{
		[Constructable]
		public SnowPeasSeedling( Mobile sower ) : base( Utility.RandomList( 0xD32, 0xD33 ) )
		{
			Movable = false;
			Name = "Snow Peas Seedling";
			Sower = sower;
			Init(this, typeof(SnowPeasCrop));
		}
		
		public SnowPeasSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SnowPeasCrop));
		}
	}

	public class SnowPeasCrop : BaseCrop
	{
		[Constructable]
		public SnowPeasCrop() : this(null) { }

		[Constructable]
		public SnowPeasCrop( Mobile sower ) : base( 0xCEF )
		{
			Movable = false;
			Name = "Snow Peas Plant";
			Hue = 0x29A;
			Sower = sower;
			Init(this, 20, 0xCF2, 0xCEF, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(SnowPeas));
		}

		public SnowPeasCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 20, 0xCF2, 0xCEF, true);
		}
	}
}