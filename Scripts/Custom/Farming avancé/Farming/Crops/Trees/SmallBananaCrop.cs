using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
namespace Server.Items.Crops
{
	public class SmallBananaSeed : BaseCrop
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public SmallBananaSeed() : this( 1 ) { }

		[Constructable]
		public SmallBananaSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Small Banana Tree Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Mounted && !CropHelper.CanWorkMounted ) { from.SendMessage( "Vous ne pouvez pas planter une graine lorsque vous �tes sur votre monture." ); return; }
			Point3D m_pnt = from.Location;
			Map m_map = from.Map;
			if ( !IsChildOf( from.Backpack ) ) { from.SendLocalizedMessage( 1042010 ); return; }
			else if ( !CropHelper.CheckCanGrow( this, m_map, m_pnt.X, m_pnt.Y ) ) { from.SendMessage( "Cette graine ne poussera pas ici." ); return; }
			ArrayList cropshere = CropHelper.CheckCrop( m_pnt, m_map, 0 );
			if ( cropshere.Count > 0 ) { from.SendMessage( "Il y a d�j� un plant qui pousse ici." ); return; }
			ArrayList cropsnear = CropHelper.CheckCrop( m_pnt, m_map, 1 );
			if ( ( cropsnear.Count > 0 ) ) { from.SendMessage( "Il y a trop de plants � proximit�." ); return; }
			if ( this.BumpZ ) ++m_pnt.Z;
			if ( !from.Mounted ) from.Animate( 32, 5, 1, true, false, 0 );
			from.SendMessage("Vous plantez la graine.");
			this.Consume();
			Item item = new SmallBananaSeedling( from );
			item.Location = m_pnt;
			item.Map = m_map;
		}

		public SmallBananaSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SmallBananaSeedling : BaseCrop
	{
		private static Mobile m_sower;
		public Timer thisTimer;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Sower{ get{ return m_sower; } set{ m_sower = value; } }

		[Constructable]
		public SmallBananaSeedling( Mobile sower ) : base( 0xCA8 )
		{
			Movable = false;
			Name = "Small Banana Tree Seedling";
			m_sower = sower;
			init( this );
		}
		public static void init( SmallBananaSeedling plant )
		{
			plant.thisTimer = new CropHelper.GrowTimer( plant, typeof(SmallBananaCrop), plant.Sower );
			plant.thisTimer.Start();
		}
		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Mounted && !CropHelper.CanWorkMounted ) { from.SendMessage( "Le plant est trop petit pour pouvoir �tre r�colt� sur votre monture." ); return; }
			else from.SendMessage( "Votre pousse est trop jeune pour �tre r�colt�e." );
		}
		public SmallBananaSeedling( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); writer.Write( m_sower ); }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_sower = reader.ReadMobile();
			init( this );
		}
	}

	public class SmallBananaCrop : BaseCrop
	{
		private const int max = 6;
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
		public SmallBananaCrop() : this(null) { }

		[Constructable]
		public SmallBananaCrop( Mobile sower ) : base( 0xCA8 )
		{
			Movable = false;
			Name = "Small Banana Tree Plant";
			Hue = 0x000;
			m_sower = sower;
			m_lastvisit = DateTime.UtcNow;
			init( this, false );
		}

		public static void init ( SmallBananaCrop plant, bool full )
		{
			plant.PickGraphic = ( 0xCA8 );
			plant.FullGraphic = ( 0xCA8 );
			plant.LastPick = DateTime.UtcNow;
			plant.regrowTimer = new CropTimer( plant );
			if ( full ) { plant.Yield = plant.Capacity; ((Item)plant).ItemID = plant.FullGraphic; }
			else { plant.Yield = 0; ((Item)plant).ItemID = plant.PickGraphic; plant.regrowTimer.Start(); }
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_sower == null || m_sower.Deleted ) m_sower = from;
		

			if ( from.Mounted && !CropHelper.CanWorkMounted ) { from.SendMessage( "Vous ne pouvez r�colter sur une monture." ); return; }
			if ( DateTime.UtcNow > lastpicked.AddSeconds(3) )
			{
				lastpicked = DateTime.UtcNow;
				int cookValue = (int)from.Skills[SkillName.Cooking].Value / 20;
				if ( cookValue == 0 ) { from.SendMessage( "Vous ignorez comment r�colter cette pousse." ); return; }
				if ( from.InRange( this.GetWorldLocation(), 1 ) )
				{
					if ( m_yield < 1 ) { from.SendMessage( "Il n'y a rien � r�colter ici." ); }
					else
					{
						from.Direction = from.GetDirectionTo( this );
						from.Animate( from.Mounted ? 29:32, 5, 1, true, false, 0 );
						m_lastvisit = DateTime.UtcNow;
						if ( cookValue > m_yield ) cookValue = m_yield + 1;
						int pick = Utility.RandomMinMax( cookValue - 4, cookValue );
						if (pick < 0 ) pick = 0;
						if ( pick == 0 ) { from.SendMessage( "Votre r�colte ne porte pas fruit." ); return; }
						m_yield -= pick;
						from.SendMessage( "Vous r�coltez {0} crop{1}!", pick, ( pick == 1 ? "" : "s" ) );
						if (m_yield < 1) ((Item)this).ItemID = pickedGraphic;
						Banana crop = new Banana( pick );
						from.AddToBackpack( crop );
						if ( !regrowTimer.Running ) { regrowTimer.Start(); }
					}
				}
				else { from.SendMessage( "Vous �tes trop loin pour r�colter quelque chose." ); }
			}
		}

		private class CropTimer : Timer
		{
			private SmallBananaCrop i_plant;
			public CropTimer( SmallBananaCrop plant ) : base( TimeSpan.FromSeconds( 450 ), TimeSpan.FromSeconds( 15 ) )
			{
				Priority = TimerPriority.OneSecond;
				i_plant = plant;
			}
			protected override void OnTick()
			{
				if ( Utility.RandomBool() )
				{
					if ( ( i_plant != null ) && ( !i_plant.Deleted ) )
					{
						int current = i_plant.Yield;
						if ( ++current >= i_plant.Capacity )
						{
							current = i_plant.Capacity;
							((Item)i_plant).ItemID = i_plant.FullGraphic;
							Stop();
						}
						else if ( current <= 0 ) current = 1;
						i_plant.Yield = current;
					}
					else Stop();
				}
			}
		}

		public override void OnChop( Mobile from )
		{
			if ( !this.Deleted ) Chop( from );
		}

		public void Chop( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				if ( from == m_sower )
				{
					from.Direction = from.GetDirectionTo( this );
					double lumberValue = from.Skills[SkillName.Lumberjacking].Value / 100;
					if ( ( lumberValue > .5 ) && ( Utility.RandomDouble() <= lumberValue ) )
					{
						Banana fruit = new Banana( Utility.Random( m_yield +2 ) );
						from.AddToBackpack( fruit );
						int cnt = Utility.Random( 40 ) + 1;
						Log logs = new Log( cnt );
						from.AddToBackpack( logs );
					}
						this.Delete();
						from.SendMessage( "Vous coupez le plant." );
				}
				else from.SendMessage( "Ce plant ne vous appartient pas !" );
			}
			else from.SendLocalizedMessage( 500446 );
		}

		public SmallBananaCrop( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( m_lastvisit );
			writer.Write( m_sower );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_lastvisit = reader.ReadDateTime();
			m_sower = reader.ReadMobile();
			init( this, true );
		}
	}
}