using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un loup blanc")]
    public class WhiteWolf : BaseCreature
    {
        [Constructable]
        public WhiteWolf()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "un loup blanc";
            Body = Utility.RandomList(34, 37);
            BaseSoundID = 0xE5;

            SetStr(56, 80);
            SetDex(56, 75);
            SetInt(31, 55);

            SetHits(34, 48);
            SetMana(0);

            SetDamage(3, 7);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 15, 20);
            SetResistance(ResistanceType.Fire, 10, 15);
            SetResistance(ResistanceType.Cold, 20, 25);
            SetResistance(ResistanceType.Poison, 10, 15);
            SetResistance(ResistanceType.Energy, 10, 15);

            SetSkill(SkillName.MagicResist, 20.1, 35.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 45.1, 60.0);

            Fame = 450;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 65.1;
        }

		public override bool CanBeParagon => false;
       	public override bool CanReveal => false;
		public WhiteWolf(Serial serial)
            : base(serial)
        {
        }
		public override int Hides => Utility.RandomMinMax(2, 4);
		public override HideType HideType => HideType.Lupus;

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteFroid>(), Utility.RandomMinMax(2, 4));
			AddLoot(LootPack.LootItem<PoilsLoup>(3, 7));
		}
		public override int Bones => Utility.RandomMinMax(2, 4);
		public override BoneType BoneType => BoneType.Lupus;

		
        public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Canine;

		public override void GenerateLoot()
		{
			AddLoot(LootPack.LootItem<PoilsLoup>());
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
