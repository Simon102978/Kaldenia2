using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une wyrm")]
    public class WhiteWyrm : BaseCreature
    {
        public override double AverageThreshold => 0.25;

        [Constructable]
        public WhiteWyrm()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = Utility.RandomBool() ? 180 : 49;
            Name = "une wyrm blanche";
            BaseSoundID = 362;

            SetStr(721, 760);
            SetDex(101, 130);
            SetInt(386, 425);

            SetHits(433, 456);

            SetDamage(17, 25);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Cold, 50);

            SetResistance(ResistanceType.Physical, 55, 70);
            SetResistance(ResistanceType.Fire, 15, 25);
            SetResistance(ResistanceType.Cold, 80, 90);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.EvalInt, 99.1, 100.0);
            SetSkill(SkillName.Magery, 99.1, 100.0);
            SetSkill(SkillName.MagicResist, 99.1, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 18000;
            Karma = -18000;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 120.3;
        }

        public WhiteWyrm(Serial serial)
            : base(serial)
        {
        }

        public override bool ReacquireOnMovement => true;
        public override int TreasureMapLevel => 4;
        public override int Meat => Utility.RandomMinMax(10, 15);
		public override int DragonBlood => 8;
		public override int Hides => Utility.RandomMinMax(10, 15);
		public override HideType HideType => HideType.Dragonique;

		public override int Bones => Utility.RandomMinMax(10, 15);
		public override BoneType BoneType => BoneType.Dragonique;

        public override FoodType FavoriteFood => FoodType.Meat | FoodType.Gold;
        public override bool CanAngerOnTame => true;
        public override bool CanFly => true;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems, Utility.Random(1, 5));
			AddLoot(LootPack.LootItem<EcaillesWyrm>(5, true));
			AddLoot(LootPack.Others, Utility.RandomMinMax(10, 15));

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