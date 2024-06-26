using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Custom.Packaging.Packages;
using System.Linq;
using Server.Scripts.Commands;

namespace Server.Gumps
{
	public class TestStoneGump : BaseProjectMGump
	{
		private CustomPlayerMobile m_From;

		public TestStoneGump(CustomPlayerMobile from) : base("Test", 700, 520, false)
		{
			m_From = from;

			int x = XBase;
			int y = YBase;

			m_From.InvalidateProperties();

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			int line = 0;
			int lineSpace = 20;
			int column = 0;
			int columnSpace = 251;

			#region Niveau
			AddSection(x - 10 + columnSpace * column, y + lineSpace * line++, 250, lineSpace * 5 - 1, "Niveau");
			line++;
			if (m_From.Niveau > 0)
				AddButton(x + 120 + columnSpace * column, y + lineSpace * line + 2, 5603, 5607, 1, GumpButtonType.Reply, 0);
			if (m_From.Niveau < 50)
				AddButton(x + 180 + columnSpace * column, y + lineSpace * line + 2, 5601, 5605, 2, GumpButtonType.Reply, 0);
			AddHtmlTexte(x + 10 + columnSpace * column, y + lineSpace * line, 150, "Niveau");
			AddHtmlTexte(x + 150 + columnSpace * column, y + lineSpace * line++, 100, m_From.Niveau.ToString());
			#endregion

			column++;
			line = 0;

			#region Ressources
			AddSection(x - 10 + columnSpace * column, y + lineSpace * line++, 250, lineSpace * 5 - 1, "Ressources");
			line++;
			AddButtonHtlml(x + 10 + columnSpace * column, y + lineSpace * line++, 10, "Pièces d'or", "#FFFFFF");
			AddButtonHtlml(x + 10 + columnSpace * column, y + lineSpace * line++, 11, "Matériaux", "#FFFFFF");
			#endregion

			column = 0;
			line++;

			#region Skills
			AddSection(x - 10, y + lineSpace * line++, 610, lineSpace * 23 - 1, "Skills");
			line++;
			var count = 0;

			var skills = from.Skills.OrderBy(f => f.Name).ToList();

			foreach (var skill in skills)
			{
				if (!Skills.IsActive(skill.SkillName))
					continue;

				if (count != 0 && count % 20 == 0)
				{
					column++;
					line = 7;
				}

				AddLabel(x + 30 + column * columnSpace, y + lineSpace * line, 2101, skill.Name);
				if (skill.Base >= 5)
					AddButton(x + 165 + column * columnSpace, y + lineSpace * line + 2, 5603, 5607, 300 + skill.SkillID, GumpButtonType.Reply, 0);
				if (skill.Base > 0)
					AddButton(x + 185 + column * columnSpace, y + lineSpace * line + 2, 5603, 5607, 100 + skill.SkillID, GumpButtonType.Reply, 0);
				AddLabel(x + 210 + column * columnSpace, y + lineSpace * line, 2101, skill.Base.ToString());
				if (skill.Base < skill.Cap && m_From.SkillsTotal < m_From.SkillsCap)
					AddButton(x + 235 + column * columnSpace, y + lineSpace * line + 2, 5601, 5605, 200 + skill.SkillID, GumpButtonType.Reply, 0);
				if (skill.Base <= (skill.Cap - 5) && m_From.SkillsTotal < m_From.SkillsCap)
					AddButton(x + 260 + column * columnSpace, y + lineSpace * line + 2, 5601, 5605, 400 + skill.SkillID, GumpButtonType.Reply, 0);
				line++;
				count++;
			}
			#endregion
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			if (info.ButtonID == 1)
			{
				m_From.Niveau--;
				m_From.SkillsCap = 1000;
				
				m_From.SendGump(new TestStoneGump(m_From));
			}
			else if (info.ButtonID == 2)
			{
				m_From.Niveau++;
				m_From.SkillsCap = 1000;
				m_From.SendGump(new TestStoneGump(m_From));
			}
			else if (info.ButtonID == 10)
			{
				m_From.BankBox.AddItem(new Gold(50000));
				m_From.SendGump(new TestStoneGump(m_From));
			}
			else if (info.ButtonID == 11)
			{
				m_From.BankBox.AddItem(new Materiaux(5000));
				m_From.SendGump(new TestStoneGump(m_From));
			}
			if (info.ButtonID >= 100 && info.ButtonID < 200)
			{
				var skill = m_From.Skills[info.ButtonID - 100];
				if (skill.Base > 0)
					skill.Base -= 1;
				if (skill.SkillName == SkillName.MagicResist)
					m_From.UpdateResistances();
				m_From.SendGump(new TestStoneGump(m_From));
			}
			else if (info.ButtonID >= 200 && info.ButtonID < 300)
			{
				var skill = m_From.Skills[info.ButtonID - 200];
				if (skill.Base < skill.Cap && m_From.SkillsTotal < m_From.SkillsCap) //En dixième de pourcent
					skill.Base += 1;
				if (skill.SkillName == SkillName.MagicResist)
					m_From.UpdateResistances();
				m_From.SendGump(new TestStoneGump(m_From));
			}
			else if (info.ButtonID >= 300 && info.ButtonID < 400)
			{
				var skill = m_From.Skills[info.ButtonID - 300];
				if (skill.Base >= 5)
					skill.Base -= 5;
				if (skill.SkillName == SkillName.MagicResist)
					m_From.UpdateResistances();
				m_From.SendGump(new TestStoneGump(m_From));
			}
			else if (info.ButtonID >= 400 && info.ButtonID < 500)
			{
				var skill = m_From.Skills[info.ButtonID - 400];
				if (skill.Base <= (skill.Cap - 5) && m_From.SkillsTotal < m_From.SkillsCap) //En dixième de pourcent
					skill.Base += 5;
				if (skill.SkillName == SkillName.MagicResist)
					m_From.UpdateResistances();
				m_From.SendGump(new TestStoneGump(m_From));
			}
		}
	}
}
