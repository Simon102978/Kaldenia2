using System;
using Server;
using Server.Targeting;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Network;
using Server.Multis;

namespace Server.Custom
{
	public class GolemSpiritWand : Item
	{
		[CommandProperty(AccessLevel.GameMaster)]
		public int Charges { get; set; }

		[Constructable]
		public GolemSpiritWand() : base(0x0DF2)
		{
			Name = "Baguette des esprits";
			Weight = 1.0;
			Layer = Layer.OneHanded;
			Charges = 100;
		}

		public GolemSpiritWand(Serial serial) : base(serial)
		{
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add($"Charges: {Charges}");
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
					if (corpse.Owner is BaseCreature creature)
					{
						CreatureSpirit spirit = new CreatureSpirit(creature);
						if (from.AddToBackpack(spirit))
						{
							from.SendMessage("Vous avez extrait l'esprit du cadavre.");
							corpse.Delete();
							m_Wand.Charges--;
							if (m_Wand.Charges <= 0)
								m_Wand.Delete();
						}
						else
						{
							from.SendMessage("Votre sac est plein.");
							spirit.Delete();
							m_Wand.Charges--;
							if (m_Wand.Charges <= 0)
								m_Wand.Delete();
						}
					}
					else
					{
						from.SendMessage("Ce cadavre ne contient pas d'esprit utilisable.");
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
			writer.Write((int)1); // version
			writer.Write(Charges);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			switch (version)
			{
				case 1:
					Charges = reader.ReadInt();
					break;
				case 0:
					Charges = 100; // Valeur par défaut
					break;
			}
		}
	}

	public class CreatureSpirit : Item
	{
		private int m_Str, m_Dex, m_Int, m_AR;
		private Dictionary<SkillName, double> m_Skills;

		public int Percentage { get; private set; }

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
		public CreatureSpirit(BaseCreature creature) : base(0x186F)
		{
			Name = "Esprit de créature";
			Weight = 0.1;
			m_Skills = new Dictionary<SkillName, double>();
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
				Percentage = 1;
			}
			else
			{
				m_Str = 0;
				m_Dex = 0;
				m_Int = 0;
				m_AR = 0;
				Percentage = 0;
			}
			UpdateHue();
		}

		public CreatureSpirit(Serial serial) : base(serial)
		{
		}

		private void UpdateHue()
		{
			if (Percentage == 1)
				Hue = 0x89F; // Light blue
			else if (Percentage >= 2 && Percentage <= 99)
				Hue = 0x8FD; // Purple
			else if (Percentage == 100)
				Hue = 0x84; // Dark blue
		}

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			base.GetContextMenuEntries(from, list);
			list.Add(new SpiritInfoEntry(from, this));
		}

		private class SpiritInfoEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private CreatureSpirit m_Spirit;

			public SpiritInfoEntry(Mobile from, CreatureSpirit spirit) : base(6200, 3)
			{
				m_From = from;
				m_Spirit = spirit;
			}

			public override void OnClick()
			{
				m_From.SendGump(new SpiritInfoGump(m_Spirit));
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add($"Esprit: [{Percentage}/100]");
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (this.Deleted)
			{
				from.SendMessage("Cet esprit n'existe plus.");
				return;
			}
			from.Target = new InternalSpiritTarget(this);
			from.SendMessage("Sélectionnez un autre esprit à fusionner avec celui-ci.");
		}

		private class InternalSpiritTarget : Target
		{
			private CreatureSpirit m_Spirit;

			public InternalSpiritTarget(CreatureSpirit spirit) : base(1, false, TargetFlags.None)
			{
				m_Spirit = spirit;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is CreatureSpirit targetSpirit)
				{
					if (targetSpirit == m_Spirit)
					{
						from.SendMessage("Vous devez sélectionner un esprit différent.");
						from.Target = new InternalSpiritTarget(m_Spirit);
						return;
					}
					if (m_Spirit.Percentage + targetSpirit.Percentage > 100)
					{
						from.SendMessage("La fusion ne peut pas dépasser 100%.");
						return;
					}
					m_Spirit.Percentage += targetSpirit.Percentage;
					m_Spirit.m_Str += targetSpirit.m_Str;
					m_Spirit.m_Dex += targetSpirit.m_Dex;
					m_Spirit.m_Int += targetSpirit.m_Int;
					m_Spirit.m_AR += targetSpirit.m_AR;
					foreach (var skill in targetSpirit.m_Skills)
					{
						if (m_Spirit.m_Skills.ContainsKey(skill.Key))
							m_Spirit.m_Skills[skill.Key] += skill.Value;
						else
							m_Spirit.m_Skills[skill.Key] = skill.Value;
					}

					m_Spirit.UpdateHue();
					m_Spirit.InvalidateProperties();
					targetSpirit.Delete();


					from.SendMessage("Les esprits ont été fusionnés avec succès.");
				}
				else
				{
					from.SendMessage("Vous devez cibler un autre esprit de créature.");
					from.Target = new InternalSpiritTarget(m_Spirit);
				}
			}
		}

		private class SpiritInfoGump : Gump
		{
			public SpiritInfoGump(CreatureSpirit spirit) : base(250, 50)
			{
				AddPage(0);

				AddBackground(0, 0, 304, 454, 9380);
				AddImage(0, 0, 10000);
				AddImage(204, 0, 10001);
				AddImage(0, 254, 10002);
				AddImage(204, 254, 10003);

				AddHtml(10, 10, 285, 18, "<div align=center><font color=#28453C><u>Force de l'Esprit</u></font></div>", false, false);

				int y = 35;

				AddSpiritInfo(spirit, ref y);
				AddSkills(spirit, ref y);
			}

			private void AddSpiritInfo(CreatureSpirit spirit, ref int y)
			{
				AddLabel(50, y, 28453, "Attributes:");
				y += 20;

				AddLabeledText("Strength:", spirit.GetStrength().ToString(), ref y);
				AddLabeledText("Dexterity:", spirit.GetDexterity().ToString(), ref y);
				AddLabeledText("Intelligence:", spirit.GetIntelligence().ToString(), ref y);
				AddLabeledText("Armor:", spirit.GetAR().ToString(), ref y);
				AddLabeledText("Spirit Percentage:", $"{spirit.Percentage}%", ref y);

				y += 10;
			}

			private void AddSkills(CreatureSpirit spirit, ref int y)
			{
				AddLabel(50, y, 28453, "Skills:");
				y += 20;

				foreach (var skill in spirit.m_Skills)
				{
					AddLabeledText(skill.Key.ToString() + ":", $"{skill.Value:F1}", ref y);
				}
			}

			private void AddLabeledText(string label, string text, ref int y)
			{
				AddLabel(50, y, 1150, label);
				AddLabel(160, y, 28453, text);
				y += 20;
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
			writer.Write(Percentage);
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
			Percentage = reader.ReadInt();
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


