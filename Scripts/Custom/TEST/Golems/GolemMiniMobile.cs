using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Custom;
using Server.Multis;
using static Server.Custom.GolemSpiritWand;
using System.Collections.Generic;

namespace Server.Custom
{
	public class GolemZyX : BaseCreature
	{
		private int m_Penalty;
		[CommandProperty(AccessLevel.GameMaster)]
		public int Penalty
		{
			get { return m_Penalty; }
			set { m_Penalty = value; }
		}

		private Mobile m_Owner;
		private MiniGolem m_MiniGolem;
		private GolemAsh.AshType m_AshType;
		private int m_MaxHitPoints;
		private CreatureSpirit m_Spirit;

		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile Owner
		{
			get { return m_Owner; }
			set
			{
				m_Owner = value;
				if (m_Owner != null)
				{
					ControlMaster = m_Owner;
					Controlled = true;
					ControlTarget = m_Owner;
					ControlOrder = OrderType.Follow;
				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public MiniGolem MiniGolem { get { return m_MiniGolem; } set { m_MiniGolem = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public GolemAsh.AshType AshType { get { return m_AshType; } set { m_AshType = value; } }

		public override double DispelDifficulty => 200.0;

		public GolemZyX(CreatureSpirit spirit, GolemAsh.AshType ashType, int ashQuantity, Mobile owner)
			: base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Owner = owner;
			m_AshType = ashType;
			m_Spirit = spirit;
			Name = $"{ashType} Golem";
			Body = 14; // Adjust as needed
			BaseSoundID = 268; // Adjust as needed

			SetStr(spirit.GetStrength());
			SetDex(spirit.GetDexterity());
			SetInt(spirit.GetIntelligence());

			m_MaxHitPoints = ashQuantity * 10; // Example: 10 hit points per ash unit
			SetHits(m_MaxHitPoints);
			SetMana(0);

			SetDamage(10, 23);

			SetDamageType(GetDamageType(ashType), 100);

			VirtualArmor = spirit.GetAR();

			// Set skills based on the spirit
			foreach (SkillName skillName in Enum.GetValues(typeof(SkillName)))
			{
				double skillValue = spirit.GetSkillValue(skillName);
				if (skillValue > 0)
				{
					SetSkill(skillName, skillValue);
				}
			}

			Fame = 3500;
			Karma = -3500;

			Hue = GetHueForAshType(ashType);

			m_MiniGolem = new MiniGolem(this, ashType);
			if (owner != null && !owner.Backpack.TryDropItem(owner, m_MiniGolem, false))
			{
				m_MiniGolem.Delete();
			}
		}

		public GolemZyX(Serial serial) : base(serial) { }

		private ResistanceType GetDamageType(GolemAsh.AshType ashType)
		{
			switch (ashType)
			{
				case GolemAsh.AshType.Feu: return ResistanceType.Fire;
				case GolemAsh.AshType.Eau: return ResistanceType.Cold;
				case GolemAsh.AshType.Glace: return ResistanceType.Cold;
				case GolemAsh.AshType.Poison: return ResistanceType.Poison;
				default: return ResistanceType.Physical;
			}
		}

		public override bool IsScaredOfScaryThings { get { return false; } }
		public override bool IsScaryToPets { get { return true; } }
		public override bool AutoDispel { get { return false; } }
		public override bool BleedImmune { get { return true; } }
		public override Poison PoisonImmune { get { return Poison.Lethal; } }

		public override void OnDoubleClick(Mobile from)
		{
			if (from == Owner)
			{
				from.SendGump(new GolemZyXAttributesGump(this));
			}
			else
			{
				base.OnDoubleClick(from);
			}
		}

		public override void OnDeath(Container c)
		{
			base.OnDeath(c);
			if (m_MiniGolem != null && !m_MiniGolem.Deleted)
			{
				m_MiniGolem.Delete();
			}
		}

		public override int GetIdleSound() { return 268; }
		public override int GetAngerSound() { return 267; }
		public override int GetHurtSound() { return 269; }
		public override int GetDeathSound() { return 270; }

		private int GetHueForAshType(GolemAsh.AshType ashType)
		{
			switch (ashType)
			{
				case GolemAsh.AshType.Feu: return 1161;
				case GolemAsh.AshType.Eau: return 1153;
				case GolemAsh.AshType.Glace: return 1152;
				case GolemAsh.AshType.Poison: return 1167;
				case GolemAsh.AshType.Sang: return 1157;
				case GolemAsh.AshType.Sylvestre: return 1171;
				case GolemAsh.AshType.Terre: return 1147;
				case GolemAsh.AshType.Vent: return 1154;
				default: return 0;
			}
		}

		public override void OnHeal(ref int amount, Mobile from)
		{
			amount = 0;
		}

		public override bool CanBeDamaged()
		{
			return true;
		}

		public override int HitsMax
		{
			get { return m_MaxHitPoints; }
		}

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			base.OnDamage(amount, from, willKill);
			if (Hits > m_MaxHitPoints)
			{
				Hits = m_MaxHitPoints;
			}
			if (m_MiniGolem != null)
			{
				m_MiniGolem.InvalidateProperties();
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write(m_MiniGolem);
			writer.Write((int)m_AshType);
			writer.Write(m_MaxHitPoints);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_MiniGolem = reader.ReadItem() as MiniGolem;
			m_AshType = (GolemAsh.AshType)reader.ReadInt();
			m_MaxHitPoints = reader.ReadInt();
		}
	}




	public class GolemZyXAttributesGump : Gump
	{
		public GolemZyXAttributesGump(GolemZyX golem) : base(250, 50)
		{
			AddPage(0);
			AddBackground(0, 0, 250, 355, 5054);
			AddImage(10, 10, 2086);
			AddHtmlLocalized(95, 15, 150, 20, 1049593, 0x80, false, false); // Attributes

			int y = 50;

			AddHtmlLocalized(45, y, 100, 20, 3000111, 0x88, false, false); // Strength
			AddLabel(150, y, 0x481, golem.Str.ToString());
			y += 20;

			AddHtmlLocalized(45, y, 100, 20, 3000112, 0x88, false, false); // Dexterity
			AddLabel(150, y, 0x481, golem.Dex.ToString());
			y += 20;

			AddHtmlLocalized(45, y, 100, 20, 3000113, 0x88, false, false); // Intelligence
			AddLabel(150, y, 0x481, golem.Int.ToString());
			y += 20;

			AddHtmlLocalized(45, y, 100, 20, 3000109, 0x88, false, false); // Hits
			AddLabel(150, y, 0x481, $"{golem.Hits}/{golem.HitsMax}");
			y += 20;

			AddHtmlLocalized(45, y, 100, 20, 1062760, 0x88, false, false); // Armor
			AddLabel(150, y, 0x481, golem.VirtualArmor.ToString());
			y += 20;

			AddHtmlLocalized(45, y, 100, 20, 1061646, 0x88, false, false); // Damage
			AddLabel(150, y, 0x481, $"{golem.DamageMin}-{golem.DamageMax}");
			y += 20;

			AddHtml(45, y, 100, 20, "<BASEFONT COLOR=#CCCCCC>Type de dégâts:</BASEFONT>", false, false);
			AddLabel(150, y, 0x481, golem.AshType.ToString());
			y += 20;

			AddHtml(45, y, 100, 20, "<BASEFONT COLOR=#CCCCCC>Pénalité:</BASEFONT>", false, false);
			AddLabel(150, y, 0x481, golem.Penalty.ToString());
			y += 30;

			AddHtmlLocalized(45, y, 150, 20, 3001016, 0x88, false, false); // Skills
			y += 20;

			foreach (SkillName skillName in Enum.GetValues(typeof(SkillName)))
			{
				Skill skill = golem.Skills[skillName];
				if (skill.Base > 0)
				{
					AddHtmlLocalized(45, y, 100, 20, SkillInfo.Table[(int)skillName].Localization, 0x88, false, false);
					AddLabel(150, y, 0x481, skill.Base.ToString("F1"));
					y += 20;
				}
			}
		}
	}


	public class MiniGolem : Item
	{
		private GolemZyX m_Golem;
		private GolemAsh.AshType m_AshType;

		[CommandProperty(AccessLevel.GameMaster)]
		public GolemZyX Golem { get { return m_Golem; } set { m_Golem = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.GameMaster)]
		public GolemAsh.AshType AshType { get { return m_AshType; } set { m_AshType = value; InvalidateProperties(); } }

		public MiniGolem(GolemZyX golem, GolemAsh.AshType ashType) : base(0x20D9)
		{
			m_Golem = golem;
			m_AshType = ashType;
			Name = $"Mini {ashType} Golem";
			Hue = GetHueForAshType(ashType);
			LootType = LootType.Blessed;
		}

		public MiniGolem(Serial serial) : base(serial) { }

		public override void OnDoubleClick(Mobile from)
		{
			if (m_Golem != null && !m_Golem.Deleted)
			{
				from.SendGump(new GolemZyXAttributesGump(m_Golem));
			}
			else
			{
				from.SendMessage("Ce mini golem n'est plus lié à un golem actif.");
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			if (m_Golem != null && !m_Golem.Deleted)
			{
				list.Add($"Énergie : [{m_Golem.Hits}/{m_Golem.HitsMax}]");
				list.Add("[astral]");
			}
			
		
			else
			{
				list.Add("[Golem inactif]");
			}
		}

		private int GetHueForAshType(GolemAsh.AshType ashType)
		{
			switch (ashType)
			{
				case GolemAsh.AshType.Feu: return 1161;
				case GolemAsh.AshType.Eau: return 1153;
				case GolemAsh.AshType.Glace: return 1152;
				case GolemAsh.AshType.Poison: return 1167;
				case GolemAsh.AshType.Sang: return 1157;
				case GolemAsh.AshType.Sylvestre: return 1171;
				case GolemAsh.AshType.Terre: return 1147;
				case GolemAsh.AshType.Vent: return 1154;
				default: return 0;
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write(m_Golem);
			writer.Write((int)m_AshType);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Golem = reader.ReadMobile() as GolemZyX;
			m_AshType = (GolemAsh.AshType)reader.ReadInt();
		}
	}
}

