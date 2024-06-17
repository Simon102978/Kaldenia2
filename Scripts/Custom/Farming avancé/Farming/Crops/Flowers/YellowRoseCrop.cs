using System;

namespace Server.Items.Crops
{
	public class YellowRoseSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public YellowRoseSeed() : this( 1 ) { }

		[Constructable]
		public YellowRoseSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Yellow Rose Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(YellowRoseSeedling));
		}

		public YellowRoseSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class YellowRoseSeedling : BaseSeedling
	{
		[Constructable]
		public YellowRoseSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Yellow Rose Seedling";
			Sower = sower;
			Init(this, typeof(YellowRoseCrop));
		}
		
		public YellowRoseSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(YellowRoseCrop));
		}
	}

	public class YellowRoseCrop : BaseCrop
	{
		[Constructable]
		public YellowRoseCrop() : this(null) { }

		[Constructable]
		public YellowRoseCrop( Mobile sower ) : base( 0x234D )
		{
			Movable = false;
			Name = "Yellow Rose Plant";
			Hue = 2858;
			Sower = sower;
			Init(this, 1, 0x234C, 0x234D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(YellowRose));
		}

		public YellowRoseCrop(Serial serial) : base(serial) { }

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