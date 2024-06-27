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

           
			
			int LClasse = 0;
			int LMetier = 0;

			if (stade == 0)
			{
				AddSection(x - 10, y + 358, 610, 250, "Description", ClasseDescription());
				Classe aucune = Classe.GetClasse(0);




				foreach (int classeId in aucune.Evolution)
				{
					Classe item = Classe.GetClasse(classeId);

					
						string color = "#ffffff";

						if (creationPerso.Classe == item.ClasseID  )
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
			if (stade == 1)
			{
				AddSection(x - 10, y + 358, 610, 250, "Description", MetierDescription());
				Metier aucune = Metier.GetMetier(0);




				foreach (int MetierId in aucune.Evolution)
				{
						Metier item = Metier.GetMetier(MetierId);

						if (!item.ClasseIncompatible.Contains(m_Creation.Classe))
						{
							string color = "#ffffff";

							if (creationPerso.Metier == item.MetierID )
							{
								color = "#ffcc00";
							}

							AddButtonHtlml(x + 321, y + 40 + LMetier * scale, 100 + item.MetierID, 2117, 2118, item.Name,color);
							LMetier++;
						}

				}
			}
			


			



		}

		public string ClasseDescription()
		{
			string description = "";

			if (m_Creation.Classe != 0)
			{

			 return Server.Classe.GetClasse(m_Creation.Classe ).ClasseDescription();
			}
			else
			{
				return description;
			}





		}

		public string MetierDescription()
		{
			string description = "";


						if (m_Creation.Metier != 0)
						{

							return Server.Metier.GetMetier(m_Creation.Metier).MetierDescription();
						}
						else
						{
							return description;
						}



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
