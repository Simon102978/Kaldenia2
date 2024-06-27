using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using System.Collections.Generic;
using Server.Custom;
using System.Security.Cryptography;

namespace Server.Gumps
{
    public class MetierIndexGump : BaseProjectMGump
	{
        private CustomPlayerMobile m_From;

        public MetierIndexGump(CustomPlayerMobile from)
            : base("Metiers Index", 560, 622, true)
        {
            m_From = from;
			int x = XBase;
			int y = YBase;
			m_From.InvalidateProperties();

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

			int yLine = 0;		



			Metier aucune = Metier.GetMetier(0);

			foreach (int item in aucune.Evolution)
			{
				Metier c = Metier.GetMetier(item);

				if (!c.Hidden)
				{
					AddButtonHtlml(x + 10, y + yLine * 20 + 40,1000 + c.MetierID,c.Name,"#FFFFFF");
					yLine++;
				}
			}

		}

		public override void OnResponse(NetState sender, RelayInfo info)
        {

			if (info.ButtonID >= 1000)
			{
				int NewMetierId = info.ButtonID - 1000;

				List<int> newList = new List<int>();
				newList.Add(NewMetierId);

				m_From.SendGump(new MetierGump(m_From, NewMetierId,newList,0));
			}
		}
    }
}
