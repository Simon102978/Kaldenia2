using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class GardenFence : BaseAddon
	{

		[Constructable]
		public GardenFence()
		{

			
			/* //Fence
					//Corners - bottom
					AddComponent( new AddonComponent( 0x835 ), 3, 4, 0 ); //corner 

					//East Side
					AddComponent( new AddonComponent( 0x837 ), 3, -2, 0 ); 
					AddComponent( new AddonComponent( 0x837 ), 3, -1, 0 );
					AddComponent( new AddonComponent( 0x837 ), 3, 0, 0 );
					AddComponent( new AddonComponent( 0x837 ), 3, 1, 0 );
					AddComponent( new AddonComponent( 0x837 ), 3, 2, 0 );
					AddComponent( new AddonComponent( 0x837 ), 3, 3, 0 );

					//South Side
					AddComponent( new AddonComponent( 0x836 ), 2, 4, 0 ); 
					AddComponent( new AddonComponent( door ), 1, 4, 0 ); 
					AddComponent( new AddonComponent( door ), 0, 4, 0 );
					AddComponent( new AddonComponent( 0x836 ), -1, 4, 0 );  
					AddComponent( new AddonComponent( 0x836 ), -2, 4, 0 ); 

					//West Side
					AddComponent( new AddonComponent( 0x837 ), -3, 4, 0 ); 
					AddComponent( new AddonComponent( 0x837 ), -3, 3, 0 ); 
					AddComponent( new AddonComponent( 0x837 ), -3, 2, 0 ); 
					AddComponent( new AddonComponent( 0x837 ), -3, 1, 0 ); 
					AddComponent( new AddonComponent( 0x837 ), -3, 0, 0 );
					AddComponent( new AddonComponent( 0x837 ), -3, -1, 0 );
					AddComponent( new AddonComponent( 0x837 ), -3, -2, 0 ); 
					AddComponent( new AddonComponent( 0x838 ), -3, -3, 0 );   

					//North Side
					AddComponent( new AddonComponent( 0x836 ), -2, -3, 0 ); 
					AddComponent( new AddonComponent( 0x836 ), -1, -3, 0 ); 
					AddComponent( new AddonComponent( 0x836 ), 0, -3, 0 ); 
					AddComponent( new AddonComponent( 0x836 ), 1, -3, 0 ); 
					AddComponent( new AddonComponent( 0x836 ), 2, -3, 0 );
					AddComponent( new AddonComponent( 0x836 ), 3, -3, 0 );

					*/
		}

		public GardenFence( Serial serial ) : base( serial )
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
	
	public class GardenGround : BaseAddon
	{

		[Constructable]
		public GardenGround()
		{

			AddonComponent ac = null;
			ac = new AddonComponent(0x31F5); AddComponent(ac, 1, 0, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 0, 0, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 0, 1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 1, 1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 0, -1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 1, -1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 0, 2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 1, 2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 0, -2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 1, -2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 0, 3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 1, 3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 0, -3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 1, -3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 0, 4, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 1, 4, 0);
			ac = new AddonComponent(954); AddComponent(ac, 0, -4, 0);
			ac = new AddonComponent(954); AddComponent(ac, 1, -4, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 0, 5, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 1, 5, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -1, 0, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -1, 1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -1, -1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -1, 2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -1, -2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -1, 3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -1, -3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -1, 4, 0);
			ac = new AddonComponent(954); AddComponent(ac, -1, -4, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -1, 5, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 2, 0, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 2, 1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -2, 0, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -2, 1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 2, -1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -2, -1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 2, 2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -2, 2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 2, -2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -2, -2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 2, 3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -2, 3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 2, -3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -2, -3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 2, 4, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -2, 4, 0);
			ac = new AddonComponent(954); AddComponent(ac, 2, -4, 0);
			ac = new AddonComponent(954); AddComponent(ac, -2, -4, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 2, 5, 0);
			ac = new AddonComponent(954); AddComponent(ac, 2, 5, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -2, 5, 0);
			ac = new AddonComponent(954); AddComponent(ac, -2, 5, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 3, 0, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 3, 1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -3, 0, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -3, 1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 3, -1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -3, -1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 3, 2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -3, 2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 3, -2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -3, -2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 3, 3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -3, 3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 3, -3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -3, -3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 3, 4, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -3, 4, 0);
			ac = new AddonComponent(954); AddComponent(ac, 3, -4, 0);
			ac = new AddonComponent(954); AddComponent(ac, -3, -4, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 3, 5, 0);
			ac = new AddonComponent(954); AddComponent(ac, 3, 5, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, -3, 5, 0);
			ac = new AddonComponent(954); AddComponent(ac, -3, 5, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 4, 0, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 4, 1, 0);
			ac = new AddonComponent(953); AddComponent(ac, 4, 0, 0);
			ac = new AddonComponent(953); AddComponent(ac, 4, 1, 0);
			ac = new AddonComponent(953); AddComponent(ac, -4, 0, 0);
			ac = new AddonComponent(953); AddComponent(ac, -4, 1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 4, -1, 0);
			ac = new AddonComponent(953); AddComponent(ac, 4, -1, 0);
			ac = new AddonComponent(953); AddComponent(ac, -4, -1, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 4, 2, 0);
			ac = new AddonComponent(953); AddComponent(ac, 4, 2, 0);
			ac = new AddonComponent(953); AddComponent(ac, -4, 2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 4, -2, 0);
			ac = new AddonComponent(953); AddComponent(ac, 4, -2, 0);
			ac = new AddonComponent(953); AddComponent(ac, -4, -2, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 4, 3, 0);
			ac = new AddonComponent(953); AddComponent(ac, 4, 3, 0);
			ac = new AddonComponent(953); AddComponent(ac, -4, 3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 4, -3, 0);
			ac = new AddonComponent(953); AddComponent(ac, 4, -3, 0);
			ac = new AddonComponent(953); AddComponent(ac, -4, -3, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 4, 4, 0);
			ac = new AddonComponent(953); AddComponent(ac, 4, 4, 0);
			ac = new AddonComponent(953); AddComponent(ac, -4, 4, 0);
			ac = new AddonComponent(954); AddComponent(ac, 4, -4, 0);
			ac = new AddonComponent(955); AddComponent(ac, 4, -4, 0);
			ac = new AddonComponent(955); AddComponent(ac, -4, -4, 0);
			ac = new AddonComponent(0x31F5); AddComponent(ac, 4, 5, 0);
			ac = new AddonComponent(953); AddComponent(ac, 4, 5, 0);
			ac = new AddonComponent(954); AddComponent(ac, 4, 5, 0);
			ac = new AddonComponent(955); AddComponent(ac, 4, 5, 0);
			ac = new AddonComponent(953); AddComponent(ac, -4, 5, 0);
			ac = new AddonComponent(955); AddComponent(ac, -4, 5, 2);
			/*//Ground 5997.1682.0 - 3 3 0
				//Corners SE-NE
						AddComponent( new AddonComponent( 0x1b2f ), 2, 3, 0 );
						AddComponent( new AddonComponent( 0x1b31 ), -2, 3, 0 );
						AddComponent( new AddonComponent( 0x1b30 ), -2, -2, 0 );
						AddComponent( new AddonComponent( 0x1b32 ), 2, -2, 0 );
				//Outlined 'dirt' S to E
					//South
						AddComponent( new AddonComponent( 0x1b27 ), 1, 3, 0 );
						AddComponent( new AddonComponent( 0x1b27 ), 0, 3, 0 );
						AddComponent( new AddonComponent( 0x1b27 ), -1, 3, 0 );
					//West
						AddComponent( new AddonComponent( 0x1b29 ), -2, 2, 0 );
						AddComponent( new AddonComponent( 0x1b29 ), -2, 1, 0 );
						AddComponent( new AddonComponent( 0x1b29 ), -2, 0, 0 );
						AddComponent( new AddonComponent( 0x1b29 ), -2, -1, 0 );
					//North
						AddComponent( new AddonComponent( 0x1b2a ), -1, -2, 0 );
						AddComponent( new AddonComponent( 0x1b2a ), 0, -2, 0 );
						AddComponent( new AddonComponent( 0x1b2a ), 1, -2, 0 );
					//East
						AddComponent( new AddonComponent( 0x1b28 ), 2, -1, 0 );
						AddComponent( new AddonComponent( 0x1b28 ), 2, 0, 0 );
						AddComponent( new AddonComponent( 0x1b28 ), 2, 1, 0 );
						AddComponent( new AddonComponent( 0x1b28 ), 2, 2, 0 );
					//Growing Dirt
						AddComponent( new AddonComponent( 0x32C9 ), -1, -1, 0 );
						AddComponent( new AddonComponent( 0x32C9 ), 0, 0, 0 );
						AddComponent( new AddonComponent( 0x32C9 ), 1, -1, 0 );
						AddComponent( new AddonComponent( 0x32C9 ), -1, 0, 0 );
						AddComponent( new AddonComponent( 0x32C9 ), 0, -1, 0 );
						AddComponent( new AddonComponent( 0x32C9 ), -1, 1, 0 );
						AddComponent( new AddonComponent( 0x32C9 ), 1, 0, 0 );
						AddComponent( new AddonComponent( 0x32C9 ), 1, 1, 0 );
						AddComponent( new AddonComponent( 0x32C9 ), -1, 2, 0 );
						AddComponent( new AddonComponent( 0x32C9 ), 0, 2, 0 );
						AddComponent( new AddonComponent( 0x32C9 ), 1, 2, 0 );
						AddComponent( new AddonComponent( 0x32C9 ), 0, 1, 0 );

						*/


		}

		public GardenGround( Serial serial ) : base( serial )
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

	}	//added after here...
}