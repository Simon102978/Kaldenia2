using System;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Mobiles;

public class SkillCard : Item
{
	private int m_Level;
	private double m_Bonus;
	private SkillName m_Skill;
	private bool m_Decrypted;

	private static Dictionary<Mobile, Dictionary<SkillName, SkillCardEffect>> s_ActiveEffects = new Dictionary<Mobile, Dictionary<SkillName, SkillCardEffect>>();

	[Constructable]
	public SkillCard() : base(0x9C14)
	{
		Name = "Carte myst�rieuse";
		m_Level = Utility.RandomMinMax(1, 5);
		m_Bonus = GenerateBonus(m_Level);
		m_Decrypted = false;
		Stackable = false;
		Hue = 0;
		Weight = 1.0;
	}

	private double GenerateBonus(int level)
	{
		double minBonus = (level - 1) * 2 + 1;
		double maxBonus = level * 2;
		return Math.Round(Utility.RandomMinMax((int)(minBonus * 10), (int)(maxBonus * 10)) / 10.0, 1);
	}

	public SkillCard(Serial serial) : base(serial)
	{
	}

	public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
	{
		base.GetContextMenuEntries(from, list);

		if (!m_Decrypted)
		{
			list.Add(new DecryptCardEntry(from, this));
		}
		else
		{
			list.Add(new UseCardEntry(from, this));
		}
	}

	public void Decrypt(Mobile from)
	{
		if (from.Skills[SkillName.Cartography].Value < m_Level * 20)
		{
			from.SendMessage("Votre comp�tence en cartographie est trop faible pour d�crypter cette carte.");
			return;
		}

		if (!from.CheckSkill(SkillName.Cartography, m_Level * 20, 100))
		{
			from.SendMessage("Vous n'avez pas r�ussi � d�crypter la carte. Essayez encore.");
			return;
		}

		m_Decrypted = true;
		AssignSkill();
		UpdateAppearance();
		from.SendMessage("Vous avez r�ussi � d�crypter la carte !");
	}

	private void AssignSkill()
	{
		SkillName[] skills = {
			SkillName.Alchemy, SkillName.Anatomy, SkillName.AnimalLore,
			SkillName.Parry, SkillName.Blacksmith, SkillName.Peacemaking,
			SkillName.Camping, SkillName.Carpentry, SkillName.Cartography, SkillName.Cooking,
			SkillName.Discordance, SkillName.EvalInt, SkillName.Healing,
			SkillName.Fishing,  SkillName.Hiding, SkillName.Provocation,
			SkillName.Inscribe, SkillName.Lockpicking, SkillName.Magery, SkillName.MagicResist,
			SkillName.Tactics, SkillName.Snooping, SkillName.Musicianship, SkillName.Poisoning,
			SkillName.Archery,  SkillName.Stealing, SkillName.Tailoring,
			SkillName.AnimalTaming,  SkillName.Tinkering, SkillName.Tracking,
			SkillName.Veterinary, SkillName.Swords, SkillName.Macing, SkillName.Fencing,
			SkillName.Wrestling, SkillName.Lumberjacking, SkillName.Mining, SkillName.Meditation,
			SkillName.Concentration, SkillName.Equitation,
			SkillName.Botanique
		};

		m_Skill = skills[Utility.Random(skills.Length)];
	}

	private void UpdateAppearance()
	{
		ItemID = 0x9C15;
		Name = GetCardName();
		Hue = GetCardHue();
	}

