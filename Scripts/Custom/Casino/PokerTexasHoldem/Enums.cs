using System;

namespace Server.Poker
{
	public enum PlayerAction
	{
		None,
		Bet,
		Raise,
		Call,
		Check,
		Fold,
		AllIn
	}

	public enum PokerGameState
	{
		Inactive,
		DealHoleCards,
		PreFlop,
		Flop,
		PreTurn,
		Turn,
		PreRiver,
		River,
		PreShowdown,
		Showdown
	}

	public enum Suit
	{
		Carreau = 1,
		Coeur = 2,
		Trefle = 3,
		Pique = 4
	}

	public enum Rank
	{
		Deux = 2,
		Trois = 3,
		Quatre = 4,
		Cinq = 5,
		Six = 6,
		Sept = 7,
		Huit = 8,
		Neuf = 9,
		Dix = 10,
		Valet = 11,
		Reine = 12,
		Roi = 13,
		As = 14
	}

	public enum HandRank
	{
		None,
		OnePair,
		TwoPairs,
		ThreeOfAKind,
		Straight,
		Flush,
		FullHouse,
		FourOfAKind,
		StraightFlush,
		RoyalFlush
	}

	public enum RankResult
	{
		Mieux,
		Pire,
		Meme
	}
}