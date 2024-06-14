namespace Server.Items.Crops
{
	public class BeetSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public BeetSeed() : this( 1 ) { }

		[Constructable]
		public BeetSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Beet Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(BeetSeedling));
		}

		public BeetSeed( Serial serial ) : base( serial ) { }

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

	public class BeetSeedling : BaseSeedling
	{
		[Constructable]
		public BeetSeedling( Mobile sower ) : base( 0xC68 )
		{
			Movable = false;
			Name = "Beet Seedling";
			Sower = sower;
			Init(this, typeof(BeetCrop));
		}
		
		public BeetSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(BeetCrop));
		}
	}

	public class BeetCrop : BaseCrop
	{
		[Constructable]
		public BeetCrop() : this(null) { }

		[Constructable]
		public BeetCrop( Mobile sower ) : base( 0xC6F )
		{
			Movable = false;
			Name = "Beet Plant";
			Hue = 0x48F;
			Sower = sower;
			Init(this, 3, 0xC6F, 0xC76, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Beet));
		}

		public BeetCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 3, 0xC6F, 0xC76, true);
		}
	}
}