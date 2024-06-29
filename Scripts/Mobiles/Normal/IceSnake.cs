using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un serpent de glace")]
    public class IceSnake : BaseCreature
    {
        [Constructable]
        public IceSnake()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "une couleuvre de glace";
            Body = 52;
            Hue = 0x480;
            BaseSoundID = 0xDB;

            SetStr(42, 54);
            SetDex(36, 45);
            SetInt(26, 30);

            SetMana(0);

            SetDamage(4, 12);

            SetDamageType(ResistanceType.Physical, 25);
            SetDamageType(ResistanceType.Cold, 25);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Cold, 80, 90);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 39.3, 54.0);
            SetSkill(SkillName.Wrestling, 39.3, 54.0);

            Fame = 900;
            Karma = -900;
        }

        public IceSnake(Serial serial)
            : base(serial)
        {
        }

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteFroid>(), Utility.RandomMinMax(2, 4));
		}


		public override bool DeathAdderCharmable => true;
        public override int Meat => 1;

		public override int Hides => 1;
		public override HideType HideType => HideType.Reptilien;


		public override int Bones => 1;
		public override BoneType BoneType => BoneType.Reptilien;

		public override void GenerateLoot()
        {
           // AddLoot(LootPack.Meager);
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