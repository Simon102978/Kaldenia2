namespace Server.Items.Crops
{
	public class DateSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public DateSeed() : this( 1 ) { }

		[Constructable]
		public DateSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Date Palm Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(DateSeedling));
		}

		public DateSeed( Serial serial ) : base( serial ) { }

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

	public class DateSeedling : BaseSeedling
	{
		[Constructable]
		public DateSeedling( Mobile sower ) : base( 0xC96 )
		{
			Movable = false;
			Name = "Date Palm Seedling";
			Sower = sower;
			Init(this, typeof(DateCrop));
		}
		
		public DateSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(DateCrop));
		}
	}

	public class DateCrop : BaseCrop
	{
		[Constructable]
		public DateCrop() : this(null) { }

		[Constructable]
		public DateCrop( Mobile sower ) : base( 0xC96 )
		{
			Movable = false;
			Name = "Date Palm Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 8, 0xC96, 0xC96, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Dates));
		}

		public DateCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 8, 0xC96, 0xC96, true);
		}
	}
}