using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un ettin")]
    public class EttinArcherBarde : BaseCreature
    {
        [Constructable]
        public EttinArcherBarde()
            : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "un ettin batteur";
            Body = 18;
            BaseSoundID = 367;

            Hue = 2137;

            SetStr(336, 385);
            SetDex(300, 715);
            SetInt(31, 55);

            SetHits(600, 731);
            SetMana(1000);

            SetDamage(15, 25);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 45, 50);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 60.3, 105.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Archery, 100.1, 110.0);
            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Magery, 70.1, 80.0);
            SetSkill(SkillName.Meditation, 85.1, 95.0);
            SetSkill(SkillName.Musicianship, 100);
			SetSkill(SkillName.Discordance, 100);
			SetSkill(SkillName.Provocation, 100);
			SetSkill(SkillName.Peacemaking, 100);


            AddItem(new GantOeilDoux());


        }

        public EttinArcherBarde(Serial serial)
            : base(serial)
        {
        }

       	public override bool CanDiscord => true;
		public override bool CanPeace => true;
		public override bool CanProvoke => true;

		public override int Hides => Utility.RandomMinMax(2, 4);

		public override HideType HideType => HideType.Geant;

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvoutePhysique>(), Utility.RandomMinMax(2, 4));
		}
		public override int Bones =>  Utility.RandomMinMax(2, 4);
		public override BoneType BoneType => BoneType.Geant;
		public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 1;
        public override int Meat => Utility.RandomMinMax(2, 4);
		public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich,5);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Potions);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));
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