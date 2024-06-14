using System;
using Server.Items;
using Server.Mobiles;
using Server.Commands;
using Server.CustomScripts;
using System.Collections.Generic;

namespace Server.Poker
{
	public class PokerDealer : Mobile
	{
		public static void Initialize()
		{
			CommandSystem.Register( "AddPokerSeat", AccessLevel.Seer, new CommandEventHandler( AddPokerSeat_OnCommand ) );
			CommandSystem.Register( "PokerKick", AccessLevel.Seer, new CommandEventHandler( PokerKick_OnCommand ) );

			EventSink.Disconnected += new DisconnectedEventHandler( EventSink_Disconnected );
		}

		private double m_Rake;
		private int m_RakeMax;
		private int m_MinBuyIn;
		private int m_MaxBuyIn;
		private int m_SmallBlind;
		private int m_BigBlind;
		private int m_MaxPlayers;
		private bool m_Active;
		private bool m_TournamentMode;
		private PokerGame m_Game;
		private List<Point3D> m_Seats;
		private Point3D m_ExitLocation;
		private Map m_ExitMap;

		private static int m_Jackpot;
		public static int Jackpot { get { return m_Jackpot; } set { m_Jackpot = value; } }

		[CommandProperty( AccessLevel.Seer )]
		public bool TournamentMode { get { return m_TournamentMode; } set { m_TournamentMode = value; } }
		[CommandProperty( AccessLevel.Seer)]
		public bool ClearSeats { get { return false; } set { m_Seats.Clear(); } }
		[CommandProperty( AccessLevel.Seer)]
		public int RakeMax { get { return m_RakeMax; } set { m_RakeMax = value; } }
		[CommandProperty( AccessLevel.Seer )]
		public int MinBuyIn { get { return m_MinBuyIn; } set { m_MinBuyIn = value; } }
		[CommandProperty( AccessLevel.Seer )]
		public int MaxBuyIn { get { return m_MaxBuyIn; } set { m_MaxBuyIn = value; } }
		[CommandProperty( AccessLevel.Seer )]
		public int SmallBlind { get { return m_SmallBlind; } set { m_SmallBlind = value; } }
		[CommandProperty( AccessLevel.Seer )]
		public int BigBlind { get { return m_BigBlind; } set { m_BigBlind = value; } }
		[CommandProperty( AccessLevel.Seer )]
		public Point3D ExitLocation { get { return m_ExitLocation; } set { m_ExitLocation = value; } }
		[CommandProperty( AccessLevel.Seer )]
		public Map ExitMap { get { return m_ExitMap; } set { m_ExitMap = value; } }
		[CommandProperty( AccessLevel.Seer )]
		public double Rake
		{
			get { return m_Rake; }
			set
			{
				if ( value > 1 )
					m_Rake = 1;
				else if ( value < 0 )
					m_Rake = 0;
				else
					m_Rake = value;
			}
		}
		[CommandProperty( AccessLevel.Seer )]
		public int MaxPlayers
		{
			get { return m_MaxPlayers; }
			set
			{
				if ( value > 22 ) m_MaxPlayers = 22;
				else if ( value < 0 ) m_MaxPlayers = 0; 
				else m_MaxPlayers = value;
			}
		}
		[CommandProperty( AccessLevel.Seer )]
		public bool Active
		{
			get { return m_Active; }
			set
			{
				List<PokerPlayer> toRemove = new List<PokerPlayer>();

				if ( !value )
					foreach ( PokerPlayer player in m_Game.Players.Players )
						if ( player.Mobile != null )
							toRemove.Add( player );

				for ( int i = 0; i < toRemove.Count; ++i )
				{
                    toRemove[i].Mobile.SendMessage("Le croupier de poker a été mis à l'état inactif par un maître de jeu, et vous êtes maintenant retirés du jeu de poker et être remboursé.");
					m_Game.RemovePlayer( toRemove[i] );
				}

				m_Active = value;
			}
		}

		public PokerGame Game { get { return m_Game; } set { m_Game = value; } }
		public List<Point3D> Seats { get { return m_Seats; } set { m_Seats = value; } }

		[Constructable]
		public PokerDealer()
			: this( 10 )
		{
		}

