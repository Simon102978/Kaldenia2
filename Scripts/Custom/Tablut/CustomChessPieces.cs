namespace Server.Items
{
    public class CustomPieceWhiteKing : BasePiece
    {
        public CustomPieceWhiteKing(BaseBoard board)
            : base(0xA621, board)
        {
        }

        public CustomPieceWhiteKing(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "white king";
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

    public class CustomPieceBlackKing : BasePiece
    {
        public CustomPieceBlackKing(BaseBoard board)
            : base(0xA628, board)
        {
        }

        public CustomPieceBlackKing(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "black king";
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

    public class CustomPieceWhiteQueen : BasePiece
    {
        public CustomPieceWhiteQueen(BaseBoard board)
            : base(0xA624, board)
        {
        }

        public CustomPieceWhiteQueen(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "white queen";
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

    public class CustomPieceBlackQueen : BasePiece
    {
        public CustomPieceBlackQueen(BaseBoard board)
            : base(0xA62B, board)
        {
        }

        public CustomPieceBlackQueen(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "black queen";
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

    public class CustomPieceWhiteRook : BasePiece
    {
        public CustomPieceWhiteRook(BaseBoard board)
            : base(0xA620, board)
        {
        }

        public CustomPieceWhiteRook(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "white rook";
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

    public class CustomPieceBlackRook : BasePiece
    {
        public CustomPieceBlackRook(BaseBoard board)
            : base(0xA627, board)
        {
        }

        public CustomPieceBlackRook(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "black rook";
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

    public class CustomPieceWhiteBishop : BasePiece
    {
        public CustomPieceWhiteBishop(BaseBoard board)
            : base(0xA61F, board)
        {
        }

        public CustomPieceWhiteBishop(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "white bishop";
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

    public class CustomPieceBlackBishop : BasePiece
    {
        public CustomPieceBlackBishop(BaseBoard board)
            : base(0xA626, board)
        {
        }

        public CustomPieceBlackBishop(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "black bishop";
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

    public class CustomPieceWhiteKnight : BasePiece
    {
        public CustomPieceWhiteKnight(BaseBoard board)
            : base(0xA622, board)
        {
        }

        public CustomPieceWhiteKnight(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "white knight";
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

    public class CustomPieceBlackKnight : BasePiece
    {
        public CustomPieceBlackKnight(BaseBoard board)
            : base(0xA629, board)
        {
        }

        public CustomPieceBlackKnight(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "black knight";
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

    public class CustomPieceWhitePawn : BasePiece
    {
        public CustomPieceWhitePawn(BaseBoard board)
            : base(0xA623, board)
        {
        }

        public CustomPieceWhitePawn(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "white pawn";
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

    public class CustomPieceBlackPawn : BasePiece
    {
        public CustomPieceBlackPawn(BaseBoard board)
            : base(0xA62A, board)
        {
        }

        public CustomPieceBlackPawn(Serial serial)
            : base(serial)
        {
        }

        public override string DefaultName => "black pawn";
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