using Server.Items;
using System;
using System.Collections.Generic;

namespace Server.Engines.Craft
{
 
    public class DefLeatherArmor : CraftSystem
    {
        #region Statics

  

        // singleton instance
        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefLeatherArmor();

                return m_CraftSystem;
            }
        }
        #endregion

        #region Constructor
        private DefLeatherArmor()
            : base(3, 4, 1.50)// base( 1, 1, 4.5 )
		{
        }

        #endregion

        #region Overrides
        public override SkillName MainSkill => SkillName.Tailoring;

		public override string GumpTitleString
		{
			get { return "<CENTER>Travail du Cuir</CENTER>"; }
		}

		public override CraftECA ECA => CraftECA.Chance3Max;

		public override double GetChanceAtMin(CraftItem item)
		{
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

        public override void InitCraftList()
        {
            int index = -1;

			#region Bottes
			AddCraft(typeof(Sandals), "Bottes", "Sandales", 10.0, 30.0, typeof(Leather), 1044462, 15, 1044463);
			AddCraft(typeof(Shoes), "Bottes", "Souliers", 10.0, 30.0, typeof(Leather), 1044462, 15, 1044463);
			AddCraft(typeof(Boots), "Bottes", "Bottes simples",20.0, 40.0,  typeof(Leather), 1044462, 15, 1044463);
			AddCraft(typeof(ThighBoots), "Bottes", "Cuissardes", 20.0, 40.0,typeof(Leather), 1044462, 15, 1044463);
			AddCraft(typeof(LeatherTalons), "Bottes", "Soulier en cuir", 20.0, 40.0, typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(SandaleCuir), "Bottes", "Sandales en cuir", 40.0, 60.0, typeof(Leather), 1044462, 10, 1044463);	
			index = AddCraft(typeof(SoulierTissus), "Bottes", "Soulier en Tissus", 40.0, 60.0, typeof(Leather), 1044462, 15, 1044463);	
			index = AddCraft(typeof(ElvenBoots), "Bottes", "Bottes délicate", 45.0, 65.0, typeof(Leather), 1044462, 15, 1044463);		
			index = AddCraft(typeof(Bottes4), "Bottes", "Bottes lacées", 50.0, 70.0,  typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(Bottes), "Bottes", "Bottes à talon", 55.0, 75.0, typeof(Leather), 1044462, 15, 1044463);			
			index = AddCraft(typeof(Bottes9), "Bottes", "Bottes à rebord", 60.0, 70.0, typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(Bottes8), "Bottes", "Bottes en tissus", 65.0, 85.0, typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(Bottes2), "Bottes", "Bottes en cuir", 70.0, 90.0, typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(Bottes6), "Bottes", "Bottes en cuir large", 70.0, 90.0, typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(Bottes3), "Bottes", "Bottes ajustées", 75.0, 95.0, typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(Bottes5), "Bottes", "Bottes à Sangles", 60.0, 80.0, typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(Bottes7), "Bottes", "Bottes en fourrure", 80.0, 100.0, typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(Bottes10), "Bottes", "Botte haute à sangles", 85.0, 105.0, typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(BottesPoils), "Bottes", "Bottes de Poils", 85.0, 105.0, typeof(Leather), 1044462, 15, 1044463);
			#endregion

			#region Ceintures

			index = AddCraft(typeof(Ceinture3), "Ceintures", "Ceinture d'artisan", 20.0, 40.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Ceinture5), "Ceintures", "Ceinture mince", 30.0, 50.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Ceinture6), "Ceintures", "Ceinture poche à gauche", 30.0, 50.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Ceinture8), "Ceintures", "Ceinture en bandouillère", 40.0, 60.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Ceinture9), "Ceintures", "Bourse carrée", 40.0, 60.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Ceinture), "Ceintures", "Ceinture boucle ronde", 40.0, 60.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(CentureDoreeLarge), "Ceintures", "Ceinture Dorée Large", 50.0, 70.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(CeintureMetal), "Ceintures", "Ceinture Métalique", 50.0, 70.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(CeintureBaril), "Ceintures", "Ceinture Barril", 60.0, 80.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Ceinture2), "Ceintures", "Ceinture boucle carrée", 60.0, 80.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Ceinture4), "Ceintures", "Ceinture à pochettes", 65.0, 85.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Ceinture7), "Ceintures", "Ceinture en tissu", 70.0, 90.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");							
			#endregion


			#region Masques
			index = AddCraft(typeof(OrcMask), "Masques", "Masque d'orc", 10.0, 30.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(BearMask), "Masques", "Masque d'ours", 10.0, 30.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(DeerMask), "Masques", "Masque de Cerf", 20.0, 40.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(TribalMask), "Masques", "Masque Tribal", 20.0, 40.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(HornedTribalMask), "Masques", "Masque tribal Ornée", 20.0, 40.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(CoiffeGuepard), "Chapeaux", "Coiffe Guepard", 30.0, 50.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(TeteCoyote), "Chapeaux", "Tete de Coyote", 40.0, 60.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(CoiffeSanglier), "Chapeaux", "Coiffe Sanglier", 55.0, 75.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");		
			index = AddCraft(typeof(CoiffeLoupBlanc), "Chapeaux", "Coiffe Loup Blanc", 60.0, 80.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");	
			index = AddCraft(typeof(CoiffeLion), "Chapeaux", "Coiffe Lion", 65.0, 85.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");	
			index = AddCraft(typeof(TeteTaureau), "Chapeaux", "Tete de Taureau", 70.0, 90.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");


			index = AddCraft(typeof(Masque11), "Chapeaux", "Bandeau oeil droit", 10, 30, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque2), "Chapeaux", "Masque Ossement d'élan", 10.0, 30.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque7), "Chapeaux", "Masque simple", 40, 60, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque8), "Chapeaux", "Masque Vénitien", 40, 60, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque10), "Chapeaux", "Lunettes d'aveugle", 40, 60, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque9), "Chapeaux", "Masque-foulard", 50, 70, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque3), "Chapeaux", "Masque Crâne", 50.0, 70.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque5), "Chapeaux", "Masque du Sage à cornes", 50.0, 75.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");	
			index = AddCraft(typeof(Masque6), "Chapeaux", "Masque de plumes fines", 50, 70, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque13), "Chapeaux", "Masque de soirée", 50, 70, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque4), "Chapeaux", "Masque Crâne à piques", 55.0, 70.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque1), "Chapeaux", "Masque ossement de cerf", 60.0, 80.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque16), "Chapeaux", "Masque simple à foulard",60, 80, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque15), "Chapeaux", "Masque du phénix", 70, 90, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque12), "Chapeaux", "Masque Crâne a foulard", 75, 95, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque14), "Chapeaux", "Masque Festif", 80, 90, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");	
			index = AddCraft(typeof(Masque17), "Chapeaux", "Masque doré", 85, 105, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(Masque18), "Chapeaux", "Masque partiel orné", 90, 110, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			//index = AddCraft(typeof(Masque19), "Chapeaux", "Masque", 70, 90, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");




			#endregion



			#region Armure de Cuir
			AddCraft(typeof(LeatherGorget), "Armures de Cuir", "Leather gorget", 20.0, 40.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			AddCraft(typeof(LeatherGloves), "Armures de Cuir", "Leather gloves", 20.0, 40.0, typeof(Leather), "Cuir", 6, "You do not have sufficient leather to make that item.");
			AddCraft(typeof(LeatherCap), "Armures de Cuir", "Leather cap", 23.0, 43.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			AddCraft(typeof(LeatherArms), "Armures de Cuir", "Leather arms", 23.0, 43.0, typeof(Leather), "Cuir", 8, "You do not have sufficient leather to make that item.");
			AddCraft(typeof(LeatherShorts), "Armures de Cuir", "Leather shorts", 25.0, 45.0, typeof(Leather), "Cuir", 11, "You do not have sufficient leather to make that item.");
			AddCraft(typeof(LeatherSkirt), "Armures de Cuir", "Leather skirt", 25.0, 45.0, typeof(Leather), "Cuir", 11, "You do not have sufficient leather to make that item.");
			AddCraft(typeof(LeatherLegs), "Armures de Cuir", "Leather legs", 25.0, 45.0, typeof(Leather), "Cuir", 11, "You do not have sufficient leather to make that item.");
			AddCraft(typeof(LeatherChest), "Armures de Cuir", "Leather chest", 30.0, 50.0, typeof(Leather), "Cuir", 14, "You do not have sufficient leather to make that item.");
			AddCraft(typeof(FemaleLeatherChest), "Armures de Cuir", "Female leather chest", 30.0, 50.0, typeof(Leather), "Cuir", 14, "You do not have sufficient leather to make that item.");
			AddCraft(typeof(LeatherBustierArms), "Armures de Cuir", "Leather bustier arms", 30.0, 50.0, typeof(Leather), "Cuir", 14, "You do not have sufficient leather to make that item.");
			#endregion

			#region Armure de Peaux
			index = AddCraft(typeof(HideChest), "Armures de Peaux", "Plastron de Peaux", 36.0, 56.0, typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(HidePauldrons), "Armures de Peaux", "Épaulettes de Peaux", 29.0, 49.0, typeof(Leather), 1044462, 12, 1044463);
			index = AddCraft(typeof(HideGloves), "Armures de Peaux", "Gants de Peaux", 26.0, 46.0, typeof(Leather), 1044462, 10, 1044463);
			index = AddCraft(typeof(HidePants), "Armures de Peaux", "Pantalons de Peaux", 31.0, 51.0, typeof(Leather), 1044462, 15, 1044463);
			index = AddCraft(typeof(HideGorget), "Armures de Peaux", "Gorgerin de Peaux", 26.0, 46.0, typeof(Leather), 1044462, 12, 1044463);
			#endregion

			#region Armure De Chitin
			index = AddCraft(typeof(PlastronChitinCuir), "Armures de Chitin", "Plastron de Chitin", 30.0, 50.0, typeof(Leather), "Cuir", 14, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(BrassardChitinCuir), "Armures de Chitin", "Brassards de Chitin", 23.0, 43.0, typeof(Leather), "Cuir", 8, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(GantChitinCuir), "Armures de Chitin", "Gants de Chitin", 20.0, 40.0, typeof(Leather), "Cuir", 6, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(JambiereChitinCuir), "Armures de Chitin", "Pantalons de Chitin", 25.0, 45.0, typeof(Leather), "Cuir", 11, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(GorgetChitinCuir), "Armures de Chitin", "Gorget de Chitin", 20.0, 40.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(CasqueChitinCuir), "Armures de Chitin", "Casque de Chitin", 20.0, 40.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			#endregion


			#region Armure Rembourrée
			index = AddCraft(typeof(PlastronRembourre), "Armure Rembourrée", "Plastron Rembourré", 30.0, 50.0, typeof(Leather), "Cuir", 14, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(BrassardRemb), "Armure Rembourrée", "Brassards Rembourrés", 23.0, 43.0, typeof(Leather), "Cuir", 8, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(GantRembourre), "Armure Rembourrée", "Gants Rembourrés", 20.0, 40.0, typeof(Leather), "Cuir", 6, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(JambiereRembourree), "Armure Rembourrée", "Pantalons Rembourrés", 25.0, 45.0, typeof(Leather), "Cuir", 11, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(GorgetRembourre), "Armure Rembourrée", "Gorget Rembourré", 20.0, 40.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(CasqueRembourre), "Armure Rembourrée", "Casque Rembourré", 20.0, 40.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(CasqueRembourre1), "Armure Rembourrée", "Casque Rembourré", 20.0, 40.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(CasqueRembourre2), "Armure Rembourrée", "Casque Rembourré", 20.0, 40.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(CasqueRembourre3), "Armure Rembourrée", "Casque Rembourré", 20.0, 40.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");



			#endregion


			#region Armure De Feuilles
			index = AddCraft(typeof(LeafChest), "Armures de Feuilles", "Plastron de feuille", 30.0, 50.0, typeof(Leather), "Cuir", 14, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(LeafArms), "Armures de Feuilles", "Brassards de feuille", 23.0, 43.0, typeof(Leather), "Cuir", 8, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(LeafGloves), "Armures de Feuilles", "Gants de feuille", 20.0, 40.0, typeof(Leather), "Cuir", 6, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(LeafLegs), "Armures de Feuilles", "Pantalons de feuille", 25.0, 45.0, typeof(Leather), "Cuir", 11, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(LeafGorget), "Armures de Feuilles", "Gorget de feuille", 20.0, 40.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(FemaleLeafChest), "Armures de Feuilles", "Torse féminin de feuille", 30.0, 50.0, typeof(Leather), "Cuir", 14, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(LeafTonlet), "Armures de Feuilles", "Jupe de feuille", 23.0, 43.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			#endregion

			#region Armure Cloutée
			index = AddCraft(typeof(StuddedGorget), "Armures Cloutée", "Studded gorget", 26.0, 46.0, typeof(Leather), "Leather", 6, "You do not have sufficient leather to make that item.");

			index = AddCraft(typeof(StuddedGloves), "Armures Cloutée", "Studded gloves", 26.0, 46.0, typeof(Leather), "Leather", 7, "You do not have sufficient leather to make that item.");

			index = AddCraft(typeof(StuddedArms), "Armures Cloutée", "Studded arms", 29.0, 49.0, typeof(Leather), "Leather", 9, "You do not have sufficient leather to make that item.");

			index = AddCraft(typeof(StuddedLegs), "Armures Cloutée", "Studded legs", 31.0, 51.0, typeof(Leather), "Leather", 12, "You do not have sufficient leather to make that item.");

			index = AddCraft(typeof(StuddedBustierArms), "Armures Cloutée", "Studded bustier arms", 36.0, 56.0, typeof(Leather), "Leather", 15, "You do not have sufficient leather to make that item.");

			index = AddCraft(typeof(StuddedChest), "Armures Cloutée", "Studded chest", 36.0, 56.0, typeof(Leather), "Leather", 15, "You do not have sufficient leather to make that item.");

			index = AddCraft(typeof(FemaleStuddedChest), "Armures Cloutée", "Female studded chest", 36.0, 56.0, typeof(Leather), "Leather", 15, "You do not have sufficient leather to make that item.");


			AddCraft(typeof(BrassardCloute), "Armures Cloutée", "Brassard Clouté", 36.0, 56.0, typeof(Leather), 1044462, 10, 1044463);
			AddCraft(typeof(JupeCloute), "Armures Cloutée", "Jupe Clouté", 36.0, 56.0, typeof(Leather), 1044462, 12, 1044463);

			AddCraft(typeof(PlastronCloute), "Armures Cloutée", "Plastron Clouté", 36.0, 56.0, typeof(Leather), 1044462, 14, 1044463);
			AddCraft(typeof(PlastronCloute2), "Armures Cloutée", "Plastron Clouté2", 36.0, 56.0, typeof(Leather), 1044462, 14, 1044463);
			AddCraft(typeof(PlastronCloute3), "Armures Cloutée", "Plastron Clouté3", 36.0, 56.0, typeof(Leather), 1044462, 14, 1044463);
			AddCraft(typeof(PlastronCloute4), "Armures Cloutée", "Plastron Clouté4", 36.0, 56.0, typeof(Leather), 1044462, 14, 1044463);
			#endregion

			#region Armure Cloutée Elfique
			index = AddCraft(typeof(PlastronClouteElfique), "Armure Cloutée Elfique", "Plastron Clouté Elfique", 36.0, 56.0, typeof(Leather), "Cuir", 14, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(BrassardClouteElfique), "Armure Cloutée Elfique", "Brassards Clouté Elfique", 29.0, 49.0, typeof(Leather), "Cuir", 8, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(GantClouteElfique), "Armure Cloutée Elfique", "Gants Clouté Elfique", 26.0, 46.0, typeof(Leather), "Cuir", 6, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(PantalonsClouteElfique), "Armure Cloutée Elfique", "Pantalons Clouté Elfique", 31.0, 51.0, typeof(Leather), "Cuir", 11, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(GorgetClouteElfique), "Armure Cloutée Elfique", "Gorget Clouté Elfique", 26.0, 46.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(CasqueClouteElfique), "Armure Cloutée Elfique", "Casque Clouté Elfique", 23.0, 43.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			#endregion

			#region Armure Cloutée Renforcée
			index = AddCraft(typeof(PlastronClouteRenforce), "Armure Renforcée", "Plastron Renforcé", 36.0, 56.0, typeof(Leather), "Cuir", 14, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(BrassardClouteRenforce), "Armure Renforcée", "Brassards Renforcé", 29.0, 49.0, typeof(Leather), "Cuir", 8, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(GantClouteRenforce), "Armure Renforcée", "Gants Renforcé", 26.0, 46.0, typeof(Leather), "Cuir", 6, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(PantalonsClouteRenforce), "Armure Renforcée", "Pantalons Renforcé", 31.0, 51.0, typeof(Leather), "Cuir", 11, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(GorgetClouteRenforce), "Armure Renforcée", "Gorget Renforcé", 26.0, 46.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			index = AddCraft(typeof(CasqueClouteRenforce), "Armure Renforcée", "Casque Renforcé", 23.0, 43.0, typeof(Leather), "Cuir", 5, "You do not have sufficient leather to make that item.");
			#endregion


			#region Divers

			index = AddCraft(typeof(RegularLeatherResourceCrate), "Divers", "Caisse de ressource de cuir", 10, 60.0, typeof(Leather), 1044462, 150, 1044463);

			index = AddCraft(typeof(Backpack), "Divers", "Sac à dos", 10, 35.0, typeof(Leather), 1044462, 5, 1044463);
			index = AddCraft(typeof(Pouch), "Divers", "Bourse", 0.0, 25.0, typeof(Leather), 1044462, 3, 1044463);
			index = AddCraft(typeof(Bag), "Divers", "Sac", 5, 30.0, typeof(Leather), 1044462, 4, 1044463);

			index = AddCraft(typeof(Carquois), "Divers", "Carquois", 80.0, 100.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(fourreau), "Divers", "Fourreau épée longue", 30.0, 80.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(fourreau2), "Divers", "Fourreau croisé", 30.0, 80.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(fourreau3), "Divers", "Fourreau bandouillère", 40.0, 90.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			index = AddCraft(typeof(FourreauDore), "Divers", "Fourreau Doré", 40.0, 90.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");

			AddCraft(typeof(BrownBearRugSouthDeed), "Decorations", "Peau Ours Sud", 35.0, 115.0, typeof(Leather), 1044462, 10, 1044463);
			AddCraft(typeof(BrownBearRugEastDeed), "Decorations", "Peau Ours Est", 35.0, 115.0, typeof(Leather), 1044462, 10, 1044463);
			AddCraft(typeof(PolarBearRugSouthDeed), "Decorations", "Peau Ours Polaire Sud", 35.0, 115.0, typeof(Leather), 1044462, 10, 1044463);
			AddCraft(typeof(PolarBearRugEastDeed), "Decorations", "Peau Ours Polaire Est", 35.0, 115.0, typeof(Leather), 1044462, 10, 1044463);

			index = AddCraft(typeof(Corde), "Divers", "Corde", 60.0, 75.0, typeof(Leather), "cuir", 10, "Vous n'avez pas assez de cuir.");
			#endregion

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
        #endregion

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
