using Server.Items;
using Server.Network;
using System;
using System.Linq;

namespace Server.Engines.Harvest
{
	public class Lumberjacking : HarvestSystem
	{
		private static Lumberjacking m_System;

		public static Lumberjacking System
		{
			get
			{
				if (m_System == null)
					m_System = new Lumberjacking();

				return m_System;
			}
		}

		private readonly HarvestDefinition m_Definition;

		public HarvestDefinition Definition => m_Definition;

		private Lumberjacking()
		{
			HarvestResource[] res;
			HarvestVein[] veins;

			#region Lumberjacking
			HarvestDefinition lumber = new HarvestDefinition
			{

				// Resource banks are every 4x3 tiles
				BankWidth = 4,
				BankHeight = 3,

				// Every bank holds from 20 to 45 logs
				MinTotal = 20,
				MaxTotal = 45,

				// A resource bank will respawn its content every 20 to 30 minutes
				MinRespawn = TimeSpan.FromMinutes(20.0),
				MaxRespawn = TimeSpan.FromMinutes(30.0),

				// Skill checking is done on the Lumberjacking skill
				Skill = SkillName.Lumberjacking,

				// Set the list of harvestable tiles
				Tiles = m_TreeTiles,

				// Players must be within 2 tiles to harvest
				MaxRange = 2,

				// Ten logs per harvest action
				ConsumedPerHarvest = 10,
				ConsumedPerFeluccaHarvest = 20,

				// The chopping effect
				EffectActions = new int[] { 7 },
				EffectSounds = new int[] { 0x13E },
				EffectCounts = (new int[] { 1 }),
				EffectDelay = TimeSpan.FromSeconds(1.6),
				EffectSoundDelay = TimeSpan.FromSeconds(0.9),

				NoResourcesMessage = 500493, // There's not enough wood here to harvest.
				FailMessage = 500495, // You hack at the tree for a while, but fail to produce any useable wood.
				OutOfRangeMessage = 500446, // That is too far away.
				PackFullMessage = 500497, // You can't place any wood into your backpack!
				ToolBrokeMessage = 500499 // You broke your axe.
			};

			res = new HarvestResource[]
			{
				new HarvestResource(00.0, 00.0, 100.0, 1072540, typeof(PalmierLog)),
				new HarvestResource(65.0, 25.0, 105.0, 1072541, typeof(OakLog)),
				new HarvestResource(80.0, 40.0, 120.0, 1072542, typeof(AshLog)),
				new HarvestResource(95.0, 55.0, 135.0, 1072543, typeof(YewLog)),
				new HarvestResource(100.0, 60.0, 140.0, 1072544, typeof(HeartwoodLog)),
				new HarvestResource(100.0, 60.0, 140.0, 1072545, typeof(BloodwoodLog)),
				new HarvestResource(100.0, 60.0, 140.0, 1072546, typeof(FrostwoodLog)),
			};

			veins = new HarvestVein[]
			{
				new HarvestVein(49.0, 0.0, res[0], null), // Ordinary Logs
                new HarvestVein(30.0, 0.5, res[1], res[0]), // Oak
                new HarvestVein(10.0, 0.5, res[2], res[0]), // Ash
                new HarvestVein(05.0, 0.5, res[3], res[0]), // Yew
                new HarvestVein(03.0, 0.5, res[4], res[0]), // Heartwood
                new HarvestVein(02.0, 0.5, res[5], res[0]), // Bloodwood
                new HarvestVein(01.0, 0.5, res[6], res[0]), // Frostwood
            };

			lumber.BonusResources = new BonusHarvestResource[]
			{
				new BonusHarvestResource(0, 82.0, null, null), //Nothing
                new BonusHarvestResource(100, 10.0, 1072548, typeof(BarkFragment)),
				new BonusHarvestResource(100, 03.0, 1072550, typeof(LuminescentFungi)),
				new BonusHarvestResource(100, 02.0, 1072547, typeof(SwitchItem)),
				new BonusHarvestResource(100, 01.0, 1072549, typeof(ParasiticPlant)),
				new BonusHarvestResource(100, 01.0, 1072551, typeof(BrilliantAmbre)),
				new BonusHarvestResource(100, 01.0, 1113756, typeof(CrystalShards), Map.TerMur),
			};

			lumber.Resources = res;
			lumber.Veins = veins;

			lumber.RaceBonus = true;
			lumber.RandomizeVeins = true;

			m_Definition = lumber;
			Definitions.Add(lumber);
			#endregion
		}

