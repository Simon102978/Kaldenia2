using Server.Engines.Quests;
using Server.Items;

using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace Server.Engines.Craft
{
	public class DefBotanique : CraftSystem
	{
		public override SkillName MainSkill => SkillName.Botanique;

		//    public override int GumpTitleNumber => 1044004;

		public override string GumpTitleString => "Jardinage";

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefBotanique();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin(CraftItem item)
		{
			return 0.5; // 50%
		}

		private DefBotanique()
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

		public override void PlayCraftEffect( Mobile from )
        	{

            from.PlaySound(0x55);
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

		public override void InitCraftList()
		{
            int index = -1;


			#region Bacs et Terre Fertile

			index = AddCraft(typeof(BacVideTerre), "Bacs et Terre Fertile", "Bac Vide avec Terre", 10.0, 30.0, typeof(PalmierBoard), "Bois", 10, "Vous n'avez pas assez de bois");


			index = AddCraft(typeof(BacVideSable), "Bacs et Terre Fertile", "Bac Vide avec Sable", 15.0, 35.0, typeof(PalmierBoard), "Bois", 10, "Vous n'avez pas assez de bois");
			AddRes(index, typeof(Sand), "Sable", 5, "Vous n'avez pas assez de sable");



			index = AddCraft(typeof(Presentoir), "Bacs et Terre Fertile", "Presentoir", 25.0, 45.0, typeof(PalmierBoard), "Bois", 10, "Vous n'avez pas assez de bois");

			// Baril présentoire
			index = AddCraft(typeof(BarilPresentoire), "Bacs et Terre Fertile", "Baril Présentoire", 20.0, 70.0, typeof(PalmierBoard), "Planches", 10, "Vous n'avez pas assez de bois");
			AddRes(index, typeof(FertileDirt), "Terre Fertile", 10, " Vous n'avez pas suffisament de Terre Fertile");

			// Baril présentoire au sol
			index = AddCraft(typeof(BarilPresentoireSol), "Bacs et Terre Fertile", "Baril Présentoire au Sol", 20.0, 70.0, typeof(PalmierBoard), "Planches", 10, "Vous n'avez pas assez de bois");
			AddRes(index, typeof(FertileDirt), "Terre Fertile", 10, " Vous n'avez pas suffisament de Terre Fertile");

			// Caisse présentoire
			index = AddCraft(typeof(CaissePresentoire), "Bacs et Terre Fertile", "Caisse Présentoire", 20.0, 70.0, typeof(PalmierBoard), "Planches", 10, "Vous n'avez pas assez de bois");
			AddRes(index, typeof(FertileDirt), "Terre Fertile", 10, " Vous n'avez pas suffisament de Terre Fertile");

			// Citrouille décoratives
			index = AddCraft(typeof(CitrouilleDecorative), "Bacs et Terre Fertile", "Citrouille Décorative", 30.0, 80.0, typeof(FertileDirt), "Terre Fertile", 5, " Vous n'avez pas suffisament de Terre Fertile");

			// Herbes suspendues
			index = AddCraft(typeof(HerbesSuspendues), "Bacs et Terre Fertile", "Herbes Suspendues", 20.0, 70.0, typeof(FertileDirt), "Terre Fertile", 2, " Vous n'avez pas suffisament de Terre Fertile");


			#endregion
			#region Cactus
			index = AddCraft(typeof(Cactus1), "Cactus", "Petit Cactus", 25.0, 45.0, typeof(FertileDirt), "Terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(BacVideSable), "Bac Vide avec Sable", 1, "il vous manque un banque vide avec sable");

			index = AddCraft(typeof(Cactus2), "Cactus", "Cactus Moyen", 30.0, 50.0, typeof(FertileDirt), "terre fertile", 2, "Vous n'avez pas assez de terre fertile");
			AddRes(index, typeof(BacVideSable), "Bac Vide avec Sable", 1, "il vous manque un banque vide avec sable");

			index = AddCraft(typeof(Cactus3), "Cactus", "Grand Cactus", 35.0, 55.0, typeof(FertileDirt), "terre fertile", 3, "Vous n'avez pas assez de terre fertile");
			AddRes(index, typeof(BacVideSable), "Bac Vide avec Sable", 1, "il vous manque un banque vide avec sable");


			index = AddCraft(typeof(Cactus4), "Cactus", "Cactus Ramifié", 40.0, 60.0, typeof(FertileDirt), "terre fertile", 4, "Vous n'avez pas assez de terre fertile");
			AddRes(index, typeof(BacVideSable), "Bac Vide avec Sable", 1, "il vous manque un banque vide avec sable");


			index = AddCraft(typeof(Cactus5), "Cactus", "Cactus Boule", 45.0, 65.0, typeof(FertileDirt), "terre fertile", 5, "Vous n'avez pas assez de terre fertile");
			AddRes(index, typeof(BacVideSable), "Bac Vide avec Sable", 1, "il vous manque un banque vide avec sable");


			index = AddCraft(typeof(Cactus6), "Cactus", "Cactus Géant", 50.0, 70.0, typeof(FertileDirt), "terre fertile", 6, "Vous n'avez pas assez de terre fertile");
			AddRes(index, typeof(BacVideSable), "Bac Vide avec Sable", 1, "il vous manque un banque vide avec sable");





			#endregion
			#region Bacs Arbustres

			index = AddCraft(typeof(BacArbutre1), "Bacs Arbustres", "Petit Arbutre", 30.0, 50.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre2), "Bacs Arbustres", "Arbutre Moyen", 35.0, 55.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre3), "Bacs Arbustres", "Grand Arbutre", 40.0, 60.0, typeof(FertileDirt), "Terre Fertile", 3, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre4), "Bacs Arbustres", "Arbutre Touffu", 45.0, 65.0, typeof(FertileDirt), "Terre Fertile", 4, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre5), "Bacs Arbustres", "Arbutre Décoratif", 50.0, 70.0, typeof(FertileDirt), "Terre Fertile", 5, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre6), "Bacs Arbustres", "Arbutre Exotique", 55.0, 75.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre7), "Bacs Arbustres", "Arbutre Rare", 60.0, 80.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre8), "Bacs Arbustres", "Arbutre de Luxe", 65.0, 85.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbre), "Bacs et Terre Fertile", "Bac avec arbre pour Arbre", 70.0, 90.0, typeof(PalmierBoard), "Bois", 15, "Vous n'avez pas assez de bois");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");



			#endregion

			#region Plantes Suspendues

			index = AddCraft(typeof(CrochetPlante), "Plantes Suspendues", "Crochet pour Plante", 20.0, 40.0, typeof(IronIngot), "Lingot de Fer", 2, "Vous n'avez pas de lingot de fer");

			index = AddCraft(typeof(Plantesuspendue1), "Plantes Suspendues", "Petite Plante Suspendue", 25.0, 45.0, typeof(FertileDirt), "terre fertile", 2, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");

			index = AddCraft(typeof(Plantesuspendue2), "Plantes Suspendues", "Plante Suspendue Moyenne", 30.0, 50.0, typeof(FertileDirt), "terre fertile", 2, "Vous n'avez pas assez de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");

			index = AddCraft(typeof(Plantesuspendue3), "Plantes Suspendues", "Grande Plante Suspendue", 35.0, 55.0, typeof(FertileDirt), "terre fertile", 3, "Vous n'avez pas assez de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");

			index = AddCraft(typeof(Plantesuspendue4), "Plantes Suspendues", "Plante Suspendue Fleurie", 40.0, 60.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");

			index = AddCraft(typeof(Plantesuspendue5), "Plantes Suspendues", "Plante Suspendue Exotique", 45.0, 65.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");

			index = AddCraft(typeof(Plantesuspendue6), "Plantes Suspendues", "Plante Suspendue Rare", 50.0, 70.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");

			index = AddCraft(typeof(Plantesuspendue7), "Plantes Suspendues", "Plante Suspendue de Luxe", 55.0, 75.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");

			index = AddCraft(typeof(Plantesuspendue8), "Plantes Suspendues", "Plante Suspendue Magique", 60.0, 80.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");

			index = AddCraft(typeof(Plantesuspendue9), "Plantes Suspendues", "Plante Suspendue Légendaire", 65.0, 85.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");

			// Vignes pendantes
			index = AddCraft(typeof(VignesPendantes), "Plantes Suspendues", "Vignes Pendantes", 30.0, 80.0, typeof(FertileDirt), "terre fertile", 3, "Vous n'avez pas de terre fertile");

			// Fleurs blanche suspendues
			index = AddCraft(typeof(FleursBlanches), "Plantes Suspendues", "Fleurs Blanches Suspendues", 80.0, 130.0, typeof(FertileDirt), "terre fertile", 3, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");

			// Marguerites suspendues
			index = AddCraft(typeof(MargueritesSuspendues), "Plantes Suspendues", "Marguerites Suspendues", 80.0, 130.0, typeof(FertileDirt), "terre fertile", 3, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");

			// Fleurs roses suspendues
			index = AddCraft(typeof(FleursRosesSuspendues), "Plantes Suspendues", "Fleurs Roses Suspendues", 80.0, 130.0, typeof(FertileDirt), "terre fertile", 3, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(CrochetPlante), "Crochet pour Plante", 1, "Vous n'avez pas de crochet pour plante");


			#endregion
			#region Plantes en Vase

			index = AddCraft(typeof(PlanteVase1), "Plantes en Vase", "Petite Plante en Vase", 30.0, 50.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(Vase), "Vase", 1, "Vous n'avez pas de vase");

			index = AddCraft(typeof(PlanteVase2), "Plantes en Vase", "Plante en Vase Moyenne", 35.0, 55.0, typeof(FertileDirt), "terre fertile", 2, "Vous n'avez pas assez de terre fertile");
			AddRes(index, typeof(Vase), "Vase", 1, "Vous n'avez pas de vase");

			index = AddCraft(typeof(PlanteVase3), "Plantes en Vase", "Grande Plante en Vase", 40.0, 60.0, typeof(FertileDirt), "terre fertile", 3, "Vous n'avez pas assez de terre fertile");
			AddRes(index, typeof(Vase), "Vase", 1, "Vous n'avez pas de vase");

			index = AddCraft(typeof(PlanteVase4), "Plantes en Vase", "Plante en Vase Fleurie", 45.0, 65.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(Vase), "Vase", 1, "Vous n'avez pas de vase");

			index = AddCraft(typeof(PlanteVase5), "Plantes en Vase", "Plante en Vase Exotique", 50.0, 70.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(Vase), "Vase", 1, "Vous n'avez pas de vase");

			index = AddCraft(typeof(PlanteVase6), "Plantes en Vase", "Plante en Vase Rare", 55.0, 75.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(Vase), "Vase", 1, "Vous n'avez pas de vase");

			index = AddCraft(typeof(PlanteVase7), "Plantes en Vase", "Plante en Vase de Luxe", 60.0, 80.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(Vase), "Vase", 1, "Vous n'avez pas de vase");

			index = AddCraft(typeof(PlanteVase8), "Plantes en Vase", "Plante en Vase Magique", 65.0, 85.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(Vase), "Vase", 1, "Vous n'avez pas de vase");

			index = AddCraft(typeof(PlanteVase9), "Plantes en Vase", "Plante en Vase Légendaire", 70.0, 90.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(Vase), "Vase", 1, "Vous n'avez pas de vase");

			index = AddCraft(typeof(PlanteVase10), "Plantes en Vase", "Plante en Vase Ancestrale", 75.0, 95.0, typeof(FertileDirt), "terre fertile", 1, "Vous n'avez pas de terre fertile");
			AddRes(index, typeof(Vase), "Vase", 1, "Vous n'avez pas de vase");

			#endregion

			#region Bouquets

			index = AddCraft(typeof(Bouquet1), "Bouquets", "Bouquet Simple", 20.0, 40.0, typeof(RedRose), "Rose Rouge", 3, "Vous n'avez pas de Rose sauvage");

			index = AddCraft(typeof(Bouquet2), "Bouquets", "Bouquet Élégant", 25.0, 45.0, typeof(RedRose), "Rose Rouge", 5, "Vous n'avez pas assez de fleurs");

			index = AddCraft(typeof(Bouquet3), "Bouquets", "Bouquet Somptueux", 30.0, 50.0, typeof(RedRose), "Rose Rouge", 7, "Vous n'avez pas assez de fleurs");

			#endregion

			#region Pots de Fleurs et Arbres

		

			index = AddCraft(typeof(LargePotdeFleur), "Pot de Fleurs", "Grand Pot de Fleur", 30.0, 50.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas assez Terre Fertile");
			AddRes(index, typeof(Pitcher), 1022544, 1, 1044253);
			SetBeverageType(index, BeverageType.Water);
			index = AddCraft(typeof(Arbrenepot), "Pot d'Arbres", "Pot d'Arbre", 25.0, 45.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas assez Terre Fertile");
			AddRes(index, typeof(Pitcher), 1022544, 1, 1044253);
			SetBeverageType(index, BeverageType.Water);
			index = AddCraft(typeof(LargeArbreenpot), "Pot d'Arbres", "Grand Pot d'Arbre", 35.0, 55.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas assez Terre Fertile");
			AddRes(index, typeof(Pitcher), 1022544, 1, 1044253);
			SetBeverageType(index, BeverageType.Water);
			index = AddCraft(typeof(PotdeFleur1), "Pot de Fleurs", "Pot de Fleur 1", 22.0, 42.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas assez Terre Fertile");
			AddRes(index, typeof(Pitcher), 1022544, 1, 1044253);
			SetBeverageType(index, BeverageType.Water);
			index = AddCraft(typeof(Potdefleur2), "Pot de Fleurs", "Pot de Fleur 2", 24.0, 44.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas assez Terre Fertile");
			AddRes(index, typeof(Pitcher), 1022544, 1, 1044253);
			SetBeverageType(index, BeverageType.Water);
			index = AddCraft(typeof(PotdeFleur3), "Pot de Fleurs", "Pot de Fleur 3", 26.0, 46.0, typeof(FertileDirt), "Terre Fertile", 3, "Vous n'avez pas assez Terre Fertile");
			AddRes(index, typeof(Pitcher), 1022544, 1, 1044253);
			SetBeverageType(index, BeverageType.Water);
			#endregion

			#region Terre Fertile d'Arbres Morts

			index = AddCraft(typeof(PotArbreMort1), "Terre Fertile d'Arbres Morts", "Pot d'Arbre Mort 1", 30.0, 50.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas assez Terre Fertile");

			index = AddCraft(typeof(PotArbreMort2), "Terre Fertile d'Arbres Morts", "Pot d'Arbre Mort 2", 35.0, 55.0, typeof(FertileDirt), "Terre Fertile", 3, "Vous n'avez pas assez Terre Fertile");

			#endregion

			#region Fleurs de Mariage et Arches

			index = AddCraft(typeof(FleurMariage1), "Fleurs de Mariage", "Fleur de Mariage 1", 40.0, 60.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas assez Terre Fertile");

			index = AddCraft(typeof(FleurMariage2), "Fleurs de Mariage", "Fleur de Mariage 2", 45.0, 65.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas assez Terre Fertile");

			index = AddCraft(typeof(FleurMariage3), "Fleurs de Mariage", "Fleur de Mariage 3", 50.0, 70.0, typeof(FertileDirt), "Terre Fertile", 3, "Vous n'avez pas assez Terre Fertile");

			index = AddCraft(typeof(ArcheMariageSouth1), "Arches de Mariage", "Arche de Mariage Sud 1", 60.0, 80.0,typeof(FertileDirt), "Terre Fertile", 10, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(FleurMariage1), "Fleur de Mariage 1", 1, "Vous n'avez pas assez de fleurs de mariage 1");

			index = AddCraft(typeof(ArcheMariageSouth2), "Arches de Mariage", "Arche de Mariage Sud 2", 65.0, 85.0,typeof(FertileDirt), "Terre Fertile", 12, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(FleurMariage2), "Fleur de Mariage 2", 2, "Vous n'avez pas assez de fleurs de mariage 2");

			index = AddCraft(typeof(ArcheMariageEast1), "Arches de Mariage", "Arche de Mariage Est 1", 70.0, 90.0,typeof(FertileDirt), "Terre Fertile", 14, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(FleurMariage3), "Fleur de Mariage 3", 3, "Vous n'avez pas assez de fleurs de mariage 3");

			index = AddCraft(typeof(ArcheMariageEast2), "Arches de Mariage", "Arche de Mariage Est 2", 75.0, 95.0,typeof(FertileDirt), "Terre Fertile", 16, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(FleurMariage1), "Fleur de Mariage 1", 1, "Vous n'avez pas assez de fleurs de mariage 1");

			#endregion

			#region Autres Plantes Décoratives

			index = AddCraft(typeof(Bonsai1), "Bonsaïs", "Bonsaï 1", 50.0, 70.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas assez de Terre Fertile");

			index = AddCraft(typeof(Bonsai2), "Bonsaïs", "Bonsaï 2", 55.0, 75.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas assez de Terre Fertile");

			index = AddCraft(typeof(Bonsai3), "Bonsaïs", "Bonsaï 3", 60.0, 80.0, typeof(FertileDirt), "Terre Fertile", 3, "Vous n'avez pas assez de Terre Fertile");

			index = AddCraft(typeof(PlantePiquante), "Plantes Décoratives", "Plante Piquante", 40.0, 60.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas assez de Terre Fertile");

			index = AddCraft(typeof(SoleilMural), "Plantes Décoratives", "Soleil Mural", 45.0, 65.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas assez de Terre Fertile");

			index = AddCraft(typeof(Monstera), "Plantes Décoratives", "Monstera", 50.0, 70.0, typeof(FertileDirt), "Terre Fertile", 3, "Vous n'avez pas assez de Terre Fertile");

			#endregion
			#region Arbres

			index = AddCraft(typeof(AppleTreeDeed), "Arbres", "Pommier", 65.0, 120.0, typeof(FertileDirt), "Terre Fertile", 50, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");
			AddRes(index, typeof(Apple), "Pomme", 5, "Vous n'avez pas de pomme");

			index = AddCraft(typeof(PeachTreeDeed), "Arbres", "Pêcher", 65.0, 120.0, typeof(FertileDirt), "Terre Fertile", 50, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");
			AddRes(index, typeof(Peach), "Pêche", 5, "Vous n'avez pas de pêche");

			index = AddCraft(typeof(PlumTreeAddonDeed), "Arbres", "Prunier", 65.0, 120.0, typeof(FertileDirt), "Terre Fertile", 50, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");
			AddRes(index, typeof(Plum), "Prune", 5, "Vous n'avez pas de prune");

			index = AddCraft(typeof(CherryBlossomTreeDeed), "Arbres", "Cerisier", 65.0, 120.0, typeof(FertileDirt), "Terre Fertile", 50, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");
			AddRes(index, typeof(Cherry), "Cerise", 5, "Vous n'avez pas de cerise");


			index = AddCraft(typeof(MapleTreeDeed), "Arbres", "Érable", 65.0, 120.0, typeof(FertileDirt), "Terre Fertile", 50, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(YuccaTree), "Arbres", "Yucca", 65.0, 120.0, typeof(FertileDirt), "Terre Fertile", 50, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");


			index = AddCraft(typeof(WillowTreeDeed), "Arbres", "Saule", 65.0, 120.0, typeof(FertileDirt), "Terre Fertile", 50, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(TreeStumpDeed), "Arbres", "Souche", 65.0, 120.0, typeof(FertileDirt), "Terre Fertile", 50, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVideTerre), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			#endregion

			// Fleurs rouge en pot
			index = AddCraft(typeof(FleursRougesPot), "Plantes en Pot", "Fleurs Rouges en Pot", 50.0, 100.0, typeof(FertileDirt), "Terre Fertile", 3, " Vous n'avez pas suffisament de Terre Fertile");
			AddRes(index, typeof(PalmierBoard), "Planches", 1, "Vous n'avez pas suffisament de planche");

			// Fleurs rose en pot
			index = AddCraft(typeof(FleursRosesPot), "Plantes en Pot", "Fleurs Roses en Pot", 50.0, 100.0, typeof(FertileDirt), "Terre Fertile", 3, " Vous n'avez pas suffisament de Terre Fertile");
			AddRes(index, typeof(PalmierBoard), "Planches", 1, "Vous n'avez pas suffisament de planche");

			// Petite fleurs fushia en pot
			index = AddCraft(typeof(PetitesFleursFushiaPot), "Plantes en Pot", "Petites Fleurs Fushia en Pot", 50.0, 100.0, typeof(FertileDirt), "Terre Fertile", 3, " Vous n'avez pas suffisament de Terre Fertile");
			AddRes(index, typeof(PalmierBoard), "Planches", 1, "Vous n'avez pas suffisament de planche");

			// Marguerites en pot
			index = AddCraft(typeof(MargueritesPot), "Plantes en Pot", "Marguerites en Pot", 50.0, 100.0, typeof(FertileDirt), "Terre Fertile", 3, " Vous n'avez pas suffisament de Terre Fertile");
			AddRes(index, typeof(PalmierBoard), "Planches", 1, "Vous n'avez pas suffisament de planche");

			// Arbres fleur rouge en pot
			index = AddCraft(typeof(ArbreFleurRougePot), "Pots d'Arbre", "Arbre à Fleurs Rouges en Pot", 50.0, 100.0, typeof(FertileDirt), "Terre Fertile", 3, " Vous n'avez pas suffisament de Terre Fertile");
			AddRes(index, typeof(PalmierBoard), "Planches", 1, "Vous n'avez pas suffisament de planche");

			// Fleur blanche en vase
			index = AddCraft(typeof(FleurBlancheVase), "Plantes en Vase", "Fleur Blanche en Vase", 75.0, 125.0, typeof(Vase), "Vase", 1, "Vous n'avez pas suffisament de Vase");
			AddRes(index, typeof(FertileDirt), "Terre Fertile", 1, " Vous n'avez pas suffisament de Terre Fertile");

			// Fleur rose en vase
			index = AddCraft(typeof(FleurRoseVase), "Plantes en Vase", "Fleur Rose en Vase", 75.0, 125.0, typeof(Vase), "Vase", 1, "Vous n'avez pas suffisament de Vase");
			AddRes(index, typeof(FertileDirt), "Terre Fertile", 1, " Vous n'avez pas suffisament de Terre Fertile");

			// Branche en vase
			index = AddCraft(typeof(BrancheVase), "Plantes en Vase", "Branche en Vase", 75.0, 125.0, typeof(Vase), "Vase", 1, "Vous n'avez pas suffisament de Vase");
			AddRes(index, typeof(FertileDirt), "Terre Fertile", 1, " Vous n'avez pas suffisament de Terre Fertile");

			// Vignes pendantes feuille large
			index = AddCraft(typeof(VignesPendantesFeuilleLarge), "Plantes Suspendues", "Vignes Pendantes à Feuilles Larges", 30.0, 80.0, typeof(FertileDirt), "Terre Fertile", 3, " Vous n'avez pas suffisament de Terre Fertile");

			// Vignes pendantes feuille blanche
			index = AddCraft(typeof(VignesPendantesFeuilleBlanches), "Plantes Suspendues", "Vignes Pendantes à Feuilles Blanches", 30.0, 80.0, typeof(FertileDirt), "Terre Fertile", 3, " Vous n'avez pas suffisament de Terre Fertile");

			// Citrouille découpé
			index = AddCraft(typeof(CitrouilleDecoupe), "Bacs et Terre Fertile", "Citrouille Découpée", 30.0, 80.0, typeof(FertileDirt), "Terre Fertile", 5, " Vous n'avez pas suffisament de Terre Fertile");

			// Corne d'abondance
			index = AddCraft(typeof(CorneDAbondance), "Bacs et Terre Fertile", "Corne d'Abondance", 30.0, 80.0, typeof(FertileDirt), "Terre Fertile", 5, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(FleurRose), "Bouquets", "Fleur Rose", 45.0, 95.0, typeof(RedRose), "Rose Rouge", 5, "Vous n'avez pas suffisament de rose rouge");


			index = AddCraft(typeof(ImmenseCitrouille), "Bac et terre fertile", "Immense citrouille", 80.0, 130.0, typeof(FertileDirt), "Terre Fertile", 10, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(CitrouilleDecorative1), "Bac et terre fertile", "Citrouille décorative", 70.0, 120.0, typeof(FertileDirt), "Terre Fertile", 4, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(CitrouilleSculptee), "Bac et terre fertile", "Citrouille sculptée", 65.0, 115.0, typeof(FertileDirt), "Terre Fertile", 5, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(FleurSolitaireEnVase), "Plantes en Vase", "Une fleur solitaire en vase", 45.0, 95.0, typeof(Vase), "Vase", 1, "Vous n'avez pas suffisament de vase");
			AddRes(index, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(PetitBouquetEnVase), "Plantes en Vase", "Un petit bouquet en vase", 45.0, 95.0, typeof(Vase), "Vase", 1, "Vous n'avez pas suffisament de vase");
			AddRes(index, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(Feuilles), "Bac et terre fertile", "Feuilles", 20.0, 70.0, typeof(FertileDirt), "Terre Fertile", 4, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(HaieDeCedre), "Bac et terre fertile", "Haie de cèdre", 20.0, 70.0, typeof(FertileDirt), "Terre Fertile", 4, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(FleursAuSol), "Bac de terre fertile", "Fleurs au sol", 25.0, 75.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(FeuillesAuSol), "Bac de terre fertile", "Feuilles au sol", 25.0, 75.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(PlanteCarnivor), "Plantes en Pot", "Plante Carnivore", 50.0, 100.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas suffisament de Terre Fertile");
			AddRes(index, typeof(PalmierBoard), "Planches", 1, "Vous n'avez pas suffisament de planches");

			index = AddCraft(typeof(FeuillageAuSol), "Bac et terre fertile", "Feuillage au sol", 35.0, 85.0, typeof(FertileDirt), "Terre Fertile", 4, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(PetitBonzaiVert), "Bonsai", "Petit bonsai vert", 35.0, 85.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas suffisament de Terre Fertile");

			index = AddCraft(typeof(PetitBonzaiRose), "Bonsai", "Petit bonsai rose", 35.0, 85.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas suffisament de Terre Fertile");

			// Set the overridable material for wood
			SetSubRes(typeof(PalmierBoard), "Palmier");

			// Add every wood type you want the player to be able to choose from
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


			MarkOption = true; Pratiquer = true;
			Repair = true;
			CanEnhance = true;
			CanAlter = true;
		}
	}
}
	


        
