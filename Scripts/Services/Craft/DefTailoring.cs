using Arya.Chess;
using Server.Engines.Quests;
using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Web.Routing;
using System.Web.UI.WebControls;
using static Server.HueData;

namespace Server.Engines.Craft
{
	public enum TailorRecipe
	{
		ElvenQuiver = 501,
		QuiverOfFire = 502,
		QuiverOfIce = 503,
		QuiverOfBlight = 504,
		QuiverOfLightning = 505,

		SongWovenMantle = 550,
		SpellWovenBritches = 551,
		StitchersMittens = 552,

		JesterShoes = 560,
		ChefsToque = 561,
		GuildedKilt = 562,
		CheckeredKilt = 563,
		FancyKilt = 564,
		FloweredDress = 565,
		EveningGown = 566,

		TigerPeltChest = 570,
		TigerPeltCollar = 571,
		TigerPeltHelm = 572,
		TigerPeltLegs = 573,
		TigerPeltShorts = 574,
		TigerPeltBustier = 575,
		TigerPeltLongSkirt = 576,
		TigerPeltSkirt = 577,

		DragonTurtleHideArms = 580,
		DragonTurtleHideChest = 581,
		DragonTurtleHideHelm = 582,
		DragonTurtleHideLegs = 583,
		DragonTurtleHideBustier = 584,

		// doom
		CuffsOfTheArchmage = 585,

		KrampusMinionHat = 586,
		KrampusMinionBoots = 587,
		KrampusMinionTalons = 588,

		MaceBelt = 1100,
		SwordBelt = 1101,
		DaggerBelt = 1102,
		ElegantCollar = 1103,
		CrimsonMaceBelt = 1104,
		CrimsonSwordBelt = 1105,
		CrimsonDaggerBelt = 1106,
		ElegantCollarOfFortune = 1107,
		AssassinsCowl = 1108,
		MagesHood = 1109,
		CowlOfTheMaceAndShield = 1110,
		MagesHoodOfScholarlyInsight = 1111,

	}

	public class DefTailoring : CraftSystem
	{
		#region Statics
		private static readonly Type[] m_TailorColorables = new Type[]
   		{
			typeof(GozaMatEastDeed), typeof(GozaMatSouthDeed),
			typeof(SquareGozaMatEastDeed), typeof(SquareGozaMatSouthDeed),
			typeof(BrocadeGozaMatEastDeed), typeof(BrocadeGozaMatSouthDeed),
			typeof(BrocadeSquareGozaMatEastDeed), typeof(BrocadeSquareGozaMatSouthDeed),
			typeof(SquareGozaMatDeed)
   		};

		private static readonly Type[] m_TailorClothNonColorables = new Type[]
		{
			typeof(DeerMask), typeof(BearMask), typeof(OrcMask), typeof(TribalMask), typeof(HornedTribalMask), typeof(CuffsOfTheArchmage)
		};

		// singleton instance
		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if (m_CraftSystem == null)
					m_CraftSystem = new DefTailoring();

