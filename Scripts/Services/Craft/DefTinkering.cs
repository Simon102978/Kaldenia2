using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using System;
using Server.Services.BasketWeaving.Baskets;
using Xanthos.ShrinkSystem;
using Server.Custom;
using static Server.Custom.GolemCrystal;
using Discord;
using Server.Custom.Packaging.Packages;
using Server.Engines.Chat;
using System.Runtime.ConstrainedExecution;
using System.Linq;

namespace Server.Engines.Craft
{
	public enum TinkerRecipes
	{
		InvisibilityPotion = 400,
		DarkglowPotion = 401,
		ParasiticPotion = 402,

		EssenceOfBattle = 450,
		PendantOfTheMagi = 451,
		ResilientBracer = 452,
		ScrappersCompendium = 453,
		HoveringWisp = 454, // Removed at OSI Publish 103

		KotlPowerCore = 455,

		// doom
		BraceletOfPrimalConsumption = 456,
		DrSpectorLenses = 457,
		KotlAutomatonHead = 458,

		WeatheredBronzeArcherSculpture = 459,
		WeatheredBronzeFairySculpture = 460,
		WeatheredBronzeGlobeSculpture = 461,
		WeatheredBronzeManOnABench = 462,

		KrampusMinionEarrings = 463,
		EnchantedPicnicBasket = 464,

		Telescope = 465,

		BarbedWhip = 466,
		SpikedWhip = 467,
		BladedWhip = 468,


		//K2

		[Description("épaulette Dorée")]
		EpauletteDoree = 10001,

		[Description("Diadème Feuille d'Or")]
		DiademeFeuilleOr = 10002,

		[Description("Tiare")]
		Tiare = 10003,

		[Description("Menottes Dorées")]
		MenotteDoree = 10004,

		[Description("Chandelier Élégant")]
		TerMurStyleCandelabra = 10005,


	}

	public class DefTinkering : CraftSystem
	{
		#region Mondain's Legacy
		public override CraftECA ECA => CraftECA.ChanceMinusSixtyToFourtyFive;
		#endregion

		public override SkillName MainSkill => SkillName.Tinkering;

		//   public override int GumpTitleNumber => 1044007;

		public override string GumpTitleString => "Bricolage";

		private static CraftSystem m_CraftSystem;
		private CraftSystem craftSystem;
		private ITool tool;

		public static CraftSystem CraftSystem
		{
			get
			{
				if (m_CraftSystem == null)
					m_CraftSystem = new DefTinkering();

				return m_CraftSystem;
			}
		}

		private DefTinkering()
			: base(3, 4, 1.50)// base( 1, 1, 3.0 )
		{
			craftSystem = this;
			tool = null; 
		}

		private void AddBeverageRes(int index, BeverageType beverageType, int quantity, string message)
		{
			AddRes(index, typeof(Pitcher), $"Pichet de {GetBeverageName(beverageType)}", 1, message);

			CraftItem craftItem = CraftItems.GetAt(index);
			craftItem.RequiredBeverage = beverageType;

			// Ajouter une propriété pour la quantité requise si elle n'existe pas déjà
			if (!craftItem.GetType().GetProperties().Any(p => p.Name == "RequiredBeverageQuantity"))
			{
				throw new InvalidOperationException("La classe CraftItem doit avoir une propriété RequiredBeverageQuantity.");
			}
			craftItem.GetType().GetProperty("RequiredBeverageQuantity").SetValue(craftItem, quantity);
		}

		private string GetBeverageName(BeverageType type)
		{
			switch (type)
			{
				case BeverageType.Milk: return "lait";
				case BeverageType.Water: return "eau";
				// Ajoutez d'autres types de boissons ici
				default: return type.ToString().ToLower();
			}
		}

		public override double GetChanceAtMin(CraftItem item)
		{
			if (item.NameNumber == 1044258 || item.NameNumber == 1046445) // potion keg 
				return 0.5; // 50%

			return 0.0; // 0%
		}

	
		public override int CanCraft(Mobile from, ITool tool, Type itemType)
		{
			int num = 0;

			if (tool == null || tool.Deleted || tool.UsesRemaining <= 0)
				return 1044038; // You have worn out your tool!
			else if (!tool.CheckAccessible(from, ref num))
				return num; // The tool must be on your person to use.
			else if (itemType == typeof(ModifiedClockworkAssembly) && !(from is PlayerMobile && ((PlayerMobile)from).MechanicalLife))
				return 1113034; // You haven't read the Mechanical Life Manual. Talking to Sutek might help!

			return 0;
		}

		private static readonly Type[] m_TinkerColorables = new Type[]
		{
			typeof(ForkLeft), typeof(ForkRight),
			typeof(SpoonLeft), typeof(SpoonRight),
			typeof(KnifeLeft), typeof(KnifeRight),
			typeof(Plate), typeof(Eventail),
			typeof(Goblet), typeof(PewterMug),
			typeof(KeyRing), typeof(FoodPlate),
			typeof(Candelabra), typeof(Scales),
			typeof(Key), typeof(Globe), typeof(Eventail),
			typeof(Spyglass), typeof(Lantern),
			typeof(HeatingStand), typeof(BroadcastCrystal), typeof(TerMurStyleCandelabra),
			typeof(GorgonLense), typeof(MedusaLightScales), typeof(MedusaDarkScales), typeof(RedScales),
			typeof(BlueScales), typeof(BlackScales), typeof(GreenScales), typeof(YellowScales), typeof(WhiteScales),
			typeof(PlantPigment), typeof(Kindling), typeof(DryReeds), typeof(PlantClippings), typeof(Bracelet1), typeof(Earrings1), typeof(Ring1), typeof(Necklace1),

			typeof(KotlAutomatonHead)
		};

		public override bool RetainsColorFrom(CraftItem item, Type type)
		{
			if (type == typeof(CrystalDust))
				return false;


			bool contains = false;
			type = item.ItemType;

			for (int i = 0; !contains && i < m_TinkerColorables.Length; ++i)
				contains = (m_TinkerColorables[i] == type);

			if (!contains && !type.IsSubclassOf(typeof(BaseIngot)))
				return false;

			if (type.IsSubclassOf(typeof(BaseBoard)))
				return true;

			return contains;
		}

		public override void PlayCraftEffect(Mobile from)
		{
			from.PlaySound(0x23B);
		}
		private static void SetItemAttribute(int index, GolemCrystal.CrystalType type)
		{
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

			#region Outils
			index = AddCraft(typeof(Scissors), "Outils", "Ciseaux", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(EmptyAleBottle), "Outils", "Bouteille de Bière Vide", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(SewingKit), "Outils", "Kit de couture (Tissus)", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(BoneSewingKit), "Outils", "Kit de couture (Os)", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Pinceaux), "Outils", "Pinceaux", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Blowpipe), "Outils", "Soufflage de verre", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(OutilsJardin), "Outils", "Outils de Jardinage", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(GolemSpiritWand), "Outils", "Baguette des Esprits", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(OutilDeTanneur), "Outils", "Outil de Tanner", 30.0, 60.0, typeof(PalmierBoard), "Planches de Palmier", 5, "Vous n'avez pas suffisament de planche de palmier");
			SetUseSubRes2(index, true);



			index = AddCraft(typeof(ToileVierge), "Outils", "Toile Vierge (Peinture)", 10.0, 50.0, typeof(PalmierBoard), "Planche de Palmier", 5, "Vous n'avez pas suffisament de planche de palmier");
			SetUseSubRes2(index, true);


