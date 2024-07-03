namespace Server.Items
{
    public class CustomPieceWhiteChecker : BasePiece
    {
        public CustomPieceWhiteChecker(BaseBoard board)
            : base(0xA61E, board)
        {
        }

        public CustomPieceWhiteChecker(Serial serial)
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

    public class CustomPieceBlackChecker : BasePiece
    {
        public CustomPieceBlackChecker(BaseBoard board)
            : base(0xA625, board)
        {
        }

        public CustomPieceBlackChecker(Serial serial)
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