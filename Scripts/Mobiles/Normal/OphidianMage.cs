using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un ophidien")]
    public class OphidianMage : BaseCreature
    {
        private static readonly string[] m_Names = new string[]
        {
            "un apprenti mage ophidien",
            "un shaman opihidien"
        };
        [Constructable]
        public OphidianMage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 3, 0.2, 0.4)
        {
            Name = m_Names[Utility.Random(m_Names.Length)];
            Body = 85;
            BaseSoundID = 639;

            SetStr(181, 205);
            SetDex(191, 215);
            SetInt(96, 120);

			SetHits(211, 326);

			SetDamage(9, 16);

            SetDamageType(ResistanceType.Physical, 100);

			SetResistance(ResistanceType.Physical, 50, 75);
			SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 35, 45);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 35, 45);

            SetSkill(SkillName.EvalInt, 85.1, 100.0);
            SetSkill(SkillName.Magery, 85.1, 100.0);
            SetSkill(SkillName.MagicResist, 75.0, 97.5);
            SetSkill(SkillName.Tactics, 65.0, 87.5);
            SetSkill(SkillName.Wrestling, 85.1, 100.0);

            Fame = 4000;
            Karma = -4000;

            AddItem(new BardicheOphidian());
        }

        public OphidianMage(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(2, 3);
        public override int TreasureMapLevel => Utility.RandomMinMax(1, 2);

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvoutePoison>(), Utility.RandomMinMax(2, 4));
		}


		public override TribeType Tribe => TribeType.Ophidian;

		public override int Hides => Utility.RandomMinMax(0, 2);
		public override HideType HideType => HideType.Ophidien;


		public override int Bones => Utility.RandomMinMax(0, 2);
		public override BoneType BoneType => BoneType.Ophidien;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich,3);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.MedScrolls);
            AddLoot(LootPack.Potions);
            AddLoot(LootPack.MageryRegs, 10);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));

			//        AddLoot(LootPack.LootItem<PainSpikeScroll>(16.7));
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
