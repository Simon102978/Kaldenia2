using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Items;
using Xanthos.ShrinkSystem;

namespace Server.Mobiles
	
{
    public class SBAnimalTrainer : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public override IShopSellInfo SellInfo => m_SellInfo;
        public override List<GenericBuyInfo> BuyInfo => m_BuyInfo;

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                
                Add(new AnimalBuyInfo(1, typeof(Horse), 1000, 10, 204, 0));
				Add(new AnimalBuyInfo(1, typeof(RidableLlama), 1000, 10, 220, 0));
				Add(new AnimalBuyInfo(1, typeof(PackHorse), 800, 10, 291, 0));
                Add(new AnimalBuyInfo(1, typeof(PackLlama), 800, 10, 292, 0));
				
                

				//Add(new AnimalBuyInfo(1, typeof(FarmBull), 500, 10, 233, 0));
				//Add(new AnimalBuyInfo(1, typeof(FarmChicken), 50, 10, 208, 0));
				//Add(new AnimalBuyInfo(1, typeof(FarmCow), 400, 10, 231, 0));
				//Add(new AnimalBuyInfo(1, typeof(FarmGoat), 300, 10, 209, 0));
				//Add(new AnimalBuyInfo(1, typeof(FarmHen), 300, 10, 208, 0));
				//Add(new AnimalBuyInfo(1, typeof(FarmPig), 200, 10, 203, 0));
				//Add(new AnimalBuyInfo(1, typeof(FarmSheep), 100, 10, 207, 0));
	

				//Add(new GenericBuyInfo("2001111", typeof(HitchingPostSouthDeed), 10000, 10, 0x14F0, 0));
				Add(new GenericBuyInfo(typeof(Saddlebag), 500, 20, 0x14F8, 0));
				Add(new GenericBuyInfo(typeof(TamerTransformBrush), 1000, 20, 0x1372, 0));

				Add(new GenericBuyInfo(typeof(TamerColorBucket), 500, 20, 0x2004, 0));

			}
		}

        public class InternalSellInfo : GenericSellInfo
        {
        }
	}
}
