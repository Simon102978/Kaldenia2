using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Corps d'homme-Lézard")]
    public class LizardmanAmbusher : BaseCreature
    {
        public override bool CanStealth => true;  //Stays Hidden until Combatant in range.
        
        [Constructable]
        public LizardmanAmbusher()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("lizardman");
            Body = Utility.RandomList(35, 36);
            BaseSoundID = 417;

            SetStr(96, 120);
            SetDex(86, 105);
            SetInt(36, 60);

            Hue = 1149;

            SetHits(58, 72);

            SetDamage(5, 7);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Poison, 20);

            SetResistance(ResistanceType.Physical, 25, 30);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Cold, 5, 10);
            SetResistance(ResistanceType.Poison, 10, 20);

            SetSkill(SkillName.MagicResist, 35.1, 60.0);
            SetSkill(SkillName.Tactics, 55.1, 80.0);
            SetSkill(SkillName.Wrestling, 50.1, 70.0);
            SetSkill(SkillName.Hiding,50.0, 70.0);
            SetSkill(SkillName.Poisoning,50.0, 70.0);

            Fame = 1500;
            Karma = -1500;
        }

        public LizardmanAmbusher(Serial serial)
            : base(serial)
        {
        }

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvouteLezard>(), Utility.RandomMinMax(2, 4));
		}

        public override Poison HitPoison => Poison.Lesser;
		public override int TreasureMapLevel => 2;
        public override InhumanSpeech SpeechType => InhumanSpeech.Lizardman;
        public override bool CanRummageCorpses => true;
        public override int Meat => 1;


		public override int Hides => 4;
		public override HideType HideType => HideType.Reptilien;

		public override int Bones => 4;
		public override BoneType BoneType => BoneType.Reptilien;

		/*    public override int Hides => 12;
			public override HideType HideType => HideType.Spined;*/
		public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            RevealingAction();
            base.OnDamage(amount, from, willKill);
        }


        public override void OnDamagedBySpell(Mobile from)
        {
            RevealingAction();
            base.OnDamagedBySpell(from);
        }

        public override void OnThink()
        {

            if (!Alive || Deleted)
            {
                return;
            }

            if (!Hidden)
            {
                double chance = 0.05;

                if (Hits < 20)
                {
                    chance = 0.1;
                }

                if (Poisoned)
                {
                    chance = 0.01;
                }

                if (Utility.RandomDouble() < chance)
                {
                    HideSelf();
                }
                base.OnThink();
            }
        }

         private void HideSelf()
        {
            if (Core.TickCount >= NextSkillTime)
            {
                Effects.SendLocationParticles(
                    EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);

                PlaySound(0x22F);
                Hidden = true;

                UseSkill(SkillName.Hiding);
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