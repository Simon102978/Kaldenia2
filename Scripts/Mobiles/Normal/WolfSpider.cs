using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une araignee loup")]
    public class WolfSpider : BaseCreature
    {
        [Constructable]
        public WolfSpider()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "une araignee loup";
            Body = 736;
            Hue = 0;

            SetStr(225, 268);
            SetDex(145, 165);
            SetInt(285, 310);

            SetHits(150, 160);
            SetMana(285, 310);
            SetStam(145, 165);

            SetDamage(15, 18);

            SetDamageType(ResistanceType.Physical, 70);
            SetDamageType(ResistanceType.Poison, 30);

            SetResistance(ResistanceType.Physical, 30, 35);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.Anatomy, 80.0, 90.0);
            SetSkill(SkillName.MagicResist, 60.0, 75.0);
            SetSkill(SkillName.Poisoning, 62.3, 77.2);
            SetSkill(SkillName.Tactics, 84.1, 95.9);
            SetSkill(SkillName.Wrestling, 80.2, 90.0);
            SetSkill(SkillName.Hiding, 105.0, 110.0);
  //          SetSkill(SkillName.Stealth, 105.0, 110.0);

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 59.1;
        }

        public WolfSpider(Serial serial)
            : base(serial)
        {
        }

        public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Arachnid;
        public override Poison PoisonImmune => Poison.Regular;
        public override Poison HitPoison => Poison.Regular;

		public override int Hides => Utility.RandomMinMax(1, 3);
		public override HideType HideType => HideType.Arachnide;
		public override int Bones => Utility.RandomMinMax(1, 3);
		public override BoneType BoneType => BoneType.Arachnide;

		public override int TreasureMapLevel => Utility.RandomMinMax(1, 3);

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Gems, 2);
            AddLoot(LootPack.LootItem<SpidersSilk>(8, true));
			AddLoot(LootPack.LootItem<VeninAraigneeNoire>(4, true));
		}

		public override int GetIdleSound()
        {
            return 1605;
        }

        public override int GetAngerSound()
        {
            return 1602;
        }

        public override int GetHurtSound()
        {
            return 1604;
        }

        public override int GetDeathSound()
        {
            return 1603;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(2);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
