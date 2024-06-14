namespace Server.Items.Crops
{
	public class SweetPotatoSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public SweetPotatoSeed() : this( 1 ) { }

		[Constructable]
		public SweetPotatoSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Sweet Potato Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(SweetPotatoSeedling));
		}

		public SweetPotatoSeed( Serial serial ) : base( serial ) { }

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

	public class SweetPotatoSeedling : BaseSeedling
	{
		[Constructable]
		public SweetPotatoSeedling( Mobile sower ) : base( 0xCB6 )
		{
			Movable = false;
			Name = "Sweet Potato Seedling";
			Sower = sower;
			Init(this, typeof(SweetPotatoCrop));
		}
		
		public SweetPotatoSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SweetPotatoCrop));
		}
	}

	public class SweetPotatoCrop : BaseCrop
	{
		[Constructable]
		public SweetPotatoCrop() : this(null) { }

		[Constructable]
		public SweetPotatoCrop( Mobile sower ) : base( 0xC63 )
		{
			Movable = false;
			Name = "Sweet Potato Plant";
			Hue = 0x29D;
			Sower = sower;
			Init(this, 4, 0xC61, 0xC63, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(SweetPotato));
		}

		public SweetPotatoCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 4, 0xC61, 0xC63, true);
		}
	}
}