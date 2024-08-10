using Server.Items;
using System;

namespace Server.Mobiles
{
	[CorpseName("Le corps d'un Kepush")]
	public class KepushMiniAmbusher  : KepushBase
	{
        public override bool ClickTitle => false;
		public override bool AlwaysMurderer => true;
		public override bool CanStealth => true;

        [Constructable]
        public KepushMiniAmbusher () : base(AIType.AI_Melee , FightMode.Closest, 10, 1, 0.2, 0.4)
        {
			SpeechHue = Utility.RandomDyedHue();
			Race = BaseRace.GetRace(7);

	//		Hue = Utility.RandomSkinHue();

			if (Female = Utility.RandomBool())
			{
				Body = 0x191;
                AddItem(new LeatherSkirt());
                AddItem(new LeatherBustierArms());
				
			}
			else
			{
				Body = 0x190;
                AddItem(new LeatherShorts());
			
			}

            Name = NameList.RandomName("savage");

			SetHits(251, 350);

            SetStr(126, 225);
            SetDex(81, 95);
            SetInt(151, 165);

            SetDamage(5, 15);

            SetDamageType(ResistanceType.Physical, 65);
            SetDamageType(ResistanceType.Fire, 15);
            SetDamageType(ResistanceType.Poison, 15);
            SetDamageType(ResistanceType.Energy, 5);

            SetResistance(ResistanceType.Physical, 35, 65);
            SetResistance(ResistanceType.Fire, 40, 60);
            SetResistance(ResistanceType.Cold, 25, 45);
            SetResistance(ResistanceType.Poison, 40, 60);
            SetResistance(ResistanceType.Energy, 35, 55);

            SetSkill(SkillName.Anatomy, 105.0, 120.0);
            SetSkill(SkillName.MagicResist, 80.0, 100.0);
            SetSkill(SkillName.Tactics, 115.0, 130.0);
            SetSkill(SkillName.Wrestling, 95.0, 120.0);
            SetSkill(SkillName.Fencing, 95.0, 120.0);
            SetSkill(SkillName.Macing, 95.0, 120.0);
            SetSkill(SkillName.Swords, 95.0, 120.0);

            SetSkill(SkillName.Ninjitsu, 95.0, 120.0);
            SetSkill(SkillName.Hiding, 100.0);
        //    SetSkill(SkillName.Stealth, 120.0);

            Fame = 8500;
            Karma = -8500;


            AddItem(new Kama());
            AddItem(new ThighBoots(Utility.RandomNeutralHue()));
            AddItem(new TribalMask());


            Utility.AssignRandomHair(this);
  
        }

        public override bool BardImmune => true;

		public override void GenerateLoot()
        {
			AddLoot(LootPack.Rich, 1);
			AddLoot(LootPack.Others, Utility.RandomMinMax(3, 4));
			AddLoot(LootPack.Potions, Utility.RandomMinMax(1, 2));
			AddLoot(LootPack.Statue,1,5);
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



        public KepushMiniAmbusher (Serial serial) : base(serial)
        {
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