using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'une liche ancienne")]
    public class Profanian : BaseCreature
    {
        public DateTime m_GlobalTimer;
		public DateTime m_NextSpawn;

        public DateTime m_SpawnChienProfanian;

        public DateTime DelayMaladies;

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
			"J'existe moi aussi !",
			"Meme si vous gagné, votre victoire sera vite oublier !",
			"Jeee vivrraiis eternelllemeent dans l'oubliie",
			"Snif, Snif"
		};
        [Constructable]
        public Profanian()
            : base(AIType.ArcherMageAI, FightMode.Closest, 10, 10, 0.2, 0.4)
        {
            Name = "Profanian";

			Title = "L'Éternelle Oubliée";
            Body = 78;
            BaseSoundID = 412;
            Hue = 1167;

           
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

            Fame = 10000;
            Karma = -10000;

		    AddItem(new GantZanYanXan());

        




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
                m_Stone.Active = true;
            }


        }



    	public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            int dam = base.Damage(amount, from, informMount, checkDisrupt);

            

			

			
            return dam;
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

        public void SpawnChien()
		{
			if (m_SpawnChienProfanian < DateTime.UtcNow)
			{

			
                Mobile target = GetRandomTarget(10);

                if (target != null)
                {
                    Emote("*Pouvez-vous jouer un peu avec lui, le temps qu'il m'oubli ?*");

                    ChienProfanian rev = new ChienProfanian(this, target, TimeSpan.FromSeconds(180));

                    if (BaseCreature.Summon(rev, false, this, target.Location, 0x81, TimeSpan.FromSeconds(182.0)))
                        rev.FixedParticles(0x373A, 1, 15, 9909, EffectLayer.Waist);				
                }


				m_SpawnChienProfanian = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(120, 200));

			}


		}

        private Mobile GetRandomTarget(int range)
        {
            List<Mobile> list = GetTargets(range, true);
            Mobile m = null;

            if (list != null && list.Count > 0)
            {
                m = list[Utility.Random(list.Count)];
                ColUtility.Free(list);
            }

            return m;
        }
        private List<Mobile> GetTargets(int range, bool playersOnly)
        {
            List<Mobile> targets = new List<Mobile>();

            IPooledEnumerable eable = GetMobilesInRange(range);
            foreach (Mobile m in eable)
            {
                if (m == this || !CanBeHarmful(m))
                    continue;

                if (!playersOnly && m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != Team))
                    targets.Add(m);
                else if (m.Player)
                    targets.Add(m);
            }
            eable.Free();

            return targets;
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





       	public override void OnThink()
		{
			base.OnThink();
			Parole();

			if (m_GlobalTimer < DateTime.UtcNow && Warmode)
			{

				
					switch (Utility.Random(3))
					{
						case 0:
							SpawnChien();
							break;
						case 1:
							 Maladies();
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

        public void Maladies()
		{
			if (DelayMaladies < DateTime.UtcNow)
			{
				int Range = 5;
				List<Mobile> targets = new List<Mobile>();

				IPooledEnumerable eable = this.GetMobilesInRange(Range);

				foreach (Mobile m in eable)
				{
					if (this != m && !(m is BaseCreature bc && bc.Tribe != TribeType.Undead)  && !m.IsStaff())
					{
						if (Core.AOS && !InLOS(m))
							continue;

						targets.Add(m);
					}
				}

				eable.Free();

				Effects.PlaySound(this, this.Map, 0x1FB);
				Effects.PlaySound(this, this.Map, 0x10B);
				Effects.SendLocationParticles(EffectItem.Create(this.Location, this.Map, EffectItem.DefaultDuration), 0x37CC, 1, 15, 1460, 3, 9917, 0);

				Emote("*Balance un nuage verdatre*");

				if (targets.Count > 0)
				{

					
					for (int i = 0; i < targets.Count; ++i)
					{
						Mobile m = targets[i];


						DoHarmful(m);

                        BleedAttack.BeginBleed(m, this, true);

                         m.FixedEffect(0x3779, 1, 10, 1271, 0);
                         m.ApplyPoison(this, Poison.DarkGlow);

					}
				}


				DelayMaladies = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(120, 180));

			}

			
		}

       	public void Parole()
		{
			if (m_LastParole < DateTime.Now && Combatant != null)
			{	
				Say(ParoleListe[Utility.Random(ParoleListe.Count)]);
			
				m_LastParole = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
			}
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
                m_Stone.Active = false;
            }

			base.OnDeath(c);
		

		}

        public Profanian(Serial serial)
            : base(serial)
        {
        }

        public override TribeType Tribe => TribeType.Undead;
		
        public override bool Unprovokable => true;
		
        public override bool BleedImmune => true;
		
        public override Poison PoisonImmune => Poison.Lethal;
		
        public override int TreasureMapLevel => 5;



		public override int GetIdleSound()
        {
            return 0x19D;
        }

        public override int GetAngerSound()
        {
            return 0x175;
        }

        public override int GetDeathSound()
        {
            return 0x108;
        }

        public override int GetAttackSound()
        {
            return 0xE2;
        }

        public override int GetHurtSound()
        {
            return 0x28B;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.MedScrolls, 2);
            AddLoot(LootPack.NecroRegs, 100, 200);
			AddLoot(LootPack.BodyPartsAndBones, Utility.RandomMinMax(3, 5));
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));
			AddLoot(LootPack.LootItem<Items.Gold>(100, 200));
			AddLoot(LootPack.LootItem<CerveauLiche>(3, 7));
			AddLoot(LootPack.Others, Utility.RandomMinMax(5, 12));

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