			index = AddCraft(typeof(MalletAndChisel), "Outils", "Maillet et Ciseau (Pierre)", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(LeatherSewingKit), "Outils", "Kit de couture (Cuir)", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Hatchet), "Outils", "Hachette", 20.0, 40.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Shovel), "Outils", "Pelle", 20.0, 40.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Pickaxe), "Outils", "Pioche", 20.0, 40.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(TinkerTools), "Outils", "Trousse de Bricolage", 15.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(SmithyHammer), "Outils", "Marteau de forgeron", 15.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Saw), "Outils", "Scie", 15.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(RollingPin), "Outils", "Rouleau à pâte", 15.0, 35.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			SetUseSubRes2(index, true);
			index = AddCraft(typeof(Skillet), "Outils", "Poêlon", 15.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(FlourSifter), "Outils", "Tamis à farine", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(MortarPestle), "Outils", "Mortier et pilon", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(MortarPestlePoisoning), "Outils", "Mortier et pilon (Poison)", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(FletcherTools), "Outils", "Outil fabrication d'arc", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(ScribesPen), "Outils", "Plume d'écriture", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(MapmakersPen), "Outils", "Plume de cartographe", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");

			//index = AddCraft(typeof(SpellsPen), "Outils", "Cristal de compétences", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			//index = AddCraft(typeof(SoulsPen), "Outils", "Plume de reviviscence", 50.0, 75.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Lockpick), "Outils", "Crochets", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(BarberScissors), "Outils", "Ciseaux de Barbier", 10.0, 70.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Ecraseur), "Outils", "Écraseur", 15.0, 30.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(BeerBreweringTools), "Outils", "Outil fabrication de bière", 35.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Recycleur), "Outils", "Recycleur", 35.0, 55.0, typeof(IronIngot), "Lingot de fer", 8, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(BrewersTools), "Outils", "Outil Brasseur", 30.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(BreweryLabelMaker), "Outils", "Marqueur de bière", 30.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(GrapevinePlacementTool), "Outils", "Outil placement de vignes", 30.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(GrinderExp), "Outils", "Broyeur à café", 05.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(WaxCraftingPot), "Outils", "Travail de la cire", 30.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(apiWaxProcessingPot), "Outils", "Outil de transformation pour la cire", 30.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(JuicersTools), "Outils", "Fabrication de Jus", 30.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(VinyardLabelMaker), "Outils", "Marqueur de vin", 30.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(WinecraftersTools), "Outils", "Outil fabrication de vin", 30.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Boline), "Outils", "Serpe", 15.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(DisguiseKit), "Outils",  "Trousse à déguisement", 50.0, 60.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(RuneChisel), "Outils", "Ciseau runique", 60.0, 90.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");

			#endregion

			#region Anneaux
			AddCraft(typeof(GoldRing), "Anneaux", "Anneau Simple", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer cet anneau.");


			AddCraft(typeof(Ring1), "Anneaux", "Anneau", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer cet anneau.");

			index = AddCraft(typeof(Ring1), "Anneaux", "Anneau serti d'ambre", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer cet anneau.");
			AddRes(index, typeof(Ambre), "Ambre", 1, "Vous n'avez pas assez d'ambre pour fabriquer cet anneau.");

			index = AddCraft(typeof(Ring1), "Anneaux", "Anneau serti de citrine", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer cet anneau.");
			AddRes(index, typeof(Citrine), "Citrine", 1, "Vous n'avez pas assez de citrine pour fabriquer cet anneau.");

			index = AddCraft(typeof(Ring1), "Anneaux", "Anneau serti d'améthyste", 20.0, 40.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer cet anneau.");
			AddRes(index, typeof(Amethyste), "Améthyste", 1, "Vous n'avez pas assez d'améthyste pour fabriquer cet anneau.");

			index = AddCraft(typeof(Ring1), "Anneaux", "Anneau serti de tourmaline", 20.0, 40.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer cet anneau.");
			AddRes(index, typeof(Tourmaline), "Tourmaline", 1, "Vous n'avez pas assez de tourmaline pour fabriquer cet anneau.");

			index = AddCraft(typeof(Ring1), "Anneaux", "Anneau serti d'émeraude", 25.0, 45.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer cet anneau.");
			AddRes(index, typeof(Emeraude), "Émeraude", 1, "Vous n'avez pas assez d'émeraude pour fabriquer cet anneau.");

			index = AddCraft(typeof(Ring1), "Anneaux", "Anneau serti de saphir", 25.0, 45.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer cet anneau.");
			AddRes(index, typeof(Sapphire), "Saphir", 1, "Vous n'avez pas assez de saphir pour fabriquer cet anneau.");

			index = AddCraft(typeof(Ring1), "Anneaux", "Anneau serti de rubis", 30.0, 50.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer cet anneau.");
			AddRes(index, typeof(Rubis), "Rubis", 1, "Vous n'avez pas assez de rubis pour fabriquer cet anneau.");

			index = AddCraft(typeof(Ring1), "Anneaux", "Anneau serti de saphir étoilé", 35.0, 55.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer cet anneau.");
			AddRes(index, typeof(SaphirEtoile), "Saphir étoilé", 1, "Vous n'avez pas assez de saphir étoilé pour fabriquer cet anneau.");

			index = AddCraft(typeof(Ring1), "Anneaux", "Anneau serti de diamant", 40.0, 60.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer cet anneau.");
			AddRes(index, typeof(Diamant), "Diamant", 1, "Vous n'avez pas assez de diamant pour fabriquer cet anneau.");
			#endregion

			#region Colliers
			AddCraft(typeof(GoldNecklace), "Colliers", "Collier Simple", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce collier.");


			AddCraft(typeof(Necklace1), "Colliers", "Collier", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce collier.");

			index = AddCraft(typeof(Necklace1), "Colliers", "Collier serti d'ambre", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce collier.");
			AddRes(index, typeof(Ambre), "Ambre", 1, "Vous n'avez pas assez d'ambre pour fabriquer ce collier.");

			index = AddCraft(typeof(Necklace1), "Colliers", "Collier serti de citrine", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce collier.");
			AddRes(index, typeof(Citrine), "Citrine", 1, "Vous n'avez pas assez de citrine pour fabriquer ce collier.");


			index = AddCraft(typeof(Necklace1), "Colliers", "Collier serti d'améthyste", 20.0, 40.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce collier.");
			AddRes(index, typeof(Amethyste), "Améthyste", 1, "Vous n'avez pas assez d'améthyste pour fabriquer ce collier.");

			index = AddCraft(typeof(Necklace1), "Colliers", "Collier serti de tourmaline", 20.0, 40.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce collier.");
			AddRes(index, typeof(Tourmaline), "Tourmaline", 1, "Vous n'avez pas assez de tourmaline pour fabriquer ce collier.");


			index = AddCraft(typeof(Necklace1), "Colliers", "Collier serti d'émeraude", 25.0, 45.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce collier.");
			AddRes(index, typeof(Emeraude), "Émeraude", 1, "Vous n'avez pas assez d'émeraude pour fabriquer ce collier.");

			index = AddCraft(typeof(Necklace1), "Colliers", "Collier serti de saphir", 25.0, 45.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce collier.");
			AddRes(index, typeof(Sapphire), "Saphir", 1, "Vous n'avez pas assez de saphir pour fabriquer ce collier.");

			index = AddCraft(typeof(Necklace1), "Colliers", "Collier serti de rubis", 30.0, 50.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce collier.");
			AddRes(index, typeof(Rubis), "Rubis", 1, "Vous n'avez pas assez de rubis pour fabriquer ce collier.");

			index = AddCraft(typeof(Necklace1), "Colliers", "Collier serti de saphir étoilé", 35.0, 55.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce collier.");
			AddRes(index, typeof(SaphirEtoile), "Saphir étoilé", 1, "Vous n'avez pas assez de saphir étoilé pour fabriquer ce collier.");

			index = AddCraft(typeof(Necklace1), "Colliers", "Collier serti de diamant", 40.0, 60.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce collier.");
			AddRes(index, typeof(Diamant), "Diamant", 1, "Vous n'avez pas assez de diamant pour fabriquer ce collier.");

			index = AddCraft(typeof(Collier), "Colliers", "Collier massif doré", 15.0, 35.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Collier2), "Colliers", "Collier croix Ânkh", 15.0, 35.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Collier3), "Colliers", "Collier bolo doré", 15.0, 35.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Collier4), "Colliers", "Grande chaîne dorée", 20.0, 40.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Collier5), "Colliers", "Collier croix Ânkh doré", 20.0, 40.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Collier6), "Colliers", "Petit collier Usekh", 20.0, 40.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Collier7), "Colliers", "Petit collier doré", 30.0, 50.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Collier8), "Colliers", "Collier de feuilles dorées", 30.0, 50.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Collier9), "Colliers", "Collier de perle", 30.0, 50.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Collier10), "Colliers", "Collier simple avec pendentif", 35.0, 55.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Collier11), "Colliers", "Collier simple", 35.0, 55.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Collier12), "Colliers", "Grand collier doré avec pendentif", 40.0, 60.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(DiademeFeuilleOr), "Colliers", "Collier doré avec pendentif", 40.0, 60.0, typeof(Diamant), "Diamant", 1, "Vous n'avez pas assez de diamant.");
			AddRes(index, typeof(GoldIngot), "Lingot d'or", 5, "Vous n'avez pas assez de lingot d'or.");
			AddRecipe(index, (int)TinkerRecipes.DiademeFeuilleOr);


			index = AddCraft(typeof(EpauletteDoree), "Colliers", "Grand collier Usekh", 40.0, 60.0, typeof(Diamant), "Diamant", 1, "Vous n'avez pas assez de diamant.");
			AddRes(index, typeof(GoldIngot), "Lingot d'or", 5, "Vous n'avez pas assez de lingot d'or.");

			AddRecipe(index, (int)TinkerRecipes.EpauletteDoree);





			#endregion

			#region Bracelets
			AddCraft(typeof(GoldBracelet), "Bracelets", "Bracelet Simple", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce bracelet.");


			AddCraft(typeof(Bracelet1), "Bracelets", "Bracelet", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce bracelet.");

		index =	AddCraft(typeof(BraceletMontre), "Bracelets", "Montre Solaire", 25.0, 70.0, typeof(IronIngot), "Lingot de fer", 10, "Vous n'avez pas assez de lingots pour fabriquer cette montre.");
			AddRes(index, typeof(ClockParts), "Pièces d'horloge", 1, "Vous n'avez pas assez de Pièces d'horloge pour fabriquer cette montre.");
			AddRes(index, typeof(Gears), "Engrenages", 3, "Vous n'avez pas assez de d'engrenages pour fabriquer cette montre.");



			index = AddCraft(typeof(Bracelet1), "Bracelets", "Bracelet serti d'ambre", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce bracelet.");
			AddRes(index, typeof(Ambre), "Ambre", 1, "Vous n'avez pas assez d'ambre pour fabriquer ce bracelet.");

			index = AddCraft(typeof(Bracelet1), "Bracelets", "Bracelet serti de citrine", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce bracelet.");
			AddRes(index, typeof(Citrine), "Citrine", 1, "Vous n'avez pas assez de citrine pour fabriquer ce bracelet.");

			index = AddCraft(typeof(Bracelet1), "Bracelets", "Bracelet serti d'améthyste", 20.0, 40.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce bracelet.");
			AddRes(index, typeof(Amethyste), "Améthyste", 1, "Vous n'avez pas assez d'améthyste pour fabriquer ce bracelet.");

			index = AddCraft(typeof(Bracelet1), "Bracelets", "Bracelet serti de tourmaline", 20.0, 40.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce bracelet.");
			AddRes(index, typeof(Tourmaline), "Tourmaline", 1, "Vous n'avez pas assez de tourmaline pour fabriquer ce bracelet.");

			index = AddCraft(typeof(Bracelet1), "Bracelets", "Bracelet serti d'émeraude", 25.0, 45.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce bracelet.");
			AddRes(index, typeof(Emeraude), "Émeraude", 1, "Vous n'avez pas assez d'émeraude pour fabriquer ce bracelet.");

			index = AddCraft(typeof(Bracelet1), "Bracelets", "Bracelet serti de saphir", 25.0, 45.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce bracelet.");
			AddRes(index, typeof(Sapphire), "Saphir", 1, "Vous n'avez pas assez de saphir pour fabriquer ce bracelet.");

			index = AddCraft(typeof(Bracelet1), "Bracelets", "Bracelet serti de rubis", 30.0, 50.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce bracelet.");
			AddRes(index, typeof(Rubis), "Rubis", 1, "Vous n'avez pas assez de rubis pour fabriquer ce bracelet.");

			index = AddCraft(typeof(Bracelet1), "Bracelets", "Bracelet serti de saphir étoilé", 35.0, 55.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce bracelet.");
			AddRes(index, typeof(SaphirEtoile), "Saphir étoilé", 1, "Vous n'avez pas assez de saphir étoilé pour fabriquer ce bracelet.");

			index = AddCraft(typeof(Bracelet1), "Bracelets", "Bracelet serti de diamant", 40.0, 60.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ce bracelet.");
			AddRes(index, typeof(Diamant), "Diamant", 1, "Vous n'avez pas assez de diamant pour fabriquer ce bracelet.");


			#endregion

			#region Boucles d'oreilles
			AddCraft(typeof(GoldEarrings), "Boucles d'oreilles", "Boucles d'oreilles simple ", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");


			AddCraft(typeof(Earrings1), "Boucles d'oreilles", "Boucles d'oreilles", 10.0, 30.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");
			
			AddCraft(typeof(Earrings), "Boucles d'oreilles", "Boucles d'oreilles  pendantes", 55.0, 120.0, typeof(IronIngot), "Lingot de fer", 3, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");
			AddRes(index, typeof(GoldWire), "Fil d'or", 1, "Vous n'avez pas assez de fil d'or pour fabriquer ces boucles d'oreilles");

			index = AddCraft(typeof(Earrings1), "Boucles d'oreilles", "Boucles d'oreilles serti d'ambre", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");
			AddRes(index, typeof(Ambre), "Ambre", 1, "Vous n'avez pas assez d'ambre pour fabriquer ces boucles d'oreilles.");

			index = AddCraft(typeof(Earrings1), "Boucles d'oreilles", "Boucles d'oreilles serti de citrine", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");
			AddRes(index, typeof(Citrine), "Citrine", 1, "Vous n'avez pas assez de citrine pour fabriquer ces boucles d'oreilles.");

			index = AddCraft(typeof(Earrings1), "Boucles d'oreilles", "Boucles d'oreilles serti d'améthyste", 20.0, 40.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");
			AddRes(index, typeof(Amethyste), "Améthyste", 1, "Vous n'avez pas assez d'améthyste pour fabriquer ces boucles d'oreilles.");

			index = AddCraft(typeof(Earrings1), "Boucles d'oreilles", "Boucles d'oreilles serti de tourmaline", 20.0, 40.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");
			AddRes(index, typeof(Tourmaline), "Tourmaline", 1, "Vous n'avez pas assez de tourmaline pour fabriquer ces boucles d'oreilles.");

			index = AddCraft(typeof(Earrings1), "Boucles d'oreilles", "Boucles d'oreilles serti d'émeraude", 25.0, 45.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");
			AddRes(index, typeof(Emeraude), "Émeraude", 1, "Vous n'avez pas assez d'émeraude pour fabriquer ces boucles d'oreilles.");

			index = AddCraft(typeof(Earrings1), "Boucles d'oreilles", "Boucles d'oreilles serti de saphir", 25.0, 45.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");
			AddRes(index, typeof(Sapphire), "Saphir", 1, "Vous n'avez pas assez de saphir pour fabriquer ces boucles d'oreilles.");

			index = AddCraft(typeof(Earrings1), "Boucles d'oreilles", "Boucles d'oreilles serti de rubis", 30.0, 50.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");
			AddRes(index, typeof(Rubis), "Rubis", 1, "Vous n'avez pas assez de rubis pour fabriquer ces boucles d'oreilles.");

			index = AddCraft(typeof(Earrings1), "Boucles d'oreilles", "Boucles d'oreilles serti de saphir étoilé", 35.0, 55.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");
			AddRes(index, typeof(SaphirEtoile), "Saphir étoilé", 1, "Vous n'avez pas assez de saphir étoilé pour fabriquer ces boucles d'oreilles.");

			index = AddCraft(typeof(Earrings1), "Boucles d'oreilles", "Boucles d'oreilles serti de diamant", 40.0, 60.0, typeof(IronIngot), "Lingot de fer", 4, "Vous n'avez pas assez de lingots pour fabriquer ces boucles d'oreilles.");
			AddRes(index, typeof(Diamant), "Diamant", 1, "Vous n'avez pas assez de diamant pour fabriquer ces boucles d'oreilles.");

			#endregion

			#region Bijoux Divers
			index = AddCraft(typeof(Couronne2), "Bijoux Divers", "Petite couronne", 30.0, 80.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Couronne3), "Bijoux Divers", "Diadème", 30.0, 80.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Couronne4), "Bijoux Divers", "Grande couronne", 35.0, 85.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Lunettes), "Bijoux Divers", "Lunette dorée", 35.0, 85.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Chapelet), "Bijoux Divers", "Chapelet", 35.0, 85.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(Tiare), "Bijoux Divers", "Tiare", 35.0, 85.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			AddRecipe(index, (int)TinkerRecipes.Tiare);


			index = AddCraft(typeof(Ceinture10), "Bijoux Divers", "Ceinture de feuilles dorées", 40.0, 90.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			index = AddCraft(typeof(MenotteDoree), "Bijoux Divers", "Menotte dorée", 40.0, 90.0, typeof(IronIngot), "lingots", 5, "Vous n'avez pas assez de lingots.");
			AddRecipe(index, (int)TinkerRecipes.MenotteDoree);


			#endregion

			#region Paniers et boîtes
			index = AddCraft(typeof(RoundBasket), "Paniers et Boîtes", "Panier rond", 10.0, 30.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 3, 1044351);
			index = AddCraft(typeof(RoundBasketHandles), "Paniers et Boîtes", "Panier rond avec poignées", 10.0, 30.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 3, 1044351);
			index = AddCraft(typeof(SmallBushel), "Paniers et Boîtes", "Petit panier rond avec poignées", 10.0, 30.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 2, 1044351);
			index = AddCraft(typeof(PicnicBasket2), "Paniers et Boîtes", "Panier à pique-nique", 25.0, 45.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 2, 1044351);
			index = AddCraft(typeof(WinnowingBasket), "Paniers et Boîtes", "Panier à vanner", 25.0, 45.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 3, 1044351);
			index = AddCraft(typeof(SquareBasket), "Paniers et Boîtes", "Panier carré", 25.0, 45.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 3, 1044351);
			index = AddCraft(typeof(BasketCraftable), "Paniers et Boîtes", "Panier tressé", 35.0, 50.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 3, 1044351);
			index = AddCraft(typeof(TallRoundBasket), "Paniers et Boîtes", "Panier haut tressé", 35.0, 50.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 4, 1044351);
			index = AddCraft(typeof(SmallSquareBasket), "Paniers et Boîtes", "Petit panier carré", 35.0, 50.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 2, 1044351);
			index = AddCraft(typeof(TallBasket), "Paniers et Boîtes", "Grand panier tressé", 45.0, 65.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 4, 1044351);
			index = AddCraft(typeof(SmallRoundBasket), "Paniers et Boîtes", "Panier tressé rond", 45.0, 65.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 2, 1044351);
			index = AddCraft(typeof(GiftBoxAngel), "Paniers et Boîtes", "Boite Cadeau, Ange", 45.0, 65.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 2, 1044351);
			index = AddCraft(typeof(GiftBoxCube), "Paniers et Boîtes", "Boite Cadeau, Carré", 55.0, 75.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 2, 1044351);
			index = AddCraft(typeof(GiftBoxCylinder), "Paniers et Boîtes", "Boite Cadeau, Cylindre", 55.0, 75.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 2, 1044351);
			index = AddCraft(typeof(GiftBoxOctogon), "Paniers et Boîtes", "Boite Cadeau, Octogone", 55.0, 75.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 2, 1044351);
			index = AddCraft(typeof(GiftBoxRectangle), "Paniers et Boîtes", "Boite Cadeau, Rectangle", 55.0, 75.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 2, 1044351);
			index = AddCraft(typeof(RedVelvetGiftBox), "Paniers et Boîtes", "Boite Cadeau, Petite rouge", 55.0, 75.0, typeof(Kindling), "Petit Bois", 5, "Vous manquez de petit bois");
			AddRes(index, typeof(Shaft), 1027125, 2, 1044351);

			// Panier Plein
			index = AddCraft(typeof(PanierPlein), "Paniers et Boîtes", "Panier Plein", 50.0, 100.0, typeof(PalmierLog), "Buches", 3, "Vous manquez de buches");
			AddRes(index, typeof(PalmierBoard), "Planches", 3, "Vous manquez de planches");

			#endregion

			#region Pièces d'assemblages
			index = AddCraft(typeof(Gears), "Pièces d'assemblages", "Engrenages", 5.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(ClockFrame), "Pièces d'assemblages", "Cadre d'horloge", 0.0, 50.0, typeof(PalmierBoard), 1044041, 6, 1044351);
			SetUseSubRes2(index, true);
			index = AddCraft(typeof(BarrelTap), "Pièces d'assemblages", "Robinet de baril", 35.0, 85.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Springs), "Pièces d'assemblages", "Ressorts", 5.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(BarrelHoops), "Pièces d'assemblages", "Cercles de tonneau", -15.0, 35.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Hinge), "Pièces d'assemblages", "Charnière", 5.0, 55.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Axle), "Pièces d'assemblages", "Essieu", -25.0, 25.0, typeof(PalmierBoard), 1044041, 2, 1044351);
			SetUseSubRes2(index, true);
			#endregion

			#region Assemblages
			index = AddCraft(typeof(AxleGears), "Assemblages", "Engrenage d'essieu", 0.0, 0.0, typeof(Axle), "Essieu", 1, 1044253);
			AddRes(index, typeof(Gears), 1044254, 1, 1044253);
			index = AddCraft(typeof(ClockParts), "Assemblages", "Pièces d'horloge", 0.0, 0.0, typeof(AxleGears), "Engrenage d'essieu", 1, 1044253);
			AddRes(index, typeof(Springs), "Ressorts", 1, 1044253);
			index = AddCraft(typeof(SextantParts), "Assemblages", "Pièces de sextant", 0.0, 0.0, typeof(AxleGears), "Engrenage d'essieu", 1, 1044253);
			AddRes(index, typeof(Hinge), "Charnière", 1, 1044253);
			index = AddCraft(typeof(ClockRight), "Assemblages", "Horloge (D)", 30.0, 60.0, typeof(ClockFrame), "Cadre d'horloge", 1, 1044253);
			AddRes(index, typeof(ClockParts), "Pièces d'horloge", 1, 1044253);
			index = AddCraft(typeof(ClockLeft), "Assemblages", "Horloge (G)", 30.0, 60.0, typeof(ClockFrame), "Cadre d'horloge", 1, 1044253);
			AddRes(index, typeof(ClockParts), "Pièces d'horloge", 1, 1044253);
			index = AddCraft(typeof(SmallGrandfatherClock), "Assemblages", "Petite Horloge Grand Père", 50.0, 90.0, typeof(ClockFrame), "Cadre d'horloge", 1, 1044253);
			AddRes(index, typeof(ClockParts), "Pièces d'horloge", 2, 1044253);
			AddRes(index, typeof(PalmierBoard), 1044041, 8, 1044351);
			SetUseSubRes2(index, true);
			index = AddCraft(typeof(LargeGrandfatherClock), "Assemblages", "Horloge Grand Père", 50.0, 90.0, typeof(ClockFrame), "Cadre d'horloge", 1, 1044253);
			AddRes(index, typeof(ClockParts), "Pièces d'horloge", 2, 1044253);
			AddRes(index, typeof(PalmierBoard), 1044041, 8, 1044351);
			SetUseSubRes2(index, true);
			index = AddCraft(typeof(WhiteGrandfatherClock), "Assemblages", "Horloge Grand Père Blanche", 50.0, 90.0, typeof(ClockFrame), "Cadre d'horloge", 1, 1044253);
			AddRes(index, typeof(ClockParts), "Pièces d'horloge", 2, 1044253);
			AddRes(index, typeof(PalmierBoard), 1044041, 8, 1044351);
			SetUseSubRes2(index, true);
			index = AddCraft(typeof(Sextant), "Assemblages", "Sextant", 0.0, 0.0, typeof(SextantParts), "Pièces de sextant", 1, 1044253);
			AddRes(index, typeof(Leather), 1044462, 3, 1044463);
			index = AddCraft(typeof(PotionKeg), "Assemblages", "Tonnelet de potions", 35.0, 55.0, typeof(Keg), "Tonnelet", 1, 1044253);
			AddRes(index, typeof(Bottle), 1044250, 10, 1044253);
			AddRes(index, typeof(BarrelLid), "Couvercle de baril", 1, 1044253);
			AddRes(index, typeof(BarrelTap), "Robinet de baril", 1, 1044253);
			index = AddCraft(typeof(Rope), "Assemblages", "Corde", 60.0, 120.0, typeof(Cloth), "Tissus", 10, "Vous avez besoin de plus de tissus");
			index = AddCraft(typeof(DistillerySouthAddonDeed), "Assemblages", "Distillerie (S)", 65.0, 100.0, typeof(LiquorBarrel), "Tonneau d'alcool", 2, 1044253);
			AddRes(index, typeof(HeatingStand), "Support chauffant", 4, 1044253);
			AddRes(index, typeof(CopperWire), "Fil de cuivre", 20, 1044253);
			ForceNonExceptional(index);
			index = AddCraft(typeof(DistilleryEastAddonDeed), "Assemblages", "Distillerie (E)", 65.0, 100.0, typeof(LiquorBarrel), "Tonneau d'alcool", 2, 1044253);
			AddRes(index, typeof(HeatingStand), "Support chauffant", 4, 1044253);
			AddRes(index, typeof(CopperWire), "Fil de cuivre", 20, 1044253);
			ForceNonExceptional(index);
			
			#endregion

			#region Ustensiles
			index = AddCraft(typeof(FoodPlate), "Ustensiles", "Assiette", 25.0, 45.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(SpoonLeft), "Ustensiles", "Cuillière (G)", 0.0, 50.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(SpoonRight), "Ustensiles", "Cuillière (D)", 0.0, 50.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(ForkLeft), "Ustensiles", "Fourchette (G)", 0.0, 50.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(ForkRight), "Ustensiles", "Fourchette (D)", 0.0, 50.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(KnifeLeft), "Ustensiles", "Couteau (G)", 0.0, 50.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(KnifeRight), "Ustensiles", "Couteau (D)", 0.0, 50.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Goblet), "Ustensiles", "Gobelet", 10.0, 60.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(PewterMug), "Ustensiles", "Chope en étain", 10.0, 60.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Tray), "Ustensiles", "Plateau", 25.0, 75.0, typeof(PalmierBoard), 1044041, 2, 1044351);
			SetUseSubRes2(index, true);
			index = AddCraft(typeof(Silverware), "Ustensiles", "Argenterie", 25.0, 75.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");

			// Vaisselle
			index = AddCraft(typeof(Vaisselle), "Ustensiles", "Vaisselle", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 2, " vous n'avez pas suffisament de Lingot de fer");

			// Chaudrons Multiples
			index = AddCraft(typeof(ChaudronsMultiples), "Ustensiles", "Chaudrons Multiples", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 2, " vous n'avez pas suffisament de Lingot de fer");


			// Plateau de coupes
			index = AddCraft(typeof(PlateauDeCoupes), "Ustensiles", "Plateau de Coupes", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(PlateauDeSoupe), "Ustensiles", "Plateau de soupe", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");


			#endregion

			#region Luminaires et décorations
			index = AddCraft(typeof(Torch), "Luminaires et Décorations", "Torche", 0.0, 50.0, typeof(PalmierBoard), 1044041, 2, 1044253);
			SetUseSubRes2(index, true);
			index = AddCraft(typeof(CandleLarge), "Luminaires et Décorations", "Chandelier Simple", 45.0, 105.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Candelabra), "Luminaires et Décorations", "Chandelier", 55.0, 105.0, typeof(IronIngot), "Lingot de fer", 10, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(CandelabraStand), "Luminaires et Décorations", "Grand Chandelier", 65.0, 105.0, typeof(IronIngot), "Lingot de fer", 8, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(WallSconce), "Luminaires et Décorations", "Chandelle Murale", 35.0, 105.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(WallTorch), "Luminaires et Décorations", "Torche murale", 35.0, 105.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Lantern), "Luminaires et Décorations", "Lanterne", 30.0, 80.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(HeatingStand), "Luminaires et Décorations", "Support chauffant", 60.0, 110.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(ShojiLantern), "Luminaires et Décorations", "Lanterne sophistiquée", 65.0, 115.0, typeof(IronIngot), "Lingot de fer", 30, " vous n'avez pas suffisament de Lingot de fer");
			AddRes(index, typeof(PalmierBoard), 1044041, 5, 1044351);
			index = AddCraft(typeof(Brazier), "Luminaires et Décorations", "Brasero", 45.0, 100.0, typeof(IronIngot), "Lingot de fer", 55, 1044253);
			index = AddCraft(typeof(BrazierTall), "Luminaires et Décorations", "Brasero Long", 65.0, 100.0, typeof(IronIngot), "Lingot de fer", 55, 1044253);
			index = AddCraft(typeof(DragonBrazier), "Luminaires et Décorations", "Brasero Cage", 85.0, 100.0, typeof(IronIngot), "Lingot de fer", 55, 1044253);

			index = AddCraft(typeof(TerMurStyleCandelabra), "Luminaires et Décorations", "Chandelier élégant", 55.0, 105.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			AddRecipe(index, (int)TinkerRecipes.TerMurStyleCandelabra);



			index = AddCraft(typeof(PaperLantern), "Luminaires et Décorations", "Lanterne en papier", 65.0, 115.0, typeof(IronIngot), "Lingot de fer", 30, " vous n'avez pas suffisament de Lingot de fer");
			AddRes(index, typeof(PalmierBoard), 1044041, 5, 1044351);
			index = AddCraft(typeof(RoundPaperLantern), "Luminaires et Décorations", "Lanterne en papier ronde", 65.0, 115.0, typeof(IronIngot), "Lingot de fer", 30, " vous n'avez pas suffisament de Lingot de fer");
			AddRes(index, typeof(PalmierBoard), 1044041, 5, 1044351);
			index = AddCraft(typeof(WindChimes), "Luminaires et Décorations", "Carillons éoliens", 80.0, 130.0, typeof(IronIngot), "Lingot de fer", 35, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(FancyWindChimes), "Luminaires et Décorations", "Carillons", 80.0, 130.0, typeof(IronIngot), "Lingot de fer", 35, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(LivresMultiples), "Luminaires et Décorations", "Livres Multiples", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 2, " vous n'avez pas suffisament de Lingot de fer");


			index = AddCraft(typeof(ClotureDeMetal), "Luminaires et Décorations", "Clôture de Métal", 70.0, 120.0, typeof(IronIngot), "Lingot de fer", 10, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(LampPost1), "Luminaires et Décorations", "Lampadaire de Rue", 70.0, 120.0, typeof(IronIngot), "Lingot de fer", 10, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(LampPost2), "Luminaires et Décorations", "Lampadaire de Rue", 70.0, 120.0, typeof(IronIngot), "Lingot de fer", 10, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(LampPost3), "Luminaires et Décorations", "Lampadaire de Rue", 70.0, 120.0, typeof(IronIngot), "Lingot de fer", 10, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(FourABois), "Luminaires et Décorations", "Four à Bois", 75.0, 125.0, typeof(IronIngot), "Lingot de fer", 15, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(TabletteEnMetal), "Luminaires et Décorations", "Tablette en métal", 40.0, 90.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(DragonLamp), "Luminaires et Décorations", "Lampe à l'huile avec dragon", 60.0, 110.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(BlueMarbleFireplaceDeed), "Luminaires et Décorations", "Foyer Marbre Bleu", 60.0, 110.0, typeof(IronIngot), "Lingot de fer", 6, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(GoldMarbleFireplaceDeed), "Luminaires et Décorations", "Foyer Marbre Or", 60.0, 110.0, typeof(IronIngot), "Lingot de fer", 6, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(GreenMarbleFireplaceDeed), "Luminaires et Décorations", "Foyer Marbre Vert", 60.0, 110.0, typeof(IronIngot), "Lingot de fer", 6, " vous n'avez pas suffisament de Lingot de fer");




			index = AddCraft(typeof(SmithingPressDeed), "Luminaires et Décorations", "Machine de forge", 75.0, 125.0, typeof(IronIngot), "Lingot de fer", 25, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(BBQSmokerDeed), "Luminaires et Décorations", "Fumoir", 75.0, 125.0, typeof(IronIngot), "Lingot de fer", 25, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(WritingDeskDeed), "Luminaires et Décorations", "Table d'écriture", 75.0, 125.0, typeof(IronIngot), "Lingot de fer", 25, " vous n'avez pas suffisament de Lingot de fer");


			index = AddCraft(typeof(SpinningLatheDeed), "Luminaires et Décorations", "Machine de menuiserie", 75.0, 125.0, typeof(IronIngot), "Lingot de fer", 25, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(FletchingStationDeed), "Luminaires et Décorations", "Machine de créations d'arc", 75.0, 125.0, typeof(IronIngot), "Lingot de fer", 25, " vous n'avez pas suffisament de Lingot de fer");


			index = AddCraft(typeof(WeddingFancyCandelabra), "Luminaires et Décorations", "Lumière de Mariage", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 10, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(FontaineDeChocolat), "Luminaires et Décorations", "Fontaine de chocolat", 65.0, 115.0, typeof(IronIngot), "Lingot de fer", 15, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(WeddingRegularCandelabra), "Luminaires et Décorations", "Chandelier à 5 chandelles", 65.0, 115.0, typeof(IronIngot), "Lingot de fer", 10, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(BoiteRessourceDecoration), "Luminaires et Décorations", "Boite de ressource décoration", 40.0, 90.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			AddRes(index, typeof(PalmierBoard), "Planches", 1, "Vous n'avez pas suffisament de planches");

			index = AddCraft(typeof(PuitEau), "Luminaires et Décorations", "Puits", 60.0, 110.0, typeof(IronIngot), "Lingot de fer", 15, " vous n'avez pas suffisament de Lingot de fer");
			AddRes(index, typeof(Rope), "Corde", 1, "Vous n'avez pas suffisament de corde");

			index = AddCraft(typeof(ChevalierDecoratif), "Luminaires et Décorations", "Chevalier décoratif", 70.0, 120.0, typeof(IronIngot), "Lingot de fer", 20, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(FourALaBroche), "Luminaires et Décorations", "Four à la broche", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 10, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(FirePitDeed), "Luminaires et Décorations", "Carré de feu de sol", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 15, " vous n'avez pas suffisament de Lingot de fer");


			index = AddCraft(typeof(StainedGlassLamp), "Luminaires et Décorations", "Lampe sur table", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 15, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(TableLamp), "Luminaires et Décorations", "Lampe sur table 2", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 15, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(TallDoubleLamp), "Luminaires et Décorations", "Lampe sur pied double", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 15, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(TallLampDeed), "Luminaires et Décorations", "Lampe sur pied ", 50.0, 100.0, typeof(IronIngot), "Lingot de fer", 15, " vous n'avez pas suffisament de Lingot de fer");


			#endregion


			#region Appats

			// Appât pour Fish
			index = AddCraft(typeof(BaitFish), "Appâts", "Appât à Poisson (1)", 0.0, 0.0, typeof(IronIngot), "Lingots de fer", 1, "Vous n'avez pas assez de lingots de fer pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, -10.0, 50.0);

			// Appât pour AutumnDragonfish
			index = AddCraft(typeof(BaitAutumnDragonfish), "Appâts", "Appât à AutumnDragonFish (1)", 0.0, 0.0, typeof(Fish), "Poisson entier", 1, "Vous n'avez pas assez de poisson pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 10.0, 30.0);

			// Appât pour BlueLobster
			index = AddCraft(typeof(BaitBlueLobster), "Appâts", "Appât à Homard Bleu (1)", 0.0, 0.0, typeof(AutumnDragonfish), "AutumnDragonFish entier", 1, "Vous n'avez pas assez de AutumnDragonFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 10.0, 30.0);

			// Appât pour BullFish
			index = AddCraft(typeof(BaitBullFish), "Appâts", "Appât à BullFish (1)", 0.0, 0.0, typeof(BlueLobster), "Homard Bleu entier", 1, "Vous n'avez pas assez de Homard Bleu pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 10.0, 30.0);

			// Appât pour CrystalFish
			index = AddCraft(typeof(BaitCrystalFish), "Appâts", "Appât à CrystalFish (1)", 0.0, 0.0, typeof(BullFish), "BullFish entier", 1, "Vous n'avez pas assez de BullFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 10.0, 30.0);

			// Appât pour FairySalmon
			index = AddCraft(typeof(BaitFairySalmon), "Appâts", "Appât à FairySalmon (1)", 0.0, 0.0, typeof(CrystalFish), "CrystalFish entier", 1, "Vous n'avez pas assez de CrystalFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 20.0, 40.0);

			// Appât pour FireFish
			index = AddCraft(typeof(BaitFireFish), "Appâts", "Appât à FireFish (1)", 0.0, 0.0, typeof(FairySalmon), "FairySalmon entier", 1, "Vous n'avez pas assez de FairySalmon pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 20.0, 40.0);

			// Appât pour GiantKoi
			index = AddCraft(typeof(BaitGiantKoi), "Appâts", "Appât à GiantKoi (1)", 0.0, 0.0, typeof(FireFish), "FireFish entier", 1, "Vous n'avez pas assez de FireFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 20.0, 40.0);

			// Appât pour GreatBarracuda
			index = AddCraft(typeof(BaitGreatBarracuda), "Appâts", "Appât à GreatBarracuda (1)", 0.0, 0.0, typeof(GiantKoi), "GiantKoi entier", 1, "Vous n'avez pas assez de GiantKoi pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 20.0, 40.0);

			// Appât pour HolyMackerel
			index = AddCraft(typeof(BaitHolyMackerel), "Appâts", "Appât à HolyMackerel (1)", 0.0, 0.0, typeof(GreatBarracuda), "GreatBarracuda entier", 1, "Vous n'avez pas assez de GreatBarracuda pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 35.0, 55.0);

			// Appât pour LavaFish
			index = AddCraft(typeof(BaitLavaFish), "Appâts", "Appât à LavaFish (1)", 0.0, 0.0, typeof(HolyMackerel), "HolyMackerel entier", 1, "Vous n'avez pas assez de HolyMackerel pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 35.0, 55.0);

			// Appât pour ReaperFish
			index = AddCraft(typeof(BaitReaperFish), "Appâts", "Appât à ReaperFish (1)", 0.0, 0.0, typeof(LavaFish), "LavaFish entier", 1, "Vous n'avez pas assez de LavaFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 35.0, 55.0);

			// Appât pour SpiderCrab
			index = AddCraft(typeof(BaitSpiderCrab), "Appâts", "Appât à SpiderCrab (1)", 0.0, 0.0, typeof(ReaperFish), "ReaperFish entier", 1, "Vous n'avez pas assez de ReaperFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 40.0, 60.0);

			// Appât pour StoneCrab
			index = AddCraft(typeof(BaitStoneCrab), "Appâts", "Appât à StoneCrab (1)", 0.0, 0.0, typeof(SpiderCrab), "SpiderCrab entier", 1, "Vous n'avez pas assez de SpiderCrab pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 40.0, 60.0);

			// Appât pour SummerDragonfish
			index = AddCraft(typeof(BaitSummerDragonfish), "Appâts", "Appât à SummerDragonfish (1)", 0.0, 0.0, typeof(StoneCrab), "StoneCrab entier", 1, "Vous n'avez pas assez de StoneCrab pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 40.0, 60.0);

			// Appât pour UnicornFish
			index = AddCraft(typeof(BaitUnicornFish), "Appâts", "Appât à UnicornFish (1)", 0.0, 0.0, typeof(SummerDragonfish), "SummerDragonfish entier", 1, "Vous n'avez pas assez de SummerDragonfish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 50.0, 70.0);

			// Appât pour YellowtailBarracuda
			index = AddCraft(typeof(BaitYellowtailBarracuda), "Appâts", "Appât à YellowtailBarracuda (1)", 0.0, 0.0, typeof(UnicornFish), "UnicornFish entier", 1, "Vous n'avez pas assez de UnicornFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 50.0, 70.0);

			// Appât pour AbyssalDragonfish
			index = AddCraft(typeof(BaitAbyssalDragonfish), "Appâts", "Appât à AbyssalDragonfish (1)", 0.0, 0.0, typeof(YellowtailBarracuda), "YellowtailBarracuda entier", 1, "Vous n'avez pas assez de YellowtailBarracuda pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 50.0, 70.0);

			// Appât pour BlackMarlin
			index = AddCraft(typeof(BaitBlackMarlin), "Appâts", "Appât à BlackMarlin (1)", 0.0, 0.0, typeof(AbyssalDragonfish), "AbyssalDragonfish entier", 1, "Vous n'avez pas assez de AbyssalDragonfish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 50.0, 70.0);

			// Appât pour BloodLobster
			index = AddCraft(typeof(BaitBloodLobster), "Appâts", "Appât à BloodLobster (1)", 0.0, 0.0, typeof(BlackMarlin), "BlackMarlin entier", 1, "Vous n'avez pas assez de BlackMarlin pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 50.0, 70.0);

			// Appât pour BlueMarlin
			index = AddCraft(typeof(BaitBlueMarlin), "Appâts", "Appât à BlueMarlin (1)", 0.0, 0.0, typeof(BloodLobster), "BloodLobster entier", 1, "Vous n'avez pas assez de BloodLobster pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 60.0, 80.0);

			// Appât pour DreadLobster
			index = AddCraft(typeof(BaitDreadLobster), "Appâts", "Appât à DreadLobster (1)", 0.0, 0.0, typeof(BlueMarlin), "BlueMarlin entier", 1, "Vous n'avez pas assez de BlueMarlin pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 60.0, 80.0);

			// Appât pour DungeonPike
			index = AddCraft(typeof(BaitDungeonPike), "Appâts", "Appât à DungeonPike (1)", 0.0, 0.0, typeof(DreadLobster), "DreadLobster entier", 1, "Vous n'avez pas assez de DreadLobster pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 60.0, 80.0);

			// Appât pour GiantSamuraiFish
			index = AddCraft(typeof(BaitGiantSamuraiFish), "Appâts", "Appât à GiantSamuraiFish (1)", 0.0, 0.0, typeof(DungeonPike), "DungeonPike entier", 1, "Vous n'avez pas assez de DungeonPike pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 60.0, 80.0);

			// Appât pour GoldenTuna
			index = AddCraft(typeof(BaitGoldenTuna), "Appâts", "Appât à GoldenTuna (1)", 0.0, 0.0, typeof(GiantSamuraiFish), "GiantSamuraiFish entier", 1, "Vous n'avez pas assez de GiantSamuraiFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 60.0, 80.0);

			// Appât pour Kingfish
			index = AddCraft(typeof(BaitKingfish), "Appâts", "Appât à Kingfish (1)", 0.0, 0.0, typeof(GoldenTuna), "GoldenTuna entier", 1, "Vous n'avez pas assez de GoldenTuna pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 70.0, 90.0);

			// Appât pour LanternFish
			index = AddCraft(typeof(BaitLanternFish), "Appâts", "Appât à LanternFish (1)", 0.0, 0.0, typeof(Kingfish), "Kingfish entier", 1, "Vous n'avez pas assez de Kingfish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 70.0, 90.0);

			// Appât pour RainbowFish
			index = AddCraft(typeof(BaitRainbowFish), "Appâts", "Appât à RainbowFish (1)", 0.0, 0.0, typeof(LanternFish), "LanternFish entier", 1, "Vous n'avez pas assez de LanternFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 70.0, 90.0);

			// Appât pour SeekerFish
			index = AddCraft(typeof(BaitSeekerFish), "Appâts", "Appât à SeekerFish (1)", 0.0, 0.0, typeof(RainbowFish), "RainbowFish entier", 1, "Vous n'avez pas assez de RainbowFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 70.0, 90.0);

			// Appât pour SpringDragonfish
			index = AddCraft(typeof(BaitSpringDragonfish), "Appâts", "Appât à SpringDragonfish (1)", 0.0, 0.0, typeof(SeekerFish), "SeekerFish entier", 1, "Vous n'avez pas assez de SeekerFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 70.0, 90.0);

			// Appât pour StoneFish
			index = AddCraft(typeof(BaitStoneFish), "Appâts", "Appât à StoneFish (1)", 0.0, 0.0, typeof(SpringDragonfish), "SpringDragonfish entier", 1, "Vous n'avez pas assez de SpringDragonfish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 75.0, 95.0);

			// Appât pour TunnelCrab
			index = AddCraft(typeof(BaitTunnelCrab), "Appâts", "Appât à TunnelCrab (1)", 0.0, 0.0, typeof(StoneFish), "StoneFish entier", 1, "Vous n'avez pas assez de StoneFish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 75.0, 95.0);

			// Appât pour VoidCrab
			index = AddCraft(typeof(BaitVoidCrab), "Appâts", "Appât à VoidCrab (1)", 0.0, 0.0, typeof(TunnelCrab), "TunnelCrab entier", 1, "Vous n'avez pas assez de TunnelCrab pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 75.0, 95.0);

			// Appât pour VoidLobster
			index = AddCraft(typeof(BaitVoidLobster), "Appâts", "Appât à VoidLobster (1)", 0.0, 0.0, typeof(VoidCrab), "VoidCrab entier", 1, "Vous n'avez pas assez de VoidCrab pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 75.0, 95.0);

			// Appât pour WinterDragonfish
			index = AddCraft(typeof(BaitWinterDragonfish), "Appâts", "Appât à WinterDragonfish (1)", 0.0, 0.0, typeof(VoidLobster), "VoidLobster entier", 1, "Vous n'avez pas assez de VoidLobster pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 80.0, 100.0);

			// Appât pour ZombieFish
			index = AddCraft(typeof(BaitZombieFish), "Appâts", "Appât à ZombieFish (1)", 0.0, 0.0, typeof(WinterDragonfish), "WinterDragonfish entier", 1, "Vous n'avez pas assez de WinterDragonfish pour faire cet appât.");
			AddSkill(index, SkillName.Fishing, 80.0, 100.0);

			// Trophé de pêche
			index = AddCraft(typeof(TropheePeche), "Appâts", "Trophée de Pêche", 75.0, 125.0, typeof(RawFishSteak), "Filet de poisson cru", 30, "Vous n'avez pas suffisament de filet de poisson cru.");
			AddRes(index, typeof(PalmierBoard), "Planches", 2, "Vous n'avez pas suffisament de planches");


			#endregion


			#region Cristaux de Golem
			index = AddCraft(typeof(GolemCrystalCitrine), "Cristaux de Golem", "Cristal de Citrine", 50.0, 70.0, typeof(Citrine), "Citrine", 25, "Vous n'avez pas suffisament de Citrine");
			AddRes(index, typeof(IronIngot), "Lingot de fer", 5, "Vous n'avez pas suffisamment de Lingot de fer");

			index = AddCraft(typeof(GolemCrystalRubis), "Cristaux de Golem", "Cristal de Rubis", 50.0, 70.0, typeof(Rubis), "Rubis", 25, "Vous n'avez pas suffisament de Rubis");
			AddRes(index, typeof(IronIngot), "Lingot de fer", 5, "Vous n'avez pas suffisamment de Lingot de fer");

			index = AddCraft(typeof(GolemCrystalAmbre), "Cristaux de Golem", "Cristal d'Ambre", 50.0, 70.0, typeof(Ambre), "Ambre", 25, "Vous n'avez pas suffisament de Ambre");
			AddRes(index, typeof(IronIngot), "Lingot de fer", 5, "Vous n'avez pas suffisamment de Lingot de fer");

			index = AddCraft(typeof(GolemCrystalTourmaline), "Cristaux de Golem", "Cristal de Tourmaline", 50.0, 70.0, typeof(Tourmaline), "Tourmaline", 25, "Vous n'avez pas suffisament de Tourmaline");
			AddRes(index, typeof(IronIngot), "Lingot de fer", 5, "Vous n'avez pas suffisamment de Lingot de fer");

			index = AddCraft(typeof(GolemCrystalSaphire), "Cristaux de Golem", "Cristal de Saphir", 60.0, 80.0, typeof(Sapphire), "Saphire", 25, "Vous n'avez pas suffisament de Saphire");
			AddRes(index, typeof(IronIngot), "Lingot de fer", 5, "Vous n'avez pas suffisamment de Lingot de fer");

			index = AddCraft(typeof(GolemCrystalEmeraude), "Cristaux de Golem", "Cristal d'Émeraude", 60.0, 80.0, typeof(Emeraude), "Emeraude", 25, "Vous n'avez pas suffisament de Emeraude");
			AddRes(index, typeof(IronIngot), "Lingot de fer", 5, "Vous n'avez pas suffisamment de Lingot de fer");

			index = AddCraft(typeof(GolemCrystalAmethyste), "Cristaux de Golem", "Cristal d'Améthyste", 60.0, 80.0, typeof(Amethyste), "Améthyste", 25, "Vous n'avez pas suffisament de Amethyste");
			AddRes(index, typeof(IronIngot), "Lingot de fer", 5, "Vous n'avez pas suffisamment de Lingot de fer");

			index = AddCraft(typeof(GolemCrystalSaphireEtoile), "Cristaux de Golem", "Cristal de Saphir d'Étoile", 80.0, 100.0, typeof(SaphirEtoile), "Saphir Étoilé", 25, "Vous n'avez pas suffisament de Saphire Étoilé");
			AddRes(index, typeof(IronIngot), "Lingot de fer", 5, "Vous n'avez pas suffisamment de Lingot de fer");

			index = AddCraft(typeof(GolemCrystalDiamant), "Cristaux de Golem", "Cristal de Diamant", 90.0, 110.0, typeof(Diamant), "Diamant", 25, "Vous n'avez pas suffisament de Diamant");
			AddRes(index, typeof(IronIngot), "Lingot de fer", 5, "Vous n'avez pas suffisamment de Lingot de fer");



			#endregion
			#region Divers
			index = AddCraft(typeof(FishSteakResourceCrate), "Divers", "Caisse de Poissons frais", 0.0, 0.0, typeof(RawFishSteak), "Steak de poisson", 150, "Vous n'avez pas suffisament de steak de poisson");
			AddSkill(index, SkillName.Fishing, 00.0, 60.0);

			AddCraft(typeof(IronIngotResourceCrate), "Divers", "Caisse de ressource", 10.0, 60.0, typeof(IronIngot), "Lingot de fer", 150, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(KeyRing), "Divers", "Trousseau de clés", 10.0, 60.0, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Key), "Divers", "Clé en fer", 20.0, 70.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(DyeTub), "Divers", "Bac de Teinture", 35.0, 65.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			SetUseSubRes2(index, true);
			index = AddCraft(typeof(Scales), "Divers", "Balance", 60.0, 110.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Globe), "Divers", "Globe terrestre", 55.0, 105.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(MagicCrystalBall), "Divers", "Boule de Cristal", 35.0, 105.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(DecoDeckOfTarot), "Divers", "Jeu de Tarot", 55.0, 105.0, typeof(BlankScroll), "Parchemins Vierge", 5, "Vous n'avez pas suffisament de parchemins vierge");
			index = AddCraft(typeof(LargeFishingPole), "Divers", "Canne à pêche", 10.0, 30.0, typeof(PalmierBoard), 1044041, 5, 1044351); //This is in the categor of Other during AoS
			AddRes(index, typeof(Cloth), 1044286, 5, 1044287);
			SetUseSubRes2(index, true);
			index = AddCraft(typeof(ParrotPerchAddonDeed), "Divers", "Perchoir à Perroquet", 30.0, 80.0, typeof(Materiaux), "Matériaux", 40, "Vous n'avez pas assez de matériaux");

			index = AddCraft(typeof(CustomSign), "Divers", "Affiche Personnalisée", 30.0, 80.0, typeof(Materiaux), "Matériaux", 10, "Vous n'avez pas assez de matériaux");
			AddRes(index, typeof(BlankScroll), "Parchemin Vierge", 3, "Vous n'avez pas suffisament de parchemin vierge");

			index = AddCraft(typeof(CommodityDeedBox), "Divers", "Boite de Commoditée", 30.0, 80.0, typeof(Materiaux), "Matériaux", 30, "Vous n'avez pas assez de matériaux");
			AddRes(index, typeof(BlankScroll), "Parchemin Vierge", 10, "Vous n'avez pas suffisament de parchemin vierge");

			index = AddCraft(typeof(MannequinDeed), "Divers", "Mannequin (F)", 60.0, 100.0, typeof(Materiaux), "Matériaux", 35, "Vous n'avez pas assez de matériaux");
			AddRes(index, typeof(Cloth), "Tissus", 20, "Vous n'avez pas suffisament de tissus");
			index = AddCraft(typeof(MannequinDeed), "Divers", "Mannequin (M)", 60.0, 100.0, typeof(Materiaux), "Matériaux", 35, "Vous n'avez pas assez de matériaux");
			AddRes(index, typeof(Cloth), "Tissus", 20, "Vous n'avez pas suffisament de tissus");

			index = AddCraft(typeof(ItemChangerSiren), "Divers", "Création unique", 70.0, 120.0, typeof(Materiaux), "Matériaux", 150, "Vous n'avez pas assez de matériaux");
			AddRes(index, typeof(Gold), "Or", 250, "Vous n'avez pas suffisament d'or");
	




			ForceNonExceptional(index);
			index = AddCraft(typeof(Spyglass), "Divers", "Longue vue", 60.0, 110.0, typeof(IronIngot), "Lingot de fer", 4, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Fouet4), "Divers", "Fouet 4 mètres", 50.0, 70.0, typeof(Leather), "Cuir", 3, 1044463);
			index = AddCraft(typeof(Fouet6), "Divers", "Fouet 6 mètres", 65.0, 85.0, typeof(Leather), "Cuir", 4, 1044463);
			index = AddCraft(typeof(Fouet8), "Divers", "Fouet 8 mètres", 85.0, 105.0, typeof(Leather), "Cuir", 5, 1044463);
			index = AddCraft(typeof(IronWire), "Divers", "Fil de fer", 30.0, 60.0, typeof(IronIngot), "Lingot de fer", 2, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(CopperWire), "Divers", "Fil de cuivre", 30.0, 60.0, typeof(CopperIngot), "Lingot de cuivre", 2, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(SilverWire), "Divers", "Fil d'argent", 50.0, 90.0, typeof(IronIngot), "Lingot de fer", 2, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(GoldWire), "Divers", "Fil d'or", 70.0, 110.0, typeof(GoldIngot), "Lingot d'or", 2, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Lunettes1), "Divers", "Lunettes de soleil", 50.0, 90.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Lunettes2), "Divers", "Lunettes de navigateur", 50.0, 90.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Lunettes3), "Divers", "Lunettes De vision", 50.0, 90.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Bottle), "Divers", "Bouteille Vide (1)", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(Bottle), "Divers", "Bouteille Vide (Max)", 15.0, 35.0, typeof(IronIngot), "Lingot de fer", 3, " vous n'avez pas suffisament de Lingot de fer");
			SetUseAllRes(index, true);
			AddCraft(typeof(EmptyWineBottle), "Divers", "Bouteille de Vin", 22.5, 42.5, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			AddCraft(typeof(EmptyAleBottle), "Divers", "Bouteille de Bière", 32.5, 52.5, typeof(IronIngot), "Lingot de fer", 5, " vous n'avez pas suffisament de Lingot de fer");
			index = AddCraft(typeof(PetLeash), "Divers", "Harnais pour animaux", 80.0, 130.0, typeof(Leather), "Cuir", 5, 1044463);
			AddRes(index, typeof(PoussiereNecrotique), "Poussière Nécrotique", 10, "Vous n'avez pas suffisament de Poussière Nécrotique");
			AddRes(index, typeof(Gold), "Pièce d'or", 100, "Vous n'avez pas suffisament de pièces d'or.");

			index = AddCraft(typeof(SceauEau), "Divers", "Sceau d'Eau", 25.0, 75.0, typeof(PalmierBoard), "Planches", 5, "Vous n'avez pas suffisament de planches");

			index = AddCraft(typeof(CoffreDore), "Divers", "Coffre doré", 40.0, 90.0, typeof(IronIngot), "Lingot de fer", 10, " vous n'avez pas suffisament de Lingot de fer");

			index = AddCraft(typeof(CoffreEnFer), "Divers", "Coffre en fer", 40.0, 90.0, typeof(IronIngot), "Lingot de fer", 10, " vous n'avez pas suffisament de Lingot de fer");


			#endregion
			#region Canons

			//	index = AddCraft(typeof(Cannonball), "Canons", "Boulet de Canon", 10.0, 60.0, typeof(IronIngot), "Lingot de Fer", 12, "Vous n'avez pas suffisament de lingot de fer");

			//			index = AddCraft(typeof(Grapeshot), "Canons", "Boulet Avancé", 15.0, 70.0, typeof(IronIngot), "Lingot de Fer", 12, "Vous n'avez pas suffisament de lingot de fer");
			//		AddRes(index, typeof(Cloth), "Tissus", 2, "Vous n'avez pas suffisament de tissus");

			//		index = AddCraft(typeof(LightShipCannonDeed), "Canons", "Canon Léger", 65.0, 100.0, typeof(IronIngot), "Lingot de Fer", 300, "Vous n'avez pas suffisament de lingot de fer");

			//		index = AddCraft(typeof(HeavyShipCannonDeed), "Canons", "Canon Lourd", 75.0, 110.0, typeof(IronIngot), "Lingot de Fer", 500, "Vous n'avez pas suffisament de lingot de fer");

			index = AddCraft(typeof(Charcoal), "Canons", "Charbon", 30.0, 50.0, typeof(PalmierBoard), "Planches", 1, "Vous n'avez pas suffisament de planches");
			
			index = AddCraft(typeof(BlackPowder), "Canons", "Poudre Noire", 50.0, 100.0, typeof(Charcoal), "Charbon", 3, "Vous n'avez pas suffisament de charbon");
			AddRes(index, typeof(SulfurousAsh), "Sulfure", 3, "Vous n'avez pas suffisament de sulfure");
			
			index = AddCraft(typeof(PowderCharge), "Canons", "Poudre à Canon", 50.0, 100.0, typeof(BlackPowder), "Poudre Noire", 4, "Vous n'avez pas suffisament de poudre noire");
			AddRes(index, typeof(Cloth), "Tissus", 1, "Vous n'avez pas suffisament de tissus");
			
			index = AddCraft(typeof(FuseCord), "Canons", "Mèche", 65.0, 110.0, typeof(Rope), "Corde", 1, "Vous n'avez pas suffisament de corde");
			AddBeverageRes(index, BeverageType.Water, 1, "Vous avez besoin d'un pichet d'eau avec 1 charge");
			AddRes(index, typeof(BlackPowder), "Poudre Noire", 1, "Vous n'avez pas suffisament de poudre noire");
			AddRes(index, typeof(Potash), "Potasse", 1, "Vous n'avez pas suffisament de potasse");

			index = AddCraft(typeof(Potash), "Canons", "Potasse", 15.0, 50.0, typeof(PalmierBoard), "Planches", 1, "Vous n'avez pas suffisament de planches");
			AddBeverageRes(index, BeverageType.Water, 1, "Vous avez besoin d'un pichet d'eau avec 1 charge");

			index = AddCraft(typeof(Ramrod), "Canons", "Baguette", 00.0, 50.0, typeof(PalmierBoard), "Planches", 8, "Vous n'avez pas suffisament de planches");

			index = AddCraft(typeof(Torch), "Canons", "Torche", 00.0, 50.0, typeof(PalmierBoard), "Planches", 3, "Vous n'avez pas suffisament de planches");
			AddRes(index, typeof(Cloth), "Tissus", 2, "Vous n'avez pas suffisament de tissus");

			index = AddCraft(typeof(Rope), "Canons", "Corde", 50.0, 100.0, typeof(Cloth), "Tissus", 10, "Vous avez besoin de plus de tissus");


			#endregion


			// Set the overridable material
			SetSubRes(typeof(IronIngot), 1044022);

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes(typeof(IronIngot), "Fer", 0.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(BronzeIngot), "Bronze", 0.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(CopperIngot), "Copper", 0.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(SonneIngot), "Sonne", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(ArgentIngot), "Argent", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(BorealeIngot), "Boréale", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(ChrysteliarIngot), "Chrysteliar", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(GlaciasIngot), "Glacias", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(LithiarIngot), "Lithiar", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(AcierIngot), "Acier", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(DurianIngot), "Durian", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(EquilibrumIngot), "Équilibrum", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(GoldIngot), "Or", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(JolinarIngot), "Jolinar", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(JusticiumIngot), "Justicium", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(AbyssiumIngot), "Abyssium", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(BloodiriumIngot), "Bloodirium", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(HerbrositeIngot), "Herbrosite", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(KhandariumIngot), "Khandarium", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(MytherilIngot), "Mytheril", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(SombralirIngot), "Sombralir", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(DraconyrIngot), "Draconyr", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(HeptazionIngot), "Heptazion", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(OceanisIngot), "Océanis", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(BraziumIngot), "Brazium", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(LuneriumIngot), "Lunerium", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(MarinarIngot), "Marinar", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(NostalgiumIngot), "Nostalgium", 100.0, "Vous n'avez pas les compétences requises pour forger ce métal.");


			// Set the overridable material for wood
			SetSubRes2(typeof(PalmierBoard), "Palmier");

			// Add every wood type you want the player to be able to choose from
			AddSubRes2(typeof(PalmierBoard), "Palmier", 0.0, "Vous ne savez pas travailler le bois Commun");
			AddSubRes2(typeof(ErableBoard), "Érable", 0.0, "Vous ne savez pas travailler le bois Érable");
			AddSubRes2(typeof(CedreBoard), "Cèdre", 20.0, "Vous ne savez pas travailler le bois Cèdre");
			AddSubRes2(typeof(CheneBoard), "Chêne", 20.0, "Vous ne savez pas travailler le bois Chêne");
			AddSubRes2(typeof(SauleBoard), "Saule", 40.0, "Vous ne savez pas travailler le bois Saule");
			AddSubRes2(typeof(CypresBoard), "Cyprès", 40.0, "Vous ne savez pas travailler le bois Cyprès");
			AddSubRes2(typeof(AcajouBoard), "Acajou", 60.0, "Vous ne savez pas travailler le bois Acajou");
			AddSubRes2(typeof(EbeneBoard), "Ébène", 60.0, "Vous ne savez pas travailler le bois Ébène");
			AddSubRes2(typeof(AmaranteBoard), "Amarante", 80.0, "Vous ne savez pas travailler le bois Amarante");
			AddSubRes2(typeof(PinBoard), "Pin", 80.0, "Vous ne savez pas travailler le bois Pin");
			AddSubRes2(typeof(AncienBoard), "Ancien", 100.0, "Vous ne savez pas travailler le bois ancien");


			MarkOption = true; Pratiquer = true;
			Repair = true;
			CanEnhance = true;
			CanAlter = true;
		}
	}

	public abstract class TrapCraft : CustomCraft
	{
		private LockableContainer m_Container;

		public LockableContainer Container => m_Container;

		public abstract TrapType TrapType { get; }

		public TrapCraft(Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, ITool tool, int quality)
			: base(from, craftItem, craftSystem, typeRes, tool, quality)
		{
		}

		private int Verify(LockableContainer container)
		{
			if (container == null || container.KeyValue == 0)
				return 1005638; // You can only trap lockable chests.
			if (From.Map != container.Map || !From.InRange(container.GetWorldLocation(), 2))
				return 500446; // That is too far away.
			if (!container.Movable)
				return 502944; // You cannot trap this item because it is locked down.
			if (!container.IsAccessibleTo(From))
				return 502946; // That belongs to someone else.
			if (container.Locked)
				return 502943; // You can only trap an unlocked object.
			if (container.TrapType != TrapType.None)
				return 502945; // You can only place one trap on an object at a time.

			return 0;
		}

		private bool Acquire(object target, out int message)
		{
			LockableContainer container = target as LockableContainer;

			message = Verify(container);

			if (message > 0)
			{
				return false;
			}
			else
			{
				m_Container = container;
				return true;
			}
		}

		public override void EndCraftAction()
		{
			From.SendLocalizedMessage(502921); // What would you like to set a trap on?
			From.Target = new ContainerTarget(this);
		}

		private class ContainerTarget : Target
		{
			private readonly TrapCraft m_TrapCraft;

			public ContainerTarget(TrapCraft trapCraft)
				: base(-1, false, TargetFlags.None)
			{
				m_TrapCraft = trapCraft;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				int message;

				if (m_TrapCraft.Acquire(targeted, out message))
					m_TrapCraft.CraftItem.CompleteCraft(m_TrapCraft.Quality, false, m_TrapCraft.From, m_TrapCraft.CraftSystem, m_TrapCraft.TypeRes, m_TrapCraft.Tool, m_TrapCraft);
				else
					Failure(message);
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				if (cancelType == TargetCancelType.Canceled)
					Failure(0);
			}

			private void Failure(int message)
			{
				Mobile from = m_TrapCraft.From;
				ITool tool = m_TrapCraft.Tool;

				if (Siege.SiegeShard)
				{
					AOS.Damage(from, Utility.RandomMinMax(80, 120), 50, 50, 0, 0, 0);
					message = 502902; // You fail to set the trap, and inadvertantly hurt yourself in the process.
				}

				if (tool != null && !tool.Deleted && tool.UsesRemaining > 0)
					from.SendGump(new CraftGump(from, m_TrapCraft.CraftSystem, tool, message));
				else if (message > 0)
					from.SendLocalizedMessage(message);
			}
		}

		public override Item CompleteCraft(out int message)
		{
			message = Verify(Container);

			if (message == 0)
			{
				int trapLevel = (int)(From.Skills.Tinkering.Value / 10);

				Container.TrapType = TrapType;
				Container.TrapPower = trapLevel * 9;
				Container.TrapLevel = trapLevel;
				Container.TrapOnLockpick = true;

				message = 1005639; // Trap is disabled until you lock the chest.
			}

			return null;
		}
	}

	[CraftItemID(0x1BFC)]
	public class DartTrapCraft : TrapCraft
	{
		public override TrapType TrapType => TrapType.DartTrap;

		public DartTrapCraft(Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, ITool tool, int quality)
			: base(from, craftItem, craftSystem, typeRes, tool, quality)
		{
		}
	}

	[CraftItemID(0x113E)]
	public class PoisonTrapCraft : TrapCraft
	{
		public override TrapType TrapType => TrapType.PoisonTrap;

		public PoisonTrapCraft(Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, ITool tool, int quality)
			: base(from, craftItem, craftSystem, typeRes, tool, quality)
		{
		}
	}

	[CraftItemID(0x370C)]
	public class ExplosionTrapCraft : TrapCraft
	{
		public override TrapType TrapType => TrapType.ExplosionTrap;

		public ExplosionTrapCraft(Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, ITool tool, int quality)
			: base(from, craftItem, craftSystem, typeRes, tool, quality)
		{
		}
	}
}
