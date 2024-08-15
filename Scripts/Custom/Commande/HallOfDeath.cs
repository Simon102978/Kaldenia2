using System;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Commands;
using Server.Gumps;
using Server.Accounting;

namespace Server.Scripts.Commands
{
    public class HallOfDeath
	{
        public static void Initialize()
        {
            CommandSystem.Register("HallOfDeath", AccessLevel.GameMaster, new CommandEventHandler(HallOfDeath_OnCommand));
        }

        public static void HallOfDeath_OnCommand(CommandEventArgs e)
        {
			Mobile from = e.Mobile;

			if (from is CustomPlayerMobile cp)
					from.SendGump(new HallOfDeathGump(cp,new System.Collections.Generic.Dictionary<Mobile, int>(), 0));



        }


    }
}
