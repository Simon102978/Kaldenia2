using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Prompts;
using Server.Custom;
using System.Collections.Generic;

namespace Server.Gumps
{
    public class ClasseValidationGump : BaseProjectMGump
    {

        CustomPlayerMobile m_from;
        Classe m_Classe;
		private List<int> m_List;
		private int m_Index;

		private int m_Page;
        public ClasseValidationGump(CustomPlayerMobile owner, Classe Classe,List<int> list, int index, int page = 0)
            : base("Changement de Classe", 400, 225, false)
        {          
            m_List = list;
			m_Index = index;
			m_Page = page;


            m_from = owner;
            m_Classe = Classe;
			int x = XBase + 15;
			int y = YBase + 5;


            int yLine = 0;

          


            AddSection(x - 15 , y , 430, 200, "Classe", $"<h3><basefont color=#ffffff>Êtes-vous certain de vouloir évoluer votre Classe vers {Classe.Name}. Cela vous coutera {(Classe.ClasseLvl - owner.Classe.ClasseLvl)} point(s) d'évolution.</basefont></h3>");
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


                        break;
                    }

                case 1: 
                    {
                        if (from.CanEvolveTo(m_Classe))
                        {
                            from.Classe = m_Classe;
                        }                        
                        break;
                    }
            }

        		from.SendGump(new ClasseGump(from, m_List[m_Index],m_List,m_Index));
        }

    
	
    }

   
    
     
    
}
