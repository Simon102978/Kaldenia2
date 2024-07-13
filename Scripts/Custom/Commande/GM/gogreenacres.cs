using System;
using System.Collections;
using System.IO;
using System.Text;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Gumps;
using Server.Engines.PartySystem;
using Server.Commands;

namespace Server.Scripts.Commands
{
    public class GoGreenacres
	{
		public static void Initialize()
		{
            CommandSystem.Register("GoGreenacres", AccessLevel.GameMaster, new CommandEventHandler(GoGreenacres_OnCommand));
			CommandSystem.Register("goga", AccessLevel.GameMaster, new CommandEventHandler(GoGreenacres_OnCommand));
		}

        [Usage("GoGreenacres")]
		[Description("teleporte au green acres")]
        public static void GoGreenacres_OnCommand(CommandEventArgs e)
		{
			
			Mobile from = e.Mobile;
			from.MoveToWorld(new Point3D(5671,8,0), Map.Felucca);
		}
		
	}


}