using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un loup de glace")]
    public class IceHound : BaseCreature
    {
        [Constructable]
        public IceHound()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un loup de glace";
            Body = 98;
            BaseSoundID = 229;
            Hue = 1153;

            SetStr(102, 150);
            SetDex(81, 105);
            SetInt(36, 60);

            SetHits(66, 125);

            SetDamage(11, 17);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Cold, 80);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Swords, 99.0);

            Fame = 3400;
            Karma = -3400;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 85.5;

            SetWeaponAbility(WeaponAbility.ParalyzingBlow);
        }

        public IceHound(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(1, 3);


		public override int Hides => Utility.RandomMinMax(2, 5);
		public override HideType HideType => HideType.Regular;

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteFroid>(), Utility.RandomMinMax(2, 4));
		}
		public override int Bones => Utility.RandomMinMax(2, 5);
		public override BoneType BoneType => BoneType.Regular;


		public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Canine;

        public override void GenerateLoot()
        {
       //     AddLoot(LootPack.Average);
       //     AddLoot(LootPack.Meager);
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