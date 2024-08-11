
using Server.Engines.Quests;
using Server.Engines.Quests.Collector;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;
using System;
using System.Collections.Generic;

namespace Server.Engines.Harvest
{
	public class CustomFishing : HarvestSystem
	{
		private static CustomFishing m_System;

		public static CustomFishing System
		{
			get
			{
				if (m_System == null)
					m_System = new CustomFishing();

				return m_System;
			}
		}

		private readonly HarvestDefinition m_Definition;

		public HarvestDefinition Definition => m_Definition;

		private CustomFishing()
		{
			HarvestResource[] res;
			HarvestVein[] veins;

			#region CustomFishing
			HarvestDefinition fish = new HarvestDefinition
			{
				// Resource banks are every 8x8 tiles
				BankWidth = 1,
				BankHeight = 1,

				// Every bank holds from 5 to 15 fish
				MinTotal = 8,
				MaxTotal = 15,

				// A resource bank will respawn its content every 10 to 20 minutes
				MinRespawn = TimeSpan.FromMinutes(15.0),
				MaxRespawn = TimeSpan.FromMinutes(25.0),

				// Skill checking is done on the Fishing skill
				Skill = SkillName.Fishing,

				// Set the list of harvestable tiles
				Tiles = m_WaterTiles,
			//	SpecialTiles = m_LavaTiles,
				//RangedTiles = true,

				// Players must be within 4 tiles to harvest
				MaxRange = 8,

				// One fish per harvest action
				ConsumedPerHarvest = 0,
				ConsumedPerFeluccaHarvest = 1,

				// The fishing
				EffectActions = new int[] { 6 },
				EffectSounds = new int[0],
				EffectCounts = new int[] { 1 },
				EffectDelay = TimeSpan.Zero,
				EffectSoundDelay = TimeSpan.FromSeconds(8.0),

				NoResourcesMessage = 503172, // The fish don't seem to be biting here.
				FailMessage = 503171, // You fish a while, but fail to catch anything.
				TimedOutOfRangeMessage = 500976, // You need to be closer to the water to fish!
				OutOfRangeMessage = 500976, // You need to be closer to the water to fish!
				PackFullMessage = 503176, // You do not have room in your backpack for a fish.
				ToolBrokeMessage = 503174 // You broke your fishing pole.
			};

			// Define different bait types and their associated fish types
			Dictionary<Bait, Type[]> baitFishMap = new Dictionary<Bait, Type[]>
			{
				{ Bait.Fish, new Type[] { typeof(Fish) } },
				{ Bait.AutumnDragonfish, new Type[] { typeof(AutumnDragonfish) } },
				{ Bait.BlueLobster, new Type[] { typeof(BlueLobster) } },
				{ Bait.BullFish, new Type[] { typeof(BullFish) } },
				{ Bait.CrystalFish, new Type[] { typeof(CrystalFish) } },
				{ Bait.FairySalmon, new Type[] { typeof(FairySalmon) } },
				{ Bait.FireFish, new Type[] { typeof(FireFish) } },
				{ Bait.GiantKoi, new Type[] { typeof(GiantKoi) } },
				{ Bait.GreatBarracuda, new Type[] { typeof(GreatBarracuda) } },
				{ Bait.HolyMackerel, new Type[] { typeof(HolyMackerel) } },
				{ Bait.LavaFish, new Type[] { typeof(LavaFish) } },
				{ Bait.ReaperFish, new Type[] { typeof(ReaperFish) } },
				{ Bait.SpiderCrab, new Type[] { typeof(SpiderCrab) } },
				{ Bait.StoneCrab, new Type[] { typeof(StoneCrab) } },
				{ Bait.SummerDragonfish, new Type[] { typeof(SummerDragonfish) } },
				{ Bait.UnicornFish, new Type[] { typeof(UnicornFish) } },
				{ Bait.YellowtailBarracuda, new Type[] { typeof(YellowtailBarracuda) } },
				{ Bait.AbyssalDragonfish, new Type[] { typeof(AbyssalDragonfish) } },
				{ Bait.BlackMarlin, new Type[] { typeof(BlackMarlin) } },
				{ Bait.BloodLobster, new Type[] { typeof(BloodLobster) } },
				{ Bait.BlueMarlin, new Type[] { typeof(BlueMarlin) } },
				{ Bait.DreadLobster, new Type[] { typeof(DreadLobster) } },
				{ Bait.DungeonPike, new Type[] { typeof(DungeonPike) } },
				{ Bait.GiantSamuraiFish, new Type[] { typeof(GiantSamuraiFish) } },
				{ Bait.GoldenTuna, new Type[] { typeof(GoldenTuna) } },
				{ Bait.Kingfish, new Type[] { typeof(Kingfish) } },
				{ Bait.LanternFish, new Type[] { typeof(LanternFish) } },
				{ Bait.RainbowFish, new Type[] { typeof(RainbowFish) } },
				{ Bait.SeekerFish, new Type[] { typeof(SeekerFish) } },
				{ Bait.SpringDragonfish, new Type[] { typeof(SpringDragonfish) } },
				{ Bait.StoneFish, new Type[] { typeof(StoneFish) } },
				{ Bait.TunnelCrab, new Type[] { typeof(TunnelCrab) } },
				{ Bait.VoidCrab, new Type[] { typeof(VoidCrab) } },
				{ Bait.VoidLobster, new Type[] { typeof(VoidLobster) } },
				{ Bait.WinterDragonfish, new Type[] { typeof(WinterDragonfish) } },
				{ Bait.ZombieFish, new Type[] { typeof(ZombieFish) } }
                // Add more mappings for other bait types as needed
            };

			res = new HarvestResource[]
						{
				new HarvestResource(130.0, 130.0, 130.0, null, null)
						};

			veins = new HarvestVein[]
			{
				new HarvestVein(100.0, 0.0, res[0], null)
			};

			fish.Resources = res;
			fish.Veins = veins;

			fish.BonusResources = new BonusHarvestResource[]
			{
			
				new BonusHarvestResource(0, 90.0, null, null), //set to same chance as mining ml gems
                new BonusHarvestResource(15.0, 2.0, "Coquillage", typeof(CoquillageHautsFonds)),
				new BonusHarvestResource(15.0, 2.0, "Coquillage", typeof(CoquillageArcEnCiel)),
				new BonusHarvestResource(15.0, 2.0, "Coquillage", typeof(CoquilleDoree)),
				new BonusHarvestResource(15.0, 2.0, "Coquillage", typeof(CoquilleEcarlate)),
				new BonusHarvestResource(15.0, 2.0, "Coquillage", typeof(CoquillePlate)),
				new BonusHarvestResource(15.0, 2.0, "Coquillage", typeof(CoquilleTachetee)),
				new BonusHarvestResource(30.0, 2.0, "Dent de requin", typeof(DentRequin)),
				new BonusHarvestResource(30.0, 2.0, "Dent d'alligator", typeof(DentAlligator)),
				new BonusHarvestResource(30.0, 2.0, "Graisse de sole", typeof(GraisseSole)),
				new BonusHarvestResource(30.0, 2.0, "Oeil de raie", typeof(OeilRaie)),
				new BonusHarvestResource(30.0, 2.0, "Oeuf de thon", typeof(OeufThon)),
				new BonusHarvestResource(30.0, 2.0, "Sang d'anguille", typeof(SangAnguille)),

				new BonusHarvestResource(1.0, 1.0, "Baril inachevé", typeof(UnfinishedBarrel)),
				new BonusHarvestResource(1.0, 1.0, "Tabouret", typeof(Stool)),
				new BonusHarvestResource(1.0, 1.0, "Horloge cassée", typeof(ClockFrame)),
				new BonusHarvestResource(1.0, 1.0, "Globe terrestre", typeof(Globe)),
				new BonusHarvestResource(1.0, 1.0, "Tonnelier", typeof(BarrelLid)),

				new BonusHarvestResource(50.0, 2.0, "PrizedFish", typeof(PrizedFish)),
				new BonusHarvestResource(50.0, 2.0, "WondrousFish", typeof(WondrousFish)),
				new BonusHarvestResource(50.0, 2.0, "TrulyRareFish", typeof(TrulyRareFish)),
				new BonusHarvestResource(50.0, 2.0, "PeculiarFish", typeof(PeculiarFish)),

				new BonusHarvestResource(10.0, 2.0, "Bottes", typeof(Boots)),
				new BonusHarvestResource(10.0, 2.0, "Chaussures", typeof(Shoes)),
				new BonusHarvestResource(1.0, 2.0, "Sandales", typeof(Sandals)),
				new BonusHarvestResource(1.0, 2.0, "Cuissardes", typeof(ThighBoots)),

				new BonusHarvestResource(5.0, 1.0, "Perle noire", typeof(BlackPearl)),
				new BonusHarvestResource(50.0, 0.5, "Perle Blanche", typeof(WhitePearl)),
				new BonusHarvestResource(50.0, 2.0, "Un poisson des bas fonds", typeof(BaseHighseasFish)),
				new BonusHarvestResource(70.0, 3.0, "Carte Mystérieuse", typeof(SkillCard)),






			}; 

			m_Definition = fish;
			Definitions.Add(fish);
			#endregion
		}
	


	

