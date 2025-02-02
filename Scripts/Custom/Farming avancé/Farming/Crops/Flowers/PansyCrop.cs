using System;

namespace Server.Items.Crops
{
	public class PansySeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }


		public override double MinSkill{ get { return 0.0; } }

		public override double MaxSkill{ get { return 40.0; } }

		[Constructable]
		public PansySeed() : this( 1 ) { }

		[Constructable]
		public PansySeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Pansy Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(PansySeedling));
		}

		public PansySeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PansySeedling : BaseSeedling
	{
		[Constructable]
		public PansySeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Pansy Seedling";
			Sower = sower;
			Init(this, typeof(PansyCrop));
		}
		
		public PansySeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(PansyCrop));
		}
	}

	public class PansyCrop : BaseCrop
	{

		public override double MinSkill{ get { return 10.0; } }

		public override double MaxSkill{ get { return 50.0; } }
		[Constructable]
		public PansyCrop() : this(null) { }

		[Constructable]
		public PansyCrop( Mobile sower ) : base( 0x234D )
		{
			Movable = false;
			Name = "Pansy Plant";
			Hue = 2971;
			Sower = sower;
			Init(this, 1, 0x234C, 0x234D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Pansy));
		}

		public PansyCrop(Serial serial) : base(serial) { }

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