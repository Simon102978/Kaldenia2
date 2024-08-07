using System;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un démon d'os")]
    public class Ali : BaseCreature
    {
        public DateTime m_LastParole = DateTime.MinValue;

		public DateTime m_LastBlockParole = DateTime.MinValue;

        public DateTime m_GlobalTimer;
		public DateTime m_Switch;
		public DateTime m_AreaExplosion;

       	public virtual int StrikingRange => 12;



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
			"Mort au traite !",
			"Je vais finir par vous vaincre !",
			"L'empire Kershe est éternelle"
		};
        
        [Constructable]
        public Ali()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Ali";
            Title = "Ex-Empereur Kershe";
            Body = 308;
            BaseSoundID = 0x48D;
            Hue = 1354;

            SetStr(500);
            SetDex(100);
            SetInt(1000);

            SetHits(1000);
            SetMana(5000);

            SetDamage(15, 20);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Cold, 50);

            SetResistance(ResistanceType.Physical, 75);
            SetResistance(ResistanceType.Fire, 60);
            SetResistance(ResistanceType.Cold, 90);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 60);

            SetSkill(SkillName.Wrestling, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.MagicResist, 150.0);
            SetSkill(SkillName.Tracking, 100.0);
            SetSkill(SkillName.Magery, 77.6, 87.5);
            SetSkill(SkillName.EvalInt, 77.6, 87.5);
            SetSkill(SkillName.Meditation, 100.0);

            Fame = 8000;
            Karma = 8000;
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

       	public override void OnDeath(Container c)
		{
            if (m_Stone != null)
            {
                m_Stone.MobActif = false;
            }

			base.OnDeath(c);
		

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
						Spawn();
						break;
					case 1:
						Switch();
						break;
					case 2:
						DoAreaExplosion();
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

				Emote("*Légion à l'attaque !*");

				int Nombre = 0;

				while(Nombre < 5)
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

				m_NextSpawn = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 120));

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






        public void DoAreaExplosion()
		{
			if (m_AreaExplosion < DateTime.UtcNow)
			{

				List<Mobile> toExplode = new List<Mobile>();

				IPooledEnumerable eable = GetMobilesInRange(8);

				foreach (Mobile mob in eable)
				{
					if (!CanBeHarmful(mob, false) || mob == this || (mob is BaseCreature && ((BaseCreature)mob).GetMaster() == this))
						continue;
					if (mob.Player)
						toExplode.Add(mob);
					if (mob is BaseCreature && (((BaseCreature)mob).Controlled || ((BaseCreature)mob).Summoned || ((BaseCreature)mob).Team != Team))
						toExplode.Add(mob);
				}
				eable.Free();

				foreach (Mobile mob in toExplode)
				{
					mob.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
					mob.PlaySound(0x307);

					int damage = Utility.RandomMinMax(50, 125);
					AOS.Damage(mob, this, damage, 0, 100, 0, 0, 0);
				}

				m_AreaExplosion = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(15, 20));
			}
		}


        public void Switch()
		{
			if (m_Switch < DateTime.UtcNow && Combatant != null && Combatant is Mobile m )
			{

				if (m.Female)
				{
					int dmg = 45;


					Combatant.FixedParticles(0x374A, 10, 30, 5013, 1153, 2, EffectLayer.Waist);

					AOS.Damage(Combatant, this, dmg, 100, 0, 0, 0, 0); // C'est un coup de vent, donc rien d'electrique...

					Say($"BEUURRRK ! Hors de ma vu ville créature!");

					KnockBack(this.Location, Combatant as Mobile, 5);

					ChangeOpponent();
				}
				else
				{
					int dmg = 45;

					Combatant.FixedParticles(0x374A, 10, 30, 5013, 1153, 2, EffectLayer.Waist);

					AOS.Damage(Combatant, this, dmg, 100, 0, 0, 0, 0); // C'est un coup de vent, donc rien d'electrique...

					Say($"Yummy ! Vien passé la soirée avec moi !");


					m.Paralyze(TimeSpan.FromSeconds(10));
				}
				

				m_Switch = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 60));
			}


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

        public void ChangeOpponent()
		{
			
				Mobile agro, best = null;
				double distance, random = Utility.RandomDouble();

				if (random < 0.75)
				{
					// find random target relatively close
					for (int i = 0; i < Aggressors.Count && best == null; i++)
					{
						agro = Validate(Aggressors[i].Attacker);

						if (agro == null)
							continue;

						distance = StrikingRange - GetDistanceToSqrt(agro);

						if (distance > 0 && distance < StrikingRange - 2 && InLOS(agro.Location))
						{
							distance /= StrikingRange;

							if (random < distance)
								best = agro;
						}
					}
				}
				else
				{
					int damage = 0;

					// find a player who dealt most damage
					for (int i = 0; i < DamageEntries.Count; i++)
					{
						agro = Validate(DamageEntries[i].Damager);

						if (agro == null)
							continue;

						distance = GetDistanceToSqrt(agro);

						if (distance < StrikingRange && DamageEntries[i].DamageGiven > damage && InLOS(agro.Location))
						{
							best = agro;
							damage = DamageEntries[i].DamageGiven;
						}
					}
				}

				if (best != null)
				{
					// teleport
					best.Location = GetSpawnPosition(Location, Map, 1);
					best.FixedParticles(0x376A, 9, 32, 0x13AF, EffectLayer.Waist);
					best.PlaySound(0x1FE);

					Timer.DelayCall(TimeSpan.FromSeconds(1), () =>
					{
						best.ApplyPoison(this, HitPoison);
						best.FixedParticles(0x374A, 10, 15, 5021, EffectLayer.Waist);
						best.PlaySound(0x474);
					});

			}


		}

        public Mobile Validate(Mobile m)
		{
			Mobile agro;

			if (m is BaseCreature)
				agro = ((BaseCreature)m).ControlMaster;
			else
				agro = m;

			if (!CanBeHarmful(agro, false) || !agro.Player /*|| Combatant == agro*/ )
				return null;

			return agro;
		}


       	public void Parole()
		{
			if (m_LastParole < DateTime.Now && Combatant != null)
			{	
				Say(ParoleListe[Utility.Random(ParoleListe.Count)]);
			
				m_LastParole = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
			}
		}



        public Ali(Serial serial)
            : base(serial)
        {
        }
	
		public override int Bones => Utility.RandomMinMax(3, 5);

		public override BoneType BoneType => BoneType.Demoniaque;
		public override bool Unprovokable => true;
        public override bool AreaPeaceImmune => true;
        public override Poison PoisonImmune => Poison.Lethal;
        public override int TreasureMapLevel => 3;


        public override TribeType Tribe => TribeType.Undead;
        public override void GenerateLoot()
        {

			AddLoot(LootPack.FilthyRich, 5);

			
			AddLoot(LootPack.Others, Utility.RandomMinMax(7, 15));
            AddLoot(LootPack.LootItem<Items.Gold>(50,100));

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
