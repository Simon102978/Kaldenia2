namespace Server.Items.Crops
{
	public class MiniKiwiSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public MiniKiwiSeed() : this( 1 ) { }

		[Constructable]
		public MiniKiwiSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Miniture Kiwi Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(MiniKiwiSeedling));
		}

		public MiniKiwiSeed( Serial serial ) : base( serial ) { }

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

	public class MiniKiwiSeedling : BaseSeedling
	{
		[Constructable]
		public MiniKiwiSeedling( Mobile sower ) : base( Utility.RandomList ( 0xCE9, 0xCEA ) )
		{
			Movable = false;
			Name = "Miniture Kiwi Tree Seedling";
			Sower = sower;
			Init(this, typeof(MiniKiwiCrop));
		}
		
		public MiniKiwiSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(MiniKiwiCrop));
		}
	}

	public class MiniKiwiCrop : BaseCrop
	{
		[Constructable]
		public MiniKiwiCrop() : this(null) { }

		[Constructable]
		public MiniKiwiCrop( Mobile sower ) : base( 3273 )
		{
			Movable = false;
			Name = "Miniture Kiwi Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 3273, 3273, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Grapefruit));
		}

		public MiniKiwiCrop(Serial serial) : base(serial) { }

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