		[Constructable]
		public PokerDealer( int maxPlayers )
		{
			Blessed = true;
			CantWalk = true;
			InitStats( 100, 100, 100 );

			Title = "Croupier de poker";
			Hue = Utility.RandomSkinHue();
			NameHue = 0x35;

			if ( this.Female = Utility.RandomBool() )
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
			}

			Dress();

			MaxPlayers = maxPlayers;
			m_Seats = new List<Point3D>();
			m_Rake = 0.10;		//10% rake default
			m_RakeMax = 5000;	//5k maximum rake default
			m_Game = new PokerGame( this );
		}

		private void Dress()
		{
			AddItem( new Shirt( 0 ) );

			Item pants = new LongPants();
			pants.Hue = 1;
			AddItem( pants );

			Item shoes = new Shoes();
			shoes.Hue = 1;
			AddItem( shoes );

			Item sash = new Sash();
			sash.Hue = 1;
			AddItem( sash );

			Utility.AssignRandomHair( this );
		}

		private static JackpotInfo m_JackpotWinners;
		public static JackpotInfo JackpotWinners { get { return m_JackpotWinners; } set { m_JackpotWinners = value; } }

		public static void AwardJackpot()
		{
			if ( m_JackpotWinners != null && m_JackpotWinners.Winners != null && m_JackpotWinners.Winners.Count > 0 )
			{
				int award = m_Jackpot / m_JackpotWinners.Winners.Count;

				if ( award <= 0 )
					return;

				foreach ( PokerPlayer m in m_JackpotWinners.Winners )
				{
					if ( m != null && m.Mobile != null && m.Mobile.BankBox != null )
					{
						m.Mobile.BankBox.DropItem( new BankCheck( award ) );
                        World.Broadcast(1161, true, "{0} a remporté le jackpot de poker. {1} pièces d'or avec {2}", m.Mobile.Name, award.ToString("#,###"), HandRanker.RankString(m_JackpotWinners.Hand));
					}
				}

				m_Jackpot = 0;
				m_JackpotWinners = null;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !m_Active )
				from.SendMessage( "Cette table est inactive." );
			else if ( !InRange( from.Location, 8 ) )
				from.SendMessage( "I am too far away to do that" );
			else if ( m_MinBuyIn == 0 || m_MaxBuyIn == 0 )
				from.SendMessage( "Cette table est inactive." );
			else if ( m_MinBuyIn > m_MaxBuyIn )
				from.SendMessage( "Cette table est inactive." );
			else if ( m_Seats.Count < m_MaxPlayers )
				from.SendMessage( "Cette table est inactive." );
			else if ( m_Game.GetIndexFor( from ) != -1 )
				return; //TODO: Grab more chips from the player's bankbox
			else if ( m_Game.Players.Count >= m_MaxPlayers )
			{
				from.SendMessage( "Cette table est pleine." );
				base.OnDoubleClick( from );
			}
			else if ( m_Game.Players.Count < m_MaxPlayers )
			{
				//TODO: Send player the poker join gump
				from.CloseGump( typeof( PokerJoinGump ) );
				from.SendGump( new PokerJoinGump( from, m_Game ) );
			}
		}

		public override void OnDelete()
		{
			List<PokerPlayer> toRemove = new List<PokerPlayer>();

			foreach ( PokerPlayer player in m_Game.Players.Players )
				if ( player.Mobile != null )
					toRemove.Add( player );

			for ( int i = 0; i < toRemove.Count; ++i )
			{
                toRemove[i].Mobile.SendMessage("Le croupier de poker a été supprimé, et vous sont maintenant retirés du jeu de poker et être remboursé.");
				m_Game.RemovePlayer( toRemove[i] );
			}

			base.OnDelete();
		}

		public static void PokerKick_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( from == null )
				return;

			foreach ( Mobile m in from.GetMobilesInRange( 0 ) )
			{
				if ( m is CustomPlayerMobile)
				{
                    CustomPlayerMobile pm = (CustomPlayerMobile)m;

					PokerGame game = pm.PokerGame;

					if ( game != null )
					{
						PokerPlayer player = game.GetPlayer( m );

						if ( player != null )
						{
							game.RemovePlayer( player );
                            from.SendMessage( "Ils ont été retirés de la table de poker.");
							return;
						}
					}
				}
			}

