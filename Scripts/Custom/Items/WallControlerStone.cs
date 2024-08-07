using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Accounting;
using Server.Misc;
using System.Drawing;

namespace Server.Items
{
  public class WallControlerStone : Item
  {
    private static readonly string m_TimerID = "CloseWallControler";

    private bool m_Active;

    private string m_WallName;
    private int m_WallHue;

    private int m_WallItemId;

    public List<Point3D> Wall = new List<Point3D>();

    public bool m_MobActif = false;

    [CommandProperty(AccessLevel.GameMaster)]
    	public bool MobActif
        {
            get
            {
                return m_MobActif;
            }
            set
            {
                m_MobActif = value;

                Active = m_MobActif;
                

            }
        }


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
                  GenerateWall();
                }
                else
                {
                  DestroyWall();
                }
            }
        }


   private void InternalClose()
   {
         Active = MobActif;
   }

    public void ActiveSwitch()
    {
      if (MobActif)
      {
        if (Active)
        {
         
          TimerRegistry.Register(m_TimerID, this, TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(10), true, TimerPriority.OneSecond, door => door.InternalClose());
        }
        else
        {
           TimerRegistry.RemoveFromRegistry(m_TimerID, this);
        }   
          Active = !Active;         
      }
    }

		private void DestroyWall()
		{
			foreach (Point3D pts in Wall)
      {

        DeleteSpecificWall(pts);
      }
		}

    private void DeleteSpecificWall(Point3D pts)
    {

           IPooledEnumerable eable = Map.GetItemsInRange(pts, 0);

            foreach (Item item in eable)
            {
                if (item is InternalItem)
                {
                  item.Delete();
                }
            }
            eable.Free();
    }

		private void GenerateWall()
		{
			foreach (Point3D item in Wall)
      {
        AddSpecificWall(item);
      }
		}

    private void AddSpecificWall(Point3D pts)
    {
          InternalItem firstGate = new InternalItem(pts, Map, this);
          firstGate.MoveToWorld(pts, Map);

    }

		[CommandProperty(AccessLevel.GameMaster)]
    	public string WallName
        {
            get
            {
                return m_WallName;
            }
            set
            {
                m_WallName = value;              
            }
        }

    [CommandProperty(AccessLevel.GameMaster)]
    	public int WallHue
        {
            get
            {
                return m_WallHue;
            }
            set
            {
                m_WallHue = value;              
            }
        }
    [CommandProperty(AccessLevel.GameMaster)]
    	public int WallItemId
        {
            get
            {
                return m_WallItemId;
            }
            set
            {
                m_WallItemId = value;              
            }
        }


    [Constructable]
    public WallControlerStone()
        : base(3796)
    {
      Movable = false;
      Visible = false;

      Name = "Controleur de Mur";
      Hue = 0xAB2;
    }

    public override void OnDoubleClick(Mobile from)
    {

        if (from.IsStaff())
        {
          from.SendGump(new WallControlerGump((CustomPlayerMobile)from,this,0));
          return;
        }  

     
         
    }


    public bool AddLocation(Point3D location)
    {
      if (Wall.Contains(location))
      {
        return false;
      }
      else
      {
        Wall.Add(location);

        if (m_Active)
        {
           AddSpecificWall(location);
        }

        return true;
      }
    }

    public void DeleteLocation(int location)
    {
      int n = 0;

      List<Point3D> newLanding = new List<Point3D>();

      foreach (Point3D item in Wall)
      {
        if (location != n)
        {
          newLanding.Add(item);
        }
        else
        {
           DeleteSpecificWall(item);
        }
        n++;
      }
      Wall = newLanding;
      
    }







    public WallControlerStone(Serial serial)
        : base(serial)
    {
    }

    public override void Serialize(GenericWriter writer)
    {
      base.Serialize(writer);

      writer.Write((int)1); // version
      writer.Write(m_MobActif);
      writer.Write(m_WallName);
      writer.Write(m_Active);
      writer.Write(m_WallItemId);
      writer.Write(Wall.Count);

			foreach (Point3D item in Wall)
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
          m_MobActif = reader.ReadBool();
          goto case 0;
        }
        case 0:
        {
          m_WallName = reader.ReadString();
          m_Active = reader.ReadBool();

          m_WallItemId = reader.ReadInt();


          Wall = new List<Point3D>();

						int count = reader.ReadInt();

						for (int i = 0; i < count; i++)
						{
							Wall.Add(reader.ReadPoint3D());
						}

          break;
        }
      }
    }
 
 
    private class InternalItem : Item
		{
      public WallControlerStone m_Controler;

		

			

			public InternalItem(Point3D loc, Map map,  WallControlerStone controler) : base(0x08E2)
			{
				Visible = true;
				Movable = false;
				ItemID = controler.WallItemId;

				Name = controler.WallName;
				Hue = controler.WallHue;
				MoveToWorld(loc, map);



				if (Deleted)
					return;


			}

			public InternalItem(Serial serial) : base(serial)
			{
			}
			public override bool OnMoveOver(Mobile m)
			{

        if (m.Alive && m.IsPlayer())
        {
          m.SendMessage("Vous ne passerez pas !");
				  return false;
        }

        return true;


			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);

				writer.Write((int)0); // version


			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);

				int version = reader.ReadInt();

				switch (version)
				{
				
					case 0:
						{
						
							break;
						}
				}
			}

			

    }
  }
}

