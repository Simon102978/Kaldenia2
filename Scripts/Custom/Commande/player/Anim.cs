using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Scripts.Commands
{
    public class Anim
    {
        public static void Initialize()
        {
			CommandSystem.Register("Anim", AccessLevel.Player, new CommandEventHandler(Anim_OnCommand));
		}

        public static void Anim_OnCommand(CommandEventArgs e)
        {
            if (e.Length == 1)
            {
                if (!e.Mobile.Mounted)
                    e.Mobile.Animate(e.GetInt32(0), 5, 1, true, false, 0);
            }
            else
            {
                e.Mobile.SendMessage("Format: Anim <action>");
            }
        }
    }
}