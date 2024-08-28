using Server;
using System;
using Server.Gumps;
using Server.Items;
using Server.Network;

public class BoatRentalGump : Gump
{
	private Mobile m_From;
	private BoatRentalNPC m_NPC;

	public BoatRentalGump(Mobile from, BoatRentalNPC npc) : base(50, 50)
	{
		m_From = from;
		m_NPC = npc;

		AddPage(0);
		AddBackground(0, 0, 300, 200, 9380);
		AddLabel(20, 20, 0, "Location de Bateau");
		AddLabel(20, 50, 0, "Durée de location :");
		AddRadio(20, 80, 208, 209, true, 1);
		AddLabel(50, 80, 0, "3 heures (1000 pièces d'or)");
		AddButton(100, 150, 4005, 4007, 1, GumpButtonType.Reply, 0);
		AddLabel(140, 150, 0, "Confirmer");
	}

	public override void OnResponse(NetState sender, RelayInfo info)
	{
		Mobile from = sender.Mobile;

		if (info.ButtonID == 1) // Bouton Confirmer
		{
			if (m_NPC.ActiveRentals.ContainsKey(from))
			{
				from.SendMessage("Vous avez déjà un bateau en location.");
				return;
			}

			m_NPC.StartBoatRental(from);
		}
	
			else
			{
				from.SendMessage("Vous n'avez pas assez d'or pour louer un bateau.");
			}
		}
	}

