using System;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Poker
{
	public class PokerLeaveGump : Gump
	{
		private PokerGame m_Game;

		public PokerLeaveGump( Mobile from, PokerGame game )
			: base( 50, 50 )
		{
			m_Game = game;

			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;
			this.AddPage( 0 );
			this.AddImageTiled( 18, 15, 350, 180, 9274 );
			//this.AddAlphaRegion( 23, 20, 340, 170 );
            this.AddBackground(0, 0, 390, 200, 9270);
			this.AddLabel( 133, 25, 37, @"Quitter la table de poker" );
			this.AddImageTiled( 42, 47, 301, 3, 96 );
			this.AddLabel( 40, 62, 41, @"Vous êtes sur le point de quitter un jeu de poker." );
			this.AddImage( 33, 38, 95, 41 );
			this.AddImage( 342, 38, 97, 41 );
			this.AddLabel( 23, 80, 41, @"Êtes-vous sûr que vous voulez laisser votre or " );
			this.AddLabel( 30, 98, 41, @"et quitter la table? Vous vous couchez et tous les" );
			this.AddLabel( 40, 116, 41, @"paris en cours seront perdus. Les gains seront " );
            this.AddLabel( 60, 136, 41, @"déposés dans votre banque." );
			this.AddButton( 163, 165, 247, 248, (int)Handlers.btnOkay, GumpButtonType.Reply, 0 );
		}

		public enum Handlers
		{
			None,
			btnOkay
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			if ( from == null )
				return;

			PokerPlayer player = m_Game.GetPlayer( from );

			if ( player != null )
			{
				if ( info.ButtonID == 1 )
				{
					if ( m_Game.State == PokerGameState.Inactive )
					{
						if ( m_Game.Players.Contains( player ) )
							m_Game.RemovePlayer( player );
						return;
					}


					if ( player.RequestLeave )
						from.SendMessage( 0x22, "Vous avez déjà soumis une demande de quitter." );
					else
					{
						from.SendMessage( 0x22, "Vous avez présenté une demande de quitter la table." );
						player.RequestLeave = true;
					}
				}
			}
		}
	}
}