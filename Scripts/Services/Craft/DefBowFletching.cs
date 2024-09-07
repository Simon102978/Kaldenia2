using Server.Items;
using System;

namespace Server.Engines.Craft
{
	#region Mondain's Legacy
	public enum BowRecipes
	{
		//magical
		BarbedLongbow = 200,
		SlayerLongbow = 201,
		FrozenLongbow = 202,
		LongbowOfMight = 203,
		RangersShortbow = 204,
		LightweightShortbow = 205,
		MysticalShortbow = 206,
		AssassinsShortbow = 207,

		// arties
		BlightGrippedLongbow = 250,
		FaerieFire = 251,
		SilvanisFeywoodBow = 252,
		MischiefMaker = 253,
		TheNightReaper = 254,

		// K2
		[Description("Arc : Folière")]
		Foliere = 60001,
		[Description("Arc : Composite")]
		Composite = 60002,
		[Description("Arc : Pieuse")]
		Pieuse = 60003,
		[Description("Arbalète : Arbavive")]
		Arbavive = 60004,
		[Description("Arbalète : Lumitrait")]
		Lumitrait = 60005,


	}
	#endregion

	public class DefBowFletching : CraftSystem
	{
		public override SkillName MainSkill => SkillName.Carpentry;

		//  public override int GumpTitleNumber => 1044006;


		public override string GumpTitleString => "Fabrication d'Arc";


		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if (m_CraftSystem == null)
					m_CraftSystem = new DefBowFletching();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin(CraftItem item)
		{
			return 0.5; // 50%
		}

		private DefBowFletching()
			: base(1, 1, 1.25)// base( 1, 2, 1.7 )
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
			// no animation
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 33, 5, 1, true, false, 0 );
			from.PlaySound(0x55);
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

		public override CraftECA ECA => CraftECA.Chance3Max;

