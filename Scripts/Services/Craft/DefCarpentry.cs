using Server.Custom.Packaging.Packages;
using Server.Items;

using System;
using System.Linq;

namespace Server.Engines.Craft
{
	#region Mondain's Legacy
	public enum CarpRecipes
	{
		// stuff
		WarriorStatueSouth = 100,
		WarriorStatueEast = 101,
		SquirrelStatueSouth = 102,
		SquirrelStatueEast = 103,
		AcidProofRope = 104,
		OrnateElvenChair = 105,
		ArcaneBookshelfSouth = 106,
		ArcaneBookshelfEast = 107,
		OrnateElvenChestSouth = 108,
		ElvenDresserSouth = 109,
		ElvenDresserEast = 110,
		FancyElvenArmoire = 111,
		ArcanistsWildStaff = 112,
		AncientWildStaff = 113,
		ThornedWildStaff = 114,
		HardenedWildStaff = 115,
		TallElvenBedSouth = 116,
		TallElvenBedEast = 117,
		StoneAnvilSouth = 118,
		StoneAnvilEast = 119,
		OrnateElvenChestEast = 120,

		// arties
		PhantomStaff = 150,
		IronwoodCrown = 151,
		BrambleCoat = 152,

		SmallElegantAquarium = 153,
		WallMountedAquarium = 154,
		LargeElegantAquarium = 155,

		KotlBlackRod = 170,
		KotlAutomaton = 171,

		// K2
		[Description("Coffre Maritime")]
		CoffreMaritime = 50001,
		[Description("Grand Coffre")]
		FinishedWoodenChest = 50002,
		[Description("poulailler")]
		ChickenCoop = 50003,
		[Description("Poteau avec Chaine")]
		PoteauChaine = 50004,
		[Description("Tête de Licorne empaillée")]
		MountedDreadHorn = 50005,




	}
	#endregion

	public class DefCarpentry : CraftSystem
	{
		public override SkillName MainSkill => SkillName.Carpentry;

		//    public override int GumpTitleNumber => 1044004;

		public override string GumpTitleString => "Menuiserie";


		public override CraftECA ECA => CraftECA.ChanceMinusSixtyToFourtyFive;

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if (m_CraftSystem == null)
					m_CraftSystem = new DefCarpentry();

