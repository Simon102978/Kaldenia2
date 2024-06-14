using System;

namespace Server.Items.Crops
{
	public class PoppySeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public PoppySeed() : this( 1 ) { }

		[Constructable]
		public PoppySeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Poppy Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(PoppySeedling));
		}

		public PoppySeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PoppySeedling : BaseSeedling
	{
		[Constructable]
		public PoppySeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Poppy Seedling";
			Sower = sower;
			Init(this, typeof(PoppyCrop));
		}
		
		public PoppySeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(PoppyCrop));
		}
	}

	public class PoppyCrop : BaseCrop
	{
		[Constructable]
		public PoppyCrop() : this(null) { }

		[Constructable]
		public PoppyCrop( Mobile sower ) : base( 0x234D )
		{
			Movable = false;
			Name = "Poppy Plant";
			Hue = 2973;
			Sower = sower;
			Init(this, 1, 0x234C, 0x234D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Poppy));
		}

		public PoppyCrop(Serial serial) : base(serial) { }

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