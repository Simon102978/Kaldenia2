using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Mobiles
{
    [CorpseName("corps d'Omar")]
    public class Omar : BaseCreature
    {
        private bool m_InHere;

		public DateTime DelayCharge;

        public DateTime m_LastParole = DateTime.MinValue;

		public DateTime m_LastBlockParole = DateTime.MinValue;
    	public DateTime TuerSummoneur;

        public DateTime m_GlobalTimer;

		public DateTime m_NextSpawn;


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


		public static List<string> ParoleListe = new List<string>()
		{
			"Mon empire existera a jamais !",
			"Rejoignez le camp des gagnants !",
			"L'empire gagne toujours.",
		};

        [Constructable]
        public Omar()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Omar";
            Title = "Ex-Empereur Kershe";
            Body = 318;
            BaseSoundID = 0x165;
            Hue = 1158;

            SetStr(500);
            SetDex(100);
            SetInt(1000);

            SetHits(1500);
            SetMana(5000);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Fire, 20);
            SetDamageType(ResistanceType.Cold, 20);
            SetDamageType(ResistanceType.Poison, 20);
            SetDamageType(ResistanceType.Energy, 20);

            SetResistance(ResistanceType.Physical, 50, 60);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 40, 60);
            SetResistance(ResistanceType.Poison, 40, 60);
            SetResistance(ResistanceType.Energy, 40, 60);

            SetSkill(SkillName.Wrestling, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.MagicResist, 150.0);
            SetSkill(SkillName.Tracking, 150.0);
            SetSkill(SkillName.Magery, 100.0);
            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Meditation, 120.0);
            SetSkill(SkillName.Necromancy, 120.0);
            SetSkill(SkillName.SpiritSpeak, 120.0);

            Fame = 8000;
            Karma = -8000;

            SetWeaponAbility(WeaponAbility.CrushingBlow);
            SetWeaponAbility(WeaponAbility.WhirlwindAttack);

            AdjustSpeeds();
        }

        public override void AdjustSpeeds()
        {
            double activeSpeed = 0.0;
			double passiveSpeed = 0.0;

			SpeedInfo.GetCustomSpeeds(this, ref activeSpeed, ref passiveSpeed);
		
			ActiveSpeed = activeSpeed;
			PassiveSpeed = passiveSpeed;
			CurrentSpeed = passiveSpeed;
        }

        public override void OnActionCombat()
        { 
            AdjustSpeeds();
        }

        protected override void OnCreate()
        {          
            base.OnCreate();
            CheckWallControler();
        }

        public override void OnThink()
		{
			base.OnThink();
			Parole();

			if (m_GlobalTimer < DateTime.UtcNow && Warmode)
			{

					switch (Utility.Random(2))
					{
						case 0:
							Charge();
							break;
						case 1:
							Spawn();
							break;
						default:
							break;
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

						cp.Damage(35);

						cp.Freeze(TimeSpan.FromSeconds(3));

						this.Location = cp.Location;
					}
				

				DelayCharge = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 50));
			}
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

       	public void Parole()
		{
			if (m_LastParole < DateTime.Now && Combatant != null)
			{	
				Say(ParoleListe[Utility.Random(ParoleListe.Count)]);
			
				m_LastParole = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
			}
		}



        public Omar(Serial serial)
            : base(serial)
        {
        }

        public override bool CanFlee => false;

        public override bool IgnoreYoungProtection => true;
        public override bool BardImmune => false;
        public override bool Unprovokable => true;
        public override bool AreaPeaceImmune => true;
        public override Poison PoisonImmune => Poison.Lethal;
        public override int TreasureMapLevel => 3;

        public override TribeType Tribe => TribeType.Undead;

        public override void OnDeath(Container c)
        {
            if (m_Stone != null)
            {
                m_Stone.MobActif = false;
            }


            base.OnDeath(c);
        }

        public override bool TeleportsTo => true;


		public override int Bones => 4;
		public override BoneType BoneType => BoneType.Demoniaque;

		public override void GenerateLoot()
        {
           
			AddLoot(LootPack.FilthyRich, 5);
            AddLoot(LootPack.HighScrolls, Utility.RandomMinMax(1, 5));
             AddLoot(LootPack.LootItem<Items.Gold>(50,100));
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (from != null && from != this && !m_InHere)
            {
                m_InHere = true;
                AOS.Damage(from, this, Utility.RandomMinMax(8, 20), 100, 0, 0, 0, 0);

                MovingEffect(from, 0xECA, 10, 0, false, false, 0, 0);
                PlaySound(0x491);

                if (0.05 > Utility.RandomDouble())
                    Timer.DelayCall(TimeSpan.FromSeconds(1.0), new TimerStateCallback(CreateBones_Callback), from);

                m_InHere = false;
            }

           	Parole();
        }

        public virtual void CreateBones_Callback(object state)
        {
            Mobile from = (Mobile)state;
            Map map = from.Map;

            if (map == null)
                return;

            int count = Utility.RandomMinMax(1, 3);

            for (int i = 0; i < count; ++i)
            {
                int x = from.X + Utility.RandomMinMax(-1, 1);
                int y = from.Y + Utility.RandomMinMax(-1, 1);
                int z = from.Z;

                if (!map.CanFit(x, y, z, 16, false, true))
                {
                    z = map.GetAverageZ(x, y);

                    if (z == from.Z || !map.CanFit(x, y, z, 16, false, true))
                        continue;
                }

                UnholyBone bone = new UnholyBone
                {
                    Hue = 0,
                    Name = "Os Maudit",
                    ItemID = Utility.Random(0xECA, 9)
                };

                bone.MoveToWorld(new Point3D(x, y, z), map);
            }
        }

        public void Spawn()
		{
			if (m_NextSpawn < DateTime.UtcNow)
			{

				Emote("*Vous pouvez venir à mon aide, s'il vous plait ?*");

				int Nombre = 0;

				while(Nombre < 4)
				{
					int monstre = Utility.Random(5);

					switch(monstre)
					{
						case 0:
						{
							SpawnHelper( new Skeleton (	), Location);	
							break;
						}
						case 1:
						{
							SpawnHelper( new SkeletalKnight (	), Location);	
							break;
						}
						case 2:
						{
							SpawnHelper( new SkeletalMage (	), Location);
							break;
						}
						case 3:
						{
							SpawnHelper( new BoneMagi(	), Location);
							break;	
						}
						case 4:
						{
							SpawnHelper( new BoneKnight	(), Location);
							break;	
						}
						default:
						{	
							SpawnHelper( new Skeleton (	), Location);	
							break;
						}
					}
					Nombre++;			
				}

				m_NextSpawn = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(120, 200));

			}


		}


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