		public override void OnConcurrentHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
		{
			from.SendLocalizedMessage(500972); // You are already fishing.
		}

	
		
		public override void StartHarvesting(Mobile from, Item tool, object toHarvest)
		{
			if (from != null && tool != null && tool.IsChildOf(from.Backpack))
			{
				Item item = from.FindItemOnLayer(Layer.OneHanded);
				Item item2 = from.FindItemOnLayer(Layer.TwoHanded);

				if (item != null)
					from.AddToBackpack(item);

				if (item2 != null)
					from.AddToBackpack(item2);

				Timer.DelayCall(TimeSpan.FromMilliseconds(250), () =>
				{
					from.EquipItem(tool);
				});
			}

			base.StartHarvesting(from, tool, toHarvest);
		}

		public override void OnHarvestStarted(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
		{
			base.OnHarvestStarted(from, tool, def, toHarvest);

			int tileID;
			Map map;
			Point3D loc;

			if (GetHarvestDetails(from, tool, toHarvest, out tileID, out map, out loc))
				Timer.DelayCall(TimeSpan.FromSeconds(1.5),
					delegate
					{
						from.RevealingAction();

						int sound = 0x364;
						int effect = 0x352D;

						

						Effects.SendLocationEffect(loc, map, effect, 16, 4);
						Effects.PlaySound(loc, map, sound);
					});
		}

		
 public override void OnHarvestFinished(Mobile from, Item tool, HarvestDefinition def, HarvestVein vein, HarvestBank bank, HarvestResource resource, object harvested)
		{
			base.OnHarvestFinished(from, tool, def, vein, bank, resource, harvested);

			if (tool is LargeFishingPole fishingPole)
			{
				fishingPole.FinishFishing(from);
			}
			else
			{
				from.SendMessage("You need a proper fishing pole to fish.");
			}

		from.RevealingAction();
		}

		public override object GetLock(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
		{
			return this;
		}

		public override bool BeginHarvesting(Mobile from, Item tool)
		{
			if (!base.BeginHarvesting(from, tool))
				return false;

			from.SendLocalizedMessage(500974); // What water do you want to fish in?
			return true;
		}

		public override bool CheckHarvest(Mobile from, Item tool)
		{
			if (!base.CheckHarvest(from, tool))
				return false;

			if (from.Mounted || from.Flying)
			{
				from.SendLocalizedMessage(500971); // You can't fish while riding!
				return false;
			}

			return true;
		}

		public override bool CheckHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
		{
			if (!base.CheckHarvest(from, tool, def, toHarvest))
				return false;

			if (from.Mounted || from.Flying)
			{
				from.SendLocalizedMessage(500971); // You can't fish while riding!
				return false;
			}

			return true;
		}

		private static readonly int[] m_WaterTiles = new int[]
		{
			0x00A8, 0x00A9,
			0x00AA, 0x00AB,

			
		};

		
	}
}
