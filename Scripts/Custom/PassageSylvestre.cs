#region References
using Server.Engines.CityLoyalty;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Regions;
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Server.Items
{

    public class PassageSylvestre : Teleporter
    {

        private int m_Niveau;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Niveau
        {
            get { return m_Niveau; }
            set
            {
                if (value > 12)
                {
                    m_Niveau = 12;
                }
                else
                {
                    m_Niveau = value;
                }

                SwitchId();

                InvalidateProperties();
            }
        }

        [Constructable]
        public PassageSylvestre()
            : this(new Point3D(0, 0, 0), null, false)
        {





        }

        [Constructable]
        public PassageSylvestre(Point3D pointDest, Map mapDest, bool creatures)
            : base(pointDest, mapDest, creatures)
        {
            CombatCheck = true;
            DestEffect = true;
            Delay = TimeSpan.FromSeconds(5);
            Visible = true;
            MapDest = Map.Felucca;
            SwitchId();

        }

        public PassageSylvestre(Serial serial)
            : base(serial)
        { }

        public override void GetProperties(ObjectPropertyList list)
        {

            list.Add("Passage Sylvestre"); // active
            list.Add("Niveau : " + m_Niveau);

        }

        public override bool CanTeleport(Mobile m)
        {
            IPooledEnumerable eable = Map.GetItemsInRange(this.Location, 0);

            foreach (Item item in eable)
            {
                if (item is PorteSylvestre)
                {
                    return false;  // Grosso modo, la raison pourquoi je  bloque ici, c'est pour que la porte sylvestre teleporte la personne et non pas le passage et donc pas de delais d'attente.
                }
            }

            double skill =  m.Skills[SkillName.Camping].Value;
            double skillRequis = 35 + m_Niveau * 5;

             if (skill <= skillRequis && !m.IsStaff())
             {
                return false;
             }   

            m.CheckSkill(SkillName.Camping, skillRequis, skillRequis + 20);

            return base.CanTeleport(m);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.Write(Niveau);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Niveau = reader.ReadInt();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from is CustomPlayerMobile sp)
            {

                double skill =  sp.Skills[SkillName.Camping].Value;
                double skillRequis = 35 + m_Niveau * 5 + 15;

                if (skill >= 100 || skill  >= skillRequis || sp.IsStaff())
                {
                    from.SendLocalizedMessage(501024); // You open a magical gate to another location

                    Effects.PlaySound(Location, Map, 0x20E);

                    PorteSylvestre firstGate = new PorteSylvestre(PointDest, MapDest);
                    firstGate.MoveToWorld(Location, Map);
                }
                else
                {
                    from.SendMessage("Vous n'avez pas la compétence pour ouvrir le passage.");
                }
            }

        }



        public override bool CanSee(Mobile m)
        {
                double skill =  m.Skills[SkillName.Camping].Value;
                double skillRequis = 35 + m_Niveau * 5;

                if (skill <= skillRequis && !m.IsStaff())
                {
                    return false;
                }   

            return base.CanSee(m);
        }

        public void SwitchId()
        {
            switch (m_Niveau)
            {
                case 1:
                    ItemID = 18491;
                    break;
                case 2:
                    ItemID = 18494;
                    break;
                case 3:
                    ItemID = 18497;
                    break;
                case 4:
                    ItemID = 18500;
                    break;
                case 5:
                    ItemID = 18503;
                    break;
                case 6:
                    ItemID = 18506;
                    break;
                case 7:
                    ItemID = 18491;
                    break;
                case 8:
                    ItemID = 18509;
                    break;
                case 9:
                    ItemID = 18560;
                    break;
                case 10:
                    ItemID = 18515;
                    break;
                case 11:
                    ItemID = 18518;
                    break;
                case 12:
                    ItemID = 18521;
                    break;
                default:
                    ItemID = 18554;
                    break;
            }




        }

    }

    public class PorteSylvestre : Moongate
    {
       

        public PorteSylvestre(Point3D target, Map map)
            : base(target, map)
        {
            Map = map;
            Hue = 1795;
            Name = "Porte Sylvestre";
            
            Dispellable = true;

            InternalTimer t = new InternalTimer(this);
            t.Start();
        }
     

       

        public PorteSylvestre(Serial serial)
            : base(serial)
        {
        }

        public override bool ShowFeluccaWarning => false;
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
