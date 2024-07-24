using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Commands;

namespace Server.Items
{
	public class AnimalTalisman : BaseTalisman
	{
		private Dictionary<AnimalBuffType, Timer> m_Timers;
		private Mobile m_Owner;
		private List<BaseCreature> m_BuffedPets;
		private HashSet<AnimalBuffType> m_ActiveBuffs;

		public enum AnimalBuffType
		{
			Lion,
			Ours,
			Aigle,
			Chat
		}

		[Constructable]
		public AnimalTalisman() : base(0x2F59)
		{
			Hue = -1;
			Name = "Animal Talisman";
			Visible = true;
			Movable = false;
			LootType = LootType.Blessed;

			m_Timers = new Dictionary<AnimalBuffType, Timer>();
			m_BuffedPets = new List<BaseCreature>();
			m_ActiveBuffs = new HashSet<AnimalBuffType>();
		}

		public AnimalTalisman(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0); // version
			writer.Write(m_ActiveBuffs.Count);
			foreach (var buff in m_ActiveBuffs)
			{
				writer.Write((int)buff);
			}
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			int count = reader.ReadInt();
			m_ActiveBuffs = new HashSet<AnimalBuffType>();
			for (int i = 0; i < count; i++)
			{
				m_ActiveBuffs.Add((AnimalBuffType)reader.ReadInt());
			}
			m_Timers = new Dictionary<AnimalBuffType, Timer>();
			m_BuffedPets = new List<BaseCreature>();
		}

		public static void Initialize()
		{
			CommandSystem.Register("lion", AccessLevel.Player, new CommandEventHandler(OnLionCommand));
			CommandSystem.Register("ours", AccessLevel.Player, new CommandEventHandler(OnBearCommand));
			CommandSystem.Register("aigle", AccessLevel.Player, new CommandEventHandler(OnHawkCommand));
			CommandSystem.Register("chat", AccessLevel.Player, new CommandEventHandler(OnCatCommand));
		}

		private static void OnLionCommand(CommandEventArgs e) { UseAnimalSkill(e.Mobile, AnimalBuffType.Lion, 15); }
		private static void OnBearCommand(CommandEventArgs e) { UseAnimalSkill(e.Mobile, AnimalBuffType.Ours, 30); }
		private static void OnHawkCommand(CommandEventArgs e) { UseAnimalSkill(e.Mobile, AnimalBuffType.Aigle, 45); }
		private static void OnCatCommand(CommandEventArgs e) { UseAnimalSkill(e.Mobile, AnimalBuffType.Chat, 60); }

		private static void UseAnimalSkill(Mobile from, AnimalBuffType buffType, double requiredSkill)
		{
			if (from.Skills[SkillName.AnimalTaming].Base < requiredSkill)
			{
				from.SendMessage($"Vous avez besoin d'au moins {requiredSkill}% en Animal Taming pour utiliser cette compétence.");
				return;
			}

			if (from.Stam < 25)
			{
				from.SendMessage("Vous n'avez pas assez de stamina pour utiliser cette compétence.");
				return;
			}

			from.Stam -= 25;

			double skillValue = from.Skills[SkillName.AnimalTaming].Base;
			TimeSpan duration = TimeSpan.FromSeconds(120 + (skillValue - 10) * 1.8); // 2 minutes à 10 skill, 5 minutes à 100 skill

			AnimalTalisman talisman = from.FindItemOnLayer(Layer.Talisman) as AnimalTalisman;
			if (talisman == null)
			{
				talisman = new AnimalTalisman();
				if (from.EquipItem(talisman))
				{
					talisman.m_Owner = from;
				}
				else
				{
					from.SendMessage("Impossible d'équiper le talisman.");
					talisman.Delete();
					return;
				}
			}

			talisman.UpdateBuff(buffType, duration);
			from.SendMessage($"Vous utilisez la compétence {buffType}!");
		}

		private void UpdateBuff(AnimalBuffType buffType, TimeSpan duration)
		{
			if (m_Timers.ContainsKey(buffType))
			{
				m_Timers[buffType].Stop();
			}

			if (!m_ActiveBuffs.Contains(buffType))
			{
				m_ActiveBuffs.Add(buffType);
				ApplyBuff(buffType);
			}

			m_Timers[buffType] = Timer.DelayCall(duration, () => EndBuff(buffType));
			UpdateTalismanProperties();
		}

		private void ApplyBuff(AnimalBuffType buffType)
		{
			foreach (Mobile m in m_Owner.GetMobilesInRange(18))
			{
				if (m is BaseCreature pet && pet.Controlled && pet.ControlMaster == m_Owner && pet.Alive)
				{
					switch (buffType)
					{
						case AnimalBuffType.Lion:
							pet.PhysicalDamage += 15;
							break;
						case AnimalBuffType.Ours:
							pet.Skills[SkillName.Wrestling].Base += 10;
							pet.Str += 10;
							break;
						case AnimalBuffType.Aigle:
							pet.Skills[SkillName.Tactics].Base += 10;
							pet.Int += 10;
							break;
						case AnimalBuffType.Chat:
							pet.Skills[SkillName.MagicResist].Base += 10;
							pet.Dex += 10;
							break;
					}
					if (!m_BuffedPets.Contains(pet))
					{
						m_BuffedPets.Add(pet);
					}
				}
			}
		}

		private void RemoveBuff(AnimalBuffType buffType)
		{
			foreach (BaseCreature pet in m_BuffedPets)
			{
				if (pet.Alive)
				{
					switch (buffType)
					{
						case AnimalBuffType.Lion:
							pet.PhysicalDamage -= 15;
							break;
						case AnimalBuffType.Ours:
							pet.Skills[SkillName.Wrestling].Base -= 10;
							pet.Str -= 10;
							break;
						case AnimalBuffType.Aigle:
							pet.Skills[SkillName.Tactics].Base -= 10;
							pet.Int -= 10;
							break;
						case AnimalBuffType.Chat:
							pet.Skills[SkillName.MagicResist].Base -= 10;
							pet.Dex -= 10;
							break;
					}
				}
			}
		}

		private void EndBuff(AnimalBuffType buffType)
		{
			if (m_ActiveBuffs.Contains(buffType))
			{
				m_ActiveBuffs.Remove(buffType);
				RemoveBuff(buffType);
				m_Owner.SendMessage($"L'effet de la compétence {buffType} s'estompe.");
				UpdateTalismanProperties();
			}

			if (m_ActiveBuffs.Count == 0)
			{
				m_Owner.RemoveItem(this);
				Delete();
			}
		}

		private void UpdateTalismanProperties()
		{
			Attributes.WeaponDamage = m_ActiveBuffs.Contains(AnimalBuffType.Lion) ? 15 : 0;
			Attributes.DefendChance = m_ActiveBuffs.Contains(AnimalBuffType.Ours) ? 10 : 0;
			Attributes.AttackChance = m_ActiveBuffs.Contains(AnimalBuffType.Aigle) ? 10 :0;
			Attributes.WeaponSpeed  = m_ActiveBuffs.Contains(AnimalBuffType.Chat) ? 10 : 0;
		}
	}
}
