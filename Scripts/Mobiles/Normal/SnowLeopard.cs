namespace Server.Mobiles
{
    [CorpseName("le corps d'un leopard")]
    public class SnowLeopard : BaseCreature
    {
        [Constructable]
        public SnowLeopard()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "un leopard des neiges";
            Body = Utility.RandomList(64, 65);
            BaseSoundID = 0x73;

            SetStr(56, 80);
            SetDex(66, 85);
            SetInt(26, 50);

            SetHits(34, 48);
            SetMana(0);

            SetDamage(3, 9);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, 25.1, 35.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 40.1, 50.0);

            Fame = 450;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 53.1;
        }

        public SnowLeopard(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(2, 4);

		public override int Hides => Utility.RandomMinMax(2, 4);

		public override HideType HideType => HideType.Regular;


		public override int Bones => Utility.RandomMinMax(2, 4);
		public override BoneType BoneType => BoneType.Regular;
		public override FoodType FavoriteFood => FoodType.Meat | FoodType.Fish;
        public override PackInstinct PackInstinct => PackInstinct.Feline;
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
