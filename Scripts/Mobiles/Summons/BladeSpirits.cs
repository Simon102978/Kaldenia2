using System;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName("a blade spirit corpse")]
	public class BladeSpirits : BaseCreature
	{
		private DateTime m_lastTargetSearch = DateTime.MinValue;


		[Constructable]
		public BladeSpirits()
			: this(false)
		{
		}

		[Constructable]
		public BladeSpirits(bool summoned)
			: base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.3, 0.6)
		{
			this.Name = "a blade spirit";
			this.Body = 574;

			bool weak = summoned && Siege.SiegeShard;

			this.SetStr(weak ? 100 : 150);
			this.SetDex(weak ? 100 : 150);
			this.SetInt(100);

			this.SetHits((Core.SE && !weak) ? 160 : 80);
			this.SetStam(250);
			this.SetMana(0);

			this.SetDamage(10, 14);

			this.SetDamageType(ResistanceType.Physical, 60);
			this.SetDamageType(ResistanceType.Poison, 20);
			this.SetDamageType(ResistanceType.Energy, 20);

			this.SetResistance(ResistanceType.Physical, 30, 40);
			this.SetResistance(ResistanceType.Fire, 40, 50);
			this.SetResistance(ResistanceType.Cold, 30, 40);
			this.SetResistance(ResistanceType.Poison, 100);
			this.SetResistance(ResistanceType.Energy, 20, 30);

			this.SetSkill(SkillName.MagicResist, 70.0);
			this.SetSkill(SkillName.Tactics, 90.0);
			this.SetSkill(SkillName.Wrestling, 90.0);

			this.Fame = 0;
			this.Karma = 0;

			this.VirtualArmor = 40;
			this.ControlSlots = (Core.SE) ? 2 : 2;
		}

		public BladeSpirits(Serial serial)
			: base(serial)
		{
		}
		public override bool CanBeHarmful(IDamageable target)
		{
			return base.CanBeHarmful(target);
		}
		
		
		public override bool DeleteCorpseOnDeath
		{
			get
			{
				return Core.AOS;
			}
		}
		public override bool IsHouseSummonable
		{
			get
			{
				return true;
			}
		}
		public override double DispelDifficulty
		{
			get
			{
				return 0.0;
			}
		}
		public override double DispelFocus
		{
			get
			{
				return 20.0;
			}
		}
		public override bool BleedImmune
		{
			get
			{
				return true;
			}
		}
		public override Poison PoisonImmune
		{
			get
			{
				return Poison.Lethal;
			}
		}
		public override double GetFightModeRanking(Mobile m, FightMode acqType, bool bPlayerOnly)
		{
			return (m.Str + m.Skills[SkillName.Tactics].Value) / Math.Max(this.GetDistanceToSqrt(m), 1.0);
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

		public override void OnThink()
		{
			base.OnThink();

			if (Combatant == null || !Combatant.Alive || Combatant.Deleted)
			{
				if (DateTime.UtcNow - m_lastTargetSearch > TimeSpan.FromSeconds(2))
				{
					FindNewTarget();
					m_lastTargetSearch = DateTime.UtcNow;
				}
			}
		}

		private void FindNewTarget()
		{
			Mobile closestMobile = null;
			double closestDistance = double.MaxValue;

			IPooledEnumerable eable = GetMobilesInRange(10);
			foreach (Mobile m in eable)
			{
				if (m != this && m.Alive && !m.IsDeadBondedPet && CanBeHarmful(m))
				{
					double distance = GetDistanceToSqrt(m);
					if (distance < closestDistance)
					{
						closestMobile = m;
						closestDistance = distance;
					}
				}
			}
			eable.Free();

			if (closestMobile != null)
			{
				Combatant = closestMobile;
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}