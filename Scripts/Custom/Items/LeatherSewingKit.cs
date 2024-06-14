using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class LeatherSewingKit : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefLeatherArmor.CraftSystem; } }

		[Constructable]
		public LeatherSewingKit() : base( 0xF9D )
		{
            Name = "Kit de couture (Cuir)";
            Hue = 1355;
            Weight = 2.0;
		}

		[Constructable]
		public LeatherSewingKit( int uses ) : base( uses, 0xF9D )
		{
            Name = "Kit de couture (Cuir)";
            Hue = 1355;
            Weight = 2.0;
		}

		public LeatherSewingKit( Serial serial ) : base( serial )
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

            if (Hue != 1355)
                Hue = 1355;
		}
	}
}