		public override Type MutateType(Type type, Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource)
		{
			Type newType = type;

			if (tool is HarvestersAxe axe && axe.Charges > 0 /*|| tool is GargishHarvestersAxe gaxe && gaxe.Charges > 0*/)
			{
				if (type == typeof(PalmierLog))
					newType = typeof(PalmierBoard);
				else if (type == typeof(OakLog))
					newType = typeof(OakBoard);
				else if (type == typeof(AshLog))
					newType = typeof(AshBoard);
				else if (type == typeof(YewLog))
					newType = typeof(YewBoard);
				else if (type == typeof(HeartwoodLog))
					newType = typeof(HeartwoodBoard);
				else if (type == typeof(BloodwoodLog))
					newType = typeof(BloodwoodBoard);
				else if (type == typeof(FrostwoodLog))
					newType = typeof(FrostwoodBoard);

				if (newType != type)
				{
					if (tool is HarvestersAxe)
					{
						((HarvestersAxe)tool).Charges--;
					}
					//else if (tool is GargishHarvestersAxe)
					//{
					//    ((GargishHarvestersAxe)tool).Charges--;
					//}
				}
			}

			return newType;
		}

		public override void SendSuccessTo(Mobile from, Item item, HarvestResource resource)
		{
			if (item != null)
			{
				if (item != null && item.GetType().IsSubclassOf(typeof(BaseWoodBoard)))
				{
					from.SendLocalizedMessage(1158776); // The axe magically creates boards from your logs.
					return;
				}
				else
				{
					foreach (HarvestResource res in m_Definition.Resources.Where(r => r.Types != null))
					{
						foreach (Type type in res.Types)
						{
							if (item.GetType() == type)
							{
								res.SendSuccessTo(from);
								return;
							}
						}
					}
				}
			}

			base.SendSuccessTo(from, item, resource);
		}

		public override bool CheckHarvest(Mobile from, Item tool)
		{
			if (!base.CheckHarvest(from, tool))
				return false;

			return true;
		}

		public override bool CheckHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
		{
			if (!base.CheckHarvest(from, tool, def, toHarvest))
				return false;

			if (tool.Parent != from && from.Backpack != null && !tool.IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1080058); // This must be in your backpack to use it.
				return false;
			}

			return true;
		}

		public override Type GetResourceType(Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource)
		{
			#region Void Pool Items
			HarvestMap hmap = HarvestMap.CheckMapOnHarvest(from, loc, def);

			if (hmap != null && hmap.Resource >= CraftResource.PalmierWood && hmap.Resource <= CraftResource.Frostwood)
			{
				hmap.UsesRemaining--;
				hmap.InvalidateProperties();

				CraftResourceInfo info = CraftResources.GetInfo(hmap.Resource);

				if (info != null)
					return info.ResourceTypes[1];
			}
			#endregion

			return base.GetResourceType(from, tool, def, map, loc, resource);
		}

		public override bool CheckResources(Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, bool timed)
		{
			if (HarvestMap.CheckMapOnHarvest(from, loc, def) == null)
				return base.CheckResources(from, tool, def, map, loc, timed);

			return true;
		}

		public override void OnBadHarvestTarget(Mobile from, Item tool, object toHarvest)
		{
			if (toHarvest is Mobile)
				((Mobile)toHarvest).PrivateOverheadMessage(MessageType.Regular, 0x3B2, 500450, from.NetState); // You can only skin dead creatures.
			else if (toHarvest is Item)
				((Item)toHarvest).LabelTo(from, 500464); // Use this on corpses to carve away meat and hide
			else if (toHarvest is Targeting.StaticTarget || toHarvest is Targeting.LandTarget)
				from.SendLocalizedMessage(500489); // You can't use an axe on that.
			else
				from.SendLocalizedMessage(1005213); // You can't do that
		}

