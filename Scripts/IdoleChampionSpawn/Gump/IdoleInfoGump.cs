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
using Server.Engines.Idole;



namespace Server.Gumps
{

	public class IdoleInfoGump : BaseProjectMGump
	{

		private CustomPlayerMobile m_From;

		private IdoleChampionSpawn m_Stone;

		private IdoleInfo m_Info;

		private int m_Page;

		public IdoleInfoGump(CustomPlayerMobile from,IdoleChampionSpawn stone, IdoleInfo info, int page = 0) : base(info.InfoName, 425, 515, false)
		{
			m_From = from;
			m_Stone  = stone;
			m_Page = page;	
			m_Info = info;	
			int y = YBase + 40;
            int line = 0;
            int scale = 25; 

			AddPage(0);

			AddSection(70 , 73, 465, 220,"Information");

			AddHtmlTexteColored( 84, y + line * scale, 75, "Nom: ", "#ffffff");
			AddTextEntryBg( 147, y + line* scale, 325, 25, 0, 1, m_Info.InfoName);
			line++;

			AddHtmlTexteColored( 84, y + line * scale, 75, "Idole ItemId: ", "#ffffff");
			AddTextEntryBg( 147, y + line* scale, 325, 25, 0, 2, m_Info.AltarItemId.ToString());
			line++;

			AddHtmlTexteColored( 84, y + line * scale, 75, "Idole Name: ", "#ffffff");
			AddTextEntryBg( 147, y + line* scale, 325, 25, 0, 3, m_Info.IdoleName.ToString());
			line++;

			AddButtonHtlml(84,y + line* scale,3, "Ajouter un niveau","#FFFFFF");
			line++;

			AddButtonHtlml(84,240,5, "Appliquer","#FFFFFF");
			
			AddSection(70 , 295, 465, 330,"Niveau","");
			AddButton(505, 339, 1, 251, 250);
			AddButton(505, 588, 2, 253, 252);

			int n = 0;

			int maxI = (page +1) * 9;

			if (maxI > m_Info.MaxLevel)
			{
				maxI = m_Info.MaxLevel;
			}


			for (int i = 0 + page * 9; i < maxI; i++)
			{
				string name = "";

				if (m_Info.Levels[i] != null && m_Info.Levels[i].Name != null)
				{
					name= m_Info.Levels[i].Name;
				}

				 AddBackground(84, 339 + n* 30, 420, 30, 9350);
				 AddButtonHtlml(90,345 + n* 30,2000 + i, $"{i}:" + name);
				 AddButton(400,345 + n* 30,1000 + i,2463,2464);
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

					m_From.SendGump(new IdoleInfoGump(m_From, m_Stone,m_Info, pagi));		
				}
				else if (info.ButtonID == 2)
				{
					int pagi = m_Page + 1;

					if (pagi >= m_Info.MaxLevel / 9 + 1)
					{
						pagi--;
					}

					m_Page = pagi;

					m_From.SendGump(new IdoleInfoGump(m_From, m_Stone,m_Info, pagi));	
				}
				else if(info.ButtonID == 3)
				{
			
					m_Info.AddLevel();	
					m_From.SendGump(new IdoleInfoGump(m_From, m_Stone,m_Info, m_Page));	
				}
				else if(info.ButtonID == 4)
				{
				
 				/*	from.SendMessage("Où désirez-vous que le murs apparaissent ? ");
                	from.Target = new SpawnLocationTarget(m_Stone);*/

				}
				else if(info.ButtonID == 5)
				{

					if (info.GetTextEntry(1) != null)
					{
						 m_Info.InfoName = info.GetTextEntry(1).Text; 
					}

					if (info.GetTextEntry(2) != null)
					{
						string IdoS = info.GetTextEntry(2).Text;

						if (int.TryParse(IdoS, out int idoleID))
						{
							 m_Info.AltarItemId = idoleID;
						}
					}

					if (info.GetTextEntry(3) != null)
					{
						 m_Info.IdoleName = info.GetTextEntry(3).Text; 
					}

					m_From.SendGump(new IdoleInfoGump(m_From, m_Stone,m_Info, m_Page));	
				}
				else if(info.ButtonID == 6)
				{
					from.SendGump(new PropertiesGump(from, m_Stone));
				}
				else if(info.ButtonID == 7)
				{
					m_Stone.Type = IdoleInfo.CreateIdoleInfo();
					m_From.SendGump(new IdoleInfoGump(m_From, m_Stone,m_Info, m_Page));	
				}

				else if(info.ButtonID >= 1000 && info.ButtonID < 2000 )
				{
					m_Info.DeleteLvl(info.ButtonID - 1000);
					m_From.SendGump(new IdoleInfoGump(m_From, m_Stone,m_Info, m_Page));	
				}
				else if (info.ButtonID >= 2000)
				{
					
					m_From.SendGump(new IdoleNiveauGump(m_From, m_Stone,m_Info,m_Info.Levels[info.ButtonID - 2000]));

					
				}
						
			
			

			}
		}	
	}
}