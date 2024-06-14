namespace Server.Items.Crops
{
	public class OnionSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public OnionSeed() : this( 1 ) { }

		[Constructable]
		public OnionSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Onion Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(OnionSeedling));
		}

		public OnionSeed( Serial serial ) : base( serial ) { }

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

	public class OnionSeedling : BaseSeedling
	{
		[Constructable]
		public OnionSeedling( Mobile sower ) : base( 0xC68 )
		{
			Movable = false;
			Name = "Onion Seedling";
			Sower = sower;
			Init(this, typeof(OnionCrop));
		}
		
		public OnionSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(OnionCrop));
		}
	}

	public class OnionCrop : BaseCrop
	{
		[Constructable]
		public OnionCrop() : this(null) { }

		[Constructable]
		public OnionCrop( Mobile sower ) : base( 0xC6F )
		{
			Movable = false;
			Name = "Onion Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 1, 0xC69, 0xC6F, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Onion));
		}

		public OnionCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 1, 0xC69, 0xC6F, true);
		}
	}
}