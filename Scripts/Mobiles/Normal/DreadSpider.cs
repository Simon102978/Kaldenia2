using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une tarenlune")]
    public class DreadSpider : BaseCreature
    {
        [Constructable]
        public DreadSpider()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "une tarenlune";
            Body = 11;
            BaseSoundID = 1170;

            SetStr(196, 220);
            SetDex(126, 145);
            SetInt(286, 310);

            SetHits(118, 132);

            SetDamage(5, 17);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Poison, 80);

            SetResistance(ResistanceType.Physical, 40, 50);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.EvalInt, 65.1, 80.0);
            SetSkill(SkillName.Magery, 65.1, 80.0);
            SetSkill(SkillName.MagicResist, 45.1, 60.0);
            SetSkill(SkillName.Tactics, 55.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 75.0);
            SetSkill(SkillName.Poisoning, 80.0);
            SetSkill(SkillName.Tracking, 50.0, 60.0);
            SetSkill(SkillName.Necromancy, 20.0);
            SetSkill(SkillName.SpiritSpeak, 20.0);

            Fame = 5000;
            Karma = -5000;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 96.0;
        }

        public DreadSpider(Serial serial)
            : base(serial)
        {
        }
		public override int Hides => Utility.RandomMinMax(5, 10);

		public override HideType HideType => HideType.Arachnide;


		public override int Bones => Utility.RandomMinMax(5, 10);

		public override BoneType BoneType => BoneType.Arachnide;
		public override bool CanAngerOnTame => true;
        public override Poison PoisonImmune => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override int TreasureMapLevel => 3;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.LootItem<SpidersSilk>(8, true));
			AddLoot(LootPack.LootItem<VeninTarenlune>(3, 7));
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
