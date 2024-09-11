using Server.Engines.Quests.Hag;
using Server.Items;
using System;
using System.Security.Policy;

namespace Server.Engines.Craft
{
	public enum AlchemyRecipes
	{
		BarrabHemolymphConcentrate = 900,
		JukariBurnPoiltice = 901,
		KurakAmbushersEssence = 902,
		BarakoDraftOfMight = 903,
		UraliTranceTonic = 904,
		SakkhraProphylaxisPotion = 905,


		//K2
		[Description("Potion de rafrai. supérieure")]
		SuperiorRefreshPotion = 70001,
		[Description("Potion de soin supérieure")]
		SuperiorHealPotion = 70002,
		[Description("Potion antidote supérieure")]
		SuperiorCurePotion = 70003,
		[Description("Potion d'agilité supérieure")]
		SuperiorAgilityPotion = 70004,
		[Description("Potion de force supérieure")]
		SuperiorStrengthPotion = 70005,
		[Description("Potion Invisibilité")]
		InvisibilityPotion = 70006,
		[Description("Potion de lien animal")]
		PetBondingPotion = 70007,
		[Description("Potion d'auto résurection")]
		AutoResPotion = 70008,
		[Description("Potion mana supérieure")]
		SuperiorManaPotion = 70009,
		[Description("Potion intelligence supérieure")]
		SuperiorIntelligencePotion = 70010,
		[Description("Potion de Keprish")]
		KeprishPotion = 70011,

		
	}

	public class DefAlchemy : CraftSystem
	{
		public override SkillName MainSkill => SkillName.Alchemy;

		// public override int GumpTitleNumber => 1044001;

		public override string GumpTitleString => "Alchimie";


		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if (m_CraftSystem == null)
					m_CraftSystem = new DefAlchemy();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin(CraftItem item)
		{
			return 0.0; // 0%
		}

		private DefAlchemy()
			: base(3, 4, 1.50)// base( 1, 1, 3.1 )
		{
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

		public override void PlayCraftEffect(Mobile from)
		{
			from.PlaySound(0x242);
		}

		private static readonly Type typeofPotion = typeof(BasePotion);

		public static bool IsPotion(Type type)
		{
			return typeofPotion.IsAssignableFrom(type);
		}

		public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
		{
			if (toolBroken)
				from.SendLocalizedMessage(1044038); // You have worn out your tool

			if (failed)
			{
				if (IsPotion(item.ItemType))
				{
					from.AddToBackpack(new Bottle());
					return 500287; // You fail to create a useful potion.
				}
				else
				{
					return 1044043; // You failed to create the item, and some of your materials are lost.
				}
			}
			else
			{
				from.PlaySound(0x240); // Sound of a filling bottle

				if (IsPotion(item.ItemType))
				{
					if (quality == -1)
						return 1048136; // You create the potion and pour it into a keg.
					else
						return 500279; // You pour the potion into a bottle...
				}
				else
				{
					return 1044154; // You create the item.
				}
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			index = AddCraft(typeof(LesserRefreshPotion), "Rafraichissement", "Potion de rafrai. mineure", 0.0, 25.0, typeof(BlackPearl), "Perle noire", 1, "Vous n'avez pas suffisament de perle noire");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(RefreshPotion), "Rafraichissement", "Potion de rafraichissement", 25.0, 50.0, typeof(BlackPearl), "Perle noire", 3, "Vous n'avez pas suffisament de perle noire");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(GreaterRefreshPotion), "Rafraichissement", "Potion de rafrai. majeure", 50.0, 75.0, typeof(BlackPearl), "Perle noire", 5, "Vous n'avez pas suffisament de perle noire");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(SuperiorRefreshPotion), "Rafraichissement", "Pot rafrai. supérieure", 75.0, 100.0, typeof(BlackPearl), "Perle noire", 7, "Vous n'avez pas suffisament de perle noire");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			AddRecipe(index, (int)AlchemyRecipes.SuperiorRefreshPotion);

