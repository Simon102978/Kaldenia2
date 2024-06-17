namespace Server.Items.Crops
{
	public class PeasSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public PeasSeed() : this( 1 ) { }

		[Constructable]
		public PeasSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Peas Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(PeasSeedling));
		}

		public PeasSeed( Serial serial ) : base( serial ) { }

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

	public class PeasSeedling : BaseSeedling
	{
		[Constructable]
		public PeasSeedling( Mobile sower ) : base( Utility.RandomList( 0xD32, 0xD33 ) )
		{
			Movable = false;
			Name = "Peas Seedling";
			Sower = sower;
			Init(this, typeof(PeasCrop));
		}
		
		public PeasSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(PeasCrop));
		}
	}

	public class PeasCrop : BaseCrop
	{
		[Constructable]
		public PeasCrop() : this(null) { }

		[Constructable]
		public PeasCrop( Mobile sower ) : base( 0xCEF )
		{
			Movable = false;
			Name = "Peas Plant";
			Hue = 0x2A9;
			Sower = sower;
			Init(this, 20, 0xCF2, 0xCEF, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Peas));
		}

		public PeasCrop(Serial serial) : base(serial) { }

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