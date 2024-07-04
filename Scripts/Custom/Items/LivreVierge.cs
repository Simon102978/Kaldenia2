using System;
using Server;

namespace Server.Items
{
    [Flipable(0xFBE, 0xFBD)]
	public class LivreVierge : Item
	{
		[Constructable]
		public LivreVierge() : base( 0xFBE )
		{
            Name = "Livre Vierge";
			Weight = 2.0;
		}

        public LivreVierge(Serial serial)
            : base(serial)
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