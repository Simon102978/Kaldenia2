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

	public class IdoleMainGump : BaseProjectMGump
	{

		private CustomPlayerMobile m_From;

		private IdoleChampionSpawn m_Stone;


		public IdoleMainGump(CustomPlayerMobile from,IdoleChampionSpawn stone ) : base("Idole", 425, 180, false)
		{
			m_From = from;
			m_Stone  = stone;


			int y = YBase + 20;
            int line = 0;
            int scale = 20; 

			AddPage(0);

			AddSection(70 , 73, 465, 220,"Options");

			AddButtonHtlml(84,y + line * scale,3, !stone.Active ? "Activez l'idole" : "Désactivez l'idole","#FFFFFF");
			line++;


			AddButtonHtlml(84,y + line * scale,7, "Créer un nouveau type","#FFFFFF");
			line++;

			AddButtonHtlml(84,y + line * scale,8, "Selectionner un type","#FFFFFF");
			line++;

		

			string infoName = "Aucun";

			if (m_Stone.Type != -1)
			{
				IdoleInfo info = m_Stone.GetIdoleInfo();

				if (info.InfoName == null)
				{
					infoName = "Sans Nom " + m_Stone.Type;
				}
				else
				{
					infoName = info.InfoName;
				}
			}
			

			AddButtonHtlml(84,y + line * scale,1, $"Type: {infoName}","#FFFFFF");
			line++;

			AddButtonHtlml(84,y + line * scale,9, "Actualiser l'Idole","#FFFFFF");
			line++;

			AddButtonHtlml(84,y + line * scale,6, "Propriétés:","#FFFFFF");
			line++;
		}






		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
		    CustomPlayerMobile from = (CustomPlayerMobile)sender.Mobile;

			if (info.ButtonID > 0)
			{
			
				if (info.ButtonID == 1)
				{
					if (m_Stone.GetIdoleInfo() == null || m_Stone.Type == -1)
					{
						m_From.SendGump(new IdoleMainGump(m_From, m_Stone));	
					}
					else
					{
						m_From.SendGump(new IdoleInfoGump(m_From,m_Stone,m_Stone.GetIdoleInfo(), 0));
					}


				}
				else if(info.ButtonID == 3)
				{
			
					m_Stone.Active = !m_Stone.Active;
					m_From.SendGump(new IdoleMainGump(m_From, m_Stone));	
				}
				else if(info.ButtonID == 6)
				{
					from.SendGump(new PropertiesGump(from, m_Stone));
				}
				else if(info.ButtonID == 7)
				{
					m_Stone.Type = IdoleInfo.CreateIdoleInfo();
					m_From.SendGump(new IdoleMainGump(m_From, m_Stone));	
				}
				else if (info.ButtonID == 8)
				{
					m_From.SendGump(new IdoleTypeSelectionGump(m_From,m_Stone,0));
				}
				else if (info.ButtonID == 9)
				{
					m_Stone.RefreshIdole();
					m_From.SendGump(new IdoleMainGump(m_From, m_Stone));	
				}

						
			
			

			}
		}

	}
}