namespace Server.Mobiles
{
    [CorpseName("le corps d'un chat des enfers")]
    public class PredatorHellCat : BaseCreature
    {
        [Constructable]
        public PredatorHellCat()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un chat des enfers";
            Body = 127;
            BaseSoundID = 0xBA;

            SetStr(161, 185);
            SetDex(96, 115);
            SetInt(76, 100);

            SetHits(97, 131);

            SetDamage(5, 17);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Fire, 25);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Energy, 5, 15);

            SetSkill(SkillName.MagicResist, 75.1, 90.0);
            SetSkill(SkillName.Tactics, 50.1, 65.0);
            SetSkill(SkillName.Wrestling, 50.1, 65.0);
            SetSkill(SkillName.Necromancy, 20.0);
            SetSkill(SkillName.SpiritSpeak, 20.0);
            SetSkill(SkillName.Wrestling, 50.1, 65.0);
            SetSkill(SkillName.Tracking, 41.2);

            Fame = 2500;
            Karma = -2500;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 90.0;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public PredatorHellCat(Serial serial)
            : base(serial)
        {
        }

     //   public override int Hides => 10;
		/*   public override HideType HideType => HideType.Spined;
		   public override FoodType FavoriteFood => FoodType.Meat;*/


		public override int Hides => Utility.RandomMinMax(5, 10);
		public override HideType HideType => HideType.Regular;


		public override int Bones => Utility.RandomMinMax(5, 10);
		public override BoneType BoneType => BoneType.Regular;


		public override PackInstinct PackInstinct => PackInstinct.Feline;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
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
