using System;
using Server;
using Server.Mobiles;

namespace Server.Custom
{
	public class GolemAsh : Item
	{
		public enum AshType
		{
			Fire,
			Water,
			Ice,
			Poison,
			Blood,
			Sylvan,
			Earth,
			Wind
		}

		private AshType m_Type;
		private int m_Quantity;

		[CommandProperty(AccessLevel.GameMaster)]
		public AshType Type { get { return m_Type; } set { m_Type = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int Quantity { get { return m_Quantity; } set { m_Quantity = value; InvalidateProperties(); } }

		[Constructable]
		public GolemAsh(AshType type, int quantity) : base(0x26B8) // Adjust ItemID as needed
		{
			m_Type = type;
			m_Quantity = quantity;
			Name = $"Cendres de {type}";
			Hue = GetHueForType(type);
		}

		public GolemAsh(Serial serial) : base(serial)
		{
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add($"Quantité: {m_Quantity}");
		}

		private int GetHueForType(AshType type)
		{
			switch (type)
			{
				case AshType.Fire: return 1161;   // Red
				case AshType.Water: return 1153;  // Blue
				case AshType.Ice: return 1152;    // Light Blue
				case AshType.Poison: return 1167; // Green
				case AshType.Blood: return 1157;  // Dark Red
				case AshType.Sylvan: return 1171; // Green
				case AshType.Earth: return 1147;  // Brown
				case AshType.Wind: return 1154;   // White
				default: return 0;
			}
		}

		public static void ApplyAshBonuses(BaseCreature golem, AshType ashType, int quantity, int enchantmentSkill)
		{
			int energy = quantity * 7;

			// Ajuster les points de vie maximum du Golem
			golem.SetHits(energy);

			// Assurez-vous que les points de vie actuels sont égaux aux points de vie maximum
			golem.Hits = golem.HitsMax;

			switch (ashType)
			{
				case AshType.Fire:
					golem.Str += 20;
					golem.Skills[SkillName.Tactics].Base += 10;
					ApplyPenalty(golem, 4);
					break;
				case AshType.Water:
					golem.Dex += 20;
					golem.Skills[SkillName.Wrestling].Base += 10;
					ApplyPenalty(golem, 3);
					break;
				case AshType.Ice:
					// Ajouter la logique d'attaque de paralysie ici
					golem.Skills[SkillName.Wrestling].Base += 20;
					ApplyPenalty(golem, 2);
					break;
				case AshType.Poison:
					// Ajouter la logique d'attaque de poison ici
					golem.Skills[SkillName.Tactics].Base += 20;
					ApplyPenalty(golem, 2);
					break;
				case AshType.Blood:
					golem.VirtualArmor *= 2;
					golem.Skills[SkillName.Wrestling].Base += 20;
					ApplyPenalty(golem, 2);
					break;
				case AshType.Sylvan:
					// Ajouter la logique d'attaque à longue portée ici
					golem.Skills[SkillName.MagicResist].Base += 20;
					ApplyPenalty(golem, 3);
					break;
				case AshType.Earth:
					golem.Skills[SkillName.Tactics].Base += 20;
					golem.Skills[SkillName.Wrestling].Base += 20;
					ApplyPenalty(golem, 3);
					break;
				case AshType.Wind:
					golem.Dex *= 2;
					golem.Skills[SkillName.Tactics].Base += 20;
					ApplyPenalty(golem, 2);
					break;
			}
		}

		private static void ApplyPenalty(BaseCreature golem, int penalty)
		{
			golem.Str /= penalty;
			golem.Dex /= penalty;
			golem.Int /= penalty;
			for (int i = 0; i < golem.Skills.Length; i++)
			{
				golem.Skills[i].Base /= penalty;
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Type);
			writer.Write(m_Quantity);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Type = (AshType)reader.ReadInt();
			m_Quantity = reader.ReadInt();
		}
	}
}