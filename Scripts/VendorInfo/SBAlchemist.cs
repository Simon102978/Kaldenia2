using Server.Engines.Quests;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class SBAlchemist : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo;
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBAlchemist(Mobile m)
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
    
                Add(new GenericBuyInfo(typeof(MortarPestle), 8, 20, 0xE9B, 0));
				Add(new GenericBuyInfo(typeof(MortarPestlePoisoning), 8, 20, 0xE9B, 0));
				Add(new GenericBuyInfo(typeof(Bottle), 5, 999, 0xF0E, 0));



				Add(new GenericBuyInfo(typeof(BlackPearl), 5, 999, 0xF7A, 0));
				Add(new GenericBuyInfo(typeof(Bloodmoss), 5, 999, 0xF7B, 0));
				Add(new GenericBuyInfo(typeof(Garlic), 5, 999, 0xF84, 0));
				Add(new GenericBuyInfo(typeof(Ginseng), 5, 999, 0xF85, 0));
				Add(new GenericBuyInfo(typeof(MandrakeRoot), 5, 999, 0xF86, 0));
				Add(new GenericBuyInfo(typeof(Nightshade), 5, 999, 0xF88, 0));
				Add(new GenericBuyInfo(typeof(SpidersSilk), 5, 999, 0xF8D, 0));
				Add(new GenericBuyInfo(typeof(SulfurousAsh), 5, 999, 0xF8C, 0));

				Add(new GenericBuyInfo(typeof(BatWing), 5, 999, 0xF78, 0));
				Add(new GenericBuyInfo(typeof(DaemonBlood), 5, 999, 0xF7D, 0));
				Add(new GenericBuyInfo(typeof(PigIron), 5, 999, 0xF8A, 0));
				Add(new GenericBuyInfo(typeof(NoxCrystal), 5, 999, 0xF8E, 0));
				Add(new GenericBuyInfo(typeof(GraveDust), 5, 999, 0xF8F, 0));




			}
		}

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(BlackPearl), 1);
                Add(typeof(Bloodmoss), 1);
                Add(typeof(MandrakeRoot), 1);
                Add(typeof(Garlic), 1);
                Add(typeof(Ginseng), 1);
                Add(typeof(Nightshade), 1);
                Add(typeof(SpidersSilk), 1);
                Add(typeof(SulfurousAsh), 1);
                Add(typeof(Bottle), 1);
                Add(typeof(MortarPestle), 1);
                Add(typeof(HairDye), 1);

                Add(typeof(NightSightPotion), 3);
                Add(typeof(AgilityPotion), 3);
                Add(typeof(StrengthPotion), 3);
                Add(typeof(RefreshPotion), 3);
                Add(typeof(LesserCurePotion), 3);
                Add(typeof(CurePotion), 5);
                Add(typeof(GreaterCurePotion), 5);
                Add(typeof(LesserHealPotion), 5);
                Add(typeof(HealPotion), 5);
                Add(typeof(GreaterHealPotion), 5);
                Add(typeof(LesserPoisonPotion), 5);
                Add(typeof(PoisonPotion), 5);
                Add(typeof(GreaterPoisonPotion), 5);
                Add(typeof(DeadlyPoisonPotion), 7);
                Add(typeof(LesserExplosionPotion), 7);
                Add(typeof(ExplosionPotion), 7);
                Add(typeof(GreaterExplosionPotion), 7);
            }
        }
    }
}