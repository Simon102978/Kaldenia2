using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un arbre maudit")]
    public class Reaper : BaseCreature
    {
        [Constructable]
        public Reaper()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un arbre maudit";
            Body = 47;
            BaseSoundID = 442;
            Hue = 1175;

            SetStr(300, 600);
            SetDex(200, 250);
            SetInt(301, 650);

            SetHits(500, 750);
            SetStam(0);
            SetMana(1000);

            SetDamage(9, 11);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Poison, 20);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 15, 25);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 100.1, 125.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 50.1, 60.0);

            Fame = 3500;
            Karma = -3500;
        }

        public Reaper(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmune => Poison.Greater;
        public override int TreasureMapLevel => 2;
        public override bool DisallowAllMoves => true;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich,2);
            AddLoot(LootPack.LootItem<PalmierLog>(5));
            AddLoot(LootPack.LootItem<MandrakeRoot>(5));
			AddLoot(LootPack.LootItem<EcorceArbreMaudit>(3, 10));
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
