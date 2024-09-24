using Server.Items;
using Server.Network;
using System;
using System.Linq;

namespace Server.Engines.Harvest
{
	public class CustomLumberjacking : HarvestSystem
	{
		private static CustomLumberjacking m_GeneralSystem { get; set; }

		public static HarvestSystem GetSystem(Item item)
		{
			Map map;
			Point3D loc;

			object root = item.RootParent;

			if (root == null)
			{
				map = item.Map;
				loc = item.Location;
			}
			else
			{
				map = ((IEntity)root).Map;
				loc = ((IEntity)root).Location;
			}

			IPooledEnumerable eable = map.GetItemsInRange(loc, 100);

			foreach (Item i in eable)
			{
				if (i is CustomLumberjackingStone)
					return ((CustomLumberjackingStone)i).HarvestSystem;
			}

			if (m_GeneralSystem == null)
				m_GeneralSystem = new CustomLumberjacking();

			return m_GeneralSystem;
		}

		private readonly HarvestDefinition m_Definition;

		public HarvestDefinition Definition => m_Definition;

		public CustomLumberjacking()
		{
			HarvestResource[] res;
			HarvestVein[] veins;

			#region CustomLumberjacking
			HarvestDefinition lumber = new HarvestDefinition
			{
				// Resource banks are every 2x2 tiles
				BankWidth = 2,
				BankHeight = 2,

				// Every bank holds from 10 to 20 logs
				MinTotal = 10,
				MaxTotal = 20,

				// A resource bank will respawn its content every 20 to 30 minutes
				MinRespawn = TimeSpan.FromMinutes(30.0),
				MaxRespawn = TimeSpan.FromMinutes(60.0),

				// Skill checking is done on the CustomLumberjacking skill
				Skill = SkillName.Lumberjacking,

				// Set the list of harvestable tiles
				Tiles = m_TreeTiles, 

				// Players must be within 2 tiles to harvest
				MaxRange = 2,

				// Five logs per harvest action
				ConsumedPerHarvest = Utility.RandomMinMax(3, 7),
				ConsumedPerFeluccaHarvest = Utility.RandomMinMax(3, 7),

				// The chopping effect
				EffectActions = new int[] { 7 },
				EffectSounds = new int[] { 0x13E },
				EffectCounts = (new int[] { 3 }),
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
				new HarvestResource(00.0, 00.0, 100.0, "Palmier",        typeof(PalmierLog)),
				new HarvestResource(00.0, 00.0, 100.0, "Érable",      typeof(ErableLog)),
				new HarvestResource(20.0, 20.0, 100.0, "Chêne",     typeof(CheneLog)),
				new HarvestResource(20.0, 20.0, 100.0, "Cèdre",     typeof(CedreLog)),
				new HarvestResource(40.0, 40.0, 100.0, "Cyprès",    typeof(CypresLog)),
				new HarvestResource(40.0, 40.0, 100.0, "Saule",      typeof(SauleLog)),
				new HarvestResource(60.0, 60.0, 100.0, "Acajou",    typeof(AcajouLog)),
				new HarvestResource(60.0, 60.0, 100.0, "Ébène",    typeof(EbeneLog)),
				new HarvestResource(80.0, 80.0, 100.0, "Amarante",     typeof(AmaranteLog)),
				new HarvestResource(80.0, 80.0, 100.0, "Pin",     typeof(PinLog)),
				new HarvestResource(90.0, 90.0, 100.0, "Ancien",        typeof(AncienLog)),
			};

			veins = new HarvestVein[]
			{
				new HarvestVein(100.0, 0.0, res[0], null), // Normal
            };

			lumber.BonusResources = new BonusHarvestResource[]
			{
				new BonusHarvestResource(0, 90.0, null, null), //Nothing
				new BonusHarvestResource(10.0, 3.0, "Vous bucher, un nid d'oiseau tombe au sol!", typeof(BirdNest)),

    //            new BonusHarvestResource(100, 10.0, 1072548, typeof(BarkFragment)),
				//new BonusHarvestResource(100, 03.0, 1072550, typeof(LuminescentFungi)),
				//new BonusHarvestResource(100, 02.0, 1072547, typeof(SwitchItem)),
				//new BonusHarvestResource(100, 01.0, 1072549, typeof(ParasiticPlant)),
				//new BonusHarvestResource(100, 01.0, 1072551, typeof(BrilliantAmbre)),
				//new BonusHarvestResource(100, 01.0, 1113756, typeof(CrystalShards), Map.TerMur),

/*new BonusHarvestResource(0, 1.0, "Vous avez trouvé de l'acacia ", typeof(Acacia)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de l'anis ", typeof(Anise)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé du basilic ", typeof(Basil)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé des feuilles de laurier ", typeof(BayLeaf)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de la camomille ", typeof(Chamomile)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé du cumin ", typeof(Caraway)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de la coriandre ", typeof(Cilantro)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de la cannelle ", typeof(Cinnamon)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé des clous de girofle ", typeof(Clove)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de la résine ", typeof(Copal)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé du cerfeuil ", typeof(Coriander)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de l'aneth ", typeof(Dill)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé du sang de dragon ", typeof(Dragonsblood)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de l'encens ", typeof(Frankincense)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de la lavande ", typeof(Lavender)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de la marjolaine ", typeof(Marjoram)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de la reine-des-prés ", typeof(Meadowsweet)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de la menthe ", typeof(Mint)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de l'armoise commune ", typeof(Mugwort)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé des graines de moutarde ", typeof(Mustard)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de la gomme d'épinette ", typeof(Myrrh)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé des olives ", typeof(Olive)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de l'origan ", typeof(Oregano)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de la racine d'orris ", typeof(Orris)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé du patchouli ", typeof(Patchouli)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé des grains de poivre ", typeof(Peppercorn)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé des roses ", typeof(RoseHerb)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé du romarin ", typeof(Rosemary)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé du safran ", typeof(Saffron)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé du bois de santal ", typeof(Sandelwood)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de l'orme glissante ", typeof(SlipperyElm)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé du thym ", typeof(Thyme)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de la valériane ", typeof(Valerian)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé de l'écorce de saule ", typeof(WillowBark)),
new BonusHarvestResource(0, 1.0, "Vous avez trouvé des baies tribales ", typeof(TribalBerry))*/
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
		private string GetSimpleWoodName(Item item)
		{
			string typeName = item.GetType().Name;
			if (typeName.EndsWith("Log"))
			{
				typeName = typeName.Substring(0, typeName.Length - 3);
			}

			switch (typeName)
			{
				case "Palmier": return "Palmier";
				case "Erable": return "Érable";
				case "Chene": return "Chêne";
				case "Cedre": return "Cèdre";
				case "Cypres": return "Cyprès";
				case "Saule": return "Saule";
				case "Acajou": return "Acajou";
				case "Ebene": return "Ébène";
				case "Amarante": return "Amarante";
				case "Pin": return "Pin";
				case "Ancien": return "Ancien";
				default: return typeName;
			}
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
							string woodName = GetSimpleWoodName(item);

							if (item.GetType() == type)
							{
								from.SendMessage($"Vous avez extrait {item.Amount} bûche(s) de {woodName}!");
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
			0x52C6, 0x52C7,

			3480, 3288,3302, 3275, 3299,3230, 3290,3296, 3286,  3283,  3293,  3476, 3287, 3304,
			3280,3274, 3395, 3461, 3160, 3326,3323,3320, 3440,3395, 3423,3326,3329,3234,3242,3221,3222,3383,
			3277, 4794,4792,4795, 3417,3395,3394,


			0x0C95, 3221, 
			0X0C96, 3222, 
			0X0CA8, 3240,
			0X0CAA, 3242,
			0XCAB, 3243, 
			0X1444, 5188,
			0X1445, 5189,
			0X1446, 5190,
			0X1447, 5191,
			0X1448, 5192,
			0X144A, 5194,
			0X144B, 5195,
			0X144C, 5196, 
			
			19626,19606,19605, 19624,19624,21580,19767,21572,21575,21574, 21573, 21578, 21579, 19613, 21576, 21803, 19627, 21851,19860,19614, 19864


		};
		#endregion
	}
}
