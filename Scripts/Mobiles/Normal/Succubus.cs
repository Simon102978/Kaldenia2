using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une succube")]
    public class Succubus : BaseCreature
    {
        [Constructable]
        public Succubus()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "une succube";
            Body = 149;
            BaseSoundID = 0x4B0;

            SetStr(488, 620);
            SetDex(121, 170);
            SetInt(498, 657);

            SetHits(312, 353);

            SetDamage(18, 28);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Energy, 25);

            SetResistance(ResistanceType.Physical, 80, 90);
            SetResistance(ResistanceType.Fire, 70, 80);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 50, 60);

            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 99.1, 100.0);
            SetSkill(SkillName.Meditation, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 100.5, 150.0);
            SetSkill(SkillName.Tactics, 80.1, 90.0);
            SetSkill(SkillName.Wrestling, 80.1, 90.0);

            Fame = 24000;
            Karma = -24000;

            SetSpecialAbility(SpecialAbility.LifeDrain);
        }

        public Succubus(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int TreasureMapLevel => 5;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.MedScrolls, 2);
			AddLoot(LootPack.Others, Utility.RandomMinMax(3, 5));
			AddLoot(LootPack.LootItem<CerveauLiche>(1, 2));


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
