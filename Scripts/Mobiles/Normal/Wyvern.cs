using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un rautour")]
    public class Wyvern : BaseCreature
    {
        [Constructable]
        public Wyvern()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un rautour";
            Body = 62;
            BaseSoundID = 362;

            SetStr(202, 240);
            SetDex(153, 172);
            SetInt(51, 90);

            SetHits(250, 300);

            SetDamage(8, 19);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 90, 100);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.Anatomy, 85.1, 95.0);
            SetSkill(SkillName.MagicResist, 82.6, 90.5);
            SetSkill(SkillName.Tactics, 95.1, 105.0);
            SetSkill(SkillName.Wrestling, 97.6, 107.5);


            Fame = 4000;
            Karma = -4000;
        }

        public Wyvern(Serial serial)
            : base(serial)
        {
        }

        public override bool ReacquireOnMovement => true;
        public override Poison PoisonImmune => Poison.Deadly;
        public override Poison HitPoison => Poison.Deadly;
        public override int TreasureMapLevel => 2;
        public override int Meat => Utility.RandomMinMax(5, 10);
        public override int Hides => Utility.RandomMinMax(2, 3);
        public override HideType HideType => HideType.Dragonique;

		public override int Bones => Utility.RandomMinMax(2, 3);
		public override BoneType BoneType => BoneType.Dragonique;

		public override bool CanFly => true;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.MedScrolls);
            AddLoot(LootPack.LootItem<LesserPoisonPotion>(true));
			AddLoot(LootPack.LootItem<Items.GemmePoison>(), (double)5);
			AddLoot(LootPack.Others, Utility.RandomMinMax(7, 14));
			AddLoot(LootPack.LootItem<SangDragon>(4, true));


		}

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteWyvern>(), Utility.RandomMinMax(2, 4));
		}

		public override int GetAttackSound()
        {
            return 713;
        }

        public override int GetAngerSound()
        {
            return 718;
        }

        public override int GetDeathSound()
        {
            return 716;
        }

        public override int GetHurtSound()
        {
            return 721;
        }

        public override int GetIdleSound()
        {
            return 725;
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
