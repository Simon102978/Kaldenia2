using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Engines.Harvest
{
	public class HarvestDefinition
	{
		public HarvestDefinition()
		{
			Banks = new Dictionary<Map, Dictionary<Point2D, HarvestBank>>();
		}

		public int BankWidth { get; set; }
		public int BankHeight { get; set; }
		public int MinTotal { get; set; }
		public int MaxTotal { get; set; }
		public int[] Tiles { get; set; }
		public int[] SpecialTiles { get; set; }
		public bool RangedTiles { get; set; }
		public TimeSpan MinRespawn { get; set; }
		public TimeSpan MaxRespawn { get; set; }
		public int MaxRange { get; set; }
		public int ConsumedPerHarvest { get; set; }
		public int ConsumedPerFeluccaHarvest { get; set; }
		public bool PlaceAtFeetIfFull { get; set; }
		public SkillName Skill { get; set; }
		public int[] EffectActions { get; set; }
		public int[] EffectCounts { get; set; }
		public int[] EffectSounds { get; set; }
		public TimeSpan EffectSoundDelay { get; set; }
		public TimeSpan EffectDelay { get; set; }
		public object NoResourcesMessage { get; set; }
		public object OutOfRangeMessage { get; set; }
		public object TimedOutOfRangeMessage { get; set; }
		public object DoubleHarvestMessage { get; set; }
		public object FailMessage { get; set; }
		public object PackFullMessage { get; set; }
		public object ToolBrokeMessage { get; set; }
		public HarvestResource[] Resources { get; set; }
		public HarvestVein[] Veins { get; set; }
		public BonusHarvestResource[] BonusResources { get; set; }
		public bool RaceBonus { get; set; }
		public bool RandomizeVeins { get; set; }
		public Dictionary<Map, Dictionary<Point2D, HarvestBank>> Banks { get; set; }

		public void SendMessageTo(Mobile from, object message)
		{
			if (message is int)
				from.SendLocalizedMessage((int)message);
			else if (message is string)
				from.SendMessage((string)message);
		}

		public static Bait ToBait(string name)
		{
			// Use Enum.TryParse to make this cleaner and less error-prone
			if (Enum.TryParse(name, out Bait bait))
				return bait;
			else
				return Bait.Aucun;
		}

		public HarvestBank GetBank(Map map, int x, int y)
		{
			if (map == null || map == Map.Internal)
				return null;

			x /= BankWidth;
			y /= BankHeight;

			if (!Banks.TryGetValue(map, out var banks))
				Banks[map] = banks = new Dictionary<Point2D, HarvestBank>();

			var key = new Point2D(x, y);

			if (!banks.TryGetValue(key, out var bank))
				banks[key] = bank = new HarvestBank(this, GetVeinAt(map, x, y));

			return bank;
		}

		public HarvestVein GetVeinAt(Map map, int x, int y)
		{
			if (Veins.Length == 1)
				return Veins[0];

			double randomValue;

			if (RandomizeVeins)
			{
				randomValue = Utility.RandomDouble();
			}
			else
			{
				Random random = new Random((x * 17) + (y * 11) + (map.MapID * 3));
				randomValue = random.NextDouble();
			}

			return GetVeinFrom(randomValue);
		}

		public HarvestVein GetVeinFrom(double randomValue)
		{
			if (Veins.Length == 1)
				return Veins[0];

			randomValue *= 100;

			foreach (var vein in Veins)
			{
				if (randomValue <= vein.VeinChance)
					return vein;

				randomValue -= vein.VeinChance;
			}

			return null;
		}

		public BonusHarvestResource GetBonusResource()
		{
			if (BonusResources == null)
				return null;

			double randomValue = Utility.RandomDouble() * 100;

			foreach (var bonusResource in BonusResources)
			{
				if (randomValue <= bonusResource.Chance)
					return bonusResource;

				randomValue -= bonusResource.Chance;
			}

			return null;
		}

		public bool Validate(int tileID)
		{
			if (RangedTiles)
			{
				foreach (var tile in Tiles)
				{
					if (tileID == tile)
						return true;
				}

				return false;
			}
			else
			{
				foreach (var tile in Tiles)
				{
					if (tile == tileID)
						return true;
				Console.WriteLine(tileID);

				}
				Console.WriteLine(tileID);

				return false;

			}
		}

		public bool ValidateSpecial(int tileID)
		{
			if (SpecialTiles == null || SpecialTiles.Length == 0)
				return true;

			foreach (var tile in SpecialTiles)
			{
				if (tileID == tile)
					return true;
			}

			return false;
		}
	}
}
