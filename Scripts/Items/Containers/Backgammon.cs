namespace Server.Items
{
    [Flipable(0xE1C, 0xFAD)]
    public class Backgammon : BaseBoard
    {
        [Constructable]
        public Backgammon()
            : base(0xE1C)
        {
        }

        public Backgammon(Serial serial)
            : base(serial)
        {
        }

        public override void CreatePieces()
        {
            for (int i = 0; i < 5; i++)
            {
                CreatePiece(new CustomPieceWhiteChecker(this), 42, (17 * i) + 6);
                CreatePiece(new CustomPieceBlackChecker(this), 42, (17 * i) + 119);

                CreatePiece(new CustomPieceBlackChecker(this), 142, (17 * i) + 6);
                CreatePiece(new CustomPieceWhiteChecker(this), 142, (17 * i) + 119);
            }

            for (int i = 0; i < 3; i++)
            {
                CreatePiece(new CustomPieceBlackChecker(this), 108, (17 * i) + 6);
                CreatePiece(new CustomPieceWhiteChecker(this), 108, (17 * i) + 153);
            }

            for (int i = 0; i < 2; i++)
            {
                CreatePiece(new CustomPieceWhiteChecker(this), 223, (17 * i) + 6);
                CreatePiece(new CustomPieceBlackChecker(this), 223, (17 * i) + 170);
            }
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
        }
    }
}