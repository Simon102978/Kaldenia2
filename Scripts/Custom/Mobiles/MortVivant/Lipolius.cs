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
	[CorpseName("Corp de Lipolius")]
	public class Lipolius : BasePeerless
	{
		public static List<string> ParoleListe = new List<string>()
		{
			""
		};

		public DateTime m_GlobalTimer;
		public DateTime m_NextSpawn;


		public DateTime m_Teleport;


		public virtual int StrikingRange => 12;
		public override bool AutoDispel => true;
		public override double AutoDispelChance => 1.0;
		public override bool BardImmune => true;
		public override bool Unprovokable => true;
		public override bool Uncalmable => true;
		public override Poison PoisonImmune => Poison.Deadly;

	    public override TribeType Tribe => TribeType.Undead;
	
		public override bool BleedImmune => true;

		public bool BlockReflect { get; set; }

		public override bool CanBeParagon => false;

		public DateTime m_LastParole = DateTime.MinValue;

		public DateTime m_LastBlockParole = DateTime.MinValue;

		[Constructable]
		public Lipolius()
			: base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Name = "Lipolius";
			Title = "Le Belliqueux";
            Body = 148;
            BaseSoundID = 451;
			Hue = 1667;

			SetStr(208, 319);
            SetDex(126, 145);
            SetInt(276, 305);

            SetHits(200, 300);

			SetDamage(15, 20);

        	SetDamageType(ResistanceType.Physical, 10);
            SetDamageType(ResistanceType.Cold, 40);
            SetDamageType(ResistanceType.Energy, 50);

            SetResistance(ResistanceType.Physical, 40, 60);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 55, 65);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.Necromancy, 89, 99.1);
            SetSkill(SkillName.SpiritSpeak, 90.0, 99.0);

            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Magery, 70.1, 80.0);
            SetSkill(SkillName.Meditation, 85.1, 95.0);
            SetSkill(SkillName.MagicResist, 80.1, 100.0);
            SetSkill(SkillName.Tactics, 70.1, 90.0);

            Fame = 8000;
            Karma = -8000;

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

				
					switch (Utility.Random(2))
					{
						case 0:
							Spawn();
							break;
						case 1:
							Teleport();
							break;
						default:
							break;
					}				
							
				m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
			}
		}


		public void Spawn()
		{
			if (m_NextSpawn < DateTime.UtcNow)
			{

				

				for (int i = 0; i < Utility.Random(1, 3); i++)
				{

						SpawnHelper( new Skeleton(), Location);					
				}

				m_NextSpawn = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 90));

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


		private void Teleport()
		{

			if (m_Teleport < DateTime.UtcNow && Combatant != null && Combatant is Mobile m)
			{
					int chance = 0;

					IPoint3D p = this.Location;
					
					while(chance < 30  &&  m.GetDistanceToSqrt(p) < 4 && !Map.LineOfSight(p, m.Location) && !Map.CanSpawnMobile(p.X, p.Y, p.Z))
					{
						int newX = this.X + Utility.Random(-10, 10);
						int newY = this.Y + Utility.Random(-10, 10);

						IPoint3D po  = new Point3D(newX,newY,0);

						SpellHelper.GetSurfaceTop(ref po);
						
						p = po;
						chance++;
					}

					if(GetDistanceToSqrt(p) > 3)
					{
						MoveToWorld(new Point3D(p), Map);
						FixedParticles(0x376A, 9, 32, 0x13AF, EffectLayer.Waist);
						PlaySound(0x1FE);
					}
		
				m_Teleport = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 20));
			}
			
		}


		public override void OnDeath(Container c)
		{
			base.OnDeath(c);
			Parole();

		}

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			base.OnDamage(amount, from, willKill);
			
			Parole();
		}

		public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            int dam = base.Damage(amount, from, informMount, checkDisrupt);

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

 			int ManaLeech = AOS.Scale(damage, 50);

			if(to.Mana > ManaLeech)
			{
				to.Mana -= ManaLeech;
				Mana += ManaLeech;
			}
			else
			{
				Mana += to.Mana;
				to.Mana = 0 ;

			}


		    to.FixedParticles(0x374A, 1, 15, 5054, 23, 7, EffectLayer.Head);
            to.PlaySound(0x1F9);

        	this.FixedParticles(0x0000, 10, 5, 2054, EffectLayer.Head);

			base.AlterMeleeDamageTo(to, ref damage);
		}

		public override void Attack(IDamageable e)
		{
			Parole();
			base.Attack(e);
		}


		public Lipolius(Serial serial)
			: base(serial)
		{
		}

		public override int TreasureMapLevel => 2;

		public override int Bones => 8;
		public override BoneType BoneType => BoneType.Regular;




		public override void GenerateLoot()
        {

            AddLoot(LootPack.MedScrolls, 1);
            AddLoot(LootPack.NecroRegs, 100, 200);
			AddLoot(LootPack.BodyPartsAndBones, Utility.RandomMinMax(3, 5));
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));
	  		AddLoot(LootPack.Rich, 2);
			AddLoot(LootPack.LootItem<Items.Gold>(50, 100));
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
