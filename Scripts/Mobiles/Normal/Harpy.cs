using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une harpie")]
    public class Harpy : BaseCreature
    {

        [Constructable]
        public Harpy()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "une harpie";
            Body = 30;
            BaseSoundID = 402;

            SetStr(96, 120);
            SetDex(86, 110);
            SetInt(51, 75);

            SetHits(58, 72);

            SetDamage(5, 7);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 30);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 10, 30);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.MagicResist, 50.1, 65.0);
            SetSkill(SkillName.Tactics, 70.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 90.0);

            Fame = 2500;
            Karma = -2500;
        }

        public Harpy(Serial serial)
            : base(serial)
        {
        }

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteEnergie>(), Utility.RandomMinMax(2, 4));
		}

		public override bool CanRummageCorpses => true;
        public override int Meat => Utility.RandomMinMax(2, 4);

		public override MeatType MeatType => MeatType.Bird;
        public override int Feathers => Utility.RandomMinMax(15, 50);
		public override bool CanFly => true;
        public override void GenerateLoot()
        {
			AddLoot(LootPack.Meager, 2);
			AddLoot(LootPack.RandomLootItem(new System.Type[] { typeof(SilverRing), typeof(Necklace), typeof(SilverNecklace), typeof(Collier), typeof(Collier2) }, 5.0, 1, false, true));
			AddLoot(LootPack.LootItem<PlumesHarpie>(2, 5));
		}

        public override int GetAttackSound()
        {
            return 916;
        }

        public override int GetAngerSound()
        {
            return 916;
        }

        public override int GetDeathSound()
        {
            return 917;
        }

        public override int GetHurtSound()
        {
            return 919;
        }

        public override int GetIdleSound()
        {
            return 918;
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