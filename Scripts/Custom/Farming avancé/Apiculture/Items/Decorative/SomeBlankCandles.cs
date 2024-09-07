using System;
using Server;

namespace Server.Items
{
	public class SomeBlankCandles : Item, IDyable
	{

		[Constructable]
		public SomeBlankCandles() : base( 0x1BD5 )
		{
			Name = "Some Blank Candles";
			Weight = 2.0;
			Hue = 1154;
		}

		public bool Dye(Mobile from, DyeTub sender)
		{
			if (Deleted)
				return false;

			Hue = sender.DyedHue;
			return true;
		}

		public SomeBlankCandles( Serial serial ) : base( serial )
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