namespace Server.Items
{
    public class Tablut : BaseBoard
    {
        public override Rectangle2D Bounds => new Rectangle2D(0,0,282+25,210+25);

        //(int x, int y, int width, int height)
        public override int DefaultGumpID => 2351;


        [Constructable]
        public Tablut()
            : base(0xFA6)
        {
            Name = "Tablut";

        }

        public Tablut(Serial serial)
            : base(serial)
        {

        }

 //       public override int LabelNumber => 1016450;// a chessboard
        public override void CreatePieces()
        {

            CreatePiece(new CustomPieceBlackPawn(this), 117, 0);
            CreatePiece(new CustomPieceBlackPawn(this), 142, 0);
            CreatePiece(new CustomPieceBlackPawn(this), 167, 0);
            CreatePiece(new CustomPieceBlackPawn(this), 142, 22);
            CreatePiece(new CustomPieceBlackPawn(this), 42, 71);
            CreatePiece(new CustomPieceBlackPawn(this), 42, 97);
            CreatePiece(new CustomPieceBlackPawn(this), 42, 122);
            CreatePiece(new CustomPieceBlackPawn(this), 68, 97);
            CreatePiece(new CustomPieceBlackPawn(this), 117, 198);
            CreatePiece(new CustomPieceBlackPawn(this), 142, 198);
            CreatePiece(new CustomPieceBlackPawn(this), 167, 198);
            CreatePiece(new CustomPieceBlackPawn(this), 143, 173);
            CreatePiece(new CustomPieceBlackPawn(this), 241, 122);
            CreatePiece(new CustomPieceBlackPawn(this), 241, 97);
            CreatePiece(new CustomPieceBlackPawn(this), 241, 71);
            CreatePiece(new CustomPieceBlackPawn(this), 216, 97);

            CreatePiece(new CustomPieceWhitePawn(this), 142, 47);
            CreatePiece(new CustomPieceWhitePawn(this), 142, 72);
            CreatePiece(new CustomPieceWhiteKing(this), 142, 83);
            CreatePiece(new CustomPieceWhitePawn(this), 142, 122);
            CreatePiece(new CustomPieceWhitePawn(this), 142, 147);
            CreatePiece(new CustomPieceWhitePawn(this), 117, 97);
            CreatePiece(new CustomPieceWhitePawn(this), 92, 97);
            CreatePiece(new CustomPieceWhitePawn(this), 167, 97);
            CreatePiece(new CustomPieceWhitePawn(this), 192, 97);

            /*      for (int i = 0; i < 8; i++)
                  {
                      CreatePiece(new PieceBlackPawn(this), 67, (25 * i) + 17);
                      CreatePiece(new PieceWhitePawn(this), 192, (25 * i) + 17);
                  }

                  // Rook
                  CreatePiece(new PieceBlackRook(this), 42, 5);
                  CreatePiece(new PieceBlackRook(this), 42, 180);

                  CreatePiece(new PieceWhiteRook(this), 216, 5);
                  CreatePiece(new PieceWhiteRook(this), 216, 180);

                  // Knight
                  CreatePiece(new PieceBlackKnight(this), 42, 30);
                  CreatePiece(new PieceBlackKnight(this), 42, 155);

                  CreatePiece(new PieceWhiteKnight(this), 216, 30);
                  CreatePiece(new PieceWhiteKnight(this), 216, 155);

                  // Bishop
                  CreatePiece(new PieceBlackBishop(this), 42, 55);
                  CreatePiece(new PieceBlackBishop(this), 42, 130);

                  CreatePiece(new PieceWhiteBishop(this), 216, 55);
                  CreatePiece(new PieceWhiteBishop(this), 216, 130);

                  // Queen
                  CreatePiece(new PieceBlackQueen(this), 42, 105);
                  CreatePiece(new PieceWhiteQueen(this), 216, 105);

                  // King
                  CreatePiece(new PieceBlackKing(this), 42, 80);
                  CreatePiece(new PieceWhiteKing(this), 216, 80);*/
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
