namespace Server.Mobiles
{
    [CorpseName("le corps d'un cerf")]
    public class GreatHart : BaseCreature
    {
        [Constructable]
        public GreatHart()
            : base(AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            Name = "un cerf";
            Body = 0xEA;

            SetStr(41, 71);
            SetDex(47, 77);
            SetInt(27, 57);

            SetHits(27, 41);
            SetMana(0);

            SetDamage(5, 9);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Cold, 5, 10);

            SetSkill(SkillName.MagicResist, 26.8, 44.5);
            SetSkill(SkillName.Tactics, 29.8, 47.5);
            SetSkill(SkillName.Wrestling, 29.8, 47.5);

            Fame = 300;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 59.1;
        }

        public GreatHart(Serial serial)
            : base(serial)
        {
        }
        public override bool CanBeParagon => false;
       	public override bool CanReveal => false;
        public override int Meat => Utility.RandomMinMax(2, 4);
        public override int Hides => Utility.RandomMinMax(4, 8);
		public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;
        public override int GetAttackSound()
        {
            return 0x82;
        }

        public override int GetHurtSound()
        {
            return 0x83;
        }

        public override int GetDeathSound()
        {
            return 0x84;
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
