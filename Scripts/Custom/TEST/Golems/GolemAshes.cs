using System;
using Server;

namespace Server.Custom
{
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
		private int m_Amount;

		[CommandProperty(AccessLevel.GameMaster)]
		public AshType Type { get { return m_Type; } set { m_Type = value; InvalidateProperties(); } }

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
			list.Add($"Quantité: {m_Amount}");
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

		public static void ApplyAshBonuses(Golem golem, AshType ashType, int ashQuantity, int inscribeSkill)
		{
			int baseBonus = ashQuantity * 7;
			int skillBonus = (int)(inscribeSkill * 0.1); // 10% de la compétence Inscribe

			switch (ashType)
			{
				case AshType.Feu:
					golem.SetStr(golem.Str + baseBonus + 20);
					golem.Skills[SkillName.Tactics].Base += 10 + skillBonus;
					golem.SetDamageType(ResistanceType.Fire, 100);
					golem.SetResistance(ResistanceType.Fire, 50);
					break;

				case AshType.Eau:
					golem.SetDex(golem.Dex + baseBonus + 20);
					golem.Skills[SkillName.Wrestling].Base += 10 + skillBonus;
					golem.SetResistance(ResistanceType.Fire, 50);
					golem.SetResistance(ResistanceType.Cold, 50);
					break;

				case AshType.Glace:
					golem.SetInt(golem.Int + baseBonus + 20);
					golem.Skills[SkillName.MagicResist].Base += 10 + skillBonus;
					golem.SetDamageType(ResistanceType.Cold, 100);
					golem.SetResistance(ResistanceType.Cold, 75);
					break;

				case AshType.Poison:
					golem.SetStr(golem.Str + baseBonus + 10);
					golem.SetDex(golem.Dex + baseBonus + 10);
					golem.Skills[SkillName.Poisoning].Base += 20 + skillBonus;
					golem.SetDamageType(ResistanceType.Poison, 100);
					golem.SetResistance(ResistanceType.Poison, 75);
					break;

				case AshType.Sang:
					golem.SetHits(golem.HitsMax + baseBonus * 3);
					golem.Skills[SkillName.Anatomy].Base += 20 + skillBonus;
					golem.SetResistance(ResistanceType.Physical, 50);
					break;

				case AshType.Sylvestre:
					golem.SetDex(golem.Dex + baseBonus + 30);
					golem.Skills[SkillName.Archery].Base += 20 + skillBonus;
					golem.SetResistance(ResistanceType.Energy, 50);
					break;

				case AshType.Terre:
					golem.SetStr(golem.Str + baseBonus + 30);
					golem.Skills[SkillName.Macing].Base += 20 + skillBonus;
					golem.SetResistance(ResistanceType.Physical, 75);
					break;

				case AshType.Vent:
					golem.SetDex(golem.Dex + baseBonus * 2);
					golem.Skills[SkillName.Tactics].Base += 15 + skillBonus;
					golem.Skills[SkillName.Wrestling].Base += 15 + skillBonus;
					golem.SetResistance(ResistanceType.Energy, 75);
					break;

				default:
					// Bonus par défaut si le type de cendre n'est pas reconnu
					golem.SetStr(golem.Str + baseBonus);
					golem.SetDex(golem.Dex + baseBonus);
					golem.SetInt(golem.Int + baseBonus);
					break;
			}

			// Appliquez des bonus généraux basés sur la quantité de cendres
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
}