		public override void OnHarvestStarted(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
		{
			base.OnHarvestStarted(from, tool, def, toHarvest);

			from.RevealingAction();
		}

		public static void Initialize()
		{
			Array.Sort(m_TreeTiles);
		}

		#region Tile lists
		private static readonly int[] m_TreeTiles = new int[]
		{
			0x4CCA, 0x4CCB, 0x4CCC, 0x4CCD, 0x4CD0, 0x4CD3, 0x4CD6, 0x4CD8,
			0x4CDA, 0x4CDD, 0x4CE0, 0x4CE3, 0x4CE6, 0x4CF8, 0x4CFB, 0x4CFE,
			0x4D01, 0x4D41, 0x4D42, 0x4D43, 0x4D44, 0x4D57, 0x4D58, 0x4D59,
			0x4D5A, 0x4D5B, 0x4D6E, 0x4D6F, 0x4D70, 0x4D71, 0x4D72, 0x4D84,
			0x4D85, 0x4D86, 0x52B5, 0x52B6, 0x52B7, 0x52B8, 0x52B9, 0x52BA,
			0x52BB, 0x52BC, 0x52BD,
			0x4CCE, 0x4CCF, 0x4CD1, 0x4CD2, 0x4CD4, 0x4CD5, 0x4CD7, 0x4CD9,
			0x4CDB, 0x4CDC, 0x4CDE, 0x4CDF, 0x4CE1, 0x4CE2, 0x4CE4, 0x4CE5,
			0x4CE7, 0x4CE8, 0x4CF9, 0x4CFA, 0x4CFC, 0x4CFD, 0x4CFF, 0x4D00,
			0x4D02, 0x4D03, 0x4D45, 0x4D46, 0x4D47, 0x4D48, 0x4D49, 0x4D4A,
			0x4D4B, 0x4D4C, 0x4D4D, 0x4D4E, 0x4D4F, 0x4D50, 0x4D51, 0x4D52,
			0x4D53, 0x4D5C, 0x4D5D, 0x4D5E, 0x4D5F, 0x4D60, 0x4D61, 0x4D62,
			0x4D63, 0x4D64, 0x4D65, 0x4D66, 0x4D67, 0x4D68, 0x4D69, 0x4D73,
			0x4D74, 0x4D75, 0x4D76, 0x4D77, 0x4D78, 0x4D79, 0x4D7A, 0x4D7B,
			0x4D7C, 0x4D7D, 0x4D7E, 0x4D7F, 0x4D87, 0x4D88, 0x4D89, 0x4D8A,
			0x4D8B, 0x4D8C, 0x4D8D, 0x4D8E, 0x4D8F, 0x4D90, 0x4D95, 0x4D96,
			0x4D97, 0x4D99, 0x4D9A, 0x4D9B, 0x4D9D, 0x4D9E, 0x4D9F, 0x4DA1,
			0x4DA2, 0x4DA3, 0x4DA5, 0x4DA6, 0x4DA7, 0x4DA9, 0x4DAA, 0x4DAB,
			0x52BE, 0x52BF, 0x52C0, 0x52C1, 0x52C2, 0x52C3, 0x52C4, 0x52C5,
			0x52C6, 0x52C7, 0x31C4, 0X31BE, 0X31CA, 0X31C2, 0x31C0, 0X31C8,
			0X31C6, 0X0CD6, 0X0CD8, 0X144C, 0X1447, 0X152A, 0X1445, 0X1446,
			0X144B, 0X0C96, 0X144A, 0X155B, 0X309F, 0X309E, 0X31CE, 0X31CD,
			0X0CCA, 0x309C, 0x309D, 0x309E, 0x309F, 0x30A0, 0x30A1, 0x30A2,
			0x30A3, 0x30C3, 0x30BD, 0x30DA, 0x30D4, 0x31BE, 0x31C0, 0x31C2,
			0x3128, 0x3129, 0x312A, 0x31CE, 0x31C8, 0x31C6, 0x31D5, 0x31D6,
			0x1BC9, 0x1BCA, 0x1BCB, 0x1BCC, 0x1BCD, 0x1BCE, 0x1BCF, 0x1444,
			0x1445, 0x1446, 0x1447, 0x1448, 0x144A, 0x144B, 0x144C, 0x144D,
			0x149B, 0x149C, 0x149D, 0x149E, 0x1519, 0x1528, 0x1529, 0x152A,
			0x152B, 0x155A, 0x155B, 0x0C95, 0x0C96, 0x0C99, 0x0C9A, 0x0C9B,
			0x0C9C, 0x0C9D, 0x0CA6, 0x0CA8, 0x0CAA, 0x0CAB, 0x1444, 0x1445,
			0x1446, 0x1447, 0x1448, 0x144A, 0x144B, 0x144C, 0x0D37, 0x0D38,
			0x0D35, 0xD37,
		};
		#endregion
	}
}
