using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un renard")]
    public class RenardTeumesse : BaseCreature
    {
        [Constructable]
        public RenardTeumesse() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.15, 0.4)
        // : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.15, 0.4)
        {


            Name = "Renard de Teumesse";
            Body = 0x117;
            Female = true;
            Hue = 1194;

            SetStr(73, 115);
            SetDex(76, 95);
            SetInt(100, 200);

            SetHits(100, 150);


            SetDamage(7, 13);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 10, 15);
            SetResistance(ResistanceType.Cold, 20, 25);
            SetResistance(ResistanceType.Poison, 5, 10);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.MagicResist, 30.1, 35.0);
            SetSkill(SkillName.Tactics, 60.3, 75.0);
            SetSkill(SkillName.Wrestling, 70.3, 80.0);

            SetWeaponAbility(WeaponAbility.BleedAttack);
            SetSpecialAbility(SpecialAbility.GraspingClaw);

            double activeSpeed = 0.0;
			double passiveSpeed = 0.0;

			SpeedInfo.GetCustomSpeeds(this, ref activeSpeed, ref passiveSpeed);
		
			ActiveSpeed = activeSpeed;
			PassiveSpeed = passiveSpeed;
			CurrentSpeed = passiveSpeed;


        }

        public override void GenerateLoot()
        {
          
            AddLoot(LootPack.Average);     
			AddLoot(LootPack.Others, Utility.RandomMinMax(0, 2));

		}

        public RenardTeumesse(Serial serial) : base(serial)
        {
        }

        public override int Meat => 5;
        public override FoodType FavoriteFood => FoodType.Meat;

        public override bool CanBeParagon => false;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (version == 0)
            {
                SetSpecialAbility(SpecialAbility.GraspingClaw);
                SetWeaponAbility(WeaponAbility.BleedAttack);
            }
        }
    }
}