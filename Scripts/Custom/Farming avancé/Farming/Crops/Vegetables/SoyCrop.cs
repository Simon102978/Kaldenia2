namespace Server.Items.Crops
{
	public class SoySeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public SoySeed() : this( 1 ) { }

		[Constructable]
		public SoySeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Soy Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(SoySeedling));
		}

		public SoySeed( Serial serial ) : base( serial ) { }

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

	public class SoySeedling : BaseSeedling
	{
		[Constructable]
		public SoySeedling( Mobile sower ) : base( 0xCB0 )
		{
			Movable = false;
			Name = "Soy Seedling";
			Sower = sower;
			Init(this, typeof(SoyCrop));
		}
		
		public SoySeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SoyCrop));
		}
	}

	public class SoyCrop : BaseCrop
	{
		[Constructable]
		public SoyCrop() : this(null) { }

		[Constructable]
		public SoyCrop( Mobile sower ) : base( 0xC7E )
		{
			Movable = false;
			Name = "Soy Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 6, 0xCB0, 0xC7E, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(SnowPeas));
		}

		public SoyCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 6, 0xCB0, 0xC7E, true);
		}
	}
}