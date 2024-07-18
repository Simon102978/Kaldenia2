using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Linq;
using Server;
using Server.Commands;
using Server.Targeting;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;

namespace CustomCommands
{
	public class ImageCommands
	{
		private static string DataPath = Path.Combine("Data", "PlayerImages.xml");
		private static string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

		public static void Initialize()
		{
			CommandSystem.Register("Upload", AccessLevel.Player, new CommandEventHandler(Upload_OnCommand));
			CommandSystem.Register("BG", AccessLevel.Player, new CommandEventHandler(BG_OnCommand));

			if (!File.Exists(DataPath))
			{
				XmlDocument doc = new XmlDocument();
				doc.AppendChild(doc.CreateElement("PlayerImages"));
				doc.Save(DataPath);
			}
		}

		[Usage("Upload <url>")]
		[Description("Upload une image liée à votre personnage.")]
		public static void Upload_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;

			if (e.Arguments.Length != 1)
			{
				from.SendMessage("Usage: .upload <url>");
				return;
			}

			string url = e.Arguments[0];

			if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
				&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
			{
				string extension = Path.GetExtension(uriResult.AbsolutePath).ToLower();
				if (!AllowedExtensions.Contains(extension))
				{
					from.SendMessage("Format de fichier non autorisé. Utilisez .jpg, .jpeg, .png, .gif ou .bmp.");
					return;
				}

				if (IsValidImageUrl(url))
				{
					SavePlayerImage(from, url);
					from.SendMessage("Nouvelle image uploadée avec succès.");
				}
				else
				{
					from.SendMessage("L'URL fournie ne contient pas une image valide.");
				}
			}
			else
			{
				from.SendMessage("URL invalide. Veuillez fournir une URL valide.");
			}
		}

		private static bool IsValidImageUrl(string url)
		{
			try
			{
				var request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "HEAD";
				using (var response = (HttpWebResponse)request.GetResponse())
				{
					return response.ContentType.ToLower().StartsWith("image/");
				}
			}
			catch
			{
				return false;
			}
		}

		[Usage("BG")]
		[Description("Affiche l'image liée au joueur ciblé.")]
		public static void BG_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;
			from.Target = new BGTarget();
			from.SendMessage("Qui voulez-vous cibler ?");
		}

		private static void SavePlayerImage(Mobile player, string url)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(DataPath);

			XmlElement root = doc.DocumentElement;
			XmlElement playerNode = root.SelectSingleNode($"Player[@Serial='{player.Serial}']") as XmlElement;

			if (playerNode == null)
			{
				playerNode = doc.CreateElement("Player");
				playerNode.SetAttribute("Serial", player.Serial.ToString());
				root.AppendChild(playerNode);
			}

			playerNode.SetAttribute("ImageURL", url);
			doc.Save(DataPath);
		}

		private static string GetPlayerImage(Mobile player)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(DataPath);

			XmlElement playerNode = doc.DocumentElement.SelectSingleNode($"Player[@Serial='{player.Serial}']") as XmlElement;

			return playerNode?.GetAttribute("ImageURL");
		}

		private class BGTarget : Target
		{
			public BGTarget() : base(12, false, TargetFlags.None) { }

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is PlayerMobile target)
				{
					if (target.NameMod != null || (target is IDeduisable deduisable && deduisable.Deguise))
					{
						from.SendMessage("Aucune image n'est associée à ce joueur.");
						return;
					}

					string imageUrl = GetPlayerImage(target);

					if (string.IsNullOrEmpty(imageUrl))
					{
						from.SendMessage("Aucune image n'est associée à ce joueur.");
						return;
					}

					from.SendGump(new ImageUrlGump(from, target.Name, imageUrl));
				}
				else
				{
					from.SendMessage("Vous devez cibler un joueur.");
				}
			}
		}

		private class ImageUrlGump : Gump
		{
			private Mobile m_From;
			private string m_ImageUrl;

			public ImageUrlGump(Mobile from, string playerName, string imageUrl) : base(50, 50)
			{
				m_From = from;
				m_ImageUrl = imageUrl;

				this.Closable = true;
				this.Disposable = true;
				this.Dragable = true;
				this.Resizable = false;

				AddPage(0);
				AddBackground(0, 0, 400, 200, 9200);
				AddHtml(10, 10, 380, 20, $"<center>Image de {playerName}</center>", false, false);
				AddHtml(10, 40, 380, 60, $"<center>{imageUrl}</center>", false, false);
				AddHtml(10, 110, 380, 40, "<center>Cliquez sur le bouton ci-dessous pour ouvrir l'image dans votre navigateur.</center>", false, false);
				AddButton(150, 160, 4005, 4007, 1, GumpButtonType.Reply, 0);
				AddLabel(190, 160, 0x480, "Ouvrir l'image");
			}

			public override void OnResponse(NetState sender, RelayInfo info)
			{
				if (info.ButtonID == 1)
				{
					m_From.LaunchBrowser(m_ImageUrl);
					m_From.SendMessage("Ouverture de l'image dans votre navigateur...");
				}
			}
		}
	}

	public interface IDeduisable
	{
		bool Deguise { get; }
	}
}
