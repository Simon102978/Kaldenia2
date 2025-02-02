namespace Server.Mobiles
{
    [CorpseName("le corps d'un vers de glace g�ant")]
    public class GiantIceWorm : BaseCreature
    {
        [Constructable]
        public GiantIceWorm()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 89;
            Name = "un vers de glace g�ant";
            BaseSoundID = 0xDC;

            SetStr(216, 245);
            SetDex(76, 100);
            SetInt(66, 85);

            SetHits(130, 147);

            SetDamage(7, 17);

            SetDamageType(ResistanceType.Physical, 10);
            SetDamageType(ResistanceType.Cold, 90);

            SetResistance(ResistanceType.Physical, 30, 35);
            SetResistance(ResistanceType.Fire, 0);
            SetResistance(ResistanceType.Cold, 80, 90);
            SetResistance(ResistanceType.Poison, 15, 25);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Poisoning, 75.1, 95.0);
            SetSkill(SkillName.MagicResist, 45.1, 60.0);
            SetSkill(SkillName.Tactics, 75.1, 80.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 4500;
            Karma = -4500;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 71.1;
        }

        public GiantIceWorm(Serial serial)
            : base(serial)
        {
        }

        public override bool SubdueBeforeTame => true;
        public override Poison PoisonImmune => Poison.Greater;
        public override Poison HitPoison => Poison.Greater;
        public override FoodType FavoriteFood => FoodType.Meat;

        public override bool StatLossAfterTame => true;

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