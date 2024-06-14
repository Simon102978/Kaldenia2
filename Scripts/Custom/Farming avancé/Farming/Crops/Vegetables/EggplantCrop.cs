namespace Server.Items.Crops
{
	public class EggplantSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public EggplantSeed() : this( 1 ) { }

		[Constructable]
		public EggplantSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Eggplant Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(EggplantSeedling));
		}

		public EggplantSeed( Serial serial ) : base( serial ) { }

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

	public class EggplantSeedling : BaseSeedling
	{
		[Constructable]
		public EggplantSeedling( Mobile sower ) : base( 0xCB6 )
		{
			Movable = false;
			Name = "Eggplant Seedling";
			Sower = sower;
			Init(this, typeof(EggplantCrop));
		}
		
		public EggplantSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(EggplantCrop));
		}
	}

	public class EggplantCrop : BaseCrop
	{
		[Constructable]
		public EggplantCrop() : this(null) { }

		[Constructable]
		public EggplantCrop( Mobile sower ) : base( Utility.RandomList( 0xC9B, 0xC9D ) )
		{
			Movable = false;
			Name = "Eggplant Plant";
			Hue = 0x2A1;
			Sower = sower;
			Init(this, 3, Utility.RandomList(0xC9B, 0xC9D), Utility.RandomList(0xC9B, 0xC9D), false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Celery));
		}

		public EggplantCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 3, Utility.RandomList(0xC9B, 0xC9D), Utility.RandomList(0xC9B, 0xC9D), true);
		}
	}
}