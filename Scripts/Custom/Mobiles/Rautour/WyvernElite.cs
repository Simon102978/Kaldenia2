using Server.Items;
using Server.Multis;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un rautour")]
    public class WyvernElite : BaseCreature
    {
        [Constructable]
        public WyvernElite()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un rautour enragé";
            Body = 62;
            BaseSoundID = 362;
            Hue = 1172;

            SetStr(300, 400);
            SetDex(253, 272);
            SetInt(250, 350);

            SetHits(300, 500);
            SetMana(1000);

            SetDamage(8, 19);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 15, 35);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 90, 100);
            SetResistance(ResistanceType.Energy, 30, 40);

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

        public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
		{
			
             if (Combatant != null && BaseBoat.FindBoatAt(Combatant) != null && BaseBoat.FindBoatAt(this) == null)
            {
                Emote("*Déploie ses ailes et saute.*");


                Location = Combatant.Location;

            }

			return base.Damage(amount,from,informMount,checkDisrupt);
		}

       	public override void OnThink()
		{
			base.OnThink();

            if (Combatant != null && BaseBoat.FindBoatAt(Combatant) != null && BaseBoat.FindBoatAt(this) == null)
            {
                Emote("Déploie ses ailes et saute.");
                Location = Combatant.Location;

            }

		}

        public WyvernElite(Serial serial)
            : base(serial)
        {
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
