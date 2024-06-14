namespace Server.Items
{
    public class PieceWhiteKing : BasePiece
    {
        public PieceWhiteKing(BaseRegularBoard RegularBoard)
            : base(0x3587, RegularBoard)
        {
        }

        public PieceWhiteKing(Serial serial)
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

    public class PieceBlackKing : BasePiece
    {
        public PieceBlackKing(BaseRegularBoard RegularBoard)
            : base(0x358E, RegularBoard)
        {
        }

        public PieceBlackKing(Serial serial)
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

    public class PieceWhiteQueen : BasePiece
    {
        public PieceWhiteQueen(BaseRegularBoard RegularBoard)
            : base(0x358A, RegularBoard)
        {
        }

        public PieceWhiteQueen(Serial serial)
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

    public class PieceBlackQueen : BasePiece
    {
        public PieceBlackQueen(BaseRegularBoard RegularBoard)
            : base(0x3591, RegularBoard)
        {
        }

        public PieceBlackQueen(Serial serial)
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

    public class PieceWhiteRook : BasePiece
    {
        public PieceWhiteRook(BaseRegularBoard RegularBoard)
            : base(0x3586, RegularBoard)
        {
        }

        public PieceWhiteRook(Serial serial)
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

    public class PieceBlackRook : BasePiece
    {
        public PieceBlackRook(BaseRegularBoard RegularBoard)
            : base(0x358D, RegularBoard)
        {
        }

        public PieceBlackRook(Serial serial)
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

    public class PieceWhiteBishop : BasePiece
    {
        public PieceWhiteBishop(BaseRegularBoard RegularBoard)
            : base(0x3585, RegularBoard)
        {
        }

        public PieceWhiteBishop(Serial serial)
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

    public class PieceBlackBishop : BasePiece
    {
        public PieceBlackBishop(BaseRegularBoard RegularBoard)
            : base(0x358C, RegularBoard)
        {
        }

        public PieceBlackBishop(Serial serial)
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

    public class PieceWhiteKnight : BasePiece
    {
        public PieceWhiteKnight(BaseRegularBoard RegularBoard)
            : base(0x3588, RegularBoard)
        {
        }

        public PieceWhiteKnight(Serial serial)
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

    public class PieceBlackKnight : BasePiece
    {
        public PieceBlackKnight(BaseRegularBoard RegularBoard)
            : base(0x358F, RegularBoard)
        {
        }

        public PieceBlackKnight(Serial serial)
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

    public class PieceWhitePawn : BasePiece
    {
        public PieceWhitePawn(BaseRegularBoard RegularBoard)
            : base(0x3589, RegularBoard)
        {
        }

        public PieceWhitePawn(Serial serial)
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

    public class PieceBlackPawn : BasePiece
    {
        public PieceBlackPawn(BaseRegularBoard RegularBoard)
            : base(0x3590, RegularBoard)
        {
        }

        public PieceBlackPawn(Serial serial)
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