namespace Server.Items.Crops
{
	public class PotatoSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public PotatoSeed() : this( 1 ) { }

		[Constructable]
		public PotatoSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Potato Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(PotatoSeedling));
		}

		public PotatoSeed( Serial serial ) : base( serial ) { }

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

	public class PotatoSeedling : BaseSeedling
	{
		[Constructable]
		public PotatoSeedling( Mobile sower ) : base( 0xCB6 )
		{
			Movable = false;
			Name = "Potato Seedling";
			Sower = sower;
			Init(this, typeof(PotatoCrop));
		}
		
		public PotatoSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(PotatoCrop));
		}
	}

	public class PotatoCrop : BaseCrop
	{
		[Constructable]
		public PotatoCrop() : this(null) { }

		[Constructable]
		public PotatoCrop( Mobile sower ) : base( 0xC63 )
		{
			Movable = false;
			Name = "Potato Plant";
			Hue = 0x290;
			Sower = sower;
			Init(this, 6, 0xCF2, 0xCEF, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Potato));
		}

		public PotatoCrop(Serial serial) : base(serial) { }

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