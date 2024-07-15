using Server.Items;
using System;
using System.Collections.Generic;

namespace Server.Engines.Craft
{
 
    public class DefBoneTailoring : CraftSystem
    {
        #region Statics

  

        // singleton instance
        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefBoneTailoring();

                return m_CraftSystem;
            }
        }
        #endregion

        #region Constructor
        private DefBoneTailoring()
            : base(1, 1, 1.25)// base( 1, 1, 4.5 )
        {
        }

        #endregion

        #region Overrides
        public override SkillName MainSkill => SkillName.Tinkering;

		public override string GumpTitleString
		{
			get { return "<CENTER>Armures d'os</CENTER>"; }
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




			#region Armure d'os Classique
			index = AddCraft(typeof(BoneHelm), "Armure d'Os Classique", "Casque d'Os Classique", 20.0, 40.0, typeof(Bone), "Os", 4, "Vous n'avez pas suffisament d'os");
			index = AddCraft(typeof(BoneGloves), "Armure d'Os Classique", "Gants d'Os Classique", 20.0, 40.0, typeof(Bone), "Os", 6, "Vous n'avez pas suffisament d'os");
			index = AddCraft(typeof(BoneArms), "Armure d'Os Classique", "Brassards d'Os Classique", 23.0, 43.0, typeof(Bone), "Os", 8, "Vous n'avez pas suffisament d'os");
			index = AddCraft(typeof(BoneGorget), "Armure d'Os Classique", "Gorgerin d'Os Classique", 20.0, 40.0, typeof(Bone), "Os", 5, "Vous n'avez pas suffisament d'os");
			index = AddCraft(typeof(BoneLegs), "Armure d'Os Classique", "Jambieres d'Os Classique", 25.0, 45.0, typeof(Bone), "Os", 10, "Vous n'avez pas suffisament d'os");
			index = AddCraft(typeof(BoneChest), "Armure d'Os Classique", "Plastron d'Os Classique", 30.0, 50.0, typeof(Bone), "Os", 12, "Vous n'avez pas suffisament d'os");
			#endregion

			#region Armure d'os
			index = AddCraft(typeof(OrcHelm), "Armure d'os", "Casque d'Orc", 20.0, 40.0, typeof(Bone), "Os", 4, "Vous n'avez pas suffisament d'os");
			index = AddCraft(typeof(CasqueOS), "Armure d'os", "Casque d'Os", 20.0, 40.0, typeof(Bone), "Os", 4, "Vous n'avez pas suffisament d'os");
			index = AddCraft(typeof(GantsOS), "Armure d'os", "Gants d'Os", 20.0, 40.0, typeof(Bone), "Os", 6, "Vous n'avez pas suffisament d'os");
			index = AddCraft(typeof(BrassardOs), "Armure d'os", "Brassards d'Os", 23.0, 43.0, typeof(Bone), "Os", 8, "Vous n'avez pas suffisament d'os");
			index = AddCraft(typeof(JambiereOs), "Armure d'os", "Jambieres d'Os", 25.0, 45.0, typeof(Bone), "Os", 10, "Vous n'avez pas suffisament d'os");
			index = AddCraft(typeof(PlastronOs), "Armure d'os", "Plastron d'Os", 30.0, 50.0, typeof(Bone), "Os", 12, "Vous n'avez pas suffisament d'os");
			//index = AddCraft(typeof(GorgetOS), "Armure d'os", "Gorgerin d'Os", 35, 55, typeof(Bone), "Os", 6, "Vous n'avez pas suffisament d'os");
			//index = AddCraft(typeof(GorgetOS1), "Armure d'os", "Gorgerin Épaule d'Os", 35, 55, typeof(Bone), "Os", 6, "Vous n'avez pas suffisament d'os");
			#endregion

			// Set the overridable material
			SetSubRes(typeof(Bone), "Os");

            // Add every material you want the player to be able to choose from
            // This will override the overridable material
            AddSubRes(typeof(Bone), "Os", 0.0, 1049312);
			AddSubRes(typeof(LupusBone), "Lupus", 30.0, 1049312);
			AddSubRes(typeof(ReptilienBone), "Reptilien", 40.0, 1049312);
			AddSubRes(typeof(GeantBone), "Geant", 50.0, 1049312);
			AddSubRes(typeof(OphidienBone), "Ophidien", 60.0, 1049312);
			AddSubRes(typeof(ArachnideBone), "Arachnide", 70.0, 1049312);
			AddSubRes(typeof(DragoniqueBone), "Dragonique", 80.0, 1049312);
			AddSubRes(typeof(DemoniaqueBone), "Demoniaque", 90.0, 1049312);
			AddSubRes(typeof(AncienBone), "Ancien", 95.0, 1049312);

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
