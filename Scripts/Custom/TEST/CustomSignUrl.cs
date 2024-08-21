using System;
using System.IO;
using System.Xml;
using Server;
using Server.Items;
using Server.Network;
using Server.Gumps;

namespace Server.Items
{
	[Flipable(0xA02A, 0xA02B)]
	public class CustomSign : Item
	{
		private string m_ImageUrl;

		[CommandProperty(AccessLevel.GameMaster)]
		public string ImageUrl
		{
			get { return m_ImageUrl; }
			set
			{
				m_ImageUrl = value;
				InvalidateProperties();
			}
		}

		[Constructable]
		public CustomSign() : base(0xA02A)
		{
			Movable = true;
			Name = "Affiche personnalisée";
		}

		public CustomSign(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (string.IsNullOrEmpty(m_ImageUrl))
			{
				from.SendGump(new UploadImageGump(this));
			}
			else
			{
				from.LaunchBrowser(m_ImageUrl);
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			if (!string.IsNullOrEmpty(m_ImageUrl))
			{
				list.Add("Image uploadée");
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version

			writer.Write(m_ImageUrl);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			m_ImageUrl = reader.ReadString();
		}
	}

	public class UploadImageGump : Gump
	{
		private CustomSign m_Sign;

		public UploadImageGump(CustomSign sign) : base(50, 50)
		{
			m_Sign = sign;

			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;

			AddPage(0);
			AddBackground(0, 0, 400, 200, 9200);
			AddHtml(10, 10, 380, 20, "<center>Uploader une image</center>", false, false);
			AddHtml(10, 40, 380, 20, "<center>Entrez l'URL de l'image :</center>", false, false);
			AddBackground(10, 70, 380, 40, 9350);
			AddTextEntry(15, 80, 370, 20, 0, 0, "");
			AddButton(150, 130, 4005, 4007, 1, GumpButtonType.Reply, 0);
			AddLabel(190, 130, 0x480, "Uploader");
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			if (info.ButtonID == 1)
			{
				string url = info.GetTextEntry(0).Text.Trim();

				if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
					&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
				{
					if (IsValidImageUrl(url))
					{
						m_Sign.ImageUrl = url;
						sender.Mobile.SendMessage("Image uploadée avec succès.");
					}
					else
					{
						sender.Mobile.SendMessage("L'URL fournie ne contient pas une image valide.");
					}
				}
				else
				{
					sender.Mobile.SendMessage("URL invalide. Veuillez fournir une URL valide.");
				}
			}
		}

		private bool IsValidImageUrl(string url)
		{
			try
			{
				var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
				request.Method = "HEAD";
				using (var response = (System.Net.HttpWebResponse)request.GetResponse())
				{
					return response.ContentType.ToLower().StartsWith("image/");
				}
			}
			catch
			{
				return false;
			}
		}
	}
}
