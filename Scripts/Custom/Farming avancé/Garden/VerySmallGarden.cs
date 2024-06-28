using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Multis;

namespace Server.Items
{
	public class VerySmallGarden : BaseGarden
	{
		
		public override Rectangle2D[] Area => new Rectangle2D[] { new Rectangle2D(0, 0, 3, 3) };

		

		[ Constructable ]
		public VerySmallGarden(Mobile owner) : base( owner )
		{
			AddonComponent ac = null;
		
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 0x31F5 );			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 0x0013 );			AddComponent( ac, 2, 2, 0 ); // 0x00A9
			SetSign(3, 3, 0);

 		}

	
		public VerySmallGarden( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override BaseGardenDeed Deed()
		{
			return new VerySmallGardenDeed();
		}
	}

	public class VerySmallGardenDeed : BaseGardenDeed
	{
		[Constructable]
		public VerySmallGardenDeed()
		{
			Name = "Very Small Garden Field";
		}

	   public override BaseGarden GetGarden (Mobile from)
	   {
		return new VerySmallGarden(from);

	   }

		public VerySmallGardenDeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }

		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}