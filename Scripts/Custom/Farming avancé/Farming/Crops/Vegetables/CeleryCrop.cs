﻿namespace Server.Items.Crops
{
	public class CelerySeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		public override double MinSkill{ get { return 0.0; } }

		public override double MaxSkill{ get { return 40.0; } }


		[Constructable]
		public CelerySeed() : this( 1 ) { }

		[Constructable]
		public CelerySeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0xF27;
			Movable = true;
			Amount = amount;
			Name = "Celery Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(CelerySeedling));
		}

		public CelerySeed( Serial serial ) : base( serial ) { }

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

	public class CelerySeedling : BaseSeedling
	{
		[Constructable]
		public CelerySeedling( Mobile sower ) : base( 0xC68 )
		{
			Movable = false;
			Name = "Celery Seedling";
			Sower = sower;
			Init(this, typeof(CeleryCrop));
		}
		
		public CelerySeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(CeleryCrop));
		}
	}

	public class CeleryCrop : BaseCrop
	{

		public override double MinSkill{ get { return 10.0; } }

		public override double MaxSkill{ get { return 40.0; } }
		[Constructable]
		public CeleryCrop() : this(null) { }

		[Constructable]
		public CeleryCrop( Mobile sower ) : base( 0xC6F )
		{
			Movable = false;
			Name = "Celery Plant";
			Hue = 0x232;
			Sower = sower;
			Init(this, 3, 0xC69, 0xC6F, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Celery));
		}

		public CeleryCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 3, 0xC69, 0xC6F, true);
		}
	}
}