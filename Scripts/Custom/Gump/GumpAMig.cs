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
  public class GumpAMig : BaseProjectMGump
	{
    public CustomPlayerMobile m_from;

    public int m_Id;


    public GumpAMig(CustomPlayerMobile from, int Id)
        : base(Classe.GetClasse(Id).Name , 560, 622,false)
    {

      m_Id = Id;
      m_from = from;

      int x = XBase;
      int y = YBase;
      int line = 0;
      int scale = 25;

      Classe cla = Classe.GetClasse(Id);

     	AddSection(x - 10, y , 610, 605,"Détail" ,cla.ClasseDescription());

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
          newId = Classe.AllClasse.Count -1;
        }
        from.SendGump(new GumpAMig(from,newId));


      }
      else if (info.ButtonID == 1001)
      {
        
       int newId = m_Id + 1;

        if (newId >=  Classe.AllClasse.Count )
        {
          newId = 0;
        }

           from.SendGump(new GumpAMig(from,newId));
      }






    }

  
  }
}

namespace Server.Scripts.Commands
{
    public class GumpAMigCmd
	{
        public static void Initialize()
        {
            CommandSystem.Register("GumpAMig", AccessLevel.GameMaster, new CommandEventHandler(GumpAMig_OnCommand));
        }

        public static void GumpAMig_OnCommand(CommandEventArgs e)
        {
          Mobile from = e.Mobile;

          if (from is CustomPlayerMobile cp)
               from.SendGump(new GumpAMig(cp,0));



        }


    }
}
