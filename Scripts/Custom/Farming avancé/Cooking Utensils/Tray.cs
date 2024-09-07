namespace Server.Items
{
	[FlipableAttribute( 2449, 2450 )]
	public class Tray : Item
	{
		[Constructable]
		public Tray() : base( Utility.RandomList( 2449, 2450 ) )
		{
		}

		public Tray( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class TrayNS : Tray
	{
		[Constructable]
		public TrayNS() : base()
		{
			Name = "Plateau";
			this.ItemID = 2450;
			Weight = 1.0;
		}

		public TrayNS( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class TrayEW : Tray
	{
		[Constructable]
		public TrayEW() : base()
		{
			this.ItemID = 2449;
		}

		public TrayEW( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
