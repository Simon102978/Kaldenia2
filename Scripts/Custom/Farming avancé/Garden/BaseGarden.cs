using Server.Custom;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Server.Items
{
    public abstract class BaseGarden : BaseAddon
    {
       
        private GardenRegion m_Region;
        private Mobile m_Owner;
        private static readonly Dictionary<Mobile, List<BaseGarden>> m_Table = new Dictionary<Mobile, List<BaseGarden>>();

        private int m_Price;

        public virtual bool IsActive => true;

        private bool m_Public = false;

        public abstract BaseGardenDeed Deed();



		public List<Mobile> GetMobiles()
        {
            if (Map == null || Map == Map.Internal)
                return new List<Mobile>();

            List<Mobile> list = new List<Mobile>();

            foreach (Mobile mobile in Region.GetMobiles())
                    list.Add(mobile);

            return list;
        }
  
        public static List<BaseGarden> GetGardens(Mobile m)
        {
            List<BaseGarden> list = new List<BaseGarden>();

            if (m != null)
            {
                m_Table.TryGetValue(m, out List<BaseGarden> exists);

                if (exists != null)
                {
                    for (int i = 0; i < exists.Count; ++i)
                    {
                        BaseGarden house = exists[i];

                        if (house != null && !house.Deleted && house.Owner == m)
                            list.Add(house);
                    }
                }
            }

            return list;
        }

        public static BaseGarden FindGardenAt(Mobile m)
        {
            if (m == null || m.Deleted)
                return null;

            return FindGardenAt(m.Location, m.Map, 16);
        }

        public static BaseGarden FindGardenAt(Item item)
        {
            if (item == null || item.Deleted)
                return null;

            return FindGardenAt(item.GetWorldLocation(), item.Map, item.ItemData.Height);
        }

        public static BaseGarden FindGardenAt(IEntity e)
        {
            if (e == null || e.Deleted)
                return null;

            if (e is Item)
            {
                return FindGardenAt((Item)e);
            }
            else if (e is Mobile)
            {
                return FindGardenAt((Mobile)e);
            }

            return FindGardenAt(e.Location, e.Map, 16);
        }

        public static BaseGarden FindGardenAt(IPoint3D p, Map map)
        {
            if (p == null)
            {
                return null;
            }

            return FindGardenAt(new Point3D(p), map, 16);
        }

        public static BaseGarden FindGardenAt(Point3D loc, Map map, int height)
        {
            if (map == null || map == Map.Internal)
                return null;

             IEnumerable<Region>  regs = Region.FindRegions(loc,map);

             foreach (Region item in regs)
             {
                if (item is GardenRegion bg)
                {
                    return bg.Garden;
                }
                
             }


            return null;
        }

        private bool IsInList(Item item, Type[] list)
        {
            foreach (Type t in list)
            {
                if (t == item.GetType() || item.GetType().IsSubclassOf(t))
                    return true;
            }

            return false;
        }

        public static List<BaseGarden> AllGardens { get; } = new List<BaseGarden>();

        public BaseGarden(Mobile owner)
            : base()
        {
            AllGardens.Add(this);

            BuiltOn = DateTime.UtcNow;
          
            m_Owner = owner;


        //   UpdateRegion();


            Movable = false;
        }

        public BaseGarden(Serial serial)
            : base(serial)
        {
            AllGardens.Add(this);
        }

      
        public abstract Rectangle2D[] Area { get; }

        public virtual void UpdateRegion()
        {

            if (m_Region != null)
            {
                 m_Region.Unregister();
            }
   

            if (Map != null)
            {
                m_Region = new GardenRegion(this);
                m_Region.Register();
            }
            else
            {
                m_Region = null;
            }
        }

        public override void OnLocationChange(Point3D oldLocation)
        {
           
              base.OnLocationChange(oldLocation);

            int x = base.Location.X - oldLocation.X;
            int y = base.Location.Y - oldLocation.Y;
            int z = base.Location.Z - oldLocation.Z;

            if (Sign != null && !Sign.Deleted)
                Sign.Location = new Point3D(Sign.X + x, Sign.Y + y, Sign.Z + z);


              UpdateRegion();
        }

        public override void OnMapChange()
        {
            base.OnMapChange();

            UpdateRegion();

            if (Sign != null && !Sign.Deleted)
                Sign.Map = Map;
         
        }




        public override bool Decays => false;

        public virtual bool CouldGardenFit(IPoint3D p, Map map)
        {
            if (Deleted)
            {
                return false;
            }

            foreach (AddonComponent c in Components)
            {
                Point3D p3D = new Point3D(p.X + c.Offset.X, p.Y + c.Offset.Y, p.Z + c.Offset.Z);

                if (!map.CanFit(p3D.X, p3D.Y, p3D.Z, Math.Max(1, c.ItemData.Height), false, false, (c.Z == 0), true))
                {
                    return false;
                }
            }
            return true;
        }
     
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1); // version
            
            writer.Write(m_Public);
            writer.Write(Price);
            writer.Write(Sign);
            writer.Write(BuiltOn);
            writer.Write(m_Owner);

          
           
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

           

            switch (version)
            {
                    case 1:
                    {
                        m_Public = reader.ReadBool();
                        goto case 0;
                    }
                    case 0:
                    {
                       Price = reader.ReadInt();
                       Sign = reader.ReadItem() as GardenSign;
                       BuiltOn = reader.ReadDateTime();   
                       m_Owner = reader.ReadMobile();

                        UpdateRegion();

                        if ((Map == null || Map == Map.Internal) && Location == Point3D.Zero)
                            Delete();

                        if (m_Owner != null)
                        {
                            List<BaseGarden> list = null;
                            m_Table.TryGetValue(m_Owner, out list);

                            if (list == null)
                                m_Table[m_Owner] = list = new List<BaseGarden>();

                            list.Add(this);
                        }
                        break;
                    }
            }
        }
    
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get
            {
                return m_Owner;
            }
            set
            {
                m_Owner = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Public
        {
            get
            {
                return m_Public;
            }
            set
            {
                if (!m_Public)
                {
                    Owner = null;
                }

                m_Public = value;
                
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Price
        {
            get
            {
                return m_Price;
            }
            set
            {
                m_Price = value; 
            }
        }


        public Region Region => m_Region;

      
        public GardenSign Sign { get; set; }
       
        public DateTime BuiltOn { get; set; }

       

        public override void OnAfterDelete()
        {
           

            if (m_Owner != null)
            {
                List<BaseGarden> list = null;
                m_Table.TryGetValue(m_Owner, out list);

                if (list == null)
                    m_Table[m_Owner] = list = new List<BaseGarden>();

                list.Remove(this);
            }
       

            if (m_Region != null)
            {
                m_Region.Unregister();
                m_Region = null;
            }

            if (Sign != null)
                Sign.Delete();

            AllGardens.Remove(this);

             base.OnAfterDelete();
        }
      
        public bool IsOwner(Mobile m)
        {
            if (m == null)
                return false;

            if (m == m_Owner || m.AccessLevel >= AccessLevel.GameMaster)
                return true;

                return false;

           // return AccountHandler.CheckAccount(m, m_Owner);
        }

        public void SetSign(int xoff, int yoff, int zoff)
        {
            Sign = new GardenSign(this);
            Sign.MoveToWorld(new Point3D(X + xoff, Y + yoff, Z + zoff), Map);
        }

        public virtual void Redeed(Mobile from)
        {


            from.AddToBackpack(Deed());
            this.Delete();
            from.SendMessage("Vous avez mis votre contrat de jardin dans votre sac.");


        }






        #region  Transfert


        private class TransferItem : Item
        {
            private readonly BaseGarden m_House;

            public override string DefaultName => "Contrat de Jardin";

            public TransferItem(BaseGarden house)
                : base(0x14F0)
            {
                m_House = house;

                Hue = 0x480;
                Movable = false;
            }

            public override void GetProperties(ObjectPropertyList list)
            {
                base.GetProperties(list);

                string houseName, owner, location;

                houseName = "Jardin";

                Mobile houseOwner = (m_House == null ? null : m_House.Owner);

                if (houseOwner == null)
                    owner = "nobody";
                else
                    owner = houseOwner.Name;

                int xLong = 0, yLat = 0, xMins = 0, yMins = 0;
                bool xEast = false, ySouth = false;

                bool valid = m_House != null && Sextant.Format(m_House.Location, m_House.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth);

                if (valid)
                    location = string.Format("{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W");
                else
                    location = "unknown";

                list.Add(1061112, Utility.FixHtml(houseName)); // House Name: ~1_val~
               // list.Add(1061113, owner); // Owner: ~1_val~
                list.Add(1061114, location); // Location: ~1_val~
            }

            public TransferItem(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(0); // version
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();

                Delete();
            }

            public override bool AllowSecureTrade(Mobile from, Mobile to, Mobile newOwner, bool accepted)
            {
                if (!base.AllowSecureTrade(from, to, newOwner, accepted))
                    return false;
                else if (!accepted)
                    return true;

                if (Deleted || m_House == null || m_House.Deleted || !m_House.IsOwner(from) || !from.CheckAlive() || !to.CheckAlive())
                    return false;

                return m_House.CheckTransferPosition(from, to);
            }

            public override void OnSecureTrade(Mobile from, Mobile to, Mobile newOwner, bool accepted)
            {
                if (Deleted)
                    return;

                Delete();

                if (m_House == null || m_House.Deleted || !m_House.IsOwner(from) || !from.CheckAlive() || !to.CheckAlive())
                    return;

                if (!accepted)
                    return;

                    from.SendMessage("Vous avez transféré le jardin.");
                    to.SendMessage("Vous êtes maintenant propriétaire du jardin.");

 
                m_House.Owner = to;

                m_House.OnTransfer();
            }
        }

        public bool CheckTransferPosition(Mobile from, Mobile to)
        {
            bool isValid = true;
            Item sign = Sign;
            Point3D p = (sign == null ? Point3D.Zero : sign.GetWorldLocation());

            if (from.Map != Map || to.Map != Map)
                isValid = false;
            else if (sign == null)
                isValid = false;
            else if (from.Map != sign.Map || to.Map != sign.Map)
                isValid = false;
            else if (!from.InRange(p, 2))
                isValid = false;
            else if (!to.InRange(p, 2))
                isValid = false;

            if (!isValid)
                from.SendLocalizedMessage(1062067); // In order to transfer the house, you and the recipient must both be outside the building and within two paces of the house sign.

            return isValid;
        }

        public void BeginConfirmTransfer(Mobile from, Mobile to)
        {
            if (Deleted || !from.CheckAlive() || !IsOwner(from))
                return;

             if (from == to)
            {
                from.SendLocalizedMessage(1005330); // You cannot transfer a house to yourself, silly.
            }
            else if (to.Player)
            {
                if (CheckTransferPosition(from, to))
                {

                    EndConfirmTransfer(from, to);
    /*                 to.CloseGump(typeof(HouseTransferGump));
                     to.SendGump(new HouseTransferGump(from, to, this));*/
                    
                }
            }
            else
            {
                from.SendLocalizedMessage(501384); // Only a player can own a house!
            }
        }

        private void ConfirmTransfer_Callback(Mobile to, bool ok, object state)
        {
            Mobile from = (Mobile)state;

            if (!ok || Deleted || !from.CheckAlive() || !IsOwner(from))
                return;

            if (CheckTransferPosition(from, to))
            {
                EndConfirmTransfer(from,to);
             /*   to.CloseGump(typeof(HouseTransferGump));
                to.SendGump(new HouseTransferGump(from, to, this));*/
            }
        }

        public void EndConfirmTransfer(Mobile from, Mobile to)
        {
            if (Deleted || !from.CheckAlive() || !IsOwner(from))
                return;

            if (from == to)
            {
                from.SendLocalizedMessage(1005330); // You cannot transfer a house to yourself, silly.
            }
            else if (to.Player)
            {
                if (CheckTransferPosition(from, to))
                {
                    NetState fromState = from.NetState, toState = to.NetState;

                    if (fromState != null && toState != null)
                    {
                        if (from.HasTrade)
                        {
                            from.SendLocalizedMessage(1062071); // You cannot trade a house while you have other trades pending.
                        }
                        else if (to.HasTrade)
                        {
                            to.SendLocalizedMessage(1062071); // You cannot trade a house while you have other trades pending.
                        }
                        else if (!to.Alive)
                        {
                            // TODO: Check if the message is correct.
                            from.SendLocalizedMessage(1062069); // You cannot transfer this house to that person.
                        }
                        else
                        {
                            Container c = fromState.AddTrade(toState);

                            c.DropItem(new TransferItem(this));
                        }
                    }
                }
            }
            else
            {
                from.SendLocalizedMessage(501384); // Only a player can own a house!
            }
        }

        public virtual void OnTransfer()
        {

        }






        #endregion


        public static void PayRent()
        {
            foreach (BaseGarden item in AllGardens)
            {
                
                if (!item.Public && item.Owner != null && item.Price > 0 && item.Owner.IsPlayer() )
                {
                    if (Banker.Withdraw(item.Owner, (int)item.Price))
                    {
                        CustomPersistence.LocationJardin += item.Price;
                    }
                    else
                    {
                        item.Owner = null;
                    }
                    
                }
            }
        }













    }


}