            from.SendMessage("Personne n'a été trouvé à cette table de poker. Assurez-vous que vous êtes sur la case du croupier.");
		}

		static void EventSink_Disconnected( DisconnectedEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( from == null )
				return;

			if ( from is CustomPlayerMobile)
			{
                CustomPlayerMobile pm = (CustomPlayerMobile)from;

				PokerGame game = pm.PokerGame;

				if ( game != null )
				{
					PokerPlayer player = game.GetPlayer( from );

					if ( player != null )
						game.RemovePlayer( player );
				}
			}
		}

		public static void AddPokerSeat_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( from == null )
				return;

			string args = e.ArgString.ToLower();
			string[] argLines = args.Split( ' ' );
			int x = 0, y = 0, z = 0;

			try
			{
				x = Convert.ToInt32( argLines[0] );
				y = Convert.ToInt32( argLines[1] );
				z = Convert.ToInt32( argLines[2] );
			}
			catch { from.SendMessage( 0x22, "Usage: [AddPokerSeat <x> <y> <z>" ); return;  }

			bool success = false;
			foreach ( Mobile m in from.GetMobilesInRange( 0 ) )
			{
				if ( m is PokerDealer )
				{
					Point3D seat = new Point3D( x, y, z );

					if ( ((PokerDealer)m).AddPokerSeat( from, seat ) != -1 )
					{
                        from.SendMessage( "Un nouveau siège a été créé avec succès.");
						success = true;
						break;
					}
					else
					{
                        from.SendMessage( "Il n'y a pas plus de place à la table pour un autre siège. Essayez d'augmenter la valeur de MaxPlayers.");
						success = true;
						break;
					}
				}
			}

			if ( !success )
                from.SendMessage( "Aucun croupier de poker a été trouvé. (Placez-vous sur la case du croupier pour ajouter un siège.)");
		}

		public int AddPokerSeat( Mobile from, Point3D seat )
		{
			if ( m_Seats.Count >= m_MaxPlayers )
				return -1;

			m_Seats.Add( seat );
			return 0;
		}

		public bool SeatTaken( Point3D seat )
		{
			for ( int i = 0; i < m_Game.Players.Count; ++i )
				if ( m_Game.Players[i].Seat == seat )
					return true;

			return false;
		}

		public int RakeGold( int gold )
		{
			double amount = gold * m_Rake;
			return (int)( amount > m_RakeMax ? m_RakeMax : amount );
		}

		public PokerDealer( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); //version

			writer.Write( m_Active );
			writer.Write( m_SmallBlind );
			writer.Write( m_BigBlind );
			writer.Write( m_MinBuyIn );
			writer.Write( m_MaxBuyIn );
			writer.Write( m_ExitLocation );
			writer.Write( m_ExitMap );
			writer.Write( m_Rake );
			writer.Write( m_RakeMax );
			writer.Write( m_MaxPlayers );

			writer.Write( m_Seats.Count );

			for ( int i = 0; i < m_Seats.Count; ++i )
				writer.Write( m_Seats[i] );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
					m_Active = reader.ReadBool();
					m_SmallBlind = reader.ReadInt();
					m_BigBlind = reader.ReadInt();
					m_MinBuyIn = reader.ReadInt();
					m_MaxBuyIn = reader.ReadInt();
					m_ExitLocation = reader.ReadPoint3D();
					m_ExitMap = reader.ReadMap();
					m_Rake = reader.ReadDouble();
					m_RakeMax = reader.ReadInt();
					m_MaxPlayers = reader.ReadInt();

					int count = reader.ReadInt();
					m_Seats = new List<Point3D>();

					for ( int i = 0; i < count; ++i )
						m_Seats.Add( reader.ReadPoint3D() );

					break;
			}

			m_Game = new PokerGame( this );
		}

		public class JackpotInfo
		{
			private List<PokerPlayer> m_Winners;
			private ResultEntry m_Hand;
			private DateTime m_Date;
			
			public List<PokerPlayer> Winners { get { return m_Winners; } }
			public ResultEntry Hand { get { return m_Hand; } }
			public DateTime Date { get { return m_Date; } }

			public JackpotInfo( List<PokerPlayer> winners, ResultEntry hand, DateTime date )
			{
				m_Winners = winners;
				m_Hand = hand;
				m_Date = date;
			}
		}
	}
}