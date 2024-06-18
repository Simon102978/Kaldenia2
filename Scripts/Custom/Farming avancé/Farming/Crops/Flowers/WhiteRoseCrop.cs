using System;

namespace Server.Items.Crops
{
	public class WhiteRoseSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public WhiteRoseSeed() : this( 1 ) { }

		[Constructable]
		public WhiteRoseSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "White Rose Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(WhiteRoseSeedling));
		}

		public WhiteRoseSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class WhiteRoseSeedling : BaseSeedling
	{
		[Constructable]
		public WhiteRoseSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "White Rose Seedling";
			Sower = sower;
			Init(this, typeof(WhiteRoseCrop));
		}
		
		public WhiteRoseSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(WhiteRoseCrop));
		}
	}

	public class WhiteRoseCrop : BaseCrop
	{
		[Constructable]
		public WhiteRoseCrop() : this(null) { }

		[Constructable]
		public WhiteRoseCrop( Mobile sower ) : base( 0x234D )
		{
			Movable = false;
			Name = "Plant de rose blanche";
			Hue = 1972;
			Sower = sower;
			Init(this, 1, 0x234C, 0x234D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(WhiteRose2));
		}

		public WhiteRoseCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 1, 0x234C, 0x234D, true);
		}
	}
}