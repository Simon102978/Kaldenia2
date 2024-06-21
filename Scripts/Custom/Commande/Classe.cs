using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Scripts.Commands
{
    public class Classe
    {
        public static void Initialize()
        {
            CommandSystem.Register("Classe", AccessLevel.Player, new CommandEventHandler(Classe_OnCommand));		
		}

        [Usage("Fiche")]
        [Description("Permet d'ouvrir l'arbre des classes")]
        public static void Classe_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from is CustomPlayerMobile cp)
            {

                List<int> list = new List<int>();
                list.Add(cp.Classe.ClasseID);
             
                from.SendGump(new ClasseGump(cp, cp.Classe.ClasseID, list, 0));
            }
		}		
    }
}