			index = AddCraft(typeof(LesserHealPotion), "Soin", "Potion de soin mineure", 0.0, 25.0, typeof(Ginseng), "Ginseng", 1, "Vous n'avez pas suffisament de Ginseng");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(HealPotion), "Soin", "Potion de soin", 25.0, 50.0, typeof(Ginseng), "Ginseng", 3, "Vous n'avez pas suffisament de Ginseng");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(GreaterHealPotion), "Soin", "Potion de soin majeure", 50.0, 75.0, typeof(Ginseng), "Ginseng", 5, "Vous n'avez pas suffisament de Ginseng");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(SuperiorHealPotion), "Soin", "Pot. soin supérieure", 75.0, 100.0, typeof(Ginseng), "Ginseng", 7, "Vous n'avez pas suffisament de Ginseng");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			AddRecipe(index, (int)AlchemyRecipes.SuperiorHealPotion);

			index = AddCraft(typeof(LesserManaPotion), "Soin", "Potion de mana mineure", 0.0, 25.0, typeof(WhitePearl), "WhitePearl", 1, "Vous n'avez pas suffisament de WhitePearl");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(ManaPotion), "Soin", "Potion de mana", 25.0, 50.0, typeof(WhitePearl), "WhitePearl", 3, "Vous n'avez pas suffisament de WhitePearl");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(GreaterManaPotion), "Soin", "Potion de mana majeure", 50.0, 75.0, typeof(WhitePearl), "WhitePearl", 5, "Vous n'avez pas suffisament de WhitePearl");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			//	index = AddCraft(typeof(SuperiorManaPotion), "Soin", "Pot. mana supérieure", 75.0, 100.0, typeof(WhitePearl), "WhitePearl", 7, "Vous n'avez pas suffisament de WhitePearl");
			//	AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			//AddRecipe(index, (int)AlchemyRecipes.SuperiorManaPotion);


			index = AddCraft(typeof(LesserCurePotion), "Antidote", "Potion d'antidote mineure", 0.0, 25.0, typeof(Garlic), "Ail", 1, "Vous n'avez pas suffisament d'Ail");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(CurePotion), "Antidote", "Potion d'antidote", 25.0, 50.0, typeof(Garlic), "Ail", 3, "Vous n'avez pas suffisament d'Ail");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(GreaterCurePotion), "Antidote", "Potion d'antidote majeure", 50.0, 75.0, typeof(Garlic), "Ail", 5, "Vous n'avez pas suffisament d'Ail");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(SuperiorCurePotion), "Antidote", "Pot. antidote supérieure", 75.0, 100.0, typeof(Garlic), "Ail", 7, "Vous n'avez pas suffisament d'Ail");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			AddRecipe(index, (int)AlchemyRecipes.SuperiorCurePotion);

			index = AddCraft(typeof(UltimeCurePotion), "Antidote", "Pot. antidote Ultime", 90.0, 150.0, typeof(Garlic), "Ail", 20, "Vous n'avez pas suffisament d'Ail");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(LesserAgilityPotion), "Dextérité", "Potion de dextérité mineure", 0.0, 25.0, typeof(Bloodmoss), "Mousse de Sang", 1, "Vous n'avez pas suffisament de Mousse de sang");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(AgilityPotion), "Dextérité", "Potion de dextérité", 25.0, 50.0, typeof(Bloodmoss), "Mousse de Sang", 3, "Vous n'avez pas suffisament de Mousse de sang");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(GreaterAgilityPotion), "Dextérité", "Potion de dextérité majeure", 50.0, 75.0, typeof(Bloodmoss), "Mousse de Sang", 5, "Vous n'avez pas suffisament de Mousse de sang");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(SuperiorAgilityPotion), "Dextérité", "Pot. dex supérieure", 75.0, 100.0, typeof(Bloodmoss), "Mousse de Sang", 7, "Vous n'avez pas suffisament de Mousse de sang");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			AddRecipe(index, (int)AlchemyRecipes.SuperiorAgilityPotion);

			index = AddCraft(typeof(LesserStrengthPotion), "Force", "Potion de force mineure", 0.0, 25.0, typeof(MandrakeRoot), "Racine de Mandragore", 1, "Vous n'avez pas suffisament de Racine de Mandragore");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(StrengthPotion), "Force", "Potion de force", 25.0, 50.0, typeof(MandrakeRoot), "Racine de Mandragore", 3, "Vous n'avez pas suffisament de Racine de Mandragore");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(GreaterStrengthPotion), "Force", "Potion de force majeure", 50.0, 75.0, typeof(MandrakeRoot), "Racine de Mandragore", 5, "Vous n'avez pas suffisament de Racine de Mandragore");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(SuperiorStrengthPotion), "Force", "Pot. force supérieure", 75.0, 100.0, typeof(MandrakeRoot), "Racine de Mandragore", 7, "Vous n'avez pas suffisament de Racine de Mandragore");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			AddRecipe(index, (int)AlchemyRecipes.SuperiorStrengthPotion);


			index = AddCraft(typeof(LesserIntelligencePotion), "Intelligence", "Potion Intelligence mineure", 0.0, 25.0, typeof(BlackPearl), "Perle Noire", 1, "Vous n'avez pas suffisament de Perle Noire");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(IntelligencePotion), "Intelligence", "Potion Intelligence", 25.0, 50.0, typeof(BlackPearl), "Perle Noire", 3, "Vous n'avez pas suffisament de Perle Noire");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(GreaterIntelligencePotion), "Intelligence", "Potion Intel. majeure", 50.0, 75.0, typeof(BlackPearl), "Perle Noire", 5, "Vous n'avez pas suffisament de Perle Noire");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			//index = AddCraft(typeof(SuperiorIntelligencePotion), "Intelligence", "Pot. Intel. supérieure", 75.0, 100.0,typeof(BlackPearl), "Perle Noire", 7, "Vous n'avez pas suffisament de Perle Noire");
			//AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 3, "Vous n'avez pas suffisament de Poudre de Coquillages");
			//AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			//AddRecipe(index, (int)AlchemyRecipes.SuperiorIntelligencePotion);

			// Explosive
			index = AddCraft(typeof(LesserExplosionPotion), "Explosion", "Potion explosive mineure", 5.0, 55.0, typeof(SulfurousAsh), "Cendres sulfureuses", 3, "Vous n'avez pas assez de Cendres sulfureuses");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(ExplosionPotion), "Explosion", "Potion explosive", 35.0, 85.0, typeof(SulfurousAsh), "Cendres sulfureuses", 5, "Vous n'avez pas assez de Cendres sulfureuses");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(GreaterExplosionPotion), "Explosion", "Potion explosive majeure", 65.0, 115.0, typeof(SulfurousAsh), "Cendres sulfureuses", 10, "Vous n'avez pas assez de Cendres sulfureuses");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(ConflagrationPotion), "Explosion", "Potion incendière", 55.0, 105.0, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(GraveDust), "Poussière Blanche", 5, "Vous n'avez pas suffisament de Poussière Blanche");

			index = AddCraft(typeof(GreaterConflagrationPotion), "Explosion", "Potion incendière majeure", 70.0, 120.0, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(GraveDust), "Poussière Blanche", 10, "Vous n'avez pas suffisament de Poussière Blanche");

			index = AddCraft(typeof(SmokeBomb), "Autres", "Bombe de Fumée", 90.0, 120.0, typeof(Eggs), "Oeuf", 1, "Vous n'avez pas suffisament d'oeuf");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(Ginseng), "Ginseng", 3, "Vous n'avez pas suffisament de Ginseng");

			index = AddCraft(typeof(InvisibilityPotion), "Autres", "Potion d'invisibilité", 65.0, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(Bloodmoss), "Mousse de Sang", 4, "Vous n'avez pas suffisament de Mousse de sang");
			AddRes(index, typeof(Nightshade), "Belladone", 3, "Vous n'avez pas suffisament de Belladone");
			AddRecipe(index, (int)AlchemyRecipes.InvisibilityPotion);


			index = AddCraft(typeof(NightSightPotion), "Autres", "Potion de vision de nuit", -25.0, 25.0, typeof(SpidersSilk), "Soie d'araignée", 1, "Vous n'avez pas suffisament de Soie d'araignée");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
		AddRes(index, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");

			index = AddCraft(typeof(PetBondingPotion), "Autres", "Potion de lien animal", 70.0, 120.0, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			AddRes(index, typeof(PlumesSaliva), "Plume de saliva", 10, "Vous n'avez pas suffisament de plume de Saliva");
			AddRes(index, typeof(SangAnguille), "Sang Anguille", 5, "Vous n'avez pas suffisament de Sang Anguille");
			AddRes(index, typeof(GraisseSole), "Graisse de Sole", 5, "Vous n'avez pas suffisament de Graisse de Sole");
			AddRecipe(index, (int)AlchemyRecipes.PetBondingPotion);




			index = AddCraft(typeof(AutoResPotion), "Autres", "Potion d'auto résurrection", 70.0, 120.0, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(DragonBlood), "Sang de Dragon", 5, "Vous n'avez pas suffisament de Sang de Dragon");
			AddRes(index, typeof(MucusDemon), "Mucus de Démon", 5, "Vous n'avez pas suffisament de Mucus de Démon");
			AddRecipe(index, (int)AlchemyRecipes.AutoResPotion);



			index = AddCraft(typeof(JukariBurnPoiltice), "Autres", "Potion de résistances", 51.0, 151.0, typeof(Bottle), 1044529, 1, 500315);
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(SangAnguille), "Sang D'anguille", 5, "Vous n'avez pas suffisament de sang d'anguille");


			index = AddCraft(typeof(KurakAmbushersEssence), "Autres", "Essence de Kurak", 51.0, 151.0, typeof(Bottle), 1044529, 1, 500315);
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(DentAlligator), "Dent Alligator", 5, "Vous n'avez pas suffisament de dents d'alligator");


			index = AddCraft(typeof(EodonianPotion), "Autres", "Brassage de Barako", 51.0, 151.0, typeof(Bottle), 1044529, 1, 500315);
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(WhitePearl), "Perle Blanche", 5, "Vous n'avez pas suffisament de perle blanche");
			

			index = AddCraft(typeof(UraliTranceTonic), "Autres", "Tonic de Urali", 51.0, 151.0, typeof(Bottle), 1044529, 1, 500315);
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(OeufThon), "Oeuf de Thon", 5, "Vous n'avez pas suffisament d'oeuf de Thon");
			
			index = AddCraft(typeof(SakkhraProphylaxisPotion), "Autres", "Potion de Sakkhra", 51.0, 151.0, typeof(Bottle), 1044529, 1, 500315);
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(GraisseSole), "Graisse de Sole", 5, "Vous n'avez pas suffisament de graisse de sole");

			index = AddCraft(typeof(KeprishPotion), "Autres", "Potion Keprish", 85.0, 120.0, typeof(Bottle), "Bouteille Vide", 1, "Vous n'avez pas de Bouteille Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillage", 1, "Vous n'avez pas suffisament de poudre de coquillage");
			AddRes(index, typeof(Hellebore), "Hellebore", 5, "Vous n'avez pas suffisament de Hellebore");
			AddRecipe(index, (int)AlchemyRecipes.KeprishPotion);


			index = AddCraft(typeof(HairDye), "Teinture pour cheveux", "Teinture à Cheveux", 75.0, 100.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac vide");
			AddRes(index, typeof(Charcoal), "Charbon", 5, "Vous n'avez pas suffisament de charbon");



			#region Teintures

			#region Teintures OR
			index = AddCraft(typeof(OrSombreDyeTub), "Teintures", "Or Sombre", 25.0, 50.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(GoldIngot), "Lingot d'or", 2, "Vous n'avez pas suffisament de Lingot d'or");

			index = AddCraft(typeof(CitronAbrasifDyeTub), "Teintures", "Citron Abrasif", 50.0, 75.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(GoldIngot), "Lingot d'or", 2, "Vous n'avez pas suffisament de Lingot d'or");

			index = AddCraft(typeof(OrDouxDyeTub), "Teintures", "Or Doux", 65.0, 85.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(GoldIngot), "Lingot d'or", 2, "Vous n'avez pas suffisament de Lingot d'or");

			index = AddCraft(typeof(BeurreDouxDyeTub), "Teintures", "Beurre Doux", 80, 100.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(GoldIngot), "Lingot d'or", 2, "Vous n'avez pas suffisament de Lingot d'or");
			#endregion

			#region Teintures BRUNE
			index = AddCraft(typeof(CafeBruleDyeTub), "Teintures", "Cafe Brûlé", 25.0, 50.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(CoffeeBean), "Grain de Café", 10, "Vous n'avez pas suffisament de Grains de Café");

			index = AddCraft(typeof(RouilleDyeTub), "Teintures", "Rouille", 50.0, 75.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(CheveuxTroll), "Cheveux de Troll", 5, "Vous n'avez pas suffisament de Cheveux de Troll");

			index = AddCraft(typeof(BronzeDyeTub), "Teintures", "Bronze", 65.0, 85.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(BronzeIngot), "Lingot de Bronze", 2, "Vous n'avez pas suffisament de Lingot de Bronze");

			index = AddCraft(typeof(CuivreDyeTub), "Teintures", "Cuivre", 80, 100.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(CopperIngot), "Lingot de Cuivre", 2, "Vous n'avez pas suffisament de Lingot de Cuivre");
			#endregion

			#region Teintures Rouge
			index = AddCraft(typeof(RougeSanguinDyeTub), "Teintures", "Rouge Sanguin", 25.0, 50.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(PattesLapin), "Patte de Lapins", 5, "Vous n'avez pas suffisament de Pattes de Lapin");

			index = AddCraft(typeof(BordeauDyeTub), "Teintures", "Bordeau", 50.0, 75.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(BaseBeverage), 1022503, 1, 1044253);
			SetBeverageType(index, BeverageType.Wine);

			index = AddCraft(typeof(CorailDyeTub), "Teintures", "Corail", 65.0, 85.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DentRequin), "Dent de Requin", 2, "Vous n'avez pas suffisament de Dent de Requin");

			index = AddCraft(typeof(RougeObscureDyeTub), "Teintures", "Rouge Obscure", 80, 100.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(CerveauSpectre), "Cerveau de Spectre", 2, "Vous n'avez pas suffisament de Cerveau de Spectre");
			#endregion

			#region Teintures Bleu
			index = AddCraft(typeof(BleuGlacierDyeTub), "Teintures", "Bleu Glacier", 25.0, 50.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(CheveuxGeant), "Cheveux de Géant", 5, "Vous n'avez pas suffisament de Cheveux de Géant");

			index = AddCraft(typeof(BleuProfondDyeTub), "Teintures", "Bleu Profond", 50.0, 75.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Blueberry), "Bleuets", 10, "Vous n'avez pas suffisament de Bleuets");


			index = AddCraft(typeof(BleuCielDyeTub), "Teintures", "Bleu Ciel", 65.0, 85.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(CerveauLiche), "Cerveau de Liche", 2, "Vous n'avez pas suffisament de Cerveaux de Liche");

			index = AddCraft(typeof(BleuSombreDyeTub), "Teintures", "Bleu Sombre", 80, 100.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(MytherilIngot), "Lingot de Mytheril", 2, "Vous n'avez pas suffisament de Lingot de Mytheril");
			#endregion

			#region Teintures Verte
			index = AddCraft(typeof(VertSombreDyeTub), "Teintures", "Vert Sombre", 25.0, 50.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(EcorceArbreGeant), "Ecorce d'arbre géant", 5, "Vous n'avez pas suffisament d'écorce d'arbre géant");

			index = AddCraft(typeof(VertOliveDyeTub), "Teintures", "Vert Olive", 50.0, 75.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Peas), "Pois", 10, "Vous n'avez pas suffisament de Pois");


			index = AddCraft(typeof(VertPrintanierDyeTub), "Teintures", "Vert Printanier", 65.0, 85.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Fougere), "Fougere", 2, "Vous n'avez pas suffisament de Fougere");

			index = AddCraft(typeof(VertIridescentDyeTub), "Teintures", "Vert Iridescent", 80, 100.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(VeninTarenlune), "Venin de Tarenlune", 2, "Vous n'avez pas suffisament de Venin de Tarenlune");
			#endregion

			#region Teintures TURQUOISE 
			index = AddCraft(typeof(TurquoisePlumePaon), "Teintures", "Plume de Paon", 25.0, 50.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(EcorceArbreGeant), "Ecorce d'arbre Géant", 5, "Vous n'avez pas suffisament d'écorce d'arbre Géant");

			index = AddCraft(typeof(TurquoiseDyeTub), "Teintures", "Turquoise", 50.0, 75.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(VeninAraigneeGeante), "Venin d'araignée Géante", 5, "Vous n'avez pas suffisament de Venin d'araignée géante");


			index = AddCraft(typeof(EcaillePoissonDyeTub), "Teintures", "Ecaille de Poisson", 65.0, 85.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(RawFishSteak), "Morceaux de Poisson cru", 2, "Vous n'avez pas suffisament de Poisson cru");

			index = AddCraft(typeof(AquaMarineDyeTub), "Teintures", "Aqua Marine", 80, 100.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Sapphire), "Un Saphir", 2, "Vous n'avez pas suffisament de Saphir");
			#endregion

			#region Teintures Mauve 
			index = AddCraft(typeof(MauveIndigoDyeTub), "Teintures", "Mauve Indigo", 25.0, 50.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(PoilsLoup), "Poils de Loup", 5, "Vous n'avez pas suffisament de poils de Loup");

			index = AddCraft(typeof(RosePerleDyeTub), "Teintures", "Rose Perle", 50.0, 75.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(RedRose), "Rose Rouge", 5, "Vous n'avez pas suffisament de Roses Rouge");


			index = AddCraft(typeof(PruneDyeTub), "Teintures", "Prune", 65.0, 85.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(PattesPanthere), "Pattes de Panthère", 2, "Vous n'avez pas suffisament de Patte de Panthère");

			index = AddCraft(typeof(MauveVelourDyeTub), "Teintures", "Mauve Velour", 80, 100.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Amethyste), "Une Améthyste", 2, "Vous n'avez pas suffisament d'améthyste");
			#endregion

			#region Teintures NEUTRE
			index = AddCraft(typeof(GrisAntraciteDyeTub), "Teintures", "Gris Antracite", 25.0, 50.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(PlumesHarpie), "Plumes de Harpie", 5, "Vous n'avez pas suffisament de Plumes de Harpie");

			index = AddCraft(typeof(GrisArgenteDyeTub), "Teintures", "Gris Argenté", 50.0, 75.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(OeufPierre), "Oeuf de Pierre", 2, "Vous n'avez pas suffisament d'oeuf de Pierre");


			index = AddCraft(typeof(NoirDyeTub), "Teintures", "Noir", 65.0, 85.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(Charcoal), "Charbon", 5, "Vous n'avez pas suffisament de Charbon");

			index = AddCraft(typeof(BlancDyeTub), "Teintures", "Blanc", 80, 100.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un Bac Vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(PlumesAigle), "Plumes d'aigles", 10, "Vous n'avez pas suffisament de plumes d'aigle");
			#endregion
			#endregion

			  #region Parfums
            index = AddCraft(typeof(PerfumGrisPotion), "Parfums ",                      "Sel de mer ",                  50.0, 60.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
            AddRes(index, typeof(DentDeLion), "Dent-de-lion", 1, "Manque : Dent-de-lion");
           AddRes(index, typeof(Sage), "Sauge", 1, "Il Vous manque :  Sauge");

            index = AddCraft(typeof(PerfumKakiPotion), "Parfums ",                      "Fromage vieillit ",            25.0, 45.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
			AddRes(index, typeof(Fougere), "Fougère", 1, "Il Vous manque : Fougere");
			AddRes(index, typeof(Anise), "Anis", 1, "Il Vous manque : Anis");

			index = AddCraft(typeof(PerfumBrunPotion), "Parfums ",                      "Vieux musk ",                  30.0, 50.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
			AddRes(index, typeof(Saffron), "Safran", 1, "Il Vous manque :  Safran l'épice.");
			AddRes(index, typeof(Myrrh), "gomme d'épinette", 1, "Il Vous manque :  gomme d'épinette");

			index = AddCraft(typeof(PerfumJaunePotion), "Parfums ",                     "Agrume en folie ",             70.0, 85.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
			AddRes(index, typeof(Tulipe), "Tulipe", 1, "Il Vous manque :  Tulipe");
			AddRes(index, typeof(Saffron), "Safran", 1, "Il Vous manque :  Safran l'épice.");


			index = AddCraft(typeof(PerfumBleuClairPotion), "Parfums ",                 "Brise d’océan ",               80.0, 90.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
			AddRes(index, typeof(RoseSauvage), "Rose Sauvage", 1, "Il Vous manque :  Rose Sauvage");
			AddRes(index, typeof(Sage), "Sauge", 1, "Il Vous manque :  Sauge");

            index = AddCraft(typeof(PerfumMauvePotion), "Parfums ",                     "Souffle magique ",             85.3, 95.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
            AddRes(index, typeof(Lilas), "Lila", 1, "Il Vous manque :  Lila");
			AddRes(index, typeof(Basil), "Basilic", 1, "Il Vous manque : Basilic");

			index = AddCraft(typeof(PerfumRougevifPotion), "Parfums ",                  "Désir et passion ",            86.7, 95.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
			AddRes(index, typeof(Tulipe), "Tulipe", 3, "Il Vous manque :  Tulipe");
			AddRes(index, typeof(Lavender), "Lavande", 1, "Il Vous manque :  Lavande");


			index = AddCraft(typeof(PerfumRosePotion), "Parfums ",                      "Synergie féérique ",           84.3, 95.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
            AddRes(index, typeof(Lys), "Lys", 1, "Il Vous manque :  Lys");
		    AddRes(index, typeof(Peppercorn), "grain de poivre", 1, "Il Vous manque :  grain de poivre");

			index = AddCraft(typeof(PerfumTurquoisePotion), "Parfums ",                 "Menthe polaire ",              68.3, 80.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
            AddRes(index, typeof(Givrelle), "Givrelle", 1, "Il Vous manque :  Givrelle");
			AddRes(index, typeof(Acacia), "Acacia", 1, "Il Vous manque :  Acacia");

			index = AddCraft(typeof(PerfumOrangePotion), "Parfums ",                    "Effervescence fruitée ",       73.2, 85.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
			AddRes(index, typeof(Camomille), "Camomille", 1, "Il Vous manque : Camomille");
			AddRes(index, typeof(Orris), "racine d'Orris", 1, "Il Vous manque :  racine d'Orris");


			index = AddCraft(typeof(PerfumMarinePotion), "Parfums ",                    "Frisson obscure ",             70.0, 90.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
            AddRes(index, typeof(Indigo), "Indigo", 1, "Il Vous manque :  Indigo");
            AddRes(index, typeof(Sandelwood), "Bois de santal", 1, "Il Vous manque :  Bois de santal");

            index = AddCraft(typeof(PerfumVioletPotion), "Parfums ",                    "Escapade florale ",            75.0, 95.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
            AddRes(index, typeof(RoseTremiere), "Rose trémière", 1, "Il Vous manque :  Rose trémière");
			AddRes(index, typeof(Mint), "Menthe", 1, "Il Vous manque :  Menthe");

			index = AddCraft(typeof(PerfumVertpommePotion), "Parfums ",                 "Regénérescence ",              80.0, 100.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
            AddRes(index, typeof(Dahlia), "Dahlia", 1, "Il Vous manque :  Dahlia");
	        AddRes(index, typeof(Rosemary), "Romarin", 1, "Il Vous manque :  Romarin");

			index = AddCraft(typeof(PerfumRougevinPotion), "Parfums ",                  "Volupté charnelle ",           85.0, 105.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
            AddRes(index, typeof(DentDeLion), "Dent-de-lion", 3, "Il Vous manque :  Dent-de-lion");
             AddRes(index, typeof(Thyme), "Thym", 1, "Il Vous manque :  Thym");

            index = AddCraft(typeof(PerfumOcrePotion), "Parfums ",                      "Opulence ",                    90.0, 110.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
            AddRes(index, typeof(Cramelia), "Cramélia", 1, "Il Vous manque :  Cramélia");
            AddRes(index, typeof(Valerian), "Valériane", 1, "Il Vous manque :  Valériane");

            index = AddCraft(typeof(PerfumVertforêtPotion), "Parfums ",                 "Bois de santal ",              95.0, 115.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
            AddRes(index, typeof(GeuleDeDragon), "Gueule de dragon", 1, "Il Vous manque :  Gueule de dragon");
			AddRes(index, typeof(Olive), "Olive", 1, "Il Vous manque :  Olive");


			index = AddCraft(typeof(PerfumNoirPotion), "Parfums ",                      "La fin des temps ",            99.0, 119.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
            AddRes(index, typeof(Ombrella), "Ombrella", 1, "Il Vous manque :  Ombrella");
            AddRes(index, typeof(TribalBerry), "baie tribale", 1, "Il Vous manque :  baie tribale");

			index = AddCraft(typeof(PerfumRemoval), "Parfums ", "Potion de retrait des parfums", 51.4, 70.0, typeof(Bottle),"Bouteille vide ", 1, "Il Vous manque :  Une Bouteille Vide");
			AddRes(index, typeof(PlumesSaliva), "Plume de saliva", 3, "Vous n'avez pas suffisament de plumes de Saliva");
		    AddRes(index, typeof(DentDeLion), "Dent-de-lion", 3, "Il Vous manque :  Dent-de-lion");
          	AddRes(index, typeof(Fougere), "Fougère", 3, "Il Vous manque : Fougere");
			#endregion
			#region Pigments

			// Bleu
			index = AddCraft(typeof(BleuOceanPigment), "Pigments", "Bleu océan", 45, 95.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(GolemCendreEau), "Cendre élémentaire d'eau", 5, "Il vous faut de la cendre élémentaire d'eau");

			index = AddCraft(typeof(BleuFruitePigment), "Pigments", "Bleu fruité", 55, 105.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Blackberry), "Mûres", 5, "Il vous faut des Mûres");

			index = AddCraft(typeof(BleuAzurPigment), "Pigments", "Bleu azur", 65, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(GolemCendreGlace), "Cendre élémentaire de glace", 5, "Il vous faut de la cendre élémentaire de glace");

			index = AddCraft(typeof(BleuCobaltPigment), "Pigments", "Bleu Cobalt", 75, 125.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(GlaciasIngot), "Lingot de Glacias", 5, "Il vous faut des lingots de glacias");

			index = AddCraft(typeof(BleuElectriquePigment), "Pigments", "Bleu électrique", 85, 135.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(SaphirEtoile), "Saphir étoilé", 5, "Il vous faut des saphirs étoilés");

			// Vert
			index = AddCraft(typeof(VertForestierPigment), "Pigments", "Vert forestier", 45, 95.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(CheneBoard), "Planche de chêne", 5, "Il vous faut des planches de chêne");

			index = AddCraft(typeof(VertMoussePigment), "Pigments", "Vert mousse", 55, 105.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(CedreBoard), "Planche de cèdre", 5, "Il vous faut des planches de cèdre");

			index = AddCraft(typeof(VertJadePigment), "Pigments", "Vert jade", 65, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(DurianIngot), "Lingot de Durian", 5, "Il vous faut du durian");

			index = AddCraft(typeof(VertBouteillePigment), "Pigments", "Vert Bouteille", 75, 125.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(GolemCendrePoison), "Cendre élémentaire de poison", 5, "Il vous faut de la cendre élémentaire de poison");

			index = AddCraft(typeof(VertLimePigment), "Pigments", "Vert lime", 85, 135.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Emeraude), "Émeraude", 5, "Il vous faut des émeraudes");

			// Jaune
			index = AddCraft(typeof(JauneImperialPigment), "Pigments", "Jaune impérial", 45, 95.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Lemon), "Citron", 5, "Il vous faut des citrons");

			index = AddCraft(typeof(JauneSablePigment), "Pigments", "Jaune sable", 55, 105.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(DentDeLion), "Dent de lion", 5, "Il vous faut des dents de lion");

			index = AddCraft(typeof(JauneMaisPigment), "Pigments", "Jaune maïs", 65, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Corn), "Maïs", 5, "Il vous faut du maïs");

			index = AddCraft(typeof(JauneCanariPigment), "Pigments", "Jaune canari", 75, 125.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Citrine), "Citrine", 5, "Il vous faut des citrines");

			index = AddCraft(typeof(JauneDOrPigment), "Pigments", "Jaune d'or", 85, 135.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Gold), "Pièces d'or", 500, "Il vous faut 500 pièces d'or");

			// Orange
			index = AddCraft(typeof(OrangeCendrePigment), "Pigments", "Orange cendré", 45, 95.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(GolemCendreFeu), "Cendre élémentaire de feu", 5, "Il vous faut de la cendre élémentaire de feu");

			index = AddCraft(typeof(OrangeRustiquePigment), "Pigments", "Orange rustique", 55, 105.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Carrot), "Carotte", 5, "Il vous faut des carottes");

			index = AddCraft(typeof(OrangeEpicePigment), "Pigments", "Orange épicé", 65, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Safran), "Safran (La Fleur)", 5, "Il vous faut du safran (La Fleur)");

			index = AddCraft(typeof(OrangeSanguinePigment), "Pigments", "Orange sanguine", 75, 125.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(GeuleDeDragon), "Gueule de dragon", 5, "Il vous faut des gueules de dragon");

			index = AddCraft(typeof(OrangeBrulePigment), "Pigments", "Orange brûlé", 85, 135.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Ambre), "Ambre", 5, "Il vous faut de l'ambre");

			// Rouge
			index = AddCraft(typeof(RougeBriquePigment), "Pigments", "Rouge brique", 45, 95.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Camomille), "Camomille", 5, "Il vous faut de la camomille");

			index = AddCraft(typeof(RougeCerisePigment), "Pigments", "Rouge cerise", 55, 105.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Beet), "Betterave", 5, "Il vous faut des betteraves");

			index = AddCraft(typeof(RougeGrenatPigment), "Pigments", "Rouge grenat", 65, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Tulipe), "Tulipe", 5, "Il vous faut des tulipes");

			index = AddCraft(typeof(RougeAntiquePigment), "Pigments", "Rouge antique", 75, 125.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(DemoniaqueBone), "Os démoniaque", 5, "Il vous faut des os démoniaques");

			index = AddCraft(typeof(RougeEcarlatePigment), "Pigments", "Rouge écarlate", 85, 135.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Rubis), "Rubis", 5, "Il vous faut des rubis");

			// Mauve
			index = AddCraft(typeof(MauveLavandePigment), "Pigments", "Mauve lavande", 45, 95.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Lilas), "Lilas", 5, "Il vous faut des lilas");

			index = AddCraft(typeof(MauvePolairePigment), "Pigments", "Mauve polaire", 55, 105.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Ombrella), "Ombrella", 5, "Il vous faut des ombrellas");

			index = AddCraft(typeof(MauveGlacePigment), "Pigments", "Mauve glacé", 65, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(BorealeIngot), "Boréal", 5, "Il vous faut du boréal");

			index = AddCraft(typeof(VieuxMauvePigment), "Pigments", "Vieux mauve", 75, 125.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Avocado), "Avocat", 5, "Il vous faut des avocats");

			index = AddCraft(typeof(MauveIntensePigment), "Pigments", "Mauve intense", 85, 135.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Indigo), "Indigo", 5, "Il vous faut de l'indigo");

			// Rose
			index = AddCraft(typeof(RoseThePigment), "Pigments", "Rose thé", 45, 95.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(RoseSauvage), "Rose sauvage", 5, "Il vous faut des roses sauvages");

			index = AddCraft(typeof(RoseTremierePigment), "Pigments", "Rose trémière", 55, 105.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(RoseTremiere), "Rose trémière", 5, "Il vous faut des roses trémières");

			index = AddCraft(typeof(RoseFoncePigment), "Pigments", "Rose foncé", 65, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Cranberry), "Canneberge", 5, "Il vous faut des canneberges");

			index = AddCraft(typeof(RoseTenebreuxPigment), "Pigments", "Rose ténébreux", 75, 125.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Cramelia), "Cramelia", 5, "Il vous faut des cramelias");

			index = AddCraft(typeof(FuchsiaPigment), "Pigments", "Fuchsia", 85, 135.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Dahlia), "Dahlia", 5, "Il vous faut des dahlias");

			// Brun/Beige
			index = AddCraft(typeof(BrunTaupePigment), "Pigments", "Brun taupe", 45, 95.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(BlackRose2), "Rose noire", 5, "Il vous faut des roses noires");

			index = AddCraft(typeof(BeigeNaturelPigment), "Pigments", "Beige naturel", 55, 105.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(ErableBoard), "Planche d'érable", 5, "Il vous faut des planches d'érable");

			index = AddCraft(typeof(BeigeChampagnePigment), "Pigments", "Beige champagne", 65, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(KhandariumIngot), "Lingot de Khandarium", 5, "Il vous faut du khandarium");

			index = AddCraft(typeof(BeigePoussiereuxPigment), "Pigments", "Beige poussiéreux", 75, 125.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(AbyssiumIngot), " Lingot d'Abyssium", 5, "Il vous faut de l'abyssium");

			index = AddCraft(typeof(BrunDeSiennePigment), "Pigments", "Brun de sienne", 85, 135.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(GolemCendreTerre), "Cendre élémentaire de terre", 5, "Il vous faut de la cendre élémentaire de terre");

			// Noir/Blanc
			index = AddCraft(typeof(BlancVieillitPigment), "Pigments", "Blanc vieillit", 45, 95.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillage");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(LithiarIngot), "Lingot de Lithiar", 5, "Il vous faut des lingots de Lithiar");


			// Noir/Blanc (suite)
			index = AddCraft(typeof(IvoirePigment), "Pigments", "Ivoire", 55, 105.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(DentAlligator), "Dent d'alligator", 5, "Il vous faut des dents d'alligator");

			index = AddCraft(typeof(ChauxPigment), "Pigments", "Chaux", 65, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Tourmaline), "Tourmaline", 5, "Il vous faut des tourmalines");

			index = AddCraft(typeof(NeigeViolaceePigment), "Pigments", "Neige violacée", 65, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Givrelle), "Givrelle", 5, "Il vous faut des givrelles");

			index = AddCraft(typeof(AlbatrePigment), "Pigments", "Albâtre", 75, 125.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(ArgentIngot), "Lingot d'argent", 5, "Il vous faut des lingots d'argent");

			index = AddCraft(typeof(BlancPerlePigment), "Pigments", "Blanc perle", 85, 135.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Lys), "Lys", 5, "Il vous faut des lys");

			index = AddCraft(typeof(NoirPrunePigment), "Pigments", "Noir prune", 85, 135.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(EbeneBoard), "Planche d'ébène", 5, "Il vous faut des planches d'ébène");

			// Bicolor
			index = AddCraft(typeof(TerreDeSiennePigment), "Pigments", "Terre de sienne", 45, 95.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Ginseng), "Ginseng", 5, "Il vous faut du ginseng");

			index = AddCraft(typeof(AntiquePigment), "Pigments", "Antique", 55, 105.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(Fougere), "Fougère", 5, "Il vous faut des fougères");

			index = AddCraft(typeof(ChartreusePigment), "Pigments", "Chartreuse", 65, 115.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(VeninAraigneeNoire), "Venin d'araignée noire", 5, "Il vous faut du venin d'araignée noire");

			index = AddCraft(typeof(FinDuMondePigment), "Pigments", "Fin du monde", 75, 125.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(GolemCendreFeu), "Cendre volcanique", 5, "Il vous faut de la cendre volcanique");

			index = AddCraft(typeof(AuroreBorealePigment), "Pigments", "Aurore boréale", 85, 135.0, typeof(Bottle), "Bouteille Vide", 1, "Il vous faut une bouteille vide");
			AddRes(index, typeof(PoudreCoquillages), "Poudre de Coquillages", 5, "Vous n'avez pas suffisament de Poudre de Coquillages");
			AddRes(index, typeof(DyeTub), "Bac de Teinture", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(BlackPearl), "Perle noire", 5, "Il vous faut des perles noires");

			#endregion
			Pratiquer = true;
		}
	}
}
