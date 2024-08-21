using Server.Items;
using Server.Multis;
using Server.Targeting;
using System;

namespace Server.Mobiles
{
    public class RisingColossus : BaseCreature
    {
        private int m_DispelDifficulty;
		private Mobile m_Controller;
		public override double DispelDifficulty => m_DispelDifficulty;
        public override double DispelFocus => 45.0;

        [Constructable]
        public RisingColossus(Mobile m, double baseskill, double boostskill)
            : base(AIType.AI_Mystic, FightMode.Closest, 10, 1, 0.4, 0.5)
        {
            int level = (int)(baseskill + boostskill);
            int statbonus = (int)((baseskill - 83) / 1.3 + ((boostskill - 30) / 1.3) + 6);
            int hitsbonus = (int)((baseskill - 83) * 1.14 + ((boostskill - 30) * 1.03) + 20);
            double skillvalue = boostskill != 0 ? ((baseskill + boostskill) / 2) : ((baseskill + 20) / 2);

            Name = "a rising colossus";
            Body = 829;
			m_Controller = m;


			SetHits(315 + hitsbonus);

            SetStr(677 + statbonus);
            SetDex(107 + statbonus);
            SetInt(127 + statbonus);

            SetDamage(level / 12, level / 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 65, 70);
            SetResistance(ResistanceType.Fire, 50, 55);
            SetResistance(ResistanceType.Cold, 50, 55);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 65, 70);

            SetSkill(SkillName.MagicResist, skillvalue);
            SetSkill(SkillName.Tactics, skillvalue);
            SetSkill(SkillName.Wrestling, skillvalue);
            SetSkill(SkillName.Anatomy, skillvalue);
            SetSkill(SkillName.Mysticism, skillvalue);
            SetSkill(SkillName.Tracking, 70.0);
            SetSkill(SkillName.EvalInt, skillvalue);
            SetSkill(SkillName.Mysticism, m.Skills[SkillName.Mysticism].Value);
            SetSkill(SkillName.Focus, m.Skills[SkillName.Focus].Value);

            ControlSlots = 3;

            m_DispelDifficulty = 91 + (int)((baseskill * 83) / 5.2);

            SetWeaponAbility(WeaponAbility.ArmorIgnore);
            SetWeaponAbility(WeaponAbility.CrushingBlow);
        }

        public override double GetFightModeRanking(Mobile m, FightMode acqType, bool bPlayerOnly)
        {
            return (m.Int + m.Skills[SkillName.Magery].Value) / Math.Max(GetDistanceToSqrt(m), 1.0);
        }
		public override bool CheckTarget(Mobile from, Target targ, object targeted)
		{
			if (from != m_Controller)
				return false;
			return base.CheckTarget(from, targ, targeted);
		}
		public override void OnThink()
		{
			base.OnThink();
			if (m_Controller != null && !m_Controller.Deleted && m_Controller.Alive)
			{
				if (Combatant == null)
				{
					Mobile closest = null;
					int detectionRange = 10; // Distance maximale de détection en cases
					double closestDist = double.MaxValue;
					foreach (Mobile m in m_Controller.GetMobilesInRange(detectionRange))
					{
						if (m != m_Controller && m != this && CanBeHarmful(m))
						{
							double dist = GetDistanceToSqrt(m);
							if (dist < closestDist)
							{
								closest = m;
								closestDist = dist;
							}
						}
					}
					if (closest != null)
					{
						Combatant = closest;
					}
				}
			}
			else
			{
				Delete();
			}
		}
		public override bool DeleteCorpseOnDeath => true;
		public override bool IsHouseSummonable => true;
		public override bool AlwaysMurderer => true;

        public override bool BleedImmune => true;

        public override Poison PoisonImmune => Poison.Lethal;

        public RisingColossus(Serial serial) : base(serial)
        {
        }

        public override int GetAttackSound()
        {
            return 0x627;
        }

        public override int GetHurtSound()
        {
            return 0x629;
        }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);
			writer.Write(m_DispelDifficulty);
			writer.Write(m_Controller);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_DispelDifficulty = reader.ReadInt();
			if (version >= 0)
			{
				m_Controller = reader.ReadMobile();
			}
		}
	}
}
