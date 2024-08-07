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

	public class WallControlerGump : BaseProjectMGump
	{

		private CustomPlayerMobile m_From;

		private WallControlerStone m_Stone;

		private int m_Page;

		public WallControlerGump(CustomPlayerMobile from,WallControlerStone stone , int page = 0) : base("Controleur de mur", 425, 515, false)
		{
			m_From = from;
			m_Stone  = stone;
			m_Page = page;	


			AddPage(0);

			AddSection(70 , 73, 465, 220,"Options");

			AddButtonHtlml(84,110,3, !stone.Active ? "Activez la pierre" : "Désactivez la pierre","#FFFFFF");
			AddButtonHtlml(84,130,4, "Ajoutez une location","#FFFFFF");

			

			AddHtmlTexteColored(84, 160, 75, "Name: ", "#ffffff");
			AddTextEntryBg(150, 155, 200, 25, 0, 0, m_Stone.WallName);

		   	AddHtmlTexteColored(84, 190, 75, "Item Id: ", "#ffffff");
			AddTextEntryBg(150, 185, 200, 25, 0, 1, m_Stone.WallItemId.ToString());

		  	AddHtmlTexteColored(84, 220, 75, "Hue: ", "#ffffff");
			AddTextEntryBg(150, 215, 200, 25, 0, 2, m_Stone.WallHue.ToString());

			AddButtonHtlml(84,240,5, "Appliquer","#FFFFFF");

	//		AddTextEntryBg(84, 120, 420, 25, 0, 10, entry.Title);

			AddSection(70 , 295, 465, 330,"Wall","");
			AddButton(505, 339, 1, 251, 250);
			AddButton(505, 588, 2, 253, 252);

			int n = 0;

			int maxI = (page +1) * 9;

			if (maxI > m_Stone.Wall.Count)
			{
				maxI = m_Stone.Wall.Count;
			}

			for (int i = 0 + page * 9; i < maxI; i++)
			{
				 AddBackground(84, 339 + n* 30, 420, 30, 9350);
				 AddHtmlTexteColored(90,345 + n* 30,100,m_Stone.Wall[i].ToString(),"#000000");
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

					m_From.SendGump(new WallControlerGump(m_From, m_Stone, pagi));		
				}
				else if (info.ButtonID == 2)
				{
					int pagi = m_Page + 1;

					if (pagi >= m_Stone.Wall.Count / 9 + 1)
					{
						pagi--;
					}

					m_Page = pagi;

					m_From.SendGump(new WallControlerGump(m_From, m_Stone, pagi));	
				}
				else if(info.ButtonID == 3)
				{
			
					m_Stone.Active = !m_Stone.Active;
					m_From.SendGump(new WallControlerGump(m_From, m_Stone, m_Page));	
				}
				else if(info.ButtonID == 4)
				{
 					from.SendMessage("Où désirez-vous que le murs apparaissent ? ");
                	from.Target = new SpawnLocationTarget(m_Stone);

				}
				else if(info.ButtonID == 5)
				{

					

					if (info.GetTextEntry(0) != null)
					{
						 m_Stone.WallName = info.GetTextEntry(0).Text; 
					}

					 string itemIdS = info.GetTextEntry(1).Text;
					 int itemIdInt = 0;

					 if (int.TryParse(itemIdS, out itemIdInt))
					 {
						m_Stone.WallItemId = itemIdInt;
					 } 


					string hueId = info.GetTextEntry(2).Text;

					int HueIdInt = 0;

 					if (int.TryParse(hueId, out HueIdInt))
					 {
						m_Stone.WallHue = HueIdInt;
					 } 

				
					m_From.SendGump(new WallControlerGump(m_From, m_Stone, m_Page));	
				}
				else if(info.ButtonID >= 1000)
				{

					m_Stone.DeleteLocation(info.ButtonID - 1000);
					m_From.SendGump(new WallControlerGump(m_From, m_Stone, m_Page));	


				}
						
			
			

			}
		}

		 public class SpawnLocationTarget : Target
        {
            public WallControlerStone m_Cercle;

            public SpawnLocationTarget(WallControlerStone cercle) : base(-1, true, TargetFlags.None)
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

				from.SendGump(new WallControlerGump((CustomPlayerMobile)from,m_Cercle,0));

            }
        }


	
	
	}
}