		public override void InitCraftList()
		{
			int index = -1;

			// Matériels
			/*          index = index = AddCraft(typeof(ElvenFletching), 1044457, 1113346, 90.0, 130.0, typeof(Feather), 1044562, 20, 1044563);
					  AddRes(index, typeof(FaeryDust), 1113358, 1, 1044253);
			*/

			#region Munitions
			index = AddCraft(typeof(Kindling), "Munitions", "Brindilles (5)", 0.0, 20.0, typeof(PalmierBoard), 1044041, 1, 1044351);
			index = AddCraft(typeof(Kindling), "Munitions", "Brindilles (Max)", 0.0, 20.0, typeof(PalmierBoard), 1044041, 1, 1044351);
			SetUseAllRes(index, true);
			index = AddCraft(typeof(Shaft), "Munitions", "Fût (5)", 0.0, 20.0, typeof(PalmierBoard), "Planche", 1, "Vous n'avez pas suffisament de planche");
			index = AddCraft(typeof(Shaft), "Munitions", "Fût (Max)", 0.0, 20.0, typeof(PalmierBoard), "Planche", 1, "Vous n'avez pas suffisament de planche");
			SetUseAllRes(index, true);
			index = AddCraft(typeof(Arrow), "Munitions", "Flèche (1)", 10.0, 30.0, typeof(Shaft), 1044560, 1, 1044561);
			AddRes(index, typeof(Feather), 1044562, 1, 1044563);
			index = AddCraft(typeof(Arrow), "Munitions", "Flèche (Max)", 10.0, 30.0, typeof(Shaft), 1044560, 1, 1044561);
			AddRes(index, typeof(Feather), 1044562, 1, 1044563);
			SetUseAllRes(index, true);
			index = AddCraft(typeof(Bolt), "Munitions", "Carreaux (1)", 10.0, 30.0, typeof(Shaft), 1044560, 1, 1044561);
			AddRes(index, typeof(Feather), 1044562, 1, 1044563);
			index = AddCraft(typeof(Bolt), "Munitions", "Carreaux (Max)", 10.0, 30.0, typeof(Shaft), 1044560, 1, 1044561);
			AddRes(index, typeof(Feather), 1044562, 1, 1044563);
			SetUseAllRes(index, true);
			index = index = AddCraft(typeof(FukiyaDarts), "Munitions", 1030246, 50.0, 73.8, typeof(PalmierBoard), 1044041, 1, 1044351);
						SetUseAllRes(index, true);
			#endregion

			#region Munitions par Archery
			index = AddCraft(typeof(Kindling), "Munitions (Archer)", "Brindilles (5)", 0.0, 0.0, typeof(PalmierBoard), 1044041, 1, 1044351);
			AddSkill(index, SkillName.Archery, 0.0, 50.0);

			index = AddCraft(typeof(Kindling), "Munitions (Archer)" , "Brindilles (Max)", 0.0, 0.0, typeof(PalmierBoard), 1044041, 1, 1044351);
			AddSkill(index, SkillName.Archery, 0.0, 50.0);
			SetUseAllRes(index, true);

			index = AddCraft(typeof(Shaft), "Munitions (Archer)" , "Fût (5)", 0.0, 0.0, typeof(PalmierBoard), "Planche", 1, "Vous n'avez pas suffisament de planche");
			AddSkill(index, SkillName.Archery, 0.0, 50.0);

			index = AddCraft(typeof(Shaft), "Munitions (Archer)" , "Fût (Max)", 0.0, 0.0, typeof(PalmierBoard), "Planche", 1, "Vous n'avez pas suffisament de planche");
			AddSkill(index, SkillName.Archery, 0.0, 50.0);
			SetUseAllRes(index, true);


			index = AddCraft(typeof(Arrow), "Munitions (Archer)" , "Flèche (1)", 0.0, 0.0, typeof(Shaft), 1044560, 1, 1044561);
			AddRes(index, typeof(Feather), 1044562, 1, 1044563);
			AddSkill(index, SkillName.Archery, 10.0, 60.0);

			index = AddCraft(typeof(Arrow), "Munitions (Archer)" , "Flèche (Max)", 0.0, 0.0, typeof(Shaft), 1044560, 1, 1044561);
			AddRes(index, typeof(Feather), 1044562, 1, 1044563);
			SetUseAllRes(index, true);
			AddSkill(index, SkillName.Archery, 10.0, 60.0);

			index = AddCraft(typeof(Bolt), "Munitions (Archer)" , "Carreaux (1)", 0.0, 0.0, typeof(Shaft), 1044560, 1, 1044561);
			AddRes(index, typeof(Feather), 1044562, 1, 1044563);
			AddSkill(index, SkillName.Archery, 10.0, 60.0);

			index = AddCraft(typeof(Bolt), "Munitions (Archery)" , "Carreaux (Max)", 0.0, 0.0, typeof(Shaft), 1044560, 1, 1044561);
			AddRes(index, typeof(Feather), 1044562, 1, 1044563);
			SetUseAllRes(index, true);
			AddSkill(index, SkillName.Archery, 10.0, 60.0);

			index = index = AddCraft(typeof(FukiyaDarts), "Munitions (Archery)" , 1030246, 0.0, 0.0, typeof(PalmierBoard), 1044041, 1, 1044351);
			SetUseAllRes(index, true);
			AddSkill(index, SkillName.Archery, 50.0, 80.0);

			#endregion


			#region Arcs
			index = AddCraft(typeof(TrainingBow), "Arcs", "Arc d'entrainement", 0.0, 50.0, typeof(PalmierBoard), 1044041, 5, 1044351);
			index = AddCraft(typeof(Blancorde), "Arcs", "Blancorde", 10.0, 40.0, typeof(PalmierBoard), 1044041, 10, 1044351);
			index = AddCraft(typeof(Glaciale), "Arcs", "Glaciale", 10.0, 40.0, typeof(PalmierBoard), 1044041, 10, 1044351);
			index = AddCraft(typeof(Bow), "Arcs", "Arc simple", 10.0, 40.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(Legarc), "Arcs", "Legarc", 15.0, 45.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(Tarkarc), "Arcs", "Arc court renforcit", 15.0, 45.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(Ebonie), "Arcs", "Ebonie", 15.0, 45.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(Mirka), "Arcs", "Mirka", 20.0, 50.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(Souplecorde), "Arcs", "Souplecorde", 20.0, 50.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(Sombrevent), "Arcs", "Sombrevent", 20.0, 50.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(CompositeBow), "Arcs", "Arc composite", 30.0, 60.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(MagicalShortbow), "Arcs", "Percecoeur", 30.0, 60.0, typeof(PalmierBoard), 1044041, 15, 1044351);
			index = AddCraft(typeof(Vigne), "Arcs", "Vigne", 30.0, 60.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(Foudre), "Arcs", "Foudre", 40.0, 70.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(Flamfleche), "Arcs", "Flamflèche", 40.0, 70.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(Yumi), "Arcs", "Arc long", 40.0, 70.0, typeof(PalmierBoard), 1044041, 10, 1044351);
			index = AddCraft(typeof(Mirielle), "Arcs", "Mirielle", 40.0, 70.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(ElvenCompositeLongbow), "Arcs", "Arc long composite", 40.0, 70.0, typeof(PalmierBoard), 1044041, 20, 1044351);
			index = AddCraft(typeof(Barbatrine), "Arcs", "Barbatrine", 40.0, 70.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(Chantefleche), "Arcs", "Chantefleche", 40.0, 70.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(Sifflecrin), "Arcs", "Sifflecrin", 45.0, 75.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(Maegie), "Arcs", "Maegie", 45.0, 75.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(Foliere), "Arcs", "Foliere", 45.0, 75.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			AddRecipe(index, (int)BowRecipes.Foliere);
			index = AddCraft(typeof(Composite), "Arcs", "Composite", 50.0, 80.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			AddRecipe(index, (int)BowRecipes.Composite);
			index = AddCraft(typeof(Pieuse), "Arcs", "Pieuse", 50.0, 80.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			AddRecipe(index, (int)BowRecipes.Pieuse);
			#endregion
			// Arbalètes	
			index = AddCraft(typeof(Crossbow), "Arbalètes", "Arbalète simple", 10.0, 40.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(Arbalete), "Arbalètes", "Arbalète", 10.0, 40.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(ArbaletteChasse), "Arbalètes", "Arbalète de chasse", 20.0, 50.0, typeof(PalmierBoard), 1044041, 10, 1044351);
			index = AddCraft(typeof(RepeatingCrossbow), "Arbalètes", "Arbalète Complexe", 20.0, 50.0, typeof(PalmierBoard), 1044041, 10, 1044351);
			index = AddCraft(typeof(HeavyCrossbow), "Arbalètes", "Arbalète lourde", 20.0, 50.0, typeof(PalmierBoard), 1044041, 10, 1044351);
			index = AddCraft(typeof(ArbalettePistolet), "Arbalètes", "Arbalète à Main", 30.0, 60.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(ArbaletteRepetition), "Arbalètes", "Arbalète à Répétition", 40.0, 70.0, typeof(PalmierBoard), 1044041, 8, 1044351);
			index = AddCraft(typeof(ArbaletteLourde), "Arbalètes", "Arbalète à Méchanisme", 40.0, 70.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(Percemurs), "Arbalètes", "Percemurs", 40.0, 70.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			index = AddCraft(typeof(Arbavive), "Arbalètes", "Arbavive", 50.0, 80.0, typeof(PalmierBoard), 1044041, 7, 1044351);
			AddRecipe(index, (int)BowRecipes.Arbavive);
			index = AddCraft(typeof(Lumitrait), "Arbalètes", "Lumitrait", 50.0, 80.0, typeof(PalmierBoard), 1044041, 10, 1044351);
			AddRecipe(index, (int)BowRecipes.Lumitrait);

			/*          index = index = AddCraft(typeof(BlightGrippedLongbow), 1044566, 1072907, 75.0, 125.0, typeof(PalmierBoard), 1044041, 20, 1044351);
					  AddRes(index, typeof(LardOfParoxysmus), 1032681, 1, 1053098);
					  AddRes(index, typeof(Blight), 1032675, 10, 1053098);
					  AddRes(index, typeof(Corruption), 1032676, 10, 1053098);
					  AddRecipe(index, (int)BowRecipes.BlightGrippedLongbow);
					  ForceNonExceptional(index);

					  index = index = AddCraft(typeof(FaerieFire), 1044566, 1072908, 75.0, 125.0, typeof(PalmierBoard), 1044041, 20, 1044351);
					  AddRes(index, typeof(LardOfParoxysmus), 1032681, 1, 1053098);
					  AddRes(index, typeof(Putrefaction), 1032678, 10, 1053098);
					  AddRes(index, typeof(Taint), 1032679, 10, 1053098);
					  AddRecipe(index, (int)BowRecipes.FaerieFire);
					  ForceNonExceptional(index);

					  index = index = AddCraft(typeof(SilvanisFeywoodBow), 1044566, 1072955, 75.0, 125.0, typeof(PalmierBoard), 1044041, 20, 1044351);
					  AddRes(index, typeof(LardOfParoxysmus), 1032681, 1, 1053098);
					  AddRes(index, typeof(Scourge), 1032677, 10, 1053098);
					  AddRes(index, typeof(Muculent), 1032680, 10, 1053098);
					  AddRecipe(index, (int)BowRecipes.SilvanisFeywoodBow);
					  ForceNonExceptional(index);

					  index = index = AddCraft(typeof(MischiefMaker), 1044566, 1072910, 75.0, 125.0, typeof(PalmierBoard), 1044041, 15, 1044351);
					  AddRes(index, typeof(DreadHornMane), 1032682, 1, 1053098);
					  AddRes(index, typeof(Corruption), 1032676, 10, 1053098);
					  AddRes(index, typeof(Putrefaction), 1032678, 10, 1053098);
					  AddRecipe(index, (int)BowRecipes.MischiefMaker);
					  ForceNonExceptional(index);

					  index = index = AddCraft(typeof(TheNightReaper), 1044566, 1072912, 75.0, 125.0, typeof(PalmierBoard), 1044041, 10, 1044351);
					  AddRes(index, typeof(DreadHornMane), 1032682, 1, 1053098);
					  AddRes(index, typeof(Blight), 1032675, 10, 1053098);
					  AddRes(index, typeof(Scourge), 1032677, 10, 1053098);
					  AddRecipe(index, (int)BowRecipes.TheNightReaper);
					  ForceNonExceptional(index);

					  index = index = AddCraft(typeof(BarbedLongbow), 1044566, 1073505, 75.0, 125.0, typeof(PalmierBoard), 1044041, 20, 1044351);
					  AddRes(index, typeof(FireRubis), 1026254, 1, 1053098);
					  AddRecipe(index, (int)BowRecipes.BarbedLongbow);

					  index = index = AddCraft(typeof(SlayerLongbow), 1044566, 1073506, 75.0, 125.0, typeof(PalmierBoard), 1044041, 20, 1044351);
					  AddRes(index, typeof(BrilliantAmbre), 1026256, 1, 1053098);
					  AddRecipe(index, (int)BowRecipes.SlayerLongbow);

					  index = index = AddCraft(typeof(FrozenLongbow), 1044566, 1073507, 75.0, 125.0, typeof(PalmierBoard), 1044041, 20, 1044351);
					  AddRes(index, typeof(Turquoise), 1026250, 1, 1053098);
					  AddRecipe(index, (int)BowRecipes.FrozenLongbow);

					  index = index = AddCraft(typeof(LongbowOfMight), 1044566, 1073508, 75.0, 125.0, typeof(PalmierBoard), 1044041, 10, 1044351);
					  AddRes(index, typeof(BlueDiamant), 1026255, 1, 1053098);
					  AddRecipe(index, (int)BowRecipes.LongbowOfMight);

					  index = index = AddCraft(typeof(RangersShortbow), 1044566, 1073509, 75.0, 125.0, typeof(PalmierBoard), 1044041, 15, 1044351);
					  AddRes(index, typeof(PerfectEmeraude), 1026251, 1, 1053098);
					  AddRecipe(index, (int)BowRecipes.RangersShortbow);

					  index = index = AddCraft(typeof(LightweightShortbow), 1044566, 1073510, 75.0, 125.0, typeof(PalmierBoard), 1044041, 15, 1044351);
					  AddRes(index, typeof(WhitePearl), 1026253, 1, 1053098);
					  AddRecipe(index, (int)BowRecipes.LightweightShortbow);

					  index = index = AddCraft(typeof(MysticalShortbow), 1044566, 1073511, 75.0, 125.0, typeof(PalmierBoard), 1044041, 15, 1044351);
					  AddRes(index, typeof(EcruCitrine), 1026252, 1, 1053098);
					  AddRecipe(index, (int)BowRecipes.MysticalShortbow);

					  index = index = AddCraft(typeof(AssassinsShortbow), 1044566, 1073512, 75.0, 125.0, typeof(PalmierBoard), 1044041, 15, 1044351);
					  AddRes(index, typeof(DarkSapphire), 1026249, 1, 1053098);
					  AddRecipe(index, (int)BowRecipes.AssassinsShortbow);*/

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



			Resmelt = true;
			MarkOption = true; Pratiquer = true;
			Repair = true;
			CanEnhance = true;
		}
	}
}
