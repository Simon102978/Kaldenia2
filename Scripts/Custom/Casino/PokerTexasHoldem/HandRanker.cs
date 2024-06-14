using System;
using System.Collections.Generic;

namespace Server.Poker
{
	public class HandRanker
	{
		public static string RankString( HandRank rank )
		{
			switch ( rank )
			{
				case HandRank.None:
					return "Carte haute";
				case HandRank.OnePair:
					return "Une paire";
				case HandRank.TwoPairs:
					return "Deux pairs";
				case HandRank.ThreeOfAKind:
					return "Brelan";
				case HandRank.Straight:
                    return "Quinte";
				case HandRank.Flush:
					return "Couleur";
				case HandRank.FullHouse:
					return "Full";
				case HandRank.FourOfAKind:
                    return "Carr�";
				case HandRank.StraightFlush:
                    return "Quinte Flush";
				case HandRank.RoyalFlush:
					return "Quinte Royale";
			}
			return String.Empty;
		}

		public static string RankString( ResultEntry entry )
		{
			switch ( entry.Rank )
			{
				case HandRank.None:
                    return String.Format("Carte haute {0}", entry.BestCards[0].RankString);
				case HandRank.OnePair:
					return String.Format( "Pair de {0}", entry.BestCards[0].RankString );
				case HandRank.TwoPairs:
					return String.Format( "Deux paires de: {0} and {1}", entry.BestCards[0].RankString, entry.BestCards[2].RankString );
				case HandRank.ThreeOfAKind:
					return String.Format( "Brelan de {0}", entry.BestCards[0].RankString );
				case HandRank.Straight:
					return String.Format( "Quinte : {0} � {1}", entry.BestCards[0].RankString, entry.BestCards[4].RankString );
				case HandRank.Flush:
					return String.Format( "Couleur: Carte haute: {0}", entry.BestCards[0].RankString );
				case HandRank.FullHouse:
					return String.Format( "Full: 3 {0} et 2 {1}", entry.BestCards[0].RankString, entry.BestCards[3].RankString );
				case HandRank.FourOfAKind:
					return String.Format( "Carr� de {0}", entry.BestCards[0].RankString );
				case HandRank.StraightFlush:
					return String.Format( "Quinte Flush: {0} � {1}", entry.BestCards[0].Name, entry.BestCards[4].Name );
				case HandRank.RoyalFlush:
					return "Quinte Royale";
			}
			return String.Empty;
		}

		public static bool UsesKicker( HandRank rank )
		{
			if ( rank < HandRank.Straight || rank == HandRank.FourOfAKind )
				return true;

			return false;
		}

		/*public static int GetKicker( List<ResultEntry> entries )
		{
			int startIndex = 0;

			switch ( entries[0].Rank )
			{
				case HandRank.None: startIndex = 1; break;
				case HandRank.OnePair: startIndex = 2; break;
				case HandRank.ThreeOfAKind: startIndex = 3; break;
				case HandRank.FourOfAKind:
				case HandRank.TwoPairs: return 4;
			}

			for ( int i = startIndex; i < 4; ++i )
			{
				foreach ( ResultEntry entry in entries )
					if ( entry.BestCards[i].Rank != entries[0].BestCards[i].Rank )
						startIndex = i;
			}

			return startIndex;
		}*/

		/// <summary>
		/// Returns whether the left Result entry is better than the right
		/// </summary>
		public static RankResult IsBetterThan( ResultEntry left, ResultEntry right )
		{
			if ( left.Rank > right.Rank )
				return RankResult.Mieux;
			if ( left.Rank < right.Rank )
				return RankResult.Pire;

			//Ranks are the same
			if ( left.Rank != HandRank.RoyalFlush )
			{
				for ( int i = 0; i < left.BestCards.Count; ++i )
				{
					if ( left.BestCards[i].Rank > right.BestCards[i].Rank )
						return RankResult.Mieux;
					if ( left.BestCards[i].Rank < right.BestCards[i].Rank )
						return RankResult.Pire;
				}
			}

			return RankResult.Meme;
		}

