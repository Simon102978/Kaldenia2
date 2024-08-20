using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Scripts
{
	public class ClothingDurabilitySystem
	{
		public static void Initialize()
		{
			EventSink.WorldLoad += OnWorldLoad;
			EventSink.WorldSave += OnWorldSave;
		}

		private static DateTime m_LastGlobalCheck;
		private static TimeSpan m_CheckInterval = TimeSpan.FromHours(1);

		public static void OnWorldLoad()
		{
			m_LastGlobalCheck = DateTime.UtcNow;
			Timer.DelayCall(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5), CheckGlobalDurability);
		}

		public static void OnWorldSave(WorldSaveEventArgs e)
		{
			CheckGlobalDurability();
		}

		public static void CheckGlobalDurability()
		{
			if (DateTime.UtcNow - m_LastGlobalCheck < m_CheckInterval)
				return;

			foreach (var m in World.Mobiles.Values)
			{
				if (m is PlayerMobile player)
				{
					foreach (var item in player.Items)
					{
						if (item is BaseClothing clothing)
						{
							ApplyTimeDegradation(clothing);
						}
					}
				}
			}

			m_LastGlobalCheck = DateTime.UtcNow;
		}

		public static void ApplyTimeDegradation(BaseClothing clothing)
		{
			if (clothing.LastDurabilityCheck == DateTime.MinValue)
				clothing.LastDurabilityCheck = DateTime.UtcNow;

			TimeSpan timeSinceLastCheck = DateTime.UtcNow - clothing.LastDurabilityCheck;
			if (timeSinceLastCheck.TotalHours >= 2)
			{
				int twoHourPeriodsElapsed = (int)(timeSinceLastCheck.TotalHours / 2);
				int degradation = twoHourPeriodsElapsed * CalculateTimeDegradation(clothing);

				clothing.HitPoints -= degradation;
				if (clothing.HitPoints < 0) clothing.HitPoints = 0;

				clothing.LastDurabilityCheck = DateTime.UtcNow;
			}
		}

		public static int CalculateTimeDegradation(BaseClothing clothing)
		{
			switch (clothing.Quality)
			{
				case ItemQuality.Low: return 2;
				case ItemQuality.Normal: return 1;
				case ItemQuality.Exceptional: return 1;
				case ItemQuality.Epic: return 1;
				case ItemQuality.Legendary: return 1;
				default: return 1;
			}
		}
	}
}
