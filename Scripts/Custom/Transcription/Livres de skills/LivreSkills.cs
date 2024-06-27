using System;
using Server.Custom.Aptitudes;
using Server.Engines.Craft;
using Server.Mobiles;
using static Server.Mobiles.CustomPlayerMobile;

namespace Server.Items
{
    [FlipableAttribute(0xFBE, 0xFBD)]
	public abstract class LivreSkills : Item, ICraftable
	{
        private double m_Max;
        private double m_Level;
        private double m_GrowValue;
        private SkillName m_Skill;
		
		public string m_Marque;

        private Mobile m_Author;
        private Mobile m_Owner;

        [CommandProperty(AccessLevel.GameMaster)]
        public double Max
        {
            get { return m_Max; }
            set { m_Max = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double Level
        {
            get { return m_Level; }
            set { m_Level = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public double GrowValue
        {
            get { return m_GrowValue; }
            set { m_GrowValue = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public SkillName Skill
        {
            get { return m_Skill; }
            set { m_Skill = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Author
        {
            get { return m_Author; }
            set { m_Author = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
        }
		
		[CommandProperty(AccessLevel.GameMaster)]
        public string Marque
        {
            get { return m_Marque; }
            set { m_Marque = value; InvalidateProperties(); }
        }
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_Marque != null && m_Marque != "")
                list.Add(1060527, m_Marque);
        }

        public LivreSkills() : this(SkillName.Alchemy, 0.0, 0.0)
        {
        }

        public LivreSkills(SkillName skill, double value, double growvalue) : base(0xFBE)
        {
            Name = "livre d'étude";
            Weight = 2.0;

            m_Skill = skill;
            m_Level = value;
            m_GrowValue = growvalue;
            m_Max = 30;
        }

        public LivreSkills(Serial serial) : base(serial)
        {
        }

        public bool CanRaise(CustomPlayerMobile from, SkillName skill)
        {
            double skillValue = from.Skills[skill].Base;

			if (m_Level <= 0)
			{
				from.SendMessage("Vous ne pouvez plus augmenter votre compétence avec ce livre.");
				return false;
			}
			//else if (from.SkillsTotal >= from.SkillsCap)
			//{
			//	from.SendMessage("Vous ne pouvez plus augmenter votre compétence avec ce livre (Total Skill Cap).");
			//	return false;
			//}
			else if (skillValue >= from.Skills[skill].Cap)
            {
                from.SendMessage("Vous ne pouvez augmenter votre skill. Vous avez atteint votre cap (Skill Cap).");
                return false;
            }
            else if (from.Skills[skill].Lock != SkillLock.Up)
            {
                from.SendMessage("Vous ne pouvez augmenter votre skill. Vérifiez le cadenas.");
                return false;
            }
            else if (skillValue >= m_Max)
            {
                from.SendMessage("Vous ne pouvez augmenter votre skill. Vous avez atteint le niveau maximum du livre.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetValue(CustomPlayerMobile from, SkillName skill, double value)
        {
            from.Skills[skill].Base = value;
        }

        public virtual bool IsInLibrary(Mobile from)
        {
			return true; // from.Region is LibraryRegion;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from is CustomPlayerMobile)
            {
                var pm = from as CustomPlayerMobile;

                if (!IsChildOf(pm.Backpack))
                {
                    pm.SendMessage("Le livre doit être dans votre sac.");
                }
                else if (m_Owner != null && m_Owner != pm)
                {
                    pm.SendMessage("Ce livre ne vous appartient pas.");
                }
                else if (!IsInLibrary(pm))
                {
                    pm.SendMessage("Vous devez être dans une bibliothèque pour lire ce livre.");
                }
                else if (!pm.CanBeginAction(typeof(Etude)))
                {
                    pm.SendMessage("Vous ne pouvez étudier présentement.");
                }
                else if (CanRaise(pm, m_Skill))
                {
                    m_Owner = pm;
                    pm.SendMessage("Vous débutez votre lecture.");
                    pm.BeginAction(typeof(Etude));

                    double duration = 68 - (0.4 * pm.RawInt);

                    if (duration > 60)
                        duration = 60;

                    if (duration < 20)
                        duration = 20;

                    new InternalTimer(pm, this, duration).Start();
                }
            }
        }

        public void Gain(CustomPlayerMobile from, SkillName skill)
        {
            double level = (double)m_Level;
            double toGain = m_GrowValue;
            double value = from.Skills[skill].Base;
            double totalValue = (double)from.SkillsTotal / 10;
            double skillsCap = (double)from.SkillsCap / 10;
            double skillCap = from.Skills[skill].Cap;
            double cap = Math.Min(skillCap, m_Max);
            SkillLock lockc = from.Skills[skill].Lock;

            from.Validate(ValidateType.Skills);

            if (lockc == SkillLock.Up)
            {
                if (toGain + value > cap)
                    toGain = cap - value;

                if (toGain > 0)
                {
                    bool isValid = totalValue + toGain <= skillsCap;

                    for (int i = 0; !isValid && i < from.Skills.Length; ++i)
                    {
                        SkillName toLower = (SkillName)i;

                        if (toLower != skill && from.Skills[toLower].Lock == SkillLock.Down && from.Skills[toLower].Base > 0)
                            SetValue(from, toLower, from.Skills[toLower].Base - ((totalValue + toGain) - skillsCap));

                        totalValue = (double)from.SkillsTotal / 10;
                        isValid = (totalValue + toGain <= skillsCap) && (toGain <= level);
                    }

                    if (!isValid && toGain + totalValue > cap)
                        toGain = cap - totalValue;

                    if (!isValid && toGain > level)
                        toGain = level;
                }

                if (toGain > 0)
                {
                    from.Skills[skill].Base += toGain;
                    m_Level -= toGain;

                    if (m_Level < 0)
                        m_Level = 0;

                    m_Owner.SendMessage("Vous terminez votre lecture.");
                }
                else
                {
                    m_Owner.SendMessage("Vous tournez les pages du livre sans trop comprendre ce qu'il est écrit.");
                }
            }
        }

        private class InternalTimer : Timer
        {
            private CustomPlayerMobile m_Owner;
            private LivreSkills m_Livre;

            public InternalTimer(CustomPlayerMobile owner, LivreSkills livre, double duration)
                : base(TimeSpan.FromSeconds(duration))
            {
                m_Owner = owner;
                m_Livre = livre;
            }

            protected override void OnTick()
            {
                if (!m_Owner.CanBeginAction(typeof(Etude)))
                {
                    m_Owner.EndAction(typeof(Etude));

                    if (m_Livre != null && m_Owner != null)
                    {
                        if (!m_Livre.IsChildOf(m_Owner.Backpack))
                        {
                            m_Owner.SendMessage("Le livre doit être dans votre sac.");
                        }
                        else if (!m_Livre.IsInLibrary(m_Owner))
                        {
                            m_Owner.SendMessage("Vous devez être dans une bibliothèque pour lire ce livre.");
                        }
                        else if (m_Livre.CanRaise(m_Owner, m_Livre.Skill))
                        {
                            m_Livre.Gain(m_Owner, m_Livre.Skill);
                        }
                    }
                }
            }
        }

        public override void OnAosSingleClick(Mobile from)
        {
            LabelTo(from, Name);
            LabelTo(from, String.Format("{0}, {1}", m_Level, m_GrowValue));
        }

		public override void Serialize( GenericWriter writer )
		{
            base.Serialize(writer);

            writer.Write((int)3); // version

			writer.Write((string)m_Marque);
			
            writer.Write(m_Max);

            writer.Write(m_Author);
            writer.Write(m_Owner);	

            writer.Write((int)m_Skill);
            writer.Write((double)m_Level);
            writer.Write((double)m_GrowValue);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            switch (version)
            {
				 case 3:
                    {
                        m_Marque = reader.ReadString();

                        goto case 2;
                    }				
                case 2:
                    {
                        m_Max = reader.ReadDouble();

                        goto case 1;
                    }
                case 1:
                {
                    m_Author = reader.ReadMobile();
                    m_Owner = reader.ReadMobile();
                    goto case 0;
                }
                case 0:
                {
                    m_Skill = (SkillName)reader.ReadInt();
                    m_Level = reader.ReadDouble();
                    m_GrowValue = reader.ReadDouble();
                    break;
                }
            }
        }

        #region ICraftable Members
		public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            if (craftSystem is DefInscription)
            {
                m_Level = 30;
                m_GrowValue = 0.2;
                m_Max = 30;

                if (from is CustomPlayerMobile pm)
                {
                    m_Level = pm.Aptitudes.GetRealValue(Aptitude.Transcription) * 3;
                    m_GrowValue = pm.Aptitudes.GetRealValue(Aptitude.Transcription) * 0.2 + pm.Capacites.Expertise * 0.1 + pm.Capacites.Perfection * 0.1;

                    double apti = pm.Aptitudes.GetRealValue(Aptitude.Transcription);

                    m_Max = 50 + apti * 5;
                }

                m_Author = from;

                switch (craftItem.NameString)
                {
                    case "Alchemy": m_Skill = SkillName.Alchemy; break;
                    case "Anatomy": m_Skill = SkillName.Anatomy; break;
                    case "AnimalLore": m_Skill = SkillName.AnimalLore; break;
                    case "ItemID": m_Skill = SkillName.ItemID; break;
                    case "ArmsLore": m_Skill = SkillName.ArmsLore; break;
                    case "Parry": m_Skill = SkillName.Parry; break;
                    case "Begging": m_Skill = SkillName.Begging; break;
                    case "Blacksmith": m_Skill = SkillName.Blacksmith; break;
                    case "Fletching": m_Skill = SkillName.Fletching; break;
                    case "Peacemaking": m_Skill = SkillName.Peacemaking; break;
                    case "Camping": m_Skill = SkillName.Camping; break;
                    case "Carpentry": m_Skill = SkillName.Carpentry; break;
                    case "Cartography": m_Skill = SkillName.Cartography; break;
                    case "Cooking": m_Skill = SkillName.Cooking; break;
                    case "DetectHidden": m_Skill = SkillName.DetectHidden; break;
                    case "Discordance": m_Skill = SkillName.Discordance; break;
                    case "EvalInt": m_Skill = SkillName.EvalInt; break;
                    case "Healing": m_Skill = SkillName.Healing; break;
                    case "Fishing": m_Skill = SkillName.Fishing; break;
                    case "Forensics": m_Skill = SkillName.Forensics; break;
                    case "Herding": m_Skill = SkillName.Herding; break;
                    case "Hiding": m_Skill = SkillName.Hiding; break;
                    case "Provocation": m_Skill = SkillName.Provocation; break;
                    case "Inscribe": m_Skill = SkillName.Inscribe; break;
                    case "Lockpicking": m_Skill = SkillName.Lockpicking; break;
                    case "Magery": m_Skill = SkillName.Magery; break;
                    case "MagicResist": m_Skill = SkillName.MagicResist; break;
                    case "Tactics": m_Skill = SkillName.Tactics; break;
                    case "Snooping": m_Skill = SkillName.Snooping; break;
                    case "Musicianship": m_Skill = SkillName.Musicianship; break;
                    case "Poisoning": m_Skill = SkillName.Poisoning; break;
                    case "Archery": m_Skill = SkillName.Archery; break;
                    case "SpiritSpeak": m_Skill = SkillName.SpiritSpeak; break;
                    case "Stealing": m_Skill = SkillName.Stealing; break;
                    case "Tailoring": m_Skill = SkillName.Tailoring; break;
                    case "AnimalTaming": m_Skill = SkillName.AnimalTaming; break;
                    case "TasteID": m_Skill = SkillName.TasteID; break;
                    case "Tinkering": m_Skill = SkillName.Tinkering; break;
                    case "Tracking": m_Skill = SkillName.Tracking; break;
                    case "Veterinary": m_Skill = SkillName.Veterinary; break;
                    case "Swords": m_Skill = SkillName.Swords; break;
                    case "Macing": m_Skill = SkillName.Macing; break;
                    case "Fencing": m_Skill = SkillName.Fencing; break;
                    case "Wrestling": m_Skill = SkillName.Wrestling; break;
                    case "Lumberjacking": m_Skill = SkillName.Lumberjacking; break;
                    case "Mining": m_Skill = SkillName.Mining; break;
                    case "Meditation": m_Skill = SkillName.Meditation; break;
                    case "Stealth": m_Skill = SkillName.Stealth; break;
                    case "RemoveTrap": m_Skill = SkillName.RemoveTrap; break;
                    case "Necromancy": m_Skill = SkillName.Necromancy; break;
                }
            }

            return 1;
        }
		#endregion
	}
}