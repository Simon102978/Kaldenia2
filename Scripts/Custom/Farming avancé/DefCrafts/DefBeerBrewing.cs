using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
    #region Recipes
    public enum BeerRecipes
    {
    }
    #endregion

    public class DefBeerBrewing : CraftSystem
	{
        public void SetNeedDistillery(int index, bool needDistillery)
        {
            CraftItem craftItem = CraftItems.GetAt(index);
            craftItem.NeedDistillery = needDistillery;
        }
        public override SkillName MainSkill { get { return SkillName.Alchemy; } }

	//	public override int GumpTitleNumber { get { return 0; } }

        public override string GumpTitleString { get { return "fabrication de Bi�re"; } }

		private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem { get { if (m_CraftSystem == null) m_CraftSystem = new DefBeerBrewing(); return m_CraftSystem; } }

		public override double GetChanceAtMin( CraftItem item ) { return 0.5; }

        private DefBeerBrewing() : base(1, 1, 1.25) { }

        public override int CanCraft(Mobile from, ITool tool, Type itemType)
        {
			if (((Item)tool).Deleted || tool.UsesRemaining < 0 ) return 1044038;
			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
            from.PlaySound(0x5B0);
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken ) from.SendLocalizedMessage( 1044038 );

			if ( failed )
			{
				if ( lostMaterial ) return 1044043;
				else return 1044157;
			}
			else
			{
				if ( quality == 0 ) return 502785;
				else if ( makersMark && quality == 2 ) return 1044156;
				else if ( quality == 2 ) return 1044155;
				else return 1044154;
			}
		}

		public override void InitCraftList()
		{
			int index = -1;
			string skillNotice = "Vous ignorez comment travailler ce type de Houblon.";

            index = AddCraft(typeof(LongNeckBottleOfMillerLite), "Long Necks", "a LongNeck Bottle Of Miller Lite", 0.0, 10.4, typeof(SnowHops), "Snow Hops", 2, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Levure � Fermentation", 1, "You need more Levure � Fermentation");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfMGD), "Long Necks", "a LongNeck Bottle Of Miller Genuine Draft", 10.0, 20.4, typeof(SweetHops), "Sweet Hops", 2, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Levure � Fermentation", 1, "You need more Levure � Fermentation");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfCoolersLite), "Long Necks", "a LongNeck Bottle Of Coolers Lite", 20.0, 35.4, typeof(SnowHops), "Snow Hops", 1, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Levure � Fermentation", 1, "You need more Levure � Fermentation");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfBudLight), "Long Necks", "a LongNeck Bottle Of Bud Light", 35.0, 50.4, typeof(BitterHops), "Bitter Hops", 1, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Levure � Fermentation", 1, "You need more Levure � Fermentation");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfBudWiser), "Long Necks", "a LongNeck Bottle Of Bud Wiser", 50.0, 65.4, typeof(BitterHops), "Bitter Hops", 2, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Levure � Fermentation", 1, "You need more Levure � Fermentation");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfCorna), "Long Necks", "a LongNeck Bottle Of Corna", 65.0, 78.4, typeof(ElvenHops), "Elven Hops", 2, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Levure � Fermentation", 1, "You need more Levure � Fermentation");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfCornaLite), "Long Necks", "a LongNeck Bottle Of Corna Lite", 78.0, 90.4, typeof(ElvenHops), "Elven Hops", 1, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Levure � Fermentation", 1, "You need more Levure � Fermentation");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfWildturkey), "Long Necks", "a LongNeck Bottle Of Wild Turkey", 90.0, 105.4, typeof(SweetHops), "Sweet Hops", 2, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Levure � Fermentation", 1, "You need more Levure � Fermentation");
            SetNeedDistillery(index, true);

			#region Bi�re Artisanale

			index = AddCraft(typeof(Larmedesirene), "Bi�res Artisanales", "Larmes de Sir�ne", 40.0, 70.0, typeof(BitterHops), "Bitter Hops", 2, "You need more Hops");
			AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
			AddRes(index, typeof(BrewersYeast), "Levure � Fermentation", 1, "You need more Levure � Fermentation");
			SetNeedDistillery(index, true);

			index = AddCraft(typeof(BiereDesFees), "Bi�res Artisanales", "Bi�re des F�es", 40.0, 70.0, typeof(SweetHops), "Sweet Hops", 2, "You need more Hops");
			AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
			AddRes(index, typeof(BrewersYeast), "Levure � Fermentation", 1, "You need more Levure � Fermentation");
			SetNeedDistillery(index, true);

			index = AddCraft(typeof(Krakabeille), "Bi�res Artisanales", "Krakabeille", 40.0, 70.0, typeof(SnowHops), "Snow Hops", 2, "You need more Hops");
			AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
			AddRes(index, typeof(BrewersYeast), "Levure � Fermentation", 1, "You need more Levure � Fermentation");
			SetNeedDistillery(index, true);






			#endregion

			SetSubRes( typeof( BitterHops ),	"Bitter Hops" );

			AddSubRes( typeof( BitterHops ),	"Bitter Hops", 10.0, skillNotice);
			AddSubRes( typeof( SnowHops ),	"Snow Hops", 20.0, skillNotice);
			AddSubRes( typeof( ElvenHops ),	"Elven Hops", 30.0, skillNotice);
			AddSubRes( typeof( SweetHops ),	"Sweet Hops", 40.0, skillNotice);

			MarkOption = true;
			Repair = false;
		}
	}
}
