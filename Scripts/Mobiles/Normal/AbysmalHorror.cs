using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une horreur abyssal")]
    public class AbysmalHorror : BaseCreature
    {
        [Constructable]
        public AbysmalHorror()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "une horreur abyssal";
            Body = 312;
            BaseSoundID = 0x451;

            SetStr(401, 420);
            SetDex(81, 90);
            SetInt(401, 420);

            SetHits(6000);

            SetDamage(13, 17);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 30, 35);
            SetResistance(ResistanceType.Fire, 100);
            SetResistance(ResistanceType.Cold, 50, 55);
            SetResistance(ResistanceType.Poison, 60, 65);
            SetResistance(ResistanceType.Energy, 77, 80);

            SetSkill(SkillName.Wrestling, 84.1, 88.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.MagicResist, 117.6, 120.0);
            SetSkill(SkillName.Poisoning, 70.0, 80.0);
            SetSkill(SkillName.Tracking, 100.0);
            SetSkill(SkillName.Magery, 112.6, 117.5);
            SetSkill(SkillName.EvalInt, 200.0);
            SetSkill(SkillName.Meditation, 200.0);
            SetSkill(SkillName.Necromancy, 120.0);
            SetSkill(SkillName.SpiritSpeak, 120.0);
            SetSkill(SkillName.Focus, 10.0, 20.0);

            Fame = 26000;
            Karma = -26000;

            SetWeaponAbility(WeaponAbility.MortalStrike);
            SetWeaponAbility(WeaponAbility.WhirlwindAttack);
            SetWeaponAbility(WeaponAbility.Block);
            //Arcane Pyromancy - Missing ability 
        }

        public AbysmalHorror(Serial serial)
            : base(serial)
        {
        }

        public override bool CanFlee => false;
        public override bool IgnoreYoungProtection => true;
        public override bool Unprovokable => true;
        public override bool AreaPeaceImmune => true;
        public override Poison PoisonImmune => Poison.Lethal;
        public override int TreasureMapLevel => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
			AddLoot(LootPack.Others, Utility.RandomMinMax(5, 10));
			AddLoot(LootPack.LootItem<CerveauSpectre>(4, true));
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
