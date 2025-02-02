﻿using System;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Misc;


namespace Server.Gumps
{


	public class CoiffureConfirmationGump : Gump
	{
		CustomPlayerMobile m_from;
		Mobile m_to;
		Coiffure m_coif;
		BarberScissorsBase m_scissor;
		

		public CoiffureConfirmationGump(CustomPlayerMobile from, Mobile m, Coiffure coiffure, BarberScissorsBase scissor)
		   : base(25, 25)
		{
			m_from = from;
			m_to = m;
			m_coif = coiffure;
			m_scissor = scissor;




				int movex = 150;
				int movey = 150;
				AddBackground(36+movex, 25+movey, 369, 170, 9270);
				AddBackground(54+movex, 43+movey, 334, 28, 3000);
			 
			AddLabel(77+movex, 47+movey, 0, from.Name +  " tente de coiffer "+ m.Name+" de cette coiffure : ");
			AddItem(195+movex,80+movey, coiffure.ItemId);
			//AddLabel(250+movex, 90+movey, 2101, coifrelate[i]);
			AddLabel(135+movex, 137+movey, 2101, "Non");
			AddLabel(245+movex, 137+movey, 2101, "Oui");
			AddButton(160+movex,137+movey, 4014, 4016, 0, GumpButtonType.Reply, 0);
			AddButton(270+movex,137+movey, 4014, 4016, 1, GumpButtonType.Reply, 0);
		}



		public override void OnResponse(NetState sender, RelayInfo info)
		{
			if (info.ButtonID == 0)
			{
				m_from.SendMessage(m_to.Name + " refuse de se faire coiffer.");
			}
			else
			{
				if (m_coif.Barbe)
				{
					m_to.FacialHairItemID = m_coif.ItemId;
				}
				else
				{
					m_to.HairItemID = m_coif.ItemId;
				}

				m_to.SendMessage("Vous vous faites coiffer par " + m_from.Name + ".");
				m_from.SendMessage("Vous coiffez " + m_to.Name);

				m_from.PlaySound(0x248);
				m_scissor.UsesRemaining = m_scissor.UsesRemaining - 1;

				if (m_scissor.UsesRemaining < 1)
				{
					m_from.SendMessage("Vous avez brisé votre outil.");
					m_scissor.Delete();
				}
			}
		}
	}


	public class CoiffureGump : BaseProjectMGump
    {
    
        public CustomPlayerMobile m_From;
        public Mobile m_to;
        public int m_Page;
		public bool m_Barbe = false;
		public BarberScissorsBase m_Item;

        public CoiffureGump(CustomPlayerMobile from, Mobile to,int page,BarberScissorsBase itemid, bool barbe = false)
            : base(!barbe ? "Choix de cheveux" : "Choix de Barbe", 560, 622, false)
        {
            m_From = from;
			m_to = to;
			m_Page = page;
			m_Barbe = barbe;
			m_Item = itemid;

			int x = XBase - 2;
			int y = YBase;
			int line = 0;
			int scale = 25;


			Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

			int n = 0;
			int column = 0;
			int showing = 0;
			int shown = 0;
			


			while (n < Coiffure.coiffure.Count)
			{
				Coiffure co = Coiffure.coiffure[n];

				bool skip = false;

				if (m_From.Skills[SkillName.Tailoring].Value < co.SkillRequis)
				{

				}
				else if (co.Barbe && m_Barbe)
				{
					skip = true;
				}
				else if (!co.Barbe && !m_Barbe)
				{
					skip = true;
				}

				if (skip)
				{
					showing++;
				


					if (showing >= 50 * m_Page &&  showing < 50 * (m_Page + 1) )
					{
						AddBackground(x + column * 86, y + line * 86, 85, 85, 9270);

						AddItem(x + 25 + column * 86, y + 25 + line * 86, Coiffure.coiffure[n].ItemId);
						AddButton(x + 30 + column * 86, y + 60 + line * 86, Coiffure.coiffure[n].Id + 1000, 2117, 2118);

						column++;
						shown++;

						if (column == 7)
						{
							column = 0;
							line++;
						}
					}
				}			
				n++;
			}


			while (column != 7 && line != 7)
			{
				AddBackground(x + column * 86, y + line * 86, 85, 85, 9270);

				column++;

				if (column == 7)
				{
					column = 0;
					line++;
				}
			}


			AddBackground(x, y + 602 , 602, 60, 9270);


			if (m_to.Race.Barbe && (!m_to.Female || m_to.Race.FemmeBarbe))
			{
				AddButtonHtlml(x + 275, y + 623, 2, barbe ? "<h3><basefont color=#ffffff>Cheveux</basefont></h3>" : "<h3><basefont color=#ffffff>Barbes</basefont></h3>");

			}

			if (m_Page > 0)
			{
				AddButton(x + 10, y + 610, 3, 4506);
			}
			if ((m_Page < Math.Ceiling((double)Coiffure.coiffure.Count / 50) - 1 ) && !m_Barbe && shown == 49)
			{
				AddButton(x + 540, y + 610, 4, 4502);
			} 


		}
			
        public override void OnResponse(NetState sender, RelayInfo info)
        {		
            int buttonID = info.ButtonID;

			if (buttonID == 2)
			{
				m_From.SendGump(new CoiffureGump(m_From, m_to, 0, m_Item, !m_Barbe));
			}
			else if (buttonID == 3)
			{
				int pagi = m_Page;

				if (pagi > 0)
				{
					pagi -= 1;
				}				

				m_From.SendGump(new CoiffureGump(m_From, m_to, pagi, m_Item, m_Barbe));
			}
			if (buttonID == 4)
			{
					int pagi = m_Page + 1;

					if (pagi > Math.Ceiling((double)Coiffure.coiffure.Count / 50) - 1)
					{
						pagi--;
					}

					m_Page = pagi;

				m_From.SendGump(new CoiffureGump(m_From, m_to, pagi, m_Item, m_Barbe));
			}
			else if(buttonID >= 1000)
			{
				Coiffure co = Coiffure.coiffure[buttonID - 1000];

				if (m_From.Skills[SkillName.Tailoring].Value < co.SkillRequis)
				{
					m_From.SendMessage("Je sais ce que tu tente de faire !");
					m_From.SendGump(new CoiffureGump(m_From, m_to, 0, m_Item, m_Barbe));
				}
				else
				{
					if (m_From == m_to || m_From.AccessLevel > AccessLevel.Player || (m_to is BaseHire bh && bh.GetMaster() == m_From)) 
					{
						if (co.Barbe)
						{
							m_to.FacialHairItemID = co.ItemId;
						}
						else
						{
							m_to.HairItemID = co.ItemId;
						}


						m_From.SendMessage("Vous coiffez.");

						if (m_From.AccessLevel > AccessLevel.Player)
						{
							m_From.SendMessage("Id : " + co.ItemId);
						}

						m_From.PlaySound(0x248);
						m_Item.UsesRemaining = m_Item.UsesRemaining - 1;

						if (m_Item.UsesRemaining < 1)
						{
							m_From.SendMessage("Vous avez brisé votre outil.");
							m_Item.Delete();
						}
					}
					else if(m_to is BaseHire bah && bah.GetMaster() != null)
					{

						bah.GetMaster().SendGump(new CoiffureConfirmationGump(m_From,m_to,co,m_Item));


					}
					else
					{
						m_to.SendGump(new CoiffureConfirmationGump(m_From, m_to, co, m_Item));
					}


				}


			}
        }		
	}	
}
