using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Scripts.Commands
{
  class Parfum
    {
    public static void Initialize()
    {
      CommandSystem.Register("Parfum", AccessLevel.GameMaster, new CommandEventHandler(Parfum_OnCommand));
    }

    [Usage("Parfum")]
    [Description("Ouvre le menu de création de Parfum.")]
    public static void Parfum_OnCommand(CommandEventArgs e)
    {
      if (e.Mobile is CustomPlayerMobile from)
      {


          
          from.SendGump(new ParfumGump(from, "6", "#6495ED"));
        
      }
    }
  }
}
