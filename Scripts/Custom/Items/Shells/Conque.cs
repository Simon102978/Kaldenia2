using System;
using Server;

namespace Server.Items
{
	public class Conque : BaseShell
	{
		[Constructable]
		public Conque() : base(4036)
		{
            Name = "Conque";
		}

        public Conque(Serial serial) : base(serial)
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