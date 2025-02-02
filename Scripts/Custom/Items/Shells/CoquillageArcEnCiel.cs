using System;
using Server;

namespace Server.Items
{
	public class CoquillageArcEnCiel : BaseShell
	{
		[Constructable]
		public CoquillageArcEnCiel() : base(4044)
		{
            Name = "Coquillage Arc En Ciel";
		}

        public CoquillageArcEnCiel(Serial serial) : base(serial)
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