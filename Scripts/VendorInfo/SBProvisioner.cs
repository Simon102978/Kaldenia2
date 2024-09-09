using Capt.MiniGames;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class SBProvisioner : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public override IShopSellInfo SellInfo => m_SellInfo;
        public override List<GenericBuyInfo> BuyInfo => m_BuyInfo;

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
				//  Add(new GenericBuyInfo("1060834", typeof(Engines.Plants.PlantBowl), 2, 20, 0x15FD, 0));

				Add(new GenericBuyInfo(typeof(RecycleBag), 50, 20, 0xE76, 0));
				Add(new GenericBuyInfo(typeof(Recycleur), 50, 20, 0x12B3, 0));
				Add(new GenericBuyInfo(typeof(Boline), 50, 20, 0xEC5, 1940));
				Add(new GenericBuyInfo(typeof(BedrollTent), 10000, 20, 0xA57, 0));
			//	Add(new GenericBuyInfo(typeof(GardenDeed), 3000, 20, 0xE88, 1164));


				Add(new GenericBuyInfo(typeof(Arrow), 5, 999, 0xF3F, 0, true));
                Add(new GenericBuyInfo(typeof(Bolt), 5, 999, 0x1BFB, 0, true));

                Add(new GenericBuyInfo(typeof(Backpack), 15, 20, 0x9B2, 0));
                Add(new GenericBuyInfo(typeof(Pouch), 6, 20, 0xE79, 0));
                Add(new GenericBuyInfo(typeof(Bag), 6, 20, 0xE76, 0));

                Add(new GenericBuyInfo(typeof(Candle), 6, 20, 0xA28, 0));
                Add(new GenericBuyInfo(typeof(Torch), 8, 20, 0xF6B, 0));
                Add(new GenericBuyInfo(typeof(Lantern), 2, 20, 0xA25, 0));
                Add(new GenericBuyInfo(typeof(OilFlask), 10, 20, 0x1C18, 0));
				Add(new GenericBuyInfo(typeof(BlackPowder), 10, 200, 0x423A, 1109));

				Add(new GenericBuyInfo(typeof(Lockpick), 12, 20, 0x14FC, 0, true));

                Add(new GenericBuyInfo(typeof(FloppyHat), 7, 20, 0x1713, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(WideBrimHat), 8, 20, 0x1714, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(Cap), 10, 20, 0x1715, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(TallStrawHat), 8, 20, 0x1716, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(StrawHat), 7, 20, 0x1717, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(WizardsHat), 11, 20, 0x1718, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(LeatherCap), 10, 20, 0x1DB9, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(FeatheredHat), 10, 20, 0x171A, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(TricorneHat), 8, 20, 0x171B, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(Bandana), 6, 20, 0x1540, Utility.RandomDyedHue()));
                Add(new GenericBuyInfo(typeof(SkullCap), 7, 20, 0x1544, Utility.RandomDyedHue()));

                Add(new GenericBuyInfo(typeof(BreadLoaf), 6, 10, 0x103B, 0, true));
                Add(new GenericBuyInfo(typeof(LambLeg), 8, 20, 0x160A, 0, true));
                Add(new GenericBuyInfo(typeof(ChickenLeg), 5, 20, 0x1608, 0, true));
                Add(new GenericBuyInfo(typeof(CookedBird), 17, 20, 0x9B7, 0, true));

                Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Ale, 7, 20, 0x99F, 0));
                Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Wine, 7, 20, 0x9C7, 0));
                Add(new BeverageBuyInfo(typeof(BeverageBottle), BeverageType.Liquor, 7, 20, 0x99B, 0));
                Add(new BeverageBuyInfo(typeof(Jug), BeverageType.Cider, 13, 20, 0x9C8, 0));

                Add(new GenericBuyInfo(typeof(Pear), 3, 20, 0x994, 0, true));
                Add(new GenericBuyInfo(typeof(Apple), 3, 20, 0x9D0, 0, true));

                Add(new GenericBuyInfo(typeof(Beeswax), 1, 20, 0x1422, 0, true));

                Add(new GenericBuyInfo(typeof(Garlic), 3, 20, 0xF84, 0));
                Add(new GenericBuyInfo(typeof(Ginseng), 3, 20, 0xF85, 0));

                Add(new GenericBuyInfo(typeof(Bottle), 5, 20, 0xF0E, 0, true));

                Add(new GenericBuyInfo(typeof(RedBook), 15, 20, 0xFF1, 0));
                Add(new GenericBuyInfo(typeof(BlueBook), 15, 20, 0xFF2, 0));
                Add(new GenericBuyInfo(typeof(TanBook), 15, 20, 0xFF0, 0));

                Add(new GenericBuyInfo(typeof(WoodenBox), 14, 20, 0xE7D, 0));
                Add(new GenericBuyInfo(typeof(Key), 2, 20, 0x100E, 0));
                Add(new GenericBuyInfo(typeof(Bedroll), 5, 20, 0xA59, 0));
             //   Add(new GenericBuyInfo(typeof(Kindling), 2, 20, 0xDE1, 0, true));

          ///      Add(new GenericBuyInfo("1041205", typeof(Multis.SmallBoatDeed), 10177, 20, 0x14F2, 0));
                Add(new GenericBuyInfo("1041060", typeof(HairDye), 60, 20, 0xEFF, 0));
                Add(new GenericBuyInfo("1016450", typeof(Chessboard), 2, 20, 0xFA6, 0));
                Add(new GenericBuyInfo("1016449", typeof(CheckerBoard), 2, 20, 0xFA6, 0));
                Add(new GenericBuyInfo(typeof(Backgammon), 2, 20, 0xE1C, 0));
			//	Add(new GenericBuyInfo(typeof(JeuTarot), 2, 20, 0x0E15, 0));
				Add(new GenericBuyInfo(typeof(Engines.Mahjong.MahjongGame), 6, 20, 0xFAA, 0));
                Add(new GenericBuyInfo(typeof(Dices), 20, 20, 0xFA7, 0));
				Add(new GenericBuyInfo(typeof(EnhancedDices), 20, 20, 0xFA7, 0));

				
				Add(new GenericBuyInfo(typeof(YahtzeeDice), 2, 20, 0xFA7, 0));
				Add(new GenericBuyInfo(typeof(SmallBagBall), 3, 20, 0x2256, 0));
                Add(new GenericBuyInfo(typeof(LargeBagBall), 3, 20, 0x2257, 0));
           //    Add(new GenericBuyInfo("1079931", typeof(SalvageBag), 1255, 20, 0xE76, Utility.RandomBlueHue()));
           //     Add(new GenericBuyInfo("1114770", typeof(SkinTingeingTincture), 1255, 20, 0xEFF, 90));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(Arrow), 1);
                Add(typeof(Bolt), 1);
                Add(typeof(Backpack), 1);
                Add(typeof(Pouch), 3);
                Add(typeof(Bag), 3);
                Add(typeof(Candle), 3);
                Add(typeof(Torch), 4);
                Add(typeof(Lantern), 1);
                Add(typeof(Lockpick), 1);
                Add(typeof(FloppyHat), 3);
                Add(typeof(WideBrimHat), 4);
                Add(typeof(Cap), 5);
                Add(typeof(TallStrawHat), 4);
                Add(typeof(StrawHat), 3);
                Add(typeof(WizardsHat), 5);
                Add(typeof(LeatherCap), 5);
                Add(typeof(FeatheredHat), 5);
                Add(typeof(TricorneHat), 4);
                Add(typeof(Bandana), 3);
                Add(typeof(SkullCap), 3);
                Add(typeof(Bottle), 3);
                Add(typeof(RedBook), 7);
                Add(typeof(BlueBook), 7);
                Add(typeof(TanBook), 7);
                Add(typeof(WoodenBox), 7);
         ///       Add(typeof(Kindling), 1);
                Add(typeof(HairDye), 30);
                Add(typeof(Chessboard), 1);
                Add(typeof(CheckerBoard), 1);
                Add(typeof(Backgammon), 1);
                Add(typeof(Dices), 1);
                Add(typeof(Beeswax), 1);


				Add(typeof(Citrine), 5);
				Add(typeof(Ambre), 8);
				Add(typeof(Tourmaline), 9);
				Add(typeof(Rubis), 10);
				Add(typeof(Amethyste), 12);
				Add(typeof(Sapphire), 15);
				Add(typeof(SaphirEtoile), 20);
				Add(typeof(Emeraude), 25);
				Add(typeof(Diamant), 35);


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
