using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un loup sombre")]
    [TypeAlias("Server.Mobiles.Direwolf")]
    public class DireWolf : BaseCreature
    {
        [Constructable]
        public DireWolf()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un loup sombre";
            Body = 23;
            BaseSoundID = 0xE5;

            SetStr(96, 120);
            SetDex(81, 105);
            SetInt(36, 60);

            SetHits(58, 72);
            SetMana(0);

            SetDamage(11, 17);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 5, 10);
            SetResistance(ResistanceType.Poison, 5, 10);
            SetResistance(ResistanceType.Energy, 10, 15);

            SetSkill(SkillName.MagicResist, 57.6, 75.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);
            SetSkill(SkillName.Necromancy, 18.0);
            SetSkill(SkillName.SpiritSpeak, 18.0);

            Fame = 2500;
            Karma = -2500;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 83.1;
        }

		public override bool CanBeParagon => false;
        
        public override bool CanReveal => false;
		public DireWolf(Serial serial)
            : base(serial)
        {
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m is BaseCreature && ((BaseCreature)m).IsMonster && m.Karma > 0)
            {
                return true;
            }

            return base.IsEnemy(m);
        }

        public override int Meat => Utility.RandomMinMax(2, 6);
		public override int Hides => Utility.RandomMinMax(2, 6);
		public override HideType HideType => HideType.Lupus;

		public override int Bones => Utility.RandomMinMax(2, 6);
		public override BoneType BoneType => BoneType.Lupus;


		public override void GenerateLoot()
		{
			AddLoot(LootPack.LootItem<PoilsLoup>(3, 7));
		}
		




		public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Canine;
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