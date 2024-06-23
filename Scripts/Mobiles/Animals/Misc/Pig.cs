namespace Server.Mobiles
{
    [CorpseName("le corps d'un cochon")]
    public class Pig : BaseCreature
    {
        [Constructable]
        public Pig()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "un cochon";
            Body = 0xCB;
            BaseSoundID = 0xC4;

            SetStr(20);
            SetDex(20);
            SetInt(5);

            SetHits(12);
            SetMana(0);

            SetDamage(2, 4);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 10, 15);

            SetSkill(SkillName.MagicResist, 5.0);
            SetSkill(SkillName.Tactics, 5.0);
            SetSkill(SkillName.Wrestling, 5.0);

            Fame = 150;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 11.1;
        }

        public Pig(Serial serial)
            : base(serial)
        {
        }
		public override int Hides => Utility.RandomMinMax(2, 4);
		public override HideType HideType => HideType.Regular;

		public override bool CanBeParagon => false;

		public override int Bones => Utility.RandomMinMax(2, 4);
		public override BoneType BoneType => BoneType.Regular;

		public override int Meat => Utility.RandomMinMax(3, 5);

		public override MeatType MeatType => MeatType.Ribs;

		public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;
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
