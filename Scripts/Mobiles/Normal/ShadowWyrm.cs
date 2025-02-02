using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une wyrm sombre")]
    public class ShadowWyrm : BaseCreature
    {
        [Constructable]
        public ShadowWyrm()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "une wyrm sombre";
            Body = 106;
            BaseSoundID = 362;
			Hue = -1;

            SetStr(898, 1030);
            SetDex(68, 200);
            SetInt(488, 620);

            SetHits(900, 1299);

            SetDamage(29, 35);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Cold, 25);

            SetResistance(ResistanceType.Physical, 65, 75);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 45, 55);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 50, 60);

            SetSkill(SkillName.EvalInt, 80.1, 100.0);
            SetSkill(SkillName.Magery, 80.1, 100.0);
            SetSkill(SkillName.Meditation, 52.5, 75.0);
            SetSkill(SkillName.MagicResist, 100.3, 130.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 97.6, 100.0);
            SetSkill(SkillName.Tracking, 90.0, 100.0);
            SetSkill(SkillName.Necromancy, 80.0, 90.0);
            SetSkill(SkillName.SpiritSpeak, 100.0, 105.0);

            Fame = 22500;
            Karma = -22500;

            Tamable = true;
            ControlSlots = 5;
            MinTameSkill = 120.0;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public ShadowWyrm(Serial serial)
            : base(serial)
        {
        }

        public override bool CanAngerOnTame => true;
        public override bool ReacquireOnMovement => !Controlled;
        public override bool AutoDispel => !Controlled;
        public override Poison PoisonImmune => Poison.Deadly;
        public override Poison HitPoison => Poison.Deadly;
        public override int TreasureMapLevel => 5;
        public override int Meat => Utility.RandomMinMax(5, 10);

		//    public override int Hides => 20;
		//     public override int Scales => 10;
		//     public override ScaleType ScaleType => ScaleType.Black;
		//      public override HideType HideType => HideType.Barbed;

		public override int Bones => Utility.RandomMinMax(6, 12);

		public override BoneType BoneType => BoneType.Dragonique;

		public override int Hides => Utility.RandomMinMax(6, 12);
		public override HideType HideType => HideType.Dragonique;
		public override bool CanFly => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.Gems, 5);
			AddLoot(LootPack.LootItem<EcaillesWyrm>(3, 7));
			AddLoot(LootPack.Others, Utility.RandomMinMax(6, 12));

		}

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteDragon>(), Utility.RandomMinMax(2, 4));
			AddLoot(LootPack.LootItem<SangEnvouteDex>(), Utility.RandomMinMax(1, 2));
			AddLoot(LootPack.LootItem<SangEnvouteInt>(), Utility.RandomMinMax(1, 2));



		}

		public override int GetIdleSound()
        {
            return 0x2D5;
        }

        public override int GetHurtSound()
        {
            return 0x2D1;
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
