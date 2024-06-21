using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Scripts.Commands
{
    public class TableXp
    {
        public static void Initialize()
        {
            CommandSystem.Register("TableNiveau", AccessLevel.Player, new CommandEventHandler(TableXP_OnCommand));		
		}

        [Usage("TableXP")]
        [Description("Permet d'ouvrir la table des niveaux")]
        public static void TableXP_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from is CustomPlayerMobile)
            {
                from.CloseGump(typeof(TableXpGump));


                from.SendGump(new TableXpGump((CustomPlayerMobile)from));
            }
		}		
    }
}