	private string GetCardName()
	{
		switch (m_Skill)
		{
			case SkillName.Alchemy: return "Carte de l'Alchimiste Mystique";
			case SkillName.Anatomy: return "Carte de l'Anatomiste �clair�";
			case SkillName.AnimalLore: return "Carte du Ma�tre des B�tes";
			case SkillName.ItemID: return "Carte de l'Identificateur Perspicace";
			case SkillName.ArmsLore: return "Carte du Connaisseur d'Armes";
			case SkillName.Parry: return "Carte du D�fenseur Agile";
			case SkillName.Blacksmith: return "Carte du Forgeron L�gendaire";
			case SkillName.Peacemaking: return "Carte du Pacificateur Harmonieux";
			case SkillName.Camping: return "Carte du Survivaliste �m�rite";
			case SkillName.Carpentry: return "Carte du Charpentier Cr�atif";
			case SkillName.Cartography: return "Carte du Cartographe Visionnaire";
			case SkillName.Cooking: return "Carte du Chef Culinaire";
			case SkillName.DetectHidden: return "Carte de l'Observateur Avis�";
			case SkillName.Discordance: return "Carte du Perturbateur Sonore";
			case SkillName.EvalInt: return "Carte de l'�valuateur d'Esprits";
			case SkillName.Healing: return "Carte du Gu�risseur Bienveillant";
			case SkillName.Fishing: return "Carte du P�cheur Chanceux";
			case SkillName.Forensics: return "Carte de l'Enqu�teur M�ticuleux";
			case SkillName.Hiding: return "Carte de l'Ombre Furtive";
			case SkillName.Provocation: return "Carte du Provocateur Habile";
			case SkillName.Inscribe: return "Carte du Scribe �rudit";
			case SkillName.Lockpicking: return "Carte du Crocheteur Astucieux";
			case SkillName.Magery: return "Carte du Mage Puissant";
			case SkillName.MagicResist: return "Carte du R�sistant Arcanique";
			case SkillName.Tactics: return "Carte du Tacticien Brillant";
			case SkillName.Snooping: return "Carte du Fouineur Discret";
			case SkillName.Musicianship: return "Carte du Virtuose M�lodieux";
			case SkillName.Poisoning: return "Carte de l'Empoisonneur Subtil";
			case SkillName.Archery: return "Carte de l'Archer Pr�cis";
			case SkillName.SpiritSpeak: return "Carte du M�dium Spirituel";
			case SkillName.Stealing: return "Carte du Voleur Insaisissable";
			case SkillName.Tailoring: return "Carte du Tailleur Raffin�";
			case SkillName.AnimalTaming: return "Carte du Dompteur Charismatique";
			case SkillName.TasteID: return "Carte du Gourmet Averti";
			case SkillName.Tinkering: return "Carte de l'Inventeur Ing�nieux";
			case SkillName.Tracking: return "Carte du Pisteur Infaillible";
			case SkillName.Veterinary: return "Carte du V�t�rinaire Compatissant";
			case SkillName.Swords: return "Carte du Bretteur �l�gant";
			case SkillName.Macing: return "Carte du Fracasseur Redoutable";
			case SkillName.Fencing: return "Carte de l'Escrimeur Agile";
			case SkillName.Wrestling: return "Carte du Lutteur Indomptable";
			case SkillName.Lumberjacking: return "Carte du B�cheron Robuste";
			case SkillName.Mining: return "Carte du Mineur Tenace";
			case SkillName.Meditation: return "Carte du Sage M�ditatif";
			case SkillName.RemoveTrap: return "Carte du D�samorceur Prudent";
			case SkillName.Necromancy: return "Carte du N�cromancien Obscur";
			case SkillName.Concentration: return "Carte du Concentr� Imperturbable";
			case SkillName.Equitation: return "Carte du Cavalier �m�rite";
			case SkillName.Botanique: return "Carte du Botaniste Passionn�";
			default: return "Carte de Comp�tence Inconnue";
		}
	}

	private int GetCardHue()
	{
		switch (m_Skill)
		{
			case SkillName.Alchemy: return 1161;
			case SkillName.Anatomy: return 1645;
			case SkillName.AnimalLore: return 1425;
			case SkillName.ItemID: return 1152;
			case SkillName.ArmsLore: return 1172;
			case SkillName.Parry: return 1266;
			case SkillName.Blacksmith: return 1150;
			case SkillName.Peacemaking: return 1285;
			case SkillName.Camping: return 2213;
			case SkillName.Carpentry: return 1891;
			case SkillName.Cartography: return 1717;
			case SkillName.Cooking: return 1193;
			case SkillName.DetectHidden: return 1109;
			case SkillName.Discordance: return 1287;
			case SkillName.EvalInt: return 1364;
			case SkillName.Healing: return 1154;
			case SkillName.Fishing: return 1165;
			case SkillName.Forensics: return 1272;
			case SkillName.Hiding: return 1109;
			case SkillName.Provocation: return 1159;
			case SkillName.Inscribe: return 1358;
			case SkillName.Lockpicking: return 2305;
			case SkillName.Magery: return 1364;
			case SkillName.MagicResist: return 1364;
			case SkillName.Tactics: return 1172;
			case SkillName.Snooping: return 1109;
			case SkillName.Musicianship: return 1191;
			case SkillName.Poisoning: return 1270;
			case SkillName.Archery: return 1172;
			case SkillName.SpiritSpeak: return 1159;
			case SkillName.Stealing: return 2305;
			case SkillName.Tailoring: return 1174;
			case SkillName.AnimalTaming: return 1425;
			case SkillName.TasteID: return 1193;
			case SkillName.Tinkering: return 1152;
			case SkillName.Tracking: return 2213;
			case SkillName.Veterinary: return 1154;
			case SkillName.Swords: return 1172;
			case SkillName.Macing: return 1172;
			case SkillName.Fencing: return 1172;
			case SkillName.Wrestling: return 1172;
			case SkillName.Lumberjacking: return 1191;
			case SkillName.Mining: return 2413;
			case SkillName.Meditation: return 1364;
			case SkillName.RemoveTrap: return 2305;
			case SkillName.Necromancy: return 1109;
			case SkillName.Concentration: return 1364;
			case SkillName.Equitation: return 1172;
			case SkillName.Botanique: return 1191;
			default: return 0;
		}
	}

	public override void GetProperties(ObjectPropertyList list)
	{
		base.GetProperties(list);
		if (m_Decrypted)
		{
			list.Add($"Comp�tence: {m_Skill}");
			list.Add($"Bonus: +{m_Bonus:F1}%");
		}
	}

