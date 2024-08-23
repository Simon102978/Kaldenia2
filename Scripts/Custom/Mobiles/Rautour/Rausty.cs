using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un rautour")]
    public class Rausty : BaseCreature
    {
        public bool BlockReflect { get; set; }

        [Constructable]
        public Rausty()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Rausty";
            Body = 62;
            BaseSoundID = 362;
            Hue = 2078;

            SetStr(500, 600);
            SetDex(200, 300);
            SetInt(200, 300);

            SetHits(1000, 1500);
            SetMana(1000, 1500);
            SetDamage(10, 25);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 90, 100);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.Poisoning, 90.1, 100.0);
            SetSkill(SkillName.Anatomy, 85.1, 95.0);
            SetSkill(SkillName.MagicResist, 82.6, 90.5);
            SetSkill(SkillName.Tactics, 95.1, 105.0);
            SetSkill(SkillName.Wrestling, 97.6, 107.5);
            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Magery, 70.1, 80.0);
            SetSkill(SkillName.Meditation, 85.1, 95.0);

            Fame = 4000;
            Karma = -4000;
        }

        public Rausty(Serial serial)
            : base(serial)
        {
        }

        public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{

			if (to is BaseCreature creature)
			{
				if (creature.Controlled || creature.Summoned)
				{

					Say("Yummy !! *Tout en fesant une bouché de " + creature.Name + "*");

					if (Hits < HitsMax)
						Hits = HitsMax;

					creature.Kill();

					Effects.PlaySound(Location, Map, 0x574);
				}
			}

			base.AlterMeleeDamageTo(to, ref damage);
		}

	    public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
		{
			int dam = base.Damage(amount, from, informMount, checkDisrupt);

			if (!BlockReflect && from != null && dam > 0)
			{
				BlockReflect = true;
				AOS.Damage(from, this, dam, 0, 0, 0, 0, 0, 0, 50);
				BlockReflect = false;

				from.PlaySound(0x1F1);
			}

			return dam;
		}


        public override bool ReacquireOnMovement => true;
        public override Poison PoisonImmune => Poison.Deadly;
        public override Poison HitPoison => Poison.Deadly;
        public override int TreasureMapLevel => 2;
        public override int Meat => Utility.RandomMinMax(5, 10);
        public override int Hides => Utility.RandomMinMax(6, 12);
        public override HideType HideType => HideType.Dragonique;

		public override int Bones => Utility.RandomMinMax(6, 12);
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
            AddLoot(LootPack.LootItem<Items.Gold>(100,200));
        


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
