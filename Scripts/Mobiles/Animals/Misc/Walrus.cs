namespace Server.Mobiles
{
    [CorpseName("le corps d'un morse")]
    public class Walrus : BaseCreature
    {
        [Constructable]
        public Walrus()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "un morse";
            Body = 0xDD;
            BaseSoundID = 0xE0;

            SetStr(21, 29);
            SetDex(46, 55);
            SetInt(16, 20);

            SetHits(14, 17);
            SetMana(0);

            SetDamage(4, 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Cold, 20, 25);
            SetResistance(ResistanceType.Poison, 5, 10);
            SetResistance(ResistanceType.Energy, 5, 10);

            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 19.2, 29.0);
            SetSkill(SkillName.Wrestling, 19.2, 29.0);

            Fame = 150;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 35.1;
        }

        public Walrus(Serial serial)
            : base(serial)
        {
        }

        public override bool CanBeParagon => false;
        public override bool CanReveal => false;
        public override int Meat => 1;
		public override int Hides => 5;
		public override HideType HideType => HideType.Regular;
		public override int Bones => 5;
		public override BoneType BoneType => BoneType.Regular;
		public override FoodType FavoriteFood => FoodType.Fish;
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
