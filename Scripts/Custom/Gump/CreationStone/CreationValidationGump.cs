using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using System.Reflection;
using Server.HuePickers;
using System.Collections.Generic;
using Server.Engines.Craft;
using Server.Accounting;

namespace Server.Gumps
{
  public class CreationValidationGump : CreationBaseGump
    {

        public CreationValidationGump(CustomPlayerMobile from, CreationPerso creationPerso)
            : base(from, creationPerso, "Validation", true, false)
        {

            int x = XBase;
            int y = YBase;
            int line = 0;
            int scale = 25;

            int space = 115;


            string info = "<h3><basefont color=#FFFFFFF>Nom: " + m_Creation.Name + "\n\nRace: " + m_Creation.Race + "\nDivinité: " + m_Creation.God.Name + "\nSexe: " + (m_Creation.Female ? "Femme" : "Homme") + "\nApparence: " + m_Creation.GetApparence() + "\nGrandeur: " + m_Creation.GetGrandeur() + "\nGrosseur: " + m_Creation.GetGrosseur() + "\n\n<basefont></h3>";

				
			info = info + "Classe: " + Classe.GetClasse(creationPerso.Classe).Name;
			info = info + "\nMetier: " + Classe.GetClasse(creationPerso.Metier).Name;

			info = info + "\n\nForce: " + creationPerso.Str;
			info = info + "\nDextérité: " + creationPerso.Dex;
			info = info + "\nIntelligence: " + creationPerso.Int;

			   if (m_Creation.Reroll != null)
			  {
				  info = info + "\n\nTransfert: " + m_Creation.Reroll.Name + "\nExpériences: " + Math.Round(creationPerso.Reroll.ExperienceNormal  + creationPerso.Reroll.ExperienceRP * 0.5 );
			  }

			AddSection(x - 10, y, 303, 508, "Information", info);

			string context = "Vous allez maintenant être envoyé dans la seconde salle de création, pour une escale avant votre destination finale.\n\nDurant cette escale, profitez bien des marchands présents dans la cité pour regarnir votre garde-robe. \n\nMais prenez garde, le bateau est rempli, vous ne pourrez que transporter ce que vous portez.";

			AddSection(x + 294, y, 304, 508, "Contexte", context);
			AddSection(x - 10, y + 509, 610, 99, "Validation");
			AddButton(x + 265, y + 550, 1, 1147, 1148);
        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
			CustomPlayerMobile from = (CustomPlayerMobile)sender.Mobile;

            if (info.ButtonID == 1)
            {
                m_Creation.Valide();
            }
            else if (info.ButtonID == 1000 || info.ButtonID == 0)
            {
				        Account acc = (Account)from.Account;

						 if (acc.Reroll.Count > 0)
						 {
							 from.SendGump(new CreationRerollGump(from, m_Creation));

						 }
						 else
						 {

						    m_from.SendGump(new CreationGodGump(m_from, m_Creation));
				         }              
			}
        }

     

    }
}
