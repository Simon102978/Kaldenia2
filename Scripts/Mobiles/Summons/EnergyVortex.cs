using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{


    [CorpseName("Le corps d'un elementaire")]
    public class EnergyVortex : BaseCreature
    {
        [Constructable]
        public EnergyVortex() : this(false)
        {
        }

		private DateTime m_lastTargetSearch = DateTime.MinValue;


		[Constructable]
        public EnergyVortex(bool summoned)
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "un elementaire de vent";

            if (0.002 > Utility.RandomDouble()) // Per OSI FoF, it's a 1/500 chance.
            {
                // Llama vortex!
                Body = 0xDC;
                Hue = -1;
            }
            else
            {
                Body = 162;
            }

            bool weak = summoned && Siege.SiegeShard;

            SetStr(weak ? 100 : 200);
            SetDex(weak ? 150 : 200);
            SetInt(100);

            SetHits(!weak ? 140 : 70);
            SetStam(250);
            SetMana(0);

            SetDamage(weak ? 10 : 14, weak ? 13 : 17);

            SetDamageType(ResistanceType.Physical, 0);
            SetDamageType(ResistanceType.Energy, 100);

            SetResistance(ResistanceType.Physical, 60, 70);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 90, 100);

            SetSkill(SkillName.MagicResist, 99.9);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 120.0);

            Fame = 0;
            Karma = 0;

            ControlSlots = 2;
        }

        public EnergyVortex(Serial serial)
            : base(serial)
        {
        }

        public override bool DeleteCorpseOnDeath => Summoned;

        public override bool AlwaysMurderer => true; // Or Llama vortices will appear gray.

        public override double DispelDifficulty => 80.0;

        public override double DispelFocus => 20.0;

        public override bool BleedImmune => true;

        public override Poison PoisonImmune => Poison.Lethal;

      
        public override int GetAngerSound()
        {
            return 0x15;
        }

        public override int GetAttackSound()
        {
            return 0x28;
        }
		public override void GenerateLoot()
		{
			AddLoot(LootPack.Average);
			AddLoot(LootPack.Meager);
			AddLoot(LootPack.LootItem<SulfurousAsh>(3, true));
			AddLoot(LootPack.LootItem<GolemCendreVent>(Utility.RandomMinMax(1,5)));

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

