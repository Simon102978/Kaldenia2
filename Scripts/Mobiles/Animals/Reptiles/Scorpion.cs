using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un scorpion")]
    public class Scorpion : BaseCreature
    {


        [Constructable]
        public Scorpion()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un scorpion";
            Body = 48;
            BaseSoundID = 397;

            SetStr(73, 115);
            SetDex(76, 95);
            SetInt(16, 30);

            SetHits(50, 63);
            SetMana(0);

            SetDamage(5, 10);

            SetDamageType(ResistanceType.Physical, 60);
            SetDamageType(ResistanceType.Poison, 40);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 10, 15);
            SetResistance(ResistanceType.Cold, 20, 25);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 10, 15);

            SetSkill(SkillName.Poisoning, 80.1, 100.0);
            SetSkill(SkillName.MagicResist, 30.1, 35.0);
            SetSkill(SkillName.Tactics, 60.3, 75.0);
            SetSkill(SkillName.Wrestling, 50.3, 65.0);

            Fame = 2000;
            Karma = -2000;

            Tamable = true;
            ControlSlots = 2;
			MinTameSkill = 47.1;
        }

        public Scorpion(Serial serial)
            : base(serial)
        {
        }
		
		public override int Meat => Utility.RandomMinMax(2, 3);

		public override int Hides => 0;//Utility.RandomMinMax(2, 3);
		public override HideType HideType => HideType.Arachnide;


		public override int Bones => 0; // Utility.RandomMinMax(2, 3);
		public override BoneType BoneType => BoneType.Arachnide;


		public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Arachnid;
        public override Poison PoisonImmune => Poison.Greater;
        public override Poison HitPoison => (0.8 >= Utility.RandomDouble() ? Poison.Lesser : Poison.Regular);
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.LootItem<LesserPoisonPotion>(true));
			AddLoot(LootPack.Others, Utility.RandomMinMax(0, 2));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
