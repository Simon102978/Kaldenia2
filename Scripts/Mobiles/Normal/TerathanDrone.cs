using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un drone terathan")]
    public class TerathanDrone : BaseCreature
    {
        [Constructable]
        public TerathanDrone()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un drone terathan";
            Body = 71;
            BaseSoundID = 594;

            SetStr(36, 65);
            SetDex(96, 145);
            SetInt(21, 45);

            SetHits(22, 39);
            SetMana(0);

            SetDamage(6, 12);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 15, 25);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 15, 25);

            SetSkill(SkillName.Poisoning, 40.1, 60.0);
            SetSkill(SkillName.MagicResist, 30.1, 45.0);
            SetSkill(SkillName.Tactics, 30.1, 50.0);
            SetSkill(SkillName.Wrestling, 40.1, 50.0);

            Fame = 2000;
            Karma = -2000;
        }

        public TerathanDrone(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(2, 4);

		public override TribeType Tribe => TribeType.Terathan;

		public override int Hides => Utility.RandomMinMax(2, 4);
		public override HideType HideType => HideType.Arachnide;


		public override int Bones => Utility.RandomMinMax(2, 4);
		public override BoneType BoneType => BoneType.Arachnide;


		public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.LootItem<SpidersSilk>(2));
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
