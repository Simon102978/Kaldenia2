using System;

namespace Server.Items.Crops
{
	public class PineappleSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public PineappleSeed() : this( 1 ) { }

		[Constructable]
		public PineappleSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Pineapple Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(PineappleSeedling));
		}

		public PineappleSeed( Serial serial ) : base( serial ) { }

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

	public class PineappleSeedling : BaseSeedling
	{
		[Constructable]
		public PineappleSeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Pineapple Seedling";
			Sower = sower;
            Init(this, typeof(PineappleCrop));
		}
		
		public PineappleSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

            Init(this, typeof(PineappleCrop));
		}
	}

	public class PineappleCrop : BaseCrop
	{
		private const int max = 1;
		private int fullGraphic;
		private int pickedGraphic;
		private DateTime lastpicked;
		private Mobile m_sower;
		private int m_yield;
		public Timer regrowTimer;
		private DateTime m_lastvisit;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime LastSowerVisit{ get{ return m_lastvisit; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Growing{ get{ return regrowTimer.Running; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Sower{ get{ return m_sower; } set{ m_sower = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Yield{ get{ return m_yield; } set{ m_yield = value; } }

		public int Capacity{ get{ return max; } }
		public int FullGraphic{ get{ return fullGraphic; } set{ fullGraphic = value; } }
		public int PickGraphic{ get{ return pickedGraphic; } set{ pickedGraphic = value; } }
		public DateTime LastPick{ get{ return lastpicked; } set{ lastpicked = value; } }

		[Constructable]
		public PineappleCrop() : this(null) { }

		[Constructable]
		public PineappleCrop( Mobile sower ) : base( 3231 )
		{
			Movable = false;
			Name = "Pineapple Plant";
			Hue = 0x000;
			Sower = sower;
			Init(this, 15, 3231, 3231, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Pineapple));
		}

		public PineappleCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, 15, 3231, 3231, true);
		}
	}
}