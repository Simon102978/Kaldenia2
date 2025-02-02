using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un dragon")]
    public class SerpentineDragon : BaseCreature
    {
        [Constructable]
        public SerpentineDragon()
            : base(AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4)
        {
            Name = "un dragon serpentin";
            Body = 103;
            BaseSoundID = 362;

            SetStr(111, 140);
            SetDex(201, 220);
            SetInt(1001, 1040);

            SetHits(480);

            SetDamage(5, 12);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Poison, 25);

            SetResistance(ResistanceType.Physical, 35, 40);
            SetResistance(ResistanceType.Fire, 25, 35);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.EvalInt, 100.1, 110.0);
            SetSkill(SkillName.Magery, 110.1, 120.0);
            SetSkill(SkillName.Meditation, 100.0);
            SetSkill(SkillName.MagicResist, 100.0);
            SetSkill(SkillName.Tactics, 50.1, 60.0);
            SetSkill(SkillName.Wrestling, 30.1, 100.0);
            SetSkill(SkillName.Tracking, 100.0);

            Fame = 15000;
            Karma = 15000;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 125.0;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public SerpentineDragon(Serial serial)
            : base(serial)
        {
        }

        public override bool ReacquireOnMovement => !Controlled;

        public override double BonusPetDamageScalar => Controlled ? 1.0 : 3.0;
        public override bool AutoDispel => !Controlled;



		public override int Bones => Utility.RandomMinMax(5, 10);

		public override BoneType BoneType => BoneType.Dragonique;

		public override int Hides => Utility.RandomMinMax(5, 10);

		public override HideType HideType => HideType.Dragonique;

		public override int Meat => Utility.RandomMinMax(5, 10);

		/*      public override int Scales => 6;

			  public override ScaleType ScaleType => (Utility.RandomBool() ? ScaleType.Black : ScaleType.White);*/
		public override int TreasureMapLevel => 4;
        public override bool CanAngerOnTame => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);          
            AddLoot(LootPack.PeculiarSeed3);
			AddLoot(LootPack.Others, Utility.RandomMinMax(7, 11));
			AddLoot(LootPack.LootItem<EcaillesWyrm>(1, true));



		}

		public override int GetIdleSound()
        {
            return 0x2C4;
        }

        public override int GetAttackSound()
        {
            return 0x2C0;
        }

        public override int GetDeathSound()
        {
            return 0x2C1;
        }

        public override int GetAngerSound()
        {
            return 0x2C4;
        }

        public override int GetHurtSound()
        {
            return 0x2C3;
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
