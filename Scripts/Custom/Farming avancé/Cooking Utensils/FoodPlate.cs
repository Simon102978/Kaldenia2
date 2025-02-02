namespace Server.Items
{
	public class FoodPlate : BaseUtensil
	{
		[Constructable]
		public FoodPlate() : base( 0x9D7 )
		{
			this.Weight = 0.1;
			Name = "Assiette";
		}

		public FoodPlate( Serial serial ) : base( serial )
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
