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
		private static string _Path => "journalEntries.json";

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

			// Charger les articles existants à partir du fichier JSON
			string json = File.ReadAllText(_Path);
			if (!string.IsNullOrEmpty(json))
				journalEntries = JsonConvert.DeserializeObject<List<JournalEntry>>(json);

			// Créer un nouveau gump pour afficher le journal
			pm.CloseGump(typeof(CJournalGump));
			pm.SendGump(new CJournalGump(pm, journalEntries, 0));
		}

		private CustomPlayerMobile m_From;
		private List<JournalEntry> m_JournalEntries;
		private int m_Page;
		private int m_ArticlePerPage = 3;

		public CJournalGump(CustomPlayerMobile from, List<JournalEntry> journalEntries, int page) : base("Journal de Colognan", 800, 720, false)
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
					AddButton(860, 80 + count * 200, 4017, 4018, 100 + index, GumpButtonType.Reply, 0);
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
					m_From.SendGump(new CJournalAddArticleGump(m_From, m_JournalEntries, _Path));
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
			else if (info.ButtonID >= 100)
			{
				if (m_From.Journaliste || m_From.AccessLevel > AccessLevel.Player)
				{
					m_JournalEntries[info.ButtonID - 100].Visible = false;
					var json = JsonConvert.SerializeObject(m_JournalEntries, Formatting.Indented);
					File.WriteAllText(_Path, json);
					m_From.SendGump(new CJournalGump(m_From, m_JournalEntries, m_Page));
				}
			}
		}
	}

	public class CJournalAddArticleGump : BaseProjectMGump
	{
		private string _Path;
		private CustomPlayerMobile m_From;
		private List<JournalEntry> m_JournalEntries;

		public CJournalAddArticleGump(CustomPlayerMobile from, List<JournalEntry> journalEntries, string path) : base("Journal de Colognan", 400, 500, false)
		{
			m_From = from;
			m_JournalEntries = journalEntries;
			_Path = path;

			AddPage(0);
			AddBackground(0, 0, 400, 400, 5054);

			AddHtml(30, 20, 340, 20, "<CENTER><BIG>Journal de Colognan</BIG></CENTER>", false, false);

			AddTextEntry(100, 80, 350, 20, 33, 10, "Titre");
			AddTextEntry(120, 100, 350, 120, 33, 11, "Paragraphe 1");
			AddTextEntry(120, 220, 350, 120, 33, 12, "Paragraphe 2");
			AddTextEntry(120, 340, 350, 120, 33, 13, "Paragraphe 3");
			AddTextEntry(120, 460, 350, 120, 33, 14, "Paragraphe 4");

			AddButton(100, 580, 2117, 2118, 1, GumpButtonType.Reply, 0);
			AddLabel(120, 580, 0x480, "Ajouter l'article au journal");
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			if (info.ButtonID == 1)
			{
				var title = info.GetTextEntry(10).Text;
				var parag1 = info.GetTextEntry(11).Text;
				var parag2 = info.GetTextEntry(12).Text;
				var parag3 = info.GetTextEntry(13).Text;
				var parag4 = info.GetTextEntry(14).Text;

				var content = string.Empty;
				if (!string.IsNullOrEmpty(parag1))
				{
					content += $"{parag1}";
					if (!string.IsNullOrEmpty(parag2) || !string.IsNullOrEmpty(parag3) || !string.IsNullOrEmpty(parag4))
						content += "\n\r\n\r";
				}
				if (!string.IsNullOrEmpty(parag2))
				{
					content += $"{parag2}";
					if (!string.IsNullOrEmpty(parag3) || !string.IsNullOrEmpty(parag4))
						content += "\n\r\n\r";
				}
				if (!string.IsNullOrEmpty(parag3))
				{
					content += $"{parag3}";
					if (!string.IsNullOrEmpty(parag4))
						content += "\n\r\n\r";
				}
				if (!string.IsNullOrEmpty(parag3))
					content += $"{parag4}";

				var newEntry = new JournalEntry(m_From.Name, title, content, DateTime.Now, true);

				m_JournalEntries.Add(newEntry);
				var json = JsonConvert.SerializeObject(m_JournalEntries, Formatting.Indented);
				File.WriteAllText(_Path, json);
			}

			m_From.SendGump(new CJournalGump(m_From, m_JournalEntries, 0));
		}
	}

	public class JournalEntry
	{
		public Guid Id { get; set; }
		public bool Visible { get; set; }
		public string WriterName { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime DateCreated { get; set; }

		public JournalEntry(string writerName, string title, string content, DateTime dateCreated, bool visible)
		{
			Id = Guid.NewGuid();
			WriterName = writerName;
			Title = title;
			Content = content;
			DateCreated = dateCreated;
			Visible = visible;
		}
	}
}