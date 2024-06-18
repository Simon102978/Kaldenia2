namespace Server.Items.Crops
{
	public class HoneydewMelonSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public HoneydewMelonSeed() : this( 1 ) { }

		[Constructable]
		public HoneydewMelonSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Honeydew Melon Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(HoneydewMelonSeedling));
		}

		public HoneydewMelonSeed( Serial serial ) : base( serial ) { }

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

	public class HoneydewMelonSeedling : BaseSeedling
	{
		[Constructable]
		public HoneydewMelonSeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Honeydew Melon Seedling";
			Sower = sower;
			Init(this, typeof(HoneydewMelonCrop));
		}
		
		public HoneydewMelonSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(HoneydewMelonCrop));
		}
	}

	public class HoneydewMelonCrop : BaseCrop
	{
		[Constructable]
		public HoneydewMelonCrop() : this(null) { }

		[Constructable]
		public HoneydewMelonCrop( Mobile sower ) : base( 3164 )
		{
			Movable = false;
			Name = "Honeydew Melon Plant";
			Hue = 2998;
			Sower = sower;
			Init(this, 2, 3164, 3164, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(HoneydewMelon));
		}

		public HoneydewMelonCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 2, 3164, 3164, true);
		}
	}
}