using Server.Items;
using Server.Misc;
using Server.Network;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName("Le corps d'un Kepush")]
	public class KepushDEfender : KepushBase
	{
        public bool BlockReflect { get; set; }


		[Constructable]
        public KepushDEfender()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Title = "Un Kepush";

			SpeechHue = Utility.RandomDyedHue();
			Race = BaseRace.GetRace(7);

			SetStr(200, 250);
			SetDex(31, 45);
			SetInt(75, 100);

			SetSkill(SkillName.ArmsLore, 64.0, 80.0);
            SetSkill(SkillName.Parry, 80.0, 100.0);
            SetSkill(SkillName.Swords, 80.0, 100.0);

//			Hue = Utility.RandomSkinHue();

			if (Female = Utility.RandomBool())
			{
				Body = 0x191;
				Name = NameList.RandomName("savage");

                AddItem(new LeatherSkirt());
                AddItem(new LeatherBustierArms());
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName("savage");
                  AddItem(new LeatherShorts());
			}

			switch (Utility.Random(3))
            {
                case 0:
                    AddItem(new Lajatang());
                    break;
                case 1:
                    AddItem(new Wakizashi());
                    break;
                case 2:
                    AddItem(new NoDachi());
                    break;
            }


            AddItem(new ThighBoots(Utility.RandomNeutralHue()));
            AddItem(new TribalMask());





         
            int hairHue = Utility.RandomNondyedHue();

            Utility.AssignRandomHair(this, hairHue);

            if (Utility.Random(7) != 0)
                Utility.AssignRandomFacialHair(this, hairHue);
        }

        public override void GenerateLoot()
        {
			AddLoot(LootPack.Rich);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));

		}




		public override void OnThink()
		{
			base.OnThink();

		

		}

       	public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            int dam = base.Damage(amount, from, informMount, checkDisrupt);

            if (!BlockReflect && from != null && dam > 0)
            {
                BlockReflect = true;
                AOS.Damage(from, this, dam, 0, 0, 0, 0, 0, 0, 100);
                BlockReflect = false;

                from.PlaySound(0x1F1);
            }

            return dam;
        }

        public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{

		 	Hits += AOS.Scale(damage, 50);

			Effects.SendPacket(to.Location, to.Map, new ParticleEffect(EffectType.FixedFrom, to.Serial, Serial.Zero, 0x377A, to.Location, to.Location, 1, 15, false, false, 1926, 0, 0, 9502, 1, to.Serial, 16, 0));
            Effects.SendPacket(to.Location, to.Map, new ParticleEffect(EffectType.FixedFrom, to.Serial, Serial.Zero, 0x3728, to.Location, to.Location, 1, 12, false, false, 1963, 0, 0, 9042, 1, to.Serial, 16, 0));

			Parole();
			base.AlterMeleeDamageTo(to, ref damage);
		}

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{

				base.OnDamage(amount, from, willKill);

		}

		

		public KepushDEfender(Serial serial)
            : base(serial)
        {
        }

		public override bool ClickTitle => false;


		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
