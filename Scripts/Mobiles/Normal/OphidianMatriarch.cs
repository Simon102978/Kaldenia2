using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une matriarche ophidien")]
    public class OphidianMatriarch : BaseCreature
    {
        [Constructable]
        public OphidianMatriarch()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 3, 0.2, 0.4)
        {
            Name = "une matriarche ophidien";
            Body = 87;
            BaseSoundID = 644;

            SetStr(416, 505);
            SetDex(96, 115);
            SetInt(366, 455);

			SetHits(342, 546);

			SetDamage(13, 17);

            SetDamageType(ResistanceType.Physical, 100);

			SetResistance(ResistanceType.Physical, 50, 75);
			SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 35, 45);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 35, 45);

            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.Meditation, 5.4, 25.0);
            SetSkill(SkillName.MagicResist, 90.1, 100.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 16000;
            Karma = -16000;

            AddItem(new BardicheOphidian());
        }

        public OphidianMatriarch(Serial serial)
            : base(serial)
        {
        }

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvoutePoison>(), Utility.RandomMinMax(2, 4));
		}


		public override Poison PoisonImmune => Poison.Greater;
        public override int TreasureMapLevel => 4;

        public override TribeType Tribe => TribeType.Ophidian;

		public override int Hides => Utility.RandomMinMax(3, 7);
		public override HideType HideType => HideType.Ophidien;


		public override int Bones => Utility.RandomMinMax(3, 7);
		public override BoneType BoneType => BoneType.Ophidien;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.MedScrolls, 2);
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
