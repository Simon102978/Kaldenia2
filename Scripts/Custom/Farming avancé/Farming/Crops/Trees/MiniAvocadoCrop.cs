namespace Server.Items.Crops
{
	public class MiniAvocadoSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public MiniAvocadoSeed() : this( 1 ) { }

		[Constructable]
		public MiniAvocadoSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Miniture Avocado Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(MiniAvocadoSeedling));
		}

		public MiniAvocadoSeed( Serial serial ) : base( serial ) { }

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

	public class MiniAvocadoSeedling : BaseSeedling
	{
		[Constructable]
		public MiniAvocadoSeedling( Mobile sower ) : base( Utility.RandomList ( 0xCE9, 0xCEA ) )
		{
			Movable = false;
			Name = "Miniture Avocado Tree Seedling";
			Sower = sower;
			Init(this, typeof(MiniAvocadoCrop));
		}
		
		public MiniAvocadoSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(MiniAvocadoCrop));
		}
	}

	public class MiniAvocadoCrop : BaseCrop
	{
		[Constructable]
		public MiniAvocadoCrop() : this(null) { }

		[Constructable]
		public MiniAvocadoCrop( Mobile sower ) : base( 3273 )
		{
			Movable = false;
			Name = "Miniture Avocado Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 3273, 3273, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Avocado));
		}

		public MiniAvocadoCrop(Serial serial) : base(serial) { }

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