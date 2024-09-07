using System;
using Server;

namespace Server.Items
{
	public class PileOfBlankCandles : Item, IDyable
	{

		[Constructable]
		public PileOfBlankCandles() : base( 0x1BD6 )
		{
			Name = "Pile of Blank Candles";
			Weight = 3.0;
			Hue = 1153;
		}
		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;
			return true;
		}

		public PileOfBlankCandles( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}