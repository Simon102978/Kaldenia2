namespace Server.Items.Crops
{
	public class AsparagusSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public AsparagusSeed() : this( 1 ) { }

		[Constructable]
		public AsparagusSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Asparagus Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(AsparagusSeedling));
		}

		public AsparagusSeed( Serial serial ) : base( serial ) { }

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

	public class AsparagusSeedling : BaseSeedling
	{
		[Constructable]
		public AsparagusSeedling( Mobile sower ) : base( 0xD32 )
		{
			Movable = false;
			Name = "Asparagus Seedling";
			Sower = sower;
			Init(this, typeof(AsparagusCrop));
		}
		
		public AsparagusSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(AsparagusCrop));
		}
	}

	public class AsparagusCrop : BaseCrop
	{
		[Constructable]
		public AsparagusCrop() : this(null) { }

		[Constructable]
		public AsparagusCrop( Mobile sower ) : base( 0xC63 )
		{
			Movable = false;
			Name = "Asparagus Plant";
			Hue = 0x1D3;
			Sower = sower;
			Init(this, 4, 0xC61, 0xC63, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Asparagus));
		}

		public AsparagusCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 4, 0xC61, 0xC63, true);
		}
	}
}