namespace Server.Items.Crops
{
	public class FieldCornSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public FieldCornSeed() : this( 1 ) { }

		[Constructable]
		public FieldCornSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Field Corn Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(FieldCornSeedling));
		}

		public FieldCornSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class FieldCornSeedling : BaseSeedling
	{
		[Constructable]
		public FieldCornSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Field Corn Seedling";
			Sower = sower;
			Init(this, typeof(FieldCornCrop));
		}
		
		public FieldCornSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(FieldCornCrop));
		}
	}

	public class FieldCornCrop : BaseCrop
	{
		[Constructable]
		public FieldCornCrop() : this(null) { }

		[Constructable]
		public FieldCornCrop( Mobile sower ) : base( 0xC7D )
		{
			Movable = false;
			Name = "Field Corn Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 2, 0xC7E, 0xC7D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(FieldCorn));
		}

		public FieldCornCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 2, 0xC7E, 0xC7D, true);
		}
	}
}