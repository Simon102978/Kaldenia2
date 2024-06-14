using System;
using System.Collections;

namespace Server.Items
{
	[Flipable(0xE1C, 0xFAD)]
	public class TablutBoard : BaseBoard
	{
		[Constructable]
		public TablutBoard() : base(0xE1C)
		{
			Name = "Jeu de Tablut";
		}

		public override void CreatePieces()
		{
			for (int i = 0; i < 2; i++)
			{
				//Vertical
				CreatePiece(new PieceRedTablut(this), 113, (160 * i) + 35);
				CreatePiece(new PieceRedTablut(this), 133, (160 * i) + 35);
				CreatePiece(new PieceRedTablut(this), 153, (160 * i) + 35);

				CreatePiece(new PieceRedTablut(this), 133, (120 * i) + 55);

				CreatePiece(new PieceBlueTablut(this), 133, (60 * i) + 75);
				CreatePiece(new PieceBlueTablut(this), 133, (60 * i) + 95);

				//Horizontal
				CreatePiece(new PieceRedTablut(this), (160 * i) + 53, 95);
				CreatePiece(new PieceRedTablut(this), (160 * i) + 53, 115);
				CreatePiece(new PieceRedTablut(this), (160 * i) + 53, 135);

				CreatePiece(new PieceRedTablut(this), (120 * i) + 73, 115);

				CreatePiece(new PieceBlueTablut(this), (60 * i) + 93, 115);
				CreatePiece(new PieceBlueTablut(this), (60 * i) + 113, 115);
			}
			
			CreatePiece(new PieceGrayTablut(this), 133, 115);
		}

		public TablutBoard(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

	}
}
