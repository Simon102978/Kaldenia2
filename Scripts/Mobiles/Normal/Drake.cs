using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un dragonneau")]
    public class Drake : BaseCreature
    {
        [Constructable]
        public Drake()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un dragonneau";
            Body = Utility.RandomList(60, 61);
            BaseSoundID = 362;

            SetStr(401, 430);
            SetDex(133, 152);
            SetInt(101, 140);

            SetHits(241, 258);

            SetDamage(11, 17);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Fire, 20);

            SetResistance(ResistanceType.Physical, 45, 50);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 65.1, 80.0);
            SetSkill(SkillName.Tactics, 65.1, 90.0);
            SetSkill(SkillName.Wrestling, 65.1, 80.0);

            Fame = 5500;
            Karma = -5500;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 84.3;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public Drake(Serial serial)
            : base(serial)
        {
        }

        public override bool ReacquireOnMovement => true;
        public override int TreasureMapLevel => 2;
        public override int Meat => Utility.RandomMinMax(5, 10);

		public override int DragonBlood => 8;

		public override int Hides => Utility.RandomMinMax(5, 10);

		public override HideType HideType => HideType.Dragonique;


		public override int Bones => Utility.RandomMinMax(5, 10);

		public override BoneType BoneType => BoneType.Dragonique;



		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteDrake>(), Utility.RandomMinMax(2, 4));
		}



		/*      public override int Hides => 20;
			  public override HideType HideType => HideType.Horned;
			  public override int Scales => 2;
			  public override ScaleType ScaleType => (Body == 60 ? ScaleType.Yellow : ScaleType.Red);*/
		public override FoodType FavoriteFood => FoodType.Meat | FoodType.Fish;
        public override bool CanFly => true;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
            AddLoot(LootPack.MageryRegs, 3);
			AddLoot(LootPack.Others, Utility.RandomMinMax(3, 5));


			AddLoot(LootPack.LootItem<Items.GemmeFeu>(), (double)5);
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
