using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using System.Collections.Generic;
using Server.Accounting;
using System.Linq;


namespace Server.Gumps
{
    public class TableXpGump : BaseProjectMGump
	{
   
        private CustomPlayerMobile m_From;
		private int m_Page;

		public TableXpGump(CustomPlayerMobile from, int page = 0)
            : base("Table des niveaux", 560, 622, true)
        {

			m_From = from;
			m_Page = page;

			int x = XBase;
			int y = YBase;

			int line = 0;
			

			int i2 = 0;

			AddHtmlTexteColored(x + 10, y + 20 + line * 20, 300, "Niveau", "#ffffff");
			AddHtmlTexteColored(x + 200, y + 20 + line * 20, 300, "FE requises", "#ffffff");
			AddHtmlTexteColored(x + 350, y + 20 + line * 20, 300, "Skill Cap", "#ffffff");
			AddHtmlTexteColored(x + 450, y + 20 + line * 20, 300, "Total Skill Cap", "#ffffff");


			foreach (KeyValuePair<int, XPLevel> item in XPLevel.XpTable)
			{
				if (i2 >= page * 28 && line < 28)
				{
					string couleur = "#ffffff";

					if (item.Key == from.Niveau)
					{
						couleur = "#ffcc00";
						
					}


					AddHtmlTexteColored(x + 10 , y + 40 + line * 20, 300, item.Key.ToString(), couleur);
					AddHtmlTexteColored(x + 200, y + 40 + line * 20, 300, item.Value.FeRequis.ToString(), couleur);
					AddHtmlTexteColored(x + 350, y + 40 + line * 20, 300, item.Value.MaxSkill.ToString(), couleur);
					AddHtmlTexteColored(x + 450, y + 40 + line * 20, 300, (item.Key * 10 + 500).ToString(), couleur);
					line++;
				}
				i2++;
			}

			if (page != 0)
			{
				AddButton(x + 5, y + 610, 1, 4506);
			}
			if (XPLevel.XpTable.Count > (page + 1) * 28)
			{
				AddButton(x + 535, y + 610, 2, 4502);
			}



		}

		public override void OnResponse(NetState sender, RelayInfo info)
        {



			     switch (info.ButtonID)
				 {
					 case 1:
						 {
							sender.Mobile.SendGump(new TableXpGump(m_From, m_Page - 1));
							 break;
						 }
					 case 2:
						 {
							sender.Mobile.SendGump(new TableXpGump(m_From, m_Page + 1));
							break;
						 }

				 }
		}
	}
}
