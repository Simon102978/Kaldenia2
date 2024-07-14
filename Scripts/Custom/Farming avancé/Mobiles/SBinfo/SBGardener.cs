using System;
using System.Collections.Generic;
using Server.Items;
using Server.Items.Crops;

namespace Server.Mobiles
{
	public class SBGardener : SBInfo
	{
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGardener(){}

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo
        {
            get
            {
                return this.m_BuyInfo;
            }
        }

        public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				//this.Add( new GenericBuyInfo( typeof( MilkBucket ), 300, 10, 0x0FFA, 0 ) );
				//this.Add( new GenericBuyInfo( typeof( CheeseForm ), 300, 10, 0x0E78, 0 ) );

					this.Add( new GenericBuyInfo( "Plant Bowl", typeof( Engines.Plants.PlantBowl ), 10, 50, 0x15FD, 0 ) );
					this.Add( new GenericBuyInfo( "Fertile Dirt", typeof( FertileDirt ), 3, 999, 0xF81, 0 ) );
					this.Add( new GenericBuyInfo( "Random Plant Seed", typeof( Engines.Plants.Seed ), 20, 100, 0xDCF, 0 ) );
				//	//	this.Add( new GenericBuyInfo( typeof( GreaterCurePotion ), 45, 20, 0xF07, 0 ) );
				////	this.Add( new GenericBuyInfo( typeof( GreaterPoisonPotion ), 45, 20, 0xF0A, 0 ) );
				////	this.Add( new GenericBuyInfo( typeof( GreaterStrengthPotion ), 45, 20, 0xF09, 0 ) );
				////	this.Add( new GenericBuyInfo( typeof( GreaterHealPotion ), 45, 20, 0xF0C, 0 ) );
				///
				this.Add(new GenericBuyInfo("Baie Tribale", typeof(TribalBerry), 3, 25, 0x9D0, 0));
				this.Add(new GenericBuyInfo("Gingembre frais", typeof(FreshGinger), 3, 20, 0x2BE3, 0));
				this.Add(new GenericBuyInfo("Asparagus Seed", typeof(AsparagusSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Beet Seed", typeof(BeetSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Broccoli Seed", typeof(BroccoliSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Cabbage Seed", typeof(CabbageSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Carrot Seed", typeof(CarrotSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Cauliflower Seed", typeof(CauliflowerSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Celery Seed", typeof(CelerySeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Eggplant Seed", typeof(EggplantSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("GreenBean Seed", typeof(GreenBeanSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Lettuce Seed", typeof(LettuceSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Onion Seed", typeof(OnionSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Peanut Seed", typeof(PeanutSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Peas Seed", typeof(PeasSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Potato Seed", typeof(PotatoSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Radish Seed", typeof(RadishSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("SnowPeas Seed", typeof(SnowPeasSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Soy Seed", typeof(SoySeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Spinach Seed", typeof(SpinachSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Strawberry Seed", typeof(StrawberrySeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("SweetPotato Seed", typeof(SweetPotatoSeed), 3, 20, 0xF27, 0));
                this.Add(new GenericBuyInfo("Turnip Seed", typeof(TurnipSeed), 3, 20, 0xF27, 0));

				this.Add(new GenericBuyInfo("Blackberry Seed", typeof(BlackberrySeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("BlackRaspberry Seed", typeof(BlackRaspberrySeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Blueberry Seed", typeof(BlueberrySeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Cranberry Seed", typeof(CranberrySeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Pineapple Seed", typeof(PineappleSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("RedRaspberry Seed", typeof(RedRaspberrySeed), 3, 20, 0xF27, 0));


				this.Add(new GenericBuyInfo("Red Rose Seed", typeof(RedRoseSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("White Rose Seed", typeof(WhiteRoseSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Black Rose Seed", typeof(BlackRoseSeed), 3, 20, 0xF27, 0));


				this.Add(new GenericBuyInfo("Cotton Seed", typeof(CottonSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Flax Seed", typeof(FlaxSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Hay Seed", typeof(HaySeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Oats Seed", typeof(OatsSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Rice Seed", typeof(RiceSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Sugarcane Seed", typeof(SugarcaneSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Wheat Seed", typeof(WheatSeed), 3, 20, 0xF27, 0));


				this.Add(new GenericBuyInfo("Garlic Seed", typeof(GarlicSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Tan Ginger Seed", typeof(TanGingerSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Ginseng Seed", typeof(GinsengSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mandrake Seed", typeof(MandrakeSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Nightshade Seed", typeof(NightshadeSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Tan Mushroom Seed", typeof(TanMushroomSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Red Mushroom Seed", typeof(RedMushroomSeed), 3, 20, 0xF27, 0));


				this.Add(new GenericBuyInfo("Bitter Hops Seed", typeof(BitterHopsSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Elven Hops Seed", typeof(ElvenHopsSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Snow Hops Seed", typeof(SnowHopsSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Sweet Hops Seed", typeof(SweetHopsSeed), 3, 20, 0xF27, 0));


				this.Add(new GenericBuyInfo("Corn Seed", typeof(CornSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Field Corn Seed", typeof(FieldCornSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Sun Flower Seed", typeof(SunFlowerSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Tea Seed", typeof(TeaSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("VanillaSeed", typeof(VanillaSeed), 3, 20, 0xF27, 0));


			//	this.Add(new GenericBuyInfo("Pommier", typeof(AppleSapling), 3, 20, 0xF27, 0));
			//	this.Add(new GenericBuyInfo("Pêcher", typeof(PeachSapling), 3, 20, 0xF27, 0));
			//	this.Add(new GenericBuyInfo("Poirier", typeof(PearSapling), 3, 20, 0xF27, 0));

				//this.Add(new GenericBuyInfo("Chili Pepper Seed", typeof(ChiliPepperSeed), 3, 20, 0xF27, 0));
				//this.Add(new GenericBuyInfo("Cucumber Seed", typeof(CucumberSeed), 3, 20, 0xF27, 0));
				//this.Add(new GenericBuyInfo("Green Pepper Seed", typeof(GreenPepperSeed), 3, 20, 0xF27, 0));
				//this.Add(new GenericBuyInfo("Orange Pepper Seed", typeof(OrangePepperSeed), 3, 20, 0xF27, 0));
				//this.Add(new GenericBuyInfo("Red Pepper Seed", typeof(RedPepperSeed), 3, 20, 0xF27, 0));
				//this.Add(new GenericBuyInfo("Tomato Seed", typeof(TomatoSeed), 3, 20, 0xF27, 0));
				//this.Add(new GenericBuyInfo("Yellow Pepper Seed", typeof(YellowPepperSeed), 3, 20, 0xF27, 0));

				this.Add(new GenericBuyInfo("Cantaloupe Seed", typeof(CantaloupeSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Green Squash Seed", typeof(GreenSquashSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Honeydew Melon Seed", typeof(HoneydewMelonSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Pumpkin Seed", typeof(PumpkinSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Squash Seed", typeof(SquashSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Watermelon Seed", typeof(WatermelonSeed), 3, 20, 0xF27, 0));

				this.Add(new GenericBuyInfo("Banana Seed", typeof(BananaSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Coconut Seed", typeof(CoconutSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Date Seed", typeof(DateSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Almond Seed", typeof(MiniAlmondSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Apple Seed", typeof(MiniAppleSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Apricot Seed", typeof(MiniApricotSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Avocado Seed", typeof(MiniAvocadoSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Cherry Seed", typeof(MiniCherrySeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Cocoa Seed", typeof(MiniCocoaSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Coffee Seed", typeof(MiniCoffeeSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Grapefruit Seed", typeof(MiniGrapefruitSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Kiwi Seed", typeof(MiniKiwiSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Lemon Seed", typeof(MiniLemonSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Lime Seed", typeof(MiniLimeSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Mango Seed", typeof(MiniMangoSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Orange Seed", typeof(MiniOrangeSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Peach Seed", typeof(MiniPeachSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Pear Seed", typeof(MiniPearSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Pistacio Seed", typeof(MiniPistacioSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Mini Pomegranate Seed", typeof(MiniPomegranateSeed), 3, 20, 0xF27, 0));
				this.Add(new GenericBuyInfo("Small Banana Seed", typeof(SmallBananaSeed), 3, 20, 0xF27, 0));










			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				//Add( typeof( MilkBucket ), 40 );
				//Add( typeof( CheeseForm ), 40 );

				Add( typeof( Apple ), 1 );
				Add( typeof( Grapes ), 1 );
				Add( typeof( Watermelon ), 1 );
				Add( typeof( YellowGourd ), 1 );
				Add( typeof( Pumpkin ), 1 );
				Add( typeof( Onion ), 1 );
				Add( typeof( Lettuce ), 2 );
				Add( typeof( Squash ), 1 );
				Add( typeof( Carrot ), 1 );
				Add( typeof( HoneydewMelon ), 1 );
				Add( typeof( Cantaloupe ), 1 );
				Add( typeof( Cabbage ), 1 );
				Add( typeof( Lemon ), 1 );
				Add( typeof( Lime ), 1 );
				Add( typeof( Peach ), 1 );
				Add( typeof( Pear ), 1 );
			}
		}
	}
}