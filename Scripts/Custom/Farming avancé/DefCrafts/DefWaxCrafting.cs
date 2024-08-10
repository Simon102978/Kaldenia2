using System;
using Server.Items;

namespace Server.Engines.Craft
{
    #region Recipes
    public enum WaxRecipes
    {
    }
    #endregion
    public class DefWaxCrafting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Alchemy;	}
		}

//		public override int GumpTitleNumber
//		{
//			get { return 1044003; } // <CENTER>COOKING MENU</CENTER>
//		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefWaxCrafting();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefWaxCrafting() : base( 1, 1, 1.25 )// base( 1, 1, 1.5 )
		{
		}

        public override int CanCraft(Mobile from, ITool tool, Type itemType)
        {
			if (((Item)tool).Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( (BaseTool)tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x5AC );
		}

		public override int PlayEndingEffect(
	Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
		{
			if (toolBroken)
			{
				from.SendLocalizedMessage(1044038); // You have worn out your tool
			}

			if (failed)
			{
				if (lostMaterial)
				{
					return 1044043; // You failed to create the item, and some of your materials are lost.
				}

				return 1044157; // You failed to create the item, but no materials were lost.
			}

			if (quality == 0)
			{
				return 502785; // You were barely able to make this item.  It's quality is below average.
			}

			if (makersMark && quality == 2)
			{
				return 1044156; // You create an exceptional quality item and affix your maker's mark.
			}

			if (quality == 2)
			{
				return 1044155; // You create an exceptional quality item.
			}
			if (makersMark && quality == 3)
			{
				from.SendMessage("Vous créez un item de qualité Épique et apposer votre marque."); ; // You create an epic quality item.			
			}
			if (quality == 3)
			{
				from.SendMessage("Vous créez un item de qualité Épique."); ; // You create an epic quality item.
			}
			if (makersMark && quality == 4)
			{
				from.SendMessage("Vous créez un item de qualité Légendaire et apposer votre marque."); ; // You create a legendary quality item.
			}
			if (quality == 4)
			{
				from.SendMessage("Vous créez un item de qualité Légendaire."); ; // You create a legendary quality item.
			}
			return 1044154; // You create the item.
		}

		public override void InitCraftList()
		{
			int index = -1;



			/* Begin Ressources */
			index = AddCraft( typeof( CandleWick ), "Ressources", "Mèche de bougie", 50.0, 80.0, typeof( Beeswax ), "Cire d'abeille", 1, "Vous n'avez pas suffisament de cire d'abeille" );
			AddRes( index, typeof( Cloth ), "Tissus", 1, "Vous n'avez pas assez de Tissus" );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( BlankCandle ), "Ressources", "Chandelle Vierge", 50.0, 80.0, typeof( Beeswax ), "Cire d'abeille", 2, "Vous n'avez pas suffisament de cire d'abeille" );
			SetNeedHeat( index, true );

			index = AddCraft(typeof(CandleFitSkull), "Ressources", "Crâne de cire Vierge", 50.0, 80.0, typeof(Beeswax), "Cire d'abeille", 4, "Vous n'avez pas suffisament de cire d'abeille");
			SetNeedHeat(index, true);
			/* End Ressources */

			/* Begin Candles */


			index = AddCraft(typeof(Candle), "Chandelles", " Petite Bougie", 25.0, 60.0, typeof(BlankCandle), "Chandelle Vierge", 1, "Vous avez besoin d'une Chandelle Vierge");
			AddRes(index, typeof(CandleWick), "Mèche de bougie", 1, "Vous avez besoin d'une Mèche de bougie");
			SetNeedHeat(index, true);

			index = AddCraft(typeof(CandleLarge), "Chandelles", " Chandelle portative", 25.0, 60.0, typeof(BlankCandle), "Chandelle Vierge", 1, "Vous avez besoin d'une Chandelle Vierge");
			AddRes(index, typeof(CandleWick), "Mèche de bougie", 1, "Vous avez besoin d'une Mèche de bougie");
			SetNeedHeat(index, true);


			index = AddCraft( typeof( CandleShort ), "Chandelles", "Petite Chandelle", 80.0, 105.0, typeof( BlankCandle ), "Chandelle Vierge", 1, "Vous avez besoin d'une Chandelle Vierge" );
			AddRes( index, typeof( CandleWick ), "Mèche de bougie", 1, "Vous avez besoin d'une Mèche de bougie" );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( CandleShortColor ), "Chandelles", "Petite chandelle colorée", 80.0, 105.0, typeof( BlankCandle ), "Chandelle Vierge", 1, "Vous avez besoin d'une Chandelle Vierge" );
			AddRes( index, typeof( Dyes ), "Dyes", 1, "Vous avez besoin d'un Dyes (Colorant)" );
			AddRes( index, typeof( CandleWick ), "Mèche de bougie", 1, "Vous avez besoin d'une Mèche de bougie" );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( CandleLong ), "Chandelles", "Large chandelle", 80.0, 110.0, typeof( BlankCandle ), "Chandelle Vierge", 1, "Vous avez besoin d'une Chandelle Vierge" );
			AddRes( index, typeof( CandleWick ), "Mèche de bougie", 1, "Vous avez besoin d'une Mèche de bougie" );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( CandleLongColor ), "Chandelles", "Large chandelle colorée", 80.0, 110.0, typeof( BlankCandle ), "Chandelle Vierge", 1, "Vous avez besoin d'une Chandelle Vierge" );
			AddRes(index, typeof(Dyes), "Dyes", 1, "Vous avez besoin d'un Dyes (Colorant)");
			AddRes( index, typeof( CandleWick ), "Mèche de bougie", 1, "Vous avez besoin d'une Mèche de bougie" );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( CandleSkull ), "Chandelles", "Chandelle en forme de crâne", 80.0, 100.0, typeof( BlankCandle ), "Chandelle Vierge", 1, "Vous avez besoin d'une Chandelle Vierge" );
			AddRes( index, typeof( CandleWick ), "Mèche de bougie", 1, "Vous avez besoin d'une Mèche de bougie" );
			AddRes( index, typeof( CandleFitSkull ), "Crâne de cire Vierge", 1, "Vous avez besoin d'un Crâne de cire Vierge");
			SetNeedHeat( index, true );

			index = AddCraft( typeof( CandleOfLove ), "Chandelles", "Chandelle de l'amour", 80.0, 100.0, typeof( BlankCandle ), "Chandelle Vierge", 1, "Vous avez besoin d'une Chandelle Vierge" );
			AddRes( index, typeof( CandleWick ), "Mèche de bougie", 1, "Vous avez besoin d'une Mèche de bougie" );
			
			SetNeedHeat( index, true );
			/* End Candles */

			/* Begin Decorative */
			index = AddCraft( typeof( DippingStick ), "Decoration", "Bâtonnets de cire", 75.0, 115.0, typeof( BlankCandle ), "Chandelle Vierge", 3, "Vous avez besoin de chandelles vierges" );
			SetNeedHeat( index, true );



			index = AddCraft( typeof( PileOfBlankCandles ), "Decoration", "Pile de chandelles vierges", 75.0, 115.0, typeof( BlankCandle ), "Chandelle Vierge", 5, "Vous avez besoin de chandelles vierges" );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( SomeBlankCandles ), "Decoration", "Quelques chandelles vierges", 75.0, 115.0, typeof( BlankCandle ), "Chandelle Vierge", 3, "Vous avez besoin de chandelles vierges" );
			SetNeedHeat( index, true );

			index = AddCraft(typeof(OfficialSealingWax), "Decoration", "Sceau officiel", 50.0, 80.0, typeof(Beeswax), "Cire d'abeille", 4, "Vous n'avez pas suffisament de cire d'abeille");
			SetNeedHeat(index, true);

			index = AddCraft( typeof( RawWaxBust ), "Decoration", "Buste de Cire", 50.0, 80.0, typeof( Beeswax ), "Cire d'abeille", 4, "Vous n'avez pas suffisament de cire d'abeille" );
			SetNeedHeat( index, true );

			index = AddCraft(typeof(OrigamiButterfly), "Decoration", "Un Papillon de Cire", 75.0, 115.0, typeof(Beeswax), "Cire d'abeille", 15, "Vous n'avez pas suffisament de cire d'abeille");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(OrigamiSwan), "Decoration", "Un Cygne de Cire", 75.0, 115.0, typeof(Beeswax), "Cire d'abeille", 15, "Vous n'avez pas suffisament de cire d'abeille");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(OrigamiFrog), "Decoration", "Une Grenouille de Cire", 75.0, 115.0, typeof(Beeswax), "Cire d'abeille", 15, "Vous n'avez pas suffisament de cire d'abeille");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(OrigamiShape), "Decoration", "Une forme de Cire", 75.0, 115.0, typeof(Beeswax), "Cire d'abeille", 15, "Vous n'avez pas suffisament de cire d'abeille");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(OrigamiSongbird), "Decoration", "Un Oiseau de Cire", 75.0, 115.0, typeof(Beeswax), "Cire d'abeille", 15, "Vous n'avez pas suffisament de cire d'abeille");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(OrigamiFish), "Decoration", "Un Poisson de Cire", 75.0, 115.0, typeof(Beeswax), "Cire d'abeille", 15, "Vous n'avez pas suffisament de cire d'abeille");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(OrigamiDragon), "Decoration", "Un Dragon de Cire", 75.0, 115.0, typeof(Beeswax), "Cire d'abeille", 15, "Vous n'avez pas suffisament de cire d'abeille");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(OrigamiBunny), "Decoration", "Un Lapin de Cire", 75.0, 115.0, typeof(Beeswax), "Cire d'abeille", 15, "Vous n'avez pas suffisament de cire d'abeille");
            SetNeedHeat(index, true); 

			/* End Decorative */

		}
	}
}
