using System.Collections.Generic;

namespace Server.Engines.TombOfKings
{
    public class ChAmbreLever : Item
    {
        public static void Generate()
        {
            foreach (Point3D loc in m_LeverLocations)
            {
                ChAmbreLever item = new ChAmbreLever(loc);
                WeakEntityCollection.Add("sa", item);

                m_Levers.Add(item);
            }
        }

        private static readonly Point3D[] m_LeverLocations = new Point3D[]
        {
            new Point3D( 25, 229, 2 ),
            new Point3D( 25, 227, 2 ),
            new Point3D( 25, 225, 2 ),

            new Point3D( 25, 221, 2 ),
            new Point3D( 25, 219, 2 ),
            new Point3D( 25, 217, 2 ),

            new Point3D( 45, 229, 2 ),
            new Point3D( 45, 227, 2 ),
            new Point3D( 45, 225, 2 ),

            new Point3D( 45, 221, 2 ),
            new Point3D( 45, 219, 2 ),
            new Point3D( 45, 217, 2 ),
        };

        private static readonly List<ChAmbreLever> m_Levers = new List<ChAmbreLever>();

        public static List<ChAmbreLever> Levers => m_Levers;

        private ChAmbre m_ChAmbre;

        public ChAmbre ChAmbre
        {
            get { return m_ChAmbre; }
            set
            {
                m_ChAmbre = value;
                InvalidateProperties();
            }
        }

        public bool IsUsable()
        {
            if (m_ChAmbre == null)
                return false;

            return !m_ChAmbre.IsOpened();
        }

        public ChAmbreLever(Point3D loc)
            : base(Utility.RandomBool() ? 0x108C : 0x108E)
        {
            Movable = false;
            MoveToWorld(loc, Map.TerMur);
        }

        public ChAmbreLever(Serial serial)
            : base(serial)
        {
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            if (IsUsable())
                list.Add(1112130); // a lever
            else
                list.Add(1112129); // a lever (unusable)
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsUsable() && from.InRange(this, 1))
                m_ChAmbre.Open();
        }

        public void Switch()
        {
            if (ItemID == 0x108C)
                ItemID = 0x108E;
            else
                ItemID = 0x108C;

            Effects.PlaySound(Location, Map, 0x3E8);

            InvalidateProperties();
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

            m_Levers.Add(this);
        }
    }
}