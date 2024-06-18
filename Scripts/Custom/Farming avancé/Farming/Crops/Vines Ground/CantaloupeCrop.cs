namespace Server.Items.Crops
{
	public class CantaloupeSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public CantaloupeSeed() : this( 1 ) { }

		[Constructable]
		public CantaloupeSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Cantaloupe Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(CantaloupeSeedling));
		}

		public CantaloupeSeed( Serial serial ) : base( serial ) { }

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

	public class CantaloupeSeedling : BaseSeedling
	{
		[Constructable]
		public CantaloupeSeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Cantaloupe Seedling";
			Sower = sower;
			Init(this, typeof(CantaloupeCrop));
		}
		
		public CantaloupeSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(CantaloupeCrop));
		}
	}

	public class CantaloupeCrop : BaseCrop
	{
		[Constructable]
		public CantaloupeCrop() : this(null) { }

		[Constructable]
		public CantaloupeCrop( Mobile sower ) : base(3164)
		{
			Movable = false;
			Name = "Cantaloupe Plant";
			Hue = 2983;
			Sower = sower;
			Init(this, 2, 3164, 3164, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Turnip));
		}

		public CantaloupeCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 2, 3164, 3164, true);
		}
	}
}