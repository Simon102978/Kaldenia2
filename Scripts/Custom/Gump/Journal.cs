using System;
using Server;
using Server.Commands;
using Server.Gumps;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using Server.Mobiles;



namespace JournalCommand
{
	public class CJournalGump : BaseProjectMGump
	{
		public static string Path => "journalEntries.json";

		public static void Initialize()
		{
			CommandSystem.Register("Journal", AccessLevel.Player, new CommandEventHandler(OnJournalCommand));
		}

		[Usage("Journal")]
		[Description("Ouvre le journal de la ville et permet de lire et d'ajouter des articles.")]
		private static void OnJournalCommand(CommandEventArgs e)
		{
			var pm = e.Mobile as CustomPlayerMobile;
			OpenJournal(pm);
		}

		public static void OpenJournal(CustomPlayerMobile pm)
		{
			List<JournalEntry> journalEntries = new List<JournalEntry>();

			// Charger les articles existants � partir du fichier JSON
			string json = File.ReadAllText(Path);
			if (!string.IsNullOrEmpty(json))
				journalEntries = JsonConvert.DeserializeObject<List<JournalEntry>>(json);

			// Cr�er un nouveau gump pour afficher le journal
			pm.CloseGump(typeof(CJournalGump));
			pm.SendGump(new CJournalGump(pm, journalEntries, 0));
		}

		private CustomPlayerMobile m_From;
		private List<JournalEntry> m_JournalEntries;
		private int m_Page;
		private int m_ArticlePerPage = 3;
		private bool m_AllVisible;

		public CJournalGump(CustomPlayerMobile from, List<JournalEntry> journalEntries, int page, bool allVisible = false) : base("Journal de Mirage", 800, 720, false)
		{
			m_From = from;
			m_JournalEntries = journalEntries;
			m_Page = page;
			m_AllVisible = allVisible;

			AddPage(0);
			AddBackground(0, 0, 800, 800, 5054);

			var count = 0;

			m_JournalEntries = m_JournalEntries.OrderByDescending(x => x.DateCreated).ToList();

			
			var tempVisible = m_JournalEntries.Where(x => x.Visible);
			var temp = tempVisible.Skip(m_Page * m_ArticlePerPage).Take(m_ArticlePerPage).ToList();

			if (allVisible)
			{
				temp = m_JournalEntries.Skip(m_Page * m_ArticlePerPage).Take(m_ArticlePerPage).ToList();
			}
			
			foreach (var entry in temp)
			{
				AddParcheminSection(75, 80 + count * 235, 830, 230, entry , from);
				count++;
			}
			
			AddBackground(75, 785, 830, 55, 9270);


			if (m_Page > 0)
				AddButton(400, 800, 4014, 4015, 2, GumpButtonType.Reply, 0);

			double maxPage = 0;

			if (m_AllVisible)
			{
				maxPage = Math.Ceiling((double)m_JournalEntries.Count() / m_ArticlePerPage) - 1;
			}
			else
			{
				maxPage = Math.Ceiling((double)tempVisible.Count() / m_ArticlePerPage) - 1;
			}

			
			AddLabel(500, 803, 0x480, $"{m_Page + 1}/{maxPage + 1}");

			if (m_Page < maxPage)
				AddButton(600, 800, 4005, 4006, 3, GumpButtonType.Reply, 0);

			if (m_From.Journaliste || m_From.AccessLevel > AccessLevel.Player)
			{
				AddButtonHtlml(100, 803,1,"Ajouter un article au journal","#FFFFFF");

			
			}

			if (m_From.AccessLevel > AccessLevel.Player)
			{
				if (!allVisible)
				{
					AddButtonHtlml(680,803,4,"Voir texte invisible","#FFFFFF");
				}
				else
				{
					AddButtonHtlml(680,803,4,"Voir seulement texte visible","#FFFFFF");
				}
				
			}
		}

		void AddParcheminSection(int x, int y, int largeur, int hauteur, JournalEntry entry, CustomPlayerMobile cm)
		{
		
		 	AddBackground(x, y, largeur, 40, 9270);
			AddBackground(x, y + 42, largeur, hauteur - 42, 9270);
			AddHtmlTexte(x + 12, y + 12, 150,entry.DateCreated.ToShortDateString());

			string couleur = "#000000";

			if (!entry.Visible)
			{
				couleur = "#696969";
			}

			AddTitleWhite(x + 225 , y +12,400, $"<h3><center>{entry.Title}</center></h3>",couleur);
			AddHtmlTexte(x + largeur - 175, y + 12, 165, $"<right>par {entry.WriterName}</right>");
			
			if ((m_From.Journaliste && m_From.Account.ToString() == entry.Account)|| m_From.AccessLevel > AccessLevel.Player)
			{
				var index = m_JournalEntries.IndexOf(entry);
				AddButton(x + largeur - 200, y + 9, 4026, 4028, 100 + index, GumpButtonType.Reply, 0);
			}

			AddHtml(x + 20, y + 55, largeur - 40, hauteur - 70, $"{entry.Content}", true, true);
		}

