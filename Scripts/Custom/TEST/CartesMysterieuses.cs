using System;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Mobiles;

public class SkillCard : Item
{
	private int m_Level;
	private SkillName m_Skill;
	private bool m_Decrypted;
	private static Dictionary<SkillName, List<Mobile>> s_ActiveCards = new Dictionary<SkillName, List<Mobile>>();

	[Constructable]
	public SkillCard() : base(0x9C14)
	{
		Name = "Carte mystérieuse";
		m_Level = Utility.RandomMinMax(1, 5);
		m_Decrypted = false;
		Stackable = false;
		Hue = 0;
		Weight = 1.0;
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
			from.SendMessage("Votre compétence en cartographie est trop faible pour décrypter cette carte.");
			return;
		}

		if (!from.CheckSkill(SkillName.Cartography, m_Level * 20, 100))
		{
			from.SendMessage("Vous n'avez pas réussi à décrypter la carte. Essayez encore.");
			return;
		}

		m_Decrypted = true;
		AssignSkill();
		UpdateAppearance();
		from.SendMessage("Vous avez réussi à décrypter la carte !");
	}

	private void AssignSkill()
	{
		SkillName[] skills = {
			SkillName.Alchemy, SkillName.Anatomy, SkillName.AnimalLore, SkillName.ItemID,
			SkillName.ArmsLore, SkillName.Parry, SkillName.Blacksmith, SkillName.Peacemaking,
			SkillName.Camping, SkillName.Carpentry, SkillName.Cartography, SkillName.Cooking,
			SkillName.DetectHidden, SkillName.Discordance, SkillName.EvalInt, SkillName.Healing,
			SkillName.Fishing, SkillName.Forensics, SkillName.Hiding, SkillName.Provocation,
			SkillName.Inscribe, SkillName.Lockpicking, SkillName.Magery, SkillName.MagicResist,
			SkillName.Tactics, SkillName.Snooping, SkillName.Musicianship, SkillName.Poisoning,
			SkillName.Archery, SkillName.SpiritSpeak, SkillName.Stealing, SkillName.Tailoring,
			SkillName.AnimalTaming, SkillName.TasteID, SkillName.Tinkering, SkillName.Tracking,
			SkillName.Veterinary, SkillName.Swords, SkillName.Macing, SkillName.Fencing,
			SkillName.Wrestling, SkillName.Lumberjacking, SkillName.Mining, SkillName.Meditation,
			SkillName.RemoveTrap, SkillName.Necromancy, SkillName.Concentration, SkillName.Equitation,
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
			case SkillName.Anatomy: return "Carte de l'Anatomiste Éclairé";
			case SkillName.AnimalLore: return "Carte du Maître des Bêtes";
			case SkillName.ItemID: return "Carte de l'Identificateur Perspicace";
			case SkillName.ArmsLore: return "Carte du Connaisseur d'Armes";
			case SkillName.Parry: return "Carte du Défenseur Agile";
			case SkillName.Blacksmith: return "Carte du Forgeron Légendaire";
			case SkillName.Peacemaking: return "Carte du Pacificateur Harmonieux";
			case SkillName.Camping: return "Carte du Survivaliste Émérite";
			case SkillName.Carpentry: return "Carte du Charpentier Créatif";
			case SkillName.Cartography: return "Carte du Cartographe Visionnaire";
			case SkillName.Cooking: return "Carte du Chef Culinaire";
			case SkillName.DetectHidden: return "Carte de l'Observateur Avisé";
			case SkillName.Discordance: return "Carte du Perturbateur Sonore";
			case SkillName.EvalInt: return "Carte de l'Évaluateur d'Esprits";
			case SkillName.Healing: return "Carte du Guérisseur Bienveillant";
			case SkillName.Fishing: return "Carte du Pêcheur Chanceux";
			case SkillName.Forensics: return "Carte de l'Enquêteur Méticuleux";
			case SkillName.Hiding: return "Carte de l'Ombre Furtive";
			case SkillName.Provocation: return "Carte du Provocateur Habile";
			case SkillName.Inscribe: return "Carte du Scribe Érudit";
			case SkillName.Lockpicking: return "Carte du Crocheteur Astucieux";
			case SkillName.Magery: return "Carte du Mage Puissant";
			case SkillName.MagicResist: return "Carte du Résistant Arcanique";
			case SkillName.Tactics: return "Carte du Tacticien Brillant";
			case SkillName.Snooping: return "Carte du Fouineur Discret";
			case SkillName.Musicianship: return "Carte du Virtuose Mélodieux";
			case SkillName.Poisoning: return "Carte de l'Empoisonneur Subtil";
			case SkillName.Archery: return "Carte de l'Archer Précis";
			case SkillName.SpiritSpeak: return "Carte du Médium Spirituel";
			case SkillName.Stealing: return "Carte du Voleur Insaisissable";
			case SkillName.Tailoring: return "Carte du Tailleur Raffiné";
			case SkillName.AnimalTaming: return "Carte du Dompteur Charismatique";
			case SkillName.TasteID: return "Carte du Gourmet Averti";
			case SkillName.Tinkering: return "Carte de l'Inventeur Ingénieux";
			case SkillName.Tracking: return "Carte du Pisteur Infaillible";
			case SkillName.Veterinary: return "Carte du Vétérinaire Compatissant";
			case SkillName.Swords: return "Carte du Bretteur Élégant";
			case SkillName.Macing: return "Carte du Fracasseur Redoutable";
			case SkillName.Fencing: return "Carte de l'Escrimeur Agile";
			case SkillName.Wrestling: return "Carte du Lutteur Indomptable";
			case SkillName.Lumberjacking: return "Carte du Bûcheron Robuste";
			case SkillName.Mining: return "Carte du Mineur Tenace";
			case SkillName.Meditation: return "Carte du Sage Méditatif";
			case SkillName.RemoveTrap: return "Carte du Désamorceur Prudent";
			case SkillName.Necromancy: return "Carte du Nécromancien Obscur";
			case SkillName.Concentration: return "Carte du Concentré Imperturbable";
			case SkillName.Equitation: return "Carte du Cavalier Émérite";
			case SkillName.Botanique: return "Carte du Botaniste Passionné";
			default: return "Carte de Compétence Inconnue";
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
			list.Add($"Compétence: {m_Skill}");
			list.Add($"Bonus: +{m_Level}%");
		}
	}

	public void Use(Mobile from)
	{
		if (!m_Decrypted)
		{
			from.SendMessage("Cette carte doit d'abord être décryptée.");
			return;
		}

		if (IsSkillCardActive(from, m_Skill))
		{
			from.SendMessage($"Vous avez déjà une carte de compétence {m_Skill} active.");
			return;
		}

		TimedSkillMod skillMod = new TimedSkillMod(m_Skill, true, m_Level, TimeSpan.FromHours(1));
		from.AddSkillMod(skillMod);
		from.SendMessage($"Votre compétence {m_Skill} a été augmentée de {m_Level}% pour 1 heure.");

		AddActiveCard(from, m_Skill);

		Timer.DelayCall(TimeSpan.FromHours(1), () =>
		{
			RemoveActiveCard(from, m_Skill);
			from.RemoveSkillMod(skillMod);
			from.SendMessage($"Le bonus de compétence {m_Skill} s'est dissipé.");
		});

		from.SendMessage("La carte se désintègre après avoir libéré son pouvoir.");
		this.Delete();
	}

	private static bool IsSkillCardActive(Mobile from, SkillName skill)
	{
		if (s_ActiveCards.TryGetValue(skill, out List<Mobile> activeUsers))
		{
			return activeUsers.Contains(from);
		}
		return false;
	}

	private static void AddActiveCard(Mobile from, SkillName skill)
	{
		if (!s_ActiveCards.ContainsKey(skill))
		{
			s_ActiveCards[skill] = new List<Mobile>();
		}
		s_ActiveCards[skill].Add(from);
	}

	private static void RemoveActiveCard(Mobile from, SkillName skill)
	{
		if (s_ActiveCards.TryGetValue(skill, out List<Mobile> activeUsers))
		{
			activeUsers.Remove(from);
			if (activeUsers.Count == 0)
			{
				s_ActiveCards.Remove(skill);
			}
		}
	}

	public override void OnDoubleClick(Mobile from)
	{
		if (m_Decrypted)
		{
			Use(from);
		}
		else
		{
			from.SendMessage("Cette carte doit d'abord être décryptée.");
		}
	}

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write((int)0); // version
		writer.Write(m_Level);
		writer.Write((int)m_Skill);
		writer.Write(m_Decrypted);
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
		m_Level = reader.ReadInt();
		m_Skill = (SkillName)reader.ReadInt();
		m_Decrypted = reader.ReadBool();
	}

	private class DecryptCardEntry : ContextMenuEntry
	{
		private Mobile m_From;
		private SkillCard m_Card;

		public DecryptCardEntry(Mobile from, SkillCard card) : base(6216, 3)
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

		public UseCardEntry(Mobile from, SkillCard card) : base(6217, 3)
		{
			m_From = from;
			m_Card = card;
		}

		public override void OnClick()
		{
			m_Card.Use(m_From);
		}
	}
}

