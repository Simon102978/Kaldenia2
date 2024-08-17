using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une panth�re")]
    public class Panther : BaseCreature
    {
        [Constructable]
        public Panther()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "une panth�re";
            Body = 0xD6;
            Hue = 0x901;
            BaseSoundID = 0x462;

            SetStr(61, 85);
            SetDex(86, 105);
            SetInt(26, 50);

            SetHits(37, 51);
            SetMana(0);

            SetDamage(4, 12);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Cold, 10, 15);
            SetResistance(ResistanceType.Poison, 5, 10);

            SetSkill(SkillName.MagicResist, 15.1, 30.0);
            SetSkill(SkillName.Tactics, 50.1, 65.0);
            SetSkill(SkillName.Wrestling, 50.1, 65.0);

            Fame = 450;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 53.1;
        }

        public Panther(Serial serial)
            : base(serial)
        {
        }

		public override bool CanBeParagon => false;
        public override bool CanReveal => false;
		public override int Meat => Utility.RandomMinMax(1, 3);

		public override int Hides => Utility.RandomMinMax(2, 6);
		public override FoodType FavoriteFood => FoodType.Meat | FoodType.Fish;
        public override PackInstinct PackInstinct => PackInstinct.Feline;

		public override void GenerateLoot()
		{
			AddLoot(LootPack.LootItem<PattesPanthere>(1, 2));
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
