namespace Server.Items.Crops
{
	public class TurnipSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public TurnipSeed() : this( 1 ) { }

		[Constructable]
		public TurnipSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Turnip Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(TurnipSeedling));
		}

		public TurnipSeed( Serial serial ) : base( serial ) { }

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

	public class TurnipSeedling : BaseSeedling
	{
		[Constructable]
		public TurnipSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Turnip Seedling";
			Sower = sower;
			Init(this, typeof(TurnipCrop));
		}
		
		public TurnipSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(TurnipCrop));
		}
	}

	public class TurnipCrop : BaseCrop
	{
		[Constructable]
		public TurnipCrop() : this(null) { }

		[Constructable]
		public TurnipCrop( Mobile sower ) : base( 0xC63 )
		{
			Movable = false;
			Name = "Turnip Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 6, 0xC62, 0xC63, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Turnip));
		}

		public TurnipCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 6, 0xC62, 0xC63, true);
		}
	}
}