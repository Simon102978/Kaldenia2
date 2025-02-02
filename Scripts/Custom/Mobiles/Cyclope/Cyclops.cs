namespace Server.Mobiles
{
    [CorpseName("le corps d'un cyclope")]
    public class Cyclops : BaseCreature
    {
        [Constructable]
        public Cyclops()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un cyclope";
            Body = 75;
            BaseSoundID = 604;

            SetStr(336, 385);
            SetDex(96, 115);
            SetInt(31, 55);

            SetHits(202, 231);
            SetMana(0);

            SetDamage(7, 23);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 45, 50);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 60.3, 105.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Wrestling, 80.1, 90.0);

            Fame = 4500;
            Karma = -4500;
        }

        public Cyclops(Serial serial)
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