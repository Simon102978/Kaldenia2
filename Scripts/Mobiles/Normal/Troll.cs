using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corps de Troll")]
    public class Troll : BaseCeosSpawn
	{
        [Constructable]
        public Troll()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un troll";
			Body = 53; /* Utility.RandomList(53, 54);*/
            BaseSoundID = 461;

            SetStr(176, 205);
            SetDex(46, 65);
            SetInt(46, 70);

            SetHits(106, 123);

            SetDamage(8, 14);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 25, 35);
            SetResistance(ResistanceType.Cold, 15, 25);
            SetResistance(ResistanceType.Poison, 5, 15);
            SetResistance(ResistanceType.Energy, 5, 15);

            SetSkill(SkillName.MagicResist, 45.1, 60.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 50.1, 70.0);

            Fame = 3500;
            Karma = -3500;
        }

        public Troll(Serial serial)
            : base(serial)
        {
        }

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvoutePhysique>(), Utility.RandomMinMax(2, 4));
		}
		public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 1;
        public override int Meat => Utility.RandomMinMax(3, 5);
		public override int Hides => Utility.RandomMinMax(3, 5);
		public override HideType HideType => HideType.Regular;


		public override int Bones => Utility.RandomMinMax(3, 5);

		public override BoneType BoneType => BoneType.Regular;
		public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
			AddLoot(LootPack.LootItem<CheveuxTroll>(5, true));
			AddLoot(LootPack.Others, Utility.RandomMinMax(3, 5));

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