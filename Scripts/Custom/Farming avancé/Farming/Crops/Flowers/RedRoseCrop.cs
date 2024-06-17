using System;

namespace Server.Items.Crops
{
	public class RedRoseSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public RedRoseSeed() : this( 1 ) { }

		[Constructable]
		public RedRoseSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Red Rose Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(RedRoseSeedling));
		}

		public RedRoseSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class RedRoseSeedling : BaseSeedling
	{
		[Constructable]
		public RedRoseSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Red Rose Seedling";
			Sower = sower;
			Init(this, typeof(RedRoseCrop));
		}
		
		public RedRoseSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(RedRoseCrop));
		}
	}

	public class RedRoseCrop : BaseCrop
	{
		[Constructable]
		public RedRoseCrop() : this(null) { }

		[Constructable]
		public RedRoseCrop( Mobile sower ) : base( 0x234D )
		{
			Movable = false;
			Name = "Plant de rose rouge";
			Hue = 2118;
			Sower = sower;
			Init(this, 1, 0x234C, 0x234D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(RedRose));
		}

		public RedRoseCrop(Serial serial) : base(serial) { }

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