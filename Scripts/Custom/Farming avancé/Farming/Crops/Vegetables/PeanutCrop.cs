namespace Server.Items.Crops
{
	public class PeanutSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public PeanutSeed() : this( 1 ) { }

		[Constructable]
		public PeanutSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Peanut Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(PeanutSeedling));
		}

		public PeanutSeed( Serial serial ) : base( serial ) { }

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

	public class PeanutSeedling : BaseSeedling
	{
		[Constructable]
		public PeanutSeedling( Mobile sower ) : base( 0xCB4 )
		{
			Movable = false;
			Name = "Peanut Seedling";
			Sower = sower;
			Init(this, typeof(PeanutCrop));
		}
		
		public PeanutSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(PeanutCrop));
		}
	}

	public class PeanutCrop : BaseCrop
	{
		[Constructable]
		public PeanutCrop() : this(null) { }

		[Constructable]
		public PeanutCrop( Mobile sower ) : base( 0xC63 )
		{
			Movable = false;
			Name = "Peanut Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 6, 0xC61, 0xC63, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Peanut));
		}

		public PeanutCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 6, 0xC61, 0xC63, true);
		}
	}
}