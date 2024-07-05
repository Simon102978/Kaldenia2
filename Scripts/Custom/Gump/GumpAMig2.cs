using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using System.Reflection;
using Server.HuePickers;
using System.Collections.Generic;
using Server.Commands;

namespace Server.Gumps
{
  public class GumpAMig2 : BaseProjectMGump
	{
    public CustomPlayerMobile m_from;

    public int m_Id;


    public GumpAMig2(CustomPlayerMobile from, int Id)
        : base(Metier.GetMetier(Id).Name , 560, 622,false)
    {

      m_Id = Id;
      m_from = from;

      int x = XBase;
      int y = YBase;
      int line = 0;
      int scale = 25;

      Metier cla = Metier.GetMetier(Id);

     	AddSection(x - 10, y , 610, 605,"Détail" ,cla.MetierDescription());

      AddSection(x - 10, y + 610, 610, 50, cla.Name);

      AddButton(x, y + 610, 1000, 4506);

      AddButton(x + 540, y + 610, 1001, 4502);
      
	}
    public override void OnResponse(NetState sender, RelayInfo info)
    {
    	CustomPlayerMobile from = (CustomPlayerMobile)sender.Mobile;

      if (from.Deleted || !from.Alive)
        return;   

      if (info.ButtonID == 1000)
      {
        int newId = m_Id - 1;

        if (newId < 0)
        {
          newId = Metier.AllMetier.Count -1;
        }

        from.SendGump(new GumpAMig2(from,newId));


      }
      else if (info.ButtonID == 1001)
      {
        
       int newId = m_Id + 1;
        

        if (newId >=  Metier.AllMetier.Count )
        {
          newId = 0;
        }

        from.SendGump(new GumpAMig2(from,newId));


      }






    }

  
  }
}

namespace Server.Scripts.Commands
{
    public class GumpAMigCmd2
	{
        public static void Initialize()
        {
            CommandSystem.Register("GumpAMig2", AccessLevel.GameMaster, new CommandEventHandler(GumpAMig2_OnCommand));
        }

        public static void GumpAMig2_OnCommand(CommandEventArgs e)
        {
          Mobile from = e.Mobile;

          if (from is CustomPlayerMobile cp)
               from.SendGump(new GumpAMig2(cp,0));



        }


    }
}
