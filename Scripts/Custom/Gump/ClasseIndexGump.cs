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
    public class ClasseIndexGump : BaseProjectMGump
	{
        private CustomPlayerMobile m_From;

        public ClasseIndexGump(CustomPlayerMobile from)
            : base("Classes Index", 560, 622, true)
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



			Classe aucune = Classe.GetClasse(0);

			foreach (int item in aucune.Evolution)
			{
				Classe c = Classe.GetClasse(item);

				if (!c.Hidden)
				{
					AddButtonHtlml(x + 10, y + yLine * 20 + 40,1000 + c.ClasseID,c.Name,"#FFFFFF");
					yLine++;
				}
			}

		}

		public override void OnResponse(NetState sender, RelayInfo info)
        {

			if (info.ButtonID >= 1000)
			{
				int NewClasseId = info.ButtonID - 1000;

				List<int> newList = new List<int>();
				newList.Add(NewClasseId);

				m_From.SendGump(new ClasseGump(m_From, NewClasseId,newList,0));
			}
		}
    }
}
