namespace Server.Items.Crops
{
	public class MiniGrapefruitSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public MiniGrapefruitSeed() : this( 1 ) { }

		[Constructable]
		public MiniGrapefruitSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Miniture Grapefruit Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(MiniGrapefruitSeedling));
		}

		public MiniGrapefruitSeed( Serial serial ) : base( serial ) { }

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

	public class MiniGrapefruitSeedling : BaseSeedling
	{
		[Constructable]
		public MiniGrapefruitSeedling( Mobile sower ) : base( Utility.RandomList ( 0xCE9, 0xCEA ) )
		{
			Movable = false;
			Name = "Miniture Grapefruit Tree Seedling";
			Sower = sower;
			Init(this, typeof(MiniGrapefruitCrop));
		}
		
		public MiniGrapefruitSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(MiniGrapefruitCrop));
		}
	}

	public class MiniGrapefruitCrop : BaseCrop
	{
		[Constructable]
		public MiniGrapefruitCrop() : this(null) { }

		[Constructable]
		public MiniGrapefruitCrop( Mobile sower ) : base( 3273 )
		{
			Movable = false;
			Name = "Miniture Grapefruit Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 3273, 3273, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Grapefruit));
		}

		public MiniGrapefruitCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 10, 3273, 3273, true);
		}
	}
}