		void AddTitleWhite(int x, int y, int largeur, string title, string couleur)
		{
			
			int larg = 0;

			AddImage(x, y,1802);
		

			if (largeur < 220)
			{
				AddImage(x + 8, y,1803);
			}
			else
			{
				bool fin = true;
				int n = 0;

				while (fin)
				{
					if (largeur - larg  > 220)
					{
						AddImage(x +larg + 8, y,1803);					
						larg += 216;
						n++;
					}
					else
					{
						int finX = largeur - 220;
						AddImage(x + finX , y,1803);														
						fin = false;
					}
					
					
				}
			}

			AddImage(x + largeur - 8, y,1804);	
			AddHtmlTexteColored(x +10,y,largeur,title,couleur);
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			if (info.ButtonID == 1)
			{
			      if (m_From.Journaliste || m_From.AccessLevel > AccessLevel.Player)
					m_From.SendGump(new CJournalAddArticleGump(m_From, m_JournalEntries, new JournalEntry(m_From.Name,"","",DateTime.Now,true,m_From.Account.ToString())));
			}
			else if (info.ButtonID == 2)
			{
				m_Page--;

				if (m_Page < 0)
					m_Page = 0;
				m_From.SendGump(new CJournalGump(m_From, m_JournalEntries, m_Page,m_AllVisible));
			}
			else if (info.ButtonID == 3)
			{
				m_Page++;

				double maxPage = 0;

				if (m_AllVisible)
				{
					maxPage = Math.Ceiling((double)m_JournalEntries.Count() / m_ArticlePerPage) - 1;
				}
				else
				{
					var tempVisible = m_JournalEntries.Where(x => x.Visible);
					maxPage = Math.Ceiling((double)tempVisible.Count() / m_ArticlePerPage) - 1;
				}		

				if (m_Page > maxPage)
					m_Page = (int)maxPage;

				m_From.SendGump(new CJournalGump(m_From, m_JournalEntries, m_Page,m_AllVisible));
			}
			else if(info.ButtonID == 4)
			{
				m_From.SendGump(new CJournalGump(m_From, m_JournalEntries, 0,!m_AllVisible));
			}
			else if (info.ButtonID >= 100 && info.ButtonID < 1000)
			{
				JournalEntry entry = m_JournalEntries[info.ButtonID - 100];

				if ((m_From.Journaliste && entry.Account.ToString() == m_From.Account.ToString())|| m_From.AccessLevel > AccessLevel.Player)
				{
					m_From.SendGump(new CJournalAddArticleGump(m_From, m_JournalEntries,entry,0));
				}
			}
		}
	}

	public class CJournalAddArticleGump : BaseProjectMGump
	{

		private CustomPlayerMobile m_From;
		private List<JournalEntry> m_JournalEntries;
		private JournalEntry m_Entry;
		private int m_Page;

