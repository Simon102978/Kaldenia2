namespace Server.Items.Crops
{
	public class RedMushroomSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public RedMushroomSeed() : this( 1 ) { }

		[Constructable]
		public RedMushroomSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Red Mushroom Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(RedMushroomSeedling));
		}

		public RedMushroomSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class RedMushroomSeedling : BaseSeedling
	{
		[Constructable]
		public RedMushroomSeedling( Mobile sower ) : base( 0xD15 )
		{
			Movable = false;
			Name = "Red Mushroom Seedling";
			Sower = sower;
			Init(this, typeof(RedMushroomCrop));
		}
		
		public RedMushroomSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(RedMushroomCrop));
		}
	}

	public class RedMushroomCrop : BaseCrop
	{
		[Constructable]
		public RedMushroomCrop() : this(null) { }

		[Constructable]
		public RedMushroomCrop( Mobile sower ) : base( 0xD16 )
		{
			Movable = false;
			Name = "Red Mushroom Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 6, 0xD16, 0xD16, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(RedMushroom));
		}

		public RedMushroomCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 6, 0xD16, 0xD16, true);
		}
	}
}