		public static HandRank GetBestHand( List<Card> sortedCards, out List<Card> bestCards )
		{
			bestCards = new List<Card>();
			if ( HasRoyalFlush( sortedCards, out bestCards ) )
			{
				return HandRank.RoyalFlush;
			}
			else if ( HasStraightFlush( sortedCards, out bestCards ) )
			{
				return HandRank.StraightFlush;
			}
			else if ( Has4OfAKind( sortedCards, out bestCards ) )
			{
				AddHighestCards( 1, sortedCards, bestCards, ref bestCards );
				return HandRank.FourOfAKind;
			}
			else if ( HasFullHouse( sortedCards, out bestCards ) )
			{
				return HandRank.FullHouse;
			}
			else if ( HasFlush( sortedCards, out bestCards ) )
			{
				return HandRank.Flush;
			}
			else if ( HasStraight( sortedCards, out bestCards ) )
			{
				return HandRank.Straight;
			}
			else if ( Has3OfAKind( sortedCards, out bestCards ) )
			{
				AddHighestCards( 2, sortedCards, bestCards, ref bestCards );
				return HandRank.ThreeOfAKind;
			}
			else if ( Has2Pairs( sortedCards, out bestCards ) )
			{
				AddHighestCards( 1, sortedCards, bestCards, ref bestCards );
				return HandRank.TwoPairs;
			}
			else if ( Has1Pair( sortedCards, out bestCards ) )
			{
				AddHighestCards( 3, sortedCards, bestCards, ref bestCards );
				return HandRank.OnePair;
			}
			else
			{
				AddHighestCards( 5, sortedCards, bestCards, ref bestCards );
				return HandRank.None;
			}
		}

		private static void AddHighestCards( int numberOfCardsToAdd, List<Card> sortedSourceCards, List<Card> excludeCards, ref List<Card> targetCards )
		{
			int cardsAdded = 0;

			foreach ( Card card in sortedSourceCards )
			{
				if ( !excludeCards.Contains( card ) )
				{
					++cardsAdded;
					targetCards.Add( card );

					if ( cardsAdded == numberOfCardsToAdd )
						return;
				}
			}
		}

		public static bool HasRoyalFlush( List<Card> sortedCards, out List<Card> royalFlushCards )
		{
			royalFlushCards = new List<Card>();

			if ( sortedCards.Count < 5 )
				return false;

			List<Card> flushCards;

			if ( !HasFlush( sortedCards, out flushCards ) )
				return false;

			//since the cards are sorted,
			//we need only to check the first and fifth card
			//if they are an Ace and 10 respectively
			if ( flushCards[0].Rank == Rank.As && flushCards[4].Rank == Rank.Dix )
			{
				for ( int i = 0; i < 5; ++i )
				{
					royalFlushCards.Add( flushCards[i] );
				}
				return true;
			}
			else
				return false;
		}

		public static bool HasStraightFlush( List<Card> sortedCards, out List<Card> straightFlushCards )
		{
			straightFlushCards = new List<Card>();

			if ( sortedCards.Count < 5 )
				return false;

			List<Card> flushCards;

			if ( !HasFlush( sortedCards, out flushCards ) )
				return false;
			if ( HasStraight( flushCards, out straightFlushCards ) )
				return true;

			return false;
		}

		public static bool Has4OfAKind( List<Card> sortedCards, out List<Card> fourOfAKindCards )
		{
			fourOfAKindCards = new List<Card>();

			if ( sortedCards.Count < 4 )
				return false;

			for ( int i = 0; i < sortedCards.Count - 3; ++i )
			{
				if ( sortedCards[i].Rank == sortedCards[i + 1].Rank && sortedCards[i].Rank == sortedCards[i + 2].Rank && sortedCards[i].Rank == sortedCards[i + 3].Rank )
				{
					fourOfAKindCards.Add( sortedCards[i] );
					fourOfAKindCards.Add( sortedCards[i + 1] );
					fourOfAKindCards.Add( sortedCards[i + 2] );
					fourOfAKindCards.Add( sortedCards[i + 3] );
					return true;
				}
			}
			return false;
		}

		public static bool HasFullHouse( List<Card> sortedCards, out List<Card> fullHouseCards )
		{
			fullHouseCards = new List<Card>();

			if ( sortedCards.Count < 5 )
				return false;

			List<Card> threeCards = new List<Card>();
			//check for 3 of a kind
			if ( !Has3OfAKind( sortedCards, out threeCards ) )
				return false;

			foreach ( Card card in threeCards )
				fullHouseCards.Add( card );

			//check for a pair, excluding the 3 cards
			List<Card> remainingCards = new List<Card>();
			foreach ( Card card in sortedCards )
				if ( !fullHouseCards.Contains( card ) )
					remainingCards.Add( card );

			List<Card> twoCards = new List<Card>();
			if ( Has1Pair( remainingCards, out twoCards ) )
			{
				foreach ( Card card in twoCards )
					fullHouseCards.Add( card );

				return true;
			}

			fullHouseCards.Clear();
			return false;
		}

