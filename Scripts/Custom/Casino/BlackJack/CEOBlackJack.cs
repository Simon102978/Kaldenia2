/*
Script Name: CEOBlackJack.cs
Author: CEO
Version: 1.1
Donations Accepted! Using this regularly on your shard? Feel free to drop me a few bucks via Paypal!
https://www.paypal.com/xclick/business=ceo%40easyuo%2ecom&item_name=CEO%20BlackJack%20Donation&no_shipping=0&no_note=
*/
//Use this if you're created a new machine with an untested ruleset that you need to profile
//#define PROFILE
// If you're using an SVN that supports List<> methods (remove the //s) to use those instead of arrays or add them for RC1.
#define RC2
// If you have XMLSpawner installed this definition will give you a larger property gump
#define XMLSPAWNER
//#undef XMLSPAWNER
using System;
using Server.Accounting;
using Server.Network;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;
#if RC2
using System.Collections.Generic;
#endif
using System.Collections;
using Server.CustomScripts;

namespace Server.Items
{
    //[DynamicFliping]
    //[Flipable(4465, 4466)]
	public class CEOBlackJack : Mobile
	{
		private bool m_Active = true;
		private int m_ErrorCode;
		private int m_OrigHue;

		//Stats and Totals
		private int m_Won = 0;
		private ulong m_TotalCollected = 0;
		private ulong m_TotalWon = 0;
		//private long m_TotalNetProfit = 0;
		private ulong m_TotalPlays = 0;

		private int m_OnCredit;
		private int m_CurrentBet = 100;
		private int m_Bet = 2;
		private int m_Escrow = 0;
		public int[] m_BetTable = new int[] { 10, 50, 100, 250, 500, 1000, 5000, 10000, 25000};
		public enum BetValues { bet10, bet50, bet100, bet250, bet500, bet1000, bet5000, bet10000, bet25000 }

		private Mobile m_SecurityCamMobile = null; // Set to a mobile to "watch" people playing
		private VerboseType m_SecurityChatter = VerboseType.Low;
		public enum VerboseType { Low, Medium, High }

		private CardDeck carddeck;
		private const int HANDSIZE = 12;
		private int m_SplitCount = 0;
		private int m_SplitAceCount = 0;
		//Mobile & timeout
		private Mobile m_InUseBy = null;
		private DateTime m_LastPlayed = DateTime.Now;
		private TimeSpan m_TimeOut;
		private TimeSpan m_IdleTimer = TimeSpan.FromMinutes(5); // How long can a person be standing at the machine not playing?

		//ATM Stuff
		private int m_CreditCashOut = 750000;
		private int m_CreditATMLimit = 50000;
		private int m_CreditATMIncrements = 100;

		//Config type stuff
		private bool m_HelpGump = false;
		private bool m_TestMode = false; // Make the game essentially free (no payouts!).
		private bool m_DealerDelay = true; // Dealer's cards are dealt one at a time
		private Casino m_Casino;
		private string m_CasinoName;
		private bool m_DoubleAfterSplit = true; // Can you double down after splitting a pair?
		private bool m_DealerHitsSoft17 = true; // Dealer must hit on soft 17 (Ace and six).
		private bool m_DealerTakesPush = true; // Dealer takes pushes (usually used with all cards up)
		private bool m_OfferInsurance = true; // Does Dealer offer insurance with ace showing?
		private bool m_Resplits = true; // Can a player split again? True = 3 resplits (4 hands allowed)
		private SplitAces m_SplitAces = SplitAces.Once; // Can player's split aces?
		private bool m_BJSplitAces21 = true; // Split Ace and 10 count as 21 and not BJ! 
		private bool m_BJSplitAcesPaysEven = true; // A blackjack from split aces pays even money
		private bool m_PlayerCardsFaceUp = true; // Player cards are dealt face up.
		private bool m_DealerCardsFaceUp = false; // Dealer cards are dealt face up.
		private DoubleDown m_DoubleDown = DoubleDown.Nine211;
		private BlackJackPays m_BlackJackPays = BlackJackPays.Three2Two;
		private short m_NumberOfDecks = 5;
		private bool m_ContinuousShuffle = false; // Shuffle after every turn.
		private bool m_CardSounds = true;
		private BetValues m_MinBet = BetValues.bet10;
		private BetValues m_MaxBet = BetValues.bet500;

		//Timers for Dealer Delayf
		private DealerTimer m_DealerTimer;

		public enum HandStatus { Waiting, InPlay, BlackJack, Win, Lose, Push, Bust, Double, Split, SplitAces }
		public enum GameStatus { Waiting, PlayerTurn, DealerTurn }
		public enum BlackJackPays { EvenMoney, Three2Two, Six2Five }
		public enum DoubleDown { AnyPair, Nine211, Ten11Only, ElevenOnly }
		public enum SplitAces { No, Once, NoLimit }
		public enum Casino { Other, CalNeva1, Reno2, Reno4, AtlanticCity1, AtlanticCity8, AtlanticCity8u, Circus2, Luxor5, Belagio6, Palms8 }
		// Use http://wizardofodds.com/blackjack/vegas.html to see calulated odds. Blackjack has very
		// low casino odds for the house, usually less then 1%. Make sure you don't inadvertantly create
		// a gold mine instead of a gold sink!

		public struct BJStruct
		{
			public GameStatus status;
			public bool doubleOn;
			public bool splitOn;
			public bool hitOn;
			public bool askInsurance;
			public short activehand;
			public short totalhands;
			public short largesthand;
			public short nextcard;
			public bool insured;
			public HandStruct[] HandInfo;
		}

		public struct HandStruct
		{
			public HandStatus status;
			public short totalcards;
			public int bet;
			public short bestscore;
			public short altscore;
			public short[] card;

		}

		public BJStruct m_BJInfo;

		public bool HelpGump
		{
			get { return m_HelpGump; }
			set
			{
				m_HelpGump = value;
			}
		}

