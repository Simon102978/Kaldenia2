using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Scripts
{
	public class ClothingDurabilitySystem
	{
		private static Dictionary<PlayerMobile, DateTime> m_PlayerLastChecks = new Dictionary<PlayerMobile, DateTime>();

		public static void Initialize()
		{
			EventSink.Login += OnLogin;
			EventSink.Logout += OnLogout;
			EventSink.WorldSave += OnWorldSave;
		}

		public static void OnLogin(LoginEventArgs e)
		{
			if (e.Mobile is PlayerMobile player)
			{
				if (!m_PlayerLastChecks.ContainsKey(player))
				{
					m_PlayerLastChecks[player] = DateTime.UtcNow;
				}
				else
				{
					CheckPlayerDurability(player);
				}
			}
		}

		public static void OnLogout(LogoutEventArgs e)
		{
			if (e.Mobile is PlayerMobile player)
			{
				CheckPlayerDurability(player);
			}
		}

		public static void OnWorldSave(WorldSaveEventArgs e)
		{
			foreach (var player in m_PlayerLastChecks.Keys)
			{
				if (player.NetState != null) // Only check online players
				{
					CheckPlayerDurability(player);
				}
			}
		}

		public static void CheckPlayerDurability(PlayerMobile player)
		{
			if (!m_PlayerLastChecks.TryGetValue(player, out DateTime lastCheck))
			{
				lastCheck = DateTime.UtcNow;
				m_PlayerLastChecks[player] = lastCheck;
			}

			TimeSpan timeSinceLastCheck = DateTime.UtcNow - lastCheck;
			if (timeSinceLastCheck.TotalHours >= 2)
			{
				foreach (var item in player.Items)
				{
					if (item is BaseClothing clothing)
					{
						ApplyTimeDegradation(clothing, timeSinceLastCheck);
					}
				}

				m_PlayerLastChecks[player] = DateTime.UtcNow;
			}
		}

		public static void ApplyTimeDegradation(BaseClothing clothing, TimeSpan timeSinceLastCheck)
		{
			int twoHourPeriodsElapsed = (int)(timeSinceLastCheck.TotalHours / 2);
			int degradation = twoHourPeriodsElapsed * CalculateTimeDegradation(clothing);

			clothing.HitPoints -= degradation;
			if (clothing.HitPoints < 0) clothing.HitPoints = 0;
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