				return m_CraftSystem;
			}
		}
	
		public override double GetChanceAtMin(CraftItem item)
		{
			return 0.5; // 50%
		}

		private DefCarpentry()
			: base(1, 1, 1.25)// base( 1, 1, 3.0 )
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

		private readonly Type[] _RetainsColor = new[]
		{
			typeof(BasePlayerBB)
		};

		public override bool RetainsColorFrom(CraftItem item, Type type)
		{
			var itemType = item.ItemType;

			if (_RetainsColor.Any(t => t == itemType || itemType.IsSubclassOf(t)))
			{
				return true;
			}

			return base.RetainsColorFrom(item, type);
		}

		public override void PlayCraftEffect(Mobile from)
		{
			from.PlaySound(0x23D);
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


			#region Divers



			
			


			index = AddCraft(typeof(MatchLight), "Divers", "Allumettes", 0.0, 30.0, typeof(Kindling), "Petit Bois", 1, "Vous n'avez pas assez de petit bois.");
			index = AddCraft(typeof(BacVide), "Divers", "Bac Vide", 00.0, 20.0, typeof(PalmierBoard), 1044041, 3, 1044351);
			index = AddCraft(typeof(PalmierWoodResourceCrate), "Divers", "Caisse de ressource de bois", 10.0, 60.0, typeof(PalmierBoard), 1044041, 150, 1044351);
			index = AddCraft(typeof(BarrelStaves), "Divers", "Douve de Tonneau", 10, 30.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			index = AddCraft(typeof(BarrelLid), "Divers", "Couvercle de Tonneau", 10, 30.0, typeof(PalmierBoard), 1044041, 4, 1044351);
			index = AddCraft(typeof(BarrelHoops), "Divers", "Cercles de Tonneau", 10, 30.0, typeof(IronIngot), 1044036, 5, 1044037);
			index = AddCraft(typeof(LargeFishingPole), "Divers", "Canne à pêche", 10.0, 30.0, typeof(PalmierBoard), 1044041, 5, 1044351); //This is in the categor of Other during AoS
			AddRes(index, typeof(Cloth), 1044286, 5, 1044287);
			index = AddCraft(typeof(PipeCourte), "Divers", "Pipe Courte ", 20.0, 40.0, typeof(PalmierBoard), "Planches", 2, "Vous n'avez pas assez de planche.");
			index = AddCraft(typeof(PipeLongue), "Divers", "Pipe Longue", 25.0, 45.0, typeof(PalmierBoard), "Planches", 2, "Vous n'avez pas assez de planche.");
			index = AddCraft(typeof(PipeCourbee), "Divers", "Pipe Courbée", 30.0, 50.0, typeof(PalmierBoard), "Planches", 2, "Vous n'avez pas assez de planche.");
			index = AddCraft(typeof(Keg), "Divers", "Tonnelet", 40, 60, typeof(BarrelStaves), "Douve de Tonneau", 3, 1044253);
			AddRes(index, typeof(BarrelHoops), "Cercles de Tonneau", 1, 1044253);
			AddRes(index, typeof(BarrelLid), "Couvercle de Tonneau", 1, 1044253);
			ForceNonExceptional(index);
			index = AddCraft(typeof(ShortMusicStandLeft), "Divers", "Petit lutrin", 45.0, 65.0, typeof(PalmierBoard), 1044041, 15, 1044351);

			index = AddCraft(typeof(TrainingDummyEastDeed), "Divers", "Mannequin d'entrainement (E)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 55, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 60, 1044287);
			index = AddCraft(typeof(TrainingDummySouthDeed), "Divers", "Mannequin d'entrainement (S)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 55, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 60, 1044287);
			index = AddCraft(typeof(PickpocketDipEastDeed), "Divers", "Mannequin de vol à la tir (E)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 65, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 60, 1044287);
			index = AddCraft(typeof(PickpocketDipSouthDeed), "Divers", "Mannequin de vol à la tir (S)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 65, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 60, 1044287);

			index = AddCraft(typeof(AdvancedTrainingDummySouthDeed), "Divers", "Mannequin d'entrainement avancé (S)", 80.0, 110.0, typeof(TrainingDummySouthDeed), 1044336, 1, 1044253);
			ForceNonExceptional(index);
			index = AddCraft(typeof(AdvancedTrainingDummyEastDeed), "Divers", "Mannequin d'entrainement avancé (E)", 80.0, 110.0, typeof(TrainingDummyEastDeed), 1044335, 1, 1044253);
			ForceNonExceptional(index);
			index = AddCraft(typeof(LiquorBarrel), "Divers", "Tonneau d'alcool", 55.0, 75.0, typeof(BarrelStaves), "Douve de Tonneau", 4, 1044253);
			AddRes(index, typeof(BarrelHoops), "Cercles de Tonneau", 2, 1044253);
			AddRes(index, typeof(BarrelLid), "Couvercle de Tonneau", 1, 1044253);
			ForceNonExceptional(index);

			index = AddCraft(typeof(Watertub), "Divers", "Tonneau d'eau", 55.0, 75.0, typeof(BarrelStaves), "Douve de Tonneau", 4, 1044253);
			AddRes(index, typeof(BarrelHoops), "Cercles de Tonneau", 2, 1044253);
			AddRes(index, typeof(BarrelLid), "Couvercle de Tonneau", 1, 1044253);
			ForceNonExceptional(index);
			index = AddCraft(typeof(PlayerBBEast), "Divers", "Tableau d'affichage (E)", 60.0, 80.0, typeof(PalmierBoard), 1044041, 50, 1044351);
			AddRes(index, typeof(BlankScroll), "Parchemin Vierge", 30, "Vous n'avez pas suffisament de parchemin vierge");
			index = AddCraft(typeof(PlayerBBSouth), "Divers", "Tableau d'affichage (S)", 60.0, 80.0, typeof(PalmierBoard), 1044041, 50, 1044351);
			AddRes(index, typeof(BlankScroll), "Parchemin Vierge", 30, "Vous n'avez pas suffisament de parchemin vierge");
			index = AddCraft(typeof(TallMusicStandLeft), "Divers", "Grand lutrin", 60.0, 80.0, typeof(PalmierBoard), 1044041, 20, 1044351);
			index = AddCraft(typeof(EasleSouth), "Divers", "Chevalet", 65.0, 85.0, typeof(PalmierBoard), 1044041, 20, 1044351);
			index = AddCraft(typeof(ElvenPodium), "Divers", "Lutrin simple", 80.0, 100.0, typeof(PalmierBoard), 1044041, 20, 1044351);




			#endregion Divers
			index = AddCraft(typeof(CopyToolbox), "Batiment", "Outils de Charpente", 70.0, 110.0, typeof(Materiaux), "Matériaux", 50, "Vous n'avez pas assez de matériaux");
			ForceNonExceptional(index);
			

			#region Armes et Boucliers
			index = AddCraft(typeof(TrainingSword), "Armes et bouclier", "Épée d'entrainement", 0.0, 50.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			index = AddCraft(typeof(TrainingKryss), "Armes et bouclier", "Estoc d'entrainement", 0.0, 50.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			index = AddCraft(typeof(TrainingMace), "Armes et bouclier", "Masse d'entrainement", 0.0, 50.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			index = AddCraft(typeof(TrainingDoublelames), "Armes et bouclier", "Double lames d'entrainement", 0.0, 50.0, typeof(PalmierBoard), 1044041, 5, 1044351);

			index = AddCraft(typeof(ShepherdsCrook), "Armes et bouclier", "Bâton de berger", 25.0, 50.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(QuarterStaff), "Armes et bouclier", "Bâton", 25.0, 50.0, typeof(PalmierBoard), 1044041, 6, 1044351);
			index = AddCraft(typeof(GnarledStaff), "Armes et bouclier", "Bâton noueux", 25.0, 50.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(Bokuto), "Armes et bouclier", "Bokuto", 25.0, 50.0, typeof(PalmierBoard), 1044041, 6, 1044351);
			index = AddCraft(typeof(Fukiya), "Armes et bouclier", "Bâton de frappe", 40.0, 60.0, typeof(PalmierBoard), 1044041, 6, 1044351);
			index = AddCraft(typeof(Tetsubo), "Armes et bouclier", "Longue massue", 40.0, 60.0, typeof(PalmierBoard), 1044041, 10, 1044351);
			index = AddCraft(typeof(WildStaff), "Armes et bouclier", "Bâton sauvage", 40.0, 60.0, typeof(PalmierBoard), 1044041, 16, 1044351);
			index = AddCraft(typeof(Club), "Armes et bouclier", "Massue", 40.0, 60.0, typeof(PalmierBoard), 1044041, 9, 1044351);
			index = AddCraft(typeof(BlackStaff), "Armes et bouclier", "Bâton noir", 40.0, 60.0, typeof(PalmierBoard), 1044041, 9, 1044351);
			index = AddCraft(typeof(BatonDragonique), "Armes et bouclier", "Bâton Dragonique", 40.0, 60.0, typeof(PalmierBoard), 1044041, 9, 1044351);
			index = AddCraft(typeof(BatonErmite), "Armes et bouclier", "Bâton de l'Ermite", 55.0, 80.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(Eterfer), "Armes et bouclier", "Eterfer", 55.0, 80.0, typeof(PalmierBoard), 1044041, 6, 1044351);
			index = AddCraft(typeof(CanneSapphire), "Armes et bouclier", "Canne Sapphire", 70.0, 90.0, typeof(PalmierBoard), 1044041, 6, 1044351);
			index = AddCraft(typeof(Crochire), "Armes et bouclier", "Crochire", 70.0, 90.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(BatonVagabond), "Armes et bouclier", "Bâton de vagabond", 70.0, 90.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(WoodenShield), "Armes et bouclier", "Bouclier en bois", 70.0, 90.0, typeof(PalmierBoard), 1044041, 9, 1044351);
			#endregion
			#region Instruments
			index = AddCraft(typeof(LapHarp), "Instruments", "Petite harpe", 10.0, 30.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 10, 1044287);
			index = AddCraft(typeof(RuneLute), "Instruments", "Luth fin", 10.0, 30.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			AddRes(index, typeof(Cloth), "Cloth", 5, "You do not have enough cloth to make that.");
			index = AddCraft(typeof(Harp), "Instruments", "Grande Harpe", 10.0, 30.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 5, 1044287);
			index = AddCraft(typeof(Drums), "Instruments", "Tambour", 10.0, 30.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 5, 1044287);
			index = AddCraft(typeof(Lute), "Instruments", "Luth", 10.0, 30.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 5, 1044287);
			index = AddCraft(typeof(Tambourine), "Instruments", "Tambourine", 30.0, 60.0, typeof(PalmierBoard), 1044041, 15, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 5, 1044287);
			index = AddCraft(typeof(TambourineTassel), "Instruments", "Tambourine décorée", 30.0, 60.0, typeof(PalmierBoard), 1044041, 15, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 5, 1044287);
			index = AddCraft(typeof(BambooFlute), "Instruments", "Flûte de bambou", 30.0, 60.0, typeof(PalmierBoard), 1044041, 15, 1044351);
			index = AddCraft(typeof(AudChar), "Instruments", "Aude-Char", 30.0, 60.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 15, 1044287);
			index = AddCraft(typeof(GuitareSolo), "Instruments", "Guitare", 30.0, 60.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 15, 1044287);
			index = AddCraft(typeof(HarpeLongue), "Instruments", "Harpe Longue", 30.0, 60.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 15, 1044287);
			index = AddCraft(typeof(CelloDeed), "Instruments", "Cello", 30.0, 60.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 15, 1044287);
			index = AddCraft(typeof(TrumpetDeed), "Instruments", "Trompette", 30.0, 60.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 15, 1044287);
			index = AddCraft(typeof(pianomodernAddonDeed), "Instruments", "Piano (E)", 30.0, 60.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 15, 1044287);
			index = AddCraft(typeof(pianomodern2AddonDeed), "Instruments", "Piano (S)", 30.0, 60.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 15, 1044287);
			index = AddCraft(typeof(FireHorn), "Instruments", "Corne de feu", 80.0, 110.0, typeof(PalmierBoard), 1044041, 15, 1044351);
			#endregion Instruments

			#region Caisses et Coffres
			index = AddCraft(typeof(WoodenBox), "Caisses et coffres", "Coffret en bois", 20.0, 40.0, typeof(PalmierBoard), 1044041, 10, 1044351);
			index = AddCraft(typeof(SmallCrate), "Caisses et coffres", "Petite caisse", 15.0, 35.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(MediumCrate), "Caisses et coffres", "Moyenne caisse", 20.0, 40.0, typeof(PalmierBoard), 1044041, 15, 1044351);
			index = AddCraft(typeof(LargeCrate), "Caisses et coffres", "Grande caisse", 30.0, 50.0, typeof(PalmierBoard), 1044041, 18, 1044351);
			index = AddCraft(typeof(WoodenChest), "Caisses et coffres", "Coffre en bois", 40, 70.0, typeof(PalmierBoard), 1044041, 20, 1044351);
			index = AddCraft(typeof(PlainWoodenChest), "Caisses et coffres", "Grand coffre simple", 50.0, 80.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			index = AddCraft(typeof(OrnateWoodenChest), "Caisses et coffres", "Grand coffre orné", 50.0, 80.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			index = AddCraft(typeof(GildedWoodenChest), "Caisses et coffres", "Grand coffre renforcé", 50.0, 80.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			index = AddCraft(typeof(WoodenFootLocker), "Caisses et coffres", "Coffre à chaussures", 50.0, 80.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			index = AddCraft(typeof(CoffreMaritime), "Caisses et coffres", "Coffre Maritime", 60.0, 90.0, typeof(PalmierBoard), 1044041, 40, 1044351);
			AddRecipe(index, (int)CarpRecipes.CoffreMaritime);



			index = AddCraft(typeof(FinishedWoodenChest), "Caisses et coffres", "Grand coffre", 70.0, 90.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			AddRecipe(index, (int)CarpRecipes.FinishedWoodenChest);


			index = AddCraft(typeof(OrnateElvenChestSouthDeed), "Caisses et coffres", "Coffre elfique orné (S)", 90.0, 115.0, typeof(PalmierBoard), 1044041, 40, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(OrnateElvenChestEastDeed), "Caisses et coffres", "Coffre elfique orné (E)", 90.0, 115.0, typeof(PalmierBoard), 1044041, 40, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(RarewoodChest), "Caisses et coffres", "Coffre en bois", 80.0, 100.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			index = AddCraft(typeof(DecorativeBox), "Caisses et coffres", "Boite décorative", 80.0, 100.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			index = AddCraft(typeof(Chest), "Caisses et coffres", "Coffre", 80.0, 100.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			#endregion


			#region Chaises
			index = AddCraft(typeof(FootStool), "Chaises", "Petit tabouret", 5.0, 25.0, typeof(PalmierBoard), 1044041, 9, 1044351);
			index = AddCraft(typeof(Stool), "Chaises", "Tabouret", 10.0, 30.0, typeof(PalmierBoard), 1044041, 9, 1044351);
			index = AddCraft(typeof(BambooChair), "Chaises", "Chaise rustique", 15.0, 35.0, typeof(PalmierBoard), 1044041, 13, 1044351);
			index = AddCraft(typeof(WoodenChair), "Chaises", "Chaise simple", 20.0, 40.0, typeof(PalmierBoard), 1044041, 13, 1044351);
			index = AddCraft(typeof(FancyWoodenChairCushion), "Chaises", "Chaise travaillée", 25.0, 45.0, typeof(PalmierBoard), 1044041, 15, 1044351);
			index = AddCraft(typeof(WoodenChairCushion), "Chaises", "Chaise avec coussin", 30.0, 50.0, typeof(PalmierBoard), 1044041, 13, 1044351);
			index = AddCraft(typeof(Throne), "Chaises", "Trône massif", 40.0, 60.0, typeof(PalmierBoard), 1044041, 19, 1044351);
			index = AddCraft(typeof(OrnateElvenChair), "Chaises", "Chaise sculptée", 50.0, 70.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			index = AddCraft(typeof(BigElvenChair), "Chaises", "Chaise ornée", 55.0, 75.0, typeof(PalmierBoard), 1044041, 40, 1044351);
			index = AddCraft(typeof(ElvenReadingChair), "Chaises", "Chaise carrée", 60.0, 80.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			index = AddCraft(typeof(ElvenLoveseatSouthDeed), "Chaises", "Chaise élégante (S)", 80.0, 100.0, typeof(PalmierBoard), 1044041, 50, 1044351);
			SetDisplayID(index, 0x2DDF);
			ForceNonExceptional(index);
			index = AddCraft(typeof(ElvenLoveseatEastDeed), "Chaises", "Chaise élégante (E)", 80.0, 100.0, typeof(PalmierBoard), 1044041, 50, 1044351);
			SetDisplayID(index, 0x2DE0);
			ForceNonExceptional(index);
			index = AddCraft(typeof(FancyCouchEastDeed), "Chaises", "Canapé (E)", 60.0, 80.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			index = AddCraft(typeof(FancyCouchWestDeed), "Chaises", "Canapé (O)", 60.0, 80.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			index = AddCraft(typeof(FancyCouchSouthDeed), "Chaises", "Canapé (S)", 60.0, 80.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			index = AddCraft(typeof(FancyCouchNorthDeed), "Chaises", "Canapé (N)", 60.0, 80.0, typeof(PalmierBoard), 1044041, 30, 1044351);

			#endregion

			#region Tables
			index = AddCraft(typeof(Nightstand), "Tables", "Petite table", 20.0, 40.0, typeof(PalmierBoard), 1044041, 17, 1044351);
			index = AddCraft(typeof(WritingTable), "Tables", "Bureau d'éctriture", 35.0, 55.0, typeof(PalmierBoard), 1044041, 17, 1044351);
			index = AddCraft(typeof(LargeTable), "Tables", "Table large", 40.0, 60.0, typeof(PalmierBoard), 1044041, 27, 1044351);
			index = AddCraft(typeof(YewWoodTable), "Tables", "Table arrondie", 50.0, 70.0, typeof(PalmierBoard), 1044041, 23, 1044351);
			index = AddCraft(typeof(TableNappe), "Tables", "Table avec Nappe (Flip)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 23, 1044351);
			index = AddCraft(typeof(TableNappe2), "Tables", "Table avec Nappe Érable (Flip)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 23, 1044351);
			index = AddCraft(typeof(ComptoirNappe), "Tables", "Comptoir avec Nappe (Flip)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 23, 1044351);
			index = AddCraft(typeof(ElegantLowTable), "Tables", "Table basse élégante", 60.0, 80.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			index = AddCraft(typeof(PlainLowTable), "Tables", "Table basse simple", 60.0, 80.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			index = AddCraft(typeof(ShortCabinet), "Tables", "Table basse étroite", 70.0, 90.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			index = AddCraft(typeof(OrnateElvenTableSouthDeed), "Tables", "Table décorée (S)", 85.0, 110.0, typeof(PalmierBoard), 1044041, 60, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(OrnateElvenTableEastDeed), "Tables", "Table décorée (E)", 85.0, 110.0, typeof(PalmierBoard), 1044041, 60, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(FancyElvenTableSouthDeed), "Tables", "Table élégante (S)", 80.0, 105.0, typeof(PalmierBoard), 1044041, 50, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(FancyElvenTableEastDeed), "Tables", "Table élégante (E)", 80.0, 105.0, typeof(PalmierBoard), 1044041, 50, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(BarComptoir), "Tables", "Comptoir Bar", 80.0, 105.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(Comptoir), "Tables", "Comptoir", 80.0, 105.0, typeof(PalmierBoard), 1044041, 20, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(LongTableSouthDeed), "Tables", "Longue table (S)", 90.0, 115.0, typeof(PalmierBoard), 1044041, 80, 1044351);
			index = AddCraft(typeof(LongTableEastDeed), "Tables", "Longue table (E)", 90.0, 115.0, typeof(PalmierBoard), 1044041, 80, 1044351);
			index = AddCraft(typeof(AlchemistTableSouthDeed), "Tables", "Comptoir alchimique (S)", 85.0, 110.0, typeof(PalmierBoard), 1044041, 70, 1044351);
			SetDisplayID(index, 0x2DD4);
			ForceNonExceptional(index);
			index = AddCraft(typeof(AlchemistTableEastDeed), "Tables", "Comptoir alchimique (E)", 85.0, 110.0, typeof(PalmierBoard), 1044041, 70, 1044351);
			SetDisplayID(index, 0x2DD3);
			ForceNonExceptional(index);
			#endregion

			#region Armoires
			index = AddCraft(typeof(EmptyBookcase), "Armoires", "Bibliothèque vide", 30.0, 50.0, typeof(PalmierBoard), 1044041, 25, 1044351);
			index = AddCraft(typeof(FullBookcase), "Armoires", "Bibliothèque", 40.0, 60.0, typeof(PalmierBoard), 1044041, 25, 1044351);

			index = AddCraft(typeof(FancyArmoire), "Armoires", "Armoire travaillée", 50.0, 70.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			index = AddCraft(typeof(Armoire), "Armoires", "Armoire", 55.0, 75.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			index = AddCraft(typeof(TallCabinet), "Armoires", "Grande commode", 60.0, 80.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			index = AddCraft(typeof(RedArmoire), "Armoires", "Petite armoire", 60.0, 80.0, typeof(PalmierBoard), 1044041, 40, 1044351);
			index = AddCraft(typeof(ElegantArmoire), "Armoires", "Table de chevet", 60.0, 80.0, typeof(PalmierBoard), 1044041, 40, 1044351);
			index = AddCraft(typeof(MapleArmoire), "Armoires", "Petite armoire décorée", 60.0, 80.0, typeof(PalmierBoard), 1044041, 40, 1044351);
			index = AddCraft(typeof(CherryArmoire), "Armoires", "Petite armoire élégante", 60.0, 80.0, typeof(PalmierBoard), 1044041, 40, 1044351);
			index = AddCraft(typeof(ArcaneBookShelfDeedSouth), "Armoires", "Étagère arcanique (S)", 70.0, 90.0, typeof(PalmierBoard), 1044041, 80, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(ArcaneBookShelfDeedEast), "Armoires", "Étagère arcanique (E)", 70.0, 90.0, typeof(PalmierBoard), 1044041, 80, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(AcademicBookCase), "Armoires", "Bibliothèque académique", 60.0, 85.0, typeof(PalmierBoard), 1044041, 25, 1044351);
			index = AddCraft(typeof(ElvenWashBasinSouthWithDrawerDeed), "Armoires", "Commode avec vanité (S)", 70.0, 95.0, typeof(PalmierBoard), 1044041, 40, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(ElvenWashBasinEastWithDrawerDeed), "Armoires", "Commode avec vanité (E)", 70.0, 95.0, typeof(PalmierBoard), 1044041, 40, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(ElvenDresserDeedSouth), "Armoires", "Armoire travaillée (S)", 75.0, 100.0, typeof(PalmierBoard), 1044041, 45, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(ElvenDresserDeedEast), "Armoires", "Armoire travaillée (E)", 75.0, 100.0, typeof(PalmierBoard), 1044041, 45, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(FancyElvenArmoire), "Armoires", "Grande armoire travaillée", 80.0, 105.0, typeof(PalmierBoard), 1044041, 60, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(SimpleElvenArmoire), "Armoires", "Grande armoire élégante", 80.0, 105.0, typeof(PalmierBoard), 1044041, 60, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(Drawer), "Armoires", "Commode", 70.0, 95.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(FancyDrawer), "Armoires", "Commode huppée", 80.0, 105.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			ForceNonExceptional(index);
			index = AddCraft(typeof(TerMurDresserEastDeed), "Armoires", "Armoire élégante (E)", 90.0, 115.0, typeof(PalmierBoard), 1044041, 60, 1044351);
			index = AddCraft(typeof(TerMurDresserSouthDeed), "Armoires", "Armoire élégante (S)", 90.0, 115.0, typeof(PalmierBoard), 1044041, 60, 1044351);
			index = AddCraft(typeof(NormDresser), "Armoires", "Coiffeuse", 70.0, 115.0, typeof(PalmierBoard), 1044041, 40, 1044351);
			#endregion Armoires

			#region Lits
			index = AddCraft(typeof(SmallBedSouthDeed), "Lits", "Petit lit (S)", 40.0, 60.0, typeof(PalmierBoard), 1044041, 100, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 100, 1044287);
			index = AddCraft(typeof(SmallBedEastDeed), "Lits", "Petit lit (E)", 40.0, 60.0, typeof(PalmierBoard), 1044041, 100, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 100, 1044287);
			index = AddCraft(typeof(LargeBedSouthDeed), "Lits", "Grand lit (S)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 150, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 150, 1044287);
			index = AddCraft(typeof(LargeBedEastDeed), "Lits", "Grand lit (E)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 150, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 150, 1044287);
			index = AddCraft(typeof(TallElvenBedSouthDeed), "Lits", "Grand lit orné (S)", 60.0, 80.0, typeof(PalmierBoard), 1044041, 200, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 100, 1044287);
			ForceNonExceptional(index);
			index = AddCraft(typeof(TallElvenBedEastDeed), "Lits", "Grand lit orné (E)", 60.0, 80.0, typeof(PalmierBoard), 1044041, 200, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 100, 1044287);
			ForceNonExceptional(index);
			index = AddCraft(typeof(ElvenBedSouthDeed), "Lits", "Lit orné (S)", 70.0, 90.0, typeof(PalmierBoard), 1044041, 100, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 100, 1044287);
			ForceNonExceptional(index);
			index = AddCraft(typeof(ElvenBedEastDeed), "Lits", "Lit orné (E)", 70.0, 90.0, typeof(PalmierBoard), 1044041, 100, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 100, 1044287);
			ForceNonExceptional(index);
			#endregion
			#region Decoration
			index = AddCraft(typeof(RedHangingLantern), "Décorations", "Lanterne rouge suspendue", 65.0, 90.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			AddRes(index, typeof(BlankScroll), 1044377, 10, 1044378);
			index = AddCraft(typeof(WhiteHangingLantern), "Décorations", "Lanterne blanche suspendue", 65.0, 90.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			AddRes(index, typeof(BlankScroll), 1044377, 10, 1044378);
			index = AddCraft(typeof(ShojiScreen), "Décorations", "Paravent léger", 60.0, 80.0, typeof(PalmierBoard), 1044041, 75, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 60, 1044287);
			index = index = AddCraft(typeof(BambooScreen), "Décorations", "Paravent simple", 80.0, 105.0, typeof(PalmierBoard), 1044041, 75, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 60, 1044287);
			index = AddCraft(typeof(Paravent), "Décorations", "Paravent de bois", 60.0, 80.0, typeof(PalmierBoard), 1044041, 50, 1044351);
			index = AddCraft(typeof(Incubator), "Décorations", "Présentoir", 60.0, 115.0, typeof(PalmierBoard), 1044041, 100, 1044351);
			index = AddCraft(typeof(ChickenCoop), "Décorations", "Poulailler", 60.0, 115.0, typeof(PalmierBoard), 1044041, 150, 1044351);
			AddRecipe(index, (int)CarpRecipes.ChickenCoop);


			index = AddCraft(typeof(DartBoardSouthDeed), "Décorations", "Jeu de dards (S)", 20.0, 40.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			index = AddCraft(typeof(DartBoardEastDeed), "Décorations", "Jeu de dard (E)", 20.0, 40.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			index = AddCraft(typeof(VanityDeed), "Décorations", "Vanité", 60.0, 80.0, typeof(PalmierBoard), 1044041, 15, 1044351);
			//index = AddCraft(typeof(AbbatoirDeed), "Décorations", "Abattoir", 70.0, 125.0, typeof(PalmierBoard), 1044041, 100, 1044351);
			//AddRes(index, typeof(IronIngot), 1044036, 40, 1044037);

			index = AddCraft(typeof(PoteauChaine), "Décorations", "Poteau avec Chaine", 90.0, 115.0, typeof(PalmierBoard), 1044041, 10, 1044351);
			AddRes(index, typeof(IronIngot), "Lingot de fer", 3, "Vous n'avez pas suffisament de lingot de fer");
			AddRecipe(index, (int)CarpRecipes.PoteauChaine);

			#endregion Decoration

			#region Statues et Trophées
			index = AddCraft(typeof(ArcanistStatueSouthDeed), "Statues et trophés", "L'Arcaniste (S)", 0.0, 35.0, typeof(PalmierBoard), 1044041, 250, 1044351);
			index = AddCraft(typeof(ArcanistStatueEastDeed), "Statues et trophés", "L'Arcaniste (E)", 0.0, 35.0, typeof(PalmierBoard), 1044041, 250, 1044351);
			index = AddCraft(typeof(WarriorStatueSouthDeed), "Statues et trophés", "Le Guerrier (S)", 30.0, 55.0, typeof(PalmierBoard), 1044041, 250, 1044351);
			index = AddCraft(typeof(WarriorStatueEastDeed), "Statues et trophés", "Le Guerrier (E)", 30.0, 55.0, typeof(PalmierBoard), 1044041, 250, 1044351);
			index = AddCraft(typeof(SquirrelStatueSouthDeed), "Statues et trophés", "L'Écureuil (S)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 250, 1044351);
			index = AddCraft(typeof(SquirrelStatueEastDeed), "Statues et trophés", "L'Écureuil (E)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 250, 1044351);
			index = AddCraft(typeof(GiantReplicaAcorn), "Statues et trophés", "Gland géant sculpté", 80.0, 105.0, typeof(PalmierBoard), 1044041, 35, 1044351);
			index = AddCraft(typeof(MountedDreadHorn), "Statues et trophés", "Tête de licorne sculptée", 90.0, 115.0, typeof(PalmierBoard), 1044041, 50, 1044351);
			AddRecipe(index, (int)CarpRecipes.MountedDreadHorn);

			#endregion
			#region Grands Outils
			index = AddCraft(typeof(SewingMachineDeed), "Grands outils", "Machine à Coudre", 40.0, 60.0, typeof(PalmierBoard), 1044041, 30, 1044351);
			AddRes(index, typeof(IronIngot), 1044036, 15, 1044037);
			index = AddCraft(typeof(SpinningwheelEastDeed), "Grands outils", "Rouet (E)", 40.0, 60.0, typeof(PalmierBoard), 1044041, 75, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 25, 1044287);
			index = AddCraft(typeof(SpinningwheelSouthDeed), "Grands outils", "Rouet (S)", 40.0, 60.0, typeof(PalmierBoard), 1044041, 75, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 25, 1044287);
			index = AddCraft(typeof(ElvenSpinningwheelEastDeed), "Grands outils", "Rouet élégant (E)", 60.0, 80.0, typeof(PalmierBoard), 1044041, 60, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 40, 1044287);
			index = AddCraft(typeof(ElvenSpinningwheelSouthDeed), "Grands outils", "Rouet élégant (S)", 60.0, 80.0, typeof(PalmierBoard), 1044041, 60, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 40, 1044287);
			index = AddCraft(typeof(LoomEastDeed), "Grands outils", "Métier à tisser (E)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 85, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 25, 1044287);
			index = AddCraft(typeof(LoomSouthDeed), "Grands outils", "Métier à tisser (S)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 85, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 25, 1044287);
			index = AddCraft(typeof(DressformFront), "Grands outils", "Mannequin face", 40.0, 60.0, typeof(PalmierBoard), 1044041, 25, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 10, 1044287);
			index = AddCraft(typeof(DressformSide), "Grands outils", "Mannequin côté", 40.0, 60.0, typeof(PalmierBoard), 1044041, 25, 1044351);
			AddRes(index, typeof(Cloth), 1044286, 10, 1044287);
			index = AddCraft(typeof(FlourMillEastDeed), "Grands outils", "Moulin à farine (E)", 70.0, 90.0, typeof(PalmierBoard), 1044041, 100, 1044351);
			AddRes(index, typeof(IronIngot), 1044036, 50, 1044037);
			index = AddCraft(typeof(FlourMillSouthDeed), "Grands outils", "Moulin à farine (S)", 70.0, 90.0, typeof(PalmierBoard), 1044041, 100, 1044351);
			AddRes(index, typeof(IronIngot), 1044036, 50, 1044037);
			index = AddCraft(typeof(WaterTroughEastDeed), "Grands outils", "Abreuvoir (E)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 150, 1044351);
			index = AddCraft(typeof(WaterTroughSouthDeed), "Grands outils", "Abreuvoir (S)", 50.0, 70.0, typeof(PalmierBoard), 1044041, 150, 1044351);
			#endregion
			#region Teintures pour le bois

			index = AddCraft(typeof(BlancBoisDyeTub), "Teintures Bois", "Teinture Bois Vierge", 0.0, 0.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(PalmierBoard), "Planches Communes", 50, "Vous n'avez pas assez de planche.");
			AddRes(index, typeof(Pitcher), "Unité de pichet d'eau", 10, "Vous n'avez pas suffisament d'eau");

			index = AddCraft(typeof(ErableBoisDyeTub), "Teintures Bois", "Teinture Bois Érable", 0.0, 0.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(PalmierBoard), "Planches Érable", 50, "Vous n'avez pas assez de planche.");
			AddRes(index, typeof(Pitcher), "Unité de pichet d'eau", 10, "Vous n'avez pas suffisament d'eau");

			index = AddCraft(typeof(CedreBoisDyeTub), "Teintures Bois", "Teinture Bois Cèdre", 20.0, 40.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(CedreBoard), "Planches Cèdre", 50, "Vous n'avez pas assez de planche.");
			AddRes(index, typeof(Pitcher), "Unité de pichet d'eau", 10, "Vous n'avez pas suffisament d'eau");

			index = AddCraft(typeof(CheneBoisDyeTub), "Teintures Bois", "Teinture Bois Chêne", 20.0, 40.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(CheneBoard), "Planches Chene", 50, "Vous n'avez pas assez de planche.");
			AddRes(index, typeof(Pitcher), "Unité de pichet d'eau", 10, "Vous n'avez pas suffisament d'eau");

			index = AddCraft(typeof(SauleBoisDyeTub), "Teintures Bois", "Teinture Bois Saule", 40.0, 60.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(SauleBoard), "Planches Saule", 50, "Vous n'avez pas assez de planche.");
			AddRes(index, typeof(Pitcher), "Unité de pichet d'eau", 10, "Vous n'avez pas suffisament d'eau");

			index = AddCraft(typeof(CypresBoisDyeTub), "Teintures Bois", "Teinture Bois Cyprès", 40.0, 60.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(CypresBoard), "Planches Cyprès", 50, "Vous n'avez pas assez de planche.");
			AddRes(index, typeof(Pitcher), "Unité de pichet d'eau", 10, "Vous n'avez pas suffisament d'eau");

			index = AddCraft(typeof(AcajouBoisDyeTub), "Teintures Bois", "Teinture Bois Acajou", 50.0, 70.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(AcajouBoard), "Planches Acajou", 50, "Vous n'avez pas assez de planche.");
			AddRes(index, typeof(Pitcher), "Unité de pichet d'eau", 10, "Vous n'avez pas suffisament d'eau");

			index = AddCraft(typeof(EbeneBoisDyeTub), "Teintures Bois", "Teinture Bois Ébène", 50.0, 70.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(EbeneBoard), "Planches Ébène", 50, "Vous n'avez pas assez de planche.");
			AddRes(index, typeof(Pitcher), "Unité de pichet d'eau", 10, "Vous n'avez pas suffisament d'eau");

			index = AddCraft(typeof(AmaranteBoisDyeTub), "Teintures Bois", "Teinture Bois Amarante", 60.0, 80.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(AmaranteBoard), "Planches Amarante", 50, "Vous n'avez pas assez de planche.");
			AddRes(index, typeof(Pitcher), "Unité de pichet d'eau", 10, "Vous n'avez pas suffisament d'eau");

			index = AddCraft(typeof(PinBoisDyeTub), "Teintures Bois", "Teinture Bois Pin", 60.0, 80.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(PinBoard), "Planches Pin", 50, "Vous n'avez pas assez de planche.");
			AddRes(index, typeof(Pitcher), "Unité de pichet d'eau", 10, "Vous n'avez pas suffisament d'eau");

			index = AddCraft(typeof(AncienBoisDyeTub), "Teintures Bois", "Teinture Bois Ancien", 70.0, 90.0, typeof(BacVide), "Bac Vide", 1, "Il vous faut un bac de teinture");
			AddRes(index, typeof(AncienBoard), "Planches Ancien", 50, "Vous n'avez pas assez de planche.");
			AddRes(index, typeof(Pitcher), "Unité de pichet d'eau", 10, "Vous n'avez pas suffisament d'eau");



			#endregion

			MarkOption = true; Pratiquer = true;
			Repair = true;
			CanEnhance = true;
			CanAlter = true;

			// Set the overridable material
			SetSubRes(typeof(PalmierBoard), "Palmier");

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes(typeof(PalmierBoard), "Palmier", 0.0, "Vous ne savez pas travailler le bois Commun");
			AddSubRes(typeof(ErableBoard), "Érable", 0.0, "Vous ne savez pas travailler le bois Érable");
			AddSubRes(typeof(CedreBoard), "Cèdre", 20.0, "Vous ne savez pas travailler le bois Cèdre");
			AddSubRes(typeof(CheneBoard), "Chêne", 20.0, "Vous ne savez pas travailler le bois Chêne");
			AddSubRes(typeof(SauleBoard), "Saule", 40.0, "Vous ne savez pas travailler le bois Saule");
			AddSubRes(typeof(CypresBoard), "Cyprès", 40.0, "Vous ne savez pas travailler le bois Cyprès");
			AddSubRes(typeof(AcajouBoard), "Acajou", 60.0, "Vous ne savez pas travailler le bois Acajou");
			AddSubRes(typeof(EbeneBoard), "Ébène", 60.0, "Vous ne savez pas travailler le bois Ébène");
			AddSubRes(typeof(AmaranteBoard), "Amarante", 80.0, "Vous ne savez pas travailler le bois Amarante");
			AddSubRes(typeof(PinBoard), "Pin", 80.0, "Vous ne savez pas travailler le bois Pin");
			AddSubRes(typeof(AncienBoard), "Ancien", 100.0, "Vous ne savez pas travailler le bois ancien");
		}
	}
}
