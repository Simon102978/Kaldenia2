using Server.Items;
using Server.Misc;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName("Le corps d'un Kepush")]
	public class KepushGuerrier : KepushBase
	{
		public DateTime DelayCharge;
	
		[Constructable]
        public KepushGuerrier()
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

		public void Charge()
		{
			if (DelayCharge < DateTime.UtcNow)
			{
				if (Combatant is CustomPlayerMobile cp)
				{

				
						Emote($"*Charge {cp.Name}*");
					
						cp.Damage(35);

						cp.Freeze(TimeSpan.FromSeconds(3));

						this.Location = cp.Location;
					

				}


				DelayCharge = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 50));
			}
		}


		public override void OnThink()
		{
			base.OnThink();

			if (Combatant != null)
			{
				if (DelayCharge < DateTime.UtcNow)
				{

					if (!this.InRange(Combatant.Location, 3))
					{
						Charge();
					}
					

					DelayCharge = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 60));
				}


			}

		}

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{

				base.OnDamage(amount, from, willKill);

		}

		

		public KepushGuerrier(Serial serial)
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
