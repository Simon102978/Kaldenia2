namespace Server.Items.Crops
{
	public class MiniOrangeSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public MiniOrangeSeed() : this( 1 ) { }

		[Constructable]
		public MiniOrangeSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Miniture Orange Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(MiniOrangeSeedling));
		}

		public MiniOrangeSeed( Serial serial ) : base( serial ) { }

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

	public class MiniOrangeSeedling : BaseSeedling
	{
		[Constructable]
		public MiniOrangeSeedling( Mobile sower ) : base( Utility.RandomList ( 0xCE9, 0xCEA ) )
		{
			Movable = false;
			Name = "Miniture Orange Tree Seedling";
			Sower = sower;
			Init(this, typeof(MiniOrangeCrop));
		}
		
		public MiniOrangeSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(MiniOrangeCrop));
		}
	}

	public class MiniOrangeCrop : BaseCrop
	{
		[Constructable]
		public MiniOrangeCrop() : this(null) { }

		[Constructable]
		public MiniOrangeCrop( Mobile sower ) : base( 3273 )
		{
			Movable = false;
			Name = "Miniture Orange Tree Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 10, 3273, 3273, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Orange));
		}

		public MiniOrangeCrop(Serial serial) : base(serial) { }

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