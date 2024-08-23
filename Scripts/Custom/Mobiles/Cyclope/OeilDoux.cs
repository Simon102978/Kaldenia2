using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un cyclope")]
    public class OeilDoux : BaseCreature
    {
        [Constructable]
        public OeilDoux()
            : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Oeil Doux";
            Title = "Le lanceur de pierres";
            Body = 75;
            BaseSoundID = 604;
            Hue = 1165;

            SetStr(336, 385);
            SetDex(200, 300);
            SetInt(31, 55);

            SetHits(600, 701);


            SetDamage(15, 25);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 60, 70);
            SetResistance(ResistanceType.Fire, 60, 80);
            SetResistance(ResistanceType.Cold, 65, 85);
            SetResistance(ResistanceType.Poison, 60, 80);
            SetResistance(ResistanceType.Energy, 60, 80);

            SetSkill(SkillName.MagicResist, 60.3, 105.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Archery, 100.1, 120.0);
            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Magery, 70.1, 80.0);
            SetSkill(SkillName.Meditation, 85.1, 95.0);

            Fame = 4500;
            Karma = -4500;

            AddItem(new GantOeilDoux());

			SetWeaponAbility(WeaponAbility.DoubleShot);

			SetWeaponAbility(WeaponAbility.LightningArrow);
        }

        public OeilDoux(Serial serial)
            : base(serial)
        {
        }
		public override int Hides => Utility.RandomMinMax(5, 10);

		public override HideType HideType => HideType.Geant;


		public override int Bones => Utility.RandomMinMax(5, 10);

		public override BoneType BoneType => BoneType.Geant;
		public override int Meat => Utility.RandomMinMax(5, 10);

		public override int TreasureMapLevel => 3;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
			AddLoot(LootPack.Others, Utility.RandomMinMax(3, 7));
            AddLoot(LootPack.LootItem<Items.Gold>(200,300));

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