namespace Server.Items.Crops
{
	public class MiniAlmondSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		public override double MinSkill{ get { return 0.0; } }

		public override double MaxSkill{ get { return 40.0; } }


		[Constructable]
		public MiniAlmondSeed() : this( 1 ) { }

		[Constructable]
		public MiniAlmondSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Miniture Almond Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(MiniAlmondSeedling));
		}

		public MiniAlmondSeed( Serial serial ) : base( serial ) { }

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

	public class MiniAlmondSeedling : BaseSeedling
	{
		[Constructable]
		public MiniAlmondSeedling( Mobile sower ) : base( Utility.RandomList ( 0xCE9, 0xCEA ) )
		{
			Movable = false;
			Name = "Miniture Almond Tree Seedling";
			Sower = sower;
			Init(this, typeof(MiniAlmondCrop));
		}
		
		public MiniAlmondSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(MiniAlmondCrop));
		}
	}

	public class MiniAlmondCrop : BaseCrop
	{

		public override double MinSkill{ get { return 10.0; } }

		public override double MaxSkill{ get { return 40.0; } }
		[Constructable]
		public MiniAlmondCrop() : this(null) { }

		[Constructable]
		public MiniAlmondCrop( Mobile sower ) : base( 3273 )
		{
			Movable = false;
			Name = "Miniture Almond Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 3273, 3273, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Almond));
		}

		public MiniAlmondCrop(Serial serial) : base(serial) { }

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