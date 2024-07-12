using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class SBFisherman : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public override IShopSellInfo SellInfo => m_SellInfo;
        public override List<GenericBuyInfo> BuyInfo => m_BuyInfo;

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {

				Add(new GenericBuyInfo("Cage à Homards Vide", typeof(LobsterTrap), 10, 50, 17615, 0));
				Add(new GenericBuyInfo(typeof(LargeFishingPole), 15, 20, 0xDC0, 0));
				Add(new GenericBuyInfo(typeof(BaitFish), 15, 30, 0x4B46, 0));

            }
        }

        public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add(typeof(FishSteakResourceCrate), 100);
				Add(typeof(Fish), 2);
				Add(typeof(AutumnDragonfish), 2);
				Add(typeof(BlueLobster), 2);
				Add(typeof(BullFish), 2);
				Add(typeof(CrystalFish), 2);
				Add(typeof(FairySalmon), 2);
				Add(typeof(FireFish), 2);
				Add(typeof(GiantKoi), 3);
				Add(typeof(GreatBarracuda), 3);
				Add(typeof(HolyMackerel), 3);
				Add(typeof(LavaFish), 3);
				Add(typeof(ReaperFish), 3);
				Add(typeof(SpiderCrab), 3);
				Add(typeof(StoneCrab), 3);
				Add(typeof(SummerDragonfish), 3);
				Add(typeof(UnicornFish), 4);
				Add(typeof(YellowtailBarracuda), 4);
				Add(typeof(AbyssalDragonfish), 4);
				Add(typeof(BlackMarlin), 4);
				Add(typeof(BloodLobster), 4);
				Add(typeof(BlueMarlin), 4);
				Add(typeof(DreadLobster), 4);
				Add(typeof(DungeonPike), 5);
				Add(typeof(GiantSamuraiFish), 5);
				Add(typeof(GoldenTuna), 5);
				Add(typeof(Kingfish), 5);
				Add(typeof(LanternFish), 5);
				Add(typeof(RainbowFish), 5);
				Add(typeof(SeekerFish), 5);
				Add(typeof(SpringDragonfish), 6);
				Add(typeof(StoneFish), 6);
				Add(typeof(TunnelCrab), 6);
				Add(typeof(VoidCrab), 6);
				Add(typeof(VoidLobster), 6);
				Add(typeof(WinterDragonfish), 6);
				Add(typeof(ZombieFish), 7);
				Add(typeof(LargeFishingPole), 7);
				Add(typeof(LobsterTrap), 1);
		

				

				Add(typeof(StoneCrabMeat), 5);
				Add(typeof(SpiderCrabMeat), 5);
				Add(typeof(BlueLobsterMeat), 5);

				/// Ajouts poissons spéciaux ///
				
			
			
			}
		}
	}
}
