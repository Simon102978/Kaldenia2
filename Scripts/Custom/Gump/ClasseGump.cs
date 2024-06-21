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
    public class ClasseGump : BaseProjectMGump
	{
        private CustomPlayerMobile m_From;
		private Classe m_Classe;
		private List<int> m_List;
		private int m_Index;

        public ClasseGump(CustomPlayerMobile from, int classeId, List<int> list, int index)
            : base("Classes", 560, 622, false)
        {
            m_From = from;
			m_Classe = Classe.GetClasse(classeId);
			m_List = list;
			m_Index = index;

		

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
			AddHtmlTexte(x + 150, y + yLine * 20, 150, m_Classe.Name);
			yLine++;

			AddHtmlTexte(x + 10, y + yLine * 20, 125, "Niveau de classe:");
			AddHtmlTexte(x + 150, y + yLine * 20, 150,  m_Classe.ClasseLvl.ToString());
			yLine++;

			AddHtmlTexte(x + 10, y + yLine * 20, 125, "Niveau Requis:");
			AddHtmlTexte(x + 150,y + yLine * 20, 150, m_Classe.NiveauRequis.ToString());
			yLine++;

			AddHtmlTexte(x + 10, y + yLine * 20, 100, "Armure:");
			AddHtmlTexte(x + 150, y + yLine * 20, 150, m_Classe.Armor.ToString());
			yLine++;

			AddButtonHtlml(x + 10, y + yLine * 20,4,"Index des classes", "#FFFFFF");

			AddSection(x + 295, y, 300, 240, "Évolutions");

			yLine = 2;

			foreach (int item in m_Classe.Evolution)
			{
				Classe evoClasse = Classe.GetClasse(item);

				AddButtonHtlml(x + 315, y + yLine * 20,1000 + evoClasse.ClasseID,evoClasse.Name,"#FFFFFF");

				yLine++;



			}







			string competence = "";

			foreach (KeyValuePair<SkillName, double> item in m_Classe.Skill)
			{
				competence = competence + item.Key.ToString() + " - " + item.Value.ToString() + "\n";
			}
			

			AddSection(x - 10, y + 245, 605, 300, "Compétences", competence);

			AddBackground(x - 10, y + 550, 605, 55, 9270);

			if (m_From.CanEvolveTo(m_Classe) && m_Classe != m_From.Classe)
			{
				AddButtonHtlml(x + 150, y + 568, 3,$"Je veux devenir un {m_Classe.Name}.","#FFFFFF");
			}

	        AddSection(x - 10, y + 610, 605, 50, m_Classe.Name);

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

				m_From.SendGump(new ClasseGump(m_From, m_List[m_Index],m_List,m_Index));
			}
			else if (info.ButtonID == 2)
			{
				m_Index++;
				m_From.SendGump(new ClasseGump(m_From, m_List[m_Index],m_List,m_Index));
			}
			else if (info.ButtonID == 3)
			{
				if (m_From.CanEvolveTo(m_Classe))
				{
					m_From.Classe = m_Classe;
					m_From.SendGump(new ClasseGump(m_From, m_List[m_Index],m_List,m_Index));
				}
				else
				{
					m_From.SendMessage("Vous ne pouvez pas évoluer votre classe.");
					m_From.SendGump(new ClasseGump(m_From, m_List[m_Index],m_List,m_Index));
				}
			}
			else if(info.ButtonID == 4)
			{
				m_From.SendGump(new ClasseIndexGump(m_From));
			}
			else if (info.ButtonID >= 1000)
			{
				int NewClasseId = info.ButtonID - 1000;

				List<int> newList = new List<int>();

				int NewIndex = 0;


					while (NewIndex <= m_Index && m_List.Count > 0)
					{
						newList.Add(m_List[NewIndex]);
						NewIndex++;
					}
				


			

				newList.Add(NewClasseId);
				m_Index++;

				m_From.SendGump(new ClasseGump(m_From, NewClasseId,newList,m_Index));
			}



	/*		if (info.ButtonID >= 100 && info.ButtonID < 200)
			{
				m_From.DecreaseSkills((SkillName)info.ButtonID - 100);

				m_From.SendGump(new FicheGump(m_From, m_GM));
			}
			else if (info.ButtonID >= 200 && info.ButtonID < 300)
			{
				m_From.IncreaseSkills((SkillName)info.ButtonID - 200);

				m_From.SendGump(new FicheGump(m_From, m_GM));
			}
			else if (info.ButtonID == 300)
			{
				m_From.DecreaseStat(StatType.Str);
				m_From.SendGump(new FicheGump(m_From, m_GM));
			}
			else if (info.ButtonID == 301)
			{
				m_From.IncreaseStat(StatType.Str);
				m_From.SendGump(new FicheGump(m_From, m_GM));
			}
			else if (info.ButtonID == 302)
			{
				m_From.DecreaseStat(StatType.Dex);
				m_From.SendGump(new FicheGump(m_From, m_GM));
			}
			else if (info.ButtonID == 303)
			{
				m_From.IncreaseStat(StatType.Dex);
				m_From.SendGump(new FicheGump(m_From, m_GM));
			}
			else if (info.ButtonID == 304)
			{
				m_From.DecreaseStat(StatType.Int);
				m_From.SendGump(new FicheGump(m_From, m_GM));
			}
			else if (info.ButtonID == 305)
			{
				m_From.IncreaseStat(StatType.Int);
				m_From.SendGump(new FicheGump(m_From, m_GM));
			}*/







			/*     switch (info.ButtonID)
				 {



					 case 0:
						 {
							 if (m_GM != null)
								 m_GM.CloseGump(typeof(FicheGump));
							 else
								 m_From.CloseGump(typeof(FicheGump));

							 break;
						 }
					 case 1:
						 {
							 break;
						 }
					 case 2:
						 {
							 if (m_GM != null)
			//                     m_GM.SendGump(new FicheAptitudesGump(m_From, m_GM));
							 else
			  //                   m_From.SendGump(new FicheAptitudesGump(m_From, m_GM));

							 break;
						 }
					 case 3:
						 {
							 if (m_GM != null)
	 //                            m_GM.SendGump(new FicheBeauteGump(m_From, m_GM));
							 else
	   //                          m_From.SendGump(new FicheBeauteGump(m_From, m_GM));

							 break;
						 }
					 case 4:
						 {
							 if (m_GM != null)
	   //                          m_GM.SendGump(new FicheConnaissancesGump(m_From, m_GM));
							 else
		 //                        m_From.SendGump(new FicheConnaissancesGump(m_From, m_GM));

							 break;
						 }
					 case 9:
						 {
							 if (m_GM != null)
	  //                           m_GM.SendGump(new ChangementClasseGump(m_From, m_GM));
							 else
		//                         m_From.SendGump(new ChangementClasseGump(m_From, m_GM));

							 break;
						 }
					 case 10:
						 {
							 XP.Evolve(m_From);
							 m_From.SendGump(new FicheGump(m_From, m_GM));
							 break;
						 }
					 case 11:
						 {
							 m_From.SendGump(new ResetGump(m_From, m_GM, ResetGumpPage.Page1));
							 m_From.CloseGump(typeof(FicheGump));

							 break;
						 }
					 case 12:
						 {
							 m_From.SendGump(new ResetGump(m_From, m_GM, ResetGumpPage.Page2));
							 m_From.CloseGump(typeof(FicheGump));	

							 break;
						 }
					 case 13:
						 {
							 m_From.SendGump(new ResetGump(m_From, m_GM, ResetGumpPage.Page3));
							 m_From.CloseGump(typeof(FicheGump));

							 break;
						 }
					 case 99:
						 {
							 if (m_From.GetRace().Equals("Aucune"))
								 m_From.SendMessage("Vous ne pouvez pas changer vos statistiques");
							 else
								 m_From.SendGump(new ChoixStatGump(m_From, false));
							 break;
						 }*/
			//   }
		}
    }
}