		[CommandProperty(AccessLevel.Administrator)]
		public bool TestMode
		{
			get { return m_TestMode; }
			set
			{
				if (m_InUseBy != null)
				{
                    this.PublicOverheadMessage(0, (this.Hue == 907 ? 0 : this.Hue), false, "Impossible de modifier le mode de test en cours d'utilisation.");
					return;
				}
				m_TestMode = value;
				if (!m_TestMode)
				{
					m_OnCredit = 0;
				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Active
		{
			get { return m_Active; }
			set
			{
				if (!m_Active && value)
				{
					if (m_OrigHue != -1)
					{
						this.Hue = m_OrigHue;
						m_OrigHue = -1;
					}
					Effects.SendLocationEffect(new Point3D(this.X, this.Y + 1, this.Z), this.Map, 0x373A, 15, this.Hue - 1, 0);
					Effects.SendLocationEffect(new Point3D(this.X + 1, this.Y, this.Z), this.Map, 0x373A, 15, this.Hue - 1, 0);
					Effects.SendLocationEffect(new Point3D(this.X, this.Y, this.Z - 1), this.Map, 0x373A, 15, this.Hue - 1, 0);
					Effects.PlaySound(new Point3D(this.X, this.Y, this.Z), this.Map, 1481);
					this.PublicOverheadMessage(0, (this.Hue == 907 ? 0 : this.Hue), false, "Table de blackjack ouverte!");
				}
				else if (m_Active && !value)
				{
					m_OrigHue = this.Hue;
					this.Hue = 1001;
                    this.PublicOverheadMessage(0, this.Hue, false, "Table de blackjack fermée.");
				}
				m_Active = value;
				InvalidateProperties();
			}
		}

		
		private void BlackJackOffline(int error)
		{
			if (m_InUseBy != null)
			{
                m_InUseBy.SendMessage("Une erreur critique a forcé ce jeu hors ligne, notifiez un Maître de jeu s'il vous plaît.");
				m_InUseBy = null;
			}
			
            string text = String.Format("Erreur critique: {0}", error);
			SecurityCamera(0, text);
			m_ErrorCode = error;
			Active = false;
		
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int OrigHue
		{
			get { return m_OrigHue; }
			set { m_OrigHue = value; if (Active) Hue = m_OrigHue; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public Casino CasinoTheme
		{
			get { return m_Casino; }
			set
			{
				m_Casino = value;
				switch (m_Casino)
				{
					case Casino.CalNeva1:
						m_CasinoName = "Cal-Neva";
						m_DoubleAfterSplit = false; // Can you double down after splitting a pair?
						m_DealerHitsSoft17 = true; // Dealer must hit on soft 17 (Ace and six).
						m_DealerTakesPush = false; // Dealer takes pushes (usually used with all cards up)
						m_OfferInsurance = false; // Does Dealer offer insurance with ace showing?
						m_Resplits = true; // Can a player split again? True = 3 resplits (4 hands allowed)
						m_SplitAces = SplitAces.No; // Can player's split aces?
						m_BJSplitAces21 = true; // Split Ace and 10 count as 21 and not BJ! 
						m_BJSplitAcesPaysEven = true; // A blackjack from split aces pays even money
						m_PlayerCardsFaceUp = false; // Player cards are dealt face up.
						m_DealerCardsFaceUp = false; // Dealer cards are dealt face up.
						m_DoubleDown = DoubleDown.Ten11Only;
						m_BlackJackPays = BlackJackPays.Three2Two;
						m_NumberOfDecks = 1;
						m_ContinuousShuffle = false;
						m_MinBet = BetValues.bet10;
						m_MaxBet = BetValues.bet100;
                        OrigHue = 41;
						break;

					case Casino.Reno2:
						m_CasinoName = "Reno Hilton";
						m_DoubleAfterSplit = true;
						m_DealerHitsSoft17 = true; 
						m_DealerTakesPush = false; 
						m_OfferInsurance = true; 
						m_Resplits = true;
						m_SplitAces = SplitAces.NoLimit;
						m_BJSplitAces21 = true;
						m_BJSplitAcesPaysEven = true;
						m_PlayerCardsFaceUp = false;
						m_DealerCardsFaceUp = false;
						m_DoubleDown = DoubleDown.AnyPair;
						m_BlackJackPays = BlackJackPays.Three2Two;
						m_NumberOfDecks = 2;
						m_ContinuousShuffle = false;
						m_MinBet = BetValues.bet10;
						m_MaxBet = BetValues.bet500;
                        OrigHue = 41;
						break;

					case Casino.Reno4:
						m_CasinoName = "Reno Atlantis";
						m_DoubleAfterSplit = true;
						m_DealerHitsSoft17 = true;
						m_DealerTakesPush = false;
						m_OfferInsurance = true;
						m_Resplits = true;
						m_SplitAces = SplitAces.NoLimit;
						m_BJSplitAces21 = true;
						m_BJSplitAcesPaysEven = true;
						m_PlayerCardsFaceUp = true;
						m_DealerCardsFaceUp = false;
						m_DoubleDown = DoubleDown.AnyPair;
						m_BlackJackPays = BlackJackPays.Three2Two;
						m_NumberOfDecks = 4;
						m_ContinuousShuffle = false;
						m_MinBet = BetValues.bet50;
						m_MaxBet = BetValues.bet500;
                        OrigHue = 41;
						break;

					case Casino.AtlanticCity1:
						m_CasinoName = "Harrah's Atlantic City";
						m_DoubleAfterSplit = true;
						m_DealerHitsSoft17 = true;
						m_DealerTakesPush = false;
						m_OfferInsurance = true;
						m_Resplits = false;
						m_SplitAces = SplitAces.Once;
						m_BJSplitAces21 = true;
						m_BJSplitAcesPaysEven = true;
						m_PlayerCardsFaceUp = false;
						m_DealerCardsFaceUp = false;
						m_DoubleDown = DoubleDown.AnyPair;
						m_BlackJackPays = BlackJackPays.Six2Five;
						m_NumberOfDecks = 1;
						m_ContinuousShuffle = false;
						m_MinBet = BetValues.bet500;
						m_MaxBet = BetValues.bet5000;
                        OrigHue = 41;
						break;

					case Casino.AtlanticCity8:
						m_CasinoName = "Taj Mahal";
						m_DoubleAfterSplit = true;
						m_DealerHitsSoft17 = false;
						m_DealerTakesPush = false;
						m_OfferInsurance = true;
						m_Resplits = true;
						m_SplitAces = SplitAces.Once;
						m_BJSplitAces21 = true;
						m_BJSplitAcesPaysEven = true;
						m_PlayerCardsFaceUp = true;
						m_DealerCardsFaceUp = false;
						m_DoubleDown = DoubleDown.AnyPair;
						m_BlackJackPays = BlackJackPays.Three2Two;
						m_NumberOfDecks = 8;
						m_ContinuousShuffle = false;
						m_MinBet = BetValues.bet10;
						m_MaxBet = BetValues.bet1000;
                        OrigHue = 41;
						break;

					case Casino.AtlanticCity8u:
						m_CasinoName = "Trump Plaza";
						m_DoubleAfterSplit = false;
						m_DealerHitsSoft17 = true;
						m_DealerTakesPush = true;
						m_OfferInsurance = false;
						m_Resplits = false;
						m_SplitAces = SplitAces.Once;
						m_BJSplitAces21 = true;
						m_BJSplitAcesPaysEven = true;
						m_PlayerCardsFaceUp = true;
						m_DealerCardsFaceUp = true;
						m_DoubleDown = DoubleDown.ElevenOnly;
						m_BlackJackPays = BlackJackPays.Six2Five;
						m_NumberOfDecks = 8;
						m_ContinuousShuffle = true;
						m_MinBet = BetValues.bet100;
						m_MaxBet = BetValues.bet10000;
                        OrigHue = 41;
						break;

					case Casino.Circus2:
						m_CasinoName = "Las Vegas Circus Circus";
						m_DoubleAfterSplit = true;
						m_DealerHitsSoft17 = true;
						m_DealerTakesPush = false;
						m_OfferInsurance = true;
						m_Resplits = true;
						m_SplitAces = SplitAces.Once;
						m_BJSplitAces21 = true;
						m_BJSplitAcesPaysEven = true;
						m_PlayerCardsFaceUp = false;
						m_DealerCardsFaceUp = false;
						m_DoubleDown = DoubleDown.AnyPair;
						m_BlackJackPays = BlackJackPays.Three2Two;
						m_NumberOfDecks = 2;
						m_ContinuousShuffle = false;
						m_MinBet = BetValues.bet10;
						m_MaxBet = BetValues.bet500;
                        OrigHue = 41;
						break;

					case Casino.Luxor5:
						m_CasinoName = "Luxor";
						m_DoubleAfterSplit = true;
						m_DealerHitsSoft17 = true;
						m_DealerTakesPush = false;
						m_OfferInsurance = true;
						m_Resplits = true;
						m_SplitAces = SplitAces.NoLimit;
						m_BJSplitAces21 = true;
						m_BJSplitAcesPaysEven = true;
						m_PlayerCardsFaceUp = true;
						m_DealerCardsFaceUp = false;
						m_DoubleDown = DoubleDown.AnyPair;
						m_BlackJackPays = BlackJackPays.Three2Two;
						m_NumberOfDecks = 5;
						m_ContinuousShuffle = true;
						m_MinBet = BetValues.bet100;
						m_MaxBet = BetValues.bet1000;
                        OrigHue = 41;
						break;

					case Casino.Belagio6:
						m_CasinoName = "Belagio";
						m_DoubleAfterSplit = true;
						m_DealerHitsSoft17 = true;
						m_DealerTakesPush = false;
						m_OfferInsurance = true;
						m_Resplits = true;
						m_SplitAces = SplitAces.Once;
						m_BJSplitAces21 = true;
						m_BJSplitAcesPaysEven = true;
						m_PlayerCardsFaceUp = true;
						m_DealerCardsFaceUp = false;
						m_DoubleDown = DoubleDown.AnyPair;
						m_BlackJackPays = BlackJackPays.Three2Two;
						m_NumberOfDecks = 6;
						m_ContinuousShuffle = false;
						m_MinBet = BetValues.bet100;
						m_MaxBet = BetValues.bet1000;
                        OrigHue = 41;
						break;
						
					case Casino.Palms8:
						m_CasinoName = "The Palms";
						m_DoubleAfterSplit = true;
						m_DealerHitsSoft17 = true;
						m_DealerTakesPush = false;
						m_OfferInsurance = true;
						m_Resplits = true;
						m_SplitAces = SplitAces.Once;
						m_BJSplitAces21 = true;
						m_BJSplitAcesPaysEven = true;
						m_PlayerCardsFaceUp = true;
						m_DealerCardsFaceUp = false;
						m_DoubleDown = DoubleDown.AnyPair;
						m_BlackJackPays = BlackJackPays.Three2Two;
						m_NumberOfDecks = 8;
						m_ContinuousShuffle = false;
						m_MinBet = BetValues.bet10;
						m_MaxBet = BetValues.bet1000;
                        OrigHue = 41;
						break;

					default:
						m_CasinoName = "Stock";
						m_DoubleAfterSplit = true;
						m_DealerHitsSoft17 = true;
						m_DealerTakesPush = false;
						m_OfferInsurance = true;
						m_Resplits = true;
						m_SplitAces = SplitAces.Once;
						m_BJSplitAces21 = true;
						m_BJSplitAcesPaysEven = true;
						m_PlayerCardsFaceUp = false;
						m_DealerCardsFaceUp = false;
						m_DoubleDown = DoubleDown.AnyPair;
						m_BlackJackPays = BlackJackPays.Three2Two;
						m_NumberOfDecks = 2;
						m_ContinuousShuffle = false;
						m_MinBet = BetValues.bet10;
						m_MaxBet = BetValues.bet100;
                        OrigHue = 41;
						break;
				}
				Name = m_CasinoName + " Blackjack";
				m_Bet = (int)m_MinBet;
				m_CurrentBet = m_BetTable[m_Bet];
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string CasinoName
		{
			get { return m_CasinoName; }
			set
			{
				m_CasinoName = value;
				Name = m_CasinoName + " Blackjack";
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile SecurityCamMob
		{
			get { return m_SecurityCamMobile; }
			set { m_SecurityCamMobile = value; }
		}
				[CommandProperty(AccessLevel.GameMaster)]
		public bool CardSounds
		{
			get { return m_CardSounds; }
			set { m_CardSounds = value; }
		}

		
		[CommandProperty(AccessLevel.GameMaster)]
		public VerboseType SecurityChatter
		{
			get { return m_SecurityChatter; }
			set { m_SecurityChatter = value; }
		}

		public int Won
		{
			get { return m_Won; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool ResetStats
		{
			get { return true; }
			set
			{
				if (value)
				{
					m_TotalWon = 0;
					m_TotalCollected = 0;
					m_TotalPlays = 0;
					InvalidateProperties();
				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int CurrentBet
		{
			get { return m_CurrentBet; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int Escrow
		{
			get { return m_Escrow; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public BetValues MinBet
		{
			get { return m_MinBet; }
			set
			{
				m_MinBet = value;
				if (m_MinBet > m_MaxBet)
					m_MinBet = m_MaxBet;
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public BetValues MaxBet
		{
			get { return m_MaxBet; }
			set
			{
				m_MaxBet = value;
				if (m_MaxBet < m_MinBet)
					m_MaxBet = m_MinBet;
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public ulong TotalPlays
		{
			get { return m_TotalPlays; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public long TotalNetProfit
		{
			get { return (long)(m_TotalCollected - m_TotalWon); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public ulong TotalCollected
		{
			get { return m_TotalCollected; }
#if PROFILE
            set { m_TotalCollected = value; }
#endif
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public ulong TotalWon
		{
			get { return m_TotalWon; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public float WinningPercentage
		{
			get
			{
				if (m_TotalWon == 0 || m_TotalCollected == 0)
					return 0;
				if (m_TotalCollected == 0)
					return (float)0;
				return ((float)(m_TotalWon / (float)m_TotalCollected) * 100.00f);
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int CreditCashOutAt
		{
			get { return m_CreditCashOut; }
			set { m_CreditCashOut = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int CreditATMLimit
		{
			get { return m_CreditATMLimit; }
			set { m_CreditATMLimit = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int CreditATMIncrements
		{
			get { return m_CreditATMIncrements; }
			set { m_CreditATMIncrements = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile InUseBy
		{
			get { return m_InUseBy; }
			set { m_InUseBy = value; InvalidateProperties(); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int ErrorCode
		{
			get { return m_ErrorCode; }
			set { m_ErrorCode = value; InvalidateProperties(); }
		}

		
		[CommandProperty(AccessLevel.GameMaster)]
		public short NumberOfDecks
		{
			get { return m_NumberOfDecks; }
			set
			{
				if (value > 10)
					value = 10;
				if (value < 1)
					value = 1;
				m_NumberOfDecks = value;
				carddeck = new CardDeck(m_NumberOfDecks, 0);
			}
		}
		
		[CommandProperty(AccessLevel.GameMaster)]
		public bool ContinuousShuffle
		{
			get { return m_ContinuousShuffle; }
			set { m_ContinuousShuffle = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool PlayerCardsFaceUp
		{
			get { return m_PlayerCardsFaceUp; }
			set { m_PlayerCardsFaceUp = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DealerCardsFaceUp
		{
			get { return m_DealerCardsFaceUp; }
			set { m_DealerCardsFaceUp = value; }
		}
		
		[CommandProperty(AccessLevel.GameMaster)]
		public bool DealerTakesPush
		{
			get { return m_DealerTakesPush; }
			set { m_DealerTakesPush = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool OfferInsurance
		{
			get { return m_OfferInsurance; }
			set { m_OfferInsurance = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public DoubleDown DoubleRule
		{
			get { return m_DoubleDown; }
			set { m_DoubleDown = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DoubleAfterSplit
		{
			get { return m_DoubleAfterSplit; }
			set { m_DoubleAfterSplit = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Resplits
		{
			get { return m_Resplits; }
			set { m_Resplits = value; }
		}
	
		[CommandProperty(AccessLevel.GameMaster)]
		public SplitAces SplitAcesRule
		{
			get { return m_SplitAces; }
			set { m_SplitAces = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool BJSplitAces21
		{
			get { return m_BJSplitAces21; }
			set { m_BJSplitAces21 = value; }
		}
		
		[CommandProperty(AccessLevel.GameMaster)]
		public bool BJSplitAPaysEven
		{
			get { return m_BJSplitAcesPaysEven; }
			set { m_BJSplitAcesPaysEven = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public  BlackJackPays BJMultiplier
		{
			get { return m_BlackJackPays; }
			set { m_BlackJackPays = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DealerHitsSoft17
		{
			get { return m_DealerHitsSoft17; }
			set { m_DealerHitsSoft17 = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DealerDelay
		{
			get { return m_DealerDelay; }
			set { m_DealerDelay = value; }
		}

		
		[Constructable]
		public CEOBlackJack()
			: base()
		{
			//Movable = false;
            Blessed = true;
            Frozen = true;
            InitStats(100, 100, 100);

            Title = "Croupier de blackjack";
            Hue = Utility.RandomSkinHue();
            NameHue = 0x35;

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
                this.Name = NameList.RandomName("female");
            }
            else
            {
                this.Body = 0x190;
                this.Name = NameList.RandomName("male");
            }

            Dress();

			CasinoTheme = Casino.Other;
			carddeck = new CardDeck(m_NumberOfDecks, 0);
			Hue = m_OrigHue;
			Active = false;
			m_BJInfo.HandInfo = new HandStruct[5];
			for (int h = 0; h < 5; h++)
			{
				m_BJInfo.HandInfo[h].bet = 0;
				m_BJInfo.HandInfo[h].totalcards = 0;
				m_BJInfo.HandInfo[h].card = new short[12];
				for (int c = 0; c < 12; c++)
					m_BJInfo.HandInfo[h].card[c] = -1;
				m_BJInfo.HandInfo[h].bestscore = 0;
				m_BJInfo.HandInfo[h].altscore = 0;
			}
		}

        private void Dress()
        {
            AddItem(new Shirt(0));

            Item pants = new LongPants();
            pants.Hue = 1;
            AddItem(pants);

            Item shoes = new Shoes();
            shoes.Hue = 1;
            AddItem(shoes);

            Item sash = new Sash();
            sash.Hue = 1;
            AddItem(sash);

            Utility.AssignRandomHair(this);
        }

		public CEOBlackJack(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version 
			writer.Write(m_Active);
			writer.Write((int)m_Casino);
			writer.Write(m_CasinoName);

			writer.Write(m_TotalPlays);
			writer.Write(m_TotalCollected);
			writer.Write(m_TotalWon);
			writer.Write(m_ErrorCode);
			writer.Write(m_OrigHue);

			writer.Write(m_InUseBy);
			writer.Write(m_OnCredit);
			writer.Write(m_Escrow);

			writer.Write(m_SecurityCamMobile);
			writer.Write((int)m_SecurityChatter);

			writer.Write(m_Bet);
			writer.Write(m_TestMode);

			// Configs
			writer.Write(m_DealerDelay);
			writer.Write(m_DoubleAfterSplit);
			writer.Write(m_DealerHitsSoft17);
			writer.Write(m_DealerTakesPush);
			writer.Write(m_Resplits);
			writer.Write((int)m_SplitAces);
			writer.Write(m_BJSplitAces21);
			writer.Write(m_BJSplitAcesPaysEven);
			writer.Write((int)m_DoubleDown);
			writer.Write(m_PlayerCardsFaceUp);
			writer.Write(m_DealerCardsFaceUp);
			writer.Write(m_NumberOfDecks);
			writer.Write(m_ContinuousShuffle);
			writer.Write((int)m_MinBet);
			writer.Write((int)m_MaxBet);
			writer.Write((int)m_BlackJackPays);
			writer.Write(m_CardSounds);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Active = reader.ReadBool();
			m_Casino = (Casino)reader.ReadInt();
			m_CasinoName = reader.ReadString();
			m_TotalPlays = reader.ReadULong();
			m_TotalCollected = reader.ReadULong();
			m_TotalWon = reader.ReadULong();
			m_ErrorCode = reader.ReadInt();
			m_OrigHue = reader.ReadInt();

			m_InUseBy = reader.ReadMobile();
			m_OnCredit = reader.ReadInt();
			m_Escrow = reader.ReadInt();

			m_SecurityCamMobile = reader.ReadMobile();
			m_SecurityChatter = (VerboseType)reader.ReadInt();

			m_Bet = reader.ReadInt();
			m_TestMode = reader.ReadBool();

			m_DealerDelay = reader.ReadBool();
			m_DoubleAfterSplit = reader.ReadBool();
			m_DealerHitsSoft17 = reader.ReadBool();
			m_DealerTakesPush = reader.ReadBool();
			m_Resplits = reader.ReadBool();
			m_SplitAces = (SplitAces)reader.ReadInt();
			m_BJSplitAces21 = reader.ReadBool();
			m_BJSplitAcesPaysEven = reader.ReadBool();
			m_DoubleDown = (DoubleDown)reader.ReadInt();
			m_PlayerCardsFaceUp = reader.ReadBool();
			m_DealerCardsFaceUp = reader.ReadBool();
			m_NumberOfDecks = reader.ReadShort();
			m_ContinuousShuffle = reader.ReadBool();
			m_MinBet = (BetValues)reader.ReadInt();
			m_MaxBet = (BetValues)reader.ReadInt();
			m_BlackJackPays = (BlackJackPays)reader.ReadInt();
			m_CardSounds = reader.ReadBool();
			carddeck = new CardDeck(m_NumberOfDecks,0);
			m_BJInfo.HandInfo = new HandStruct[5];
			for (int h = 0; h < 5; h++)
			{
				m_BJInfo.HandInfo[h].bet = 0;
				m_BJInfo.HandInfo[h].totalcards = 0;
				m_BJInfo.HandInfo[h].card = new short[12];
				for (int c = 0; c < 12; c++)
					m_BJInfo.HandInfo[h].card[c] = -1;
				m_BJInfo.HandInfo[h].bestscore = 0;
				m_BJInfo.HandInfo[h].altscore = 0;
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			if (!m_Active)
			{
				if (m_ErrorCode == 0)
					list.Add(1060658, "Status\tOffline");
				else
					list.Add(1060658, "Status\tMaintenance Required({0})", m_ErrorCode);
				return;
			}
			else if (m_InUseBy == null)
				list.Add(1060658, "Status\tAvailable");
			else
			{
				list.Add(1060658, "Status\tIn Use");
				list.Add(1060659, "Player\t{0}", m_InUseBy.Name);
			}
		}

		//public override bool HandlesOnMovement { get { return (m_InUseBy != null && m_Active); } }// Tell the core that we implement OnMovement

		public override void OnMovement(Mobile m, Point3D oldLocation)
		{
			if (m_InUseBy != null)
			{
				if (!m_InUseBy.InRange(Location, 3) || m_InUseBy.Map == Map.Internal)
				{
					m_InUseBy.CloseGump(typeof(BlackJackCardGump));
					if (m_OnCredit != 0 && !m_TestMode)
					{
						m_InUseBy.PlaySound(52);
                        m_InUseBy.SendMessage("Hey, vous avez laissé un peu d'argent sur la table! Voilà pour vous!");
						if (m_Escrow > 0)
						{
							Credit(0, 0, m_Escrow); // Take their bet, too bad, so sad.
							m_Escrow = 0;
						}
						DoCashOut(m_InUseBy); // Give them their winnings
					}
					else
                        m_InUseBy.SendMessage("Vous marchez loin de cette table de jeu.");
					InUseBy = null;
					InvalidateProperties();
				}
			}
		}


		public override void OnDoubleClick(Mobile from)
		{
			if (!from.InRange(Location, 2) && (from.AccessLevel >= AccessLevel.GameMaster))
			{
#if XMLSPAWNER
                from.SendGump(new XmlPropertiesGump(from, this));
#else
				from.SendGump(new PropertiesGump(from, this));
#endif
				return;
			}

			if (!from.InRange(Location, 2) || !from.InLOS(this))
			{
                from.SendMessage( "Ceci est trop loin."); // That is too far away.
				return;
			}
			if (!m_Active)
			{
				if (m_InUseBy != null && (from == InUseBy))
				{
					from.CloseGump(typeof(BlackJackCardGump));
					if (m_OnCredit != 0)
						DoCashOut(from);
				}
                from.SendMessage( "Désolé, cette table de jeu est actuellement en maintenance.");
				return;
			}

			m_TimeOut = DateTime.Now - m_LastPlayed;
			string message = null;
			m_DealerTimer = null;
			if (m_InUseBy == null || m_InUseBy.Deleted)
			{
				m_Bet = (int)m_MinBet;
				m_CurrentBet = m_BetTable[m_Bet];
				message = "Bienvenue à la table de jeu de BlackJack!";
				m_BJInfo.status = GameStatus.Waiting;
				m_BJInfo.askInsurance = false;
				m_BJInfo.totalhands = m_BJInfo.largesthand = 0;
				m_OnCredit = m_Escrow = 0;
				InUseBy = from;
			}
			else
			{
				if (m_IdleTimer < m_TimeOut)
				{
					if (m_Escrow > 0)
						m_OnCredit += m_Escrow;
                    string tempName = m_InUseBy != null ? m_InUseBy.Name : "Quelqu'un";
					if (m_InUseBy != null && m_InUseBy != from && m_OnCredit != 0)
						DoCashOut(m_InUseBy); // Previous user disconnected or something? Give them their cash before releasing.
                    from.SendMessage( "Quelqu'un a laissé cette table inactive trop longtemps, il est maintenant à vous de jouer.", tempName); 
                    InUseBy = from;
					m_BJInfo.status = GameStatus.Waiting;
					m_BJInfo.totalhands = m_BJInfo.largesthand = 0;
					m_OnCredit = m_Escrow = 0;
				}
			}
			if (from == m_InUseBy)
			{
				if (message == null)
					message = "Rebienvenue";
				from.CloseGump(typeof(BlackJackCardGump),1);
			}
			else
			{
				string text = String.Format("Quelqu'un utilise présentement la table de jeu.", m_InUseBy.Name);
                from.SendMessage( text);
				return;
			}
			if (m_TestMode)
			{
				m_OnCredit = 100000;
			}
			m_HelpGump = false;		
			from.CloseGump(typeof(BlackJackCardGump),1);
			from.SendGump(new BlackJackCardGump(from, this, message));
		}

		public void PlayBlackJack(Mobile from)
		{
			PlayBlackJack(from, m_CurrentBet);
		}

		private void PlayBlackJack(Mobile from, int cost)
		{
			if (!from.InRange(this.Location, 10) || !from.InLOS(this))
			{
                from.SendMessage( "Vous êtes trop loin de la table de jeu pour y jouer.");
				RemovePlayer(from);
				return;
			}
			m_Escrow = 0;
			if (m_TestMode)
			{
				if (m_OnCredit < m_CurrentBet)
					m_OnCredit = 100000;
			}
			if (from.Backpack.ConsumeTotal(typeof(CasinoToken), 1))
			{
				m_Bet = 0;
				m_CurrentBet = cost = 100;
				Credit(m_CurrentBet); // Accounting mumbo jumbo to asure 100gp is recorded
				Debit(m_CurrentBet, true);
			}
			if (!GetBet(from, m_CurrentBet))
			{
				from.SendGump(new BlackJackCardGump(from, this, "Fonds insuffisants pour jouer!"));
				return;
			}
			if (m_ContinuousShuffle || carddeck.Remaining < 16)
				carddeck.QuickShuffle();
			m_LastPlayed = DateTime.Now;
			m_TotalPlays++;
			m_Won = m_SplitCount = m_SplitAceCount;
			m_BJInfo.status = GameStatus.PlayerTurn;
			m_BJInfo.totalhands = m_BJInfo.largesthand = 2;	
			m_BJInfo.askInsurance = false;
			m_BJInfo.splitOn = false;
			m_BJInfo.doubleOn = false;
			m_BJInfo.hitOn = true;
			m_BJInfo.activehand = 1;
			m_BJInfo.HandInfo[m_BJInfo.activehand].bet = m_CurrentBet;
			for (int c = 0; c < 12; c++)
				for (int h = 0; h < 4; h++)
					m_BJInfo.HandInfo[h].card[c] = -1;
			for (int c = 0; c < 2; c++)
				for (int h = 0; h < 2; h++)
					m_BJInfo.HandInfo[h].card[c] = carddeck.GetOneCard();
			m_BJInfo.HandInfo[0].totalcards = 2;
			m_BJInfo.HandInfo[1].totalcards = 2;
			m_BJInfo.HandInfo[0].status = HandStatus.Waiting;
			m_BJInfo.HandInfo[1].status = HandStatus.InPlay;
			/*
			 Testing specific card sets
			 */
			//m_BJInfo.HandInfo[0].card[0] = 9;
			//m_BJInfo.HandInfo[0].card[1] = 9;
			//m_BJInfo.HandInfo[1].card[0] = 7;
			//m_BJInfo.HandInfo[1].card[1] = 7;
			//
			from.CloseGump(typeof(BlackJackCardGump),1);
			for (int h = 0; h < 2; h++)
				m_BJInfo.HandInfo[h] = EvalHand(m_BJInfo.HandInfo[h]);
			if (!DealerCardsFaceUp && m_BJInfo.HandInfo[0].card[1] % 13 == 0)
				m_BJInfo.askInsurance = m_OfferInsurance ? true : false;
			if (!m_BJInfo.askInsurance)
			{
				string message;
				if (CheckForBlackJack(m_BJInfo.HandInfo[0]))
				{
					m_BJInfo.status = GameStatus.Waiting;
					m_BJInfo.HandInfo[0].status = HandStatus.BlackJack;
					DoLosingSound(from);
					if (CheckForBlackJack(m_BJInfo.HandInfo[1]))
					{
						if (DealerTakesPush)
						{
							message = "Désolé, le croupier prend le push";
							m_BJInfo.HandInfo[1].status = HandStatus.Lose;
							RecordCollected(m_BJInfo.HandInfo[1].bet);
						}
						else
						{
                            if (from.Female)
                                message = "Chanceuse, c'est un push.";
                            else
                                message = "Chanceux, c'est un push.";
							m_BJInfo.HandInfo[1].status = HandStatus.Push;
							Credit(m_BJInfo.HandInfo[1].bet);
						}
					}
					else
					{
						message = "Le croupier a un blackjack!";
						m_BJInfo.HandInfo[1].status = HandStatus.Lose;
						RecordCollected(m_BJInfo.HandInfo[1].bet);
					}
					from.SendGump(new BlackJackCardGump(from, this, message));
					return;
				}
				else if (CheckForBlackJack(m_BJInfo.HandInfo[1]))
				{
					m_BJInfo.HandInfo[1].status = HandStatus.BlackJack;
					Credit((int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, false)),
							(int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, true)),
							(int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, true)));
					DoBlackJackSound(from, this);
					m_BJInfo.status = GameStatus.Waiting;
					message = "Vous avez un blackjack!";
					from.SendGump(new BlackJackCardGump(from, this, message));
					return;
				}
			}
			m_BJInfo.splitOn = OkToSplit(m_BJInfo);
			m_BJInfo.doubleOn = OkToDouble(m_BJInfo, m_DoubleDown);
			from.SendGump(new BlackJackCardGump(from, this, null));
			from.PlaySound(739);
			if (from.Hidden && from.AccessLevel == AccessLevel.Player) // Don't let someone sit at the slots and play hidden
			{
				from.Hidden = false;
				from.SendLocalizedMessage(500816); // You have been revealed!
			}
		}

		private bool GetBet(Mobile from, int amount)
		{
			int checkamount;
			if (from.Backpack.ConsumeTotal(typeof(CasinoToken), 1))
			{
				m_Bet = 0;
				m_CurrentBet = amount = 100;
				Credit(amount);
				Debit(amount, true);
				return true;
			}
			else if (from.Backpack.ConsumeTotal(typeof(Gold), amount))
			{
				Credit(amount);
				Debit(amount, true);
				return true;
			}
			else if (m_OnCredit >= amount)
			{
				Debit(amount, true);
				return true;
			}
			else if (CashCheck(from, out checkamount))
			{
                string message = string.Format("Encaissement de chèque de {0} de votre sac.", checkamount);
				Credit(checkamount);
                from.SendMessage( message);
				return GetBet(from, amount);
			}
			return false;
		}

		private bool CashCheck(Mobile m, out int checkamount)
		{
			checkamount = 0;
			if (m == null)
			{
                m.SendMessage( "Ce jeu a besoin d'entretien.");
                SecurityCamera(0, "Ce jeu a besoin d'entretien.");
				Active = false;
				return false;
			}
			if (m == null || m.Backpack == null || m_TestMode)
				return false;
#if RC2
			List<Item> packlist = m.Backpack.Items;
#else
			ArrayList packlist = m.Backpack.Items;
#endif

			for (int i = 0; i < packlist.Count; ++i)
			{
				Item item = (Item)packlist[i];
				if (item != null && !item.Deleted && item is BankCheck)
				{
					checkamount = ((BankCheck)item).Worth;
					item.Delete();
					if (item.Deleted)
					{
						string text = null;
						Effects.PlaySound(new Point3D(this.X, this.Y, this.Z), this.Map, 501);
						text = String.Format("{0}:Chèque={1}.", m.Name, checkamount);
						SecurityCamera(checkamount > 5000 ? 0 : 1, text);
                        text = String.Format("À crédit={1}.", m.Name, m_OnCredit);
						SecurityCamera(m_OnCredit > 10000 ? 1 : 2, text);
					}
					else
					{
                        m.SendMessage( "Il y a un problème en essayant d'encaisser un chèque dans votre sac à dos, ce jeu est déconnecté. Demandez l'aide d'un Maître de jeu.");
						BlackJackOffline(9503);
						return false;
					}
					return true;
				}
			}
			return false;
		}

		public bool RemovePlayer(Mobile from)
		{
			if (from == null )
			{
                this.PublicOverheadMessage(0, (this.Hue == 907 ? 0 : this.Hue), false, "Ce jeu a besoin d'entretien.");
                SecurityCamera(0, "Ce jeu a besoin d'entretien.");
				Active = false;
				return false;
			}
			string text = String.Format("Supprimé: {0}.", from.Name);
			SecurityCamera(0, text);
			if (m_OnCredit != 0)
				DoCashOut(from);
			m_InUseBy = null;
			InvalidateProperties();
			return true;
		}

		private void DoCashOut(Mobile from)
		{
			if (m_TestMode)
			{
				m_OnCredit = 0;
				return;
			}
			else if (from == null)
			{
                this.PublicOverheadMessage(0, (this.Hue == 907 ? 0 : this.Hue), false, "Ce jeu a besoin d'entretien.");
                SecurityCamera(0, "Ce jeu a besoin d'entretien.");
				Active = false;
				return;
			}
			else if (m_OnCredit == 0 || from.Deleted)
				return;
			else if (!m_Active && (m_ErrorCode == 9500 || m_ErrorCode == 9501 || m_ErrorCode == 9502)) // Prevent a loop cashing out
				return;
			else if (m_OnCredit < 0) // This should never happen but protect against some kind of overflow and a wild payout
			{
				if (from.AccessLevel >= AccessLevel.GameMaster) // Allow a GM to clear out the invalid amount
				{
                    from.SendMessage( "Invalide montant remporté ({0}), réinitialiser à 0.", m_OnCredit);
					m_OnCredit = m_Won = 0;
				}
                from.SendMessage( "Il y a un problème en essayant d'encaisser un chèque dans votre sac à dos, ce jeu est déconnecté. Demandez l'aide d'un Maître de jeu.");
				BlackJackOffline(9502);
				return;
			}
			int credit = m_OnCredit;
			if (m_OnCredit < 1000)
			{
				try
				{
					from.AddToBackpack(new Gold(m_OnCredit));
                    from.SendMessage( "{0} pièces d'or a été ajouté à votre sac.", m_OnCredit);
				}
				catch
				{
                    from.SendMessage( "Il y a un problème en essayant d'encaisser un chèque dans votre sac à dos, ce jeu est déconnecté. Demandez l'aide d'un Maître de jeu.");
					BlackJackOffline(9500);
					return;
				}
			}
			else 
			{
				try
				{
					from.AddToBackpack(new BankCheck(m_OnCredit));
                    from.SendMessage( "Un chèque de banque de {0} pièces d'or a été placé dans votre sac.", m_OnCredit);
				}
				catch
				{
                    from.SendMessage("Il y a un problème en essayant d'encaisser un chèque dans votre sac à dos, ce jeu est déconnecté. Demandez l'aide d'un Maître de jeu.");
					BlackJackOffline(9501);
					return;
				}

			}
			m_OnCredit = m_Won = 0;
			m_InUseBy = null;
			string text = null;
			if (credit >= 10000)
			{
				text = String.Format("Quelqu'un a encaissé {1} pièces d'or!", from.Name, credit);
				this.PublicOverheadMessage(0, (this.Hue == 907 ? 0 : this.Hue), false, text);
			}
            text = String.Format("Quelqu'un a encaissé {1} pièces d'or!", from.Name, credit);
			SecurityCamera(m_OnCredit >= 10000 ? 0 : 1, text);			
			from.PlaySound(52);
			from.PlaySound(53);
			from.PlaySound(54);
			from.PlaySound(55);
		}

		public void Hit(Mobile from, bool gump)
		{
			string message = null;
			m_BJInfo.doubleOn = false;
			m_BJInfo.HandInfo[m_BJInfo.activehand].status = HandStatus.InPlay;
			m_BJInfo.HandInfo[m_BJInfo.activehand].card[m_BJInfo.HandInfo[m_BJInfo.activehand].totalcards] = carddeck.GetOneCard();
			m_BJInfo.HandInfo[m_BJInfo.activehand].totalcards++;
			if (m_CardSounds && m_BJInfo.status != GameStatus.DealerTurn)
				from.PlaySound(85);
			if (m_BJInfo.largesthand < m_BJInfo.HandInfo[m_BJInfo.activehand].totalcards)
				m_BJInfo.largesthand = m_BJInfo.HandInfo[m_BJInfo.activehand].totalcards;
			m_BJInfo.HandInfo[m_BJInfo.activehand] = EvalHand(m_BJInfo.HandInfo[m_BJInfo.activehand]);
			if (m_BJInfo.HandInfo[m_BJInfo.activehand].bestscore > 21)
			{
				RecordCollected(m_BJInfo.HandInfo[m_BJInfo.activehand].bet);
				m_BJInfo.HandInfo[m_BJInfo.activehand].status = HandStatus.Bust;
                message = m_BJInfo.totalhands > 2 ? string.Format("La main {0} a dépassé 21.", m_BJInfo.activehand) : "Vous avez dépassé 21.";
				DoLosingSound(from);
				Stand(from, message);
				return;
			}
			if (m_BJInfo.HandInfo[m_BJInfo.activehand].totalcards > 10)
			{
				m_BJInfo.HandInfo[m_BJInfo.activehand].status = HandStatus.Waiting;
                message = (m_BJInfo.totalhands > 2 ? string.Format("La main {0} ", m_BJInfo.activehand) : "Votre main ") + "doit tenir à onze cartes.";
				Stand(from, message);
				return;
			}
			if (gump)
				from.SendGump(new BlackJackCardGump(from, this, message));
		}

		public void Stand(Mobile from)
		{
			Stand(from, null);
		}

		public void Stand(Mobile from, string message)
		{
			if (m_BJInfo.activehand == 0)
				return; // Dealer Standing
			m_BJInfo.HandInfo[m_BJInfo.activehand].altscore = 0; // go with the best score
			m_BJInfo.activehand--;
			if (m_BJInfo.activehand == 0)
			{
				DealersTurn(from);
				return;
			}
			else
			{
				if (m_BJInfo.HandInfo[m_BJInfo.activehand].status == HandStatus.Split)
				{
					Hit(from, false);
					if (AcesX2(m_BJInfo))
					{
						m_BJInfo.doubleOn = false;
						m_BJInfo.splitOn = true;
						m_BJInfo.hitOn = false;
						from.SendGump(new BlackJackCardGump(from, this, message));
						return;
					}
					if (m_DoubleAfterSplit)
						m_BJInfo.doubleOn = OkToDouble(m_BJInfo, m_DoubleDown);
					if (m_BJInfo.HandInfo[m_BJInfo.activehand].card[0] % 13 == 0)
					{
						m_BJInfo.HandInfo[m_BJInfo.activehand].status = HandStatus.SplitAces;
						if (!m_BJSplitAces21 && CheckForBlackJack(m_BJInfo.HandInfo[m_BJInfo.activehand]))
						{
							if (PlayerCardsFaceUp)
							{
								m_BJInfo.HandInfo[m_BJInfo.activehand].status = HandStatus.BlackJack;
								DoBlackJackSound(from, this);
							}
							if (m_BJSplitAcesPaysEven)
							{
								Credit(m_BJInfo.HandInfo[m_BJInfo.activehand].bet * 2,
									m_BJInfo.HandInfo[m_BJInfo.activehand].bet,
									m_BJInfo.HandInfo[m_BJInfo.activehand].bet);
							}
							else
							{
								Credit((int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, false)),
										(int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, true)),
										(int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, true)));
							}
						}
						m_BJInfo.hitOn = false;
						Stand(from, message);
						return;
					}
					else
					{
						m_BJInfo.hitOn = true;
						m_BJInfo.splitOn = OkToSplit(m_BJInfo);
					}
					from.SendGump(new BlackJackCardGump(from, this, message));
					return;
				}
				else
				{
					if (m_CardSounds)
						from.PlaySound(85);
					Stand(from, message);
				}
			}
		}

		public void Double(Mobile from)
		{
			if (!GetBet(from, m_CurrentBet))
			{
				from.SendGump(new BlackJackCardGump(from, this, "Fonds insuffisant pour doubler."));
				return;
			}
			m_BJInfo = Double(from, m_CurrentBet, m_BJInfo);
			m_BJInfo.HandInfo[m_BJInfo.activehand] = EvalHand(m_BJInfo.HandInfo[m_BJInfo.activehand]);
			if (m_BJInfo.HandInfo[m_BJInfo.activehand].bestscore > 21)
			{
				m_BJInfo.HandInfo[m_BJInfo.activehand].status = HandStatus.Bust;
				RecordCollected(m_BJInfo.HandInfo[m_BJInfo.activehand].bet);
			}
			if (m_CardSounds)
				from.PlaySound(85);
			Stand(from);
		}
		
		private BJStruct Double(Mobile from, int bet, BJStruct bj)
		{
			bj.HandInfo[bj.activehand].bet += bet;
			bj.doubleOn = false;
			bj.HandInfo[bj.activehand].status = HandStatus.Double;
			bj.HandInfo[bj.activehand].card[bj.HandInfo[bj.activehand].totalcards] = carddeck.GetOneCard();
			bj.HandInfo[bj.activehand].totalcards++;
			if (bj.largesthand < bj.HandInfo[bj.activehand].totalcards)
				bj.largesthand = bj.HandInfo[bj.activehand].totalcards;
			return bj;
		}

		public void Split(Mobile from)
		{
			if (!GetBet(from, m_CurrentBet))
			{
				from.SendGump(new BlackJackCardGump(from, this, "Fonds insuffisants pour séparer."));
				return;
			}
			m_BJInfo = Split(from, m_CurrentBet, m_BJInfo);
			m_SplitCount++;
			Hit(from, false);
			if (AcesX2(m_BJInfo))
			{
				m_BJInfo.doubleOn = false;
				m_BJInfo.splitOn = true;
				m_BJInfo.hitOn = false;
				if (CardSounds)
					from.PlaySound(85);
				from.SendGump(new BlackJackCardGump(from, this, null));
				return;
			}
			if (m_BJInfo.HandInfo[m_BJInfo.activehand].card[0] % 13 == 0)
			{
				m_BJInfo.HandInfo[m_BJInfo.activehand].status = HandStatus.SplitAces;
				if (!m_BJSplitAces21 && CheckForBlackJack(m_BJInfo.HandInfo[m_BJInfo.activehand]))
				{
					if (PlayerCardsFaceUp)
					{
						m_BJInfo.HandInfo[m_BJInfo.activehand].status = HandStatus.BlackJack;
					}
					if (m_BJSplitAcesPaysEven)
					{
						Credit(m_BJInfo.HandInfo[m_BJInfo.activehand].bet * 2,
							m_BJInfo.HandInfo[m_BJInfo.activehand].bet,
							m_BJInfo.HandInfo[m_BJInfo.activehand].bet);
					}
					else
					{
						Credit((int)(m_BJInfo.HandInfo[m_BJInfo.activehand].bet * BlackJackMultiplier(m_BlackJackPays, false)),
							(int)(m_BJInfo.HandInfo[m_BJInfo.activehand].bet * BlackJackMultiplier(m_BlackJackPays, true)),
							(int)(m_BJInfo.HandInfo[m_BJInfo.activehand].bet * BlackJackMultiplier(m_BlackJackPays, true)));
					}
				}
				m_BJInfo.hitOn = false;
				Stand(from);
				return;
			}
			if (m_DoubleAfterSplit)
				m_BJInfo.doubleOn = OkToDouble(m_BJInfo, m_DoubleDown);
			m_BJInfo.splitOn = OkToSplit(m_BJInfo);
			from.SendGump(new BlackJackCardGump(from, this, null));
		}

		private static bool OkToDouble(BJStruct bj, DoubleDown ddRule)
		{
			if (ddRule == DoubleDown.AnyPair)
				return true;
			else if (ddRule == DoubleDown.Nine211)
			{
				if (bj.HandInfo[bj.activehand].bestscore == 9 ||
					bj.HandInfo[bj.activehand].bestscore == 10 ||
					bj.HandInfo[bj.activehand].bestscore == 11 ||
					bj.HandInfo[bj.activehand].altscore == 9 ||
					bj.HandInfo[bj.activehand].altscore == 10 ||
					bj.HandInfo[bj.activehand].altscore == 11)
					return true;
				else
					return false;
			}
			else if (ddRule == DoubleDown.Ten11Only)
			{
				if (bj.HandInfo[bj.activehand].bestscore == 10 ||
				bj.HandInfo[bj.activehand].bestscore == 11 ||
				bj.HandInfo[bj.activehand].altscore == 10 ||
				bj.HandInfo[bj.activehand].altscore == 11)
					return true;
				else
					return false;
			}
			else if (ddRule == DoubleDown.ElevenOnly)
			{
				if (bj.HandInfo[bj.activehand].bestscore == 11 ||
				bj.HandInfo[bj.activehand].altscore == 11)
					return true;
				else
					return false;
			}
			return false;
		}

		private bool OkToSplit(BJStruct bj)
		{
			if ((bj.HandInfo[bj.activehand].card[0] % 13) == 0 && (bj.HandInfo[bj.activehand].card[1] % 13) == 0)
				return AcesX2(bj);
				if (!m_Resplits && m_SplitCount != 0)
				return false;
			if (bj.HandInfo[bj.activehand].card[0] % 13 == bj.HandInfo[bj.activehand].card[1] % 13 && bj.totalhands < 5)
				return true;
			return false;
		}

		private bool AcesX2(BJStruct bj)
		{
			switch (m_SplitAces)
			{
				case SplitAces.No:
					return false;
				case SplitAces.Once:
					if (m_SplitAceCount != 0)
						return false;
					break;
				default:
					break;
			}
			if (!m_Resplits && m_SplitCount != 0)
				return false;
			if ((bj.HandInfo[bj.activehand].card[0] % 13) == 0 && (bj.HandInfo[bj.activehand].card[1] % 13) == 0 && bj.totalhands < 5)
			{
				m_SplitAceCount++;
				return true;
			}
			return false;
		}

		private static BJStruct Split(Mobile from, int bet, BJStruct bj)
		{
			short nexthand = bj.totalhands;
			bj.HandInfo[nexthand].card[0] = bj.HandInfo[bj.activehand].card[1]; 
			bj.HandInfo[bj.activehand].card[1] = -1;
			bj.HandInfo[bj.activehand].totalcards = 1;
			bj.HandInfo[bj.activehand] = EvalHand(bj.HandInfo[bj.activehand]);
			bj.HandInfo[bj.activehand].status = HandStatus.Split;
			bj.HandInfo[nexthand].bet = bet;
			bj.totalhands++;
			bj.HandInfo[nexthand].totalcards = 1;
			bj.activehand = nexthand;
			return bj;
		}

		public void ProcessInsurance(Mobile from, int choice)
		{
			m_BJInfo.askInsurance = false;
			m_BJInfo.insured = false;
			string message = "Le croupier n'a pas blackjack.";
			if (choice != 0)
			{
				if (CheckForBlackJack(m_BJInfo.HandInfo[0]))
				{
					m_BJInfo.status = GameStatus.Waiting;
					m_BJInfo.HandInfo[0].status = HandStatus.BlackJack;
					message = "Vous auriez du prendre l'assurance!";
					DoLosingSound(from);
					if (CheckForBlackJack(m_BJInfo.HandInfo[1]))
					{
						if (DealerTakesPush)
						{
							message += " Le croupier prend le push.";
							m_BJInfo.HandInfo[1].status = HandStatus.Lose;
							RecordCollected(m_BJInfo.HandInfo[1].bet);
						}
						else
						{
                            if (from.Female)
							    message += " Chanceuse! Push!";
                            else
                                message += " Chanceux! Push!";
							m_BJInfo.HandInfo[1].status = HandStatus.Push;
							m_OnCredit += (int)(m_BJInfo.HandInfo[1].bet);
						}
					}
					else
					{
						m_BJInfo.HandInfo[1].status = HandStatus.Lose;
						RecordCollected(m_BJInfo.HandInfo[1].bet);
					}
				}
				else if (CheckForBlackJack(m_BJInfo.HandInfo[1]))
				{
					m_BJInfo.status = GameStatus.Waiting;
					m_BJInfo.HandInfo[0].status = HandStatus.Lose;
					m_BJInfo.HandInfo[1].status = HandStatus.BlackJack;
					Credit((int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, false)),
						(int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, true)),
						(int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, true)));
                    message = "Le croupier n'a pas de Blackjack, mais vous oui!";
					DoBlackJackSound(from, this);
				}
				from.SendGump(new BlackJackCardGump(from, this, message));
				return;
			}
			else if (!GetBet(from, m_CurrentBet/2))
			{
				from.SendGump(new BlackJackCardGump(from, this, "Fonds insuffisant pour l'assurance."));
				return;
			}
			m_BJInfo.insured = true;
			RecordCollected(m_BJInfo.HandInfo[1].bet/2, false);
			if (CheckForBlackJack(m_BJInfo.HandInfo[0]))
			{
				message = "Assurance payée!";
				Credit((int)(m_BJInfo.HandInfo[1].bet * 1.50),
						(int)(m_BJInfo.HandInfo[1].bet/2.00),
						(int)(m_BJInfo.HandInfo[1].bet * 1.50));
				m_BJInfo.status = GameStatus.Waiting;
				m_BJInfo.HandInfo[0].status = HandStatus.BlackJack;
				m_BJInfo.HandInfo[0].totalcards = 2;
				if (CheckForBlackJack(m_BJInfo.HandInfo[1]))
				{
					if (DealerTakesPush)
					{
						message += " Le croupier prendre le push.";
						m_BJInfo.HandInfo[1].status = HandStatus.Lose;
						RecordCollected(m_BJInfo.HandInfo[1].bet);
					}
					else
					{
						message += " Et c'est un push!";
						m_BJInfo.HandInfo[1].status = HandStatus.Push;
						m_OnCredit += (int)(m_BJInfo.HandInfo[1].bet); // and now even money	
					}
				}
				else
				{
					m_BJInfo.HandInfo[1].status = HandStatus.Lose; // Basically loses 1/2 bet
					RecordCollected(m_BJInfo.HandInfo[1].bet);
				}
				from.SendGump(new BlackJackCardGump(from, this, message));
				return;
			}
			else if (CheckForBlackJack(m_BJInfo.HandInfo[1]))
			{
				m_BJInfo.HandInfo[1].status = HandStatus.BlackJack;
				Credit((int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, false)),
						(int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, true)),
						(int)(m_BJInfo.HandInfo[1].bet * BlackJackMultiplier(m_BlackJackPays, true)));
				DoBlackJackSound(from, this);
				m_BJInfo.status = GameStatus.Waiting;
                message = "Le croupier n'a pas de Blackjack, mais vous oui!";
			}
			from.SendGump(new BlackJackCardGump(from, this, message));
		}

		private static HandStruct EvalHand(HandStruct hand)
		{
			hand.bestscore = 0;
			hand.altscore = 0;
			bool AceFound = false;
			for (int c = 0; c < hand.totalcards; c++)
			{
				if ((hand.card[c] % 13) == 0)
					AceFound = true;
				hand.bestscore += CardValue(hand.card[c], false);
			}
			if (AceFound)
			{
				hand.altscore = hand.bestscore;
				hand.bestscore = 0;
				int Ace = 0;
				for (int c = 0; c < hand.totalcards; c++)
				{
					if ((hand.card[c] % 13) == 0)
						Ace++;
					if (Ace == 1)
						hand.bestscore += CardValue(hand.card[c], true);
					else
						hand.bestscore += CardValue(hand.card[c], false);
				}
				if (hand.bestscore > 21)
				{
					hand.bestscore = hand.altscore;
					hand.altscore = 0;
				}
			}
			return hand;
		}

		private static bool CheckForBlackJack(HandStruct hand)
		{
			return (CardValue(hand.card[0], true) + CardValue(hand.card[1], true)) == 21 ? true : false;
		}

		private static short CardValue(short card, bool AceHigh)
		{
			short c = (short) (card % 13);
			if (c == 0)
				return (short) (AceHigh ? 11 : 1);
			return (short) (c > 8 ? 10 : c + 1);
		}

		private void DealersTurn(Mobile from)
		{
			bool DrawCards = false;
			for (int h = 1; h < m_BJInfo.totalhands; h++)
			{
				if (m_BJInfo.HandInfo[h].status != HandStatus.Bust && m_BJInfo.HandInfo[h].status != HandStatus.BlackJack)
					DrawCards = true;
			}
			if (!DrawCards)
			{
				m_BJInfo.status = GameStatus.Waiting;
				from.SendGump(new BlackJackCardGump(from, this, null));
				return;
			}
			if (m_DealerDelay)
			{
				string message = null;
				m_BJInfo.status = GameStatus.DealerTurn;
				if (DealerMustHit(m_BJInfo.HandInfo[0], m_DealerHitsSoft17, out message))
				{
					from.SendGump(new BlackJackCardGump(from, this, message));
					Hit(from, false);
					if (m_DealerTimer == null)
						m_DealerTimer = new DealerTimer(this, from, TimeSpan.FromMilliseconds(750));
					m_DealerTimer.Start();
				}
				else
					DealerStands(from, message);
			}
			else
				DealerHits(from);
		}

		private void DealerHits(Mobile from)
		{
			string message = null;
			bool hitme = true;
			while (hitme)
			{
				m_BJInfo.HandInfo[0] = EvalHand(m_BJInfo.HandInfo[0]);
				if (DealerMustHit(m_BJInfo.HandInfo[0], m_DealerHitsSoft17, out message))
					Hit(from, false);
				else
					hitme = false;
				if (m_BJInfo.HandInfo[0].totalcards > 10)
				{
					m_BJInfo.status = GameStatus.Waiting;
                    message = "Le croupier doit se tenir avec onze cartes.";
					hitme = false;
				}
			}
			DealerStands(from, null);
		}

		private void DealerChecksCards(Mobile from)
		{
			if (m_InUseBy != from)
			{
				m_BJInfo.status = GameStatus.Waiting;
				if (m_DealerTimer != null)
					m_DealerTimer.Stop();
				return;
			}
			string message = null;
			if (m_DealerTimer != null)
				m_DealerTimer.Stop();
			m_BJInfo.status = GameStatus.DealerTurn;
			m_BJInfo.HandInfo[0] = EvalHand(m_BJInfo.HandInfo[0]);
			if (CardSounds)
				from.PlaySound(85);
			if (!DealerMustHit(m_BJInfo.HandInfo[0], m_DealerHitsSoft17, out message))
			{
				from.CloseGump(typeof(BlackJackCardGump),1);
				DealerStands(from, message);
				return;
			}
			from.CloseGump(typeof(BlackJackCardGump),1);
			from.SendGump(new BlackJackCardGump(from, this, message));
			Hit(from, false);
			if (m_DealerTimer == null)
				m_DealerTimer = new DealerTimer(this, from, TimeSpan.FromMilliseconds(750));
			m_DealerTimer.Start();
		}

		private static bool DealerMustHit(HandStruct hand, bool DealerHitsSoft17, out string message)
		{
			if (hand.totalcards > 10)
			{
                message = "Le croupier doit se tenir avec onze cartes.";
				return false;
			}
			if (hand.bestscore > 16)
			{
				if (Soft17(hand) && DealerHitsSoft17)
				{
					message = "Le croupier doit frapper le 'soft 17'.";
					return true;
				}
				else
				{
					if (hand.bestscore > 21)
						message = "Le croupier dépasse 21. ";
					else if (hand.bestscore == 21)
						message = "Le croupier arrête à 21.";
					else
						message = "Le croupier arrête.";
					return false;
				}
			}
			message = DealerHitMessage();
			return true;
		}

		private static string DealerHitMessage()
		{
			switch (Utility.Random(6))
			{
				case 0:
					return "Le croupier tire une autre carte.";
				case 1:
                    return "Le croupier tire une autre carte.";
				case 2:
                    return "Le croupier doit tirer une autre carte.";
				case 3:
                    return "Le croupier tire une autre carte.";
				case 4:
                    return "Le croupier tire une autre carte.";
				default:
                    return "Le croupier tire une autre carte.";
			}
		}

		private void DealerStands(Mobile from, string message)
		{
				short dealerscore = m_BJInfo.HandInfo[0].bestscore;
				for (int h = 1; h < m_BJInfo.totalhands; h++)
				{
					if (m_BJInfo.HandInfo[h].status == HandStatus.SplitAces && m_BJInfo.HandInfo[h].bestscore == 21)
					{
						if (m_BJSplitAces21)
							m_BJInfo.HandInfo[h].status = HandStatus.Waiting;
						else
							m_BJInfo.HandInfo[h].status = HandStatus.BlackJack;
					}
					if (m_BJInfo.HandInfo[h].status == HandStatus.Double)
					{
						m_BJInfo.HandInfo[h] = EvalHand(m_BJInfo.HandInfo[h]);
						if (m_BJInfo.HandInfo[h].bestscore > 21)
							m_BJInfo.HandInfo[h].status = HandStatus.Bust;
					}
					if (dealerscore > 21)
					{
						if (m_BJInfo.HandInfo[h].status != HandStatus.Bust && m_BJInfo.HandInfo[h].status != HandStatus.BlackJack)
						{
							m_BJInfo.HandInfo[h].status = HandStatus.Win;
							Credit(m_BJInfo.HandInfo[h].bet * 2,
								m_BJInfo.HandInfo[h].bet,
								m_BJInfo.HandInfo[h].bet);
						}
					}
					else if (m_BJInfo.HandInfo[h].status == HandStatus.Bust || m_BJInfo.HandInfo[h].status == HandStatus.BlackJack)
					{
					}
					else if (m_BJInfo.HandInfo[h].bestscore < dealerscore)
					{
						m_BJInfo.HandInfo[h].status = HandStatus.Lose;
						RecordCollected(m_BJInfo.HandInfo[h].bet);
					}
					else if (m_BJInfo.HandInfo[h].bestscore == dealerscore)
					{
						if (DealerTakesPush)
						{
							m_BJInfo.HandInfo[h].status = HandStatus.Lose;
							RecordCollected(m_BJInfo.HandInfo[h].bet);
						}
						else
						{
							m_BJInfo.HandInfo[h].status = HandStatus.Push;
							m_OnCredit += (int)(m_BJInfo.HandInfo[h].bet);
						}
					}
					else
					{
						m_BJInfo.HandInfo[h].status = HandStatus.Win;
						Credit(m_BJInfo.HandInfo[h].bet * 2, m_BJInfo.HandInfo[h].bet, 
							m_BJInfo.HandInfo[h].bet);
					}
				}
				m_BJInfo.status = GameStatus.Waiting;
				from.SendGump(new BlackJackCardGump(from, this, message));
		}

		private static bool Soft17(HandStruct hand)
		{
			if (hand.totalcards > 2 || hand.bestscore != 17)
				return false;
			if (hand.card[0] % 13 == 0 || hand.card[1] % 13 == 0)
				return true;
			return false;
		}

		private static double BlackJackMultiplier(BlackJackPays bjPays, bool WonAmount)
		{
			switch(bjPays)
			{
				case BlackJackPays.EvenMoney:
					return (WonAmount ? 1.00 : 2.00);
					case BlackJackPays.Six2Five:
						return (WonAmount ? 1.20 : 2.20);
				default:
					return (WonAmount ? 1.50 : 2.50);
			}
		}

		public void IncBet()
		{
			m_Bet++;
			if ( m_Bet > ((int)m_MaxBet))
				m_Bet = (int)m_MinBet;
			m_CurrentBet = m_BetTable[m_Bet];
		}

		public void DecBet()
		{
			m_Bet--;
			if (m_Bet < (int)m_MinBet)
				m_Bet = (int)m_MaxBet;
			m_CurrentBet = m_BetTable[m_Bet];
		}

		private void RecordCollected(int amount)
		{
			RecordCollected(amount, true);
		}
		private void RecordCollected(int amount, bool outEscrow)
		{
			m_TotalCollected += (ulong)amount;
			if (outEscrow)
				m_Escrow -= amount;
		}

		private void Debit(int amount)
		{
			Debit(amount, false);
		}
		private void Debit(int amount, bool inEscrow)
		{
			m_OnCredit -= amount;
			if (inEscrow)
				m_Escrow += amount;
		}

		private void Credit(int amount)
		{
			Credit(amount, 0, 0);
		}
		private void Credit(int amount, int bet, int won)
		{
			m_OnCredit += amount;
			if (bet != 0)
				m_Escrow -= bet;
			if (won != 0)
			{
				m_TotalWon += (ulong)won;
				m_Won += won;
			}
		}


		public int OnCredit()
		{
			return m_OnCredit;
		}

		public int OnCredit(Mobile from, int amount)
		{
			if (from == null || from.Deleted || amount < 0)
			{
                this.PublicOverheadMessage(0, (this.Hue == 907 ? 0 : this.Hue), false, "Ce jeu a besoin d'entretien.");
                SecurityCamera(0, "Ce jeu a besoin d'entretien.");
				Active = false;
				return m_OnCredit;
			}
			m_OnCredit += amount;
			return m_OnCredit;
		}

		private void SecurityCamera(int chatter, string text)
		{
			if (m_SecurityCamMobile == null || m_SecurityCamMobile.Deleted)
				return;
			if (chatter > (int)m_SecurityChatter)
				return;
			if (m_SecurityCamMobile.Player)
				m_SecurityCamMobile.SendMessage(text);
			else
				m_SecurityCamMobile.PublicOverheadMessage(0, (this.Hue == 907 ? 0 : this.Hue), false, text);
		}

		private static void DoBlackJackSound(Mobile from, CEOBlackJack ceobj)
		{
			ceobj.PublicOverheadMessage(0, (ceobj.Hue == 907 ? 0 : ceobj.Hue), false, "Blackjack!");
			if (Utility.RandomDouble() < .005)
				//DoFireworks(from);
				switch (Utility.Random(7))
				{
					case 0:
						from.PlaySound(from.Female ? 794 : 1066);
						break;
					case 1:
						from.PlaySound(from.Female ? 797 : 1069);
						break;
					case 2:
						from.PlaySound(from.Female ? 783 : 1054);
						break;
					case 3:
						from.PlaySound(from.Female ? 823 : 1097);
						break;
					default:
						break;
				}
		}

		private static void DoLosingSound(Mobile from)
		{
			if (Utility.RandomDouble() > .10)
				return;
			if (from.Female)
				from.PlaySound(Utility.RandomList(1372, 1373, 816, 796, 782));
			else
				from.PlaySound(Utility.RandomList(1372, 1373, 1090, 1068, 1053));
		}

		//private static void DoFireworks(Mobile m)
		//{
		//	FireworksWand fwand = new FireworksWand();

		//	if (fwand != null && !fwand.Deleted)
		//	{
		//		try
		//		{
		//			fwand.Parent = m;
		//			fwand.BeginLaunch(m, true);
		//			fwand.Delete();
		//		}
		//		catch { }
		//	}
		//}

		private class DealerTimer : Timer
		{
			private Mobile m;
			private CEOBlackJack m_CEObj;

			public DealerTimer(CEOBlackJack CEObj,Mobile from, TimeSpan delay)
				: base(delay)
			{
				Priority = TimerPriority.TwoFiftyMS;
				m = from;
				m_CEObj = CEObj;
			}

			protected override void OnTick()
			{
				if (m.Map != Map.Internal)
				{
					NetState ns = m.NetState;
					if (ns != null)
					{
						m_CEObj.DealerChecksCards(m);
					}
				}
			}
		}

		public static string HelpText()
		{
			const int TC = 0x000000;
			const int HC = 0xDD0000;
            return Color("Objectif: ", HC, false) +
                Color("La prémisse de base du Blackjack est que vous voulez avoir une valeur de la main qui est plus proche de 21 que celle du croupier, sans dépasser 21. Votre main est strictement jouée contre la main du croupier. Les règles du jeu du croupier sont strictement dictées, lui laissant aucune décision. Si vous jouez à un 'jeu de chaussures' (supérieures à 2 paquets de carte), les cartes de joueurs sont habituellement distribuées face vers le haut. ", TC, false) +
                "<p>" + Color("Les valeurs des cartes: ", HC, false) +
                Color("Dans le Blackjack, les cartes sont évaluées comme suit:", TC, false) +
                "<p>" + Color("Un As peut compter comme 1 ou 11, comme le montre ci-dessous.", TC, false) +
                "<P>" + Color("Les cartes de 2 à 9 sont évalués comme indiqué.", TC, false) +
                "<P>" + Color("Le 10, Valet, Dame et Roi sont tous évalués à 10.", TC, false) +
                "<P>" + Color("Les costumes des cartes n'ont pas de signification dans le jeu, ce n'est qu'une décoration. La valeur d'une main est tout simplement la somme des points de chaque carte dans la main. Par exemple, une main contenant (5,7,9) a la valeur de 21. L'As peut être compté comme 1 ou 11. Vous ne devez pas spécifier quelle valeur a l'As, par contre il aura toujours la valeur qui fait la meilleure main. Par exemple: Supposons que vous ayez le début la main (As, 6). Cette main peut être soit 7 ou 17. Si vous vous arrêtez là, ce sera 17. Supposons que vous tirez une autre carte à la main et ont maintenant (Ace, 6, 3). Votre main totale est maintenant de 20, en comptant l'As comme 11. Supposons que vous avez tiré une troisième carte qui était un 8. La main est maintenant (Ace, 6, 8) qui totalise 15. Notez que maintenant l'As doit être compté comme 1 seul pour éviter d'aller plus de 21.", TC, false) +
                "<P>" + Color("Une main qui contient un As est appelé un total 'soft' si l'As peut être compté comme 1 ou 11 sans que le total dépasser 21. Par exemple (As, 6) est un 'soft' 17. La description provient de la fait que le joueur peut toujours tirer une autre carte à un total 'soft' sans danger de dépasser 21. la main (As, 6,10) d'autre part est un 'hard' 17, puisque maintenant l'As doit être compté comme 1 seul, encore une fois parce le compter comme 11 ferait passer la main sur 21.", TC, false) +
				"<P><P>" + Color("Si jamais vous avez du mal à comprendre, sachez que sur Google, vous pourrez trouver les règles du Blackjack", HC, false) +
				"</p><p>" + Color(Center("Copyright par ceo@easyuo.com"), 0x696969, false) +
                "</p></BASEFONT>";
		}

		private static string Center(string text)
		{
			return String.Format("<CENTER>{0}</CENTER>", text);
		}

		private static string Color(string text, int color)
		{
			return Color(text, color, true);
		}

		private static string Color(string text, int color, bool usetag)
		{
			if (usetag)
				return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
			return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</COLOR>", color, text);
		}
	}
}

namespace Server.Gumps
{
	public class BlackJackCardGump : Gump
	{
		private CEOBlackJack m_CEOBlackJack;
		private int[,] m_Card = new int[4, 12];
		private int m_xSize;
		private int m_ySize;
		private bool m_HelpGump;
		private int cardsizex = 45;
		private int cardsizey = 60;
		private int m_Base;
		private int buttonx = 50;
		private string[] CardFace = new string[] {"A", "2", "3", "4", "5", "6" , "7",
                "8", "9", "10", "J", "Q", "K", "A"};
		private string[] suit_t = new string[] { "\u2660", "\u25C6", "\u2663", "\u2665" };
		private int[] color_t = new int[] { 0, 36, 0, 36 };

		public BlackJackCardGump(Mobile player, CEOBlackJack CEOBlackJack, string message)
			: base(20, 20)
		{
			if (CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.DealerTurn)
				Closable = false;
			else
				Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;
			m_CEOBlackJack = CEOBlackJack;
			m_ySize = (m_CEOBlackJack.m_BJInfo.totalhands == 0) ? 360 + 60 : 115 + 60 + (m_CEOBlackJack.m_BJInfo.totalhands * 80);
			int m_yButtonStart = m_ySize - 75;
			if (m_CEOBlackJack.m_BJInfo.largesthand < 6)
				m_xSize = m_CEOBlackJack.m_BJInfo.totalhands == 0 ? 525 : 470;
			else
				m_xSize = 125 + (m_CEOBlackJack.m_BJInfo.largesthand * 55);
			m_Base = Utility.Random(500);
			m_HelpGump = m_CEOBlackJack.HelpGump;
			m_Base = Utility.Random(2000);

			AddBackground(0, 0, m_xSize, m_ySize, 9260);

			if (m_HelpGump)
                AddBackground(m_xSize, 0, 280, m_ySize, 9260);
			if (m_CEOBlackJack.TestMode)
				AddLabel(3, 2, 37, "Jouer gratuitement");

			AddLabel(m_xSize / 2 - 40, 10, m_CEOBlackJack.Hue, "Table de Blackjack");
			if (m_CEOBlackJack.m_BJInfo.totalhands == 0)
				DisplayRuleInfo();
			else
				DisplayCards();
			if (m_CEOBlackJack.m_BJInfo.askInsurance)
			{
				AddLabel(buttonx + 170, m_yButtonStart - 70, 97, @"Assurance ?");
				AddButton(buttonx + 160, m_yButtonStart - 45, 4023, 4025, m_Base + 250, GumpButtonType.Reply, 0); //OK
				AddButton(buttonx + 210, m_yButtonStart - 45, 4017, 4019, m_Base + 251, GumpButtonType.Reply, 0); //Cancel

			}
			else if (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.PlayerTurn)
			{
				AddButton(buttonx, m_yButtonStart, 4002, 4004, m_Base + 201, GumpButtonType.Reply, 0); //Stand
				AddLabel(buttonx + 30, m_yButtonStart, 1149, @"Reste");
				if (m_CEOBlackJack.m_BJInfo.hitOn)
				{
					AddButton(buttonx + 90, m_yButtonStart, 4026, 4028, m_Base + 200, GumpButtonType.Reply, 0); //Hit
					AddLabel(buttonx + 120, m_yButtonStart, 1149, @"Continuer");
				}
				if (m_CEOBlackJack.m_BJInfo.doubleOn)
				{
					AddButton(buttonx + 180, m_yButtonStart, 4008, 4010, m_Base + 202, GumpButtonType.Reply, 0); //Double
					AddLabel(buttonx + 210, m_yButtonStart, 1149, @"Doubler");
				}
				if (m_CEOBlackJack.m_BJInfo.splitOn)
				{
					AddButton(buttonx + 270, m_yButtonStart, 4020, 4022, m_Base + 203, GumpButtonType.Reply, 0); //Split
					AddLabel(buttonx + 300, m_yButtonStart, 1149, @"Séparer");
				}
			}
			else if (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.Waiting)
			{
				AddButton(95, 19, 0x983, 0x984, m_Base + 101, GumpButtonType.Reply, 0);
                AddButton(95, 31, 0x985, 0x986, m_Base + 102, GumpButtonType.Reply, 0);
				AddButton(buttonx - 35, m_yButtonStart, 4020, 4021, m_Base + 300, GumpButtonType.Reply, 0); //PLAY
				AddLabel(buttonx, m_yButtonStart, 1149, @"Jouer");
				AddButton(buttonx + 55, m_yButtonStart, 4029, 4030, m_Base + 301, GumpButtonType.Reply, 0); //CASHOUT
				if (m_CEOBlackJack.TestMode)
					AddLabel(buttonx + 85, m_yButtonStart, 1149, @"Quitter");
				else
				{
					if (m_CEOBlackJack.OnCredit() == 0)
						AddLabel(buttonx + 90, m_yButtonStart, 1149, @"Quitter");
					else
						AddLabel(buttonx + 90, m_yButtonStart, 1149, @"Encaisser");
					AddButton(buttonx + 150, m_yButtonStart - 10, 4037, 4036, m_Base + 302, GumpButtonType.Reply, 0); //ATM
					AddLabel(buttonx + 190, m_yButtonStart, 1149, @"Ajouter pièces d'or");
				}
			}
			string score = null;
			int labelcolor;
			for (int h = 0; h < m_CEOBlackJack.m_BJInfo.totalhands; h++)
			{
				if (m_CEOBlackJack.m_BJInfo.activehand != h || m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.Waiting)
					labelcolor = 912;
				else
					labelcolor = 57;
				AddLabel(15, 40 + h * 80, labelcolor, h == 0 ? "Croupier" : (m_CEOBlackJack.m_BJInfo.totalhands > 2 ? "Main " + h.ToString() : "Vous"));
				if (h == 0)
				{
					if (m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.BlackJack)
					{
						AddLabel(15, 55 + h * 80, labelcolor, "21");
						AddLabel(15, 75 + h * 80, 2213, "Blackjack!");
					}
					else if (m_CEOBlackJack.DealerCardsFaceUp)
					{
						score = String.Format("{0} {1}", m_CEOBlackJack.m_BJInfo.HandInfo[h].bestscore, (m_CEOBlackJack.m_BJInfo.HandInfo[h].altscore == 0 || (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.Waiting) || (m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.Waiting)) ? null : "or " + m_CEOBlackJack.m_BJInfo.HandInfo[h].altscore.ToString());
						AddLabel(15, 55 + h * 80, labelcolor, score);
					}
					else if (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.PlayerTurn)
					{
						short c = (short)(m_CEOBlackJack.m_BJInfo.HandInfo[0].card[1] % 13);
						if (c == 0)
							c = 11;
						else
							c = (short)(c > 8 ? 10 : c + 1);
						AddLabel(15, 55 + h * 80, labelcolor, c.ToString());
					}
					else
					{
						score = String.Format("{0} {1}", m_CEOBlackJack.m_BJInfo.HandInfo[h].bestscore, (m_CEOBlackJack.m_BJInfo.HandInfo[h].altscore == 0 || (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.Waiting) || (m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.Waiting)) ? null : "or " + m_CEOBlackJack.m_BJInfo.HandInfo[h].altscore.ToString());
						AddLabel(15, 55 + h * 80, labelcolor, score);
					}

				}
				else
				{
					if (m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.BlackJack)
					{
						AddLabel(15, 55 + h * 80, labelcolor, "21");
						AddLabel(15, 75 + h * 80, 2213, "Blackjack!");
					}
					else if (m_CEOBlackJack.PlayerCardsFaceUp)
					{
						score = String.Format("{0} {1}", m_CEOBlackJack.m_BJInfo.HandInfo[h].bestscore, (m_CEOBlackJack.m_BJInfo.HandInfo[h].altscore == 0 || (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.Waiting) || (m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.Waiting)) ? null : "ou " + m_CEOBlackJack.m_BJInfo.HandInfo[h].altscore.ToString());
						AddLabel(15, 55 + h * 80, labelcolor, score);
						if (m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.Bust)
							AddLabel(15, 75 + h * 80, 37, "Dépassé");
					}
					else if (m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.SplitAces)
					{
						AddLabel(15, 55 + h * 80, labelcolor, "11 ou 1");
						AddLabel(15, 75 + h * 80, 87, "Bonne chance!");
					}
					else if (h > 0 && m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.Double)
					{
						AddLabel(15, 75 + h * 80, 17, "Bonne chance!");
						score = String.Format("{0} {1}", m_CEOBlackJack.m_BJInfo.HandInfo[h].bestscore, (m_CEOBlackJack.m_BJInfo.HandInfo[h].altscore == 0 || (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.Waiting) || (m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.Waiting)) ? null : "ou " + m_CEOBlackJack.m_BJInfo.HandInfo[h].altscore.ToString());
						AddLabel(15, 55 + h * 80, labelcolor, score);
					}
					else
					{
						score = String.Format("{0} {1}", m_CEOBlackJack.m_BJInfo.HandInfo[h].bestscore, (m_CEOBlackJack.m_BJInfo.HandInfo[h].altscore == 0 || (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.Waiting) || (m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.Waiting)) ? null : "ou " + m_CEOBlackJack.m_BJInfo.HandInfo[h].altscore.ToString());
						AddLabel(15, 55 + h * 80, labelcolor, score);
					}
					AddLabel(70, 27 + h * 80, labelcolor, String.Format("Parier: {0}", m_CEOBlackJack.m_BJInfo.HandInfo[h].bet));
				}
				if (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.Waiting)
				{
					switch (m_CEOBlackJack.m_BJInfo.HandInfo[h].status)
					{
						case CEOBlackJack.HandStatus.BlackJack:
							{
							}
							break;
						case CEOBlackJack.HandStatus.Lose:
							{
								AddLabel(15, 75 + h * 80, 37, "Perdu");
							}
							break;
						case CEOBlackJack.HandStatus.Bust:
							{
								AddLabel(15, 75 + h * 80, 37, "Dépassé");
							}
							break;
						case CEOBlackJack.HandStatus.Push:
							{
								AddLabel(15, 75 + h * 80, 48, "Push");
							}
							break;
						case CEOBlackJack.HandStatus.Win:
							{
								AddLabel(15, 75 + h * 80, 162, "Gagné!");
							}
							break;
						default:
							{
							}
							break;
					}
				}
			}

            int y = 20;
            int line = 0;
            int space = 20;

            AddLabel(15, y + space * line, 0, "Pari courant:");
            AddLabel(110, y + space * line++, 2213, m_CEOBlackJack.CurrentBet.ToString());

			if (player.AccessLevel >= AccessLevel.GameMaster)
			{
				int paybackhue = (m_CEOBlackJack.WinningPercentage > 99.0) ? 37 : 66;
				AddLabel(m_xSize - 220, 40, 1152, "Pourcentage de paiement:");
				string text = String.Format("{0:##0.00%}", m_CEOBlackJack.WinningPercentage / 100);
                AddLabel(m_xSize - 62, 40, paybackhue, text);
			}
			if (message != null)
				AddLabel(15, m_ySize - 35, 1150, message);
			if (Utility.RandomDouble() < .0008)
				CEOCookie(m_CEOBlackJack.Hue, player);
            if (m_CEOBlackJack.m_BJInfo.status != CEOBlackJack.GameStatus.DealerTurn)
            {
                AddButton(m_xSize - 75, m_yButtonStart, m_HelpGump ? 4014 : 4005, m_HelpGump ? 4016 : 4007, m_Base + 401, GumpButtonType.Reply, 0); //Help
                AddLabel(m_xSize - 40, m_yButtonStart, 1152, "Aide");
            }
            AddLabel(15, m_yButtonStart + 25, 0, "Crédits:");
            AddLabel(70, m_yButtonStart + 25, 2213, m_CEOBlackJack.OnCredit().ToString());
            AddLabel(120, m_yButtonStart + 25, 0, "Dernière victoire:");
            AddLabel(245, m_yButtonStart + 25, 2213, CEOBlackJack.Won.ToString());
            if (m_HelpGump)
				DisplayHelpGump();
		}

		private void DisplayRuleInfo()
		{
            int y = 40;
            int line = 0;
            int space = 20;

			string text;
            AddLabel(15, y + space * line, 0, "Pari Min/Max:");
			text = string.Format("{0}/{1}", m_CEOBlackJack.m_BetTable[(int)m_CEOBlackJack.MinBet], m_CEOBlackJack.m_BetTable[(int)m_CEOBlackJack.MaxBet]);
            AddLabel(110, y + space * line++, 2212, text);
            AddHtml(40, y + space * line++, m_xSize - 80, 25, Center(string.Format("Ensemble de règles: {0}", m_CEOBlackJack.CasinoName)), false, false);
            if (m_CEOBlackJack.ContinuousShuffle)
                DisplayRule(1, y + space * line, "(Aléatoire continu)", null);
            DisplayRule(y + space * line++, "Nombre de paquets:", m_CEOBlackJack.NumberOfDecks.ToString());
            DisplayRule(y + space * line++, "Cartes de joueur face vers le haut:", YesNo(m_CEOBlackJack.PlayerCardsFaceUp));
            DisplayRule(y + space * line++, "Cartes du croupier face vers le haut:", YesNo(m_CEOBlackJack.DealerCardsFaceUp));
            DisplayRule(y + space * line++, "Croupier gagne sur un push:", YesNo(m_CEOBlackJack.DealerTakesPush));
            DisplayRule(y + space * line++, "Croupier prend une carte sur un 'soft' 17:", YesNo(m_CEOBlackJack.DealerHitsSoft17));
            DisplayRule(y + space * line++, "Croupier offre l'assurance:", YesNo(m_CEOBlackJack.OfferInsurance));

			text = (m_CEOBlackJack.Resplits) ? "Jusqu'à 3 fois (4 mains)" : "1 fois";
            DisplayRule(y + space * line++, "Séparer les paires:", YesNo(m_CEOBlackJack.DealerHitsSoft17));

			if (m_CEOBlackJack.SplitAcesRule == CEOBlackJack.SplitAces.No)
				text = "Non";
			else if (m_CEOBlackJack.SplitAcesRule == CEOBlackJack.SplitAces.Once)
				text = "1 fois";
			else
				text = "Sans limite";
            DisplayRule(y + space * line++, "Séparer les As:", text);
            DisplayRule(y + space * line++, "Séparer les As Blackjack compte comme 21: ", YesNo(m_CEOBlackJack.BJSplitAces21));
            DisplayRule(y + space * line++, "Sinon, séparer les As paie: ", (m_CEOBlackJack.BJSplitAPaysEven ? "Même argent" : "Même que les autres Blackjacks"));
			if (m_CEOBlackJack.DoubleRule == CEOBlackJack.DoubleDown.AnyPair)
				text = "Tous les deux premières cartes";
			else if (m_CEOBlackJack.DoubleRule == CEOBlackJack.DoubleDown.Nine211)
				text = "9 - 11";
			else if (m_CEOBlackJack.DoubleRule == CEOBlackJack.DoubleDown.Ten11Only)
				text = "10 et 11 seulement";
			else
				text = "11 seulement";
            DisplayRule(y + space * line++, "Doubler sur:", text);
            DisplayRule(y + space * line++, "Doubler après avoir séparé:", YesNo(m_CEOBlackJack.DoubleAfterSplit));

			if (m_CEOBlackJack.BJMultiplier == CEOBlackJack.BlackJackPays.EvenMoney)
				text = "1:1";
			else if (m_CEOBlackJack.BJMultiplier == CEOBlackJack.BlackJackPays.Three2Two)
				text = "3:2";
			else
				text = "6:5";
            DisplayRule(y + space * line++, "Blackjack paye", text);
		}

		private static string YesNo(bool value)
		{
			return value ? "Oui" : "Non";
		}

		private void DisplayRule(int y, string header, string rule)
		{
			DisplayRule(0, y, header, rule);
		}

		private void DisplayRule(int col, int y, string header, string rule)
		{
			AddLabel(col == 0 ? 15 : 330, y, rule == null ? 1145 : m_CEOBlackJack.Hue - 1, header);
			if (rule != null)
				AddLabel(col == 0 ? 300 : 600, y, 1149, rule);
		}

		private void DisplayCards()
		{
			int cardbasex = 100;
			int cardbasey = 45;
			for (int h = 0; h < m_CEOBlackJack.m_BJInfo.totalhands; h++)
			{
				for (int c = 0; c < m_CEOBlackJack.m_BJInfo.HandInfo[h].totalcards; c++)
				{
					if (m_CEOBlackJack.m_BJInfo.HandInfo[h].card[c] < 0)
						break;
					else if (h > 0 && m_CEOBlackJack.PlayerCardsFaceUp)
						DrawCard(cardbasex + (c * 55), cardbasey + h * 80, m_CEOBlackJack.m_BJInfo.HandInfo[h].card[c]);
					else if (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.Waiting)
						DrawCard(cardbasex + (c * 55), cardbasey + h * 80, m_CEOBlackJack.m_BJInfo.HandInfo[h].card[c]);
					else if (h > 0 && c == 2 && m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.Double)
						DrawBlankCard(cardbasex + (c * 55), cardbasey + h * 80);
					else if (h > 0 && c == 1 && m_CEOBlackJack.m_BJInfo.HandInfo[h].status == CEOBlackJack.HandStatus.SplitAces)
						DrawBlankCard(cardbasex + (c * 55), cardbasey + h * 80);
					else if (h == 0 && c == 0 && m_CEOBlackJack.DealerCardsFaceUp)
						DrawCard(cardbasex + (c * 55), cardbasey + h * 80, m_CEOBlackJack.m_BJInfo.HandInfo[h].card[c]);
					else if (h == 0 && c == 0 && (m_CEOBlackJack.m_BJInfo.status != CEOBlackJack.GameStatus.DealerTurn))
						DrawBlankCard(cardbasex + (c * 55), cardbasey + h * 80);
					else
						DrawCard(cardbasex + (c * 55), cardbasey + h * 80, m_CEOBlackJack.m_BJInfo.HandInfo[h].card[c]);
				}
			}
		}

		private void DisplayHelpGump()
		{
			AddHtml(m_xSize + 15, 15, 250, m_ySize - 30, CEOBlackJack.HelpText(), true, true);
		}

		private string Center(string text)
		{
			return String.Format("<CENTER>{0}</CENTER>", text);
		}

		private string Color(string text, int color)
		{
			return Color(text, color, true);
		}

		private string Color(string text, int color, bool usetag)
		{
			if (usetag)
				return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
			return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</COLOR>", color, text);
		}

		private void CEOCookie(int hue, Mobile m)
		{
			AddImage(m_xSize - 90, 100, 990);
			AddLabel(15, m_ySize - 20, hue, "Bonjour, aimez-vous mes cartes de BlackJack ? *Sourit*");
			m.PlaySound(Utility.RandomList(1358, 1359, 1360, 1361, 1362, 1363, 1368, 1382));
		}

		private void DrawBlankCard(int x, int y)
		{
			AddImageTiled(x, y, cardsizex, cardsizey, 2624);
			AddImageTiled(x + 2, y + 2, cardsizex - 4, cardsizey - 4, 9384); // or 9304
			AddItem(x + 10, y + 7, 5367);
		}		

		private void DrawCard(int x, int y, int card)
		{
			int suit_i = card / 13;
			int color = color_t[suit_i];
			AddImageTiled(x, y, cardsizex, cardsizey, 2624);
			AddImageTiled(x + 2, y + 2, cardsizex - 4, cardsizey - 4, 0xBBC);
			AddLabel(x + cardsizex / 2 - 5, y + cardsizey / 2 - 10, color, CardFace[card % 13]);
			AddLabel(x + 3, y + 3, color, suit_t[suit_i]);
			AddLabel(x + cardsizex - 21, y + cardsizey - 20, color, suit_t[suit_i]);
		}

		public override void OnResponse(NetState state, RelayInfo info)
		{
			Mobile from = state.Mobile;
			string message = null;
			if (from == null)
				return;
			else if (info.ButtonID == 0) // CashOut
			{
                Console.WriteLine("Fermé");
				m_CEOBlackJack.RemovePlayer(from);
				return;
			}
            else if (info.ButtonID == 1) // Close by machine
            {
                return;
            }
			else if (info.ButtonID == m_Base + 401) // HelpGump
			{
				m_CEOBlackJack.HelpGump = !m_CEOBlackJack.HelpGump;
				from.SendGump(new BlackJackCardGump(from, m_CEOBlackJack, null));
				return;
			}
			else if (m_CEOBlackJack.m_BJInfo.askInsurance)
			{
				m_CEOBlackJack.ProcessInsurance(from, info.ButtonID - m_Base - 250);
				return;
			}
			else if (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.PlayerTurn)
			{
				if (info.ButtonID == m_Base + 200) // Hit
				{
					if (m_CEOBlackJack.m_BJInfo.hitOn)
					{
						m_CEOBlackJack.Hit(from, true);
						return;
					}
				}
				else if (info.ButtonID == m_Base + 201) // Stand
				{
					m_CEOBlackJack.Stand(from,null);
					return;
				}
				else if (info.ButtonID == m_Base + 202) // Double
				{
					if (m_CEOBlackJack.m_BJInfo.doubleOn)
					{
						m_CEOBlackJack.Double(from);
						return;
					}
				}
				else if (info.ButtonID == m_Base + 203) // Split
				{
					if (m_CEOBlackJack.m_BJInfo.splitOn)
					{
						m_CEOBlackJack.Split(from);
						return;
					}
				}
			}
			else if (m_CEOBlackJack.m_BJInfo.status == CEOBlackJack.GameStatus.Waiting)
			{
				if (info.ButtonID == m_Base + 300) // Play
				{
					m_CEOBlackJack.PlayBlackJack(from);
					return;
				}
				else if (info.ButtonID == m_Base + 301) // CashOut
				{
					m_CEOBlackJack.RemovePlayer(from);
					return;
				}
				else if (info.ButtonID == m_Base + 302) // ATM
				{
					if (m_CEOBlackJack.OnCredit() >= m_CEOBlackJack.CreditATMLimit)
					{
						message = "Cette table est au niveau ou au-dessus de sa limite de crédit.";
					}
					else if (m_CEOBlackJack.TestMode)
					{
						message = "Cette table en mode de test, double-cliquez pour crédits.";
					}
					else
					{
						int amount = (m_CEOBlackJack.CreditATMLimit - m_CEOBlackJack.OnCredit() < m_CEOBlackJack.CreditATMIncrements) ? m_CEOBlackJack.CreditATMLimit - m_CEOBlackJack.OnCredit() : m_CEOBlackJack.CreditATMIncrements;
						if (from.BankBox.ConsumeTotal(typeof(Gold), amount))
						{
							m_CEOBlackJack.OnCredit(from, amount);
							message = string.Format("{0} pièces d'or retirée de votre banque.", amount);
							Effects.PlaySound(new Point3D(m_CEOBlackJack.X, m_CEOBlackJack.Y, m_CEOBlackJack.Z), m_CEOBlackJack.Map, 501);
							//string text = String.Format("{0}:ATM={1}.", from.Name, amount);
							//m_Keno.SecurityCamera(amount > 5000 ? 0 : 1, text);
							//text = String.Format("OnCredit={1}.", from.Name, m_CEOBlackJack.OnCredit());
							//m_Keno.SecurityCamera(m_Player.OnCredit > 10000 ? 1 : 2, text);
						}
						else
							message = "Fonds insuffisants dans votre banque pour retrait";
					}
					from.SendGump(new BlackJackCardGump(from, m_CEOBlackJack, message));
					return;
				}
				else if ((info.ButtonID == m_Base + 101))
				{
					m_CEOBlackJack.IncBet();
				}
				else if ((info.ButtonID == m_Base + 102))
				{
					m_CEOBlackJack.DecBet();
				}
				from.SendGump(new BlackJackCardGump(from, m_CEOBlackJack, null));
			}
		}
	}
}
