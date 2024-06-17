namespace Server.Items.Crops
{
	public class MandrakeSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public MandrakeSeed() : this( 1 ) { }

		[Constructable]
		public MandrakeSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Mandrake Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(MandrakeSeedling));
		}

		public MandrakeSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class MandrakeSeedling : BaseSeedling
	{
		[Constructable]
		public MandrakeSeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Mandrake Seedling";
			Sower = sower;
			Init(this, typeof(MandrakeCrop));
		}
		
		public MandrakeSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(MandrakeCrop));
		}
	}

	public class MandrakeCrop : BaseCrop
	{
		[Constructable]
		public MandrakeCrop() : this(null) { }

		[Constructable]
		public MandrakeCrop( Mobile sower ) : base( 0x18DF )
		{
			Movable = false;
			Name = "Mandrake Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 6, 0xC61, 0x18DF, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(MandrakeRoot));
		}

		public MandrakeCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 6, 0xC61, 0x18DF, true);
		}
	}
}