		public CJournalAddArticleGump(CustomPlayerMobile from, List<JournalEntry> journalEntries, JournalEntry entry, int page = 0) : base("Journal de Mirage", 425, 607, false)
		{
			m_From = from;
			m_JournalEntries = journalEntries;
			m_Entry = entry;
			m_Page = page;	

			string[] Corps = entry.Content.Split(new[] { "\n\r\n\r" }, StringSplitOptions.None);
		
			AddPage(0);

			AddSection(70 , 73, 465, 100,"Titre");
			AddTextEntryBg(84, 120, 420, 25, 0, 10, entry.Title);

			AddSection(70 , 175, 465, 345,"Corps","");
			AddButton(505, 219, 1, 251, 250);
			AddButton(505, 483, 2, 253, 252);

			string corp1 = "";
			string corp2 = "";
			string corp3 = "";

			if (Corps.Length >= 3*page +1)
			{
				corp1 = Corps[3*page];
			}

			if (Corps.Length >= 3*page +2)
			{
				corp2 = Corps[3*page + 1];
			}

			if (Corps.Length >= 3*page +3)
			{
				corp3 = Corps[3*page + 2];
			}

			AddTextEntryBg(84, 219, 420, 95, 0, 11, corp1);
			AddTextEntryBg(84, 313, 420, 95, 0, 12, corp2);
			AddTextEntryBg(84, 407, 420, 95, 0, 13, corp3);
	//		AddTextEntryBg(120, 460, 350, 120, 0, 14, "Paragraphe 4");

			AddSection(70 , 522, 465, 100,"Auteur");
			AddTextEntryBg(84, 569, 420, 25, 0, 14, entry.WriterName);

			AddBackground(70, 623, 232, 100, 9270);
			AddBackground(303, 623, 232, 100, 9270);

			AddButtonLabeled(80, 640,3,0x480,"Sauvegarder l'article");

			if (m_Entry.Visible)
			{
				AddButtonLabeled(80, 665,4,0x480,"Suprimer l'article");
			}
			else
			{
				AddButtonLabeled(80, 665,4,0x480,"Faire réapparaitre l'article");
			}

			AddHtmlTexte(315,640,200,entry.DateCreated.ToString());

			string compte = "";

			if (entry.Account != null)
			{
				compte = entry.Account;
			}


			AddHtmlTexte(315,665,200,$"Compte: {compte}" );
			
					
		//	AddButton(100, 580, 2117, 2118, 1, GumpButtonType.Reply, 0);
		//	AddLabel(120, 580, 0x480, "Ajouter l'article au journal");
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			if (info.ButtonID > 0)
			{
				var title = info.GetTextEntry(10).Text;
				var parag1 = info.GetTextEntry(11).Text;
				var parag2 = info.GetTextEntry(12).Text;
				var parag3 = info.GetTextEntry(13).Text;

				var author = info.GetTextEntry(14).Text;

				List<string> Corps = m_Entry.Content.Split(new[] { "\n\r\n\r" }, StringSplitOptions.None).ToList();

				if (Corps.Count < m_Page * 3 + 1)
				{
					Corps.Add(parag1);
				}
				else
				{
					Corps[ m_Page * 3] = parag1;
				}
		
				if (Corps.Count < m_Page * 3 + 2)
				{
					Corps.Add(parag2);
				}
				else
				{
					Corps[ m_Page * 3 + 1] = parag2;
				}

				
				if (Corps.Count < m_Page * 3 + 3)
				{
					Corps.Add(parag3);
				}
				else
				{
					Corps[ m_Page * 3 + 2] = parag3;
				}	

				m_Entry.Content = 	String.Join("\n\r\n\r", Corps.ToArray());	
				m_Entry.DateCreated = DateTime.Now;
				m_Entry.Title = title;
				m_Entry.WriterName = author;

				if (info.ButtonID == 1)
				{
					int pagi = m_Page;

					if (pagi > 0)
					{
						pagi -= 1;
					}		

					m_Page = pagi;			
				}
				else if (info.ButtonID == 2)
				{
					int pagi = m_Page + 1;

					if (pagi >= 5)
					{
						pagi = 5;
					}

					m_Page = pagi;
				}
				else if(info.ButtonID == 4)
				{
					m_Entry.Visible = !m_Entry.Visible;


				}
						
				if (info.ButtonID == 3 || info.ButtonID == 4)
				{



					if (m_Entry.Account == null || sender.Account.AccessLevel == AccessLevel.Player)
					{
						m_Entry.Account = sender.Account.ToString();
					}				

						// Charger les articles existants � partir du fichier JSON - Fait comme ca, pour eviter que si quelqu'un d'autre a editer entre temp, les changements se perdre.
					string json2 = File.ReadAllText(CJournalGump.Path);

					if (!string.IsNullOrEmpty(json2))
						m_JournalEntries = JsonConvert.DeserializeObject<List<JournalEntry>>(json2);


					JournalEntry customListItem2 = m_JournalEntries.Where(i=> i.Id == m_Entry.Id).FirstOrDefault();
				 	var index = m_JournalEntries.IndexOf(customListItem2);

					if(index != -1)
					{
						m_JournalEntries[index] = m_Entry;
					}
					else
					{
						m_JournalEntries.Add(m_Entry);
					}
						

					var json = JsonConvert.SerializeObject(m_JournalEntries, Formatting.Indented);
					File.WriteAllText(CJournalGump.Path, json);


				}
				else
				{
					m_From.SendGump(new CJournalAddArticleGump(m_From, m_JournalEntries, m_Entry,m_Page));
				}




				

			}

			

		//	m_From.SendGump(new CJournalGump(m_From, m_JournalEntries, 0));
		}
	}

	public class JournalEntry
	{
		public Guid Id { get; set; }
		public bool Visible { get; set; }
		public string WriterName { get; set; }

		public string Account { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime DateCreated { get; set; }

		public JournalEntry(string writerName, string title, string content, DateTime dateCreated, bool visible, string account = "") 
		{
			Id = Guid.NewGuid();
			WriterName = writerName;
			Title = title;
			Content = content;
			DateCreated = dateCreated;
			Visible = visible;
			Account = account;
		}
	}
}