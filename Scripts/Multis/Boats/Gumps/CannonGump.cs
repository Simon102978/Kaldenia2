using Server.Items;
using Server.Network;
using System;

namespace Server.Gumps
{
	public class CannonGump : Gump
	{
		public static readonly int LabelColor = 0xFFFFFF;
		public static readonly int GreenHue = 0x40;
		public static readonly int RedHue = 0x22;

		private readonly BaseCannon m_Cannon;
		private readonly Mobile m_From;

		public BaseCannon Cannon => m_Cannon;

		public CannonGump(BaseCannon cannon, Mobile from)
			: base(50, 50)
		{
			m_Cannon = cannon;
			m_From = from;

			AddPage(0);

			AddBackground(0, 0, 300, 300, 9200);
			AddImageTiled(10, 10, 280, 280, 2524);
			AddAlphaRegion(10, 10, 280, 280);

			AddImage(8, 8, 10460);
			AddImage(232, 8, 10460);
			AddImage(8, 232, 10460);
			AddImage(232, 232, 10460);

			AddHtmlLocalized(0, 15, 300, 16, 1149614 + (int)m_Cannon.Position, LabelColor, false, false);

			AddHtmlLocalized(45, 40, 100, 16, 1149626, LabelColor, false, false);  //CLEAN

			bool charged = cannon.Charged;
			bool loaded = cannon.AmmoType != AmmunitionType.Empty;
			bool primed = cannon.Primed;

			if (!charged)
				AddHtmlLocalized(45, 60, 100, 16, 1149630, LabelColor, false, false);  //CHARGE
			else
				AddHtmlLocalized(45, 60, 100, 16, 1149629, LabelColor, false, false);  //REMOVE

			if (!loaded)
				AddHtmlLocalized(45, 80, 100, 16, 1149635, LabelColor, false, false);  //LOAD
			else
				AddHtmlLocalized(45, 80, 100, 16, 1149629, LabelColor, false, false);  //REMOVE

			if (!primed)
				AddHtmlLocalized(45, 100, 100, 16, 1149637, LabelColor, false, false); //PRIME
			else if (cannon.CanLight)
				AddHtmlLocalized(45, 100, 100, 16, 1149638, LabelColor, false, false); //FIRE
			else
				AddHtmlLocalized(45, 100, 100, 16, 1149629, LabelColor, false, false);  //REMOVE

			AddHtmlLocalized(150, 40, 100, 16, cannon.Cleaned ? 1149627 : 1149628, cannon.Cleaned ? GreenHue : RedHue, false, false);
			AddHtmlLocalized(150, 60, 100, 16, charged ? 1149631 : 1149632, charged ? GreenHue : RedHue, false, false);

			if (!loaded)
				AddHtmlLocalized(150, 80, 100, 16, 1149636, RedHue, false, false); //Not Loaded
			else
				AddHtmlLocalized(150, 80, 100, 16, 1114057, AmmoInfo.GetAmmoName(cannon).ToString(), GreenHue, false, false);

			AddHtmlLocalized(150, 100, 100, 16, primed ? 1149640 : 1149639, primed ? GreenHue : RedHue, false, false);

			AddButton(10, 40, 4005, 4007, 1, GumpButtonType.Reply, 0);
			AddButton(10, 60, 4005, 4007, 2, GumpButtonType.Reply, 0);
			AddButton(10, 80, 4005, 4007, 3, GumpButtonType.Reply, 0);
			AddButton(10, 100, 4005, 4007, 4, GumpButtonType.Reply, 0);

			if (!cannon.Actions.ContainsKey(from) || cannon.Actions[from].Count == 0)
				cannon.AddAction(from, 1149653); //You are now operating the cannon.

			int y = 250;
			int count = cannon.Actions[from].Count - 1;

			for (int i = count; i >= Math.Max(0, count - 3); i--)
			{
				int hue = i == count ? 0x35 : 0x482;
				AddHtmlLocalized(10, y, 280, 20, cannon.Actions[from][i], hue, false, false);
				y -= 20;
			}
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;

			if (m_Cannon == null || m_Cannon.Deleted || !from.InRange(m_Cannon.Location, 3))
				return;

			switch (info.ButtonID)
			{
				case 1: m_Cannon.TryClean(from); break;
				case 2:
					if (!m_Cannon.Charged) m_Cannon.TryCharge(from);
					else m_Cannon.RemoveCharge(from);
					break;
				case 3:
					if (m_Cannon.AmmoType == AmmunitionType.Empty) m_Cannon.TryLoad(from);
					else m_Cannon.RemoveLoad(from);
					break;
				case 4:
					if (!m_Cannon.Primed) m_Cannon.TryPrime(from);
					else if (m_Cannon.CanLight) m_Cannon.TryLightFuse(from);
					else m_Cannon.RemovePrime(from);
					break;
			}

			from.SendGump(new CannonGump(m_Cannon, m_From));
		}
	}
}
