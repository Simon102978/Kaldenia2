using System;

namespace Server.Items.Crops
{
	public class SpiritRoseSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public SpiritRoseSeed() : this( 1 ) { }

		[Constructable]
		public SpiritRoseSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Spirit Rose Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(SpiritRoseSeedling));
		}

		public SpiritRoseSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SpiritRoseSeedling : BaseSeedling
	{
		[Constructable]
		public SpiritRoseSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Spirit Rose Seedling";
			Sower = sower;
			Init(this, typeof(SpiritRoseCrop));
		}
		
		public SpiritRoseSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SpiritRoseCrop));
		}
	}

	public class SpiritRoseCrop : BaseCrop
	{
		[Constructable]
		public SpiritRoseCrop() : this(null) { }

		[Constructable]
		public SpiritRoseCrop( Mobile sower ) : base( 0x234D )
		{
			Movable = false;
			Name = "Spirit Rose Plant";
			Hue = 1947;
			Sower = sower;
			Init(this, 1, 0x234C, 0x234D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(SpiritRose));
		}

		public SpiritRoseCrop(Serial serial) : base(serial) { }

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