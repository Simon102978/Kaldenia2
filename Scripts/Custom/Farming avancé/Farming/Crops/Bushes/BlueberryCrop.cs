using System;

namespace Server.Items.Crops
{
	public class BlueberrySeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public BlueberrySeed() : this( 1 ) { }

		[Constructable]
		public BlueberrySeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Blueberry Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(BlueberrySeedling));
		}

		public BlueberrySeed( Serial serial ) : base( serial ) { }

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

	public class BlueberrySeedling : BaseSeedling
	{
		[Constructable]
		public BlueberrySeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Blueberry Seedling";
			Sower = sower;
            Init(this, typeof(BlueberryCrop));
		}
		
		public BlueberrySeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

            Init(this, typeof(BlueberryCrop));
		}
	}

	public class BlueberryCrop : BaseCrop
	{
		[Constructable]
		public BlueberryCrop() : this(null) { }

		[Constructable]
		public BlueberryCrop( Mobile sower ) : base( 3272 )
		{
			Movable = false;
			Name = "Blueberry Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 15, 3272, 3272, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Blueberry));
		}

		public BlueberryCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 15, 3272, 3272, true);
		}
	}
}