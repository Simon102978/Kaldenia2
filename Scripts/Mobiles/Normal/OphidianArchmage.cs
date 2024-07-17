using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un opihidien")]
    public class OphidianArchmage : BaseCreature
    {
        private static readonly string[] m_Names = new string[]
        {
            "un archimage ophidien"
            
        };
        [Constructable]
        public OphidianArchmage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 3, 0.2, 0.4)
        {
            Name = m_Names[Utility.Random(m_Names.Length)];
            Body = 85;
            BaseSoundID = 639;

            SetStr(281, 305);
            SetDex(191, 215);
            SetInt(226, 250);

            SetHits(233, 374);
            SetStam(36, 45);

            SetDamage(5, 10);

            SetDamageType(ResistanceType.Physical, 100);

			SetResistance(ResistanceType.Physical, 50, 75);
			SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 35, 40);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.EvalInt, 95.1, 100.0);
            SetSkill(SkillName.Magery, 95.1, 100.0);
            SetSkill(SkillName.MagicResist, 75.0, 97.5);
            SetSkill(SkillName.Tactics, 65.0, 87.5);
            SetSkill(SkillName.Wrestling, 85.1, 100.0);

           AddItem(new BardicheOphidian());

            Fame = 11500;
            Karma = -11500;
        }

        public OphidianArchmage(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(1, 2);

		public override int TreasureMapLevel => 2;

        public override TribeType Tribe => TribeType.Ophidian;

		public override int Hides => Utility.RandomMinMax(1, 3);

		public override HideType HideType => HideType.Ophidien;

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvoutePoison>(), Utility.RandomMinMax(2, 4));
		}

		public override int Bones => Utility.RandomMinMax(1, 3);
		public override BoneType BoneType => BoneType.Ophidien;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
            AddLoot(LootPack.MageryRegs, 5, 15);
            AddLoot(LootPack.NecroRegs, 5, 15);
			AddLoot(LootPack.Others, Utility.RandomMinMax(2, 4));

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
