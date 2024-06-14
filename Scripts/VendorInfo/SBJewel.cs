using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class SBJewel : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public override IShopSellInfo SellInfo => m_SellInfo;
        public override List<GenericBuyInfo> BuyInfo => m_BuyInfo;

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
				Add(new GenericBuyInfo(typeof(GoldRing), 250, 20, 0x108A, 0));
				Add(new GenericBuyInfo(typeof(Necklace), 250, 20, 0x1085, 0));
				Add(new GenericBuyInfo(typeof(GoldNecklace), 250, 20, 0x1088, 0));
				Add(new GenericBuyInfo(typeof(GoldBeadNecklace), 250, 20, 0x1089, 0));
				Add(new GenericBuyInfo(typeof(Beads), 250, 20, 0x108B, 0, true));
				Add(new GenericBuyInfo(typeof(GoldBracelet), 250, 20, 0x1086, 0));
				Add(new GenericBuyInfo(typeof(GoldEarrings), 250, 20, 0x1087, 0));
			}
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {

				Add(typeof(Citrine), 5);
				Add(typeof(Amber), 8);
				Add(typeof(Tourmaline), 9);
				Add(typeof(Ruby), 10);
				Add(typeof(Amethyst), 12);
				Add(typeof(Sapphire), 15);
				Add(typeof(StarSapphire), 20);
				Add(typeof(Emerald), 25);
				Add(typeof(Diamond), 35);

				Add(typeof(GoldRing), 7);
                Add(typeof(SilverRing), 5);
                Add(typeof(Necklace), 5);
                Add(typeof(GoldNecklace), 7);
                Add(typeof(GoldBeadNecklace), 7);
                Add(typeof(SilverNecklace), 6);
                Add(typeof(SilverBeadNecklace), 6);
                Add(typeof(Beads), 5);
                Add(typeof(GoldBracelet), 7);
                Add(typeof(SilverBracelet), 6);
                Add(typeof(GoldEarrings), 7);
                Add(typeof(SilverEarrings), 6);
            }
        }
    }
}
