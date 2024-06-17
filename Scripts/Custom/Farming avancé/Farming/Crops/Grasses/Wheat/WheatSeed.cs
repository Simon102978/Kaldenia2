namespace Server.Items.Crops
{
	public class WheatSeed : BaseSeed
	{
		public override bool CanGrowGarden{ get{ return true; } }

		[Constructable]
		public WheatSeed() : this( 1 ) 
        {
        }

		[Constructable]
		public WheatSeed( int amount ) : base( 0xF27 )
		{
            Name = "Wheat Seed";
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(WheatSeedling));
		}

		public WheatSeed( Serial serial ) : base( serial )
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