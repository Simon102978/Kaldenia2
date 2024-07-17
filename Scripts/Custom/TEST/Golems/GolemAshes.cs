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
using Server.Custom;

public class GolemAsh : Item
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

	private AshType m_Type;

	[CommandProperty(AccessLevel.GameMaster)]
	public AshType Type
	{
		get { return m_Type; }
		set { m_Type = value; InvalidateProperties(); }
	}



	private int m_Amount;


	[CommandProperty(AccessLevel.GameMaster)]
	public int Amount
	{
		get { return m_Amount; }
		set
		{
			m_Amount = value;
			InvalidateProperties();
		}
	}

	[Constructable]
	public GolemAsh(AshType type) : this(type, 1)
	{
	}

	[Constructable]
	public GolemAsh(AshType type, int amount) : base(0x26B8)
	{
		Stackable = true;
		m_Type = type;
		m_Amount = amount;
		Name = $"Cendres de {type}";
		Hue = GetHueForType(type);
	}

	public GolemAsh(Serial serial) : base(serial)
	{
	}

	public override void GetProperties(ObjectPropertyList list)
	{
		base.GetProperties(list);
		list.Add($"Type: {m_Type}");
		list.Add($"Quantit�: {m_Amount}");
	}

	private int GetHueForType(AshType type)
	{
		switch (type)
		{
			case AshType.Feu: return 1161;
			case AshType.Eau: return 1153;
			case AshType.Glace: return 1152;
			case AshType.Poison: return 1167;
			case AshType.Sang: return 1157;
			case AshType.Sylvestre: return 1171;
			case AshType.Terre: return 1147;
			case AshType.Vent: return 1154;
			default: return 0;
		}
	}

	public static string GetAshBonusDescription(AshType type)
	{
		switch (type)
		{
			case AshType.Feu:
				return "Bonus de 20 points � la STR\nBonus de 10 points � la Tactics\nP�nalit�: 4";
			case AshType.Eau:
				return "Bonus de 20 points � la DEX\nBonus de 10 points au Wrestling\nP�nalit�: 3";
			case AshType.Glace:
				return "Attaque de paralysie\nBonus de 20 points au Wrestling\nP�nalit�: 2";
			case AshType.Poison:
				return "Attaque de poison\nBonus de 20 points � la Tactics\nP�nalit�: 2";
			case AshType.Sang:
				return "Double armure\nBonus de 20 points au Wrestling\nP�nalit�: 2";
			case AshType.Sylvestre:
				return "Attaque de longue port�e\nBonus de 20 points au Magic Resist\nP�nalit�: 3";
			case AshType.Terre:
				return "Bonus de 20 points � la Tactics\nBonus de 20 points au Wrestling\nP�nalit�: 3";
			case AshType.Vent:
				return "Double DEX\nBonus de 20 points � la Tactics\nP�nalit�: 2";
			default:
				return "Aucun bonus sp�cifique";
		}
	}

	public static void ApplyAshBonuses(GolemZyX golem, AshType ashType, int ashQuantity, double inscribeSkill)
	{
		int baseBonus = ashQuantity * 7;
		int skillBonus = (int)(inscribeSkill * 0.1);

		switch (ashType)
		{
			case AshType.Feu:
				golem.SetStr(golem.Str + baseBonus + 20);
				golem.Skills[SkillName.Tactics].Base += 10 + skillBonus;
				golem.SetDamageType(ResistanceType.Fire, 100);
				golem.SetResistance(ResistanceType.Fire, 50);
				golem.Penalty = 4;
				break;
			case AshType.Eau:
				golem.SetDex(golem.Dex + baseBonus + 20);
				golem.Skills[SkillName.Wrestling].Base += 10 + skillBonus;
				golem.SetDamageType(ResistanceType.Cold, 100);
				golem.SetResistance(ResistanceType.Cold, 50);
				golem.Penalty = 3;
				break;
			case AshType.Glace:
				golem.Skills[SkillName.Wrestling].Base += 20 + skillBonus;
				golem.SetDamageType(ResistanceType.Cold, 100);
				golem.SetResistance(ResistanceType.Cold, 50);
				golem.Penalty = 2;
				break;
			case AshType.Poison:
				golem.Skills[SkillName.Tactics].Base += 20 + skillBonus;
				golem.SetDamageType(ResistanceType.Poison, 100);
				golem.SetResistance(ResistanceType.Poison, 50);
				golem.Penalty = 2;
				break;
			case AshType.Sang:
				golem.Skills[SkillName.Wrestling].Base += 20 + skillBonus;
				golem.VirtualArmor *= 2;
				golem.SetDamageType(ResistanceType.Physical, 100);
				golem.Penalty = 2;
				break;
			case AshType.Sylvestre:
				golem.Skills[SkillName.MagicResist].Base += 20 + skillBonus;
				golem.SetDamageType(ResistanceType.Energy, 100);
				golem.SetResistance(ResistanceType.Energy, 50);
				golem.Penalty = 3;
				break;
			case AshType.Terre:
				golem.Skills[SkillName.Tactics].Base += 20 + skillBonus;
				golem.Skills[SkillName.Wrestling].Base += 20 + skillBonus;
				golem.SetDamageType(ResistanceType.Physical, 100);
				golem.SetResistance(ResistanceType.Physical, 50);
				golem.Penalty = 3;
				break;
			case AshType.Vent:
				golem.SetDex(golem.Dex * 2);
				golem.Skills[SkillName.Tactics].Base += 20 + skillBonus;
				golem.SetDamageType(ResistanceType.Energy, 100);
				golem.SetResistance(ResistanceType.Energy, 50);
				golem.Penalty = 2;
				break;
		}

		golem.SetHits(golem.HitsMax + (baseBonus * 2));
		golem.SetDamage(golem.DamageMin + (baseBonus / 10), golem.DamageMax + (baseBonus / 5));
	}

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write((int)1); // version
		writer.Write((int)m_Type);
		writer.Write(m_Amount);
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
		m_Type = (AshType)reader.ReadInt();
		if (version > 0)
		{
			m_Amount = reader.ReadInt();
		}
	}
}
