using System;
using Server;
using Server.Targeting;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Custom
{
	public class GolemSpiritWand : Item
	{
		[Constructable]
		public GolemSpiritWand() : base(0x0DF2)
		{
			Name = "Baguette des esprits";
			Weight = 1.0;
			Layer = Layer.OneHanded;
		}

		public GolemSpiritWand(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			from.SendMessage("Ciblez un cadavre pour en extraire l'esprit.");
			from.Target = new InternalTarget(this);
		}

		private class InternalTarget : Target
		{
			private GolemSpiritWand m_Wand;

			public InternalTarget(GolemSpiritWand wand) : base(3, false, TargetFlags.None)
			{
				m_Wand = wand;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is Corpse corpse)
				{
					CreatureSpirit spirit = new CreatureSpirit(corpse);
					if (from.AddToBackpack(spirit))
					{
						from.SendMessage("Vous avez extrait l'esprit du cadavre.");
						corpse.Delete();
					}
					else
					{
						from.SendMessage("Votre sac est plein.");
						spirit.Delete();
					}
				}
				else
				{
					from.SendMessage("Vous devez cibler un cadavre.");
				}
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class CreatureSpirit : Item
	{
		private int m_Str, m_Dex, m_Int, m_AR;
		private Dictionary<SkillName, double> m_Skills;

		public int m_Percentage { get; private set; }

		public int GetStrength() { return m_Str; }
		public int GetDexterity() { return m_Dex; }
		public int GetIntelligence() { return m_Int; }
		public int GetAR() { return m_AR; }

		public double GetSkillValue(SkillName skillName)
		{
			if (m_Skills != null && m_Skills.TryGetValue(skillName, out double value))
				return value;
			return 0.0;
		}

		[Constructable]
		public CreatureSpirit(Corpse corpse) : base(0x186F)
		{
			Name = "Esprit de créature";
			Weight = 0.1;

			m_Skills = new Dictionary<SkillName, double>();

			BaseCreature creature = corpse.Owner as BaseCreature;
			if (creature != null)
			{
				m_Str = creature.Str;
				m_Dex = creature.Dex;
				m_Int = creature.Int;
				m_AR = creature.VirtualArmor;

				for (int i = 0; i < creature.Skills.Length; i++)
				{
					if (creature.Skills[i].Base > 0)
					{
						m_Skills[creature.Skills[i].SkillName] = creature.Skills[i].Base;
					}
				}

				m_Percentage = 1;
			}
			else
			{
				m_Str = 0;
				m_Dex = 0;
				m_Int = 0;
				m_AR = 0;
				m_Percentage = 0;
			}

			UpdateHue();
		}

		public CreatureSpirit(Serial serial) : base(serial)
		{
		}

		private class SpiritInfoEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private CreatureSpirit m_Spirit;

			public SpiritInfoEntry(Mobile from, CreatureSpirit spirit) : base(6103, 3)
			{
				m_From = from;
				m_Spirit = spirit;
			}

			public override void OnClick()
			{
				m_From.SendGump(new SpiritInfoGump(m_Spirit));
			}
		}

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			base.GetContextMenuEntries(from, list);
			list.Add(new SpiritInfoEntry(from, this));
		}

		private void UpdateHue()
		{
			if (m_Percentage == 1)
				Hue = 0x89F; // Light blue
			else if (m_Percentage >= 2 && m_Percentage <= 99)
				Hue = 0x8FD; // Purple
			else if (m_Percentage == 100)
				Hue = 0x84; // Dark blue
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (this.Deleted)
			{
				from.SendMessage("Cet esprit n'existe plus.");
				return;
			}

			from.SendMessage("Ciblez un autre esprit pour les combiner.");
			from.Target = new InternalTarget(this);
		}

		private class InternalTarget : Target
		{
			private CreatureSpirit m_Spirit;

			public InternalTarget(CreatureSpirit spirit) : base(3, false, TargetFlags.None)
			{
				m_Spirit = spirit;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_Spirit.Deleted)
				{
					from.SendMessage("L'esprit source n'existe plus.");
					return;
				}

				if (targeted is CreatureSpirit otherSpirit)
				{
					if (m_Spirit == otherSpirit)
					{
						from.SendMessage("Vous ne pouvez pas combiner un esprit avec lui-même.");
						return;
					}

					if (m_Spirit.m_Percentage + otherSpirit.m_Percentage > 100)
					{
						from.SendMessage("La combinaison dépasserait 100%. Impossible de combiner.");
						return;
					}

					m_Spirit.m_Str = (m_Spirit.m_Str + otherSpirit.m_Str) / 2;
					m_Spirit.m_Dex = (m_Spirit.m_Dex + otherSpirit.m_Dex) / 2;
					m_Spirit.m_Int = (m_Spirit.m_Int + otherSpirit.m_Int) / 2;
					m_Spirit.m_AR = (m_Spirit.m_AR + otherSpirit.m_AR) / 2;
					m_Spirit.m_Percentage += otherSpirit.m_Percentage;

					if (m_Spirit.m_Skills == null)
						m_Spirit.m_Skills = new Dictionary<SkillName, double>();

					if (otherSpirit.m_Skills != null)
					{
						foreach (var skill in otherSpirit.m_Skills)
						{
							if (m_Spirit.m_Skills.ContainsKey(skill.Key))
							{
								m_Spirit.m_Skills[skill.Key] = (m_Spirit.m_Skills[skill.Key] + skill.Value) / 2;
							}
							else
							{
								m_Spirit.m_Skills[skill.Key] = skill.Value;
							}
						}
					}

					m_Spirit.UpdateHue();
					otherSpirit.Delete();

					from.SendMessage($"Les esprits ont été combinés. Nouveau pourcentage : {m_Spirit.m_Percentage}%");
				}
				else
				{
					from.SendMessage("Vous devez cibler un autre esprit.");
				}
			}
		}

		public override void OnAosSingleClick(Mobile from)
		{
			base.OnAosSingleClick(from);
			from.SendGump(new SpiritInfoGump(this));
		}

		private class SpiritInfoGump : Gump
		{
			public SpiritInfoGump(CreatureSpirit spirit) : base(100, 100)
			{
				AddPage(0);
				AddBackground(0, 0, 300, 400, 9380);
				AddHtmlLocalized(20, 20, 260, 20, 1049644, false, false); // Spirit Information

				AddHtml(20, 50, 100, 20, "Strength:", false, false);
				AddHtml(120, 50, 100, 20, spirit.m_Str.ToString(), false, false);

				AddHtml(20, 70, 100, 20, "Dexterity:", false, false);
				AddHtml(120, 70, 100, 20, spirit.m_Dex.ToString(), false, false);

				AddHtml(20, 90, 100, 20, "Intelligence:", false, false);
				AddHtml(120, 90, 100, 20, spirit.m_Int.ToString(), false, false);

				AddHtml(20, 110, 100, 20, "Armor Rating:", false, false);
				AddHtml(120, 110, 100, 20, spirit.m_AR.ToString(), false, false);

				AddHtml(20, 130, 100, 20, "Percentage:", false, false);
				AddHtml(120, 130, 100, 20, $"{spirit.m_Percentage}%", false, false);

				int y = 150;
				if (spirit.m_Skills != null)
				{
					foreach (var skill in spirit.m_Skills)
					{
						AddHtml(20, y, 150, 20, $"{skill.Key}:", false, false);
						AddHtml(170, y, 100, 20, skill.Value.ToString("F1"), false, false);
						y += 20;
					}
				}
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version

			writer.Write(m_Str);
			writer.Write(m_Dex);
			writer.Write(m_Int);
			writer.Write(m_AR);
			writer.Write(m_Percentage);

			writer.Write(m_Skills.Count);
			foreach (var skill in m_Skills)
			{
				writer.Write((int)skill.Key);
				writer.Write(skill.Value);
			}
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			m_Str = reader.ReadInt();
			m_Dex = reader.ReadInt();
			m_Int = reader.ReadInt();
			m_AR = reader.ReadInt();
			m_Percentage = reader.ReadInt();

			m_Skills = new Dictionary<SkillName, double>();
			int count = reader.ReadInt();
			for (int i = 0; i < count; i++)
			{
				SkillName skillName = (SkillName)reader.ReadInt();
				double skillValue = reader.ReadDouble();
				m_Skills[skillName] = skillValue;
			}

			UpdateHue();
		}
	}
}
