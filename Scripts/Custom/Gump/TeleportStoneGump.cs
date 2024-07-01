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



namespace Server.Gumps
{

	public class TeleportStoneGump : BaseProjectMGump
	{

		private CustomPlayerMobile m_From;

		private TeleportStone m_TeleportStone;

		private int m_Page;

		public TeleportStoneGump(CustomPlayerMobile from,TeleportStone stone , int page = 0) : base("Pierre de téléportation", 425, 415, false)
		{
			m_From = from;
			m_TeleportStone  = stone;
			m_Page = page;	


			AddPage(0);

			AddSection(70 , 73, 465, 120,"Options");

			AddButtonHtlml(84,110,3, !stone.Active ? "Activez la pierre" : "Désactivez la pierre","#FFFFFF");
			AddButtonHtlml(84,130,4, "Ajoutez une location d'arriver","#FFFFFF");
			AddButtonHtlml(84,150,5, "Vous liez à cette pierre.","#FFFFFF");

	//		AddTextEntryBg(84, 120, 420, 25, 0, 10, entry.Title);

			AddSection(70 , 195, 465, 330,"Landings","");
			AddButton(505, 239, 1, 251, 250);
			AddButton(505, 488, 2, 253, 252);

			int n = 0;

			int maxI = (page +1) * 9;

			if (maxI > m_TeleportStone.Landing.Count)
			{
				maxI = m_TeleportStone.Landing.Count;
			}

			for (int i = 0 + page * 9; i < maxI; i++)
			{
				 AddBackground(84, 239 + n* 30, 420, 30, 9350);
				 AddHtmlTexteColored(90,245 + n* 30,100,m_TeleportStone.Landing[i].ToString(),"#000000");
				 AddButton(300,245 + n* 30,1000 + i,2463,2464);
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

					m_From.SendGump(new TeleportStoneGump(m_From, m_TeleportStone, pagi));		
				}
				else if (info.ButtonID == 2)
				{
					int pagi = m_Page + 1;

					if (pagi >= m_TeleportStone.Landing.Count / 9 + 1)
					{
						pagi--;
					}

					m_Page = pagi;

					m_From.SendGump(new TeleportStoneGump(m_From, m_TeleportStone, pagi));	
				}
				else if(info.ButtonID == 3)
				{
			
					m_TeleportStone.Active = !m_TeleportStone.Active;
					m_From.SendGump(new TeleportStoneGump(m_From, m_TeleportStone, m_Page));	
				}
				else if(info.ButtonID == 4)
				{
 					from.SendMessage("Où désirez-vous que les magiciens apparaissent ? ");
                	from.Target = new SpawnLocationTarget(m_TeleportStone);

				}
				else if(info.ButtonID == 5)
				{
					m_TeleportStone.LinkToStone(from);
					m_From.SendGump(new TeleportStoneGump(m_From, m_TeleportStone, m_Page));	
				}
				else if(info.ButtonID >= 1000)
				{

					m_TeleportStone.DeleteLocation(info.ButtonID - 1000);
					m_From.SendGump(new TeleportStoneGump(m_From, m_TeleportStone, m_Page));	


				}
						
			
			

			}
		}

		 public class SpawnLocationTarget : Target
        {
            public TeleportStone m_Cercle;

            public SpawnLocationTarget(TeleportStone cercle) : base(-1, true, TargetFlags.None)
            {
                m_Cercle = cercle;
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

					if (m_Cercle.AddLocation(point))
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

				from.SendGump(new TeleportStoneGump((CustomPlayerMobile)from,m_Cercle,0));

            }
        }


	
	
	}
}