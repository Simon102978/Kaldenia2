using System;
using Server;
using System.Collections;
using System.Text;
using Server.Gumps;
using Server.Network;
using Server.Spells;
using Server.Mobiles;


namespace Server.Items
{
	public class WildBeehive: Item
	{
		private int m_UsesRemaining = Utility.RandomMinMax( 1, 3 );
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		
		[Constructable]
		public WildBeehive() : base( 0x91A )
		{
			Name = "Ruche";
			Movable = false;
		}
		public override void OnDoubleClick(Mobile from)
		{
			if (from.InRange(this.GetWorldLocation(), 1))
			{
				if (m_UsesRemaining == 1)
				{
					GiveRandomItem(from);
					InvalidateProperties();
					from.SendMessage("Vous retirez le dernier produit, la ruche s'effondre.");
					this.Delete();
				}
				else
				{
					GiveRandomItem(from);
					m_UsesRemaining -= 1;
					InvalidateProperties();
					from.SendMessage("Vous récoltez un produit de la ruche.");
				}
			}
			else
			{
				from.SendMessage("Vous êtes trop loin de la ruche.");
			}
		}

		private void GiveRandomItem(Mobile from)
		{
			int randomChoice = Utility.Random(3);
			int amount = Utility.RandomMinMax(1, 2);

			switch (randomChoice)
			{
				case 0:
					from.AddToBackpack(new HoneyComb(amount));
					from.SendMessage($"Vous obtenez {amount} rayon(s) de miel.");
					break;
				case 1:
					from.AddToBackpack(new Beeswax(amount));
					from.SendMessage($"Vous obtenez {amount} morceau(x) de cire d'abeille.");
					break;
				case 2:
					from.AddToBackpack(new JarHoney(amount));
					from.SendMessage($"Vous obtenez {amount} pot(s) de miel.");
					break;
			}
		}

		public override bool HandlesOnMovement
		{
			get { return true; }
		}

		public WildBeehive( Serial serial ) : base( serial )
		{
		}		

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );

			writer.Write( (int) m_UsesRemaining ); 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_UsesRemaining = (int)reader.ReadInt(); 			
			
		}
	}
}
