using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Custom;
using Server.Multis;
using static Server.Custom.GolemSpiritWand;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Engines.Quests;

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

		private int m_CurrentHits;

		[CommandProperty(AccessLevel.GameMaster)]
		public int CurrentHits
		{
			get { return m_CurrentHits; }
			set
			{
				m_CurrentHits = Math.Max(0, Math.Min(value, m_MaxHitPoints));
				Delta(MobileDelta.Hits);
				InvalidateProperties();
			}
		}

		/*	[CommandProperty(AccessLevel.GameMaster)]
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
			}*/

		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile SummonMaster
		{
			get { return m_SummonMaster; }
			set
			{
				m_SummonMaster = value;
				if (m_SummonMaster != null)
				{
					ControlMaster = m_SummonMaster;
					Controlled = true;
					ControlTarget = m_SummonMaster;
					ControlOrder = OrderType.Follow;
				}
			}
		}

		private Mobile m_SummonMaster;

		[CommandProperty(AccessLevel.GameMaster)]
		public MiniGolem MiniGolem { get { return m_MiniGolem; } set { m_MiniGolem = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public GolemAsh.AshType AshType { get { return m_AshType; } set { m_AshType = value; } }

		public override double DispelDifficulty => 200.0;

		public GolemZyX(CreatureSpirit spirit, GolemAsh.AshType ashType, int ashQuantity, Mobile owner)
			: base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			SummonMaster = owner;
			m_AshType = ashType;
			m_Spirit = spirit;
			Name = $"Golem de {ashType}";
			Body = 14; // Adjust as needed
			BaseSoundID = 268; // Adjust as needed



			SetStr(spirit.GetStrength());
			SetDex(spirit.GetDexterity());
			SetInt(spirit.GetIntelligence());

			m_MaxHitPoints = ashQuantity * 5; 
			CurrentHits = m_MaxHitPoints;
			SetHits(m_MaxHitPoints);
			SetMana(0);

			Summoned = true;
			SummonMaster = owner;
			ControlOrder = OrderType.Follow;
			ControlSlots = 3;
			Controlled = true;

		



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

		public override bool NoHouseRestrictions { get { return true; } }
		public override bool IsInvulnerable { get { return false; } }
		public override bool IsBondable { get { return false; } }
		public override bool Unprovokable { get { return true; } }
		public override bool CanRummageCorpses { get { return false; } }
		public override bool BardImmune { get { return true; } }

		public override bool DeleteCorpseOnDeath => true;
		public override bool CanBeRenamedBy(Mobile from) => true;
		public override bool IsDispellable => false; // Empêche le golem d'être 




		public override void OnDoubleClick(Mobile from)
		{
			if (from == SummonMaster)
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

			if (SummonMaster != null && Map != null && Map != Map.Internal)
			{
				SummonMaster.SendLocalizedMessage(1006265, Name); // ~1_NAME~ has been killed.
			}

			// Ne pas dissiper le golem à la mort
			Delete();
		}

		public override bool OnBeforeDeath()
		{
			return base.OnBeforeDeath();
		}

		public override bool CanBeControlledBy(Mobile m)
		{
			return m == SummonMaster;
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
				case GolemAsh.AshType.Eau: return 1156;
				case GolemAsh.AshType.Glace: return 1152;
				case GolemAsh.AshType.Poison: return 1193;
				case GolemAsh.AshType.Sang: return 1194;
				case GolemAsh.AshType.Sylvestre: return 1190;
				case GolemAsh.AshType.Terre: return 1175;
				case GolemAsh.AshType.Vent: return -1;
				default: return 0;
			}
		}

		public override void OnHeal(ref int amount, Mobile from)
		{
			amount = 0;
			if (from != null && from.Player)
			{
				
				from.SendMessage("Ce golem ne peut pas être soigné.");
			}
		}

		public virtual bool  CanRegenHits() { return false; }

		

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			if (Deleted || !Alive)
				return;

			CurrentHits -= amount;

			if (CurrentHits <= 0)
			{
				Kill();
			}

			if (m_MiniGolem != null)
			{
				m_MiniGolem.InvalidateProperties();
			}
			{
				if (AshType == GolemAsh.AshType.Glace)
				AttemptParalyze(from);
			}
			{
				if (AshType == GolemAsh.AshType.Poison)
				AttemptPoison(from);
				{
				}
			}
		}

		public override bool CanBeDamaged()
		{
			return true;
		}

		public override int HitsMax
		{
			get { return m_MaxHitPoints; }
		}

		public void AttemptParalyze(Mobile target)
		{
			if (AshType != GolemAsh.AshType.Glace)
				return;

			if (target != null && target.Alive && !target.Paralyzed)
			{
				double skill = Skills[SkillName.Wrestling].Value;
				if (skill / 150.0 > Utility.RandomDouble())
				{
					target.Paralyze(TimeSpan.FromSeconds(3 + skill / 50));
					target.PlaySound(0x204);
					target.FixedEffect(0x376A, 6, 1);
				}
			}
		}

		public void RangedAttack(Mobile target)
		{
			if (AshType != GolemAsh.AshType.Sylvestre)
				return;

			if (target != null && target.Alive && InRange(target, 5))
			{
				Direction = GetDirectionTo(target);
				MovingEffect(target, 0xF42, 7, 1, false, false);
				DoHarmful(target);
				AOS.Damage(target, this, Utility.RandomMinMax(DamageMin, DamageMax), 100, 0, 0, 0, 0);
			}
		}
		public void AttemptPoison(Mobile target)
		{
			if (AshType != GolemAsh.AshType.Poison)
				return;

			if (target != null && target.Alive && !target.Poisoned)
			{
				double skill = Skills[SkillName.Poisoning].Value;
				if (skill / 100.0 > Utility.RandomDouble())
				{
					int level = (int)(skill / 30.0);
					target.ApplyPoison(this, Poison.GetPoison(Math.Min(level, 3)));
					target.PlaySound(0x205);
					target.FixedEffect(0x3779, 1, 10);
				}
			}
		}



		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)1); // version

			writer.Write(m_MiniGolem);
			writer.Write((int)m_AshType);
			writer.Write(m_MaxHitPoints);
			writer.Write(m_CurrentHits);
			writer.Write(m_SummonMaster);
			writer.Write(m_Penalty);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 1:
					m_MiniGolem = reader.ReadItem() as MiniGolem;
					m_AshType = (GolemAsh.AshType)reader.ReadInt();
					m_MaxHitPoints = reader.ReadInt();
					m_CurrentHits = reader.ReadInt();
					m_SummonMaster = reader.ReadMobile();
					m_Penalty = reader.ReadInt();
					break;
				case 0:
					m_MiniGolem = reader.ReadItem() as MiniGolem;
					m_AshType = (GolemAsh.AshType)reader.ReadInt();
					m_MaxHitPoints = reader.ReadInt();
					m_CurrentHits = m_MaxHitPoints;
					break;
			}

			if (version < 1)
			{
				m_SummonMaster = null;
				m_Penalty = 0;
			}

			Summoned = true;
			ControlSlots = 3;
			Controlled = true;

			if (m_SummonMaster != null)
			{
				ControlMaster = m_SummonMaster;
				ControlTarget = m_SummonMaster;
				ControlOrder = OrderType.Follow;
			}
		}
	}






	public class GolemZyXAttributesGump : Gump
	{
		private static readonly int LabelColor = 0x7FFF;

		public GolemZyXAttributesGump(GolemZyX golem) : base(250, 50)
		{
			AddPage(0);

			AddImage(100, 100, 2080);
			AddImage(118, 137, 2081);
			AddImage(118, 207, 2081);
			AddImage(118, 277, 2081);
			AddImage(118, 347, 2083);

			AddHtml(147, 108, 210, 18, String.Format("<center><i>{0}</i></center>", golem.Name), false, false);

			AddButton(240, 77, 2093, 2093, 2, GumpButtonType.Reply, 0);

			AddImage(140, 138, 2091);
			AddImage(140, 335, 2091);

			int pages = 3;
			int page = 0;

			// Page 1: Attributes
			AddPage(++page);

			AddImage(128, 152, 2086);
			AddHtmlLocalized(147, 150, 160, 18, 1049593, 200, false, false); // Attributes

			AddHtmlLocalized(153, 168, 160, 18, 1049578, LabelColor, false, false); // Hits
			AddHtml(280, 168, 75, 18, FormatAttributes(golem.Hits, golem.HitsMax), false, false);

			AddHtmlLocalized(153, 186, 160, 18, 1028335, LabelColor, false, false); // Strength
			AddHtml(320, 186, 35, 18, FormatStat(golem.Str), false, false);

			AddHtmlLocalized(153, 204, 160, 18, 3000113, LabelColor, false, false); // Dexterity
			AddHtml(320, 204, 35, 18, FormatStat(golem.Dex), false, false);

			AddHtmlLocalized(153, 222, 160, 18, 3000112, LabelColor, false, false); // Intelligence
			AddHtml(320, 222, 35, 18, FormatStat(golem.Int), false, false);

			AddHtmlLocalized(153, 240, 160, 18, 1062760, LabelColor, false, false); // Armor
			AddHtml(320, 240, 35, 18, FormatStat(golem.VirtualArmor), false, false);

			AddHtmlLocalized(153, 258, 160, 18, 1061646, LabelColor, false, false); // Damage
			AddHtml(300, 258, 55, 18, FormatDamage(golem.DamageMin, golem.DamageMax), false, false);

			AddHtml(153, 276, 160, 18, "<BASEFONT COLOR=#CCCCCC>Type de dégâts:</BASEFONT>", false, false);
			AddHtml(320, 276, 35, 18, golem.AshType.ToString(), false, false);

			AddHtml(153, 294, 160, 18, "<BASEFONT COLOR=#CCCCCC>Pénalité:</BASEFONT>", false, false);
			AddHtml(320, 294, 35, 18, golem.Penalty.ToString(), false, false);

			AddButton(340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1);
			AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, pages);

			// Page 2: Skills
			AddPage(++page);

			AddImage(128, 152, 2086);
			AddHtmlLocalized(147, 150, 160, 18, 3001030, 200, false, false); // Skills

			int y = 168;
			foreach (SkillName skillName in Enum.GetValues(typeof(SkillName)))
			{
				Skill skill = golem.Skills[skillName];
				if (skill.Base > 0)
				{
					AddHtmlLocalized(153, y, 160, 18, SkillInfo.Table[(int)skillName].Localization, LabelColor, false, false);
					AddHtml(320, y, 35, 18, FormatSkill(golem, skillName), false, false);
					y += 18;
				}
			}

			AddButton(340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1);
			AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1);

			// Page 3: Additional Information (if needed)
			AddPage(++page);

			AddImage(128, 152, 2086);
			AddHtmlLocalized(147, 150, 160, 18, 1049594, 200, false, false); // Informations supplémentaires

			// Add any additional information you want to display here

			AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1);
		}

		private string FormatAttributes(int current, int max)
		{
			return String.Format("{0}/{1}", current, max);
		}

		private string FormatStat(int value)
		{
			return value.ToString();
		}

		private string FormatDamage(int min, int max)
		{
			return String.Format("{0}-{1}", min, max);
		}

		private string FormatSkill(GolemZyX golem, SkillName skillName)
		{
			return golem.Skills[skillName].Base.ToString("F1");
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

		public MiniGolem(GolemZyX golem, GolemAsh.AshType ashType) : base(0x20D7)
		{
			m_Golem = golem;
			m_AshType = ashType;
			Name = $"Mini Golem de {ashType}";
			Hue = GetHueForAshType(ashType);
			LootType = LootType.Blessed;
		}

		public MiniGolem(Serial serial) : base(serial) { }

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			base.GetContextMenuEntries(from, list);
			if (m_Golem != null && from == m_Golem.ControlMaster)
			{
				list.Add(new OpenAttributesGumpEntry(from, m_Golem));
			}
		}

		private class OpenAttributesGumpEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private GolemZyX m_Golem;

			public OpenAttributesGumpEntry(Mobile from, GolemZyX golem) : base(6200, 3)
			{
				m_From = from;
				m_Golem = golem;
			}

			public override void OnClick()
			{
				m_From.SendGump(new GolemZyXAttributesGump(m_Golem));
			}
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (m_Golem != null && from == m_Golem.ControlMaster)
			{
				if (m_Golem.Deleted || !m_Golem.Alive)
				{
					from.SendMessage("Ce golem n'est plus fonctionnel.");
					return;
				}

				if (m_Golem.Map != Map.Internal) // Le golem est matérialisé
				{
					// Dématérialiser le golem
					m_Golem.ControlMaster = null;
					m_Golem.Controlled = false;
					m_Golem.SetControlMaster(from);
					m_Golem.Map = Map.Internal;
					m_Golem.Location = new Point3D(0, 0, 0);

					from.SendMessage("Vous avez dématérialisé le golem.");
				}
				else // Le golem est dématérialisé
				{
					// Matérialiser le golem
					m_Golem.SetControlMaster(from);
					m_Golem.Controlled = true;
					m_Golem.Map = from.Map;
					m_Golem.Location = from.Location;

					from.SendMessage("Vous avez matérialisé le golem.");
				}
			}
			else
			{
				base.OnDoubleClick(from);
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
				case GolemAsh.AshType.Eau: return 1156;
				case GolemAsh.AshType.Glace: return 1152;
				case GolemAsh.AshType.Poison: return 1193;
				case GolemAsh.AshType.Sang: return 1194;
				case GolemAsh.AshType.Sylvestre: return 1190;
				case GolemAsh.AshType.Terre: return 1175;
				case GolemAsh.AshType.Vent: return -1;
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

			switch (version)
			{
				case 0:
					m_Golem = reader.ReadMobile() as GolemZyX;
					m_AshType = (GolemAsh.AshType)reader.ReadInt();
					break;
			}

			if (m_Golem == null || m_Golem.Deleted)
			{
				Delete();
			}
		}
	}
}
