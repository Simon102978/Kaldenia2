using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une pixie")]
    public class Pixie : BaseCreature
    {
        [Constructable]
        public Pixie()
            : base(AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("pixie");
            Body = 128;
            BaseSoundID = 0x467;

            SetStr(21, 30);
            SetDex(301, 400);
            SetInt(201, 250);

            SetHits(13, 18);

            SetDamage(9, 15);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 80, 90);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.Meditation, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 100.5, 150.0);
            SetSkill(SkillName.Tactics, 10.1, 20.0);
            SetSkill(SkillName.Wrestling, 10.1, 12.5);

            Fame = 7000;
            Karma = 7000;
        }

        public Pixie(Serial serial)
            : base(serial)
        {
        }

        public override bool InitialInnocent => true;
		//   public override HideType HideType => HideType.Spined;
		//   public override int Hides => 5;


		public override int Hides => Utility.RandomMinMax(2, 4);

		public override HideType HideType => HideType.Regular;


		public override int Bones => Utility.RandomMinMax(2, 4);
		public override BoneType BoneType => BoneType.Regular;


		public override int Meat => Utility.RandomMinMax(2, 4);

		public override TribeType Tribe => TribeType.Fey;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.Gems, 2);
            AddLoot(LootPack.Statue);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));

		}

		public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.3)
                c.DropItem(new PixieLeg());
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