	public void Use(Mobile from)
	{
		if (!m_Decrypted)
		{
			from.SendMessage("Cette carte doit d'abord �tre d�crypt�e.");
			return;
		}

		if (IsSkillCardActive(from, m_Skill))
		{
			from.SendMessage($"Vous avez d�j� une carte de comp�tence {m_Skill} active.");
			return;
		}

		double baseValue = from.Skills[m_Skill].Base;
		double cap = from.Skills[m_Skill].Cap;
		double effectiveBonus = Math.Min(m_Bonus, cap - baseValue);

		SkillMod mod = new DefaultSkillMod(m_Skill, true, effectiveBonus);
		from.AddSkillMod(mod);

		SkillCardEffect effect = new SkillCardEffect
		{
			Owner = from,
			Skill = m_Skill,
			Bonus = m_Bonus,
			ExpireTime = DateTime.UtcNow + TimeSpan.FromHours(1),
			SkillMod = mod
		};

		if (!s_ActiveEffects.ContainsKey(from))
		{
			s_ActiveEffects[from] = new Dictionary<SkillName, SkillCardEffect>();
		}
		s_ActiveEffects[from][m_Skill] = effect;

		from.SendMessage($"Votre comp�tence {m_Skill} a �t� augment�e de {effectiveBonus:F1}% pour 1 heure.");

		Timer.DelayCall(TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30), () => CheckAndAdjustBonus(effect));
		Timer.DelayCall(TimeSpan.FromHours(1), () => RemoveEffect(effect));

		from.SendMessage("La carte se d�sint�gre apr�s avoir lib�r� son pouvoir.");
		this.Delete();
	}

	private static void CheckAndAdjustBonus(SkillCardEffect effect)
	{
		if (effect.Owner.Deleted || DateTime.UtcNow >= effect.ExpireTime)
		{
			RemoveEffect(effect);
			return;
		}

		double baseValue = effect.Owner.Skills[effect.Skill].Base;
		double cap = effect.Owner.Skills[effect.Skill].Cap;
		double currentBonus = effect.SkillMod.Value;
		double newBonus = Math.Min(effect.Bonus, cap - baseValue);

		if (Math.Abs(newBonus - currentBonus) > 0.1)
		{
			effect.Owner.RemoveSkillMod(effect.SkillMod);
			SkillMod newMod = new DefaultSkillMod(effect.Skill, true, newBonus);
			effect.Owner.AddSkillMod(newMod);
			effect.SkillMod = newMod;
			effect.Owner.SendMessage($"Votre bonus de comp�tence {effect.Skill} a �t� ajust� � {newBonus:F1}%.");
		}
	}

	private static void RemoveEffect(SkillCardEffect effect)
	{
		if (effect.Owner != null && !effect.Owner.Deleted)
		{
			effect.Owner.RemoveSkillMod(effect.SkillMod);
			effect.Owner.SendMessage($"Le bonus de comp�tence {effect.Skill} s'est dissip�.");
		}

		if (s_ActiveEffects.ContainsKey(effect.Owner))
		{
			s_ActiveEffects[effect.Owner].Remove(effect.Skill);
			if (s_ActiveEffects[effect.Owner].Count == 0)
			{
				s_ActiveEffects.Remove(effect.Owner);
			}
		}
	}

	private static bool IsSkillCardActive(Mobile from, SkillName skill)
	{
		return s_ActiveEffects.ContainsKey(from) && s_ActiveEffects[from].ContainsKey(skill);
	}

	public override void OnDoubleClick(Mobile from)
	{
		if (m_Decrypted)
		{
			Use(from);
		}
		else
		{
			from.SendMessage("Cette carte doit d'abord �tre d�crypt�e.");
		}
	}

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write((int)1); // version
		writer.Write(m_Level);
		writer.Write(m_Bonus);
		writer.Write((int)m_Skill);
		writer.Write(m_Decrypted);
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();

		if (version >= 1)
		{
			m_Level = reader.ReadInt();
			m_Bonus = reader.ReadDouble();
		}
		else
		{
			m_Level = reader.ReadInt();
			m_Bonus = m_Level; // Pour la compatibilit� avec l'ancienne version
		}

		m_Skill = (SkillName)reader.ReadInt();
		m_Decrypted = reader.ReadBool();
	}

	private class DecryptCardEntry : ContextMenuEntry
	{
		private Mobile m_From;
		private SkillCard m_Card;

		public DecryptCardEntry(Mobile from, SkillCard card) : base(3006150, 1)
		{
			m_From = from;
			m_Card = card;
		}

		public override void OnClick()
		{
			m_Card.Decrypt(m_From);
		}
	}

	private class UseCardEntry : ContextMenuEntry
	{
		private Mobile m_From;
		private SkillCard m_Card;

		public UseCardEntry(Mobile from, SkillCard card) : base(3006150, 1)
		{
			m_From = from;
			m_Card = card;
		}

		public override void OnClick()
		{
			m_Card.Use(m_From);
		}
	}

	private class SkillCardEffect
	{
		public Mobile Owner { get; set; }
		public SkillName Skill { get; set; }
		public double Bonus { get; set; }
		public DateTime ExpireTime { get; set; }
		public SkillMod SkillMod { get; set; }
	}
}

