using System;

namespace Server.Items
{
    public class BaitFish : BaseBait
	{
		[Constructable]
		public BaitFish() : this( 20 )
		{
		}

		[Constructable]
		public BaitFish( int charge ) : base( Bait.Fish, charge )
		{
		}

		public BaitFish( Serial serial ) : base( serial )
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