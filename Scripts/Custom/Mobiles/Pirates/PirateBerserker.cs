using Server.Items;
using Server.Misc;
using Server.Network;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName("Le corps d'un Pirate")]
	public class PirateBerserker : PirateBase
	{
        public DateTime LastRage;

        public bool m_Rage = false;
	
		[CommandProperty(AccessLevel.GameMaster)]
		public bool Rage
		{
			get 
			{
				if (m_Rage && LastRage.AddMinutes(1) < DateTime.Now )
				{
					m_Rage = false;
					DesactiverRage();
				}


				return m_Rage;

			
			} 
			set
			{
				if (value && !m_Rage)
				{
					m_Rage = value;
					ActiverRage();
				}
				else if (!value && m_Rage )
				{
					m_Rage = value;
					DesactiverRage();
				}
				else
				{
					m_Rage = value;
				}
			}
		}

        public override bool BardImmune => Rage;
		public override bool Unprovokable => Rage;
		public override bool Uncalmable => Rage;

        [Constructable]
        public PirateBerserker()
            : this(0)
        {

           
        }

		[Constructable]
        public PirateBerserker(int PirateBoatId)
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4, PirateBoatId)
        {
           
            		
			SetStr(200, 250);
			SetDex(61, 85);
			SetInt(75, 100);
            SetHits(300,400);
			SetDamage(15, 25);

            SetResistance(ResistanceType.Physical, 50, 60);
			SetResistance(ResistanceType.Fire, 30, 50);
			SetResistance(ResistanceType.Cold, 50, 60);
			SetResistance(ResistanceType.Poison, 50, 60);
			SetResistance(ResistanceType.Energy, 50, 60);


			SetSkill(SkillName.ArmsLore, 80.0, 100.0);
            SetSkill(SkillName.Parry, 100.0, 120.0);
            SetSkill(SkillName.Macing, 100.0, 120.0);
            SetSkill(SkillName.Tactics, 100.0, 120.0);
            SetSkill(SkillName.Anatomy, 100.0, 120.0);         

            AddItem(new Marteau());
             
        }

           
        public void ActiverRage()
		{
            SetResistance(ResistanceType.Physical, 100);
            SetResistance(ResistanceType.Fire, 10, 20);
			SetResistance(ResistanceType.Cold, 10, 20);
			SetResistance(ResistanceType.Poison, 10, 20);
			SetResistance(ResistanceType.Energy, 10, 20);
			LastRage = DateTime.Now;


			AdjustSpeeds();
			Say("WHAAARRG !");

		}

		
		public void DesactiverRage()
		{
            SetResistance(ResistanceType.Physical, 50, 60);
            SetResistance(ResistanceType.Fire, 30, 50);
			SetResistance(ResistanceType.Cold, 50, 60);
			SetResistance(ResistanceType.Poison, 50, 60);
			SetResistance(ResistanceType.Energy, 50, 60);
			AdjustSpeeds();
			Emote("*Se calme*");

		}


		public override void AdjustSpeeds()
		{
			double activeSpeed = 0.0;
			double passiveSpeed = 0.0;


			if (Rage)
			{
				SpeedInfo.GetCustomSpeeds(this, ref activeSpeed, ref passiveSpeed);
			}
			else
			{
				SpeedInfo.GetSpeeds(this, ref activeSpeed, ref passiveSpeed);
			}

			ActiveSpeed = activeSpeed;
			PassiveSpeed = passiveSpeed;
			CurrentSpeed = passiveSpeed;
		}


		public void CheckRage(Mobile m)
		{
			if (LastRage.AddMinutes(5) < DateTime.Now && !Rage )
			{
				Combatant = m;
				Rage = true;
			}

		}




		public override void OnThink()
		{
			base.OnThink();

            if (Rage)
            {
                Hits += 2;
            }



		

		}

        public override void Parole()
		{

			//  Mis la parce que presque tout call ca.

			

			base.Parole();

		}



        public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{


			Parole();

			base.AlterMeleeDamageTo(to, ref damage);
		}

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{

                if (this != null && from != null &&  Hits < 150)
                {
                    CheckRage(from);
                }





				base.OnDamage(amount, from, willKill);

		}

	
		public override void ThrowingDetonate (Mobile m)
		{
 				
                if (m != null)
                {
                      DoHarmful(m);
                      m.Paralyze(TimeSpan.FromSeconds(5));
                    
                }
		}	

		public PirateBerserker(Serial serial)
            : base(serial)
        {
        }

		public override bool ClickTitle => false;


	public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(0); // version

			writer.Write(m_Rage);
			writer.Write(LastRage);


		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			var version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Rage = reader.ReadBool();
					LastRage = reader.ReadDateTime();
					break;

				}
				
			}


		}

    }
}
