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
	[CorpseName("Corp de Rugal")]
	public class Rugal : BaseCreature, IAuraCreature
	{

		public override bool CanStealth => true;
		public static List<string> ParoleListe = new List<string>()
		{
			"Ecoutez moi ! Je suis l'autorité !",
			"J'ai un rapport à remplir, je reviens plutard !",
			"Quoi? Il faut que je me batte !",
			"Regarde par la ! Pouf ! ",
			"Vous allez m'écouter à la fin ?!?",
		};

		public DateTime m_GlobalTimer;

		public DateTime DelayAoe1;

		public virtual int StrikingRange => 12;
		public override bool AutoDispel => true;
		public override double AutoDispelChance => 1.0;
		public override bool BardImmune => true;
		public override bool Unprovokable => true;
		public override bool Uncalmable => true;
		public override Poison PoisonImmune => Poison.Lethal;
	    public override Poison HitPoison => Poison.DarkGlow;

		public override bool CanBeParagon => false;

		public DateTime m_LastParole = DateTime.MinValue;

		public DateTime m_LastBlockParole = DateTime.MinValue;

		[Constructable]
		public Rugal()
			: base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Name = "Rugal";
			Title = "Titan Furtif";
			Body = 76;
			BaseSoundID = 609;
			Hue = 2500;

			SetStr(600, 700);
			SetDex(606, 700);
			SetInt(760, 850);


			SetHits(6000);
			SetStam(507, 669);
			SetMana(5000, 6000);

			SetDamage(25, 30);

			SetDamageType(ResistanceType.Physical, 25);
			SetDamageType(ResistanceType.Poison, 25);
			SetDamageType(ResistanceType.Cold, 50);

			SetResistance(ResistanceType.Physical, 75, 85);
			SetResistance(ResistanceType.Fire, 60, 70);
			SetResistance(ResistanceType.Cold, 30, 40);
			SetResistance(ResistanceType.Poison, 55, 65);
			SetResistance(ResistanceType.Energy, 50, 60);

			SetSkill(SkillName.Wrestling, 120.0);
			SetSkill(SkillName.Tactics, 120.0);
			SetSkill(SkillName.MagicResist, 120.0);
			SetSkill(SkillName.Anatomy, 120.0);
			SetSkill(SkillName.Poisoning, 120.0);
			SetSkill(SkillName.Hiding, 120.0);

			SetWeaponAbility(WeaponAbility.InfectiousStrike);
		
		    SetAreaEffect(AreaEffect.AuraDamage);

			HideSelf();
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			list.Add(1050045, "Le titan furtif"); // ~1_PREFIX~~2_NAME~~3_SUFFIX~
		}

		public override void OnThink()
		{
			base.OnThink();
			Parole();

		    if (!Hidden)
            {
                double chance = 0.05;

                if (Hits < 20)
                {
                    chance = 0.1;
                }

                if (Poisoned)
                {
                    chance = 0.01;
                }

                if (Utility.RandomDouble() < chance)
                {
                    HideSelf();
                }
  			}

			if (m_GlobalTimer < DateTime.UtcNow && Warmode)
			{

				
					switch (Utility.Random(3))
					{
						case 0:
							ChangeOpponent();
							break;
						case 1:
							Fumigenes();
							break;
						case 2:
							{
								    Mobile combatant = Combatant as Mobile;

								    if (combatant != null)
									{
										if (CanTakeLife(combatant))
											TakeLife(combatant);

									}
								break;
							}
						default:
							break;
					}				
							
				m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
			}
		}

	   #region Take Life
        private DateTime m_NextTakeLife;

        public bool CanTakeLife(Mobile from)
        {
            if (m_NextTakeLife > DateTime.UtcNow)
                return false;

            if (!CanBeHarmful(from))
                return false;

            if (Hits > 0.1 * HitsMax || Hits < 0.025 * HitsMax)
                return false;

            return true;
        }

        public void TakeLife(Mobile from)
        {
            Hits += from.Hits / (from.Player ? 2 : 6);

            FixedParticles(0x376A, 9, 32, 5005, EffectLayer.Waist);
            PlaySound(0x1F2);

			Say("De la vie ! De la vie !");

            m_NextTakeLife = DateTime.UtcNow + TimeSpan.FromSeconds(15 + Utility.RandomDouble() * 45);
        }

        #endregion

  		 public void AuraEffect(Mobile m)
        {
            m.FixedParticles(0x374A, 10, 30, 5052, Hue, 0, EffectLayer.Waist);
            m.PlaySound(0x5C6);

			m.Mana -= 20;

            m.SendMessage("Vous etes mentalement affecté par la présence de Rugal.");
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
	
	#region FlameWave

		public void Fumigenes()
		{

			if (Combatant != null && Combatant is Mobile Caster && DelayAoe1 < DateTime.UtcNow)
			{
				
				new FlameWaveTimer(this).Start();

				DelayAoe1 = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(90, 180));

				HideSelf();
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
							Effects.SendLocationParticles(EffectItem.Create(m_Point, m_Map, EffectItem.DefaultDuration), 0x3709, 10, 30,2500, 0, 5052,0);
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


				     if (m is CustomPlayerMobile cp)
                    {
 
						cp.Freeze(TimeSpan.FromSeconds(5));
                                
                    }
                    else if (m is BaseCreature bc && bc.Controlled && bc.ControlMaster is CustomPlayerMobile)
                    {
                        m.Kill();   
                    }
			}

			
			private double GetDist(Point3D start, Point3D end)
			{
				int xdiff = start.X - end.X;
				int ydiff = start.Y - end.Y;
				return Math.Sqrt((xdiff * xdiff) + (ydiff * ydiff));
			}
		}

		#endregion

		public void ChangeOpponent()
		{
			
				Mobile agro, best = null;
				double distance, random = Utility.RandomDouble();
	
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



		private void HideSelf()
        {
            if (Core.TickCount >= NextSkillTime)
            {
                Effects.SendLocationParticles(
                    EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);

                PlaySound(0x22F);
                Hidden = true;

                UseSkill(SkillName.Hiding);

				ChangeOpponent();
            }
        }

		public void Parole()
		{
			if (m_LastParole < DateTime.Now && Combatant != null && !Hidden)
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

		public Rugal(Serial serial)
			: base(serial)
		{
		}
		public override int Meat => 4;
		public override int TreasureMapLevel => 5;
		public override int Hides => 8;
		public override HideType HideType => HideType.Ancien;
		public override int Bones => 8;
		public override BoneType BoneType => BoneType.Ancien;

	

		

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
