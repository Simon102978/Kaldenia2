using System;
using Server;
using Server.Mobiles;

namespace Server.Custom
{
	public static class TamingFollowersBonus
	{
		private const int BaseFollowersMax = 3; // Valeur de base pour FollowersMax

		public static void Initialize()
		{
			EventSink.Login += OnLogin;
			EventSink.Logout += OnLogout;
		}

		public static void OnLogin(LoginEventArgs e)
		{
			if (e.Mobile is PlayerMobile player)
			{
				ApplyBonus(player);
			}
		}

		public static void OnLogout(LogoutEventArgs e)
		{
			if (e.Mobile is PlayerMobile player)
			{
				player.FollowersMax = BaseFollowersMax;
			}
		}

		public static int CalculateBonus(Mobile m)
		{
			int tamingSkill = (int)m.Skills[SkillName.AnimalTaming].Base;

			if (tamingSkill >= 100)
				return 2;
			else if (tamingSkill >= 25)
				return 1;
			else
				return 0;
		}

		public static void ApplyBonus(PlayerMobile player)
		{
			int newBonus = CalculateBonus(player);
			int newFollowersMax = BaseFollowersMax + newBonus;

			if (player.FollowersMax != newFollowersMax)
			{
				player.FollowersMax = newFollowersMax;
				player.SendMessage($"Votre bonus de familiers est maintenant de +{newBonus} bas� sur votre comp�tence Taming.");
			}
		}

		public static void CheckBonus(Mobile m)
		{
			if (m is PlayerMobile player)
			{
				ApplyBonus(player);
			}
		}
	}
}
