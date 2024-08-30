using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Accounting;
using Server.Misc;

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
                  KillAllGate();
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

    public void CreateGate(CustomPlayerMobile from)
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

      Timer.DelayCall(TimeSpan.FromSeconds(1), () =>
      {
          Point3D loc = GetLocation();

          Item GateThere = GateAt(Map, loc);

          if (GateThere != null)
          {
            GateThere.Delete();
          }

          from.SendLocalizedMessage(501024); // You open a magical gate to another location

          Effects.PlaySound(from.Location, from.Map, 0x20E);

          InternalItem firstGate = new InternalItem(loc, Map);
          firstGate.MoveToWorld(from.Location, from.Map);

          Effects.PlaySound(loc, Map, 0x20E);

          InternalItem secondGate = new InternalItem(from.Location, from.Map);
          secondGate.MoveToWorld(loc, Map);

          firstGate.LinkedGate = secondGate;
          secondGate.LinkedGate = firstGate;
      });

    }


    public void KillAllGate()
    {

      foreach (Point3D item in Landing)
      {
        Item gate = GateAt(Map, item);

        if(gate != null)
          gate.Delete();
      }




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

			if (!from.InRange(GetWorldLocation(), 3)) 
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
 
 
        [DispellableField]
        private class InternalItem : Moongate
        {
            [CommandProperty(AccessLevel.GameMaster)]
            public Moongate LinkedGate { get; set; }

           

            public InternalItem(Point3D target, Map map)
                : base(target, map)
            {
                Map = map;

                if (ShowFeluccaWarning && map == Map.Felucca)
                    ItemID = 0xDDA;

                Dispellable = true;

                InternalTimer t = new InternalTimer(this);
                t.Start();
            }

            public override void UseGate(Mobile m)
            {
                if (LinkedGate == null || !(LinkedGate is InternalItem) || !LinkedGate.Deleted)
                {
                    base.UseGate(m);
                }
                else
                    m.SendMessage("The other gate no longer exists.");
            }


            public override void OnDelete()
            {

              base.OnDelete();

              if (LinkedGate != null)
              {
                ((InternalItem)LinkedGate).LinkedGate = null;

                LinkedGate.Delete();



              }
            }



            public InternalItem(Serial serial)
                : base(serial)
            {
            }

            public override bool ShowFeluccaWarning => true;
            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                Delete();
            }

            private class InternalTimer : Timer
            {
                private readonly Item m_Item;

                public InternalTimer(Item item)
                    : base(TimeSpan.FromSeconds(30.0))
                {
                    Priority = TimerPriority.OneSecond;
                    m_Item = item;
                }

                protected override void OnTick()
                {
                    m_Item.Delete();
                }
            }
        }
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
  }
}

