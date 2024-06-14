using System;

namespace Server.Items.Crops
{
	public class BlackRoseSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public BlackRoseSeed() : this( 1 ) { }

		[Constructable]
		public BlackRoseSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 2393;
			Movable = true;
			Amount = amount;
			Name = "Black Rose Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(BlackRoseSeedling));
		}

		public BlackRoseSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BlackRoseSeedling : BaseSeedling
	{
		[Constructable]
		public BlackRoseSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Black Rose Seedling";
			Sower = sower;
            Init(this, typeof(BlackRoseCrop));
		}
		
		public BlackRoseSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(BlackRoseCrop));
		}
	}

	public class BlackRoseCrop : BaseCrop
	{
		[Constructable]
		public BlackRoseCrop() : this(null) { }

		[Constructable]
		public BlackRoseCrop( Mobile sower ) : base( 0x234D )
		{
			Movable = false;
			Name = "Plant de rose noire";
			Hue = 1109;
			Sower = sower;
			Init(this, 1, 0x234C, 0x234D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(BlackRose2));
		}

		public BlackRoseCrop(Serial serial) : base(serial) { }

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