using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un chevalier ophidien")]
    public class OphidianKnight : BaseCreature
    {
        private static readonly string[] m_Names = new string[]
        {
            "un chevalier ophidien"
            
        };
        [Constructable]
        public OphidianKnight()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 3, 0.2, 0.4)
        {
            Name = m_Names[Utility.Random(m_Names.Length)];
            Body = 86;
            BaseSoundID = 634;

            SetStr(417, 595);
            SetDex(166, 175);
            SetInt(46, 70);

            SetHits(342, 546);
            SetMana(0);

            SetDamage(18, 23);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 50, 75);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 35, 45);
            SetResistance(ResistanceType.Poison, 90, 100);
            SetResistance(ResistanceType.Energy, 35, 45);

            SetSkill(SkillName.Poisoning, 60.1, 80.0);
            SetSkill(SkillName.MagicResist, 65.1, 80.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 10000;
            Karma = -10000;
            AddItem(new BardicheOphidian());
        }

        public OphidianKnight(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(1, 2);

		public override Poison PoisonImmune => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override int TreasureMapLevel => 3;

        public override TribeType Tribe => TribeType.Ophidian;

		public override int Hides => Utility.RandomMinMax(1, 3);
		public override int Bones => Utility.RandomMinMax(1, 3);
		public override HideType HideType => HideType.Ophidien;

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvoutePoison>(), Utility.RandomMinMax(2, 4));
		}


		
		public override BoneType BoneType => BoneType.Ophidien;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 4);
            AddLoot(LootPack.LootItem<LesserPoisonPotion>());
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
