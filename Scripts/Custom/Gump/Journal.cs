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

		public CJournalGump(CustomPlayerMobile from, List<JournalEntry> journalEntries, int page) : base("Journal de Mirage", 800, 720, false)
		{
			m_From = from;
			m_JournalEntries = journalEntries;
			m_Page = page;

			AddPage(0);
			AddBackground(0, 0, 800, 800, 5054);

			var count = 0;

			m_JournalEntries = m_JournalEntries.OrderByDescending(x => x.DateCreated).ToList();

			var tempVisible = m_JournalEntries.Where(x => x.Visible);
			var temp = tempVisible.Skip(m_Page * m_ArticlePerPage).Take(m_ArticlePerPage).ToList();

			foreach (var entry in temp)
			{
				void AddParcheminSection(int x, int y, int largeur, int hauteur, string titre)
				{
					AddBackground(x, y, largeur, hauteur, 9300);
					AddHtml(x, y + 1, largeur, 20, String.Concat("<h3><center><basefont color=#000000>", titre, "</basefont></center></h3>"), false, false);
				}


				AddParcheminSection(100, 80 + count * 200, 770, 180, $"{entry.Title}");

				if (m_From.Journaliste || m_From.AccessLevel > AccessLevel.Player)
				{
					var index = m_JournalEntries.IndexOf(entry);
					AddButton(860, 80 + count * 200, 4026, 4028, 100 + index, GumpButtonType.Reply, 0);
				}

				AddHtml(120, 100 + count * 200, 730, 140, $"<CENTER> - {entry.DateCreated} -\n\r{entry.Content}</CENTER>", true, true);
				count++;

			}

			if (m_Page > 0)
				AddButton(400, 760, 4014, 4015, 2, GumpButtonType.Reply, 0);

			var maxPage = Math.Ceiling((double)tempVisible.Count() / m_ArticlePerPage) - 1;
			AddLabel(500, 760, 0x480, $"{m_Page + 1}/{maxPage + 1}");

			if (m_Page < maxPage)
				AddButton(600, 760, 4005, 4006, 3, GumpButtonType.Reply, 0);

			if (m_From.Journaliste || m_From.AccessLevel > AccessLevel.Player)
			{
				AddButton(100, 780, 2117, 2118, 1, GumpButtonType.Reply, 0);
				AddLabel(120, 780, 0x480, "Ajouter un article au journal");
			}
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
				m_From.SendGump(new CJournalGump(m_From, m_JournalEntries, m_Page));
			}
			else if (info.ButtonID == 3)
			{
				m_Page++;
				var tempVisible = m_JournalEntries.Where(x => x.Visible);
				var maxPage = Math.Ceiling((double)tempVisible.Count() / m_ArticlePerPage) - 1;
				if (m_Page > maxPage)
					m_Page = (int)maxPage;

				m_From.SendGump(new CJournalGump(m_From, m_JournalEntries, m_Page));
			}
			else if (info.ButtonID >= 100 && info.ButtonID < 1000)
			{
				if (m_From.Journaliste || m_From.AccessLevel > AccessLevel.Player)
				{

						m_From.SendGump(new CJournalAddArticleGump(m_From, m_JournalEntries,m_JournalEntries[info.ButtonID - 100],0));
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