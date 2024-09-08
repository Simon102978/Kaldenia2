using Server.ContextMenus;
using Server.Gumps;
using Server.Network;
using System.Collections.Generic;

namespace Server.Items
{
	[FlipableAttribute(0x12AB, 0x12AC, 0x12A6, 0x12A5, 0x12A7, 0x12A8, 0x12AA)]
	public class DecoDeckOfTarot : Item
	{
		[Constructable]
		public DecoDeckOfTarot()
			: base(0x12AB)
		{
			Movable = true;
			Stackable = false;
			Name = "Jeu de Tarot";
		}

		public DecoDeckOfTarot(Serial serial)
			: base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (from.Alive && from.InRange(this.GetWorldLocation(), 2))
			{
				from.CloseGump(typeof(InternalGump));
				from.SendGump(new InternalGump(this));
			}
			else
				base.OnDoubleClick(from);
		}

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			base.GetContextMenuEntries(from, list);

			if (from.Alive && from.InRange(this.GetWorldLocation(), 2))
				list.Add(new UseEntry(this));
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		private class UseEntry : ContextMenuEntry
		{
			private readonly DecoDeckOfTarot m_Deck;

			public UseEntry(DecoDeckOfTarot deck)
				: base(6245)
			{
				m_Deck = deck;
			}

			public override void OnClick()
			{
				Mobile from = Owner.From;
				if (m_Deck.Deleted || !from.Alive || !from.InRange(m_Deck.GetWorldLocation(), 2))
					return;

				from.CloseGump(typeof(InternalGump));
				from.SendGump(new InternalGump(m_Deck));
			}
		}

		private class InternalGump : Gump
		{
			private readonly DecoDeckOfTarot m_Deck;

			public InternalGump(DecoDeckOfTarot deck)
				: base(200, 200)
			{
				m_Deck = deck;

				AddPage(0);

				AddBackground(0, 0, 291, 155, 0x13BE);
				AddImageTiled(5, 6, 280, 20, 0xA40);
				AddHtmlLocalized(9, 8, 280, 20, 1075994, 0x7FFF, false, false); // Fortune Teller
				AddImageTiled(5, 31, 280, 91, 0xA40);
				AddHtmlLocalized(9, 35, 280, 40, 1076025, 0x7FFF, false, false); // Ask your question
				AddImageTiled(9, 55, 270, 20, 0xDB0);
				AddImageTiled(10, 55, 270, 2, 0x23C5);
				AddImageTiled(9, 55, 2, 20, 0x23C3);
				AddImageTiled(9, 75, 270, 2, 0x23C5);
				AddImageTiled(279, 55, 2, 22, 0x23C3);

				AddTextEntry(12, 56, 263, 17, 0xA28, 15, "");

				AddButton(5, 129, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
				AddHtmlLocalized(40, 131, 100, 20, 1060051, 0x7FFF, false, false); // CANCEL

				AddButton(190, 129, 0xFB7, 0xFB8, 1, GumpButtonType.Reply, 0);
				AddHtmlLocalized(225, 131, 100, 20, 1006044, 0x7FFF, false, false); // OK
			}

			public override void OnResponse(NetState sender, RelayInfo info)
			{
				if (m_Deck == null || m_Deck.Deleted)
					return;

				if (info.ButtonID == 1)
				{
					TextRelay text = info.GetTextEntry(15);

					if (text != null && !string.IsNullOrEmpty(text.Text))
					{
						foreach (Mobile m in m_Deck.GetMobilesInRange(4))
						{
							if (m.NetState != null)
							{
								m.CloseGump(typeof(FortuneGump));
								m.SendGump(new FortuneGump(text.Text, sender.Mobile.Name));
							}
						}
					}
					else
						sender.Mobile.SendGump(this);
				}
			}
		}

		private class FortuneGump : Gump
		{
			public FortuneGump(string text, string askerName)
				: base(200, 200)
			{
				AddPage(0);

				AddImage(0, 0, 0x7724);

				int one, two, three;

				one = Utility.RandomMinMax(1, 19);
				two = Utility.RandomMinMax(one + 1, 20);
				three = Utility.RandomMinMax(0, one - 1);

				AddImageTiled(28, 140, 115, 180, 0x7725 + one);
				AddTooltip(GetTooltip(one));
				AddHtmlLocalized(28, 115, 125, 20, 1076079, 0x7FFF, false, false); // The Past
				AddImageTiled(171, 140, 115, 180, 0x7725 + two);
				AddTooltip(GetTooltip(two));
				AddHtmlLocalized(171, 115, 125, 20, 1076081, 0x7FFF, false, false); // The Question
				AddImageTiled(314, 140, 115, 180, 0x7725 + three);
				AddTooltip(GetTooltip(three));
				AddHtmlLocalized(314, 115, 125, 20, 1076080, 0x7FFF, false, false); // The Future

				AddHtml(30, 32, 400, 25, $"{askerName} demande : {text}", true, false);
			}

			private int GetTooltip(int number)
			{
				if (number > 9)
					return 1076015 + number - 10;

				switch (number)
				{
					case 0: return 1076063;
					case 1: return 1076060;
					case 2: return 1076061;
					case 3: return 1076057;
					case 4: return 1076062;
					case 5: return 1076059;
					case 6: return 1076058;
					case 7: return 1076065;
					case 8: return 1076064;
					case 9: return 1076066;
				}

				return 1052009; // I have seen the error of my ways!
			}
		}
	}
}
