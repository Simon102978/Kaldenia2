using System;
using Server;

namespace Server.Items
{
	public class PieceBlueTablut : BasePiece
	{
		public override string DefaultName
		{
			get { return "Jeton bleu Défenseur"; }
		}

		public PieceBlueTablut( BaseBoard board ) : base( 0x3593, board )
		{
		}

		public PieceBlueTablut( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class PieceRedTablut : BasePiece
	{
		public override string DefaultName
		{
			get { return "Jeton rouge Attaquant"; }
		}

		public PieceRedTablut( BaseBoard board ) : base( 0x3594, board )
		{
		}

		public PieceRedTablut( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class PieceGrayTablut : BasePiece
	{
		public override string DefaultName
		{
			get { return "Jeton gris Roi"; }
		}

		public PieceGrayTablut( BaseBoard board ) : base( 0x3592, board )
		{
		}

		public PieceGrayTablut( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}