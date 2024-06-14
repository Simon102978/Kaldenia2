namespace Server.Items
{
    public class PieceWhiteChecker : BasePiece
    {
        public PieceWhiteChecker(BaseRegularBoard RegularBoard)
            : base(0x3584, RegularBoard)
        {
        }

        public PieceWhiteChecker(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "white checker";
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class PieceBlackChecker : BasePiece
    {
        public PieceBlackChecker(BaseRegularBoard RegularBoard)
            : base(0x358B, RegularBoard)
        {
        }

        public PieceBlackChecker(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "black checker";
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}