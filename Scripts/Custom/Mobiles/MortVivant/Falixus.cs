﻿using Server.Engines.CannedEvil;
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
	[CorpseName("Corp de Falixus")]
	public class Falixus : BasePeerless
	{
		public static List<string> ParoleListe = new List<string>()
		{
			""
		};

		public DateTime m_GlobalTimer;
		public DateTime m_NextSpawn;

		public DateTime DelayCharge;
		public DateTime DelayMortalStrike;

		public virtual int StrikingRange => 12;
		public override bool AutoDispel => true;
		public override double AutoDispelChance => 1.0;
		public override bool BardImmune => true;
		public override bool Unprovokable => true;
		public override bool Uncalmable => true;
		public override Poison PoisonImmune => Poison.Deadly;
		public override bool CanRummageCorpses => true;

      //  public override TribeType Tribe => TribeType.Undead;

  		public override TribeType Tribe => TribeType.Terathan;

        public override bool BleedImmune => true;
		


		public bool BlockReflect { get; set; }

		public override bool CanBeParagon => false;

		public DateTime m_LastParole = DateTime.MinValue;

		public DateTime m_LastBlockParole = DateTime.MinValue;

		[Constructable]
		public Falixus()
			: base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Name = "Falixus";
			Title = "L'honorable";
			Body = 147;
			Hue = 1654;
			
			SetStr(208, 319);
            SetDex(98, 132);
            SetInt(45, 91);

            SetHits(616, 884);

            SetDamage(8, 13);

            SetDamageType(ResistanceType.Physical, 100);
         
            SetResistance(ResistanceType.Physical, 55, 62);
            SetResistance(ResistanceType.Fire, 40, 48);
            SetResistance(ResistanceType.Cold, 71, 80);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 50, 60);

            SetSkill(SkillName.Wrestling, 75.3, 90.5);
            SetSkill(SkillName.Tactics, 75.5, 90.8);
            SetSkill(SkillName.MagicResist, 102.8, 117.9);
            SetSkill(SkillName.Anatomy, 75.5, 90.2);

            Fame = 18000;
            Karma = -18000;

            SetSpecialAbility(SpecialAbility.LifeDrain);
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			list.Add(1050045, "L'honorable"); // ~1_PREFIX~~2_NAME~~3_SUFFIX~
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
							Charge();
							break;
						case 1:
							GlobaleMortalStrike();
							break;
						case 2:
						//	Spawn();
							break;
						default:
							break;
					}				
							
				m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
			}
		}

		public override void OnDeath(Container c)
		{
			base.OnDeath(c);
			Parole();

		}

		public void GlobaleMortalStrike()
		{
			if (DelayMortalStrike < DateTime.UtcNow)
			{
				int Range = 3;
				List<Mobile> targets = new List<Mobile>();

				IPooledEnumerable eable = this.GetMobilesInRange(Range);

				foreach (Mobile m in eable)
				{
					if (this != m  && !m.IsStaff())
					{
						if (m is BaseCreature bc && bc.Tribe == TribeType.Undead)
						{
							continue;
						}

						targets.Add(m);
					}
				}

				eable.Free();

				

				if (targets.Count > 0)
				{
					Emote("*Frappes le sol*");

					int dmg = 25;



					for (int i = 0; i < targets.Count; ++i)
					{
						Mobile m = targets[i];


						DoHarmful(m);
						AOS.Damage(m, this, dmg, 100, 0, 0, 0, 0); // C'est un coup de vent, donc rien d'electrique...

						MortalStrike.BeginWound(m, TimeSpan.FromSeconds(20));

					
					}
				}

				DelayMortalStrike = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));

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

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			base.OnDamage(amount, from, willKill);
			
		 if (from is BaseCreature)
			{
				BaseCreature creature = (BaseCreature)from;

				if (creature.Controlled || creature.Summoned)
				{

				

					if (Hits < HitsMax)
						Hits = HitsMax;

					creature.Kill();

					Effects.PlaySound(Location, Map, 0x574);
				}
			}
			
			Parole();
		}

		public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            int dam = base.Damage(amount, from, informMount, checkDisrupt);

            /*if (!BlockReflect && from != null && dam > 0)
            {
                BlockReflect = true;
                AOS.Damage(from, this, dam, 0, 0, 0, 0, 0, 0, 50);
                BlockReflect = false;

                from.PlaySound(0x1F1);
            }*/

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

		 	Hits += AOS.Scale(damage, 50);

			Effects.SendPacket(to.Location, to.Map, new ParticleEffect(EffectType.FixedFrom, to.Serial, Serial.Zero, 0x377A, to.Location, to.Location, 1, 15, false, false, 1926, 0, 0, 9502, 1, to.Serial, 16, 0));
            Effects.SendPacket(to.Location, to.Map, new ParticleEffect(EffectType.FixedFrom, to.Serial, Serial.Zero, 0x3728, to.Location, to.Location, 1, 12, false, false, 1963, 0, 0, 9042, 1, to.Serial, 16, 0));

			Parole();
			base.AlterMeleeDamageTo(to, ref damage);
		}

		public override void Attack(IDamageable e)
		{
			Parole();
			base.Attack(e);
		}
		public Falixus(Serial serial)
			: base(serial)
		{
		}



	



		public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.ArcanistScrolls);
		    AddLoot(LootPack.LootItem<Scimitar>());
            AddLoot(LootPack.LootItem<WoodenShield>());
			AddLoot(LootPack.Bones, Utility.RandomMinMax(3, 5));
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));

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
