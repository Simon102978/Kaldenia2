using System.Collections;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un loup tsuki")]
    public class TsukiWolf : BaseCreature
    {
        private static readonly Hashtable m_Table = new Hashtable();
        [Constructable]
        public TsukiWolf()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un loup tsuki";
            Body = 250;

            switch (Utility.Random(3))
            {
                case 0:
                    Hue = Utility.RandomNeutralHue();
                    break; //No, this really isn't accurate ;->
            }

            SetStr(401, 450);
            SetDex(151, 200);
            SetInt(66, 76);

            SetHits(376, 450);
            SetMana(40);

            SetDamage(14, 18);

            SetDamageType(ResistanceType.Physical, 90);
            SetDamageType(ResistanceType.Cold, 5);
            SetDamageType(ResistanceType.Energy, 5);

            SetResistance(ResistanceType.Physical, 40, 60);
            SetResistance(ResistanceType.Fire, 50, 70);
            SetResistance(ResistanceType.Cold, 50, 70);
            SetResistance(ResistanceType.Poison, 50, 70);
            SetResistance(ResistanceType.Energy, 50, 70);

            SetSkill(SkillName.Anatomy, 65.1, 72.0);
            SetSkill(SkillName.MagicResist, 65.1, 70.0);
            SetSkill(SkillName.Tactics, 95.1, 110.0);
            SetSkill(SkillName.Wrestling, 97.6, 107.5);
            SetSkill(SkillName.Necromancy, 20.0);
            SetSkill(SkillName.SpiritSpeak, 20.0);
            SetSkill(SkillName.Anatomy, 40.0, 50.0);
            SetSkill(SkillName.Tracking, 100.0);
            SetSkill(SkillName.Parry, 90.0, 100.0);

            Fame = 8500;
            Karma = -8500;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 96.0;

            SetSpecialAbility(SpecialAbility.Rage);
        }

        public TsukiWolf(Serial serial)
            : base(serial)
        {
        }

        public override bool CanAngerOnTame => true;

        public override int TreasureMapLevel => 3;
        public override int Meat => Utility.RandomMinMax(3, 5);
		public override int Hides => Utility.RandomMinMax(5, 10);
		public override HideType HideType => HideType.Lupus;
		public override int Bones => Utility.RandomMinMax(5, 10);
		public override BoneType BoneType => BoneType.Lupus;
		public override FoodType FavoriteFood => FoodType.Meat;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.BodyPartsAndBones);
            AddLoot(LootPack.PeculiarSeed1);
			AddLoot(LootPack.Others, Utility.RandomMinMax(3, 6));

		}

		public override int GetAngerSound()
        {
            return 0x52D;
        }

        public override int GetIdleSound()
        {
            return 0x52C;
        }

        public override int GetAttackSound()
        {
            return 0x52B;
        }

        public override int GetHurtSound()
        {
            return 0x52E;
        }

        public override int GetDeathSound()
        {
            return 0x52A;
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
