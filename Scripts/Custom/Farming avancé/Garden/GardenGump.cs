using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{
    public class GardenDGump : Gump
    {
        public GardenDGump(GardenDestroyer gardendestroyer, Mobile owner)
            : base(150, 75)
        {
            m_GardenDestroyer = gardendestroyer;
            owner.CloseGump(typeof(GardenDGump));
            Closable = false;
            Disposable = false;
            Dragable = true;
            Resizable = false;
            AddPage(0);
            AddBackground(0, 0, 445, 250, 9200);
            AddBackground(10, 10, 425, 160, 3500);
            AddLabel(95, 30, 195, @"* Do you want to destroy your garden? *");
            AddLabel(60, 70, 1359, @"I hope you took heed to my warning, and removed any");
            AddLabel(60, 90, 1359, @"items from your Garden Secure before you decide to");
            AddLabel(60, 110, 1359, @"destroy your ENTIRE garden.");
            AddLabel(107, 205, 172, @"Destroy");
            AddLabel(270, 205, 32, @"Don't Destroy");
            AddButton(115, 180, 4023, 4024, 1, GumpButtonType.Reply, 0);
            AddButton(295, 180, 4017, 4018, 0, GumpButtonType.Reply, 0);
        }

        private GardenDestroyer m_GardenDestroyer;

        public override void OnResponse(NetState state, RelayInfo info) //Function for GumpButtonType.Reply Buttons 
        {

            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
                    {
                        //Cancel
                        from.SendMessage("Your choose not to destroy your garden.");
                        break;
                    }

                case 1: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
                    {

                        //RePack 
                        m_GardenDestroyer.Delete();
                        from.AddToBackpack(new GardenDeed());
                        from.SendMessage("You destroyed your garden, and placed the creation tools back in your back pack.");
                        break;
                    }
            }
        }
    }

   
    
}
