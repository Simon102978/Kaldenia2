using System;

namespace Server.Items.Crops
{
	public class CranberrySeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public CranberrySeed() : this( 1 ) { }

		[Constructable]
		public CranberrySeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Cranberry Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(CranberrySeedling));
		}

		public CranberrySeed( Serial serial ) : base( serial ) { }

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

	public class CranberrySeedling : BaseSeedling
	{
		[Constructable]
		public CranberrySeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Cranberry Seedling";
			Sower = sower;
            Init(this, typeof(CranberryCrop));
		}
		
		public CranberrySeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(CranberryCrop));
		}
	}

	public class CranberryCrop : BaseCrop
	{
		[Constructable]
		public CranberryCrop() : this(null) { }

		[Constructable]
		public CranberryCrop( Mobile sower ) : base( 3272 )
		{
			Movable = false;
			Name = "Cranberry Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 15, 3272, 3272, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Cranberry));
		}

		public CranberryCrop(Serial serial) : base(serial) { }

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