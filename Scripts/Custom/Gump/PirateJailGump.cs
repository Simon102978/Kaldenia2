using System;
using Server;
using Server.Commands;
using Server.Gumps;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;
using Server.Custom;



namespace Server.Gumps
{

	public class PirateJailGump : BaseProjectMGump
	{

		private CustomPlayerMobile m_From;

	
		private int m_Page;

		public PirateJailGump(CustomPlayerMobile from, int page = 0) : base("Pirate Jail", 425, 515, false)
		{
			m_From = from;
			m_Page = page;	


			AddPage(0);

			AddSection(70 , 73, 465, 220,"Options");

			AddButtonHtlml(84,110,3, !CustomPersistence.PirateJailActive ? "Activez la prison" : "Désactivez la prison","#FFFFFF");
			AddButtonHtlml(84,130,4, "Ajoutez une location","#FFFFFF");

	
	//		AddTextEntryBg(84, 120, 420, 25, 0, 10, entry.Title);

			AddSection(70 , 295, 465, 330,"Position","");
			AddButton(505, 339, 1, 251, 250);
			AddButton(505, 588, 2, 253, 252);

			int n = 0;

			int maxI = (page +1) * 9;

			if (maxI > CustomPersistence.m_PirateLanding.Count)
			{
				maxI = CustomPersistence.m_PirateLanding.Count;
			}

			for (int i = 0 + page * 9; i < maxI; i++)
			{
				 AddBackground(84, 339 + n* 30, 420, 30, 9350);
				 AddHtmlTexteColored(90,345 + n* 30,175,CustomPersistence.m_PirateLanding[i].ToString(),"#000000");
				 AddButton(300,345 + n* 30,1000 + i,2463,2464);
				 n++;

			}






		}






		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
		    CustomPlayerMobile from = (CustomPlayerMobile)sender.Mobile;

			if (info.ButtonID > 0)
			{
			
				
				if (info.ButtonID == 1)
				{
					int pagi = m_Page;

					if (pagi > 0)
					{
						pagi -= 1;
					}		

					m_Page = pagi;	

					m_From.SendGump(new PirateJailGump(m_From, pagi));		
				}
				else if (info.ButtonID == 2)
				{
					int pagi = m_Page + 1;

					if (pagi >= CustomPersistence.m_PirateLanding.Count / 9 + 1)
					{
						pagi--;
					}

					m_Page = pagi;

					m_From.SendGump(new PirateJailGump(m_From, pagi));	
				}
				else if(info.ButtonID == 3)
				{
					if (CustomPersistence.m_PirateLanding.Count == 0)
					{
						m_From.SendMessage("Vous devez mettre des endroits avant d'activer la prison.");
					}
					else
					{
						CustomPersistence.PirateJailActive = !CustomPersistence.PirateJailActive ;
					}
				
					m_From.SendGump(new PirateJailGump(m_From, m_Page));	
				}
				else if(info.ButtonID == 4)
				{
 					from.SendMessage("Où désirez-vous que les joueurs apparaissent ? ");
                	from.Target = new SpawnLocationTarget();

				}
				else if(info.ButtonID >= 1000)
				{

					CustomPersistence.DeletePirateJailLocation(info.ButtonID - 1000);
					m_From.SendGump(new PirateJailGump(m_From,  m_Page));	


				}
						
			
			

			}
		}

		 public class SpawnLocationTarget : Target
        {
            public SpawnLocationTarget() : base(-1, true, TargetFlags.None)
            {

            }

            protected override void OnTarget(Mobile from, object o)
            {

                IPoint3D p = o as IPoint3D;

                if (p != null)
                {
                    if (p is Item)
                        p = ((Item)p).GetWorldTop();
                    else if (p is Mobile)
                        p = ((Mobile)p).Location;

                    Point3D point = new Point3D(p);

					if (CustomPersistence.AddPirateJailLocation(point))
					{
						from.SendMessage("Vous rajoutez la location.");
					}
					else
					{
						from.SendMessage("Location invalide.");
					}
                }
                else
                {
                    from.SendMessage("Vous ciblez une location.");
                }

				from.SendGump(new PirateJailGump((CustomPlayerMobile)from,0));

            }
        }


	
	
	}
}