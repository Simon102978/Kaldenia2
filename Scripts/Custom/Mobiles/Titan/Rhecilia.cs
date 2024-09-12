using Server.Engines.CannedEvil;
using Server.Items;
using Server.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName("Corp de Rhecilia")]
	public class Rhecilia : BasePeerless
	{
		public static List<string> ParoleRobe = new List<string>()
		{
			"Ne me forcer pas a mettre mon armure !",
			"Quoi encore?!",
			"*Soupire* Je crois que m'en va me tuer...",
			"Ou sont mes affreux !? Je suis toujours obligée de faire tout toute seule.",
			"Allez vous en ! Je dois trouver mon prince charmant!",
		};

		public static List<string> EnArmureParole = new List<string>()
		{
			"Allez les affreux, massacrer les !",
			"Meme pas mal !",
			"Amenez vous ! Je fais que ca de mes journées",
			"Mourrez tous bande d'abrutis.",
			"Vous me forcez vraiment à faire quelque chose ?",
		};

		public DateTime DelayCharge;
		public DateTime TuerSummoneur;

		public DateTime DelayWarCrie;
		public DateTime m_GlobalTimer;

		public DateTime m_NextSpawn;
		public DateTime m_NextDisarm;

	    private DateTime m_NextBomb;
        private int m_Thrown;


  		public bool BlockReflect { get; set; }

		public virtual int StrikingRange => 12;


		private bool m_EnArmure = false;

		public override bool AutoDispel => true;
		public override double AutoDispelChance => 1.0;
		public override bool BardImmune => true;
		public override bool Unprovokable => true;
		public override bool Uncalmable => true;
		public override Poison PoisonImmune => Poison.Lethal;
		public override TribeType Tribe => TribeType.Orc;

		[CommandProperty(AccessLevel.GameMaster)]
		public bool EnArmure
		{
			get => m_EnArmure;
			set
			{
				if (value && !m_EnArmure)
				{
					m_EnArmure = value;
					EnArmurer();
				}
				else if (!value && m_EnArmure)
				{
					m_EnArmure = value;
					EnRobe();
				}
				else
				{
					m_EnArmure = value;
				}
			}
		}
		public override bool CanBeParagon => false;

		public DateTime m_LastParole = DateTime.MinValue;


		[Constructable]
		public Rhecilia()
			: base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Name = "Rhecilia";
			Title = "Titan femelle en robe legere";
			Body = 76;
			BaseSoundID = 609;
			Hue = 2081;

			SetStr(600, 700);
			SetDex(76, 82);
			SetInt(76, 85);


			SetHits(5000);
			SetStam(507, 669);
			SetMana(1200, 1300);

			SetDamage(23, 27);

			//		SetDamage(2, 3);

			SetDamageType(ResistanceType.Physical, 80);
			SetDamageType(ResistanceType.Fire, 20);

			SetResistance(ResistanceType.Physical, 60, 70);
			SetResistance(ResistanceType.Fire, 60, 70);
			SetResistance(ResistanceType.Cold, 30, 40);
			SetResistance(ResistanceType.Poison, 55, 65);
			SetResistance(ResistanceType.Energy, 50, 60);

			SetSkill(SkillName.Wrestling, 120.0);
			SetSkill(SkillName.Tactics, 120.0);
			SetSkill(SkillName.MagicResist, 120.0);
			SetSkill(SkillName.Anatomy, 120.0);
			SetSkill(SkillName.Regeneration, 300.0);

			SetWeaponAbility(WeaponAbility.ParalyzingBlow);
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			list.Add(1050045, "Le bebe titan roux"); // ~1_PREFIX~~2_NAME~~3_SUFFIX~

			if (EnArmure)
			{
				list.Add(1050045, "<\th3><basefont color=#FF8000>EnArmure</basefont></h3>\t");
			}
		}


		public override void OnThink()
		{
			base.OnThink();
			Parole();

			if (m_GlobalTimer < DateTime.UtcNow && Warmode)
			{

				if (EnArmure)
				{
					Hits += 5;

					switch (Utility.Random(2))
					{
						case 0:
							WarCrie();		
							break;
						case 1:
							Spawn();
							break;
						default:
							break;
					}				


				}

				else if (Combatant != null) 
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

			
				m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
			}
		}


		public void Charge()
		{
			if (DelayCharge < DateTime.UtcNow)
			{
					if (Combatant is CustomPlayerMobile cp)
					{

						Emote($"*Effectue une charge vers {cp.Name}*");

						cp.Damage(15);

						cp.Freeze(TimeSpan.FromSeconds(3));

						this.Location = cp.Location;
					}
				

				DelayCharge = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 50));
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

							cp.Damage(15);

							cp.Freeze(TimeSpan.FromSeconds(3));

							this.Location = cp.Location;

						}
					}
				
				TuerSummoneur = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 50));
			}
		}


        public void WarCrie()
		{


			if (DelayWarCrie < DateTime.UtcNow)
			{
					int Range = 10;
					List<Mobile> targets = new List<Mobile>();

					IPooledEnumerable eable = this.GetMobilesInRange(Range);

					foreach (Mobile m in eable)
					{
						if (this != m && !(m is Brigand) &&  !(m is BrigandArcher) && !(m is BrigandAmbusher) && !(m is BrigandApprenti) && !(m is Courtisane) &&  !m.IsStaff())
						{
							if (Core.AOS && !InLOS(m))
								continue;

							targets.Add(m);
						}
					}

					eable.Free();

					Emote("*Lance un crie de guerre*");

					if (targets.Count > 0)
					{
                        Hits += targets.Count * 10;
			
						for (int i = 0; i < targets.Count; ++i)
						{
							Mobile m = targets[i];

							DoHarmful(m);

                            if (m is CustomPlayerMobile cp)
                            {
                                if (Combatant is BaseCreature)
                                {
                                    Combatant = cp;
                                }

                                DoDisarm(cp);
                                
                            }
                            else if (m is BaseCreature bc)
                            {
                                DoProvoke(bc);
                            }


						}
					}

				DelayWarCrie = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 60));
			
			}
		}

        public virtual void DoProvoke(BaseCreature creature)
		{
                    if (creature == null || !creature.Alive )
                    {
                       return; 
                    }
                    else if (creature.ControlMaster != null)
                    {
                        return;
                    }
                     
            
                    Mobile target = creature.ControlMaster;

                    if (creature.Unprovokable)
                    {
                       return;
                    }

                    else if (creature != target)
                    {
                        if ((CanBeHarmful(creature, true, false, true) && CanBeHarmful(target, true, false, true)))
                        {                                    
                                    creature.Provoke(this, target, true);                                          
                        }
                    }		
		}

        public virtual void DoDisarm(Mobile to)
        {
                 Item toDisarm = to.FindItemOnLayer(Layer.OneHanded);

                    if (toDisarm == null || !toDisarm.Movable)
                        toDisarm = to.FindItemOnLayer(Layer.TwoHanded);

                    Container pack = to.Backpack;

                    if (pack == null || (toDisarm != null && !toDisarm.Movable))
                    {
                        to.SendLocalizedMessage(1004001); // You cannot disarm your opponent.
                    }
                    else if (toDisarm == null || toDisarm is BaseShield)
                    {
                        to.SendLocalizedMessage(1060849); // Your target is already unarmed!
                    }
                
                SendLocalizedMessage(1060092); // You disarm their weapon!
                to.SendLocalizedMessage(1060093); // Your weapon has been disarmed!

                to.PlaySound(0x3B9);
                to.FixedParticles(0x37BE, 232, 25, 9948, EffectLayer.LeftHand);

                pack.DropItem(toDisarm);

                BuffInfo.AddBuff(to, new BuffInfo(BuffIcon.NoRearm, 1075637, TimeSpan.FromSeconds(5.0), to));

                BaseWeapon.BlockEquip(to, TimeSpan.FromSeconds(5.0));

                if (to is BaseCreature)
                {
                    Timer.DelayCall(TimeSpan.FromSeconds(5.0) + TimeSpan.FromSeconds(Utility.RandomMinMax(3, 10)), () =>
                    {
                        if (toDisarm != null && !toDisarm.Deleted && toDisarm.IsChildOf(to.Backpack))
                            to.EquipItem(toDisarm);
                    });
                }

                	try
					{
						Disarm.AddImmunity(to, TimeSpan.FromSeconds(10));
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Erreur lors de l'appel � Disarm.AddImmunity : {ex.Message}");
					}
        }



      public override void OnDamagedBySpell(Mobile attacker)
      {
        base.OnDamagedBySpell(attacker);

	    
        DoCounterMagic(attacker);
      }

	  public override void OnGotMeleeAttack(Mobile attacker)
      {
            base.OnGotMeleeAttack(attacker);

            DoMeleeCounter(attacker);
      }

	    private void DoMeleeCounter(Mobile attacker)
        {
			  if (DateTime.UtcNow >= m_NextDisarm)
            {
				Item toDisarm = attacker.FindItemOnLayer(Layer.OneHanded);
				if (toDisarm == null || !toDisarm.Movable)
					toDisarm = attacker.FindItemOnLayer(Layer.TwoHanded);

				Container pack = attacker.Backpack;
				if (pack == null || (toDisarm != null && !toDisarm.Movable))
				{
					attacker.SendLocalizedMessage(1004001); // You cannot disarm your opponent.
				}
				else if (toDisarm == null || toDisarm is BaseShield)
				{
					attacker.SendLocalizedMessage(1060849); // Your target is already unarmed!
				}
				else
				{
					SendLocalizedMessage(1060092); // You disarm their weapon!
					attacker.SendLocalizedMessage(1060093); // Your weapon has been disarmed!
					attacker.PlaySound(0x3B9);
					attacker.FixedParticles(0x37BE, 232, 25, 9948, EffectLayer.LeftHand);

					if (pack != null)
					{
						pack.DropItem(toDisarm);
					}

					BuffInfo.AddBuff(attacker, new BuffInfo(BuffIcon.NoRearm, 1075637, TimeSpan.FromSeconds(5.0), attacker));
					BaseWeapon.BlockEquip(attacker, TimeSpan.FromSeconds(5.0));

					if (attacker is BaseCreature)
					{
						Timer.DelayCall(TimeSpan.FromSeconds(5.0) + TimeSpan.FromSeconds(Utility.RandomMinMax(3, 10)), () =>
						{
							if (attacker != null && !attacker.Deleted && toDisarm != null && !toDisarm.Deleted && toDisarm.IsChildOf(attacker.Backpack))
								attacker.EquipItem(toDisarm);
						});
					}

					try
					{
						Disarm.AddImmunity(attacker, TimeSpan.FromSeconds(10));
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Erreur lors de l'appel � Disarm.AddImmunity : {ex.Message}");
					}
				}

               m_NextDisarm = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 50));
            }
		}

	    private void DoCounterMagic(Mobile attacker)
        {
            if (DateTime.UtcNow >= m_NextBomb)
            {
                ThrowBomb(attacker);

                m_Thrown++;

                if (0.75 >= Utility.RandomDouble() && (m_Thrown % 2) == 1) // 75% chance to quickly throw another bomb
                    m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds(3.0);
                else
                    m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds(5.0 + (10.0 * Utility.RandomDouble())); // 5-15 seconds
            }
        }


		public void ThrowBomb(Mobile m)
        {
            DoHarmful(m);

            MovingParticles(m, 0x1C19, 1, 0, false, true, 0, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0);

            new InternalTimer(m, this).Start();
        }


        private class InternalTimer : Timer
        {
            private readonly Mobile m_Mobile;
            private readonly Mobile m_From;
            public InternalTimer(Mobile m, Mobile from)
                : base(TimeSpan.FromSeconds(1.0))
            {
                m_Mobile = m;
                m_From = from;
                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                m_Mobile.PlaySound(0x11D);
                AOS.Damage(m_Mobile, m_From, Utility.RandomMinMax(30, 50), 0, 100, 0, 0, 0);
            }
        }

		public override void OnDeath(Container c)
		{
			base.OnDeath(c);
			Parole();

		}


		public void EnArmurer()
		{
			
			Hue = 2073;

			Mana = ManaMax;
			Stam = StamMax;
			Title = "Titan femelle en robe armure";

			SetResistance(ResistanceType.Physical, 80, 90);
			SetResistance(ResistanceType.Fire, 80, 90);
			SetResistance(ResistanceType.Cold, 80, 90);
			SetResistance(ResistanceType.Poison, 80, 90);
			SetResistance(ResistanceType.Energy, 80, 90);

			SetDamage(15, 20);

			Say("Vous m'y avez forcer !");

			SetWeaponAbility(WeaponAbility.MortalStrike);
			RemoveWeaponAbility(WeaponAbility.ParalyzingBlow);

		}

		public void EnRobe()
		{
			
			Hue = 2081;

			Mana = ManaMax;
			Stam = StamMax;
			Title = "Titan femelle en robe legere";

			SetResistance(ResistanceType.Physical, 60, 70);
			SetResistance(ResistanceType.Fire, 60, 70);
			SetResistance(ResistanceType.Cold, 30, 40);
			SetResistance(ResistanceType.Poison, 55, 65);
			SetResistance(ResistanceType.Energy, 50, 60);
			SetDamage(23, 27);

			Say("Ohh, plus besoin !");

			SetWeaponAbility(WeaponAbility.MortalStrike);
			RemoveWeaponAbility(WeaponAbility.ParalyzingBlow);

		}


		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			base.OnDamage(amount, from, willKill);

		


			if (!EnArmure && (Hits  < 3000))
			{
				EnArmure = true;
			}
			else if( EnArmure && (Hits  > 3000) )
			{
				EnArmure = false;
			}

			if (EnArmure)
			{
				
			}

			Parole();
		}

		public void Parole()
		{

			if (m_LastParole < DateTime.Now && Combatant != null)
			{
				if (EnArmure)
				{
					Say(EnArmureParole[Utility.Random(EnArmureParole.Count)]);
				}
				else
				{
					Say(ParoleRobe[Utility.Random(ParoleRobe.Count)]);


				}


				m_LastParole = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
			}





		}

		public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{

			Parole();

			Hits += AOS.Scale(damage, 50);

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

  		public void Spawn()
		{
			if (m_NextSpawn < DateTime.UtcNow)
			{

				Emote("*Allez les gars !*");

				int Nombre = 0;

				while(Nombre < 5)
				{
					int monstre = Utility.Random(5);

					switch(monstre)
					{
						case 0:
						{
							SpawnHelper( new OrcishMage (	), Location);	
							break;
						}
						case 1:
						{
							SpawnHelper( new OrcishLord (	), Location);	
							break;
						}
						case 2:
						{
							SpawnHelper( new OrcScout (	), Location);
							break;
						}
						case 3:
						{
							SpawnHelper( new OrcChopper(	), Location);
							break;	
						}
						case 4:
						{
							SpawnHelper( new OrcCaptain	(), Location);
							break;	
						}
						default:
						{	
							SpawnHelper( new OrcCaptain (	), Location);	
							break;
						}
					}
					Nombre++;			
				}

				m_NextSpawn = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 120));

			}


		}


		public override void Attack(IDamageable e)
		{
			Parole();
			base.Attack(e);
		}


		public Rhecilia(Serial serial)
			: base(serial)
		{
		}


		public override int Meat => 4;
		public override int TreasureMapLevel => 5;
		public override int Hides => 8;
		public override HideType HideType => HideType.Ancien;
		public override int Bones => 8;
		public override BoneType BoneType => BoneType.Ancien;





	




		public void SpawnHelper(BaseCreature helper, Point3D location)
		{
			if (helper == null)
				return;

			helper.Home = location;
			helper.RangeHome = 4;

			if (Combatant != null)
			{
				helper.Warmode = true;
				helper.Combatant = Combatant;
			}


			BaseCreature.Summon(helper, false, this, this.Location, -1, TimeSpan.FromMinutes(2));
			helper.MoveToWorld(location, Map);
		}















		public override void GenerateLoot()
        {
			AddLoot(LootPack.SuperBoss, 8);
            AddLoot(LootPack.MedScrolls);
            AddLoot(LootPack.PeculiarSeed1);
            AddLoot(LootPack.LootItem<Items.RoastPig>(10.0));
			AddLoot(LootPack.LootItem<Items.Gold>(5000,10000));
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);

			writer.Write(m_EnArmure);


        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

			switch (version)
			{
				case 1:
					{
						m_EnArmure = reader.ReadBool();
						break;
					}
				default:
					break;
			}

		}
    }










}
