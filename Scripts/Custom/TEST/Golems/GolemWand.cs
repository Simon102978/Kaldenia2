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
			if (from.Skills[SkillName.Inscribe].Base < 50.0)
			{
				from.SendMessage("Vous avez besoin d'au moins 50 en Inscription pour utiliser cette baguette.");
				return;
			}

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
						if (Utility.RandomBool()) // 50% de chance de capturer l'esprit
						{
							CreatureSpirit spirit = new CreatureSpirit(creature);
							if (from.AddToBackpack(spirit))
							{
								from.SendMessage("Vous avez extrait l'esprit du cadavre.");
								corpse.Delete();
								m_Wand.Charges--;
								m_Wand.UpdateProperties(); 
								if (m_Wand.Charges <= 0)
									m_Wand.Delete();
							}
							else
							{
								from.SendMessage("Votre sac est plein.");
								spirit.Delete();
								m_Wand.Charges--;
								m_Wand.UpdateProperties(); 
								if (m_Wand.Charges <= 0)
									m_Wand.Delete();
							}
						}
						else
						{
							from.SendMessage("Vous avez échoué à capturer l'esprit.");
							corpse.Delete();
							m_Wand.Charges--;
							m_Wand.UpdateProperties();
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
		public void UpdateProperties()
		{
			InvalidateProperties();
			Delta(ItemDelta.Update);
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

		[CommandProperty(AccessLevel.GameMaster)]
		public int Percentage { get;  set; }

		public int GetStrength() { return m_Str; }
		public int GetDexterity() { return m_Dex; }
		public int GetIntelligence() { return m_Int; }
		public int GetAR() { return m_AR; }

		public Dictionary<SkillName, double> Skills
		{
			get { return m_Skills; }
			
		}


		public double GetSkillValue(SkillName skillName)
		{
			if (m_Skills != null && m_Skills.TryGetValue(skillName, out double value))
				return value;
			return 0.0;
		}

		[Constructable]
		public CreatureSpirit(BaseCreature creature) : base(0x3198)
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
				Hue = 0; // Light blue
			else if (Percentage >= 2 && Percentage <= 99)
				Hue = 0x20D; // Purple
			else if (Percentage == 100)
				Hue = 0x1F1; // Dark blue
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
					int totalPercentage = m_Spirit.Percentage + targetSpirit.Percentage;
					double ratio = (double)targetSpirit.Percentage / totalPercentage;

					m_Spirit.m_Str = (int)(m_Spirit.m_Str * (1 - ratio) + targetSpirit.m_Str * ratio);
					m_Spirit.m_Dex = (int)(m_Spirit.m_Dex * (1 - ratio) + targetSpirit.m_Dex * ratio);
					m_Spirit.m_Int = (int)(m_Spirit.m_Int * (1 - ratio) + targetSpirit.m_Int * ratio);
					m_Spirit.m_AR = (int)(m_Spirit.m_AR * (1 - ratio) + targetSpirit.m_AR * ratio);

					foreach (var skill in targetSpirit.m_Skills)
					{
						if (m_Spirit.m_Skills.ContainsKey(skill.Key))
							m_Spirit.m_Skills[skill.Key] = m_Spirit.m_Skills[skill.Key] * (1 - ratio) + skill.Value * ratio;
						else
							m_Spirit.m_Skills[skill.Key] = skill.Value * ratio;
					}

					m_Spirit.Percentage += targetSpirit.Percentage;
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

		public class SpiritInfoGump : Gump
		{
			private static readonly int LabelColor = 0x7FFF;

			public SpiritInfoGump(CreatureSpirit spirit) : base(250, 50)
			{
				AddPage(0);

				AddImage(100, 100, 2080);
				AddImage(118, 137, 2081);
				AddImage(118, 207, 2081);
				AddImage(118, 277, 2081);
				AddImage(118, 347, 2083);

				AddHtml(147, 108, 210, 18, "<center><i>Force de votre Esprit</i></center>", false, false);

				AddButton(240, 77, 2093, 2093, 2, GumpButtonType.Reply, 0);

				AddImage(140, 138, 2091);
				AddImage(140, 335, 2091);

				int pages = 2;
				int page = 0;

				// Page 1: Attributes
				AddPage(++page);

				AddImage(128, 152, 2086);
				AddHtmlLocalized(147, 150, 160, 18, 1049593, 200, false, false); // Attributes

				int y = 168;
				AddHtmlLocalized(153, y, 160, 18, 1028335, LabelColor, false, false); // Strength
				AddHtml(320, y, 35, 18, spirit.GetStrength().ToString(), false, false);
				y += 18;

				AddHtmlLocalized(153, y, 160, 18, 3000113, LabelColor, false, false); // Dexterity
				AddHtml(320, y, 35, 18, spirit.GetDexterity().ToString(), false, false);
				y += 18;

				AddHtmlLocalized(153, y, 160, 18, 3000112, LabelColor, false, false); // Intelligence
				AddHtml(320, y, 35, 18, spirit.GetIntelligence().ToString(), false, false);
				y += 18;

				AddHtmlLocalized(153, y, 160, 18, 1062760, LabelColor, false, false); // Armor
				AddHtml(320, y, 35, 18, spirit.GetAR().ToString(), false, false);
				y += 18;

				AddHtml(153, y, 160, 18, "<BASEFONT COLOR=#0x7FFF>% Total:</BASEFONT>", false, false);
				AddHtml(320, y, 35, 18, $"{spirit.Percentage}%", false, false);

				AddButton(340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1);
				AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, pages);

				// Page 2: Skills
				AddPage(++page);

				AddImage(128, 152, 2086);
				AddHtmlLocalized(147, 150, 160, 18, 3001030, 200, false, false); // Skills

				y = 168;
				foreach (var skill in spirit.m_Skills)
				{
					AddHtml(153, y, 160, 18, $"<BASEFONT COLOR=#0x7FFF>{skill.Key}:</BASEFONT>", false, false);
					AddHtml(320, y, 35, 18, $"{skill.Value:F1}", false, false);
					y += 18;
				}

				AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1);
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


