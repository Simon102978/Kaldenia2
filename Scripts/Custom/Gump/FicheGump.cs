using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using System.Collections.Generic;
using Server.Custom;



namespace Server.Gumps
{
    public class FicheGump : BaseProjectMGump
	{

        private CustomPlayerMobile m_From;
        private CustomPlayerMobile m_GM;

        public FicheGump(CustomPlayerMobile from, CustomPlayerMobile gm)
            : base("Fiche de personnage", 560, 622, false)
        {
            m_From = from;
            m_GM = gm;

			int x = XBase;
			int y = YBase;
			

			//    m_From.Validate(ValidateType.All);
			m_From.InvalidateProperties();

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;


			AddSection(x - 10, y, 250, 180, "Informations");

			AddHtmlTexte(x +10, y + 40, 100, "Nom:");
			AddHtmlTexte(x + 125, y + 40, 150, from.GetBaseName());

			AddHtmlTexte(x + 10, y + 60, 100, "Race:");
			AddHtmlTexte(x + 125, y + 60, 150, from.Race.Name);

			AddHtmlTexte(x + 10, y + 80, 100, "Statut Social:");
			AddHtmlTexte(x + 125, y + 80, 150, from.StatutSocialString());

			AddHtmlTexte(x + 10, y + 100, 100, "Apparence:");
			AddHtmlTexte(x + 125, y + 100, 150, from.Apparence());

			AddHtmlTexte(x + 10, y + 120, 100, "Grandeur:");
			AddHtmlTexte(x + 125, y + 120, 150, from.GrandeurString());

			AddHtmlTexte(x + 10, y + 140, 100, "Grosseur:");
			AddHtmlTexte(x + 125, y + 140, 150, from.GrosseurString());



			//201

			AddSection(x - 10, y+ 181, 250, 155, "Classes");

			AddHtmlTexte(x + 10, y + 221, 150, "Classe: ");


		   AddButtonHtlml(x + 107, y+ 221,1,from.Classe.ToString(),"#FFFFFF");
			


			AddHtmlTexte(x + 10, y + 241, 150, "Métier: ");
			AddButtonHtlml(x + 107,y + 241, 2, from.Metier.ToString(),"#FFFFFF");

			AddHtmlTexte(x + 10, y + 261, 150, "Pts d'évolution: ");
			AddHtmlTexte(x + 125, y + 261, 150, from.CalculePtsEvolution().ToString());


			AddHtmlTexte(x + 10, y + 281, 100, "Niveau:");
			AddHtmlTexte(x + 125, y +281, 150, from.Niveau.ToString());

			AddHtmlTexte(x + 10, y + 301, 150, "Armure: ");
			AddHtmlTexte(x + 125, y + 301, 100, m_From.Armure.ToString());

			// 402

			AddSection(x - 10, y + 337, 250, 135, "Expériences");

			AddHtmlTexte(x + 10, y + 375, 150, "FE Normal:");

			int day = (int)(DateTime.Now - CustomPersistence.Ouverture).TotalDays + 1;

			string couleur = "#FFFFFF";

			if (day * 5 <= m_From.FENormalTotal)
			{
				couleur = "#336699";
			}

			AddHtmlTexteColored(x + 125, y + 375, 100, m_From.FENormalTotal.ToString(), couleur);
	
			AddHtmlTexte(x + 10, y + 395, 150, "FE RP:");		
			AddHtmlTexte(x + 125, y + 395, 100, m_From.FERPTotal.ToString());

			AddHtmlTexte(x + 10, y + 415, 150, "FE Total:");
			AddHtmlTexte(x + 125, y + 415, 100, m_From.FETotal.ToString());
			AddHtmlTexte(x + 10, y + 435, 150, "Heures jouées:");
			AddHtmlTexte(x + 125, y + 435, 100, Math.Round(m_From.Account.TotalGameTime.TotalHours, 2).ToString());

	

			AddSection(x - 10, y + 473, 250, 190, "Statistique");

	


			AddHtmlTexte(x + 10, y + 510, 150, "Force :");

			if (m_From.CanDecreaseStat(StatType.Str))
			{
				AddButton(x + 100, y + 512, 5603, 5607, 300, GumpButtonType.Reply, 0);
			}

			if (m_From.CanIncreaseStat(StatType.Str))
			{
				AddButton(x + 160, y + 512, 5601, 5605, 301, GumpButtonType.Reply, 0);
			}

			AddLabel(x + 130, y + 510, 150, m_From.Str.ToString());

			AddHtmlTexte(x + 10, y + 530, 150, "Dextérité :");

			if (m_From.CanDecreaseStat(StatType.Dex))
			{
				AddButton(x + 100, y + 532, 5603, 5607, 302, GumpButtonType.Reply, 0);
			}

			if (m_From.CanIncreaseStat(StatType.Dex))
			{
			  AddButton(x + 160, y + 532, 5601, 5605, 303, GumpButtonType.Reply, 0);
			}
			AddLabel(x + 130, y + 530, 150, m_From.Dex.ToString());

			AddHtmlTexte(x + 10, y + 550, 150, "Intelligence :");

			if (m_From.CanDecreaseStat(StatType.Int))
			{
			AddButton(x + 100, y + 552, 5603, 5607, 304, GumpButtonType.Reply, 0);
			}

			if (m_From.CanIncreaseStat(StatType.Int))
			{
				AddButton(x + 160, y + 552, 5601, 5605, 305, GumpButtonType.Reply, 0);
			}

			AddLabel(x + 130, y + 550, 150, m_From.Int.ToString());

			AddHtmlTexte(x + 10, y + 570, 150, "à placer :");
			AddLabel(x + 130, y + 570, 150, (225 - m_From.RawStr - m_From.RawDex - m_From.RawInt - m_From.StatAttente).ToString());

			AddHtmlTexte(x + 10, y + 590, 150, "En attente :");
			AddLabel(x + 130, y + 590, 150, m_From.StatAttente.ToString());

			AddHtmlTexte(x + 10, y + 610, 150, "Faim :");
			AddLabel(x + 130, y + 610, 150, m_From.Hunger * 5 + " / 100".ToString());

			AddHtmlTexte(x + 10, y + 630, 150, "Soif :");
			AddLabel(x + 130, y + 630, 150, m_From.Thirst * 5 + " / 100".ToString());

			AddSection(x + 241, y, 359, 452, "Talents");

			int line = 0;

			line = 0;

			AddSection(x + 241, y + 453, 359, 210, "Dévotions");

			foreach (KeyValuePair<MagieType, int> item in m_From.Classe.MagicAffinity)
			{
				AddHtmlTexte(x + 261, y + 493 + line * 25, 150, item.Key.ToString() );
				AddLabel(x + 525, y + 493 + line * 25, 150, item.Value.ToString());		
				line++;
			}

		}

		public override void OnResponse(NetState sender, RelayInfo info)
        {
			if (info.ButtonID == 1)
			{		
 				List<int> list = new List<int>();
                list.Add(m_From.Classe.ClasseID);
             
                m_From.SendGump(new ClasseGump(m_From, m_From.Classe.ClasseID, list, 0));
			}
			else if (info.ButtonID == 2)
			{		
 				List<int> list = new List<int>();
                list.Add(m_From.Metier.MetierID);
             
                m_From.SendGump(new MetierGump(m_From, m_From.Metier.MetierID, list, 0));
			}

			if (info.ButtonID == 300)
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
			}
			
		}
    }
}
