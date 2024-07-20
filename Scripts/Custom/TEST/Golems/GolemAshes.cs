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
using Server.Prompts;

public class GolemAsh : Item, ICommodity
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
	public GolemAsh(AshType type, int amount) : base(0x0F7C)
	{
		Name = "Cendres Élémentaires";
		Stackable = true;
		m_Type = type;
		Amount = amount;
		Hue = GetHueForType(type);
		Weight = 0.1;
	}

	public GolemAsh(Serial serial) : base(serial)
	{
	}
	TextDefinition ICommodity.Description => LabelNumber;
	bool ICommodity.IsDeedable => true;

	public override void AddNameProperty(ObjectPropertyList list)
	{
		if (Amount > 1)
			list.Add(1050039, "{0}\t{1}", Amount.ToString(), $"Cendres Élémentaires [{m_Type}]"); // ~1_NUMBER~ ~2_ITEMNAME~
		else
			list.Add($"Cendres Élémentaires [{m_Type}]");
	}
	public override void GetProperties(ObjectPropertyList list)
	{
		base.GetProperties(list);
		list.Add($"Type: {m_Type}");
	

	//list.Add($"Quantité: {Amount}");
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
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
		m_Type = (AshType)reader.ReadInt();
	}
	public override bool OnDragLift(Mobile from)
	{
		return true;
	}

	public override bool OnDragDrop(Mobile from, Item dropped)
	{
		GolemAsh ash = dropped as GolemAsh;

		if (ash != null && ash.Type == this.Type)
		{
			Amount += ash.Amount;
			ash.Delete();
			return true;
		}

		return false;
	}

	public override void OnDoubleClick(Mobile from)
	{
		if (!Movable)
			return;

		if (Amount > 1)
		{
			from.SendMessage("Entrez la quantité que vous souhaitez prendre :");
			from.Prompt = new InternalPrompt(this);
		}
		else
		{
			// Comportement par défaut pour un seul objet
			if (!from.InRange(GetWorldLocation(), 1))
				from.SendLocalizedMessage(500446); // That is too far away.
			else
				from.SendAsciiMessage($"Vous prenez {Amount} cendre{(Amount > 1 ? "s" : "")} de type {m_Type}.");
		}
	}

	private class InternalPrompt : Prompt
	{
		private GolemAsh m_Ash;

		public InternalPrompt(GolemAsh ash)
		{
			m_Ash = ash;
		}

		public override void OnResponse(Mobile from, string text)
		{
			if (!m_Ash.Deleted && m_Ash.IsAccessibleTo(from))
			{
				if (int.TryParse(text, out int amount))
				{
					if (amount < 1 || amount > m_Ash.Amount)
					{
						from.SendMessage("Quantité invalide.");
					}
					else if (amount == m_Ash.Amount)
					{
						from.SendMessage("Vous prenez toutes les cendres.");
						from.AddToBackpack(m_Ash);
					}
					else
					{
						m_Ash.Amount -= amount;
						GolemAsh newAsh = new GolemAsh(m_Ash.Type, amount);
						from.AddToBackpack(newAsh);
						from.SendMessage($"Vous prenez {amount} cendres.");
					}
				}
				else
				{
					from.SendMessage("Quantité invalide. Veuillez entrer un nombre.");
				}
			}
		}

	

		public override void OnCancel(Mobile from)
		{
			from.SendLocalizedMessage(502980); // Message canceled.
		}
	}

	public override void OnAfterDuped(Item newItem)
	{
		GolemAsh newAsh = newItem as GolemAsh;
		if (newAsh != null)
		{
			newAsh.Type = this.Type;
		}
	}








}
