using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un lezard")]
    public class ToxicSlith : BaseCreature
    {
        [Constructable]
        public ToxicSlith() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un lezard toxique";
            Body = 734;
            Hue = 476;

            SetStr(223, 306);
            SetDex(231, 258);
            SetInt(30, 35);

            SetHits(197, 215);
            SetStam(231, 258);

            SetDamage(6, 24);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 0, 9);
            SetResistance(ResistanceType.Cold, 5, 10);
            SetResistance(ResistanceType.Poison, 100, 100);
            SetResistance(ResistanceType.Energy, 5, 7);

            SetSkill(SkillName.MagicResist, 95.4, 98.3);
            SetSkill(SkillName.Tactics, 85.5, 90.9);
            SetSkill(SkillName.Wrestling, 90.4, 95.1);
            SetSkill(SkillName.Poisoning, 90.0, 110.0);

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public override int DragonBlood => Utility.RandomMinMax(3, 6);



		public ToxicSlith(Serial serial) : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(3, 6);


		public override int Hides => Utility.RandomMinMax(3, 6);

		public override HideType HideType => HideType.Reptilien;


		public override int Bones => Utility.RandomMinMax(3, 6);
		public override BoneType BoneType => BoneType.Reptilien;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));
			AddLoot(LootPack.LootItem<OeufSerpent>(2, 4));


		}

		public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.05)
            {
                switch (Utility.Random(2))
                {
                    case 0:
                        c.DropItem(new ToxicVenomSac());
                        break;
                    case 2:
                        c.DropItem(new SlithEye());
                        break;
                }
            }

            if (Utility.RandomDouble() < 0.25)
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
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
