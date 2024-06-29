namespace Server.Mobiles
{
    [CorpseName("a horse corpse")]
    public class AnkhShe : BaseMount
    {
        [Constructable]
        public AnkhShe() : this("Cheval AnkhShe")
        {
        }

        [Constructable]
        public AnkhShe(string name) : base(name, 228, 0x3EA1, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0xA8;
			Hue = 2490;
            SetStr(55, 98);
            SetDex(56, 75);
            SetInt(6, 10);

            SetHits(28, 45);
            SetMana(0);

            SetDamage(3, 4);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 15, 20);

            SetSkill(SkillName.MagicResist, 25.1, 30.0);
            SetSkill(SkillName.Tactics, 29.3, 44.0);
            SetSkill(SkillName.Wrestling, 29.3, 44.0);

            Fame = 300;
            Karma = 300;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 29.1;
        }

        public override int Meat => Utility.RandomMinMax(5, 10);

		public override int Hides => Utility.RandomMinMax(5, 10);

		public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;

        public AnkhShe(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
