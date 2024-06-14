namespace Server.Items
{
    public class BasePiece : Item
    {
        private BaseRegularBoard m_RegularBoard;
        public BasePiece(int itemID, BaseRegularBoard RegularBoard)
            : base(itemID)
        {
            m_RegularBoard = RegularBoard;
        }

        public BasePiece(Serial serial)
            : base(serial)
        {
        }

        public BaseRegularBoard RegularBoard
        {
            get
            {
                return m_RegularBoard;
            }
            set
            {
                m_RegularBoard = value;
            }
        }
        public override bool IsVirtualItem => true;
        public override bool CanTarget => false;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
            writer.Write(m_RegularBoard);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_RegularBoard = (BaseRegularBoard)reader.ReadItem();

                        if (m_RegularBoard == null || Parent == null)
                            Delete();

                        break;
                    }
            }
        }

        public override bool OnDragLift(Mobile from)
        {
            if (m_RegularBoard == null || m_RegularBoard.Deleted)
            {
                Delete();
                return false;
            }
            else if (!IsChildOf(m_RegularBoard))
            {
                m_RegularBoard.DropItem(this);
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool DropToMobile(Mobile from, Mobile target, Point3D p)
        {
            return false;
        }

        public override bool DropToItem(Mobile from, Item target, Point3D p)
        {
            return (target == m_RegularBoard && p.X != -1 && p.Y != -1 && base.DropToItem(from, target, p));
        }

        public override bool DropToWorld(Mobile from, Point3D p)
        {
            return false;
        }

        public override int GetLiftSound(Mobile from)
        {
            return -1;
        }
    }
}
