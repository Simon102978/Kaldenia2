using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Prompts;
using Server.Custom;

namespace Server.Gumps
{
    public class GardenRentGump : BaseProjectMGump
    {

        CustomPlayerMobile m_from;
        BaseGarden m_Garden;

        double price = 0;
        public GardenRentGump(CustomPlayerMobile owner, BaseGarden garden)
            : base("Location de Jardin", 400, 225, false)
        {          

            m_from = owner;
            m_Garden = garden;
			int x = XBase + 15;
			int y = YBase + 5;


            int yLine = 0;

          

            double day = (CustomPersistence.ProchainePay  - DateTime.Now).TotalDays;
            price = Math.Round(garden.Price * day / 7) ;

            AddSection(x - 15 , y , 430, 200, "Contrat", $"<h3><basefont color=#ffffff>Louer ce jardin vous coutera {price.ToString("### ##0")} jusqu'au {CustomPersistence.ProchainePay.ToShortDateString()}. Par la suite, le coût sera de {m_Garden.Price.ToString("### ##0")} par semaine.</basefont></h3>");
            AddBackground(x - 15, y + 201, 430, 60, 9270);   

            AddButton(x + 100, y + 212, 1, 1147);
			AddButton(x + 240, y + 212, 0, 1144);
                    


             

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
                        if (m_Garden.Owner == null)
                        {
                            if (int.TryParse(price.ToString(), out int ToPay))
                            {
                                if (Banker.Withdraw(from, ToPay))
                                {
                                    CustomPersistence.LocationJardin += ToPay;
                                    m_Garden.Owner = from;
                                    m_from.SendMessage("Vous êtes maintenant locataire d'un jardin.");

                                }
                                else
                                {
                                    m_from.SendMessage("Vous n'avez pas assez d'or pour faire cette location.");
                                }
                            }
                            else
                            {
                                    m_from.SendMessage("Parsing ?");
                            }
                           
                        }
                        

    
                        
                        break;
                    }
            }
        }

    
	
    }

   
    
     
    
}
