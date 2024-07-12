using System;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class SBJoueur : SBInfo
	{
	
			private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
			private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

			public SBJoueur()
		{
		}

		public override IShopSellInfo SellInfo => m_SellInfo;
		public override List<GenericBuyInfo> BuyInfo => m_BuyInfo;

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
            {
                Add(new GenericBuyInfo(typeof(Dices), 30, Utility.RandomMinMax(5, 15), 0xFA7, 0));
                Add(new GenericBuyInfo("1016450", typeof(Chessboard), 240, Utility.RandomMinMax(5, 15), 0xFA6, 0));
                Add(new GenericBuyInfo("1016449", typeof(CheckerBoard), 240, Utility.RandomMinMax(5, 15), 0xFA6, 0));
                
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
            {
                Add(typeof(Chessboard), 120);
                Add(typeof(CheckerBoard), 120);
		    }
		}
	}
}