using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une araign�e de glace")]
    public class FrostSpider : BaseCreature
    {
        [Constructable]
        public FrostSpider()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "une araign�e de glace";
            Body = 20;
            BaseSoundID = 0x388;

            if (Utility.RandomBool())
                Hue = 1154;

            SetStr(76, 100);
            SetDex(126, 145);
            SetInt(36, 60);

            SetHits(46, 60);
            SetMana(0);

            SetDamage(6, 16);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Cold, 80);

            SetResistance(ResistanceType.Physical, 25, 30);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.MagicResist, 25.1, 40.0);
            SetSkill(SkillName.Tactics, 35.1, 50.0);
            SetSkill(SkillName.Wrestling, 50.1, 65.0);

            Fame = 775;
            Karma = -775;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 74.7;
        }

        public FrostSpider(Serial serial)
            : base(serial)
        {
        }
		public override int Hides => Utility.RandomMinMax(5, 10);

		public override HideType HideType => HideType.Arachnide;


		public override int Bones => Utility.RandomMinMax(5, 10);

		public override BoneType BoneType => BoneType.Arachnide;
		public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Arachnid;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Poor);
            AddLoot(LootPack.LootItem<SpidersSilk>(7));
			AddLoot(LootPack.LootItem<VeninTarenlune>(1, 3));
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
