namespace Server.Items.Crops
{
	public class RiceSeed : BaseSeed
	{
        public override bool CanGrowFarm { get { return false; } }
        public override bool CanGrowSwamp { get { return true; } }

		[Constructable]
		public RiceSeed() : this( 1 ) { }

		[Constructable]
		public RiceSeed( int amount ) : base( 0xF27 )
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Rice Seed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Sow(from, typeof(RiceSeedling));
		}

		public RiceSeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}