namespace Server.Items.Crops
{
	public class CabbageSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public CabbageSeed() : this( 1 ) { }

		[Constructable]
		public CabbageSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Cabbage Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(CabbageSeedling));
		}

		public CabbageSeed( Serial serial ) : base( serial ) { }

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

	public class CabbageSeedling : BaseSeedling
	{
		[Constructable]
		public CabbageSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Cabbage Seedling";
			Sower = sower;
			Init(this, typeof(CabbageCrop));
		}
		
		public CabbageSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(CabbageCrop));
		}
	}

	public class CabbageCrop : BaseCrop
	{
		[Constructable]
		public CabbageCrop() : this(null) { }

		[Constructable]
		public CabbageCrop( Mobile sower ) : base( 0xC7C )
		{
			Movable = false;
			Name = "Cabbage Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 1, 0xC62, 0xCC7, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Cabbage));
		}

		public CabbageCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 1, 0xC61, 0xC7C, true);
		}
	}
}