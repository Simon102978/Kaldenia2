using Server.Items;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un loup")]
    public class LeatherWolf : BaseCreature, IRepairableMobile
    {
        public Type RepairResource => typeof(IronIngot);

        private const int MaxFellows = 3;

        private readonly List<Mobile> m_Fellows = new List<Mobile>();
        private Timer m_FellowsTimer;

        [Constructable]
        public LeatherWolf()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "un loup";
            Body = 739;
            BaseSoundID = 0xE5;

            SetStr(104, 125);
            SetDex(102, 125);
            SetInt(20, 34);

            SetHits(291, 329);

            SetDamage(12, 23);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 40, 49);
            SetResistance(ResistanceType.Fire, 20, 29);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 21, 29);
            SetResistance(ResistanceType.Energy, 20, 25);

            SetSkill(SkillName.MagicResist, 79.5, 94.9);
            SetSkill(SkillName.Tactics, 80.6, 89.4);
            SetSkill(SkillName.Wrestling, 70.9, 88.4);

            Fame = 4500;
            Karma = -4500;

            Tamable = false;
            SetWeaponAbility(WeaponAbility.BleedAttack);
        }

        public LeatherWolf(Serial serial)
            : base(serial)
        {
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (!Controlled && 0.2 > Utility.RandomDouble())
                c.DropItem(new LeatherWolfSkin());
        }

        public override void OnCombatantChange()
        {
            if (Combatant != null && m_FellowsTimer == null)
            {
                m_FellowsTimer = new InternalTimer(this);
                m_FellowsTimer.Start();
            }
        }

        public void CheckFellows()
        {
            if (!Alive || Combatant == null || Map == null || Map == Map.Internal)
            {
                m_Fellows.ForEach(f => f.Delete());
                m_Fellows.Clear();

                m_FellowsTimer.Stop();
                m_FellowsTimer = null;
            }
            else
            {
                for (int i = 0; i < m_Fellows.Count; i++)
                {
                    Mobile friend = m_Fellows[i];

                    if (friend.Deleted)
                        m_Fellows.Remove(friend);
                }

                bool spawned = false;

                for (int i = m_Fellows.Count; i < MaxFellows; i++)
                {
                    BaseCreature friend = new LeatherWolfFellow();

                    friend.MoveToWorld(Map.GetSpawnPosition(Location, 6), Map);
                    friend.Combatant = Combatant;

                    if (friend.AIObject != null)
                        friend.AIObject.Action = ActionType.Combat;

                    m_Fellows.Add(friend);

                    spawned = true;
                }

                if (spawned)
                {
                    Say(1113132); // The leather wolf howls for help
                    PlaySound(0xE6);
                }
            }
        }

        private class InternalTimer : Timer
        {
            private readonly LeatherWolf m_Owner;

            public InternalTimer(LeatherWolf owner)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(30.0))
            {
                m_Owner = owner;
            }

            protected override void OnTick()
            {
                m_Owner.CheckFellows();
            }
        }

        public override bool AlwaysMurderer => true;

        public override int Meat => Utility.RandomMinMax(2, 5);
		public override PackInstinct PackInstinct => PackInstinct.Canine;

		public override int Hides => Utility.RandomMinMax(2, 5);
		public override HideType HideType => HideType.Lupus;


		public override int Bones => Utility.RandomMinMax(2, 5);

		public override BoneType BoneType => BoneType.Lupus;
		public override FoodType FavoriteFood => FoodType.Meat;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager, 2);
			AddLoot(LootPack.LootItem<PoilsLoup>(3, 7));

		}

		public override int GetIdleSound()
        {
            return 1545;
        }

        public override int GetAngerSound()
        {
            return 1542;
        }

        public override int GetHurtSound()
        {
            return 1544;
        }

        public override int GetDeathSound()
        {
            return 1543;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class LeatherWolfFellow : BaseCreature
    {
        [Constructable]
        public LeatherWolfFellow()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a leather wolf";
            Body = 739;
            BaseSoundID = 0xE5;

            SetStr(105, 115);
            SetDex(101, 114);
            SetInt(23, 34);

            SetHits(81, 110);

            SetDamage(9, 20);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 36, 50);
            SetResistance(ResistanceType.Fire, 10, 18);
            SetResistance(ResistanceType.Cold, 23, 29);
            SetResistance(ResistanceType.Poison, 10, 17);
            SetResistance(ResistanceType.Energy, 10, 15);

            SetSkill(SkillName.MagicResist, 59.2, 75);
            SetSkill(SkillName.Tactics, 53.3, 64.8);
            SetSkill(SkillName.Wrestling, 64, 79);

            Fame = 2500;
            Karma = -2500;
        }

        public override PackInstinct PackInstinct => PackInstinct.Canine;

        public LeatherWolfFellow(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            /*int version = */
            reader.ReadInt();
        }
    }
}