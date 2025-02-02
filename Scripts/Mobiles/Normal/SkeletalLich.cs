using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une liche squelette")]
    public class SkeletalLich : BaseCreature
    {
        [Constructable]
        public SkeletalLich() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "une liche squelette";
            Body = 309;
            Hue = 1345;
            BaseSoundID = 0x48D;

            SetStr(301, 350);
            SetDex(75);
            SetInt(151, 200);

            SetHits(1200);
            SetStam(150);
            SetMana(0);

            SetDamage(8, 10);

            SetDamageType(ResistanceType.Physical, 0);
            SetDamageType(ResistanceType.Cold, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 50, 70);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.EvalInt, 127.2);
            SetSkill(SkillName.Magery, 127.2);
            SetSkill(SkillName.Necromancy, 100.0, 120.0);
            SetSkill(SkillName.MagicResist, 187.1);
            SetSkill(SkillName.Tactics, 91.7);
            SetSkill(SkillName.Wrestling, 98.5);

            Fame = 6000;
            Karma = -6000;

            SetWeaponAbility(WeaponAbility.Dismount);
        }

        public SkeletalLich(Serial serial) : base(serial)
        {
        }
		
        public override bool BleedImmune => true;
		
        public override Poison PoisonImmune => Poison.Lethal;
		
        public override int TreasureMapLevel => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
			AddLoot(LootPack.LootItem<CerveauLiche>(1, 2));
			AddLoot(LootPack.Others, Utility.RandomMinMax(2, 4));


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
