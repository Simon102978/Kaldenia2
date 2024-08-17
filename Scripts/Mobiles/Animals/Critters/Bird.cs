namespace Server.Mobiles
{
    [CorpseName("le corps d'un oiseau")]
    public class Bird : BaseCreature
    {
        [Constructable]
        public Bird()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            if (Utility.RandomBool())
            {
                Hue = 0x901;

                switch (Utility.Random(3))
                {
                    case 0:
                        Name = "un corbeau";
                        break;
                    case 2:
                        Name = "une corneille";
                        break;
                    case 1:
                        Name = "une pie";
                        break;
                }
            }
            else
            {
                Hue = Utility.RandomBirdHue();
                Name = NameList.RandomName("un oiseau");
            }

            Body = 6;
            BaseSoundID = 0x1B;

            SetStr(10);
            SetDex(25, 35);
            SetInt(10);

            SetDamage(0);

            SetDamageType(ResistanceType.Physical, 100);

            SetSkill(SkillName.Wrestling, 4.2, 6.4);
            SetSkill(SkillName.Tactics, 4.0, 6.0);
            SetSkill(SkillName.MagicResist, 4.0, 5.0);

            Fame = 150;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -6.9;
        }

        public Bird(Serial serial)
            : base(serial)
        {
        }

		public override bool CanBeParagon => false;

       	public override bool CanReveal => false;
		public override MeatType MeatType => MeatType.Bird;
        public override int Meat => Utility.RandomMinMax(0, 2);
        public override int Feathers => Utility.RandomMinMax(5, 25);
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

    [CorpseName("le corps d'un oiseau")]
    public class TropicalBird : BaseCreature
    {
        [Constructable]
        public TropicalBird()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Hue = Utility.RandomBirdHue();
            Name = "un oiseau tropical";

            Body = 6;
            BaseSoundID = 0xBF;

            SetStr(10);
            SetDex(25, 35);
            SetInt(10);

            SetDamage(0);

            SetDamageType(ResistanceType.Physical, 100);

            SetSkill(SkillName.Wrestling, 4.2, 6.4);
            SetSkill(SkillName.Tactics, 4.0, 6.0);
            SetSkill(SkillName.MagicResist, 4.0, 5.0);

            Fame = 150;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -6.9;
        }

        public TropicalBird(Serial serial)
            : base(serial)
        {
        }

        public override MeatType MeatType => MeatType.Bird;
        public override int Meat => Utility.RandomMinMax(0,2);
        public override int Feathers => Utility.RandomMinMax(5, 15);
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
