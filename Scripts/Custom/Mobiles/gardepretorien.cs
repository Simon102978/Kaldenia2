using Server.ContextMenus;
using Server.Items;
using Server.Network;
using Server.Regions;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class GardePretorien : BaseHire
    {
		public DateTime DelayCharge;
		public DateTime TuerSummoneur;

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

		public override bool CanDetectHidden => true;
     	public override bool BardImmune => true;
		public override bool Unprovokable => true;
		public override bool Uncalmable => true;

		public override TribeType Tribe => TribeType.Legion;


		[Constructable]
        public GardePretorien()
           //         : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
            : base()
        {
            SpeechHue = Utility.RandomDyedHue();
           
            Title = "Garde Pretorien";


            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                
            }

			Race = BaseRace.GetRace(Utility.Random(4));

			SetStr(386, 400);
            SetDex(251, 265);
            SetInt(161, 175);

            SetDamage(15, 25);

			SetHits(3000);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 55, 60);
            SetResistance(ResistanceType.Cold, 55, 60);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 50, 60);


            SetSkill(SkillName.Anatomy, 125.0);
            SetSkill(SkillName.Fencing, 46.0, 77.5);
            SetSkill(SkillName.Macing, 35.0, 57.5);
            SetSkill(SkillName.Poisoning, 60.0, 82.5);
            SetSkill(SkillName.MagicResist, 83.5, 92.5);
            SetSkill(SkillName.Swords, 150.0);
            SetSkill(SkillName.Tactics, 125.0);
            SetSkill(SkillName.Lumberjacking, 125.0);
	        SetSkill(SkillName.Wrestling, 150.0);

            Fame = 5000;
            Karma = -5000;

            AddItem(new Backpack());



			AddItem(new Sandals
			{
				Name = "Sandales",
			    LootType = LootType.Blessed,
				Layer = Layer.Shoes,
			});
			AddItem(new TogeKoraine
			{
				Name = "Toge Évasée",
				Hue = 1461,
				LootType = LootType.Blessed,
				Layer = Layer.MiddleTorso,
			});
			AddItem(new EpeeCourte
			{
				Name = "Épée Courte",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.FirstValid,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new Pavois
			{
				Name = "Pavois",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.TwoHanded,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new BrassardEmbellit
			{
				Name = "Brassard de demi-plaque",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.Arms,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new CasqueKorain
			{
				Name = "Casque Korain",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.Helm,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new KnightsPlateGorget
			{
				Name = "Gorget Plaque",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.Neck,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new GantsEmbellit
			{
				Name = "Gants de demi-plaque",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.Gloves,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});
			AddItem(new CapeOfCourage
			{
				Name = "Cape de la Garde pretorienne",
				Hue = 1461,
				LootType = LootType.Blessed,
				Layer = Layer.Cloak,
				Quality = ItemQuality.Legendary,
			});
			AddItem(new Kilt2
			{
				Name = "Kilt à Bandouillère",
				Hue = 2354,
				LootType = LootType.Blessed,
				Layer = Layer.Pants,
			});
			AddItem(new Earrings
			{
				Name = "Boucles d'oreilles pendantes",
				Hue = 1940,
				LootType = LootType.Blessed,
				Layer = Layer.Earrings,
				Quality = ItemQuality.Legendary,
				Resource = CraftResource.Nostalgium,
			});

            Utility.AssignRandomHair(this);
        }

        public GardePretorien(Serial serial)
            : base(serial)
        {
        }



		public bool BlockReflect { get; set; }

		public override void OnThink()
		{
			base.OnThink();

		    if (Rage)
            {
                Hits += 2;
            }


			if (Combatant != null)
			{

                    if (Combatant is BaseCreature bc)
                    {
                        AntiSummon();
                    }

					if (!this.InRange(Combatant.Location, 3) && this.InRange(Combatant.Location, 10))
					{
						Charge();
					}
			}
		}

		  public void ActiverRage()
		{

			LastRage = DateTime.Now;


			AdjustSpeeds();

		}

		
		public void DesactiverRage()
		{
			AdjustSpeeds();
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



		public void Charge()
		{
			if (DelayCharge < DateTime.UtcNow)
			{
					if (Combatant is CustomPlayerMobile cp)
					{

						Emote($"*Effectue une charge vers {cp.Name}*");

						cp.Damage(50);

						cp.Freeze(TimeSpan.FromSeconds(6));

						this.Location = cp.Location;
					}
				

				DelayCharge = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(15, 20));
			}
		}

		public void AntiSummon()
		{
			if (TuerSummoneur < DateTime.UtcNow)
			{
				if (Combatant is BaseCreature bc)
				{

						if (bc.ControlMaster is CustomPlayerMobile cp)
						{
							Combatant = cp;

							Emote($"*Effectue une charge vers {cp.Name}*");

							cp.Damage(50);

							cp.Freeze(TimeSpan.FromSeconds(6));

							this.Location = cp.Location;

						}
					}
				
				TuerSummoneur = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(15, 20));
			}
		}
	     public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{

		 	Hits += AOS.Scale(damage, 100);

			Effects.SendPacket(to.Location, to.Map, new ParticleEffect(EffectType.FixedFrom, to.Serial, Serial.Zero, 0x377A, to.Location, to.Location, 1, 15, false, false, 1926, 0, 0, 9502, 1, to.Serial, 16, 0));
            Effects.SendPacket(to.Location, to.Map, new ParticleEffect(EffectType.FixedFrom, to.Serial, Serial.Zero, 0x3728, to.Location, to.Location, 1, 12, false, false, 1963, 0, 0, 9042, 1, to.Serial, 16, 0));

			base.AlterMeleeDamageTo(to, ref damage);
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

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{

                if (this != null && from != null &&  Hits < 150)
                {
                    CheckRage(from);
                }





				base.OnDamage(amount, from, willKill);

		}

        public override void GenerateLoot()
        {
            // Pas un monstre bande de tordus..
		}

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

            int version = reader.ReadInt();

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
