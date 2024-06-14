using System;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Poker;

namespace Server.Poker
{
	public class PokerJoinGump : Gump
	{
		private PokerGame m_Game;

		public PokerJoinGump( Mobile from, PokerGame game )
			: base( 50, 50 )
		{
			m_Game = game;

			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;
			this.AddPage( 0 );
            this.AddBackground(0, 0, 385, 393, 9260); //9270
			//this.AddImageTiled( 18, 15, 350, 320, 9274 );
			//this.AddAlphaRegion( 23, 19, 340, 310 );
            this.AddLabel( 125, 10, 37, @"Texas Hold-em du Casino");
			this.AddLabel( 103, 25, 37, @" Joignez-vous à table de poker" );
			this.AddImageTiled( 42, 47, 301, 3, 96 );
			this.AddLabel( 12, 62, 41, @"Vous êtes sur le point de vous joindre à un jeu de Poker." );
			this.AddImage( 33, 38, 95, 41 );
			this.AddImage( 342, 38, 97, 41 );
            this.AddLabel( 12, 80, 41, @"Tous les paris impliquent vos pièces d'or et aucun rembour-");
			this.AddLabel( 18, 98, 41, @"sement ne sera fait. Si vous vous sentez mal à l'aise" );
			this.AddLabel( 15, 116, 41, @"de perdre l'or ou n'êtes pas pas familier avec les règles" );
			this.AddLabel( 20, 134, 41, @"du Texas Hold'em, nous vous déconseillons de jouer." );

			this.AddLabel( 70, 161, 41, @"Petit Blind:" );
			this.AddLabel( 70, 181, 41, @"Grosse Blind:" );
			this.AddLabel( 70, 201, 41, @"Or Minimum:" );
			this.AddLabel( 70, 221, 41, @"Or Maximum:" );
			this.AddLabel( 70, 241, 41, @"Votre or en banque:" );
			this.AddLabel( 70, 261, 41, @"Cave:" );

			this.AddLabel( 200, 161, 148, m_Game.Dealer.SmallBlind.ToString( "#,###" ) + "po" );
			this.AddLabel( 200, 181, 148, m_Game.Dealer.BigBlind.ToString( "#,###" ) + "po" );
			this.AddLabel( 200, 201, 148, m_Game.Dealer.MinBuyIn.ToString( "#,###" ) + "po" );
			this.AddLabel( 200, 221, 148, m_Game.Dealer.MaxBuyIn.ToString( "#,###" ) + "po" );

			int balance = Banker.GetBalance( from );
			int balancehue = 28;
			int layout = 0;

			if ( balance >= m_Game.Dealer.MinBuyIn )
			{
				balancehue = 266;
				layout = 1;
			}

			this.AddLabel( 200, 241, balancehue, balance.ToString( "#,###" ) + "po" );

			if ( layout == 0 )
			{
				this.AddLabel( 200, 261, 1149, "(Or insuffisant)" );
				this.AddButton( 163, 292, 242, 241, (int)Handlers.btnCancel, GumpButtonType.Reply, 0 );
			}
			else if ( layout == 1 )
			{
				this.AddImageTiled( 200, 261, 80, 19, 0xBBC );
				this.AddAlphaRegion( 200, 261, 80, 19 );
				this.AddTextEntry( 203, 261, 77, 19, 68, (int)Handlers.txtBuyInAmount, m_Game.Dealer.MinBuyIn.ToString() );
				this.AddButton( 123, 292, 247, 248, (int)Handlers.btnOkay, GumpButtonType.Reply, 0 );
				this.AddButton( 200, 292, 242, 241, (int)Handlers.btnCancel, GumpButtonType.Reply, 0 );
			}
		}

		public enum Handlers
		{
			btnOkay = 1,
			btnCancel,
			txtBuyInAmount
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			int buyInAmount = 0;

			if ( info.ButtonID == 1 )
			{
				int balance = Banker.GetBalance( from );
				if ( balance >= m_Game.Dealer.MinBuyIn )
				{
					try
					{
						buyInAmount = Convert.ToInt32( ( info.TextEntries[0] ).Text );
					}
					catch
					{
                        from.SendMessage(0x22, "Utilisez des nombres sans virgule pour entrer le montant de votre cave (ex: 1000)");
						return;
					}

					if ( buyInAmount <= balance && buyInAmount >= m_Game.Dealer.MinBuyIn && buyInAmount <= m_Game.Dealer.MaxBuyIn )
					{
						PokerPlayer player = new PokerPlayer( from );
						player.Gold = buyInAmount;
						m_Game.AddPlayer( player );
					}
					else
						from.SendMessage( 0x22, "Vous ne pouvez pas rejoindre avec cette quantité d'or. Cave minimum: " + Convert.ToString( m_Game.Dealer.MinBuyIn ) +
							", Cave maximum: " + Convert.ToString( m_Game.Dealer.MaxBuyIn ) );
				}
				else
					from.SendMessage( 0x22, "Vous ne pouvez pas rejoindre avec cette quantité d'or. Cave minimum: " + Convert.ToString( m_Game.Dealer.MinBuyIn ) +
						", Cave maximum: " + Convert.ToString( m_Game.Dealer.MaxBuyIn ) );
			}
			else if ( info.ButtonID == 2 )
				return;
		}
	}
}