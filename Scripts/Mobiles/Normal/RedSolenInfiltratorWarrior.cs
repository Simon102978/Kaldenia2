using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("un corps d'espionne fourifeu")]
    public class RedSolenInfiltratorWarrior : BaseCreature, IRedSolen
    {
        [Constructable]
        public RedSolenInfiltratorWarrior()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "une espionne fourifeu";
            Body = 782;
            BaseSoundID = 959;

            SetStr(206, 230);
            SetDex(121, 145);
            SetInt(66, 90);

            SetHits(96, 107);

            SetDamage(5, 15);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Poison, 20);

            SetResistance(ResistanceType.Physical, 20, 35);
            SetResistance(ResistanceType.Fire, 20, 35);
            SetResistance(ResistanceType.Cold, 10, 25);
            SetResistance(ResistanceType.Poison, 20, 35);
            SetResistance(ResistanceType.Energy, 10, 25);

            SetSkill(SkillName.MagicResist, 80.0);
            SetSkill(SkillName.Tactics, 80.0);
            SetSkill(SkillName.Wrestling, 80.0);

            Fame = 3000;
            Karma = -3000;
        }

		public override int Hides => Utility.RandomMinMax(1, 5);
		public override HideType HideType => HideType.Arachnide;


		public override int Bones => Utility.RandomMinMax(1, 5);
		public override BoneType BoneType => BoneType.Arachnide;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));


		}

		public RedSolenInfiltratorWarrior(Serial serial)
            : base(serial)
        {
        }

        public override int GetAngerSound()
        {
            return 0xB5;
        }

        public override int GetIdleSound()
        {
            return 0xB5;
        }

        public override int GetAttackSound()
        {
            return 0x289;
        }

        public override int GetHurtSound()
        {
            return 0xBC;
        }

        public override int GetDeathSound()
        {
            return 0xE4;
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
