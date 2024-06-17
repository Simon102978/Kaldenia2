namespace Server.Items.Crops
{
	public class SpinachSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public SpinachSeed() : this( 1 ) { }

		[Constructable]
		public SpinachSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Spinach Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(SpinachSeedling));
		}

		public SpinachSeed( Serial serial ) : base( serial ) { }

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

	public class SpinachSeedling : BaseSeedling
	{
		[Constructable]
		public SpinachSeedling( Mobile sower ) : base( 0xCB4 )
		{
			Movable = false;
			Name = "Spinach Seedling";
			Sower = sower;
			Init(this, typeof(SpinachCrop));
		}
		
		public SpinachSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SpinachCrop));
		}
	}

	public class SpinachCrop : BaseCrop
	{
		[Constructable]
		public SpinachCrop() : this(null) { }

		[Constructable]
		public SpinachCrop( Mobile sower ) : base( 0xD0B )
		{
			Movable = false;
			Name = "Spinach Plant";
			Hue = 0x29D;
			Sower = sower;
			Init(this, 10, Utility.RandomList(0xD09, 0xD0A), 0xD0B, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Spinach));
		}

		public SpinachCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 10, Utility.RandomList(0xD09, 0xD0A), 0xD0B, true);
		}
	}
}