		public static bool HasFlush( List<Card> sortedCards, out List<Card> flushCards )
		{
			flushCards = new List<Card>();
			int cardCount = sortedCards.Count;

			if ( cardCount < 5 )
				return false;

			int errThreshhold = cardCount - 5;
			int errs;
			foreach ( Suit suit in Enum.GetValues( typeof( Suit ) ) )
			{
				errs = 0;
				flushCards.Clear();

				foreach ( Card card in sortedCards )
				{
					if ( card.Suit == suit )
					{
						flushCards.Add( card );

						if ( flushCards.Count == 5 )
							return true;
					}
					else
					{
						errs++;
						if ( errs > errThreshhold )
							break;
					}
				}
			}
			flushCards.Clear();
			return false;
		}

		public static bool HasStraight( List<Card> sortedCards, out List<Card> straightCards )
		{
			straightCards = new List<Card>();

			if ( sortedCards.Count < 5 )
				return false;

			int sequenceCardCount = 0;
			foreach ( Card card in sortedCards )
			{
				if ( sortedCards.IndexOf( card ) == 0 )
				{
					straightCards.Add( card );
					sequenceCardCount++;
				}
				else
				{
					//current card is in sequence with previous
					if ( straightCards[straightCards.Count - 1].Rank == card.Rank + 1 )
					{
						straightCards.Add( card );
						sequenceCardCount++;
					}
					//current card is the same value as previous, ignore
					else if ( straightCards[straightCards.Count - 1].Rank == card.Rank )
					{
					}
					//current card is not in sequence with previous so reset
					else
					{
						straightCards.Clear();
						straightCards.Add( card );
						sequenceCardCount = 1;
					}
				}
				if ( sequenceCardCount == 5 )
					break;
			}
			if ( sequenceCardCount == 5 )
				return true;

			//special case, if the straight is 5, 4, 3, 2, 
			//check for the ace which should be the first card in sortedCards
			else if ( sequenceCardCount == 4
				&& straightCards[straightCards.Count - 1].Rank == Rank.Deux
				&& sortedCards[0].Rank == Rank.As )
			{
				straightCards.Add( sortedCards[0] );
				return true;
			}
			return false;
		}

		public static bool Has3OfAKind( List<Card> sortedCards, out List<Card> threeOfAKindCards )
		{
			threeOfAKindCards = new List<Card>();

			if ( sortedCards.Count < 3 )
				return false;

			for ( int i = 0; i < sortedCards.Count - 2; ++i )
			{
				if ( sortedCards[i].Rank == sortedCards[i + 1].Rank && sortedCards[i].Rank == sortedCards[i + 2].Rank )
				{
					threeOfAKindCards.Add( sortedCards[i] );
					threeOfAKindCards.Add( sortedCards[i + 1] );
					threeOfAKindCards.Add( sortedCards[i + 2] );
					return true;
				}
			}
			return false;
		}

		public static bool Has2Pairs( List<Card> sortedCards, out List<Card> twoPairsCards )
		{
			twoPairsCards = new List<Card>();

			if ( sortedCards.Count < 4 )
				return false;

			int pairCount = 0;
			for ( int i = 0; i < sortedCards.Count - 1; ++i )
			{
				if ( sortedCards[i].Rank == sortedCards[i + 1].Rank )
				{
					twoPairsCards.Add( sortedCards[i] );
					twoPairsCards.Add( sortedCards[i + 1] );
					++pairCount;
					
					if ( pairCount == 2 )
						return true;

					//skip the next card
					++i;
				}
			}
			return false;
		}

		public static bool Has1Pair( List<Card> sortedCards, out List<Card> onePairCards )
		{
			onePairCards = new List<Card>();

			if ( sortedCards.Count < 2 )
				return false;

			for ( int i = 0; i < sortedCards.Count - 1; ++i )
			{
				if ( sortedCards[i].Rank == sortedCards[i + 1].Rank )
				{
					onePairCards.Add( sortedCards[i] );
					onePairCards.Add( sortedCards[i + 1] );
					return true;
				}
			}
			return false;
		}
	}
}