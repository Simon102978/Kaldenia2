using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using System.Reflection;
using Server.HuePickers;
using System.Collections.Generic;

namespace Server.Gumps
{
    public class ParfumGump : BaseProjectMGump
    {
        private CustomPlayerMobile m_from;
        private int m_page;

        public ParfumGump(CustomPlayerMobile from, string hue, string Html)
            : base("Parfums", 200, 100)
        {

            m_from = from;
  
            int x = XBase;
            int y = YBase;
            int line = 0;
            int scale = 25;
            int space = 80;


            AddHtmlTexte(x, y + line * scale, 50, "Hue");
            AddTextEntryBg(x + 50, y + line * scale, 100, 15, 0,0, hue);
            line++;
            AddHtmlTexte(x, y + line * scale, 50, "Html");
            AddTextEntryBg(x + 50, y + line * scale, 100, 15, 0, 1, Html);

            line++;
            AddButtonHtlml(x + 50, y + line * scale, 1, "Tester");


        }

     public override void OnResponse(NetState sender, RelayInfo info)
     {
      CustomPlayerMobile from = (CustomPlayerMobile)sender.Mobile;

      if (from.Deleted || !from.Alive)
        return;

            if (info.ButtonID == 1)
            {
                string hueEnter = info.GetTextEntry(0).Text; 
                string HtmlEnter = info.GetTextEntry(1).Text;


                int hueEnt = -1;

                int.TryParse(hueEnter, out hueEnt);

                if (hueEnt == -1)
                {
                    from.SendMessage("Le hue doit etre un nombre.");
                }
                else
                {
                    // m_from.NameHue = hueEnt;


                    Perfume NewPerf = new Perfume("test", hueEnt, HtmlEnter, TimeSpan.FromDays(7));

                    from.AddPerfume(NewPerf);
                    


                   // m_from.OnAosSingleClick(from);
                }


                from.SendGump(new ParfumGump(m_from, hueEnter,HtmlEnter));
            }
           
        }
    }
}
