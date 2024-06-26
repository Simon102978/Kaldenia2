using System;
using Server.Engines.Quests;
using Server.Items;

namespace Server.Items
{
	public class RessourcesStone : Item
	{
		public override string DefaultName
		{
			get { return "Une Pierre de Ressources"; }
		}

		[Constructable]
		public RessourcesStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 1999;
		}

		public override void OnDoubleClick( Mobile from )
		{
			BagOfRessources regBag = new BagOfRessources( 50 );
			BagofTools toolBag = new BagofTools();



			if ( !from.AddToBackpack( regBag ) )
				regBag.Delete();
			if ( !from.AddToBackpack( toolBag ) )
				toolBag.Delete();
		}

		public RessourcesStone( Serial serial ) : base( serial )
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