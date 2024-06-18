using System;

namespace Server.Items.Crops
{
	public class SnapdragonSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public SnapdragonSeed() : this( 1 ) { }

		[Constructable]
		public SnapdragonSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Snapdragon Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(SnapdragonSeedling));
		}

		public SnapdragonSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SnapdragonSeedling : BaseSeedling
	{
		[Constructable]
		public SnapdragonSeedling( Mobile sower ) : base( 0xCB5 )
		{
			Movable = false;
			Name = "Snapdragon Seedling";
			Sower = sower;
			Init(this, typeof(SnapdragonCrop));
		}
		
		public SnapdragonSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Init(this, typeof(SnapdragonCrop));
		}
	}

	public class SnapdragonCrop : BaseCrop
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
		public SnapdragonCrop() : this(null) { }

		[Constructable]
		public SnapdragonCrop( Mobile sower ) : base( 0x234D )
		{
			Movable = false;
			Name = "Snapdragon Plant";
			Hue = 2816;
			Sower = sower;
			Init(this, 1, 0x234C, 0x234D, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			Gather(from, typeof(Snapdragon));
		}

		public SnapdragonCrop(Serial serial) : base(serial) { }

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