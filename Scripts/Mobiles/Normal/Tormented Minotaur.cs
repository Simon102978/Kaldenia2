using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un minotaure")]
    public class TormentedMinotaur : BaseCreature
    {
        [Constructable]
        public TormentedMinotaur()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un minotaure tourmenté";
            Body = 262;

            SetStr(822, 930);
            SetDex(401, 415);
            SetInt(128, 138);

            SetHits(4000, 4200);

            SetDamage(16, 30);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 62);
            SetResistance(ResistanceType.Fire, 74);
            SetResistance(ResistanceType.Cold, 54);
            SetResistance(ResistanceType.Poison, 56);
            SetResistance(ResistanceType.Energy, 54);

            SetSkill(SkillName.Wrestling, 110.1, 111.0);
            SetSkill(SkillName.Tactics, 100.7, 102.8);
            SetSkill(SkillName.MagicResist, 104.3, 116.3);

            Fame = 20000;
            Karma = -20000;

            SetWeaponAbility(WeaponAbility.Dismount);
        }

        public TormentedMinotaur(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmune => Poison.Deadly;
        public override int TreasureMapLevel => 3;
		public override int Hides => Utility.RandomMinMax(3, 5);

		public override HideType HideType => HideType.Geant;


		public override int Bones => Utility.RandomMinMax(3, 5);
		public override BoneType BoneType => BoneType.Geant;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 10);
			AddLoot(LootPack.Others, Utility.RandomMinMax(4, 7));
			AddLoot(LootPack.LootItem<CheveuxGeant>(4, true));


		}

		public override int GetDeathSound()
        {
            return 0x596;
        }

        public override int GetAttackSound()
        {
            return 0x597;
        }

        public override int GetIdleSound()
        {
            return 0x598;
        }

        public override int GetAngerSound()
        {
            return 0x599;
        }

        public override int GetHurtSound()
        {
            return 0x59A;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}