namespace Server.Items.Crops
{
	public class BlackberrySeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public BlackberrySeed() : this( 1 ) { }

		[Constructable]
		public BlackberrySeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = 0.1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Blackberry Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(BlackberrySeedling));
		}
		
		public BlackberrySeed( Serial serial ) : base( serial ) { }

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

	public class BlackberrySeedling : BaseSeedling
	{
		[Constructable]
		public BlackberrySeedling( Mobile sower ) : base( 0xC61 )
		{
			Movable = false;
			Name = "Blackberry Seedling";
			Sower = sower;
            Init(this, typeof(BlackberryCrop));
		}
		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Mounted && !CropHelper.CanWorkMounted ) 
			{ 
				from.SendMessage( "Le plant est trop petit pour pouvoir être récolté sur votre monture." ); 
				return; 
			}
			else 
				from.SendMessage( "Votre pousse est trop jeune pour être récoltée." );
		}
		public BlackberrySeedling( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

            Init(this, typeof(BlackberryCrop));
		}
	}

	public class BlackberryCrop : BaseCrop
	{
		[Constructable]
		public BlackberryCrop() : this(null) { }

		[Constructable]
		public BlackberryCrop( Mobile sower ) : base( 3272 )
		{
			Movable = false;
			Name = "Blackberry Plant";
			Hue = 0x000;
			Sower = sower;
			Init( this, 15, 3272, 3272, false );
		}

		public override void OnDoubleClick( Mobile from )
		{
			Gather(from, typeof(Blackberry));
		}

		public BlackberryCrop( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			Init( this, 15, 3272, 3272, true );
		}
	}
}