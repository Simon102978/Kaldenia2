namespace Server.Items.Crops
{
	public class GreenBeanSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public GreenBeanSeed() : this( 1 ) { }

		[Constructable]
		public GreenBeanSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Green Bean Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(GreenBeanSeedling));
		}

		public GreenBeanSeed( Serial serial ) : base( serial ) { }

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

	public class GreenBeanSeedling : BaseSeedling
	{
		[Constructable]
		public GreenBeanSeedling( Mobile sower ) : base( Utility.RandomList( 0xD32, 0xD33 ) )
		{
			Movable = false;
			Name = "Green Bean Seedling";
			Sower = sower;
			Init(this, typeof(GreenBeanCrop));
		}
		
		public GreenBeanSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(GreenBeanCrop));
		}
	}

	public class GreenBeanCrop : BaseCrop
	{
		[Constructable]
		public GreenBeanCrop() : this(null) { }

		[Constructable]
		public GreenBeanCrop( Mobile sower ) : base( 0xD04 )
		{
			Movable = false;
			Name = "Green Bean Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 0xD04, 0xD04, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Celery));
		}

		public GreenBeanCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 10, 0xD04, 0xD04, true);
		}
	}
}