using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un serpent")]
    public class Snake : BaseCreature
    {
        [Constructable]
        public Snake()
            : base(AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            Name = "un serpent";
            Body = 52;
            Hue = Utility.RandomSnakeHue();
            BaseSoundID = 0xDB;

            SetStr(22, 34);
            SetDex(16, 25);
            SetInt(6, 10);

            SetHits(15, 19);
            SetMana(0);

            SetDamage(1, 4);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 15, 20);
            SetResistance(ResistanceType.Poison, 20, 30);

            SetSkill(SkillName.Poisoning, 50.1, 70.0);
            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 19.3, 34.0);
            SetSkill(SkillName.Wrestling, 19.3, 34.0);

            Fame = 300;
            Karma = -300;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 59.1;
        }

		public override bool CanBeParagon => false;
        public override bool CanReveal => false;
		public Snake(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmune => Poison.Lesser;
        public override Poison HitPoison => Poison.Lesser;
        public override bool DeathAdderCharmable => true;
        public override int Meat => Utility.RandomMinMax(1, 2);

		public override int Hides => Utility.RandomMinMax(1, 2);
		public override HideType HideType => HideType.Reptilien;


		public override int Bones => Utility.RandomMinMax(1, 2);
		public override BoneType BoneType => BoneType.Reptilien;
		public override FoodType FavoriteFood => FoodType.Eggs;

		public override void GenerateLoot()
		{
			AddLoot(LootPack.LootItem<OeufSerpent>(2, 5));
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