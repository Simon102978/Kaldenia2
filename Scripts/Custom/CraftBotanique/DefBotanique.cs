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


			#region Bacs et Pots

			index = AddCraft(typeof(BacVide), "Bacs et Pots", "Bac Vide", 10.0, 30.0, typeof(PalmierBoard), "Bois", 10, "Vous n'avez pas assez de bois");

			index = AddCraft(typeof(BacVideSable), "Bacs et Pots", "Bac Vide avec Sable", 15.0, 35.0, typeof(PalmierBoard), "Bois", 10, "Vous n'avez pas assez de bois");
			AddRes(index, typeof(Sand), "Sable", 5, "Vous n'avez pas assez de sable");

			index = AddCraft(typeof(BacArbre), "Bacs et Pots", "Bac pour Arbre", 20.0, 40.0, typeof(PalmierBoard), "Bois", 15, "Vous n'avez pas assez de bois");

			index = AddCraft(typeof(Presentoir), "Bacs et Pots", "Presentoir", 25.0, 45.0, typeof(PalmierBoard), "Bois", 10, "Vous n'avez pas assez de bois");



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

			index = AddCraft(typeof(BacArbutre1), "Bacs Arbustres", "Petit Arbuste", 30.0, 50.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVide), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre2), "Bacs Arbustres", "Arbuste Moyen", 35.0, 55.0, typeof(FertileDirt), "Terre Fertile", 2, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(BacVide), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre3), "Bacs Arbustres", "Grand Arbuste", 40.0, 60.0, typeof(FertileDirt), "Terre Fertile", 3, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(BacVide), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre4), "Bacs Arbustres", "Arbuste Touffu", 45.0, 65.0, typeof(FertileDirt), "Terre Fertile", 4, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(BacVide), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre5), "Bacs Arbustres", "Arbuste Décoratif", 50.0, 70.0, typeof(FertileDirt), "Terre Fertile", 5, "Vous n'avez pas assez de Terre Fertile");
			AddRes(index, typeof(BacVide), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre6), "Bacs Arbustres", "Arbuste Exotique", 55.0, 75.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVide), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre7), "Bacs Arbustres", "Arbuste Rare", 60.0, 80.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVide), "Bac Vide", 1, "Vous n'avez pas de bac vide");

			index = AddCraft(typeof(BacArbutre8), "Bacs Arbustres", "Arbuste de Luxe", 65.0, 85.0, typeof(FertileDirt), "Terre Fertile", 1, "Vous n'avez pas de Terre Fertile");
			AddRes(index, typeof(BacVide), "Bac Vide", 1, "Vous n'avez pas de bac vide");

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

			index = AddCraft(typeof(Bouquet1), "Bouquets", "Bouquet Simple", 20.0, 40.0, typeof(RedRose), "Fleur", 1, "Vous n'avez pas de Rose sauvage");

			index = AddCraft(typeof(Bouquet2), "Bouquets", "Bouquet Élégant", 25.0, 45.0, typeof(RedRose), "Fleur", 2, "Vous n'avez pas assez de fleurs");

			index = AddCraft(typeof(Bouquet3), "Bouquets", "Bouquet Somptueux", 30.0, 50.0, typeof(RedRose), "Fleur", 3, "Vous n'avez pas assez de fleurs");

			#endregion












		}
	}
}

        
