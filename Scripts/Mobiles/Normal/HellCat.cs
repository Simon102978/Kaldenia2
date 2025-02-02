namespace Server.Mobiles
{
    [CorpseName("le corps d'un chat des enfers")]
    public class HellCat : BaseCreature
    {
        [Constructable]
        public HellCat()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un chat des enfers";
            Body = 0xC9;
            Hue = Utility.RandomList(0x647, 0x650, 0x659, 0x662, 0x66B, 0x674);
            BaseSoundID = 0x69;

            SetStr(51, 100);
            SetDex(52, 150);
            SetInt(13, 85);

            SetHits(48, 67);

            SetDamage(6, 12);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Fire, 60);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 80, 90);
            SetResistance(ResistanceType.Energy, 15, 20);

            SetSkill(SkillName.MagicResist, 45.1, 60.0);
            SetSkill(SkillName.Tactics, 40.1, 55.0);
            SetSkill(SkillName.Wrestling, 30.1, 40.0);
            SetSkill(SkillName.Necromancy, 18.0);
            SetSkill(SkillName.SpiritSpeak, 18.0);

            Fame = 1000;
            Karma = -1000;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 71.1;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public HellCat(Serial serial)
            : base(serial)
        {
        }

		/*    public override int Hides => 10;
			public override HideType HideType => HideType.Spined;*/

		public override int Hides => Utility.RandomMinMax(3, 5);
		public override HideType HideType => HideType.Regular;


		public override int Bones => Utility.RandomMinMax(3, 5);
		public override BoneType BoneType => BoneType.Regular;
		public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Feline;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));

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
