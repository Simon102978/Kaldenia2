using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("un corps d'eclaireuse fourifeu")]
    public class RedSolenWorker : BaseCreature, IRedSolen
    {
        [Constructable]
        public RedSolenWorker()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "une eclaireuse fourifeu";
            Body = 781;
            BaseSoundID = 959;

            SetStr(96, 120);
            SetDex(81, 105);
            SetInt(36, 60);

            SetHits(58, 72);

            SetDamage(5, 7);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 30);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, 60.0);
            SetSkill(SkillName.Tactics, 65.0);
            SetSkill(SkillName.Wrestling, 60.0);

            Fame = 1500;
            Karma = -1500;
        }

		public override int Hides => Utility.RandomMinMax(1, 5);

		public override HideType HideType => HideType.Arachnide;


		public override int Bones => Utility.RandomMinMax(1, 5);
		public override BoneType BoneType => BoneType.Arachnide;
		public override void GenerateLoot()
        {
            AddLoot(LootPack.Gems, 1, 2);
			AddLoot(LootPack.Others, Utility.RandomMinMax(0, 2));

		}

		public RedSolenWorker(Serial serial)
            : base(serial)
        {
        }

        public override int GetAngerSound()
        {
            return 0x269;
        }

        public override int GetIdleSound()
        {
            return 0x269;
        }

        public override int GetAttackSound()
        {
            return 0x186;
        }

        public override int GetHurtSound()
        {
            return 0x1BE;
        }

        public override int GetDeathSound()
        {
            return 0x8E;
        }

        public override bool IsEnemy(Mobile m)
        {
            if (SolenHelper.CheckRedFriendship(m))
                return false;
            else
                return base.IsEnemy(m);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            SolenHelper.OnRedDamage(from);

            base.OnDamage(amount, from, willKill);
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
}
