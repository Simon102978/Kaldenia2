using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un serpent argent�")]
    public class SilverSerpent : BaseCreature
    {
        [Constructable]
        public SilverSerpent()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 92;
            Name = "un serpent argent�";
            BaseSoundID = 219;
            Hue = 1150;

            SetStr(161, 360);
            SetDex(151, 300);
            SetInt(21, 40);

            SetHits(97, 216);

            SetDamage(5, 21);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Cold, 5, 10);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 5, 10);

            SetSkill(SkillName.Poisoning, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 95.1, 100.0);
            SetSkill(SkillName.Tactics, 80.1, 95.0);
            SetSkill(SkillName.Wrestling, 85.1, 100.0);
            SetSkill(SkillName.Tracking, 50.0, 55.0);

            Fame = 7000;
            Karma = -7000;
        }

        public SilverSerpent(Serial serial)
            : base(serial)
        {
        }

        public override bool DeathAdderCharmable => true;
        public override int Meat => Utility.RandomMinMax(2, 5);


		public override int Hides => Utility.RandomMinMax(2, 5);
		public override HideType HideType => HideType.Reptilien;


		public override int Bones => Utility.RandomMinMax(2, 5);
		public override BoneType BoneType => BoneType.Reptilien;

		public override Poison PoisonImmune => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems, 2);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));
			AddLoot(LootPack.LootItem<OeufSerpent>(3, 7));


		}

		public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.1)
                c.DropItem(new SilverSerpentVenom());
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