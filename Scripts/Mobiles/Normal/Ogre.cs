using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un ogre")]
    public class Ogre : BaseCeosSpawn
	{
        [Constructable]
        public Ogre()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un ogre";
            Body = 1;
            BaseSoundID = 427;

            SetStr(166, 195);
            SetDex(46, 65);
            SetInt(46, 70);

            SetHits(100, 117);
            SetMana(0);

            SetDamage(9, 11);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 30, 35);
            SetResistance(ResistanceType.Fire, 15, 25);
            SetResistance(ResistanceType.Cold, 15, 25);
            SetResistance(ResistanceType.Poison, 15, 25);
            SetResistance(ResistanceType.Energy, 25);

            SetSkill(SkillName.MagicResist, 55.1, 70.0);
            SetSkill(SkillName.Tactics, 60.1, 70.0);
            SetSkill(SkillName.Wrestling, 70.1, 80.0);

            Fame = 3000;
            Karma = -3000;
        }

        public Ogre(Serial serial)
            : base(serial)
        {
        }

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvoutePhysique>(), Utility.RandomMinMax(2, 4));
		}
		public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 1;
        public override int Meat => 2;

		public override int Hides => Utility.RandomMinMax(3, 6);
		public override HideType HideType => HideType.Geant;


		public override int Bones => Utility.RandomMinMax(3, 6);
		public override BoneType BoneType => BoneType.Geant;


		public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Potions);
			AddLoot(LootPack.Others, Utility.RandomMinMax(3, 4));
			AddLoot(LootPack.LootItem<CheveuxGeant>(3, 7));
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