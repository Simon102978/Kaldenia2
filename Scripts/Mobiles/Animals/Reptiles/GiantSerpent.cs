using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un serpent geant")]
    public class GiantSerpent : BaseCreature
    {
        [Constructable]
        public GiantSerpent()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un serpent geant";
            Body = 0x15;
            Hue = Utility.RandomSnakeHue();
            BaseSoundID = 219;

            SetStr(186, 215);
            SetDex(56, 80);
            SetInt(66, 85);

            SetHits(112, 129);
            SetMana(0);

            SetDamage(7, 17);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Poison, 60);

            SetResistance(ResistanceType.Physical, 30, 35);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Poison, 70, 90);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Poisoning, 70.1, 100.0);
            SetSkill(SkillName.MagicResist, 25.1, 40.0);
            SetSkill(SkillName.Tactics, 65.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 2500;
            Karma = -2500;
        }

        public GiantSerpent(Serial serial)
            : base(serial)
        {
        }

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvoutePoison>(), Utility.RandomMinMax(2, 4));
		}


		public override Poison PoisonImmune => Poison.Greater;
        public override Poison HitPoison => (0.8 >= Utility.RandomDouble() ? Poison.Greater : Poison.Deadly);
        public override bool DeathAdderCharmable => true;
        public override int Meat => Utility.RandomMinMax(2, 4);
		/*     public override int Hides => 15;
			 public override HideType HideType => HideType.Spined;*/


		public override int Hides => Utility.RandomMinMax(2, 4);
		public override HideType HideType => HideType.Reptilien;


		public override int Bones => Utility.RandomMinMax(2, 4);
		public override BoneType BoneType => BoneType.Reptilien;


		public override void GenerateLoot()
        {
    //        AddLoot(LootPack.Average);
            AddLoot(LootPack.LootItem<Bone>());
			AddLoot(LootPack.LootItem<OeufSerpent>());
			AddLoot(LootPack.LootItem<EcaillesSerpentGeant>());
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 3));



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
