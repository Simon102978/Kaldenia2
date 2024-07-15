using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[Flipable(0x194F, 0x194F)]
	public class OutilsJardin : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBotanique.CraftSystem; } }

		[Constructable]
		public OutilsJardin() : base(0x194F)
		{
            Name = "Outils Jardinage";
			Weight = 2.0;
		}

		[Constructable]
		public OutilsJardin( int uses ) : base( uses, 0x194F)
        {
            Name = "Pinceaux";
			Weight = 2.0;
		}

		public OutilsJardin( Serial serial ) : base( serial )
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

			if ( Weight == 1.0 )
                Weight = 2.0;

            Name = "Outils Jardinage";
            ItemID = 0x194F;
		}
	}
}