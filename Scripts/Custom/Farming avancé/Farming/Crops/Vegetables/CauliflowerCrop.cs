namespace Server.Items.Crops
{
	public class CauliflowerSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public CauliflowerSeed() : this( 1 ) { }

		[Constructable]
		public CauliflowerSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Cauliflower Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(CauliflowerSeedling));
		}

		public CauliflowerSeed( Serial serial ) : base( serial ) { }

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

	public class CauliflowerSeedling : BaseSeedling
	{
		[Constructable]
		public CauliflowerSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Cauliflower Seedling";
			Sower = sower;
			Init(this, typeof(CauliflowerCrop));
		}
		
		public CauliflowerSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(CauliflowerCrop));
		}
	}

	public class CauliflowerCrop : BaseCrop
	{
		[Constructable]
		public CauliflowerCrop() : this(null) { }

		[Constructable]
		public CauliflowerCrop( Mobile sower ) : base( 0xD06 )
		{
			Movable = false;
			Name = "Cauliflower Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 1, 0xD06, 0xD06, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Carrot));
		}

		public CauliflowerCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 1, 0xD06, 0xD06, true);
		}
	}
}