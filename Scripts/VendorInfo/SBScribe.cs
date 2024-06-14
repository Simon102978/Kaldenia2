using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class SBScribe : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo;
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBScribe(Mobile m)
        {
            if (m != null)
            {
                m_BuyInfo = new InternalBuyInfo(m);
            }
        }

        public override IShopSellInfo SellInfo => m_SellInfo;
        public override List<GenericBuyInfo> BuyInfo => m_BuyInfo;

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo(Mobile m)
            {
				Add(new GenericBuyInfo(typeof(Missive), 5, 999, 0x14EE, 0));
				Add(new GenericBuyInfo(typeof(ScribesPen), 10, 20, 0xFBF, 0));
				Add(new GenericBuyInfo(typeof(CarnetAdresse), 15, 20, 0xFF2, 1633));

		//		Add(new GenericBuyInfo(typeof(SpellsPen), 15, 20, 0x1F19, 2079));
		//		Add(new GenericBuyInfo(typeof(SoulsPen), 50, 20, 0x10E7, 2962));

				

				Add(new GenericBuyInfo(typeof(BlankScroll), 20, 999, 0x0E34, 0));
				Add(new GenericBuyInfo(typeof(BrownBook), 15, 10, 0xFEF, 0));
                Add(new GenericBuyInfo(typeof(TanBook), 15, 10, 0xFF0, 0));
                Add(new GenericBuyInfo(typeof(BlueBook), 15, 10, 0xFF2, 0));
        //        Add(new GenericBuyInfo(typeof(LivreVierge), 500, 999, 0xFBE, 0));
            }
        }
    }

    public class InternalSellInfo : GenericSellInfo
    {
        public InternalSellInfo()
        {
            Add(typeof(ScribesPen), 1);
            Add(typeof(BrownBook), 2);
            Add(typeof(TanBook), 2);
			Add(typeof(BlueBook), 2);
            Add(typeof(BlankScroll), 1);
        }
    }
}



