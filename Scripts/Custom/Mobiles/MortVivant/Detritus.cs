using System;
using System.Collections.Generic;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("le corps de Detritus")]
    public class Detritus : BaseCreature
    {

        public override bool CanStealth => true;  //Stays Hidden until Combatant in range.
	    public DateTime m_LastParole = DateTime.MinValue;

		public DateTime m_LastBlockParole = DateTime.MinValue;

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
			"D-TRI-TUS !",
			"Deeeeeetrrriiittuuuuss !",
			"DETRITUS !",
			"De-Tri-Tus ?"
		};





        [Constructable]
        public Detritus()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "Detritus";
            Body = 302;
            BaseSoundID = 959;
            Hue = 1157;

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


            SetSkill(SkillName.Poisoning,100.0, 120.0);
            SetSkill(SkillName.Hiding, 100.0);
            SetSkill(SkillName.Wrestling, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.MagicResist, 150.0);
            SetSkill(SkillName.Tracking, 150.0);
            
            Fame = 8000;
            Karma = 8000;

            SetSpecialAbility(SpecialAbility.VenomousBite);


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
            RevealingAction();
			base.OnDamage(amount, from, willKill);
			Parole();
		}

         public override void OnDamagedBySpell(Mobile from)
        {
            RevealingAction();
            base.OnDamagedBySpell(from);
         	Parole();
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

            if (!Alive || Deleted)
            {
                return;
            }

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
                base.OnThink();
            }
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

        public override int Bones => Utility.RandomMinMax(3, 5);
        public override Poison HitPoison => Poison.Lethal;

		public override BoneType BoneType => BoneType.Demoniaque;
		public override bool Unprovokable => true;
        public override bool AreaPeaceImmune => true;
        public override Poison PoisonImmune => Poison.Lethal;
        public override int TreasureMapLevel => 3;

        public override TribeType Tribe => TribeType.Undead;

        public Detritus(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
          
			AddLoot(LootPack.FilthyRich, 5);
        	AddLoot(LootPack.Bones, Utility.RandomMinMax(3, 5));
			AddLoot(LootPack.Others, Utility.RandomMinMax(3,5));
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));
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