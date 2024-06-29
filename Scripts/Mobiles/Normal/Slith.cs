using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un lezard")]
    public class Slith : BaseCreature
    {
        [Constructable]
        public Slith() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un lezard";
            Body = 734;

            SetStr(129, 136);
            SetDex(72, 75);
            SetInt(12, 13);

            SetHits(84, 85);

            SetDamage(6, 24);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 40);
            SetResistance(ResistanceType.Fire, 35, 45);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 25, 30);

            SetSkill(SkillName.MagicResist, 59.1, 63.5);
            SetSkill(SkillName.Tactics, 74.6, 76.4);
            SetSkill(SkillName.Wrestling, 62.0, 77.1);

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 80.7;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public Slith(Serial serial) : base(serial)
        {
        }

        public override int DragonBlood => 8;

        public override int TreasureMapLevel => 2;
        public override int Meat => Utility.RandomMinMax(3, 6);


		public override int Hides => Utility.RandomMinMax(3, 6);
		public override HideType HideType => HideType.Reptilien;


		public override int Bones => Utility.RandomMinMax(3, 6);
		public override BoneType BoneType => BoneType.Reptilien;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));

		}

		public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (!Controlled && Utility.RandomDouble() < 0.05)
            {
                c.DropItem(new SlithEye());
            }

            if (!Controlled && Utility.RandomDouble() < 0.25)
            {
                switch (Utility.Random(2))
                {
                    case 0:
                        c.DropItem(new AncientPotteryFragments());
                        break;
                    case 1:
                        c.DropItem(new TatteredAncientScroll());
                        break;
                }
            }
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
