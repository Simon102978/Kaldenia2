using System;
using Server;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using System.Collections.Generic;

namespace Server.Items
{
	public class EnhancedDices : Item
	{
		private DateTime m_NextUse;
		private int m_Count;

		[Constructable]
		public EnhancedDices() : base(0xFA7)
		{
			Name = "Dés à plusieurs faces";
			Weight = 1.0;
		}

		public EnhancedDices(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (from.Alive && from.InRange(this.GetWorldLocation(), 2))
			{
				from.CloseGump(typeof(DiceRollGump));
				from.SendGump(new DiceRollGump(this));
			}
			else
				base.OnDoubleClick(from);
		}

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			base.GetContextMenuEntries(from, list);

			if (from.Alive && from.InRange(this.GetWorldLocation(), 2))
				list.Add(new RollDiceEntry(this));
		}

		public void Roll(Mobile from, int dice, int faces)
		{
			if (!from.InRange(this.GetWorldLocation(), 2))
				return;

			if (m_NextUse < DateTime.UtcNow)
			{
				int sum = 0;
				string text = string.Empty;

				for (int i = 0; i < dice; i++)
				{
					int roll = Utility.Random(faces) + 1;
					text = string.Format("{0}{1}{2}", text, i > 0 ? " " : "", roll);
					sum += roll;
				}

				this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("*{0} lance {1}d{2}: {3} (Total: {4})*", from.Name, dice, faces, text, sum));

				if (m_Count > 0 && DateTime.UtcNow - m_NextUse < TimeSpan.FromSeconds(m_Count))
					m_NextUse = DateTime.UtcNow + TimeSpan.FromSeconds(3);

				if (m_Count++ == 5)
				{
					m_NextUse = DateTime.UtcNow;
					m_Count = 0;
				}
			}
			else
				from.SendLocalizedMessage(501789); // You must wait before trying again.
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		private class RollDiceEntry : ContextMenuEntry
		{
			private EnhancedDices m_Dices;

			public RollDiceEntry(EnhancedDices dices) : base(6234)
			{
				m_Dices = dices;
			}

			public override void OnClick()
			{
				if (m_Dices.Deleted || !Owner.From.Alive || !Owner.From.InRange(m_Dices.GetWorldLocation(), 2))
					return;

				Owner.From.CloseGump(typeof(DiceRollGump));
				Owner.From.SendGump(new DiceRollGump(m_Dices));
			}
		}

		private class DiceRollGump : Gump
		{
			private readonly EnhancedDices m_Dices;

			public DiceRollGump(EnhancedDices dices)
				: this(dices, 1, 6)
			{
			}

			public DiceRollGump(EnhancedDices dices, int dice, int faces)
				: base(60, 36)
			{
				m_Dices = dices;

				AddHtmlLocalized(14, 12, 273, 20, 1075995, 0x7FFF, false, false); // Dés améliorés

				AddPage(0);
				AddBackground(0, 0, 273, 324, 0x13BE);
				AddImageTiled(10, 10, 253, 20, 0xA40);
				AddImageTiled(10, 40, 253, 244, 0xA40);
				AddImageTiled(10, 294, 253, 20, 0xA40);
				AddAlphaRegion(10, 10, 253, 304);
				AddButton(10, 294, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
				AddHtmlLocalized(45, 294, 80, 20, 1060051, 0x7FFF, false, false); // CANCEL
				AddButton(130, 294, 0xFB7, 0xFB9, 1, GumpButtonType.Reply, 0);
				AddHtmlLocalized(165, 294, 80, 20, 1076002, 0x7FFF, false, false); // Roll

				AddHtmlLocalized(14, 50, 120, 20, 1076000, 0x7FFF, false, false); // Number of dice
				AddGroup(0);
				for (int i = 1; i <= 7; i++)
				{
					AddRadio(14, 70 + (i - 1) * 30, 0xD2, 0xD3, dice == i, 1000 + i);
					AddLabel(44, 70 + (i - 1) * 30, 0x481, i.ToString());
				}

				AddHtmlLocalized(130, 50, 120, 20, 1076001, 0x7FFF, false, false); // Number of faces
				AddGroup(1);
				int[] faceOptions = { 4, 6, 8, 10, 12, 20, 100 };
				for (int i = 0; i < faceOptions.Length; i++)
				{
					AddRadio(130, 70 + i * 30, 0xD2, 0xD3, faces == faceOptions[i], faceOptions[i]);
					AddLabel(160, 70 + i * 30, 0x481, faceOptions[i].ToString());
				}
			}

			public override void OnResponse(NetState sender, RelayInfo info)
			{
				if (m_Dices == null || m_Dices.Deleted)
					return;

				if (info.ButtonID == 1)
				{
					int dice = 1;
					int faces = 6;

					for (int i = 0; i < info.Switches.Length; i++)
					{
						int switchID = info.Switches[i];
						if (switchID >= 1001 && switchID <= 1007)
							dice = switchID - 1000;
						else if (switchID == 4 || switchID == 6 || switchID == 8 || switchID == 10 || switchID == 12 || switchID == 20 || switchID == 100)
							faces = switchID;
					}

					m_Dices.Roll(sender.Mobile, dice, faces);
				}
			}
		}
	}
}
