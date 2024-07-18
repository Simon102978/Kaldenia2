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



	

	[Constructable]
	public GolemAsh(AshType type) : this(type, 1)
	{
	}

	[Constructable]
	public GolemAsh(AshType type, int Amount) : base(0x0F7C)
	{
		Name = "Cendres Élémentaires"; // Name = $"Cendres de {type}";
		Stackable = true;
		m_Type = type;
		//Amount = amount;
		Hue = GetHueForType(type);
		Weight = 1.0;
	}

	public GolemAsh(Serial serial) : base(serial)
	{
	}

	public override void GetProperties(ObjectPropertyList list)
	{
		base.GetProperties(list);
		list.Add($"Type: {m_Type}");
		list.Add($"Quantité: {Amount}");
	}

	


	private int GetHueForType(AshType type)
	{
		switch (type)
		{
			case AshType.Feu: return 1161;
			case AshType.Eau: return 1156;
			case AshType.Glace: return 1152;
			case AshType.Poison: return 1193;
			case AshType.Sang: return 1194;
			case AshType.Sylvestre: return 1190;
			case AshType.Terre: return 1175;
			case AshType.Vent: return -1;
			default: return 0;
		}
	}

	public static string GetAshBonusDescription(AshType type)
	{
		switch (type)
		{
			case AshType.Feu:
				return "Bonus de 20 points à la STR\nBonus de 10 points à la Tactics\nPénalité: 4";
			case AshType.Eau:
				return "Bonus de 20 points à la DEX\nBonus de 10 points au Wrestling\nPénalité: 3";
			case AshType.Glace:
				return "Attaque de paralysie\nBonus de 20 points au Wrestling\nPénalité: 2";
			case AshType.Poison:
				return "Attaque de poison\nBonus de 20 points à la Tactics\nPénalité: 2";
			case AshType.Sang:
				return "Double armure\nBonus de 20 points au Wrestling\nPénalité: 2";
			case AshType.Sylvestre:
				return "Attaque de longue portée\nBonus de 20 points au Magic Resist\nPénalité: 3";
			case AshType.Terre:
				return "Bonus de 20 points à la Tactics\nBonus de 20 points au Wrestling\nPénalité: 3";
			case AshType.Vent:
				return "Double DEX\nBonus de 20 points à la Tactics\nPénalité: 2";
			default:
				return "Aucun bonus spécifique";
		}
	}

	public static void ApplyAshBonuses(GolemZyX golem, AshType ashType, int ashQuantity, double animaltamingSkill)
	{
		int baseBonus = ashQuantity * 5;
		int skillBonus = (int)(animaltamingSkill * 0.1);

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
		writer.Write(Amount);
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
		m_Type = (AshType)reader.ReadInt();
		if (version > 0)
		{
			Amount = reader.ReadInt();
		}
	}
}
