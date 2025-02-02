using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class SBCarpenter : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public override IShopSellInfo SellInfo => m_SellInfo;
        public override List<GenericBuyInfo> BuyInfo => m_BuyInfo;

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                //Add(new GenericBuyInfo(typeof(Nails), 3, 20, 0x102E, 0));
                //Add(new GenericBuyInfo(typeof(Axle), 2, 20, 0x105B, 0, true));
           //     Add(new GenericBuyInfo(typeof(PalmierBoard), 3, 100, 0x1BD7, 0, true));
                //Add(new GenericBuyInfo(typeof(DrawKnife), 10, 20, 0x10E4, 0));
                //Add(new GenericBuyInfo(typeof(Froe), 10, 20, 0x10E5, 0));
                //Add(new GenericBuyInfo(typeof(Scorp), 10, 20, 0x10E7, 0));
                //Add(new GenericBuyInfo(typeof(Inshave), 10, 20, 0x10E6, 0));
                //Add(new GenericBuyInfo(typeof(DovetailSaw), 12, 20, 0x1028, 0));
                Add(new GenericBuyInfo(typeof(Saw), 15, 20, 0x1034, 0));
				//Add(new GenericBuyInfo(typeof(Hammer), 17, 20, 0x102A, 0));
				//Add(new GenericBuyInfo(typeof(MouldingPlane), 11, 20, 0x102C, 0));
				//Add(new GenericBuyInfo(typeof(SmoothingPlane), 10, 20, 0x1032, 0));
				//Add(new GenericBuyInfo(typeof(JointingPlane), 11, 20, 0x1030, 0));
				Add(new GenericBuyInfo(typeof(Drums), 50, 20, 0xE9C, 0));
				Add(new GenericBuyInfo(typeof(Tambourine), 50, 20, 0xE9D, 0));
				Add(new GenericBuyInfo(typeof(LapHarp), 50, 20, 0xEB2, 0));
				Add(new GenericBuyInfo(typeof(Lute), 50, 20, 0xEB3, 0));
			}
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(WoodenBox), 7);
                Add(typeof(SmallCrate), 5);
                Add(typeof(MediumCrate), 6);
                Add(typeof(LargeCrate), 7);
                Add(typeof(WoodenChest), 7);

                Add(typeof(LargeTable), 5);
                Add(typeof(Nightstand), 3);
                Add(typeof(YewWoodTable), 5);

                Add(typeof(Throne), 12);
                Add(typeof(WoodenThrone), 6);
                Add(typeof(Stool), 6);
                Add(typeof(FootStool), 6);

                Add(typeof(FancyWoodenChairCushion), 5);
                Add(typeof(WoodenChairCushion), 5);
                Add(typeof(WoodenChair), 8);
                Add(typeof(BambooChair), 6);
                Add(typeof(WoodenBench), 6);

                Add(typeof(Saw), 1);
                Add(typeof(Scorp), 1);
                Add(typeof(SmoothingPlane), 2);
                Add(typeof(DrawKnife), 2);
                Add(typeof(Froe), 2);
                Add(typeof(Hammer), 1);
                Add(typeof(Inshave), 6);
                Add(typeof(JointingPlane), 1);
                Add(typeof(MouldingPlane), 1);
                Add(typeof(DovetailSaw), 1);
               // //Add(typeof(PalmierBoard), 2);
                Add(typeof(Axle), 1);

                Add(typeof(Club), 5);

                Add(typeof(Lute), 3);
                Add(typeof(LapHarp), 3);
                Add(typeof(Tambourine), 3);
                Add(typeof(Drums), 3);
				Add(typeof(PipeCourte), 2);
				Add(typeof(PipeLongue), 2);
				Add(typeof(PipeCourbee), 3);

				//Add(typeof(PalmierLog), 1);
				Add(typeof(PalmierWoodResourceCrate), 100);

				////Add(typeof(PalmierBoard), 2);
			}
        }
    }
}
