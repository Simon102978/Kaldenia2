using System;

namespace Server.Items.Crops
{
	public class IrishRoseSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public IrishRoseSeed() : this( 1 ) { }

		[Constructable]
		public IrishRoseSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Irish Rose Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(IrishRoseSeedling));
		}

		public IrishRoseSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class IrishRoseSeedling : BaseSeedling
	{
		[Constructable]
		public IrishRoseSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Irish Rose Seedling";
			Sower = sower;
            Init(this, typeof(IrishRoseCrop));
		}
		
		public IrishRoseSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(IrishRoseCrop));
		}
	}

	public class IrishRoseCrop : BaseCrop
	{
		[Constructable]
		public IrishRoseCrop() : this(null) { }

		[Constructable]
		public IrishRoseCrop( Mobile sower ) : base( 0x234D )
		{
			Movable = false;
			Name = "Irish Rose Plant";
			Hue = 2723;
			Sower = sower;
			Init(this, 1, 0x234C, 0x234D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(IrishRose));
		}

		public IrishRoseCrop(Serial serial) : base(serial) { }

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