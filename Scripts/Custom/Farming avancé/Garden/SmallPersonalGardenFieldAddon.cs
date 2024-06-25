using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Multis;

namespace Server.Items
{
	public class SmallPersonalGardenFieldAddon : BaseGarden
	{
		
		public override Rectangle2D[] Area => new Rectangle2D[] { new Rectangle2D(-4, -4, 8, 10) };

		

		[ Constructable ]
		public SmallPersonalGardenFieldAddon(Mobile owner) : base( owner )
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, 3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, -3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, -3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, 4, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, 4, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, 0, -4, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, 1, -4, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, 5, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, 5, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -1, 3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -1, -3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -1, 4, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, -1, -4, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -1, 5, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, 3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -2, 3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, -3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -2, -3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, 4, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -2, 4, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, 2, -4, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, -2, -4, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, 5, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, 2, 5, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -2, 5, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, -2, 5, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 3, 0, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 3, 1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -3, 0, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -3, 1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 3, -1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -3, -1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 3, 2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -3, 2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 3, -2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -3, -2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 3, 3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -3, 3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 3, -3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -3, -3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 3, 4, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -3, 4, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, 3, -4, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, -3, -4, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 3, 5, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, 3, 5, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, -3, 5, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, -3, 5, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 4, 0, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 4, 1, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, 4, 0, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, 4, 1, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, -4, 0, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, -4, 1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 4, -1, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, 4, -1, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, -4, -1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 4, 2, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, 4, 2, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, -4, 2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 4, -2, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, 4, -2, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, -4, -2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 4, 3, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, 4, 3, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, -4, 3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 4, -3, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, 4, -3, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, -4, -3, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 4, 4, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, 4, 4, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, -4, 4, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, 4, -4, 0 );
			ac = new AddonComponent( 955 );				AddComponent( ac, 4, -4, 0 );
			ac = new AddonComponent( 955 );				AddComponent( ac, -4, -4, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 4, 5, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, 4, 5, 0 );
			ac = new AddonComponent( 954 );				AddComponent( ac, 4, 5, 0 );
			ac = new AddonComponent( 955 );				AddComponent( ac, 4, 5, 0 );
			ac = new AddonComponent( 953 );				AddComponent( ac, -4, 5, 0 );
			ac = new AddonComponent( 955 );				AddComponent( ac, -4, 5, 2 );

			SetSign(-3, 6, 2);

 		}

	
		public SmallPersonalGardenFieldAddon( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override BaseGardenDeed Deed()
		{
			return new SmallPersonalGardenFieldAddonDeed();
		}
	}

	public class SmallPersonalGardenFieldAddonDeed : BaseGardenDeed
	{
		[Constructable]
		public SmallPersonalGardenFieldAddonDeed()
		{
			Name = "SmallPersonalGardenField";
		}

	   public override BaseGarden GetGarden (Mobile from)
	   {
		return new SmallPersonalGardenFieldAddon(from);

	   }

		public SmallPersonalGardenFieldAddonDeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }

		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}