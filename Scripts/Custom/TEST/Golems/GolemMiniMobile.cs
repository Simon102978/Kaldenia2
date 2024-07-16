using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom
{
	public class Golem : BaseCreature
	{
		private Mobile m_Owner;

		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile Owner
		{
			get { return m_Owner; }
			set
			{
				m_Owner = value;
				if (m_Owner != null)
				{
					ControlMaster = m_Owner;
					Controlled = true;
					ControlTarget = m_Owner;
					ControlOrder = OrderType.Follow;
				}
			}
		}
		private MiniGolem m_MiniGolem;
		private GolemAsh.AshType m_AshType;

		[CommandProperty(AccessLevel.GameMaster)]
		public MiniGolem MiniGolem { get { return m_MiniGolem; } set { m_MiniGolem = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public GolemAsh.AshType AshType { get { return m_AshType; } set { m_AshType = value; } }

		[Constructable]
		public Golem(GolemAsh.AshType ashType, Mobile owner) : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Owner = owner;
			m_AshType = ashType;

			Name = $"{ashType} Golem";
			Body = 14; // Adjust as needed
			BaseSoundID = 268; // Adjust as needed

			SetStr(200);
			SetDex(200);
			SetInt(100);

			SetHits(150);
			SetMana(0);

			SetDamage(10, 23);

			SetDamageType(ResistanceType.Physical, 100);

			SetResistance(ResistanceType.Physical, 35);
			SetResistance(ResistanceType.Fire, 35);
			SetResistance(ResistanceType.Cold, 35);
			SetResistance(ResistanceType.Poison, 35);
			SetResistance(ResistanceType.Energy, 35);

			SetSkill(SkillName.MagicResist, 60.0, 75.0);
			SetSkill(SkillName.Tactics, 80.0, 90.0);
			SetSkill(SkillName.Wrestling, 80.0, 90.0);

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 38;

			Container pack = new Backpack();
			pack.Movable = false;
			AddItem(pack);
		}

		public Golem(Serial serial) : base(serial) { }

		public override bool IsScaredOfScaryThings { get { return false; } }
		public override bool IsScaryToPets { get { return true; } }

		public override bool AutoDispel { get { return true; } }
		public override bool BleedImmune { get { return true; } }
		public override Poison PoisonImmune { get { return Poison.Lethal; } }

		public override void OnDeath(Container c)
		{
			base.OnDeath(c);

			if (m_MiniGolem != null && !m_MiniGolem.Deleted)
			{
				m_MiniGolem.Delete();
			}
		}

		public override void GenerateLoot()
		{
			AddLoot(LootPack.Average);
			AddLoot(LootPack.Gems, 1);
		}

		public override int GetIdleSound()
		{
			return 268;
		}

		public override int GetAngerSound()
		{
			return 267;
		}

		public override int GetHurtSound()
		{
			return 269;
		}

		public override int GetDeathSound()
		{
			return 270;
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write(m_MiniGolem);
			writer.Write((int)m_AshType);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_MiniGolem = reader.ReadItem() as MiniGolem;
			m_AshType = (GolemAsh.AshType)reader.ReadInt();
		}
	}

	public class MiniGolem : Item
	{
		private Golem m_Golem;
		private GolemAsh.AshType m_AshType;

		[CommandProperty(AccessLevel.GameMaster)]
		public Golem Golem { get { return m_Golem; } set { m_Golem = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public GolemAsh.AshType AshType { get { return m_AshType; } set { m_AshType = value; } }

		public MiniGolem(Golem golem, GolemAsh.AshType ashType) : base(0x20D7) // Adjust ItemID as needed
		{
			m_Golem = golem;
			m_AshType = ashType;
			Name = $"Mini Golem de {ashType}";
			Hue = GetHueForAshType(ashType);
		}

		public MiniGolem(Serial serial) : base(serial) { }

		public override void OnDoubleClick(Mobile from)
		{
			if (m_Golem == null || m_Golem.Deleted)
			{
				from.SendMessage("Ce Mini Golem est inactif.");
				return;
			}

			if (m_Golem.Owner != from)
			{
				from.SendMessage("Ce n'est pas votre Golem.");
				return;
			}

			if (!m_Golem.Summoned)
			{
				m_Golem.MoveToWorld(from.Location, from.Map);
				m_Golem.Summoned = true;
				m_Golem.ControlTarget = from;
				m_Golem.ControlOrder = OrderType.Follow;
				from.SendMessage("Vous avez matérialisé votre Golem.");
			}
			else
			{
				m_Golem.Summoned = false;
				m_Golem.ControlTarget = null;
				m_Golem.ControlOrder = OrderType.None;
				m_Golem.Map = Map.Internal;
				from.SendMessage("Vous avez dématérialisé votre Golem.");
			}
		}

		private int GetHueForAshType(GolemAsh.AshType ashType)
		{
			switch (ashType)
			{
				case GolemAsh.AshType.Feu: return 1161;
				case GolemAsh.AshType.Eau: return 1153;
				case GolemAsh.AshType.Glace: return 1152;
				case GolemAsh.AshType.Poison: return 1167;
				case GolemAsh.AshType.Sang: return 1157;
				case GolemAsh.AshType.Sylvestre: return 1171;
				case GolemAsh.AshType.Terre: return 1147;
				case GolemAsh.AshType.Vent: return 1154;
				default: return 0;
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write(m_Golem);
			writer.Write((int)m_AshType);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Golem = reader.ReadMobile() as Golem;
			m_AshType = (GolemAsh.AshType)reader.ReadInt();
		}
	}
}
