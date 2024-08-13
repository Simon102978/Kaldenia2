using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using System.Collections.Generic;
using System.Reflection;
using Server.Engines.Idole;


namespace Server.Gumps
{
    public class IdoleTypeSelectionGump : BaseProjectMGump
	{
   
        private CustomPlayerMobile m_From;
		private int m_Page;
		private IdoleChampionSpawn m_Stone;

		public IdoleTypeSelectionGump(CustomPlayerMobile from, IdoleChampionSpawn stone,    int page = 0)
            : base("Types", 200, 200, true)
        {

			m_From = from;
			m_Page = page;
			m_Stone = stone;

			int x = XBase;
			int y = YBase;

			int line = 0;

			
				int n = page * 9 ;
			

				while (n < (int) IdoleInfo.Table.Count)
				{				

						IdoleInfo info = IdoleInfo.Table[n];

						AddButtonHtlml(x + 10, y + 20 + line * 20, info.InfoName, 200, 40, n + 100, "#ffffff");

						n++;
						line++;

						if (line ==  9)
						{
								break;
						}
				}

				if (page != 0)
				{
					AddButton(x + 5, y + 188, 1, 4506);
				}
				if (IdoleInfo.Table.Count > (page + 1) * 9)
				{
					AddButton(x + 175, y + 188, 2, 4502);
				}
				

		}

		public override void OnResponse(NetState sender, RelayInfo info)
        {
			     switch (info.ButtonID)
				 {
					 case 0:
						 {
							m_From.SendGump(new IdoleMainGump(m_From, m_Stone));	
						 	break;
						 }
					 case 1:
						 {
							m_From.SendGump(new IdoleTypeSelectionGump(m_From,m_Stone, m_Page - 1));
							 break;
						 }
					 case 2:
						 {
							m_From.SendGump(new IdoleTypeSelectionGump(m_From, m_Stone, m_Page + 1));
							break;
						 }

				 }

			if (info.ButtonID >= 100)
			{

				m_Stone.Type = info.ButtonID - 100;
				m_From.SendGump(new IdoleMainGump(m_From, m_Stone));	
			}
		}
	}
}
