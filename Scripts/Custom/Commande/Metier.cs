using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Scripts.Commands
{
    public class Metier
    {
        public static void Initialize()
        {
            CommandSystem.Register("Metier", AccessLevel.Player, new CommandEventHandler(Metier_OnCommand));		
		}

        [Usage("Metier")]
        [Description("Permet d'ouvrir l'arbre des metiers")]
        public static void Metier_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from is CustomPlayerMobile cp)
            {

                List<int> list = new List<int>();
                list.Add(cp.Metier.MetierID);
             
                from.SendGump(new MetierGump(cp, cp.Metier.MetierID, list, 0));
            }
		}		
    }
}


