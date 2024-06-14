namespace Server.Items.Crops
{
	public class MiniPistacioSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public MiniPistacioSeed() : this( 1 ) { }

		[Constructable]
		public MiniPistacioSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Miniture Pistacio Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(MiniPistacioSeedling));
		}

		public MiniPistacioSeed( Serial serial ) : base( serial ) { }

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

	public class MiniPistacioSeedling : BaseSeedling
	{
		[Constructable]
		public MiniPistacioSeedling( Mobile sower ) : base( Utility.RandomList ( 0xCE9, 0xCEA ) )
		{
			Movable = false;
			Name = "Miniture Pistacio Tree Seedling";
			Sower = sower;
			Init(this, typeof(MiniPistacioCrop));
		}
		
		public MiniPistacioSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(MiniPistacioCrop));
		}
	}

	public class MiniPistacioCrop : BaseCrop
	{
		[Constructable]
		public MiniPistacioCrop() : this(null) { }

		[Constructable]
		public MiniPistacioCrop( Mobile sower ) : base( 3273 )
		{
			Movable = false;
			Name = "Miniture Pistacio Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 3273, 3273, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Pistacio));
		}

		public MiniPistacioCrop(Serial serial) : base(serial) { }

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