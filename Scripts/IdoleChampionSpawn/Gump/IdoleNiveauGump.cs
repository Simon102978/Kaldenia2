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
using System.Collections;
using Microsoft.SqlServer.Server;



namespace Server.Gumps
{

	public class IdoleNiveauGump : BaseProjectMGump
	{

		private CustomPlayerMobile m_From;

		private IdoleChampionSpawn m_Stone;

		private IdoleInfo m_IdoleInfo;

		private IdoleLevelInfo m_Niveau;


		private int m_Page;


		public IdoleNiveauGump(CustomPlayerMobile from,IdoleChampionSpawn stone, IdoleInfo info, IdoleLevelInfo level, int page = 0 ) : base("Idole", 425, 515, false)
		{
			m_From = from;
			m_Stone  = stone;
			m_IdoleInfo = info;
			m_Niveau = level;
			m_Page = page;


			int y = YBase + 40;
            int line = 0;
            int scale = 25; 

			AddPage(0);

			AddSection(70 , 73, 465, 170,"Options");

			AddHtmlTexteColored( 84, y + line * scale, 75, "Nom: ", "#ffffff");
			AddTextEntryBg( 147, y + line* scale, 325, 25, 0, 1, level.Name);
			line++;

			AddHtmlTexteColored( 84, y + line * scale, 75, "Hue: ", "#ffffff");
			AddTextEntryBg( 147, y + line* scale, 325, 25, 0, 2, level.AltarHue.ToString());
			line++;

			AddHtmlTexteColored( 84, y + line * scale, 75, "Parole: ", "#ffffff");
			AddTextEntryBg( 147, y + line* scale, 325, 25, 0, 3, level.Parole);
			line++;

			AddButtonHtlml(84,y + line* scale,5, level.Boss ? "Boss": "Normal","#FFFFFF");
			line++;


			AddButtonHtlml(84,y + line* scale - 5,4, "Ajouter un monstre","#FFFFFF");
			line++;

			AddSection(70 , 245, 465, 330,"Niveau","");
			AddButton(505, 289, 1, 251, 250);
			AddButton(505, 538, 2, 253, 252);

			int n = 0;

			int maxI = (page +1) * 9;

			if (maxI > m_Niveau.Types.Count)
			{
				maxI = m_Niveau.Types.Count;
			}

			for (int i = 0 + page * 9; i < maxI; i++)
			{
				string name = "Aucun";
				string atuer = "0";

				if (m_Niveau.Types[i] != null )
				{
					if ( m_Niveau.Types[i].SpawnType != null)
					{
						name = m_Niveau.Types[i].SpawnType.Name;
					}

					atuer = m_Niveau.Types[i].Required.ToString();

				}

				 AddBackground(84, 289 + n* 30, 420, 30, 9350);
				 AddButtonHtlml(90,295 + n* 30,2000 + i,name );

				 AddHtmlTexteColored( 300, 295 + n* 30, 75, "À tuer: ", "#000000");

				 AddTextEntryBg( 355, 292 + n* 30, 40, 25, 0, 1000 + i, atuer);

				 AddButton(400,295 + n* 30,1000 + i,2463,2464);
				 n++;

			}

 			AddBackground(70 , 577, 465, 50, 9270);
			AddButtonHtlml(260,590,3, "Appliquer","#FFFFFF");
		
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

					m_From.SendGump(new IdoleNiveauGump(m_From, m_Stone,m_IdoleInfo,m_Niveau, pagi));		

				}
				else if (info.ButtonID == 2)
				{
					int pagi = m_Page + 1;

					if (pagi >= m_Niveau.Types.Count / 9 + 1)
					{
						pagi--;
					}

					m_Page = pagi;

					m_From.SendGump(new IdoleNiveauGump(m_From, m_Stone,m_IdoleInfo,m_Niveau, pagi));	
				}
				else if(info.ButtonID == 3)
				{
					if (info.GetTextEntry(1) != null)
					{
						 m_Niveau.Name = info.GetTextEntry(1).Text; 
					}

					string hueId = info.GetTextEntry(2).Text;

					int HueIdInt = 0;

 					if (int.TryParse(hueId, out HueIdInt))
					{
						m_Niveau.AltarHue = HueIdInt;
					} 


					if (info.GetTextEntry(3) != null)
					{
						 m_Niveau.Parole = info.GetTextEntry(3).Text; 
					}


					int maxI = (m_Page +1) * 9;

					if (maxI > m_Niveau.Types.Count)
					{
						maxI = m_Niveau.Types.Count;
					}


					for (int i = 0 + m_Page * 9; i < maxI; i++)
					{

						string ToKill = info.GetTextEntry(1000 +i).Text;

						int ToKillInt = 0;

						if (int.TryParse(ToKill, out ToKillInt))
						{
							m_Niveau.Types[i].Required = ToKillInt;
						} 
					}

					m_From.SendGump(new IdoleInfoGump(m_From, m_Stone,m_IdoleInfo, 0));						
				}
				else if(info.ButtonID == 4)
				{
					m_Niveau.AddLevel();
			
					m_From.SendGump(new IdoleNiveauGump(m_From, m_Stone,m_IdoleInfo,m_Niveau, m_Page));	
				}
				else if(info.ButtonID == 5)
				{
					m_Niveau.Boss = !m_Niveau.Boss;
			
					m_From.SendGump(new IdoleNiveauGump(m_From, m_Stone,m_IdoleInfo,m_Niveau, m_Page));	
				}
				else if(info.ButtonID >= 1000 && info.ButtonID < 2000 )
				{
					m_Niveau.DeleteLevel(info.ButtonID - 1000);
					m_From.SendGump(new IdoleNiveauGump(m_From, m_Stone,m_IdoleInfo,m_Niveau, m_Page));	
				}
				else if (info.ButtonID >= 2000)
				{						
					ArrayList types = IdoleAddGump.Match("Ratman");
					m_From.SendGump(new IdoleAddGump(m_From,"" ,0,types,false,info.ButtonID - 2000,m_Stone,m_IdoleInfo,m_Niveau));									
				}
			}
		}

	}
}