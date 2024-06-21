using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using System.Reflection;
using Server.HuePickers;
using System.Collections.Generic;

namespace Server.Gumps
{
	public class ClasseEvoGump : CreationBaseGump
	{
		int m_stade;


		public ClasseEvoGump(CustomPlayerMobile from, CreationPerso creationPerso, int stade = 0)
			: base(from, creationPerso, creationPerso.Title(stade), true, creationPerso.ClasseSelection(stade))
		{

			int x = XBase;
			int y = YBase;
			int line = 0;
			int scale = 20;
			int space = 115;
			m_stade = stade;

            AddSection(x - 10, y, 300, 357, "Classes");
			AddSection(x + 291, y, 309, 357, "Métiers");

            AddSection(x - 10, y + 358, 610, 250, "Description", ClasseDescription());
			
			int LClasse = 0;
			int LMetier = 0;

			Classe aucune = Classe.GetClasse(0);




			foreach (int classeId in aucune.Evolution)
			{
				Classe item = Classe.GetClasse(classeId);

				if (stade == 1 && (( creationPerso.Classe == item.ClasseID) ||  !item.Metier))
				{

				}
				else
				{
					string color = "#ffffff";

					if ((stade == 0 && creationPerso.Classe == item.ClasseID ) || (stade == 1 && creationPerso.Metier == item.ClasseID) )
					{
						color = "#ffcc00";
					}

					if (item.Metier)
					{
						AddButtonHtlml(x + 321, y + 40 + LMetier * scale, 100 + item.ClasseID, 2117, 2118, item.Name,color);
						LMetier++;
					}
					else
					{
						AddButtonHtlml(x + 20, y + 40 + LClasse * scale, 100 + item.ClasseID, 2117, 2118, item.Name, color);
						
						LClasse++;
					}

				}
			}



		}

		public string ClasseDescription()
		{
			string description = "";

			switch (m_stade)
			{
				case 0:
					{
						if (m_Creation.Classe != 0)
						{

							Classe Classe = Server.Classe.GetClasse(m_Creation.Classe );

							description = Classe.Name + "\n\n";

							foreach (KeyValuePair<SkillName,double> item in Classe.Skill)
							{
								description = description + "  -" + item.Key.ToString() + ": " + item.Value+ "\n";
							}

							description = description + "\nArmure: " + Classe.Armor;

							description = description + "\n\nDévotions: \n" ;

							foreach (KeyValuePair<MagieType, int> item in Classe.MagicAffinity)
							{
								description = description + "  -" + item.Key.ToString() + ": " + item.Value.ToString() + "\n";
							}

							return description;
						}
						else
						{
							return description;
						}
					}
				case 1:
					{
						if (m_Creation.Metier != 0)
						{

							Classe Classe = Server.Classe.GetClasse(m_Creation.Metier);

							description = Classe.Name + "\n\n";

							foreach (KeyValuePair<SkillName,double> item in Classe.Skill)
							{
								description = description + "  -" + item.Key.ToString() + ": " + item.Value+ "\n";
							}

							description = description + "\nArmure: " + Classe.Armor;

							description = description + "\n\nDévotions: \n" ;

							foreach (KeyValuePair<MagieType, int> item in Classe.MagicAffinity)
							{
								description = description + "  -" + item.Key.ToString() + ": " + item.Value.ToString() + "\n";
							}

							return description;
						}
						else
						{
							return description;
						}
					}
				
				default:
					break;
			}




			return description;
		}

	
		public override void OnResponse(NetState sender, RelayInfo info)
        {

          CustomPlayerMobile from = (CustomPlayerMobile)sender.Mobile;

          if (from.Deleted || !from.Alive)
            return;





            if (info.ButtonID >= 100 && info.ButtonID < 200)
            {

				switch (m_stade)
					{
						case 0:
							m_Creation.Classe = info.ButtonID - 100;
							break;
						case 1:
							m_Creation.Metier = info.ButtonID - 100;
							break;

						default:
							break;
					}

					from.SendGump(new ClasseEvoGump(m_from, m_Creation, m_stade));
				

			}
			else if (info.ButtonID == 1001)
            {
				if (m_stade == 1)
				{
					from.SendGump(new CreationStatistique(m_from, m_Creation));
				}
				else
				{
					m_stade++;

					from.SendGump(new ClasseEvoGump(m_from, m_Creation, m_stade));
				}
				
				
				
            }
            else if (info.ButtonID == 1000 || info.ButtonID == 0)
            {

				if (m_stade == 0)
				{
					from.SendGump(new InfoGenGump(m_from, m_Creation));
				}
				else
				{
					m_stade--;

					from.SendGump(new ClasseEvoGump(m_from, m_Creation, m_stade));
				}

				
            }
        }
	}
}
