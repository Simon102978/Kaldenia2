using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Corps de Salazar")]
    public class Salazar : BaseCreature
    {
        [Constructable]
        public Salazar()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Salazar";
            Body = 35;
            BaseSoundID = 417;

            SetStr(177, 195);
            SetDex(251, 269);
            SetInt(153, 170);
            Hue = 1158;

            SetHits(350, 400);

            SetDamage(13, 24);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 55, 60);
            SetResistance(ResistanceType.Fire, 25, 35);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 55.0, 65.0);
            SetSkill(SkillName.Tactics, 80.0, 100.0);
            SetSkill(SkillName.Wrestling, 80.0, 100.0);

            SetSkill(SkillName.Musicianship, 100);
            SetSkill(SkillName.Discordance, 100);
            SetSkill(SkillName.Provocation, 100);
            SetSkill(SkillName.Peacemaking, 100);

            Fame = 5000;
            Karma = 0;
        }

        public Salazar(Serial serial)
            : base(serial)
        {
        }

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteLezard>(), Utility.RandomMinMax(2, 4));
            
		}

		public override int TreasureMapLevel => 1;
        public override InhumanSpeech SpeechType => InhumanSpeech.Lizardman;
        public override bool CanRummageCorpses => true;
        public override int Meat => 1;

		public override int Hides => 4;
		public override HideType HideType => HideType.Reptilien;

		public override int Bones => 4;
		public override BoneType BoneType => BoneType.Reptilien;

       	public override bool CanDiscord => true;
        public override bool CanPeace => true;
        public override bool CanProvoke => true;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls);
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