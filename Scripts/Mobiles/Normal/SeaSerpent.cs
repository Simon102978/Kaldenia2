using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("un corps de serpent de mer")]
    [TypeAlias("Server.Mobiles.Seaserpant")]
    public class SeaSerpent : BaseCreature
    {
        [Constructable]
        public SeaSerpent()
            : base(AIType.MaritimeMageAI, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un serpent de mer";
            Body = 150;
            BaseSoundID = 447;

            Hue = Utility.Random(0x530, 9);

            SetStr(168, 225);
            SetDex(58, 85);
            SetInt(53, 95);

            SetHits(110, 127);

            SetDamage(7, 13);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 15, 20);

            SetSkill(SkillName.MagicResist, 60.1, 75.0);
            SetSkill(SkillName.Tactics, 60.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 70.0);

            Fame = 6000;
            Karma = -6000;

            CanSwim = true;
            CantWalk = true;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public SeaSerpent(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(5, 10);

		public override MeatType MeatType => MeatType.SeaSerpentSteak;
        public override int TreasureMapLevel => Utility.RandomList(1, 2);


		public override int Hides => Utility.RandomMinMax(5, 10);

		public override HideType HideType => HideType.Reptilien;


		public override int Bones => Utility.RandomMinMax(5, 10);

		public override BoneType BoneType => BoneType.Reptilien;




		/*       public override int Hides => 10;
			   public override HideType HideType => HideType.Horned;
			   public override int Scales => 8;
			   public override ScaleType ScaleType => ScaleType.Blue;*/

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.LootItem<RawFishSteak>());
            AddLoot(LootPack.RandomLootItem(new[] { typeof(SulfurousAsh), typeof(BlackPearl) }, 100.0, 4, false, true));
			

		}

		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

			if (version == 0)
			{
				AI = AIType.MaritimeMageAI;
			}
		}
    }
}
