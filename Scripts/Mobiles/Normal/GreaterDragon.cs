using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un dragon")]
    public class GreaterDragon : BaseCreature
    {
        [Constructable]
        public GreaterDragon()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.3, 0.5)
        {
            Name = "un grand Dragon";
            Body = Utility.RandomList(12, 59);
            BaseSoundID = 362;

            SetStr(1025, 1425);
            SetDex(81, 148);
            SetInt(475, 675);

            SetHits(1000, 2000);

            SetDamage(24, 33);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 60, 85);
            SetResistance(ResistanceType.Fire, 65, 90);
            SetResistance(ResistanceType.Cold, 40, 55);
            SetResistance(ResistanceType.Poison, 40, 60);
            SetResistance(ResistanceType.Energy, 50, 75);

            SetSkill(SkillName.Meditation, 0);
            SetSkill(SkillName.EvalInt, 110.0, 140.0);
            SetSkill(SkillName.Magery, 110.0, 140.0);
            SetSkill(SkillName.Poisoning, 0);
            SetSkill(SkillName.Anatomy, 0);
            SetSkill(SkillName.MagicResist, 110.0, 140.0);
            SetSkill(SkillName.Tactics, 110.0, 140.0);
            SetSkill(SkillName.Wrestling, 115.0, 145.0);

            Fame = 22000;
            Karma = -15000;

            Tamable = true;
            ControlSlots = 5;
            MinTameSkill = 125.7;

            SetWeaponAbility(WeaponAbility.BleedAttack);
            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public GreaterDragon(Serial serial)
            : base(serial)
        {
        }

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteDragon>(), Utility.RandomMinMax(2, 4));
		}

		public override bool StatLossAfterTame => true;
        public override bool ReacquireOnMovement => !Controlled;
        public override bool AutoDispel => !Controlled;
        public override int TreasureMapLevel => 5;
        public override int Meat => Utility.RandomMinMax(5, 10);

		/*        public override int Hides => 30;
				public override HideType HideType => HideType.Barbed;
				public override int Scales => 7;
				public override ScaleType ScaleType => (Body == 12 ? ScaleType.Yellow : ScaleType.Red);*/

		public override int Hides => Utility.RandomMinMax(5, 10);

		public override HideType HideType => HideType.Dragonique;

		public override int Bones => Utility.RandomMinMax(5, 10);

		public override BoneType BoneType => BoneType.Dragonique;

		public override FoodType FavoriteFood => FoodType.Meat;
        public override bool CanAngerOnTame => true;
        public override bool CanFly => true;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 4);
            AddLoot(LootPack.Gems, 8);
			AddLoot(LootPack.Others, Utility.RandomMinMax(71, 10));

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
