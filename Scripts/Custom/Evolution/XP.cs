using Server.Custom;
using Server.Mobiles;
using Server.Network;
using Server;
using System;
using Server.Items;

public class XP
{
	public static TimeSpan m_IntervaleXP = TimeSpan.FromMinutes(10);

	public static void Initialize()
	{
		new XPTimer().Start();
	}

	public class XPTimer : Timer
	{
		public XPTimer()
			: base(m_IntervaleXP, m_IntervaleXP)
		{
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick()
		{
			int day = (int)(DateTime.Now - CustomPersistence.Ouverture).TotalDays + 1;

			if (CustomPersistence.ProchainePay <= DateTime.Now)
			{
				CustomPersistence.ProchainePay = CustomPersistence.ProchainePay.AddDays(7);
				Server.Custom.System.GuildRecruter.Pay();

				BaseGarden.PayRent();

			}
			foreach (NetState state in NetState.Instances)
			{
				Mobile m = state.Mobile;

				if (m != null && m is CustomPlayerMobile pm && !pm.Jail)
				{
					bool isAccelerated = pm.FENormalTotal < 170;
					TimeSpan waitTime = isAccelerated ? TimeSpan.FromMinutes(10) : TimeSpan.FromMinutes(30);

					if (pm.NextFETime <= TimeSpan.Zero)
					{
						GainFE(pm, isAccelerated);
						pm.NextFETime = waitTime;
					}
					else
					{
						if (pm.LastLoginTime < DateTime.Now - TimeSpan.FromMinutes(10))
						{
							pm.NextFETime -= TimeSpan.FromMinutes(10);
						}
						else
						{
							pm.NextFETime -= DateTime.Now - pm.LastLoginTime;
						}
					}
				}
			}
		}
	}

	public static void GainFE(CustomPlayerMobile pm, bool isAccelerated)
	{
		if (pm == null)
			return;

		if (pm.LastNormalFE.Day != DateTime.Now.Day)
		{
			pm.FEDay = 0;
		}

		pm.LastNormalFE = DateTime.Now;

		int feToGain = isAccelerated ? 3 : 1;

		if (!isAccelerated && pm.FEDay + feToGain > 9)
		{
			pm.SendMessage("Vous avez atteint la limite quotidienne de FE.");
			return;
		}

		pm.FEDay += feToGain;
		pm.FENormalTotal += feToGain;

		pm.SendMessage($"Vous obtenez {feToGain} nouvelle(s) FE !");

		if (pm.StatAttente > 0)
		{
			int statGain = Math.Min(pm.StatAttente, feToGain * 3);
			pm.StatAttente -= statGain;
			pm.SendMessage($"Vous récupérez {statGain} points de statistiques en attente.");
		}
	}
}
