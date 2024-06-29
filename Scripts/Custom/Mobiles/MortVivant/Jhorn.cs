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
	[CorpseName("Corp de Jhorn")]
	public class Jhorn : BasePeerless
	{
		public static List<string> ParoleListe = new List<string>()
		{
			""
		};

		public DateTime m_GlobalTimer;


		public DateTime m_Teleport;
		public DateTime DelayCoupVentre;

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
		public Jhorn()
			: base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Name = "Jhorn";
			Title = "Briseur De Vent";
			Body = 56;
            BaseSoundID = 0x48D;
			Hue = 1942;

			SetStr(208, 319);
            SetInt(45, 91);

            SetHits(200, 300);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Physical, 100);
         
            SetResistance(ResistanceType.Physical, 55, 62);
            SetResistance(ResistanceType.Fire, 40, 48);
            SetResistance(ResistanceType.Cold, 71, 80);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 50, 60);

            SetSkill(SkillName.Archery, 75.3, 90.5);
            SetSkill(SkillName.Wrestling, 75.3, 90.5);
            SetSkill(SkillName.Tactics, 75.5, 90.8);
            SetSkill(SkillName.MagicResist, 102.8, 117.9);
            SetSkill(SkillName.Anatomy, 75.5, 90.2);

            Fame = 18000;
            Karma = -18000;

			SetWeaponAbility(WeaponAbility.DoubleShot);
			AddItem(new Bow());

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
							CoupMultiple();
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


		public void CoupMultiple()
		{

			if (DelayCoupVentre < DateTime.UtcNow && Combatant != null && Combatant is Mobile m ) 
			{
				Emote("*Tire de multiple fleche.");


         	   	Weapon.OnSwing(this, Combatant);
				Weapon.OnSwing(this, Combatant);	
				Weapon.OnSwing(this, Combatant);
		
				DelayCoupVentre = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 60));
			}
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

	//		if(Utility.Random(0,2) >= 1)
			{
				KnockBack(this.Location, to, 2);
			}

			


			base.AlterMeleeDamageTo(to, ref damage);
		}

		public void KnockBack(Point3D destination, Mobile target, int Distance)
		{

			if (target.Alive)
			{

				int Distance2 = Distance;
				int Neg = 1;

				if (Distance < 0)
				{
					Distance2 = -Distance;   // Sert a faire en sorte, que si c'est négative, ca va avancer.
					Neg = -1;
				}

				for (int i = 0; i < Distance2; i++)  // Le script valide toute les tiles jusqu'a la distance maximum. Si une d'entre elle est bloquer, il revient a la précédente (ou player location du départ) et stun la target.
				{
					Point3D point = KnockBackCalculation(destination, target, i * Neg);

					if (!target.Map.CanFit(point, 16, false, false) && i != Distance2)
					{
						target.Paralyze(TimeSpan.FromSeconds((Distance2 - i + 1)));
						break;
					}
					else
					{
						target.MoveToWorld(point, target.Map);
					}
				}
			}
		}
		public Point3D KnockBackCalculation(Point3D Loc, Mobile target, int Distance)
		{
			return KnockBackCalculation(Loc, new Point3D(target.Location), Distance);
		}

		public Point3D KnockBackCalculation(Point3D Loc, Point3D point, int Distance)
		{

			Direction d = Utility.GetDirection(point, Loc);

			switch (d)
			{
				case (Direction)0x0: case (Direction)0x80: point.Y += Distance; break; //North
				case (Direction)0x1: case (Direction)0x81: { point.X -= Distance; point.Y += Distance; break; } //Right
				case (Direction)0x2: case (Direction)0x82: point.X -= Distance; break; //East
				case (Direction)0x3: case (Direction)0x83: { point.X -= Distance; point.Y -= Distance; break; } //Down
				case (Direction)0x4: case (Direction)0x84: point.Y -= Distance; break; //South
				case (Direction)0x5: case (Direction)0x85: { point.X += Distance; point.Y -= Distance; break; } //Left
				case (Direction)0x6: case (Direction)0x86: point.X += Distance; break; //West
				case (Direction)0x7: case (Direction)0x87: { point.X += Distance; point.Y += Distance; break; } //Up
				default: { break; }
			}
			return point;
		}


		public override void Attack(IDamageable e)
		{
			Parole();
			base.Attack(e);
		}
		public Jhorn(Serial serial)
			: base(serial)
		{
		}

		public override int TreasureMapLevel => 2;
		public override int Bones => 8;
		public override BoneType BoneType => BoneType.Regular;


		public void CoupVentre()
		{

			if (DelayCoupVentre < DateTime.UtcNow && Combatant != null && Combatant is Mobile m && m.InRange(this,3)) 
			{
				int dmg = 45;


				DelayCoupVentre = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 60));
			}
		}

	

		public override void GenerateLoot()
        {
			AddLoot(LootPack.Rich, 5);
            AddLoot(LootPack.MedScrolls);
            AddLoot(LootPack.PeculiarSeed1);
			AddLoot(LootPack.LootItem<Items.Gold>(250, 400));

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
