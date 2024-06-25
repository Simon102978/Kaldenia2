using System;
using System.IO;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Gumps;
using System.Collections.Generic;
using Server.Custom;

namespace Server
{
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

		 if(CustomPersistence.ProchainePay <= DateTime.Now)
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

			if (pm.NextFETime <= TimeSpan.FromMinutes(10))
			{
				if (pm.FENormalTotal < day * 3)
	    		{
					GainFE(pm);
				}

				ResetFETime(pm);
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

    public static void ResetFETime(CustomPlayerMobile pm)
    {
			pm.NextFETime = TimeSpan.FromMinutes(30); 
    }

    public static void GainFE(CustomPlayerMobile pm)
    {
      if (pm == null)
        return;

	  if (pm.LastNormalFE.Day != DateTime.Now.Day)
	  {
		pm.FEDay = 0;
	  }

	  pm.LastNormalFE = DateTime.Now;

	  if (pm.FEDay >= 9)
	  {
		 pm.SendMessage("Vous avez atteint la limite quotidienne de FE.");
		 
	  }
	  else
	  {

		 pm.FE++;
		 pm.FEDay++;
		 pm.FENormalTotal++;


		 pm.SendMessage("Vous obtenez une nouvelle FE !");
	  }

			if (pm.StatAttente > 0)
			{
				if (pm.StatAttente > 3)
				{
					pm.StatAttente -= 3;
				}
				else
				{
					pm.StatAttente = 0;
				}		
			}
    
    }
  }
}