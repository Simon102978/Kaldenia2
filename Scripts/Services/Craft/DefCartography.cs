﻿using Server.Items;
using System;
using System.Collections.Generic;

namespace Server.Engines.Craft
{
    public enum CartographyRecipes
    {
        EodonianWallMap = 1000
    }

    public class DefCartography : CraftSystem
    {
        private static CraftSystem m_CraftSystem;
        private DefCartography()
            : base(1, 1, 1.25)// base( 1, 1, 3.0 )
        {
        }

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefCartography();

                return m_CraftSystem;
            }
        }
        public override SkillName MainSkill => SkillName.Cartography;
		//   public override int GumpTitleNumber => 1044008;

		public override string GumpTitleString => "Cartographie";

		public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0; // 0%
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
            from.PlaySound(0x249);
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
                else if (item.ItemType == typeof(StarChart))
                    return 1158494; // Which telescope do you wish to create the star chart from?
                else
                    return 1044154; // You create the item.
            }
        }

        public override void InitCraftList()
        {
			AddCraft(typeof(BlankMap), "Carte", "carte vierge", 00.0, 50.0, typeof(BlankScroll), "Parchemin Vierge", 1, "Vous n'avez pas suffisament de parchemins vierges");

			AddCraft(typeof(LocalMap), "Carte", "Carte Locale", 15.0, 65.0, typeof(BlankMap), "Carte Vierge", 1, "Vous n'avez pas suffisament de carte vierge");
            AddCraft(typeof(CityMap), "Carte", "Carte de la ville", 25.0, 85.0, typeof(BlankMap), "Carte Vierge", 3, "Vous n'avez pas suffisament de carte vierge");
            AddCraft(typeof(SeaChart), "Carte", "Carte des eaux", 35.0, 95.0, typeof(BlankMap), "Carte Vierge", 5, "Vous n'avez pas suffisament de carte vierge");
            AddCraft(typeof(WorldMap), "Carte", "Carte du Monde", 50, 99.5, typeof(BlankMap), "Carte Vierge", 7, "Vous n'avez pas suffisament de carte vierge");

			/*          int index = AddCraft(typeof(TatteredWallMapSouth), 1044448, 1072891, 90.0, 150.0, typeof(TreasureMap), 1073494, 10, 1073495);
					  AddRes(index, typeof(TreasureMap), 1073498, 5, 1073499);
					  AddRes(index, typeof(TreasureMap), 1073500, 3, 1073501);
					  AddRes(index, typeof(TreasureMap), 1073502, 1, 1073503);
					  AddResCallback(index, ConsumeTatteredWallMapRes);

					  index = AddCraft(typeof(TatteredWallMapEast), 1044448, 1072892, 90.0, 150.0, typeof(TreasureMap), 1073494, 10, 1073495);
					  AddRes(index, typeof(TreasureMap), 1073498, 5, 1073499);
					  AddRes(index, typeof(TreasureMap), 1073500, 3, 1073501);
					  AddRes(index, typeof(TreasureMap), 1073502, 1, 1073503);
					  AddResCallback(index, ConsumeTatteredWallMapRes);

					  index = AddCraft(typeof(EodonianWallMap), 1044448, 1156690, 65.0, 125.0, typeof(BlankMap), 1044449, 50, 1044450);
					  AddRes(index, typeof(UnabridgedAtlasOfEodon), 1156721, 1, 1156722);
					  AddRecipe(index, (int)CartographyRecipes.EodonianWallMap);

					  index = AddCraft(typeof(StarChart), 1044448, 1158493, 0.0, 60.0, typeof(BlankMap), 1044449, 1, 1044450);
					  SetForceSuccess(index, 75);
			*/
			SetSubRes(typeof(BlankMap), "Carte Vierge");
		}

		// Set the overridable material
		

		public int ConsumeTatteredWallMapRes(Mobile from, ConsumeType type)
        {
            Item[] maps = from.Backpack.FindItemsByType(typeof(TreasureMap));
            List<Item> toConsume = new List<Item>();

            int one = 10;
            int three = 5;
            int four = 3;
            int five = 1;

            foreach (Item item in maps)
            {
                TreasureMap map = item as TreasureMap;

                if (map != null)
                {
                    switch (map.Level)
                    {
                        case 1:
                            if (map.CompletedBy == from)
                            {
                                one--;
                                toConsume.Add(map);
                            }
                            break;
                        case 3:
                            if (map.CompletedBy == from)
                            {
                                three--;
                                toConsume.Add(map);
                            }
                            break;
                        case 4:
                            if (map.CompletedBy == from)
                            {
                                four--;
                                toConsume.Add(map);
                            }
                            break;
                        case 5:
                            if (map.CompletedBy == from)
                            {
                                five--;
                                toConsume.Add(map);
                            }
                            break;
                    }
                }
            }

            int message = 0;

            if (one > 0)
                message = 1073495;
            else if (three > 0)
                message = 1073499;
            else if (four > 0)
                message = 1073501;
            else if (five > 0)
                message = 1073503;

            if (message == 0 && type == ConsumeType.All)
            {
                foreach (Item item in toConsume)
                    item.Consume();
            }

            toConsume.Clear();
            return message;
        }
    }
}
