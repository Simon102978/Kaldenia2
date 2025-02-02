using Server.Items;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class PirateSecond : PirateBase
    {

		public DateTime TuerSummoneur;

        public DateTime DelayWarCrie;

        public DateTime m_NextSpawn;

        public DateTime m_GlobalTimer;

		public override Poison HitPoison => Poison.Lethal;

	    public DateTime LastSprint;

		public bool m_Sprint = false;
		public override bool BardImmune => true;
		public override bool Unprovokable => true;
		public override bool Uncalmable => true;

		public override bool IsScaryToPets => true;

	
		[CommandProperty(AccessLevel.GameMaster)]
		public bool Sprint
		{
			get
			{
				if (m_Sprint && LastSprint.AddSeconds(30) < DateTime.Now )
				{
					m_Sprint = false;
					DesactiverSprint();
				}


				return m_Sprint;

			
			} 
			set
			{
				if (value && !m_Sprint)
				{
					m_Sprint = value;
					ActiverSprint();
				}
				else if (!value && m_Sprint )
				{
					m_Sprint = value;
					DesactiverSprint();
				}
				else
				{
					m_Sprint = value;
				}
			}
		}

		private WallControlerStone m_Stone;

        [CommandProperty(AccessLevel.GameMaster)]
        public WallControlerStone Stone
        {
            get
            {
                return m_Stone;
            }
            set
            {
                m_Stone = value;
                
               
            }
        }


   

 /*       public static List<string> ParoleListe = new List<string>()
		{
   
		};*/
		[Constructable]
		 public PirateSecond()
		     : this(0)
        {

           
        }

		[Constructable]
        public PirateSecond(int PirateBoatId)
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4,PirateBoatId)
        {

            Title = "Second de "+ GetPirateBoat().ToStringWithPronom();
   
			SetStr(275, 325);
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

            SetSkill(SkillName.Anatomy, 100.0);
            SetSkill(SkillName.MagicResist, 83.5, 92.5);
            SetSkill(SkillName.Fencing, 120);
            SetSkill(SkillName.Tactics, 100.0);


            Fame = 3000;
            Karma = -3000;


            AddItem(new Kryss());
        }

	  protected override void OnCreate()
        {          
            base.OnCreate();
            CheckWallControler();
        }

		 public void CheckWallControler()
        {

           IPooledEnumerable eable = Map.GetItemsInRange(this.Location, 10);

            foreach (Item item in eable)
            {
                if (item is WallControlerStone wc)
                {
                    m_Stone = wc;
                    break;
                }
            }
            eable.Free();

            if (m_Stone != null)
            {
                m_Stone.MobActif = true;
            }

        }


        public override void OnDeath(Container c)
        {
            if (m_Stone != null)
            {
                m_Stone.MobActif = false;
            }


            base.OnDeath(c);
        }



        public PirateSecond(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer => true;

		public override TribeType Tribe => TribeType.Pirate;
		public bool BlockReflect { get; set; }


		public override void OnThink()
		{
			base.OnThink();
            Parole();

			if (Sprint)
			{
				// checker la fin du sprint.
			}

            if (m_GlobalTimer < DateTime.UtcNow && Warmode)
			{

					switch (Utility.Random(3))
					{
						case 0:
							{
                            	if (Combatant != null)
                                {

                                        if (Combatant is BaseCreature bc && bc.GetMaster() != null)
                                        {
                                            AntiSummon();
                                        }
										else
										{
											CheckSprint();
										}

                                }
                                break;
                            }
							
						case 1:
							Spawn();
							break;
						case 2:
							WarCrie();
							break;
						default:
							break;
					}	

					m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));			
				}

		}

		public void CheckSprint()
		{
			if (LastSprint.AddMinutes(2) < DateTime.Now && !Sprint)
			{

				Sprint = true;
			}

		}


		public void ActiverSprint()
		{
			LastSprint = DateTime.Now;
			AdjustSpeeds();
			Emote("*Se met à courrir.*");

		}

		
		public void DesactiverSprint()
		{
			AdjustSpeeds();
			Emote("*Se calme*");

		}


		public override void AdjustSpeeds()
		{
			double activeSpeed = 0.0;
			double passiveSpeed = 0.0;


			if (Sprint)
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

		public override void ThrowingDetonate (Mobile m)
		{
 				
                if (m != null)
                {
                      DoHarmful(m);
                      m.Paralyze(TimeSpan.FromSeconds(6));
                    
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
						if (this != m && !(m is PirateBase) &&  !m.IsStaff() && !(m is BaseCreature bc && bc.GetMaster() == null))
						{
							if (Core.AOS && !InLOS(m))
								continue;

							targets.Add(m);
						}
					}

					eable.Free();

					Emote("*Lance un crie de guerre* WAAARRGGG !");

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
			else
			{
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
						if (to != null && !to.Deleted && toDisarm != null && !toDisarm.Deleted && toDisarm.IsChildOf(to.Backpack))
							to.EquipItem(toDisarm);
					});
				}

				Disarm.AddImmunity(to, TimeSpan.FromSeconds(10));
			}
		}



		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			base.OnDamage(amount, from, willKill);
		
		

			Parole();
		}

        public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{

			Parole();

			base.AlterMeleeDamageTo(to, ref damage);
		}

       	public override void Attack(IDamageable e)
		{
			Parole();
			base.Attack(e);
		}

	public void Spawn()
	{
		if (m_NextSpawn < DateTime.UtcNow)
		{
			Emote("*Allez les gars !*");
			int nombre = 0;
			while (nombre < 2)
			{
				int monstre = Utility.Random(6); // Changé à 6 pour inclure tous les cas
				BaseCreature creature = null;

				switch (monstre)
				{
					case 0:
						creature = new PirateAmbusher(PirateBoatID);
						break;
					case 1:
						creature = new PirateArbaletrier(PirateBoatID);
						break;
					case 2:
						creature = new PirateBerserker(PirateBoatID);
						break;
					case 3:
						creature = new PirateBarde(PirateBoatID);
						break;
					case 4:
						creature = new PirateChaman(PirateBoatID);
						break;
					default:
						creature = new PirateDefender(PirateBoatID);
						break;
				}

				if (creature != null && !this.Deleted && this.Map != null)
				{
					SpawnHelper(creature, this.Location);
					nombre++;
				}
			}

			m_NextSpawn = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 120));
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

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich,4);
            AddLoot(LootPack.Meager);
			AddLoot(LootPack.Others, Utility.RandomMinMax(5, 6));
			AddLoot(LootPack.LootItem<Items.Gold>(1000, 2000));

			base.GenerateLoot();

		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version 
			writer.Write(m_Stone);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

			m_Stone = (WallControlerStone)reader.ReadItem();
        }
    }
}
