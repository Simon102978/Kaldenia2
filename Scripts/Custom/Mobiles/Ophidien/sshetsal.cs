using System;
using System.Collections.Generic;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("le corps de Sshetsal")]
    public class Sshetsal : BaseCreature
    {
        public DateTime m_GlobalTimer;

        public DateTime DelayFlash;

        public DateTime DelayRegardParalysant;
		public DateTime LastFreeze;
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

        [Constructable]
        public Sshetsal()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 3, 0.2, 0.4)
        {
            Name = "Sshetsal";
            Title = "Boureau";
            Body = 86;
            BaseSoundID = 634;
            Hue = 1159;

            SetStr(417, 595);
            SetDex(300, 400);
            SetInt(46, 70);


            SetHits(900, 1000);
            SetMana(0);

            SetDamage(15, 20);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 50, 75);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 90, 100);
            SetResistance(ResistanceType.Poison, 90, 100);
            SetResistance(ResistanceType.Energy, 35, 45);

            SetSkill(SkillName.Wrestling, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.MagicResist, 150.0);
            SetSkill(SkillName.Tracking, 150.0);
            SetSkill(SkillName.Regeneration, 150.0);

            Fame = 5000;
            Karma = -5000;
            AddItem(new BardicheOphidian());
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

        public Sshetsal(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(1, 2);

		public override Poison PoisonImmune => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override int TreasureMapLevel => 3;
        public override bool Unprovokable => true;

        public override bool CanBeParagon => false;

        public override TribeType Tribe => TribeType.Ophidian;

        public override bool CanFlee => false;

		public override int Hides => Utility.RandomMinMax(1, 3);
		public override int Bones => Utility.RandomMinMax(1, 3);
		public override HideType HideType => HideType.Ophidien;

		public override void GenerateLootParagon()
		{
			AddLoot(LootPack.LootItem<SangEnvoutePoison>(), Utility.RandomMinMax(2, 4));

		}
    

      	public override void OnThink()
		{
			base.OnThink();


			if (Sprint && LastSprint.AddSeconds(20) > DateTime.Now )
			{
				Sprint = false;
			}


			if (m_GlobalTimer < DateTime.UtcNow && Warmode)
			{

				
					switch (Utility.Random(3))
					{
						case 0:
							CheckSprint();
							break;
						case 1:
		                   RegardParalysant();
							break;
						case 2:
							Flash();
							break;
						default:
							break;
					}				
							
				m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
			}
		}

        public void Flash()
        {
            if (DelayFlash < DateTime.UtcNow)
            {

                Emote($"*Sa vitesse augmente rapidement.*");

                FlashTimer timer = new FlashTimer(this);
                

                DelayFlash = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 120));
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




        private class FlashTimer : Timer
        {
            private readonly Sshetsal m_From;
            private int m_Count;
       

            //   public BleedTimer(Mobile from, Mobile m, bool blooddrinker)
            //     : base(TimeSpan.FromSeconds(2.0), TimeSpan.FromSeconds(2.0))


            public FlashTimer(Sshetsal from) : base(TimeSpan.FromSeconds(5.0), TimeSpan.FromSeconds(10.0))
            {
                m_From = from;        
                Priority = TimerPriority.TwoFiftyMS;
                Start();
            }

            protected override void OnTick()
            {

                if (m_Count++ > 5)
                {
                    this.Stop();
                    return;
                }

                List<Mobile> targets = new List<Mobile>();

                IPooledEnumerable eable = m_From.GetMobilesInRange(16);

                foreach (Mobile m in eable)
                {
                    if (m_From != m && !m.IsStaff())
                    {
                        if (Core.AOS && !m_From.InLOS(m))
                            continue;

                        targets.Add(m);
                    }
                }

                eable.Free();

                bool findTarget = false;

                if (targets.Count > 0)
                {
                    for (int i = 0; i < 25; ++i)
                    {

                        Mobile cible = targets[Utility.Random(targets.Count)];


                        if (cible != null && cible.Map != null && cible.Map != Map.Internal && cible.AccessLevel == AccessLevel.Player && !cible.Hidden && cible.Alive)
                        {
                            

                            if (cible is CustomPlayerMobile sp)
                            {

                                if (i > 20 || m_From.Combatant != cible)
                                {
                                    m_From.Combatant = cible;
                                    m_From.MoveToWorld(m_From.Combatant.Location, m_From.Combatant.Map);
                                    findTarget = true;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else if (cible is BaseCreature sd)
                            {

                                if (sd.Controlled && sd.ControlMaster is CustomPlayerMobile sp1)
                                {
                                    m_From.Combatant = sp1;
                                    m_From.MoveToWorld(m_From.Combatant.Location, m_From.Combatant.Map);
                                    findTarget = true;
                                    break;

                                }
                                else
                                {
                                   
                                        continue;
                                    
                                }
                            }
                            else if (i > 20 )
                            {
                                if (m_From.Combatant != null)
                                {
                                    m_From.MoveToWorld(m_From.Combatant.Location, m_From.Combatant.Map);
                                    findTarget = true;
                                    break;
                                }
                                m_From.Combatant = cible;

                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }


      	public void RegardParalysant()
		{


			if (DelayRegardParalysant < DateTime.UtcNow && Combatant != null && Combatant is Mobile m && m.InRange(this,6))
			{
				Emote($"*Son regard transforme partiellement en pierre {Combatant.Name}*");

				m.Freeze(TimeSpan.FromSeconds((Utility.RandomMinMax(3, 7))));


				DelayRegardParalysant = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 50));

			}
		}
	



		
		public override BoneType BoneType => BoneType.Ophidien;

		public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 4);
            AddLoot(LootPack.LootItem<LesserPoisonPotion>());
			AddLoot(LootPack.Others, Utility.RandomMinMax(2, 4));
            AddLoot(LootPack.LootItem<Items.Gold>(400,900));

		}

		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
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
