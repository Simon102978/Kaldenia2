namespace Server.Mobiles
{
    [CorpseName("le corps d'un �cureuil")]
    public class Squirrel : BaseCreature
    {
        [Constructable]
        public Squirrel()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "un �cureuil";
            Body = 0x116;

            SetStr(44, 50);
            SetDex(35);
            SetInt(5);

            SetHits(42, 50);

            SetDamage(1, 2);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 30, 34);
            SetResistance(ResistanceType.Fire, 10, 14);
            SetResistance(ResistanceType.Cold, 30, 35);
            SetResistance(ResistanceType.Poison, 20, 25);
            SetResistance(ResistanceType.Energy, 20, 25);

            SetSkill(SkillName.MagicResist, 4.0);
            SetSkill(SkillName.Tactics, 4.0);
            SetSkill(SkillName.Wrestling, 4.0);

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -21.3;
        }

        public Squirrel(Serial serial)
            : base(serial)
        {
        }

		public override bool CanBeParagon => false;
        public override bool CanReveal => false;
		public override int Meat => Utility.RandomMinMax(1, 2);


		public override int Hides => Utility.RandomMinMax(1, 2);

		public override HideType HideType => HideType.Regular;


		public override int Bones => Utility.RandomMinMax(1, 2);

		public override BoneType BoneType => BoneType.Regular;

		public override FoodType FavoriteFood => FoodType.FruitsAndVegies;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
