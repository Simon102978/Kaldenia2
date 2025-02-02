using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un serpent de glace")]
    public class IceSerpent : BaseCreature
    {
        [Constructable]
        public IceSerpent()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un serpent de glace geant";
            Body = 89;
            BaseSoundID = 219;

            SetStr(216, 245);
            SetDex(26, 50);
            SetInt(66, 85);

            SetHits(130, 147);
            SetMana(0);

            SetDamage(7, 17);

            SetDamageType(ResistanceType.Physical, 10);
            SetDamageType(ResistanceType.Cold, 90);

            SetResistance(ResistanceType.Physical, 30, 35);
            SetResistance(ResistanceType.Cold, 80, 90);
            SetResistance(ResistanceType.Poison, 15, 25);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Anatomy, 27.5, 50.0);
            SetSkill(SkillName.MagicResist, 25.1, 40.0);
            SetSkill(SkillName.Tactics, 75.1, 80.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 3500;
            Karma = -3500;
        }

        public IceSerpent(Serial serial)
            : base(serial)
        {
        }

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteFroid>(), Utility.RandomMinMax(2, 4));
		}
		public override bool DeathAdderCharmable => true;
        public override int Meat => Utility.RandomMinMax(5, 10);

		/*       public override int Hides => 15;
			   public override HideType HideType => HideType.Spined;*/

		public override int Hides => Utility.RandomMinMax(5, 10);

		public override HideType HideType => HideType.Reptilien;


		public override int Bones => Utility.RandomMinMax(5, 10);

		public override BoneType BoneType => BoneType.Reptilien;
		public override void GenerateLoot()
        {
     //       AddLoot(LootPack.Meager);
       //     AddLoot(LootPack.LootItem<GlacialStaff>(2.5));
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
