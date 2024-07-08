namespace Server.Engines.TombOfKings
{
    public class ChAmbreSwitch : Item
    {
        private readonly ChAmbre m_ChAmbre;

        public ChAmbreSwitch(ChAmbre chAmbre, Point3D loc, int itemId)
            : base(itemId)
        {
            m_ChAmbre = chAmbre;

            Movable = false;
            MoveToWorld(loc, Map.TerMur);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!m_ChAmbre.IsOpened() && from.InRange(this, 1))
            {
                Effects.PlaySound(Location, Map, 0x3E8);

                switch (ItemID)
                {
                    case 0x108F: ItemID = 0x1090; break;
                    case 0x1090: ItemID = 0x108F; break;
                    case 0x1091: ItemID = 0x1092; break;
                    case 0x1092: ItemID = 0x1091; break;
                }

                m_ChAmbre.Open();
            }
        }

        public ChAmbreSwitch(Serial serial)
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
    }
}