using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class SBTinker : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo;
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBTinker(BaseVendor owner)
        {
            m_BuyInfo = new InternalBuyInfo(owner);
        }

        public override IShopSellInfo SellInfo => m_SellInfo;
        public override List<GenericBuyInfo> BuyInfo => m_BuyInfo;

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo(BaseVendor owner)
            {
          



                Add(new GenericBuyInfo("1024111", typeof(Key), 8, 20, 0x100F, 0));
                Add(new GenericBuyInfo("1024112", typeof(Key), 8, 20, 0x1010, 0));
                Add(new GenericBuyInfo("1024115", typeof(Key), 8, 20, 0x1013, 0));
                Add(new GenericBuyInfo(typeof(KeyRing), 8, 20, 0x1010, 0));
                Add(new GenericBuyInfo(typeof(Lockpick), 5, 20, 0x14FC, 0, true));

				
				Add(new GenericBuyInfo(typeof(TinkerTools), 20, 20, 0x1EB8, 0));
                Add(new GenericBuyInfo(typeof(PalmierBoard), 3, 50, 0x1BD7, 0, true));
                Add(new GenericBuyInfo(typeof(IronIngot), 3, 50, 0x1BF2, 0, true));
                Add(new GenericBuyInfo(typeof(SewingKit), 20, 20, 0xF9D, 0));
				Add(new GenericBuyInfo(typeof(LeatherSewingKit), 20, 20, 0xF9D, 0xF9D));
				Add(new GenericBuyInfo(typeof(BoneSewingKit), 20, 20, 0xF9D, 1109));

				//Add(new GenericBuyInfo(typeof(DrawKnife), 10, 20, 0x10E4, 0));
    

                Add(new GenericBuyInfo(typeof(ButcherKnife), 13, 20, 0x13F6, 0));

                Add(new GenericBuyInfo(typeof(Scissors), 11, 20, 0xF9F, 0));

          
                Add(new GenericBuyInfo(typeof(Saw), 20, 20, 0x1034, 0));

               
                Add(new GenericBuyInfo(typeof(SmithHammer), 20, 20, 0x13E3, 0));
                // TODO: Sledgehammer

                Add(new GenericBuyInfo(typeof(Shovel), 30, 20, 0xF39, 0));

              

                Add(new GenericBuyInfo(typeof(Pickaxe), 25, 20, 0xE86, 0));

               
				

			}
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
				
                Add(typeof(Drums), 7);
                Add(typeof(Tambourine), 7);
                Add(typeof(LapHarp), 7);
                Add(typeof(Lute), 7);

                Add(typeof(Shovel), 5);
                Add(typeof(SewingKit), 1);
                Add(typeof(Scissors), 2);
                //Add(typeof(Tongs), 7);
                Add(typeof(Key), 1);

                //Add(typeof(DovetailSaw), 6);
                //Add(typeof(MouldingPlane), 6);
                //Add(typeof(Nails), 1);
                //Add(typeof(JointingPlane), 6);
                //Add(typeof(SmoothingPlane), 6);
                Add(typeof(Saw), 2);

                Add(typeof(Clock), 10);
                Add(typeof(ClockParts), 1);
                Add(typeof(AxleGears), 1);
                Add(typeof(Gears), 1);
                Add(typeof(Hinge), 1);
                Add(typeof(Sextant), 6);
                Add(typeof(SextantParts), 2);
                Add(typeof(Axle), 1);
                Add(typeof(Springs), 1);

                //Add(typeof(DrawKnife), 5);
                //Add(typeof(Froe), 5);
                //Add(typeof(Inshave), 5);
                //Add(typeof(Scorp), 5);

                Add(typeof(Lockpick), 1);
                Add(typeof(TinkerTools), 2);

                ////Add(typeof(PalmierBoard), 1);
				//Add(typeof(PalmierLog), 1);
				Add(typeof(PalmierWoodResourceCrate), 100);
				Add(typeof(IronIngotResourceCrate), 100);
				Add(typeof(RegularLeatherResourceCrate), 100);



				Add(typeof(Pickaxe), 7);
                Add(typeof(Hammer), 3);
                Add(typeof(SmithHammer), 2);
                Add(typeof(ButcherKnife), 3);
				
            }
        }
    }
}