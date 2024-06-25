using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Prompts;

namespace Server.Gumps
{
    public class GardenGump : BaseProjectMGump
    {

        CustomPlayerMobile m_from;
        BaseGarden m_Garden;
        public GardenGump(CustomPlayerMobile owner, BaseGarden garden)
            : base("Jardin", 250, 200, true)
        {          

            m_from = owner;
            m_Garden = garden;
			int x = XBase + 10;
			int y = YBase + 10;

            owner.CloseGump(typeof(GardenGump));

            int yLine = 0;

            if(m_Garden.Owner == null)
            {
                AddHtmlTexte(x +18, y + yLine * 20, 250, 60, $"Propriétaire: Aucun");
            }
            else
            {
                AddHtmlTexte(x +18, y + yLine * 20, 250, 60, $"Propriétaire: {((CustomPlayerMobile)m_Garden.Owner).BaseName}");
            }			
            yLine++;


            AddHtmlTexte(x +18, y + yLine * 20, 250, 60, $"Statut: { (m_Garden.Public ? "Publique" : "Privée") }");
            yLine++;

            if (!m_Garden.Public)
            {
                 AddButtonHtlml(x, y+ yLine * 20,1,"Transfèrer le jardin","#FFFFFF");
                 yLine++; 
            }


            if (m_from.IsStaff())
            {
              AddButtonHtlml(x, y+ yLine * 20,2,"Remettre en Deed","#FFFFFF");
              yLine++; 

              if (m_Garden.Public)
              {
                 AddButtonHtlml(x, y+ yLine * 20,3,"Rendre le jardin privé","#FFFFFF");
                 yLine++; 
              }
              else
              {
                 AddButtonHtlml(x, y+ yLine * 20,3,"Rendre le jardin publique","#FFFFFF");
                 yLine++; 

                 AddButtonHtlml(x, y+ yLine * 20,6,$"Modifier le prix: {m_Garden.Price}","#FFFFFF");
                 yLine++; 
              }

              if (m_Garden.Owner != m_from)
              {
                AddButtonHtlml(x, y+ yLine * 20,4,"Prendre Possession du jardin","#FFFFFF");
                yLine++;

              }

   





            }

            if (m_Garden.Owner == m_from)
            {
                AddButtonHtlml(x, y+ yLine * 20,5,"Abandonné le jardin","#FFFFFF");
                yLine++;
            }
        
                    


             

        }

        public override void OnResponse(NetState state, RelayInfo info) //Function for GumpButtonType.Reply Buttons 
        {

            CustomPlayerMobile from = state.Mobile as CustomPlayerMobile;

            switch (info.ButtonID)
            {
                case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
                    {
                        //Cancel
                      //  from.SendMessage("Your choose not to destroy your garden.");
                        break;
                    }

                case 1: 
                    {
                        from.Target = new TransfertHouse(from,m_Garden);
                        break;
                    }
                 case 2: 
                    {
                        if (m_from.IsStaff())
                        {
                            m_Garden.Redeed(m_from);
                        }
                        break;
                    }
                  case 3: 
                    {
                        if (m_from.IsStaff())
                        {
                            m_Garden.Public = !m_Garden.Public;
                        }
                        break;
                    }
                  case 4: 
                  {

                    if (m_from.IsStaff())
                    {
                        
                        m_Garden.Owner = m_from;
                    }

                    break;
                  }
                  case 5: 
                  {

                    if (m_Garden.Owner == m_from)
                    {
                        
                        m_Garden.Owner = null;
                    }

                    break;
                  }
                  case 6:
                  {
					m_from.Prompt = new AjustPricePrompt(m_from,m_Garden);
					m_from.SendMessage("Veuillez écrire le nouveau montant.");

                    break;
                  }

            }
        }

        private class TransfertHouse : Target
		{
			private CustomPlayerMobile m_Master;
            private BaseGarden m_Garden;

			public TransfertHouse(CustomPlayerMobile master, BaseGarden garden) : base(12, false, TargetFlags.None)
			{
				m_Master = master;
                m_Garden = garden;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is CustomPlayerMobile cp)
				{
					m_Garden.BeginConfirmTransfer(from,cp);
				}			
				else
				{
					from.SendMessage("Vous devez choisir un joueur.");
				}
			}
		}



		private class AjustPricePrompt : Prompt
		{
			private CustomPlayerMobile m_From;
            private BaseGarden m_Garden;


			public AjustPricePrompt(CustomPlayerMobile from, BaseGarden garden)
			{
				m_From = from;
                m_Garden = garden;

			}

			public override void OnCancel(Mobile from)
			{
				m_From.SendGump(new GardenGump(m_From, m_Garden));
			}

			public override void OnResponse(Mobile from, string text)
			{
				int Salaire = 0;

                if (int.TryParse(text, out Salaire))
                {
                    m_Garden.Price = Salaire;
                }			



				m_From.SendGump(new GardenGump(m_From, m_Garden));
			}
		}












    }

   
    
     
    
}
