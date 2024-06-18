namespace Server.Items.Crops
{
	public class SmallBananaSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public SmallBananaSeed() : this( 1 ) { }

		[Constructable]
		public SmallBananaSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Small Banana Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(SmallBananaSeedling));
		}

		public SmallBananaSeed( Serial serial ) : base( serial ) { }

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

	public class SmallBananaSeedling : BaseSeedling
	{
		[Constructable]
		public SmallBananaSeedling( Mobile sower ) : base( 0xCA8 )
		{
			Movable = false;
			Name = "Small Banana Tree Seedling";
			Sower = sower;
			Init(this, typeof(SmallBananaCrop));
		}
		
		public SmallBananaSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SmallBananaCrop));
		}
	}

	public class SmallBananaCrop : BaseCrop
	{
		[Constructable]
		public SmallBananaCrop() : this(null) { }

		[Constructable]
		public SmallBananaCrop( Mobile sower ) : base( 0xCA8 )
		{
			Movable = false;
			Name = "Small Banana Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 6, 0xCA8, 0xCA8, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Pomegranate));
		}

		public SmallBananaCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 6, 0xCA8, 0xCA8, true);
		}
	}
}