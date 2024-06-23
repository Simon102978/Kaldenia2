namespace Server.Mobiles
{
    [CorpseName("le corps d'un poulet")]
    public class Chicken : BaseCreature
    {
        [Constructable]
        public Chicken()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "un poulet";
            Body = 0xD0;
            BaseSoundID = 0x6E;

            SetStr(5);
            SetDex(15);
            SetInt(5);

            SetHits(3);
            SetMana(0);

            SetDamage(1);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 1, 5);

            SetSkill(SkillName.MagicResist, 4.0);
            SetSkill(SkillName.Tactics, 5.0);
            SetSkill(SkillName.Wrestling, 5.0);

            Fame = 150;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -0.9;
        }

        public Chicken(Serial serial)
            : base(serial)
        {
        }

		public override bool CanBeParagon => false;

		public override int Meat => Utility.RandomMinMax(1, 2);
		public override MeatType MeatType => MeatType.Chicken;
        public override FoodType FavoriteFood => FoodType.GrainsAndHay;
        public override bool CanFly => true;
        public override int Feathers => Utility.RandomMinMax(5, 15);
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
