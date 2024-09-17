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

		public override Poison HitPoison => Poison.Parasitic;

			    public DateTime LastSprint;

		public bool m_Sprint = false;
	
		[CommandProperty(AccessLevel.GameMaster)]
		public bool Sprint
		{
			get => m_Sprint;
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

			SetHits(600);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 55, 60);
            SetResistance(ResistanceType.Cold, 55, 60);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 50, 60);

            SetSkill(SkillName.Anatomy, 100.0);
            SetSkill(SkillName.MagicResist, 83.5, 92.5);
            SetSkill(SkillName.Swords, 120);
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

		public override TribeType Tribe => TribeType.Brigand;
		public bool BlockReflect { get; set; }


		public override void OnThink()
		{
			base.OnThink();
            Parole();

			if (Sprint && LastSprint.AddSeconds(20) > DateTime.Now )
			{
				Sprint = false;
			}

            if (m_GlobalTimer < DateTime.UtcNow && Warmode)
			{

					switch (Utility.Random(3))
					{
						case 0:
							{
                            	if (Combatant != null)
                                {

                                        if (Combatant is BaseCreature bc)
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
			AdjustSpeeds();
			Emote("*Se met à courrir.*");

		}

		
		public void DesactiverSprint()
		{
			AdjustSpeeds();
			Emote("*Se calme*");

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
						if (this != m && !(m is PirateBase) &&  !m.IsStaff())
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
				int Nombre = 0;
				while (Nombre < 2)
				{
					int monstre = Utility.Random(5);
					BaseCreature creature = null;
					try
					{
						switch (monstre)

					{
						case 0:
						{
							SpawnHelper( new PirateAmbusher (PirateBoatID	), Location);	
							break;
						}
						case 1:
						{
							SpawnHelper( new PirateArbaletrier (PirateBoatID	), Location);	
							break;
						}
						case 2:
						{
							SpawnHelper( new PirateBarde (PirateBoatID	), Location);
							break;
						}
						case 3:
						{
							SpawnHelper( new PirateBarde(PirateBoatID	), Location);
							break;	
						}
						case 4:
						{
							SpawnHelper( new PirateChaman	(PirateBoatID), Location);
							break;	
						}
						default:
						{	
							SpawnHelper( new PirateDefender (PirateBoatID	), Location);	
							break;
						}
					}
						if (creature != null && !this.Deleted && this.Map != null)
						{
							SpawnHelper(creature, this.Location);
							Nombre++;
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Erreur lors de la création de la créature: {ex.Message}");
						if (creature != null)
							creature.Delete();
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
			AddLoot(LootPack.LootItem<Items.Gold>(500, 2000));

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
