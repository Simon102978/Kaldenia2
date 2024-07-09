using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class SBVagabond : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public override IShopSellInfo SellInfo => m_SellInfo;
        public override List<GenericBuyInfo> BuyInfo => m_BuyInfo;

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
				/*
                Add(new GenericBuyInfo(typeof(GoldRing), 27, 20, 0x108A, 0));
                Add(new GenericBuyInfo(typeof(Necklace), 26, 20, 0x1085, 0));
                Add(new GenericBuyInfo(typeof(GoldNecklace), 27, 20, 0x1088, 0));
                Add(new GenericBuyInfo(typeof(GoldBeadNecklace), 27, 20, 0x1089, 0));
                Add(new GenericBuyInfo(typeof(Beads), 27, 20, 0x108B, 0));
                Add(new GenericBuyInfo(typeof(GoldBracelet), 27, 20, 0x1086, 0));
                Add(new GenericBuyInfo(typeof(GoldEarrings), 27, 20, 0x1087, 0));

                Add(new GenericBuyInfo(typeof(Board), 3, 20, 0x1BD7, 0, true));
                Add(new GenericBuyInfo(typeof(IronIngot), 6, 20, 0x1BF2, 0, true));

                Add(new GenericBuyInfo(typeof(SaphirEtoile), 125, 20, 0x0F0F, 0, true));
                Add(new GenericBuyInfo(typeof(Emeraude), 100, 20, 0xF10, 0, true));
                Add(new GenericBuyInfo(typeof(Sapphire), 100, 20, 0xF11, 0, true));
                Add(new GenericBuyInfo(typeof(Rubis), 75, 20, 0xF13, 0, true));
                Add(new GenericBuyInfo(typeof(Citrine), 50, 20, 0xF15, 0, true));
                Add(new GenericBuyInfo(typeof(Amethyste), 100, 20, 0xF16, 0, true));
                Add(new GenericBuyInfo(typeof(Tourmaline), 75, 20, 0x0F18, 0, true));
                Add(new GenericBuyInfo(typeof(Ambre), 50, 20, 0xF25, 0, true));
                Add(new GenericBuyInfo(typeof(Diamant), 200, 20, 0xF26, 0, true));
				*/
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
       /*         Add(typeof(Board), 1);
                Add(typeof(IronIngot), 3);

                Add(typeof(Ambre), 25);
                Add(typeof(Amethyste), 50);
                Add(typeof(Citrine), 25);
                Add(typeof(Diamant), 100);
                Add(typeof(Emeraude), 50);
                Add(typeof(Rubis), 37);
                Add(typeof(Sapphire), 50);
                Add(typeof(SaphirEtoile), 62);
                Add(typeof(Tourmaline), 47);
                Add(typeof(GoldRing), 13);
                Add(typeof(SilverRing), 10);
                Add(typeof(Necklace), 13);
                Add(typeof(GoldNecklace), 13);
                Add(typeof(GoldBeadNecklace), 13);
                Add(typeof(SilverNecklace), 10);
                Add(typeof(SilverBeadNecklace), 10);
                Add(typeof(Beads), 13);
                Add(typeof(GoldBracelet), 13);
                Add(typeof(SilverBracelet), 10);
                Add(typeof(GoldEarrings), 13);
                Add(typeof(SilverEarrings), 10);
	   */
            }
        }
    }
}
