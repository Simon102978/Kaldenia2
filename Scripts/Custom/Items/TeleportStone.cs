using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Accounting;

namespace Server.Items
{
  public class TeleportStone : Item
  {

    public bool m_Active;

    public List<Point3D> m_Landing = new List<Point3D>();


		[CommandProperty(AccessLevel.GameMaster)]
    	public bool Active
        {
            get
            {
                return m_Active;
            }
            set
            {
                m_Active = value;
                
                if (m_Active)
                {
                  Hue = 0xAB2;
                }
                else
                {
                  Hue = 1157;
                }
            }
        }


      [CommandProperty(AccessLevel.GameMaster)]
    	public  List<Point3D> Landing
        {
            get
            {
                return m_Landing;
            }
            set
            {
                m_Landing = value;
            }
        }

    [Constructable]
    public TeleportStone()
        : base(0x2fdc)
    {
      Movable = false;

      Name = "Pierre de téléporation";
      Hue = 0xAB2;
    }

    public bool AddLocation(Point3D location)
    {
      if (Landing.Contains(location))
      {
        return false;
      }
      else
      {
        Landing.Add(location);
        return true;
      }
    }

    public void DeleteLocation(int location)
    {
      int n = 0;

      List<Point3D> newLanding = new List<Point3D>();

      foreach (Point3D item in Landing)
      {
        if (location != n)
        {
          newLanding.Add(item);
        }
        n++;
      }
      Landing = newLanding;
      
    }

    public void LinkToStone(CustomPlayerMobile from)
    {
      from.TeleportStone = this;
      from.SendMessage("Vous vous êtes lié à la pierre de téléportation.");
    }

    public void Teleport(CustomPlayerMobile from)
    {
      
      if (!m_Active)
      {
        from.SendMessage("La pierre ne répond pas.");
        return;
      }
      else if(Landing.Count == 0)
      {
        from.SendMessage("Vous ne pouvez pas vous téléporter à cette pierre.");
        return;
      } 

      Point3D loc = GetLocation();

      BaseCreature.TeleportPets(from, loc, Map, true);
      from.PlaySound(0x1FC);
      from.MoveToWorld(loc, Map);
      from.PlaySound(0x1FC);
      from.MoveToWorld(loc,Map);


    }

    public Point3D GetLocation()
    {

      for (int i = 0; i < 25; i++)
      {
        Point3D p = Landing[Utility.Random(m_Landing.Count)];

        if (Map.CanSpawnMobile(p) && !GateExistsAt(Map,p))
        {
          return p;
        }       
      }
        return Landing[Utility.Random(m_Landing.Count)];
    }

     private bool GateExistsAt(Map map, Point3D loc)
     {
            bool _gateFound = false;

            IPooledEnumerable eable = map.GetItemsInRange(loc, 0);
            foreach (Item item in eable)
            {
                if (item is Moongate || item is PublicMoongate)
                {
                    _gateFound = true;
                    break;
                }
            }
            eable.Free();

            return _gateFound;
    }

      private Item GateAt(Map map, Point3D loc)
      {

            IPooledEnumerable eable = map.GetItemsInRange(loc, 0);

            foreach (Item item in eable)
            {
                if (item is Moongate || item is PublicMoongate)
                {
                  return item;
                }
            }
            eable.Free();

            return null;
      }

    public override void OnDoubleClick(Mobile from)
    {

        if (from.IsStaff())
        {
          from.SendGump(new TeleportStoneGump((CustomPlayerMobile)from,this,0));
          return;
        }  

        if (!m_Active)
        {
           from.SendMessage("Vous ne pouvez pas vous liez à une pierre inactive.");
           return;
        }

        if (from.InRange(Location, 3))
        {
            from.SendMessage("Vous devez vous approcher de la pierre.");
            return;
        }

        LinkToStone((CustomPlayerMobile)from);
         
    }

    public TeleportStone(Serial serial)
        : base(serial)
    {
    }

    public override void Serialize(GenericWriter writer)
    {
      base.Serialize(writer);

      writer.Write((int)1); // version

      writer.Write(m_Active);

      writer.Write(m_Landing.Count);

      foreach (Point3D item in m_Landing)
      {
        writer.Write(item);
      }
    }

    public override void Deserialize(GenericReader reader)
    {
      base.Deserialize(reader);

      int version = reader.ReadInt();

      switch (version)
      {
        case 1:
        {
          Active = reader.ReadBool();
          int n = reader.ReadInt();

          for (int i = 0; i < n; i++)
          {
            m_Landing.Add(reader.ReadPoint3D());
          }
          break;
        }
      }
    }
  }
}

