using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Collections;



namespace Server.Engines.Craft
{
	public enum MasonryRecipes
	{
		AnniversaryVaseShort = 701,
		AnniversaryVaseTall = 702
	}

	public class DefMasonry : CraftSystem
	{
		public override SkillName MainSkill => SkillName.Carpentry;

		//    public override int GumpTitleNumber => 1044500;

		public override string GumpTitleString => "Maconnerie";

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if (m_CraftSystem == null)
					m_CraftSystem = new DefMasonry();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin(CraftItem item)
		{
			return 0.0; // 0% 
		}

		private DefMasonry()
			: base(1, 1, 1.25)// base( 1, 2, 1.7 ) 
		{
		}

		public override bool RetainsColorFrom(CraftItem item, Type type)
		{
			return true;
		}

		public override int CanCraft(Mobile from, ITool tool, Type itemType)
		{
			int num = 0;

			if (tool == null || tool.Deleted || tool.UsesRemaining <= 0)
				return 1044038; // You have worn out your tool!
			else if (tool is Item && !BaseTool.CheckTool((Item)tool, from))
				return 1048146; // If you have a tool equipped, you must use that tool.
			else if (!(from is PlayerMobile && ((PlayerMobile)from).Masonry && from.Skills[SkillName.Carpentry].Base >= 50.0))
				return 1044633; // You havent learned stonecraft.
			else if (!tool.CheckAccessible(from, ref num))
				return num; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect(Mobile from)
		{
		}

		// Delay to synchronize the sound with the hit on the anvil 
		private class InternalTimer : Timer
		{
			private readonly Mobile m_From;

			public InternalTimer(Mobile from)
				: base(TimeSpan.FromSeconds(0.7))
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.PlaySound(0x23D);
			}
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
			int index;
			// Décorations
			index = AddCraft(typeof(VaseEndommage), "Décorations", "Vase Endommagé", 30.0, 80.0, typeof(Granite), 1044514, 2, 1044513);
			index = AddCraft(typeof(PetitVaseLigne), "Décorations", "Petit Vase Ligné", 33.0, 83.0, typeof(Granite), 1044514, 2, 1044513);
			index = AddCraft(typeof(Vase), "Décorations", "Vase", 36.0, 86.0, typeof(Granite), 1044514, 1, 1044513);
			index = AddCraft(typeof(LargeVase), "Décorations", "Grand vase", 39.0, 89.0, typeof(Granite), 1044514, 3, 1044513);
			index = AddCraft(typeof(LargeVase2), "Décorations", "Grand Vase 2", 45.0, 95.0, typeof(Granite), 1044514, 4, 1044513);
			index = AddCraft(typeof(GrandVaseLigne), "Décorations", "Grand Vase Ligné", 48.0, 98.0, typeof(Granite), 1044514, 4, 1044513);
			index = AddCraft(typeof(VaseGobelet), "Décorations", "Vase Gobelet", 51.0, 101.0, typeof(Granite), 1044514, 4, 1044513);
			index = AddCraft(typeof(Urne3), "Décorations", "Urne 3", 54.0, 104.0, typeof(Granite), 1044514, 5, 1044513);
			index = AddCraft(typeof(VaseAnthracite), "Décorations", "Vase Anthracite", 57.0, 107.0, typeof(Granite), 1044514, 5, 1044513);
			index = AddCraft(typeof(VaseDore), "Décorations", "Vase Doré", 60.0, 110.0, typeof(Granite), 1044514, 5, 1044513);
			index = AddCraft(typeof(UrneBleue), "Décorations", "Urne Bleue", 63.0, 113.0, typeof(Granite), 1044514, 6, 1044513);
			index = AddCraft(typeof(VaseDecoratif), "Décorations", "Vase Décoratif", 66.0, 116.0, typeof(Granite), 1044514, 5, 1044513);
			index = AddCraft(typeof(UrneEvasee), "Décorations", "Urne Évasée", 69.0, 119.0, typeof(Granite), 1044514, 6, 1044513);
			index = AddCraft(typeof(VaseTravaille), "Décorations", "Vase Travaillé", 72.0, 122.0, typeof(Granite), 1044514, 6, 1044513);
			index = AddCraft(typeof(GrandeUrne), "Décorations", "Grande Urne", 75.0, 125.0, typeof(Granite), 1044514, 7, 1044513);
			index = AddCraft(typeof(VaseEgyptien), "Décorations", "Vase Egyptien", 78.0, 128.0, typeof(Granite), 1044514, 7, 1044513);
			index = AddCraft(typeof(VaseRunique), "Décorations", "Vase Runique", 81.0, 131.0, typeof(Granite), 1044514, 8, 1044513);
			index = AddCraft(typeof(VaseEgyptien2), "Décorations", "Vase Egyptien 2", 84.0, 134.0, typeof(Granite), 1044514, 8, 1044513);
			index = AddCraft(typeof(VaseRunique2), "Décorations", "Vase Runique 2", 87.0, 137.0, typeof(Granite), 1044514, 9, 1044513);
			index = AddCraft(typeof(VaseTresor), "Décorations", "Vase Trésor", 90.0, 140.0, typeof(Granite), 1044514, 10, 1044513);
			index = AddCraft(typeof(GargoyleVase), "Décorations", "Vase sculpté", 93.0, 143.0, typeof(Granite), 1044514, 3, 1044513);
			index = AddCraft(typeof(GargoylePainting), "Décorations", "Sculpture murale", 95.0, 152.0, typeof(Granite), 1044514, 5, 1044513);
			index = AddCraft(typeof(SmallUrn), "Décorations", "Petite urne", 96.0, 146.0, typeof(Granite), 1044514, 3, 1044513);
			index = AddCraft(typeof(SmallTowerSculpture), "Décorations", "Monolite", 99.0, 149.0, typeof(Granite), 1044514, 3, 1044513);
			index = AddCraft(typeof(PetiteFontaine), "Décorations", "Petite fontaine", 50.0, 100.0, typeof(Granite), 1044514, 5, 1044513);
			/*
						index = AddCraft(typeof(AnniversaryVaseTall), 1044501, 1156147, 60.0, 110.0, typeof(Granite), 1044514, 6, 1044513);
						AddRecipe(index, (int)MasonryRecipes.AnniversaryVaseTall);

						index = AddCraft(typeof(AnniversaryVaseShort), 1044501, 1156148, 60.0, 110.0, typeof(Granite), 1044514, 6, 1044513);
						AddRecipe(index, (int)MasonryRecipes.AnniversaryVaseShort);
			*/

			// Mobilier
			AddCraft(typeof(StoneChair), "Mobilier", "Chaise en granite", 55.0, 105.0, typeof(Granite), 1044514, 4, 1044513);
			AddCraft(typeof(MediumStoneTableEastDeed), "Mobilier", "Table en granite (E)", 65.0, 115.0, typeof(Granite), 1044514, 6, 1044513);
			AddCraft(typeof(MediumStoneTableSouthDeed), "Mobilier", "Table en granite (S)", 65.0, 115.0, typeof(Granite), 1044514, 6, 1044513);
			AddCraft(typeof(LargeStoneTableEastDeed), "Mobilier", "Grande table en granite (E)", 75.0, 125.0, typeof(Granite), 1044514, 9, 1044513);
			AddCraft(typeof(LargeStoneTableSouthDeed), "Mobilier", "Grande table en granite (S)", 75.0, 125.0, typeof(Granite), 1044514, 9, 1044513);
			AddCraft(typeof(RitualTableDeed), "Mobilier", "Table rituel", 94.7, 103.5, typeof(Granite), 1044514, 8, 1044513);
			index = AddCraft(typeof(LargeGargoyleBedSouthDeed), "Mobilier", "Grand lit sculpté (S)", 76.0, 115.0, typeof(Granite), 1044514, 20, 1044513);
			AddRes(index, typeof(Cloth), 1044286, 100, 1044287);
			index = AddCraft(typeof(LargeGargoyleBedEastDeed), "Mobilier", "Grand lit sculpté (E)", 76.0, 115.0, typeof(Granite), 1044514, 20, 1044513);
			AddRes(index, typeof(Cloth), 1044286, 100, 1044287);



			// Statues
			AddCraft(typeof(StatueGargoyleEast), "Statues", "Statue 5", 30.0, 80.0, typeof(Granite), 1044514, 20, 1044513);
			AddCraft(typeof(StatueGryphonEast), "Statues", "Statue 6", 35.0, 85.0, typeof(Granite), 1044514, 15, 1044513);
			AddCraft(typeof(StatueSouth), "Statues", "Statue 1", 40.0, 90.0, typeof(Granite), 1044514, 5, 1044513);
			AddCraft(typeof(StatueNorth), "Statues", "Statue 2", 42.0, 92.0, typeof(Granite), 1044514, 5, 1044513);
			AddCraft(typeof(StatueEast), "Statues", "Statue 3", 44.0, 94.0, typeof(Granite), 1044514, 5, 1044513);
			AddCraft(typeof(StatueSwan), "Statues", "Statue Swan", 46.0, 96.0, typeof(Granite), 1044514, 8, 1044513);
			AddCraft(typeof(StatueSwanNoir), "Statues", "Statue Swan Noir", 48.0, 98.0, typeof(Granite), 1044514, 9, 1044513);
			AddCraft(typeof(FauconDore), "Statues", "Faucon Doré", 50.0, 100.0, typeof(Granite), 1044514, 7, 1044513);
			AddCraft(typeof(StatueDon), "Statues", "Statue Don", 52.0, 102.0, typeof(Granite), 1044514, 10, 1044513);
			AddCraft(typeof(StatueJackal), "Statues", "Statue Jackal", 54.0, 104.0, typeof(Granite), 1044514, 11, 1044513);
			AddCraft(typeof(StatuePegasusSouth), "Statues", "Statue 4", 56.0, 106.0, typeof(Granite), 1044514, 10, 1044513);
			AddCraft(typeof(LutrinMarbre), "Statues", "Lutrin Marbre", 58.0, 108.0, typeof(Granite), 1044514, 15, 1044513);
			AddCraft(typeof(StatueMeduse), "Statues", "Statue Meduse", 60.0, 110.0, typeof(Granite), 1044514, 12, 1044513);
			AddCraft(typeof(StatueLion), "Statues", "Statue Lion", 62.0, 112.0, typeof(Granite), 1044514, 13, 1044513);
			AddCraft(typeof(StatueEgypte), "Statues", "Statue Egypte", 64.0, 114.0, typeof(Granite), 1044514, 14, 1044513);
			AddCraft(typeof(ColonneMarbre), "Statues", "Colonne Marbre", 66.0, 116.0, typeof(Granite), 1044514, 25, 1044513);
			AddCraft(typeof(StatueCheval), "Statues", "Statue Cheval", 68.0, 118.0, typeof(Granite), 1044514, 15, 1044513);
			AddCraft(typeof(StatueAnge), "Statues", "Statue Ange", 70.0, 120.0, typeof(Granite), 1044514, 16, 1044513);
			AddCraft(typeof(StatueGuerrier), "Statues", "Statue Guerrier", 72.0, 122.0, typeof(Granite), 1044514, 18, 1044513);
			AddCraft(typeof(AltarBlanc), "Statues", "Autel Blanc", 74.0, 124.0, typeof(Granite), 1044514, 30, 1044513);
			AddCraft(typeof(StatueDieu), "Statues", "Statue Dieu", 76.0, 126.0, typeof(Granite), 1044514, 20, 1044513); 

	/*		// Paliers
			index = AddCraft(typeof(CraftableHouseItem), "Paliers", "Palier 1", 60.0, 110.0, typeof(Granite), 1044514, 5, 1044513);
			SetData(index, CraftableItemType.RoughBlock);
			SetDisplayID(index, 1928);
			index = AddCraft(typeof(CraftableHouseItem), "Paliers", "Palier 2", 60.0, 110.0, typeof(Granite), 1044514, 5, 1044513);
			SetData(index, CraftableItemType.RoughSteps);
			SetDisplayID(index, 1929);
			index = AddCraft(typeof(CraftableHouseItem), "Paliers", "Palier 3", 60.0, 110.0, typeof(Granite), 1044514, 5, 1044513);
			SetData(index, CraftableItemType.RoughCornerSteps);
			SetDisplayID(index, 1934);
			index = AddCraft(typeof(CraftableHouseItem), "Paliers", "Palier 4", 60.0, 110.0, typeof(Granite), 1044514, 5, 1044513);
			SetData(index, CraftableItemType.RoughRoundedCornerSteps);
			SetDisplayID(index, 1938);
			index = AddCraft(typeof(CraftableHouseItem), "Paliers", "Palier 5", 60.0, 110.0, typeof(Granite), 1044514, 5, 1044513);
			SetData(index, CraftableItemType.RoughInsetSteps);
			SetDisplayID(index, 1941);
			index = AddCraft(typeof(CraftableHouseItem), "Paliers", "Palier 6", 60.0, 110.0, typeof(Granite), 1044514, 5, 1044513);
			SetData(index, CraftableItemType.RoughRoundedInsetSteps);
			SetDisplayID(index, 1945);*/

			// Cuisine
			AddCraft(typeof(StoneOvenEastDeed), "Cuisine", "Four en pierre (E)", 68.4, 93.4, typeof(Granite), "Granite", 40, "Vous n'avez pas suffisament de Granite");
			AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
			AddCraft(typeof(StoneOvenSouthDeed), "Cuisine", "Four en pierre (S)", 68.4, 93.4, typeof(Granite), "Granite", 40, "Vous n'avez pas suffisament de Granite");
			AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
			AddCraft(typeof(StoneFireplaceEastDeedExp), "Cuisine", "Foyer en pierre (E)", 68.4, 93.4, typeof(Granite), "Granite", 40, "Vous n'avez pas suffisament de Granite");
			AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
			AddCraft(typeof(StoneFireplaceSouthDeedExp), "Cuisine", "Foyer en pierre (S)", 68.4, 93.4, typeof(Granite), "Granite", 40, "Vous n'avez pas suffisament de Granite");
			AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
			AddCraft(typeof(GrayBrickFireplaceEastDeedExp), "Cuisine", "Foyer en brique (E)", 68.4, 93.4, typeof(Granite), "Granite", 40, "Vous n'avez pas suffisament de Granite");
			AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
			AddCraft(typeof(GrayBrickFireplaceSouthDeedExp), "Cuisine", "Foyer en brique (S)", 68.4, 93.4, typeof(Granite), "Granite", 40, "Vous n'avez pas suffisament de Granite");
			AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
			AddCraft(typeof(SandstoneFireplaceEastDeedExp), "Cuisine", "Foyer en grès (E)", 68.4, 93.4, typeof(Granite), "Granite", 40, "Vous n'avez pas suffisament de Granite");
			AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
			AddCraft(typeof(SandstoneFireplaceSouthDeedExp), "Cuisine", "Foyer en grès (S)", 68.4, 93.4, typeof(Granite), "Granite", 40, "Vous n'avez pas suffisament de Granite");
			AddSkill(index, SkillName.Tinkering, 50.0, 55.0);
			AddCraft(typeof(ElvenStoveSouthDeed), "Cuisine", "Four élégant (S)", 85.0, 110.0, typeof(Granite), "Granite", 40, "Vous n'avez pas suffisament de Granite");
			ForceNonExceptional(index);
			AddCraft(typeof(ElvenStoveEastDeed), "Cuisine", "Four élégant (E)", 85.0, 110.0, typeof(Granite), "Granite", 40, "Vous n'avez pas suffisament de Granite");
			ForceNonExceptional(index);

			AddCraft(typeof(EvierEnMarbre), "Cuisine", "Évier en marbre", 50.0, 100.0, typeof(Granite), "Granite", 20, "Vous n'avez pas suffisament de Granite");

			 AddCraft(typeof(FourDeBriqueEteint), "Cuisine", "Four de brique éteint", 50.0, 100.0, typeof(Granite), "Granite", 20, "Vous n'avez pas suffisament de Granite");

			 AddCraft(typeof(FourDeBriqueAllume), "Cuisine", "Four de brique allumé", 50.0, 100.0, typeof(Granite), "Granite", 20, "Vous n'avez pas suffisament de Granite");


			// Forge
			AddCraft(typeof(SmallForgeDeed), "Forge", "Petite forge", 73.6, 98.6, typeof(Granite), 1044514, 20, 1044513);
			AddSkill(index, SkillName.Blacksmith, 75.0, 80.0);
			AddRes(index, typeof(IronIngot), 1044036, 75, 1044037);
			AddCraft(typeof(LargeForgeEastDeed), "Forge", "Grande forge (E)", 78.9, 103.9, typeof(Granite), 1044514, 30, 1044513);
			AddSkill(index, SkillName.Blacksmith, 80.0, 85.0);
			AddRes(index, typeof(IronIngot), 1044036, 100, 1044037);
			AddCraft(typeof(LargeForgeSouthDeed), "Forge", "Grande forge (S)", 78.9, 103.9, typeof(Granite), 1044514, 30, 1044513);
			AddSkill(index, SkillName.Blacksmith, 80.0, 85.0);
			AddRes(index, typeof(IronIngot), 1044036, 100, 1044037);
			AddCraft(typeof(ElvenForgeDeed), "Forge", "Forge runique", 90.0, 110.0, typeof(Granite), 1044514, 30, 1044513);
			ForceNonExceptional(index);
			AddCraft(typeof(StoneAnvilSouthDeed), "Forge", "Anclume en granite (S)", 90.0, 115.0, typeof(Granite), 1044514, 30, 1044513);
			AddCraft(typeof(StoneAnvilEastDeed), "Forge", "Anclume en granite (E)", 90.0, 115.0, typeof(Granite), 1044514, 30, 1044513);


			
						// Stone Weapons
				//		AddCraft(typeof(StoneWarSword), 1111719, 1022304, 55.0, 105.0, typeof(Granite), 1044514, 18, 1044513);
			
			/*          // Stone Walls
					  index = AddCraft(typeof(CraftableHouseItem), 1155792, 1155794, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, CraftableItemType.RoughWindowless);
					  SetDisplayID(index, 464);

					  index = AddCraft(typeof(CraftableHouseItem), 1155792, 1155797, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, CraftableItemType.RoughWindow);
					  SetDisplayID(index, 467);

					  index = AddCraft(typeof(CraftableHouseItem), 1155792, 1155799, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, CraftableItemType.RoughArch);
					  SetDisplayID(index, 469);

					  index = AddCraft(typeof(CraftableHouseItem), 1155792, 1155804, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, CraftableItemType.RoughPillar);
					  SetDisplayID(index, 474);

					  index = AddCraft(typeof(CraftableHouseItem), 1155792, 1155805, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, CraftableItemType.RoughRoundedArch);
					  SetDisplayID(index, 475);

					  index = AddCraft(typeof(CraftableHouseItem), 1155792, 1155810, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, CraftableItemType.RoughSmallArch);
					  SetDisplayID(index, 480);

					  index = AddCraft(typeof(CraftableHouseItem), 1155792, 1155814, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, CraftableItemType.RoughAngledPillar);
					  SetDisplayID(index, 486);

					  index = AddCraft(typeof(CraftableHouseItem), 1155792, 1155816, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, CraftableItemType.ShortRough);
					  SetDisplayID(index, 488);

					  index = AddCraft(typeof(CraftableStoneHouseDoor), 1155792, 1156078, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, DoorType.StoneDoor_S_In);
					  SetDisplayID(index, 804);
					  AddCreateItem(index, CraftableStoneHouseDoor.Create);

					  index = AddCraft(typeof(CraftableStoneHouseDoor), 1155792, 1156079, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, DoorType.StoneDoor_E_Out);
					  SetDisplayID(index, 805);
					  AddCreateItem(index, CraftableStoneHouseDoor.Create);

					  AddCraft(typeof(CraftableStoneHouseDoor), 1155792, 1156348, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, DoorType.StoneDoor_S_Out);
					  SetDisplayID(index, 804);
					  AddCreateItem(index, CraftableStoneHouseDoor.Create);

					  AddCraft(typeof(CraftableStoneHouseDoor), 1155792, 1156349, 60.0, 110.0, typeof(Granite), 1044514, 10, 1044513);
					  SetData(index, DoorType.StoneDoor_E_In);
					  SetDisplayID(index, 805);
					  AddCreateItem(index, CraftableStoneHouseDoor.Create);

					  // Stone Floors
					  index = AddCraft(typeof(CraftableHouseItem), 1155877, 1155878, 60.0, 110.0, typeof(Granite), 1044514, 5, 1044513);
					  SetData(index, CraftableItemType.LightPaver);
					  SetDisplayID(index, 1305);

					  index = AddCraft(typeof(CraftableHouseItem), 1155877, 1155879, 60.0, 110.0, typeof(Granite), 1044514, 5, 1044513);
					  SetData(index, CraftableItemType.MediumPaver);
					  SetDisplayID(index, 1309);

					  index = AddCraft(typeof(CraftableHouseItem), 1155877, 1155880, 60.0, 110.0, typeof(Granite), 1044514, 5, 1044513);
					  SetData(index, CraftableItemType.DarkPaver);
					  SetDisplayID(index, 1313);
		  */
			MarkOption = true; Pratiquer = true;
			Repair = true;
			CanEnhance = true;

			SetSubRes(typeof(Granite), "Granite");

			AddSubRes(typeof(Granite), "Granite", 0.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(BronzeGranite), "Granite de Bronze", 0.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(CopperGranite), "Granite de Copper", 0.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(SonneGranite), "Granite de Sonne", 20.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(ArgentGranite), "Granite de Argent", 20.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(BorealeGranite), "Granite de Boréale", 20.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(ChrysteliarGranite), "Granite de Chrysteliar", 20.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(GlaciasGranite), "Granite de Glacias", 20.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(LithiarGranite), "Granite de Lithiar", 20.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(AcierGranite), "Granite de Acier", 40.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(DurianGranite), "Granite de Durian", 40.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(EquilibrumGranite), "Granite de Équilibrum", 40.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(GoldGranite), "Granite de Or", 40.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(JolinarGranite), "Granite de Jolinar", 40.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(JusticiumGranite), "Granite de Justicium", 40.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(AbyssiumGranite), "Granite de Abyssium", 60.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(BloodiriumGranite), "Granite de Bloodirium", 60.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(HerbrositeGranite), "Granite de Herbrosite", 60.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(KhandariumGranite), "Granite de Khandarium", 60.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(MytherilGranite), "Granite de Mytheril", 60.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(SombralirGranite), "Granite de Sombralir", 60.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(DraconyrGranite), "Granite de Draconyr", 80.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(HeptazionGranite), "Granite de Heptazion", 80.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(OceanisGranite), "Granite de Océanis", 80.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(BraziumGranite), "Granite de Brazium", 80.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(LuneriumGranite), "Granite de Lunerium", 80.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(MarinarGranite), "Granite de Marinar", 80.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
			AddSubRes(typeof(NostalgiumGranite), "Granite de Nostalgium", 90.0, "Vous n'avez pas les compétences requises pour travailler ce granite.");
		
		}
	}
}
