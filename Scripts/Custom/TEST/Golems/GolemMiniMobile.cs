using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Multis;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Custom
{
	public static class GolemAsh
	{
		public enum AshType
		{
			Feu,
			Eau,
			Glace,
			Poison,
			Sang,
			Sylvestre,
			Terre,
			Vent
		}

		public static AshType GetAshTypeFromAsh(BaseGolemAsh ash)
		{
			if (ash is GolemCendreFeu) return AshType.Feu;
			if (ash is GolemCendreEau) return AshType.Eau;
			if (ash is GolemCendreGlace) return AshType.Glace;
			if (ash is GolemCendrePoison) return AshType.Poison;
			if (ash is GolemCendreSang) return AshType.Sang;
			if (ash is GolemCendreSylvestre) return AshType.Sylvestre;
			if (ash is GolemCendreTerre) return AshType.Terre;
			if (ash is GolemCendreVent) return AshType.Vent;

			throw new ArgumentException("Type de cendre inconnu");
		}
	}

	public class GolemZyX : BaseCreature
	{
		private int m_Penalty;
		private MiniGolem m_MiniGolem;
		private GolemAsh.AshType m_AshType;
		private int m_MaxHitPoints;
		private CreatureSpirit m_Spirit;
		private Mobile m_SummonMaster;
		private bool m_IsMaterialized;
		public bool MaterializationInProgress { get; private set; }



		[CommandProperty(AccessLevel.GameMaster)]
		public int Penalty
		{
			get { return m_Penalty; }
			set { m_Penalty = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int CurrentHits
		{
			get { return Hits; }
			set
			{
				Hits = Math.Max(0, Math.Min(value, m_MaxHitPoints));
				Delta(MobileDelta.Hits);
				InvalidateProperties();
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile SummonMaster
		{
			get { return m_SummonMaster; }
			set
			{
				if (m_SummonMaster != value)
				{
					if (m_SummonMaster != null)
					{
						//m_SummonMaster.Followers -= ControlSlots;
					}

					m_SummonMaster = value;

					if (m_SummonMaster != null)
					{
						ControlMaster = m_SummonMaster;
						Controlled = true;
						ControlTarget = m_SummonMaster;
						ControlOrder = OrderType.Come;
						//m_SummonMaster.Followers += ControlSlots;
					}
				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public MiniGolem MiniGolem { get { return m_MiniGolem; } set { m_MiniGolem = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public GolemAsh.AshType AshType { get { return m_AshType; } set { m_AshType = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public CreatureSpirit Spirit { get { return m_Spirit; } set { m_Spirit = value; } }

		private int m_StoredHits;

		[CommandProperty(AccessLevel.GameMaster)]
		public int StoredHits
		{
			get { return m_StoredHits; }
			set { m_StoredHits = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool IsMaterialized
		{
			get { return m_IsMaterialized; }
			set
			{
				if (m_IsMaterialized != value)
				{
					if (value)
					{
						if (!MaterializationInProgress)
						{
							MaterializationInProgress = true;
							Materialize(SummonMaster);
						}
					}
					else
					{
						Dematerialize();
					}
				}
			}
		}


		public GolemZyX(CreatureSpirit spirit, GolemAsh.AshType ashType, int ashQuantity, Mobile owner)
			: base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			SummonMaster = owner;
			m_AshType = ashType;
			m_Spirit = spirit;
			Name = $"Golem de {ashType}";
			Body = 14;
			BaseSoundID = 268;
			MaterializationInProgress = false;
			SetStr(spirit.GetStrength());
			SetDex(spirit.GetDexterity());
			SetInt(spirit.GetIntelligence());

			m_MaxHitPoints = ashQuantity * 3;
			SetHits(m_MaxHitPoints);
			SetMana(0);

			Summoned = false;
			ControlSlots = 0;
			SetControlMaster(owner);
			Controlled = true;
			ControlTarget = owner;
			ControlOrder = OrderType.Come;

			SetDamage(spirit.GetDamageMin() +2, spirit.GetDamageMax() +3); 
			SetDamageType(GetDamageType(ashType), 100);
			VirtualArmor = spirit.GetAR() +5;


			SetResistance(ResistanceType.Physical, spirit.GetPhysicalResistance());
			SetResistance(ResistanceType.Fire, spirit.GetFireResistance());
			SetResistance(ResistanceType.Cold, spirit.GetColdResistance());
			SetResistance(ResistanceType.Poison, spirit.GetPoisonResistance());
			SetResistance(ResistanceType.Energy, spirit.GetEnergyResistance());
			foreach (SkillName skillName in Enum.GetValues(typeof(SkillName)))
			{
				double skillValue = spirit.GetSkillValue(skillName);
				if (skillValue > 0)
				{
					SetSkill(skillName, skillValue);
				}
			}

			Fame = 0;
			Karma = 0;
			Hue = GetHueForAshType(ashType);

			m_MiniGolem = new MiniGolem(this, ashType);
			if (owner?.Backpack != null)
			{
				owner.Backpack.DropItem(m_MiniGolem);
			}
			else
			{
				m_MiniGolem.Delete();
			}

			IsMaterialized = false; // Le golem commence dématérialisé
		}

		public GolemZyX(Serial serial) : base(serial) { }

		public void Materialize(Mobile master)
{
    if (m_IsMaterialized)
    {
        master.SendMessage("Le golem est déjà matérialisé.");
        return;
    }

    if (master == null || master.Deleted)
        return;

    if (master.Followers + ControlSlots > master.FollowersMax)
    {
        master.SendMessage("Vous n'avez pas assez de place pour matérialiser ce golem.");
        return;
    }

    master.SendMessage("Le golem commence à se matérialiser...");
    MaterializationInProgress = true;

    Timer.DelayCall(TimeSpan.FromSeconds(10), () =>
    {
        if (Deleted || master.Deleted)
        {
            MaterializationInProgress = false;
            return;
        }

        SetControlMaster(master);
        ControlTarget = master;
        ControlOrder = OrderType.Come;
        Map = master.Map;
        Location = master.Location;
        master.Followers = +3;

        m_IsMaterialized = true;
        MaterializationInProgress = false;
        Hidden = false;
        if (Map == null || Map == Map.Internal)
        {
            Map = master.Map;
        }

        Effects.SendLocationParticles(EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x3728, 10, 30, 5052);
        PlaySound(0x201);

        master.SendMessage("Le golem s'est matérialisé.");
    });
}


		public void Dematerialize()
		{
			if (ControlMaster != null)
			{
				ControlMaster.Followers = - 3; 
				ControlMaster.SendMessage("Vous avez dématérialisé le golem.");
			}

			ControlMaster = null;
			Controlled = false;
			m_IsMaterialized = false;
			Hidden = true;
			Map = Map.Internal;
		}

		private ResistanceType GetDamageType(GolemAsh.AshType ashType)
		{
			switch (ashType)
			{
				case GolemAsh.AshType.Feu: return ResistanceType.Fire;
				case GolemAsh.AshType.Eau:
				case GolemAsh.AshType.Glace: return ResistanceType.Cold;
				case GolemAsh.AshType.Poison: return ResistanceType.Poison;
				default: return ResistanceType.Physical;
			}
		}

		public override bool IsScaredOfScaryThings => false;
		public override bool IsScaryToPets => true;
		public override bool AutoDispel => false;
		public override bool BleedImmune => true;
		public override Poison PoisonImmune => Poison.Lethal;
		public override bool NoHouseRestrictions => true;
		public override bool IsInvulnerable => false;
		public override bool IsBondable => false;
		public override bool Unprovokable => true;
		public override bool CanRummageCorpses => false;
		public override bool BardImmune => true;
		public override bool DeleteCorpseOnDeath => true;
		public override bool CanBeRenamedBy(Mobile from) => true;
		public override bool IsDispellable => false;

		public override void OnDoubleClick(Mobile from)
		{
			if (from == ControlMaster)
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
				SummonMaster.Followers = -3;
			}
			Delete();
		}

		public override bool CanBeControlledBy(Mobile m) => m == SummonMaster;

		public override int GetIdleSound() => 268;
		public override int GetAngerSound() => 267;
		public override int GetHurtSound() => 269;
		public override int GetDeathSound() => 270;

		private int GetHueForAshType(GolemAsh.AshType ashType)
		{
			switch (ashType)
			{
				case GolemAsh.AshType.Feu: return 2952;
				case GolemAsh.AshType.Eau: return 2733;
				case GolemAsh.AshType.Glace: return 2988;
				case GolemAsh.AshType.Poison: return 2971;
				case GolemAsh.AshType.Sang: return 2958;
				case GolemAsh.AshType.Sylvestre: return 2949;
				case GolemAsh.AshType.Terre: return 1175;
				case GolemAsh.AshType.Vent: return 2794;
				default: return 0;
			}
		}

		public override void OnHeal(ref int amount, Mobile from)
		{
			// La guérison ne devrait être autorisée que par la méthode spécifique que nous définissons
			amount = 0;
			from?.SendMessage("Ce golem ne peut pas être soigné par des moyens conventionnels.");
		}

		// Nouvelle méthode pour gérer la guérison par les cendres
		public void HealByAsh(BaseGolemAsh ash, int amount)
		{
			if (GolemAsh.GetAshTypeFromAsh(ash) == this.AshType)
			{
				int healedAmount = Math.Min(HitsMax - Hits, amount);
				Hits += healedAmount;
				StoredHits = Hits; // Mettre à jour StoredHits
				InvalidateProperties();
			}
		}
		

		public override bool CanRegenHits => false;
		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			if (Deleted || !Alive)
				return;

			Hits -= amount;
			StoredHits = Hits; // Mettre à jour StoredHits

			if (Hits <= 0)
			{
				Kill();
			}

			m_MiniGolem?.InvalidateProperties();

			if (AshType == GolemAsh.AshType.Glace)
				AttemptParalyze(from);

			if (AshType == GolemAsh.AshType.Poison)
				AttemptPoison(from);
		}
		public override bool CanBeDamaged() => true;

		public override int HitsMax => m_MaxHitPoints;

		public void AttemptParalyze(Mobile target)
		{
			if (AshType != GolemAsh.AshType.Glace || target == null || !target.Alive || target.Paralyzed)
				return;

			double skill = Skills[SkillName.Wrestling].Value;
			if (skill / 150.0 > Utility.RandomDouble())
			{
				target.Paralyze(TimeSpan.FromSeconds(3 + skill / 50));
				target.PlaySound(0x204);
				target.FixedEffect(0x376A, 6, 1);
			}
		}

		public void RangedAttack(Mobile target)
		{
			if (AshType != GolemAsh.AshType.Sylvestre || target == null || !target.Alive || !InRange(target, 5))
				return;

			Direction = GetDirectionTo(target);
			MovingEffect(target, 0xF42, 7, 1, false, false);
			DoHarmful(target);
			AOS.Damage(target, this, Utility.RandomMinMax(DamageMin, DamageMax), 100, 0, 0, 0, 0);
		}

		public void AttemptPoison(Mobile target)
		{
			if (AshType != GolemAsh.AshType.Poison || target == null || !target.Alive || target.Poisoned)
				return;

			double skill = Skills[SkillName.Poisoning].Value;
			if (skill / 100.0 > Utility.RandomDouble())
			{
				int level = (int)(skill / 30.0);
				target.ApplyPoison(this, Poison.GetPoison(Math.Min(level, 3)));
				target.PlaySound(0x205);
				target.FixedEffect(0x3779, 1, 10);
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0); // version

			writer.Write(m_MiniGolem);
			writer.Write((int)m_AshType);
			writer.Write(m_MaxHitPoints);
			writer.Write(m_Penalty);
			writer.Write(m_SummonMaster);
			writer.Write(m_Spirit);
			writer.Write(m_IsMaterialized);
			writer.Write(Hits); 
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			m_MiniGolem = reader.ReadItem() as MiniGolem;
			m_AshType = (GolemAsh.AshType)reader.ReadInt();
			m_MaxHitPoints = reader.ReadInt();
			m_Penalty = reader.ReadInt();
			m_SummonMaster = reader.ReadMobile();
			m_Spirit = reader.ReadItem() as CreatureSpirit;
			m_IsMaterialized = reader.ReadBool();
			int storedHits = reader.ReadInt();

			

			// Restaurer les points de vie
			if (storedHits > 0 && storedHits <= HitsMax)
			{
				Hits = storedHits;
			}
			else
			{
				Hits = HitsMax;
			}

			if (m_IsMaterialized)
			{
				Hidden = false;
				if (m_SummonMaster != null)
				{
					SetControlMaster(m_SummonMaster);
					Controlled = true;
				}
			}
			else
			{
				Hidden = true;
				Map = Map.Internal;
			}

			// Assurez-vous que le MiniGolem est correctement lié
			if (m_MiniGolem != null)
			{
				m_MiniGolem.Golem = this;
			}
		}
	}



	public class GolemZyXAttributesGump : Gump
	{
		private static readonly int LabelColor = 0x7FFF;
		private const int SkillsPerPage = 9;

		public GolemZyXAttributesGump(GolemZyX golem) : base(250, 50)
		{
			AddPage(0);

			AddImage(100, 100, 2080);
			AddImage(118, 137, 2081);
			AddImage(118, 207, 2081);
			AddImage(118, 277, 2081);
			AddImage(118, 347, 2083);

			AddHtml(147, 108, 210, 18, $"<center><i>{golem.Name}</i></center>", false, false);

			AddButton(240, 77, 2093, 2093, 2, GumpButtonType.Reply, 0);

			AddImage(140, 138, 2091);
			AddImage(140, 335, 2091);

			List<SkillName> activeSkills = new List<SkillName>();
			foreach (SkillName skillName in Enum.GetValues(typeof(SkillName)))
			{
				if (golem.Skills[skillName].Base > 0)
				{
					activeSkills.Add(skillName);
				}
			}

			int skillPages = (int)Math.Ceiling((double)activeSkills.Count / SkillsPerPage);
			int totalPages = 2 + skillPages; // Attributes + Skills + Resistances

			int currentPage = 0;

			// Page 1: Attributes
			AddPage(++currentPage);
			AddAttributesPage(golem);
			AddButton(340, 358, 5601, 5605, 0, GumpButtonType.Page, currentPage + 1);
			AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, totalPages);

			// Skills Pages
			for (int i = 0; i < skillPages; i++)
			{
				AddPage(++currentPage);
				AddSkillsPage(golem, activeSkills, i);
				AddButton(340, 358, 5601, 5605, 0, GumpButtonType.Page, currentPage + 1);
				AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, currentPage - 1);
			}

			// Resistances Page
			AddPage(++currentPage);
			AddResistancesPage(golem);
			AddButton(317, 358, 5603, 5607, 0, GumpButtonType.Page, currentPage - 1);
		}

		private void AddAttributesPage(GolemZyX golem)
		{
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
		}

		private void AddSkillsPage(GolemZyX golem, List<SkillName> activeSkills, int pageIndex)
		{
			AddImage(128, 152, 2086);
			AddHtmlLocalized(147, 150, 160, 18, 3001030, 200, false, false); // Skills

			int startIndex = pageIndex * SkillsPerPage;
			int endIndex = Math.Min(startIndex + SkillsPerPage, activeSkills.Count);

			for (int i = startIndex, y = 168; i < endIndex; i++, y += 18)
			{
				SkillName skillName = activeSkills[i];
				AddHtmlLocalized(153, y, 160, 18, SkillInfo.Table[(int)skillName].Localization, LabelColor, false, false);
				AddHtml(320, y, 35, 18, FormatSkill(golem, skillName), false, false);
			}
		}

		private void AddResistancesPage(GolemZyX golem)
		{
			AddImage(128, 152, 2086);
			AddHtmlLocalized(147, 150, 160, 18, 1061645, 200, false, false); // Resistances

			AddHtmlLocalized(153, 168, 160, 18, 1061646, LabelColor, false, false); // Physical
			AddHtml(320, 168, 35, 18, FormatElement(golem.PhysicalResistance), false, false);

			AddHtmlLocalized(153, 186, 160, 18, 1061647, LabelColor, false, false); // Fire
			AddHtml(320, 186, 35, 18, FormatElement(golem.FireResistance), false, false);

			AddHtmlLocalized(153, 204, 160, 18, 1061648, LabelColor, false, false); // Cold
			AddHtml(320, 204, 35, 18, FormatElement(golem.ColdResistance), false, false);

			AddHtmlLocalized(153, 222, 160, 18, 1061649, LabelColor, false, false); // Poison
			AddHtml(320, 222, 35, 18, FormatElement(golem.PoisonResistance), false, false);

			AddHtmlLocalized(153, 240, 160, 18, 1061650, LabelColor, false, false); // Energy
			AddHtml(320, 240, 35, 18, FormatElement(golem.EnergyResistance), false, false);
		}


		private string FormatElement(int value)
		{
			return value.ToString();
		}

		private string FormatAttributes(int current, int max)
		{
			return $"{current}/{max}";
		}

		private string FormatStat(int value)
		{
			return value.ToString();
		}

		private string FormatDamage(int min, int max)
		{
			return $"{min}-{max}";
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
			if (m_Golem != null && from == m_Golem.SummonMaster)
			{
				list.Add(new OpenAttributesGumpEntry(from, m_Golem));
			}
		}

		private class OpenAttributesGumpEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private GolemZyX m_Golem;

			public OpenAttributesGumpEntry(Mobile from, GolemZyX golem) : base(3006150, 1)
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
			if (m_Golem != null)
			{
				if (from == m_Golem.SummonMaster)
				{
					if (m_Golem.Deleted)
					{
						from.SendMessage("Ce golem n'est plus fonctionnel.");
						return;
					}

					if (m_Golem.IsMaterialized)
					{
						m_Golem.Dematerialize();
						from.SendMessage("Le golem a été dématérialisé.");
					}
					else
					{
						if (m_Golem.MaterializationInProgress)
						{
							from.SendMessage("Le golem est déjà en cours de matérialisation.");
						}
						else
						{
							m_Golem.Materialize(from);
						}
					}
				}
				else if (from != m_Golem.SummonMaster && from.Alive)
				{
					// Transfert de propriété
					Mobile oldOwner = m_Golem.SummonMaster;

					// Vérifier si le nouveau propriétaire a assez de slots de follower disponibles
					if (m_Golem.IsMaterialized && from.Followers + 3 > from.FollowersMax)
					{
						from.SendMessage("Vous n'avez pas assez de slots de follower disponibles pour contrôler ce golem.");
						return;
					}

					// Ajuster les followers seulement si le golem est matérialisé
					if (m_Golem.IsMaterialized)
					{
						if (oldOwner != null)
						{
							oldOwner.Followers -= 3;
						}
						from.Followers += 3;
					}

					m_Golem.SummonMaster = from;
					m_Golem.SetControlMaster(from);

					from.SendMessage("Vous êtes maintenant le propriétaire de ce MiniGolem.");
					if (oldOwner != null && oldOwner.NetState != null)
						oldOwner.SendMessage("Votre MiniGolem a un nouveau propriétaire.");

					// Mettre à jour le MiniGolem
					this.Golem = m_Golem;
					this.InvalidateProperties();
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
				case GolemAsh.AshType.Feu: return 2952;
				case GolemAsh.AshType.Eau: return 2733;
				case GolemAsh.AshType.Glace: return 2988;
				case GolemAsh.AshType.Poison: return 2971;
				case GolemAsh.AshType.Sang: return 2958;
				case GolemAsh.AshType.Sylvestre: return 2949;
				case GolemAsh.AshType.Terre: return 1175;
				case GolemAsh.AshType.Vent: return 2794;
				default: return 0;
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0); // version
			writer.Write(m_Golem);
			writer.Write((int)m_AshType);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			m_Golem = reader.ReadMobile() as GolemZyX;
			m_AshType = (GolemAsh.AshType)reader.ReadInt();

			if (m_Golem == null || m_Golem.Deleted)
			{
				Delete();
			}
		}
	}
}