				return m_CraftSystem;
			}
		}
		#endregion

		#region Constructor
		private DefTailoring()
			: base(3, 4, 1.50)// base( 1, 1, 4.5 )
		{
		}

		#endregion

		#region Overrides
		public override SkillName MainSkill => SkillName.Tailoring;

		public override string GumpTitleString => "Couture";
		public override CraftECA ECA => CraftECA.Chance3Max;

		public override double GetChanceAtMin(CraftItem item)
		{
			if (item.NameNumber == 1157348 || item.NameNumber == 1159225 || item.NameNumber == 1159213 || item.NameNumber == 1159212 ||
				item.NameNumber == 1159211 || item.NameNumber == 1159228 || item.NameNumber == 1159229)
				return 0.05; // 5%

			return 0.5; // 50%
		}

		public override int CanCraft(Mobile from, ITool tool, Type itemType)
		{
			int num = 0;

			if (tool == null || tool.Deleted || tool.UsesRemaining <= 0)
				return 1044038; // You have worn out your tool!
			else if (!tool.CheckAccessible(from, ref num))
				return num; // The tool must be on your person to use.

			return 0;
		}

		public override bool RetainsColorFrom(CraftItem item, Type type)
		{
			if (type != typeof(Cloth) && type != typeof(UncutCloth) && type != typeof(AbyssalCloth))
				return false;

			type = item.ItemType;

			bool contains = false;

			for (int i = 0; !contains && i < m_TailorColorables.Length; ++i)
				contains = (m_TailorColorables[i] == type);

			return contains;
		}

		public override bool RetainsColorFromException(CraftItem item, Type type)
		{
			if (item == null || type == null)
				return false;

			if (type != typeof(Cloth) && type != typeof(UncutCloth) && type != typeof(AbyssalCloth))
				return false;

			bool contains = false;

			for (int i = 0; !contains && i < m_TailorClothNonColorables.Length; ++i)
				contains = (m_TailorClothNonColorables[i] == item.ItemType);

			return contains;
		}

		public override void PlayCraftEffect(Mobile from)
		{
			from.PlaySound(0x248);
		}

		public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
		{
			if (toolBroken)
				from.SendLocalizedMessage(1044038); // You have worn out your tool

			if (failed)
			{
				if (lostMaterial)
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				if (quality == 0)
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if (makersMark && quality == 2)
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if (quality == 2)
					return 1044155; // You create an exceptional quality item.
				else
					return 1044154; // You create the item.
			}
		}
		#endregion

		public override void InitCraftList()
		{
			int index = -1;

			index = AddCraft(typeof(Skirt), "Jupes", "Jupe Simple", 0.0, 20.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeCourte7), "Jupes", "Jupe Courte Droite", 10.0, 30.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Jupe11), "Jupes", "Jupe Sombre", 10.0, 30.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeCourte3), "Jupes", "Mini jupe à Ceinture", 15.0, 35.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Jupe5), "Jupes", "Jupe à Motifs", 20.0, 40.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeLacee), "Jupes", "Jupe Lacée", 20.0, 40.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Jupe8), "Jupes", "Jupe Provocante à Volants", 30.0, 50.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pareo), "Jupes", "Jupe Ouverte", 35.0, 55.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Jupe9), "Jupes", "Jupe à Saccoche", 40.0, 60.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeVoiles2), "Jupes", "Jupe à Volant", 45.0, 65.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Jupe), "Jupes", "Jupe à Bande", 45.0, 65.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeLacee3), "Jupes", "Jupe Lacée Droite", 50.0, 70.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeOndule2), "Jupes", "Jupe Ondulée froufrou", 55.0, 75.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeCourte4), "Jupes", "Jupe Barbare", 60.0, 80.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Jupe7), "Jupes", "Jupe Provocante", 60.0, 80.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeLacee2), "Jupes", "Jupe Lacée Sombre", 60.0, 80.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Jupe3), "Jupes", "Jupe Droite", 60.0, 80.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeCourte5), "Jupes", "Jupe Courte à Vollant", 65.0, 85.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Jupe6), "Jupes", "Jupe à Plis", 65.0, 85.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeCourte2), "Jupes", "Jupe Quadrillée", 65.0, 85.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			//index	=	AddCraft(typeof(JupeOndulee),	"Jupes",	Jupe Ondulée,	70.0,	90.0,	typeof(Cloth),	"Tissus",	8	,"Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Jupe4), "Jupes", "Jupe Artisane", 70.0, 90.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Jupe10), "Jupes", "Jupe à Jartelles", 75.0, 95.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Jupe2), "Jupes", "Jupe Délicate", 75.0, 95.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeVoiles), "Jupes", "Jupe à Banquet", 80.0, 100.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeCourte6), "Jupes", "Jupe Courte à Vollant Unie", 80.0, 100.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(PareoCourt), "Jupes", "Paréo", 85.0, 105.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JupeCourte), "Jupes", "Jupe Courte Lacée", 90.0, 110.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");


			index = AddCraft(typeof(JesterSuit), "Hauts", "Tunique de bouffon", 0.0, 20.0, typeof(Cloth), "Tissus", 24, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tunic), "Hauts", "Tunique", 0.0, 20.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Shirt), "Hauts", "Camisole", 0.0, 20.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(FancyShirt), "Hauts", "Chandail de banquet", 10.0, 30.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tabar3), "Hauts", "Tabar à cape", 10.0, 30.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Veste3), "Hauts", "Manteau Propre", 10.0, 30.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(TuniqueCeinture), "Hauts", "Tunique à Ceinture", 10.0, 30.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ManteauDore), "Hauts", "Manteau Doré", 20.0, 40.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			//index	=	AddCraft(typeof(HautLacet),	"Hauts",	Haut Lacet,	20.0,	40.0,	typeof(Cloth),	"Tissus",	8	,"Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Bustier), "Hauts", "Bustier ailé", 20.0, 40.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tabar9), "Hauts", "Grand tabar ouvert", 20.0, 40.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tunique3), "Hauts", "Tunique à plis", 20.0, 40.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chemise), "Hauts", "Chemise propre", 20.0, 40.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chemise2), "Hauts", "Chemise noble", 30.0, 50.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Doublet6), "Hauts", "Doublet à bouton", 30.0, 50.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Uniforme), "Hauts", "Manteau d'uniforme", 30.0, 50.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tabar5), "Hauts", "Tabar sombre à griffon", 30.0, 50.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Doublet), "Hauts", "Doublet", 30.0, 50.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tabar8), "Hauts", "Tabar doré capitoné", 30.0, 50.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tabar11), "Hauts", "Grand tabar simple", 40.0, 60.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tabar6), "Hauts", "Tabar à arbre", 40.0, 60.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Veste), "Hauts", "Veste", 40.0, 60.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chandail10), "Hauts", "Chandail manche ample", 40.0, 60.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chemise6), "Hauts", "Chemise lacée", 40.0, 60.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tunique10), "Hauts", "Tunique à ceinture", 50.0, 70.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ChemiseRiche), "Hauts", "Chemise Riche", 50.0, 70.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(VestonRiche), "Hauts", "Veston Riche", 50.0, 70.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ChemiseCorset1), "Hauts", "Chemise à Corset 1", 50.0, 70.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ChemiseCorset2), "Hauts", "Chemise à Corset 2", 50.0, 70.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CorsetCuir), "Hauts", "Corset de Cuir", 55.0, 75.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CorsetEpaule), "Hauts", "Corset Épaule", 55.0, 75.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CorsetSimple), "Hauts", "Corset Simple", 55.0, 75.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CorsetTissus), "Hauts", "Corset de Tissus", 55.0, 75.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tabar7), "Hauts", "Tabar doré", 55.0, 75.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tunique), "Hauts", "Tunique en peaux", 60.0, 80.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tabar1), "Hauts", "Tabar orné", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tabar10), "Hauts", "Grand tabar de toile", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Doublet4), "Hauts", "Doublet lacé croisé", 60.0, 80.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tunique6), "Hauts", "Tunique à voilant", 60.0, 80.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Doublet5), "Hauts", "Doublet ajusté", 60.0, 80.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Manteau2), "Hauts", "Manteau ample", 60.0, 80.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tabar), "Hauts", "Tabar simple", 65.0, 85.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chandail7), "Hauts", "Grand chandail", 65.0, 85.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tunique2), "Hauts", "Tunique bouffante", 65.0, 85.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tunique5), "Hauts", "Tunique sans manche", 65.0, 85.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tunique7), "Hauts", "Tunique propre", 65.0, 85.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Camisole), "Hauts", "Chandail de travail", 65.0, 85.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tunique4), "Hauts", "Tunique à cordon", 65.0, 85.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chandail), "Hauts", "Chandail distingué", 65.0, 85.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chandail6), "Hauts", "Doublet demi - manche", 70.0, 90.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Surcoat), "Hauts", "Tunique ajustée", 70.0, 90.0, typeof(Cloth), "Tissus", 14, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ElvenShirt), "Hauts", "Chemise ornée", 70.0, 90.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ElvenDarkShirt), "Hauts", "Chemise ornée sombre", 70.0, 90.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Bustier3), "Hauts", "Bustier demi - manche", 70.0, 90.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chandail4), "Hauts", "Chandail ample", 70.0, 90.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chandail5), "Hauts", "Chandail propre", 75.0, 95.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Veste2), "Hauts", "Veste manche courte", 75.0, 95.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chandail2), "Hauts", "Tunique ornée", 75.0, 95.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Doublet2), "Hauts", "Doublet à épaulette", 75.0, 95.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chandail9), "Hauts", "Chandail bouffant", 75.0, 95.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chemise3), "Hauts", "Chemise longue lacée", 75.0, 95.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tunique8), "Hauts", "Tunique élégante", 80.0, 100.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Bustier2), "Hauts", "Bustier provocant", 80.0, 100.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chandail3), "Hauts", "Corset Bouffant", 80.0, 100.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chemise5), "Hauts", "Chemise ajustée", 80.0, 100.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tunique9), "Hauts", "Tunique sombre", 85.0, 105.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Tabar4), "Hauts", "Tabar sombre", 85.0, 105.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Doublet3), "Hauts", "Doublet lacé", 85.0, 105.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chandail8), "Hauts", "Chandail Manche Longue", 85.0, 105.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chemise7), "Hauts", "Manteau élégant", 90.0, 110.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chemise4), "Hauts", "Chemise simple", 90.0, 110.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");


			index = AddCraft(typeof(PlainDress), "Robes", "Robe Paysane", 0.0, 20.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(FancyDress), "Robes", "Robe élégante", 10.0, 30.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeProvocante5), "Robes", "Robe provocante légère", 10.0, 30.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe14), "Robes", "Robe Provocante délicate", 15.0, 35.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe15), "Robes", "Robe légère", 20.0, 40.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeProvocante2), "Robes", "Robe Provocante ornée", 20.0, 40.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe6), "Robes", "Robe à volants", 30.0, 50.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe11), "Robes", "Robe Délicate sombre", 35.0, 55.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe19), "Robes", "Robe sans manche", 40.0, 60.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe2), "Robes", "Robe provocante", 40.0, 60.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe13), "Robes", "Robe Ouverte", 40.0, 60.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeProvocante3), "Robes", "Robe dorée sombre", 45.0, 65.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe9), "Robes", "Robe d'érudit", 45.0, 65.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeProvocante), "Robes", "Robe Sombre", 50.0, 70.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeProvocante6), "Robes", "Robe dorée", 50.0, 70.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe18), "Robes", "Robe à Col", 50.0, 70.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeCourteLacet), "Robes", "Robe Courte Lacet", 55.0, 75.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeBleudecolte), "Robes", "Robe Bleue decoltée", 55.0, 75.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeLacetCuir), "Robes", "Robe Lacet Cuir", 55.0, 75.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeDessin), "Robes", "Robe à motifs", 60.0, 80.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeCorset), "Robes", "Robe Corset", 60.0, 80.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeNobleDecolte), "Robes", "Robe Noble Decoltée", 60.0, 80.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeDoree), "Robes", "Robe Dorée", 60.0, 80.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeMoulante), "Robes", "Robe Moulante", 60.0, 80.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(MaleElvenRobe), "Robes", "Robe à Capuchon", 65.0, 85.0, typeof(Cloth), "Tissus", 30, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(FemaleElvenRobe), "Robes", "Grande Robe Toge", 65.0, 85.0, typeof(Cloth), "Tissus", 30, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe5), "Robes", "Robe artisane", 65.0, 85.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeCourte), "Robes", "Robe Courte", 70.0, 90.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeProvocante4), "Robes", "Robe décoltée", 70.0, 90.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe16), "Robes", "Robe provocante Sombre", 75.0, 95.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe8), "Robes", "Robe lacée large", 75.0, 95.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe12), "Robes", "Robe délicate", 80.0, 100.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe3), "Robes", "Robe manches courtes", 80.0, 100.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RobeNim), "Robes", "Robe Nimunique", 80.0, 100.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");

			index = AddCraft(typeof(Robe7), "Robes", "Robe Simple", 85.0, 105.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Robe10), "Robes", "Robe manches amples", 90.0, 110.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");


			index = AddCraft(typeof(Robe), "Toges", "Toge Simple", 50.0, 70.0, typeof(Cloth), "Tissus", 16, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Toge), "Toges", "Toge Souple", 50.0, 70.0, typeof(Cloth), "Tissus", 18, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Toge3), "Toges", "Toge à épaulettes", 55.0, 75.0, typeof(Cloth), "Tissus", 18, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Toge8), "Toges", "Toge Sombre", 55.0, 75.0, typeof(Cloth), "Tissus", 18, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(TogeKoraine), "Toges", "Toge Évasée", 60.0, 80.0, typeof(Cloth), "Tissus", 18, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(TogeSparte), "Toges", "Toge Stylée", 60.0, 80.0, typeof(Cloth), "Tissus", 18, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Toge5), "Toges", "Toge à ceinture large", 60.0, 80.0, typeof(Cloth), "Tissus", 18, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Manteau), "Toges", "Toge ornée", 60.0, 80.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Toge4), "Toges", "Toge en toile", 65.0, 85.0, typeof(Cloth), "Tissus", 18, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Toge2), "Toges", "Toge Propre", 65.0, 85.0, typeof(Cloth), "Tissus", 18, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Toge6), "Toges", "Toge à ceinture dorée", 65.0, 85.0, typeof(Cloth), "Tissus", 18, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Toge7), "Toges", "Toge en voile", 70.0, 90.0, typeof(Cloth), "Tissus", 18, "Vous n'avez pas assez de tissus.");



			index = AddCraft(typeof(ShortPants), "Pantalons", "Short Taille Haute", 50.0, 70.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Kilt), "Pantalons", "Kilt", 50.0, 70.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(LongPants), "Pantalons", "Pantalon Taille Haute", 55.0, 75.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon7), "Pantalons", "Short Droit", 55.0, 75.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon8), "Pantalons", "Short Ample", 55.0, 75.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon4), "Pantalons", "Pantalon à Poches", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Kilt3), "Pantalons", "Grand Kilt", 60.0, 80.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon12), "Pantalons", "Pantalon de Toile", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon2), "Pantalons", "Pantalon à Motifs", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon5), "Pantalons", "Pantalon de Cuir", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon6), "Pantalons", "Pantalon Court à Ceinture", 65.0, 85.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon10), "Pantalons", "Pantalon Sombre", 65.0, 85.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon11), "Pantalons", "Pantalon Jupe", 65.0, 85.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Kilt2), "Pantalons", "Kilt à Bandouillère", 70.0, 90.0, typeof(Cloth), "Tissus", 8, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon9), "Pantalons", "Short de Toile", 70.0, 90.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ElvenPants), "Pantalons", "Pantalon Moulant", 75.0, 95.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(WoodlandBelt), "Pantalons", "Pagne", 75.0, 95.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Salopette), "Pantalons", "Salopette", 80.0, 100.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CulotteLeopard), "Pantalons", "Pantalons Léopard", 80.0, 100.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon1), "Pantalons", "Pantalon Ample", 85.0, 105.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Pantalon3), "Pantalons", "Pantalon à Fourrure", 90.0, 110.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");


			index = AddCraft(typeof(Capuche), "Chapeaux", "Capuche", 0.0, 20.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(SkullCap), "Chapeaux", "Bandana", 10.0, 30.0, typeof(Cloth), "Tissus", 2, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Bandana), "Chapeaux", "Bandeau", 10.0, 30.0, typeof(Cloth), "Tissus", 2, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Capuche1), "Chapeaux", "Capuche", 15.0, 35.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ChapeauPlume1), "Chapeaux", "Chapeau à Plume Longue", 20.0, 40.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ChapeauToc), "Chapeaux", "Petite Toque", 20.0, 40.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ChapeauPlume2), "Chapeaux", "Chapeau à Plume Courte", 30.0, 50.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ToquePlume), "Chapeaux", "Toque à Plume", 35.0, 55.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ChapeauPirate), "Chapeaux", "Chapeau de Pirate", 40.0, 60.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CapucheToile), "Chapeaux", "Capuche à Toile", 40.0, 60.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ChapeauFoulard), "Chapeaux", "Chapeau Foulard", 40.0, 60.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ChapeauMousquetaire), "Chapeaux", "Chapeau Mousquetaire", 45.0, 65.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ChapeauPlume3), "Chapeaux", "Chapeau à Plume", 45.0, 65.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Chale1), "Chapeaux", "Chale voilé", 50.0, 70.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CoiffeEgypte), "Chapeaux", "Coiffe Égyptienne", 50.0, 70.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CoiffeColore), "Chapeaux", "Coiffe Colorée", 50.0, 70.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ToqueBouffon), "Chapeaux", "Toque Bouffon", 55.0, 75.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(MasqueEpouvantail), "Chapeaux", "Masque Epouvantail", 55.0, 75.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(FloppyHat), "Chapeaux", "Chapeau", 55.0, 75.0, typeof(Cloth), "Tissus", 11, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cap), "Chapeaux", "Petit Chapeau", 60.0, 80.0, typeof(Cloth), "Tissus", 11, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(WideBrimHat), "Chapeaux", "Petit chapeau de paille", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(StrawHat), "Chapeaux", "Chapeau de paille", 60.0, 80.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Bonnet), "Chapeaux", "Bonnet", 60.0, 80.0, typeof(Cloth), "Tissus", 11, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(FeatheredHat), "Chapeaux", "Chapeau à plume", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(TricorneHat), "Chapeaux", "Chapeau Tricorne", 65.0, 85.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(TallStrawHat), "Chapeaux", "Grand chapeau de paille", 65.0, 85.0, typeof(Cloth), "Tissus", 13, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(WizardsHat), "Chapeaux", "Chapeau de sorcier", 65.0, 85.0, typeof(Cloth), "Tissus", 15, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(JesterHat), "Chapeaux", "Chapeau de bouffon", 70.0, 90.0, typeof(Cloth), "Tissus", 15, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ChapeauMage), "Chapeaux", "majisto", 70.0, 90.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ClothNinjaHood), "Chapeaux", "Capuche de ninja", 75.0, 95.0, typeof(Cloth), "Tissus", 13, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(VoileTete), "Chapeaux", "Voile", 75.0, 95.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Toque), "Chapeaux", "Toque", 80.0, 100.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Turban), "Chapeaux", "Turban", 80.0, 100.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(VoileTeteLong), "Chapeaux", "Long voile", 85.0, 105.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Capuche2), "Chapeaux", "Grande Capuche", 90.0, 110.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");





			index = AddCraft(typeof(Cloak), "Capes", "Cape Simple", 40.0, 60.0, typeof(Cloth), "Tissus", 14, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cape10), "Capes", "Demi cape en cuir", 40.0, 60.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cape), "Capes", "Cape à Vollets", 45.0, 65.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cape7), "Capes", "Cape à bande doré", 45.0, 65.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cape2), "Capes", "Cape à plumes", 50.0, 70.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cape6), "Capes", "Cape en fourrure", 50.0, 70.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cape9), "Capes", "Demi cape Distinguée", 55.0, 75.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cape5), "Capes", "Cape à rabat", 55.0, 75.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cape11), "Capes", "Demi cape élégante", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CapeBleu), "Capes", "Cape Bleue", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CapePaon), "Capes", "Cape Paon", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Drapee), "Capes", "Drapée", 60.0, 80.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cape4), "Capes", "Cape à épaulière", 65.0, 85.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cape8), "Capes", "Demi cape avec cuir", 65.0, 85.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(VoileDos), "Capes", "Voile De Dos", 65.0, 85.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cape3), "Capes", "Cape à voiles", 70.0, 90.0, typeof(Cloth), "Tissus", 12, "Vous n'avez pas assez de tissus.");




			index = AddCraft(typeof(Foulard), "Divers", "Foulard", 0.0, 20.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CombineCloth), "Divers", "Tissus Combiné", 10.0, 30.0, typeof(Cloth), "Tissus", 1, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(HalfApron), "Divers", "Demi - Tablier", 10.0, 30.0, typeof(Cloth), "Tissus", 6, "Vous n'avez pas assez de tissus.");
			//index = AddCraft(typeof(GargoyleHalfApron), "Divers", "Demi tablier élégant", 15.0, 35.0, typeof(Cloth), "Tissus", 6   ,"Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(FullApron), "Divers", "Tablier", 20.0, 40.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(SmallDiamondPillow), "Divers", "Petit Coussin Diamant", 20.0, 40.0, typeof(Cloth), "Tissus", 15, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BodySash), "Divers", "Ceinture de Corps", 30.0, 50.0, typeof(Cloth), "Tissus", 4, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Sash), "Divers", "Cocarde élégant", 35.0, 55.0, typeof(Cloth), "Tissus", 4, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BourseCeinture), "Divers", "Bourse de Ceinture", 40.0, 60.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CacheOeil1), "Divers", "Cache Oeil Droit", 40.0, 60.0, typeof(Cloth), "Tissus", 3, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CacheOeil2), "Divers", "Cache Oeil Gauche", 40.0, 60.0, typeof(Cloth), "Tissus", 3, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CacheOeil3), "Divers", "Bandeau Oeil droit", 45.0, 65.0, typeof(Cloth), "Tissus", 3, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(GozaMatEastDeed), "Divers", "Tapis Goza Rectangle(E)", 45.0, 65.0, typeof(Cloth), "Tissus", 25, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(GozaMatSouthDeed), "Divers", "Tapis Goza Rectangle(S)", 50.0, 70.0, typeof(Cloth), "Tissus", 25, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(SquareGozaMatDeed), "Divers", "Tapis Goza Carré Uni", 50.0, 70.0, typeof(Cloth), "Tissus", 25, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(SquareGozaMatEastDeed), "Divers", "Tapis Goza Ligne(E)", 50.0, 70.0, typeof(Cloth), "Tissus", 25, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(SquareGozaMatSouthDeed), "Divers", "Tapis Goza Ligne(S)", 55.0, 75.0, typeof(Cloth), "Tissus", 25, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BrocadeGozaMatEastDeed), "Divers", "Tapis Goza Brodé Rectangle(E)", 55.0, 75.0, typeof(Cloth), "Tissus", 25, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BrocadeGozaMatSouthDeed), "Divers", "Tapis Goza Brodé Rectangle(S)", 55.0, 75.0, typeof(Cloth), "Tissus", 25, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BrocadeSquareGozaMatEastDeed), "Divers", "Tapis Goza Carré Brodé Ligné(E)", 60.0, 80.0, typeof(Cloth), "Tissus", 25, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BrocadeSquareGozaMatSouthDeed), "Divers", "Tapis Goza Carré Brodé Ligné(S)", 60.0, 80.0, typeof(Cloth), "Tissus", 25, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(LargeDiamondPillow), "Divers", "Coussin Diamant", 60.0, 80.0, typeof(Cloth), "Tissus", 25, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(LargeSquarePillow), "Divers", "Coussin Carré", 60.0, 80.0, typeof(Cloth), "Tissus", 20, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ThrowPillow), "Divers", "Coussin à Motifs", 60.0, 80.0, typeof(Cloth), "Tissus", 20, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Coussin1), "Divers", "Coussin Cylindré", 65.0, 85.0, typeof(Cloth), "Tissus", 20, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Coussin2), "Divers", "Coussin Rond et Plissé", 65.0, 85.0, typeof(Cloth), "Tissus", 20, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Coussin3), "Divers", "Coussin Rond et Lisse", 65.0, 85.0, typeof(Cloth), "Tissus", 20, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Coussin4), "Divers", "Coussin Carré et Plissé", 70.0, 90.0, typeof(Cloth), "Tissus", 20, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Coussin5), "Divers", "Coussin Capitonné", 70.0, 90.0, typeof(Cloth), "Tissus", 20, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Coussin6), "Divers", "Coussin Coussin Carré et Lisse", 75.0, 95.0, typeof(Cloth), "Tissus", 20, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Foulard4), "Divers", "Cache - Visage", 75.0, 95.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Foulard2), "Divers", "Foulard épaule", 80.0, 100.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Cocarde), "Divers", "Cocarde", 40.0, 700.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BandagesPieds), "Divers", "Bandages Pieds", 40.0, 70.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BandagesBras), "Divers", "Bandages Bras", 40.0, 70.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BandagesTaille), "Divers", "Bandages Taille", 40.0, 70.0, typeof(Cloth), "Tissus", 10, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(ElvenQuiver), "Divers", "Petit Carquois", 40.0, 70.0, typeof(Leather), 1044462, 12, 1044463);
			index = AddCraft(typeof(Draps), "Divers", "Draps", 30.0, 60.0, typeof(Cloth), "Tissus", 25, "Vous n'avez pas assez de tissus.");
			AddCraftAction(index, CombineCloth);

			index = AddCraft(typeof(RideauBlanc), "Tapis / Rideaux", "Rideaux Blanc(flip)", 50.0, 70.0, typeof(Cloth), "Tissus", 30, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RideauRouge), "Tapis / Rideaux", "Rideaux Rouge(flip)", 50.0, 70.0, typeof(Cloth), "Tissus", 30, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(Voilage), "Tapis / Rideaux", "Voilage(flip)", 55.0, 75.0, typeof(Cloth), "Tissus", 30, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BlueDecorativeRugDeed), "Tapis / Rideaux", "Tapis décoratif bleu", 40.0, 70.0, typeof(Cloth), "Tissus", 50, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BlueFancyRugDeed), "Tapis / Rideaux", "Tapis huppé bleu", 40.0, 70.0, typeof(Cloth), "Tissus", 50, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(BluePlainRugDeed), "Tapis / Rideaux", "Tapis bleu", 40.0, 70.0, typeof(Cloth), "Tissus", 50, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(CinnamonFancyRugDeed), "Tapis / Rideaux", "Tapis Cannelle", 40.0, 70.0, typeof(Cloth), "Tissus", 50, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(GoldenDecorativeRugDeed), "Tapis / Rideaux", "Tapis Doré", 40.0, 70.0, typeof(Cloth), "Tissus", 50, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(PinkFancyRugDeed), "Tapis / Rideaux", "Tapis Huppé Rose", 50.0, 80.0, typeof(Cloth), "Tissus", 50, "Vous n'avez pas assez de tissus.");
			index = AddCraft(typeof(RedPlainRugDeed), "Tapis / Rideaux", "Tapis Rouge", 50.0, 80.0, typeof(Cloth), "Tissus", 50, "Vous n'avez pas assez de tissus.");
			//index = AddCraft(typeof(RoseRugAddonDeed), "Tapis / Rideaux", "Tapis Rose", 50.0, 80.0, typeof(Cloth), "Tissus", 50  ,"Vous n'avez pas assez de tissus.");


			// Set the overridable material
			SetSubRes(typeof(Leather), " Cuir Regulier");

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes(typeof(Leather), "Régulier", 0.0, "Vous ne savez pas travailler le cuir Regulier");
			AddSubRes(typeof(LupusLeather), "Lupus", 30.0, "Vous ne savez pas travailler le cuir Lupus");
			AddSubRes(typeof(ReptilienLeather), "Reptilien", 40.0, "Vous ne savez pas travailler le cuir Reptilien");
			AddSubRes(typeof(GeantLeather), "Geant", 50.0, "Vous ne savez pas travailler le cuir Geant");
			AddSubRes(typeof(OphidienLeather), "Ophidien", 60.0, "Vous ne savez pas travailler le cuir Ophidien");
			AddSubRes(typeof(ArachnideLeather), "Arachnide", 70.0, "Vous ne savez pas travailler le cuir Arachnide");
			AddSubRes(typeof(DragoniqueLeather), "Dragonique", 80.0, "Vous ne savez pas travailler le cuir Dragonique");
			AddSubRes(typeof(DemoniaqueLeather), "Demoniaque", 90.0, "Vous ne savez pas travailler le cuir Demoniaque");
			AddSubRes(typeof(AncienLeather), "Ancien", 95.0, "Vous ne savez pas travailler le cuir Ancien");

			MarkOption = true;
			Repair = true;
			CanEnhance = true;
			CanAlter = true;
		}

		private void CutUpCloth(Mobile m, CraftItem craftItem, ITool tool)
		{
			PlayCraftEffect(m);

			Timer.DelayCall(TimeSpan.FromSeconds(Delay), () =>
			{
				if (m.Backpack == null)
				{
					m.SendGump(new CraftGump(m, this, tool, null));
				}

				Dictionary<int, int> bolts = new Dictionary<int, int>();
				List<Item> toConsume = new List<Item>();
				object num = null;
				Container pack = m.Backpack;

				foreach (Item item in pack.Items)
				{
					if (item.GetType() == typeof(BoltOfCloth))
					{
						if (!bolts.ContainsKey(item.Hue))
						{
							toConsume.Add(item);
							bolts[item.Hue] = item.Amount;
						}
						else
						{
							toConsume.Add(item);
							bolts[item.Hue] += item.Amount;
						}
					}
				}

				if (bolts.Count == 0)
				{
					num = 1044253; // You don't have the components needed to make that.
				}
				else
				{
					foreach (Item item in toConsume)
					{
						item.Delete();
					}

					foreach (KeyValuePair<int, int> kvp in bolts)
					{
						UncutCloth cloth = new UncutCloth(kvp.Value * 50)
						{
							Hue = kvp.Key
						};

						DropItem(m, cloth, tool);
					}
				}

				if (tool != null)
				{
					tool.UsesRemaining--;

					if (tool.UsesRemaining <= 0 && !tool.Deleted)
					{
						tool.Delete();
						m.SendLocalizedMessage(1044038);
					}
					else
					{
						m.SendGump(new CraftGump(m, this, tool, num));
					}
				}

				ColUtility.Free(toConsume);
				bolts.Clear();
			});
		}

		private void CombineCloth(Mobile m, CraftItem craftItem, ITool tool)
		{
			PlayCraftEffect(m);

			Timer.DelayCall(TimeSpan.FromSeconds(Delay), () =>
			{
				if (m.Backpack == null)
				{
					m.SendGump(new CraftGump(m, this, tool, null));
				}

				Container pack = m.Backpack;

				Dictionary<int, int> cloth = new Dictionary<int, int>();
				List<Item> toConsume = new List<Item>();
				object num = null;

				foreach (Item item in pack.Items)
				{
					Type t = item.GetType();

					if (t == typeof(UncutCloth) || t == typeof(Cloth) || t == typeof(CutUpCloth))
					{
						if (!cloth.ContainsKey(item.Hue))
						{
							toConsume.Add(item);
							cloth[item.Hue] = item.Amount;
						}
						else
						{
							toConsume.Add(item);
							cloth[item.Hue] += item.Amount;
						}
					}
				}

				if (cloth.Count == 0)
				{
					num = 1044253; // You don't have the components needed to make that.
				}
				else
				{
					foreach (Item item in toConsume)
					{
						item.Delete();
					}

					foreach (KeyValuePair<int, int> kvp in cloth)
					{
						UncutCloth c = new UncutCloth(kvp.Value)
						{
							Hue = kvp.Key
						};

						DropItem(m, c, tool);
					}
				}

				if (tool != null)
				{
					tool.UsesRemaining--;

					if (tool.UsesRemaining <= 0 && !tool.Deleted)
					{
						tool.Delete();
						m.SendLocalizedMessage(1044038);
					}
					else
					{
						m.SendGump(new CraftGump(m, this, tool, num));
					}
				}

				ColUtility.Free(toConsume);
				cloth.Clear();
			});
		}

		private void DropItem(Mobile from, Item item, ITool tool)
		{
			if (tool is Item && ((Item)tool).Parent is Container)
			{
				Container cntnr = (Container)((Item)tool).Parent;

				if (!cntnr.TryDropItem(from, item, false))
				{
					if (cntnr != from.Backpack)
						from.AddToBackpack(item);
					else
						item.MoveToWorld(from.Location, from.Map);
				}
			}
			else
			{
				from.AddToBackpack(item);
			}
		}
	}
}
