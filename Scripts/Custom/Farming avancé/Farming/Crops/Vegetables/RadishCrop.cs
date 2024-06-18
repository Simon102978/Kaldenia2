namespace Server.Items.Crops
{
	public class RadishSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public RadishSeed() : this( 1 ) { }

		[Constructable]
		public RadishSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Radish Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(RadishSeedling));
		}

		public RadishSeed( Serial serial ) : base( serial ) { }

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

	public class RadishSeedling : BaseSeedling
	{
		[Constructable]
		public RadishSeedling( Mobile sower ) : base( 0xC68 )
		{
			Movable = false;
			Name = "Radish Seedling";
			Sower = sower;
			Init(this, typeof(RadishCrop));
		}
		
		public RadishSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(RadishCrop));
		}
	}

	public class RadishCrop : BaseCrop
	{
		[Constructable]
		public RadishCrop() : this(null) { }

		[Constructable]
		public RadishCrop( Mobile sower ) : base( 0xC6F )
		{
			Movable = false;
			Name = "Radish Plant";
			Hue = 0x232;
			Sower = sower;
			Init(this, 8, 0xC69, 0xC6F, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Radish));
		}

		public RadishCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 8, 0xC69, 0xC6F, true);
		}
	}
}