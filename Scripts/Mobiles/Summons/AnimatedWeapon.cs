using System;
using Server.Targeting;

namespace Server.Mobiles
{
    [CorpseName("an animated weapon corpse")]
    public class AnimatedWeapon : BaseCreature
    {

		private Mobile m_Controller;
		public override bool DeleteCorpseOnDeath => true;
        public override bool IsHouseSummonable => true;

        public override double DispelDifficulty => 0.0;
        public override double DispelFocus => 20.0;

        [Constructable]
        public AnimatedWeapon(Mobile caster, int level)
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.3, 0.6)
        {
            Name = "an animated weapon";
            Body = 692;

            SetStr(10 + level);
            SetDex(10 + level);
            SetInt(10);

            SetHits(20 + (level * 3 / 2));
            SetStam(10 + level);
            SetMana(0);

			m_Controller = caster;

			if (level >= 120)
                SetDamage(14, 18);
            else if (level >= 105)
                SetDamage(13, 17);
            else if (level >= 90)
                SetDamage(12, 15);
            else if (level >= 75)
                SetDamage(11, 14);
            else if (level >= 60)
                SetDamage(10, 12);
            else if (level >= 45)
                SetDamage(9, 11);
            else if (level >= 30)
                SetDamage(8, 9);
            else
                SetDamage(7, 8);

            SetDamageType(ResistanceType.Physical, 60);
            SetDamageType(ResistanceType.Poison, 20);
            SetDamageType(ResistanceType.Energy, 20);

            SetResistance(ResistanceType.Physical, 40, 50);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, level);
            SetSkill(SkillName.Tactics, caster.Skills[SkillName.Magery].Value / 2);
            SetSkill(SkillName.Wrestling, level);
            SetSkill(SkillName.Anatomy, caster.Skills[SkillName.EvalInt].Value / 2);
            SetSkill(SkillName.Tracking, 40, 50);

            Fame = 0;
            Karma = 0;

            ControlSlots = 4;
        }

        public override int GetAngerSound()
        {
            return 0x23A;
        }

        public override int GetAttackSound()
        {
            return 0x3B8;
        }

        public override int GetHurtSound()
        {
            return 0x23A;
        }

        public AnimatedWeapon(Serial serial) : base(serial)
        {
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
		public override bool CheckTarget(Mobile from, Target targ, object targeted)
		{
			if (from != m_Controller)
				return false;

			return base.CheckTarget(from, targ, targeted);
		}
		public override double GetFightModeRanking(Mobile m, FightMode acqType, bool bPlayerOnly)
        {
            return (m.Str + m.Skills[SkillName.Magery].Value) / Math.Max(GetDistanceToSqrt(m), 1.0);
        }

        public override bool AlwaysMurderer => true;
        public override bool BleedImmune => true;
        public override Poison PoisonImmune => Poison.Lethal;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }
    }
}
