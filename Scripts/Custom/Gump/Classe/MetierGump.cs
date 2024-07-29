using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using System.Collections.Generic;
using Server.Custom;
using System.Security.Cryptography;

namespace Server.Gumps
{
    public class MetierGump : BaseProjectMGump
	{
        private CustomPlayerMobile m_From;
		private Metier m_Metier;
		private List<int> m_List;
		private int m_Index;

		private int m_Page;

        public MetierGump(CustomPlayerMobile from, int MetierId, List<int> list, int index, int page = 0)
            : base("Metiers", 560, 622, false)
        {
            m_From = from;
			m_Metier = Metier.GetMetier(MetierId);
			m_List = list;
			m_Index = index;
			m_Page = page;

		

			int x = XBase;
			int y = YBase;
			m_From.InvalidateProperties();

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

			AddSection(x - 10, y, 300, 240, "Description");



			int yLine = 2;

			AddHtmlTexte(x +10, y + yLine * 20, 100, "Nom:");
			AddHtmlTexte(x + 150, y + yLine * 20, 150, m_Metier.Name);
			yLine++;

			AddHtmlTexte(x + 10, y + yLine * 20, 125, "Niveau de Metier:");
			AddHtmlTexte(x + 150, y + yLine * 20, 150,  m_Metier.MetierLvl.ToString());
			yLine++;

			AddHtmlTexte(x + 10, y + yLine * 20, 125, "Niveau Requis:");
			AddHtmlTexte(x + 150,y + yLine * 20, 150, m_Metier.NiveauRequis.ToString());
			yLine++;

			AddHtmlTexte(x + 10, y + yLine * 20, 100, "Armure:");
			AddHtmlTexte(x + 150, y + yLine * 20, 150, m_Metier.Armor.ToString());
			yLine++;

			AddButtonHtlml(x + 10, y + yLine * 20,4,"Index des Metiers", "#FFFFFF");

			AddSection(x + 295, y, 300, 240, "Évolutions","");
			AddButton(x +566, y + 43, 5, 251, 250);
			AddButton(x +566, y + 202, 6, 253, 252);

/*			AddSection(70 , 175, 465, 345,"Corps","");
			AddButton(505, 219, 1, 251, 250);
			AddButton(505, 483, 2, 253, 252);
*/
			yLine = 2;

			int MaxCount = m_Metier.Evolution.Count < 8 + m_Page * 8 ? m_Metier.Evolution.Count : 8 + m_Page * 8  ;

			for (int i = m_Page * 8; i < MaxCount; i++)
			{
				Metier evoMetier = Metier.GetMetier(m_Metier.Evolution[i]);

				AddButtonHtlml(x + 315, y + 5 + yLine * 20,1000 + evoMetier.MetierID,evoMetier.Name,"#000000");

				yLine++;
			}

			
			string competence = "";

			competence = m_Metier.SkillsDescription() +  "\n\n" ;

			AddSection(x - 10, y + 245, 605, 300, "Compétences", competence);

			AddBackground(x - 10, y + 550, 605, 55, 9270);

			if (m_Metier == m_From.Metier)
			{
				AddHtmlTexteColored(x + 10, y + 568, 605,$"<center>Il s'agit de votre métier.</center>","#FFFFFF");
			}
			else if (m_From.CanEvolveMetierTo(m_Metier))
			{
				AddButtonHtlml(x + 150, y + 568, 3,$"Je veux devenir un {m_Metier.Name}.","#FFFFFF");
			}
			else if (!m_From.Metier.Evolution.Contains(m_Metier.MetierID))
			{
				AddHtmlTexteColored(x + 10, y + 568, 605,$"<center>Métier Incompatible avec votre métier actuel.</center>","#FFFFFF");
			}
			else if((DateTime.Now - m_From.LastEvolutionMetier).TotalDays < m_From.NombreJourEvolution(m_Metier.MetierLvl) )
			{
				AddHtmlTexteColored(x + 10, y + 568, 605,$"<center>Vous devez attendre {Math.Round(m_From.NombreJourEvolution(m_Metier.MetierLvl) - (DateTime.Now - m_From.LastEvolutionMetier).TotalDays,2)} jours avant de pouvoir changer de métier.</center>","#FFFFFF");

			}	
			else if (m_Metier.MetierLvl - m_From.Metier.MetierLvl > m_From.CalculePtsEvolution())
			{
				AddHtmlTexteColored(x + 150, y + 568, 300,$"<center>Vous n'avez pas assez de points d'évolution.</center>","#FFFFFF");
			}	


	        AddSection(x - 10, y + 610, 605, 50, m_Metier.Name);

			if (m_Index > 0)
			{
				AddButton(x, y + 610, 1, 4506);
			}
			if (m_Index + 1 < m_List.Count)
			{
				AddButton(x + 540, y + 610, 2, 4502);
			} 

		}

		public override void OnResponse(NetState sender, RelayInfo info)
        {
			if (info.ButtonID == 1)
			{		
				m_Index--;

				if (m_Index < 0)
				{
					m_Index = 0;
				}

				m_From.SendGump(new MetierGump(m_From, m_List[m_Index],m_List,m_Index));
			}
			else if (info.ButtonID == 2)
			{
				m_Index++;
				m_From.SendGump(new MetierGump(m_From, m_List[m_Index],m_List,m_Index));
			}
			else if (info.ButtonID == 3)
			{
				if (m_From.CanEvolveMetierTo(m_Metier))
				{
					m_From.SendGump(new MetierValidationGump(m_From,m_Metier,m_List,m_Index));
				//	m_From.Metier = m_Metier;
				//	m_From.SendGump(new MetierGump(m_From, m_List[m_Index],m_List,m_Index));
				}
				else
				{
					m_From.SendMessage("Vous ne pouvez pas évoluer votre Metier.");
					m_From.SendGump(new MetierGump(m_From, m_List[m_Index],m_List,m_Index));
				}
			}
			else if(info.ButtonID == 4)
			{
				m_From.SendGump(new MetierIndexGump(m_From));
			}
			else if (info.ButtonID == 5)
			{
				m_Page--;

				if (m_Page < 0)
					m_Page = 0;

				m_From.SendGump(new MetierGump(m_From, m_List[m_Index],m_List,m_Index, m_Page));
			}
			else if (info.ButtonID == 6)
			{
				m_Page++;

				double maxPage = (m_Metier.Evolution.Count + 8) / 8  - 1;

				if (m_Page > maxPage)
					m_Page = (int)maxPage;

				m_From.SendGump(new MetierGump(m_From, m_List[m_Index],m_List,m_Index, m_Page));
			}
			else if (info.ButtonID >= 1000)
			{
				int NewMetierId = info.ButtonID - 1000;

				List<int> newList = new List<int>();

				int NewIndex = 0;


					while (NewIndex <= m_Index && m_List.Count > 0)
					{
						newList.Add(m_List[NewIndex]);
						NewIndex++;
					}
				


			

				newList.Add(NewMetierId);
				m_Index++;

				m_From.SendGump(new MetierGump(m_From, NewMetierId,newList,m_Index));
			}

		}
    }
}
