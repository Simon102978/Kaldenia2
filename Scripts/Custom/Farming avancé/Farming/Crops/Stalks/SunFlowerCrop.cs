namespace Server.Items.Crops
{
	public class SunFlowerSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		public override double MinSkill{ get { return 0.0; } }

		public override double MaxSkill{ get { return 40.0; } }


		[Constructable]
		public SunFlowerSeed() : this( 1 ) { }

		[Constructable]
		public SunFlowerSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Sun Flower Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(SunFlowerSeedling));
		}

		public SunFlowerSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SunFlowerSeedling : BaseSeedling
	{
		[Constructable]
		public SunFlowerSeedling( Mobile sower ) : base( 0xCB6 )
		{
			Movable = false;
			Name = "Sun Flower Seedling";
			Sower = sower;
			Init(this, typeof(SunFlowerCrop));
		}
		
		public SunFlowerSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SunFlowerCrop));
		}
	}

	public class SunFlowerCrop : BaseCrop
	{

		public override double MinSkill{ get { return 10.0; } }

		public override double MaxSkill{ get { return 40.0; } }
		[Constructable]
		public SunFlowerCrop() : this(null) { }

		[Constructable]
		public SunFlowerCrop( Mobile sower ) : base( 0xC85 )
		{
			Movable = false;
			Name = "Sun Flower Plant";
			Hue = 0x489;
			Sower = sower;
			Init(this, 20, 0xC85, 0xC85, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(EdibleSun));
		}

		public SunFlowerCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 20, 0xC85, 0xC85, true);
		}
	}
}