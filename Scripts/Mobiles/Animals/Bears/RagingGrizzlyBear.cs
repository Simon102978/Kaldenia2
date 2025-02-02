namespace Server.Mobiles
{
    [CorpseName("le corps d'un grizzly")]
    public class RagingGrizzlyBear : BaseCreature
    {
        [Constructable]
        public RagingGrizzlyBear()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "un grizzly enrage";
            Body = 212;
            BaseSoundID = 0xA3;

            SetStr(1251, 1550);
            SetDex(801, 1050);
            SetInt(151, 400);

            SetHits(751, 930);
            SetMana(0);

            SetDamage(18, 23);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 50, 70);
            SetResistance(ResistanceType.Cold, 30, 50);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Wrestling, 73.4, 88.1);
            SetSkill(SkillName.Tactics, 73.6, 110.5);
            SetSkill(SkillName.MagicResist, 32.8, 54.6);
            SetSkill(SkillName.Anatomy, 0, 0);

     //       Fame = 10000;  //Guessing here
      //      Karma = 10000;  //Guessing here

            Tamable = false;
        }

        public RagingGrizzlyBear(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(1, 6);
		public override int Hides => Utility.RandomMinMax(1, 6);
		public override int Bones => Utility.RandomMinMax(1, 6);
		public override PackInstinct PackInstinct => PackInstinct.Bear;
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
