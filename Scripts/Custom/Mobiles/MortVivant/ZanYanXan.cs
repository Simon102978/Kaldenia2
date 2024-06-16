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
	[CorpseName("Corp de ZanYanXan ")]
	public class ZanYanXan  : BasePeerless
	{
		public static List<string> ParoleListe = new List<string>()
		{
			"Plus de puissance, plus de pouvoir !",
			"Vous ne pouvez pas gagner, je serais toujours réanimé !",
			"Venez vivre avec moi dans l'éternelle passée !",
			"La nostalgie triomphera toujours !"
		};

		public DateTime m_GlobalTimer;
		public DateTime m_NextSpawn;

		public DateTime DelayAoe1;
	
		public DateTime DelayActivationManaShield;
		public DateTime LastActivationManaShield;
		public bool ManaShield = false;

		public override TribeType Tribe => TribeType.Undead;
  		
		public override bool BleedImmune => true;

		public virtual int StrikingRange => 12;
		public override bool AutoDispel => true;
		public override double AutoDispelChance => 1.0;
		public override bool BardImmune => true;
		public override bool Unprovokable => true;
		public override bool Uncalmable => true;
		public override Poison PoisonImmune => Poison.Lethal;

		public bool BlockReflect { get; set; }

		public override bool CanBeParagon => false;

		public DateTime m_LastParole = DateTime.MinValue;

		public DateTime m_LastBlockParole = DateTime.MinValue;

		[Constructable]
		public ZanYanXan ()
			: base(AIType.ArcherMageAI, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Name = "Zan'Yan'Xan";
			Title = "L'Éternelle Réanimée";
     		Body = 78;
            BaseSoundID = 412;
			Hue = 2936;

			

            SetStr(216, 305);
            SetDex(96, 115);
            SetInt(966, 1045);

            SetHits(560, 595);
			SetMana(1000, 2000);

            SetDamage(13, 17);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Cold, 40);
            SetDamageType(ResistanceType.Energy, 40);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 25, 30);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 25, 30);

            SetSkill(SkillName.EvalInt, 120.1, 130.0);
            SetSkill(SkillName.Magery, 120.1, 130.0);
            SetSkill(SkillName.Meditation, 100.1, 101.0);
            SetSkill(SkillName.Poisoning, 100.1, 101.0);
            SetSkill(SkillName.MagicResist, 175.2, 200.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 75.1, 100.0);
            SetSkill(SkillName.Necromancy, 120.1, 130.0);
            SetSkill(SkillName.SpiritSpeak, 120.1, 130.0);

            Fame = 23000;
            Karma = -23000;

		     AddItem(new GantZanYanXan());
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			list.Add(1050045, Title); // ~1_PREFIX~~2_NAME~~3_SUFFIX~
		}

		public override void OnThink()
		{
			base.OnThink();
			Parole();

			if (m_GlobalTimer < DateTime.UtcNow && Warmode)
			{

				
					switch (Utility.Random(3))
					{
						case 0:
							ActiveManaShield();
							break;
						case 1:
							 VagueIncendiaire();
							break;
						case 2:
							Spawn();
							break;
						default:
							break;
					}				
							
				m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
			}
		}
		public bool ManaShieldActive()
		{
			if(!ManaShield)
				return false;
			
			if (LastActivationManaShield > DateTime.UtcNow)
			{
			
				return true;
			}
			else if(LastActivationManaShield < DateTime.UtcNow)
			{
				DesactiveMana();
				return false;
			}
			
			return false;
		}

		public void ActiveManaShield()
		{
			
			if(DelayActivationManaShield < DateTime.UtcNow && !ManaShield)
			{ 
				Say("*Active son bouclier de mana*");
				Hue = 2933;
				ManaShield = true;
				LastActivationManaShield = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
			}
		}

		public void DesactiveMana()
		{
			Say("*Desactive son bouclier de mana*");
			Hue = 2936;	
			ManaShield = false;	
			DelayActivationManaShield = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));

		}

		public override void OnDeath(Container c)
		{
			base.OnDeath(c);
			Parole();

		}

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			base.OnDamage(amount, from, willKill);
		
			

			Say (amount);


			Parole();
		}

		public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            int dam = base.Damage(amount, from, informMount, checkDisrupt);

			

			if(ManaShieldActive())
			{
				int ManaAbsorbe = dam / 2;
			
				if(Mana > ManaAbsorbe)
				{
					Mana -= ManaAbsorbe;
					dam -=  ManaAbsorbe;
				}
				else
				{
					
					dam -= Mana;
					Mana = 0;
					DesactiveMana();
				}
			}
            return dam;
        }

		public void Parole()
		{
			if (m_LastParole < DateTime.Now && Combatant != null)
			{	
				Say(ParoleListe[Utility.Random(ParoleListe.Count)]);
			
				m_LastParole = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
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
		public ZanYanXan (Serial serial)
			: base(serial)
		{
		}

		public override int TreasureMapLevel => 3;
		public override int Bones => 8;
		public override BoneType BoneType => BoneType.Regular;



	#region FlameWave


		public void VagueIncendiaire()
		{

			if (Combatant != null && Combatant is Mobile Caster && DelayAoe1 < DateTime.UtcNow)
			{
				new FlameWaveTimer(this).Start();

				DelayAoe1 = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(90, 180));
			}

		}


		internal class FlameWaveTimer : Timer
		{
			private Mobile m_From;
			private Point3D m_StartingLocation;
			private Map m_Map;
			private int m_Count;
			private Point3D m_Point;

			public FlameWaveTimer(Mobile from)
				: base(TimeSpan.FromMilliseconds(300.0), TimeSpan.FromMilliseconds(300.0))
			{
				m_From = from;
				m_StartingLocation = from.Location;
				m_Map = from.Map;
				m_Count = 0;
				m_Point = new Point3D();
				SetupDamage(from);
			}

			protected override void OnTick()
			{
				double dist = 0.0;

				for (int i = -m_Count; i < m_Count + 1; i++)
				{
					for (int j = -m_Count; j < m_Count + 1; j++)
					{
						m_Point.X = m_StartingLocation.X + i;
						m_Point.Y = m_StartingLocation.Y + j;
						m_Point.Z = m_Map.GetAverageZ(m_Point.X, m_Point.Y);
						dist = GetDist(m_StartingLocation, m_Point);
						if (dist < ((double)m_Count + 0.1) && dist > ((double)m_Count - 3.1))
						{
							Effects.SendLocationParticles(EffectItem.Create(m_Point, m_Map, EffectItem.DefaultDuration), 0x3709, 10, 30,2936, 0, 5052,0);
						}
					}
				}

				m_Count += 3;

				if (m_Count > 15)
					Stop();
			}

			private void SetupDamage(Mobile from)
			{
				foreach (Mobile m in from.GetMobilesInRange(10))
				{
					if (m != from && !m.IsStaff())
					{
						Timer.DelayCall(TimeSpan.FromMilliseconds(300 * (GetDist(m_StartingLocation, m.Location) / 3)), new TimerStateCallback(Hurt), m);
					}
				}
			}

			public void Hurt(object o)
			{
				Mobile m = o as Mobile;

				if (m_From == null || m == null || m.Deleted)
					return;


				int dmg = 40; // 5 + 20



			
				if(m.Mana > dmg)
				{
					m.Mana -=  dmg;
				}
				else
				{
					dmg += dmg - m.Mana;
					m.Mana = 0;
				}


					// It looked like it delt 67 damage, presuming 70% fire res thats about 223 damage delt before resistance.
				AOS.Damage(m, m_From, dmg, 0, 100, 0, 0, 0);

				


			}
			private double GetDist(Point3D start, Point3D end)
			{
				int xdiff = start.X - end.X;
				int ydiff = start.Y - end.Y;
				return Math.Sqrt((xdiff * xdiff) + (ydiff * ydiff));
			}
		}

		#endregion












		public void Spawn()
		{
			if (m_NextSpawn < DateTime.UtcNow)
			{

				Emote("*Venez à moi, supporteur !*");

				int Nombre = 0;

				while(Mana >= 25 && Nombre < 4)
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
					Mana -= 25;				
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


		public override void GenerateLoot()
        {
			AddLoot(LootPack.SuperBoss, 8);
            AddLoot(LootPack.MedScrolls);
            AddLoot(LootPack.PeculiarSeed1);
            AddLoot(LootPack.LootItem<Items.RoastPig>(10.0));
			AddLoot(LootPack.LootItem<Items.Gold>(15000,20000));
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

			switch (version)
			{
				
				default:
					break;
			}

		}
    }










}
