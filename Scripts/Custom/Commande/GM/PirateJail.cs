using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Scripts.Commands
{
    public class PirateJail
    {
        public static void Initialize()
        {
            CommandSystem.Register("PirateJail", AccessLevel.GameMaster, new CommandEventHandler(PirateJail_OnCommand));
		
		}

        [Usage("PirateJail")]
        [Description("Permet d'ouvrir le menu de la prison pirate.")]
        public static void PirateJail_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from is CustomPlayerMobile cp)
            {
                from.SendGump(new PirateJailGump(cp, 0));
            }
		}


	
    }
}


