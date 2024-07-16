using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Custom
{
	public class MiniGolem : Item
	{
		private Golem m_Golem;
		private GolemAsh.AshType m_AshType;

		[CommandProperty(AccessLevel.GameMaster)]
		public Golem Golem { get { return m_Golem; } set { m_Golem = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public GolemAsh.AshType AshType { get { return m_AshType; } set { m_AshType = value; Hue = -1; } }

		public MiniGolem(Golem golem, GolemAsh.AshType ashType) : base(0x20D9) // Adjust ItemID as needed
		{
			m_Golem = golem;
			AshType = ashType;
			Name = "Mini Golem";
		}

		public MiniGolem(Serial serial) : base(serial) { }

		public override void OnDoubleClick(Mobile from)
		{
			if (m_Golem == null || m_Golem.Deleted)
			{
				from.SendMessage("Ce Mini Golem est inactif.");
				return;
			}

			if (m_Golem.ControlMaster != from)
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

		public override void OnAosSingleClick(Mobile from)
		{
			base.OnAosSingleClick(from);

			if (m_Golem != null && !m_Golem.Deleted)
			{
				from.SendMessage($"Énergie: {m_Golem.Hits}/{m_Golem.HitsMax}");
				from.SendMessage($"Statut: {(m_Golem.Summoned ? "Matérialisé" : "Dématérialisé")}");
				// Ajouter d'autres attributs ici si nécessaire
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

	public class Golem : BaseCreature
	{
		private MiniGolem m_MiniGolem;

		[CommandProperty(AccessLevel.GameMaster)]
		public MiniGolem MiniGolem { get { return m_MiniGolem; } set { m_MiniGolem = value; } }

		public Golem(GolemAsh.AshType ashType) : base(AIType.AI_Melee, FightMode.Closest, 10, 2, 0.2, 0.4)
		{
			Name = "Golem";
			Body = 14; // Adjust as needed
			BaseSoundID = 268; // Adjust as needed

			SetStr(100);
			SetDex(100);
			SetInt(100);

			SetHits(100);
			SetStam(100);

			SetDamage(10, 23);

			SetDamageType(ResistanceType.Physical, 100);

			SetResistance(ResistanceType.Physical, 35, 40);
			SetResistance(ResistanceType.Fire, 30, 40);
			SetResistance(ResistanceType.Cold, 20, 30);
			SetResistance(ResistanceType.Poison, 30, 40);
			SetResistance(ResistanceType.Energy, 30, 40);

			SetSkill(SkillName.MagicResist, 60.0, 75.0);
			SetSkill(SkillName.Tactics, 80.0, 90.0);
			SetSkill(SkillName.Wrestling, 80.0, 90.0);

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 38;

			Hue = -1;
		}

		public Golem(Serial serial) : base(serial) { }

		public override void OnDeath(Container c)
		{
			base.OnDeath(c);

			if (m_MiniGolem != null && !m_MiniGolem.Deleted)
			{
				m_MiniGolem.Delete();
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write(m_MiniGolem);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_MiniGolem = reader.ReadItem() as MiniGolem;
		}
	}

	public static class GolemExtensions
	{
		public static void SetName(this Golem golem, string name)
		{
			golem.Name = name;
			if (golem.MiniGolem != null)
			{
				golem.MiniGolem.Name = name;
			}
		}
	}
}