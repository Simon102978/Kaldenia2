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
	[CorpseName("Corp de Saek'Ssthis")]
	public class SaekSsthis : BasePeerless
	{
		public static List<string> ParoleListe = new List<string>()
		{
			"Un nouveau sSsacrifisSe!",
			"Viieens Voiir Saek'Ssthis !",
			"Joouoons à un Jeu ! Saek'SsThis te mange, pendant que tu hurle !",
			"Medusa va être fière de sa fille."
		};


		public DateTime m_GlobalTimer;
		public DateTime m_NextSpawn;

		public DateTime DelayRegardParalysant;
		public DateTime DelayCoupBaton;

		public DateTime DelayPrisonGlace;

		public virtual int StrikingRange => 12;
		public override bool AutoDispel => true;
		public override double AutoDispelChance => 1.0;
		public override bool BardImmune => true;
		public override bool Unprovokable => true;
		public override bool Uncalmable => true;
		public override Poison PoisonImmune => Poison.Deadly;

		public bool BlockReflect { get; set; }

		public override bool CanBeParagon => false;

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

		[Constructable]
		public SaekSsthis()
			: base(AIType.AI_Archer, FightMode.Closest, 10, 5, 0.2, 0.4)
		{
			Name = "Saek'Ssthis";
			Title = "Fille de Medusa";
		    Body = 85;
            BaseSoundID = 639;
			Hue = 1327;

            SetStr(281, 305);
            SetDex(191, 215);
            SetInt(226, 250);

            SetHits(400, 600);
            SetStam(36, 45);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Cold, 100);

            SetResistance(ResistanceType.Physical, 40, 45);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 35, 40);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.EvalInt, 95.1, 100.0);
            SetSkill(SkillName.Magery, 95.1, 100.0);
            SetSkill(SkillName.MagicResist, 75.0, 97.5);
            SetSkill(SkillName.Tactics, 90.0, 100.5);
            SetSkill(SkillName.Wrestling, 90.2, 100.0);
			SetSkill(SkillName.Anatomy, 90.2, 100.0);
		    SetSkill(SkillName.Regeneration, 150.0);

            Fame = 11500;
            Karma = -11500;

			 AddItem(new GantSaekSsthis());

			SetWeaponAbility(WeaponAbility.DoubleShot);

			SetWeaponAbility(WeaponAbility.LightningArrow);
			//LightningArrow
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



		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			list.Add(1050045, "Fille de Medusa"); // ~1_PREFIX~~2_NAME~~3_SUFFIX~
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
							CoupBaton();
							break;
						case 1:
							RegardParalysant();
							break;
						case 2:
							PrisondeGlace();
							break;
						default:
							break;
					}				
							
				m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
			}
		}

		public override void OnDeath(Container c)
		{
			if (m_Stone != null)
            {
                m_Stone.MobActif = false;
            }

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


			base.AlterMeleeDamageTo(to, ref damage);
		}

		public override void Attack(IDamageable e)
		{
			Parole();
			base.Attack(e);
		}
		public SaekSsthis(Serial serial)
			: base(serial)
		{
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
	
		#region  CoupBaton

		public void CoupBaton()
		{

			if (DelayCoupBaton < DateTime.UtcNow && Combatant != null && Combatant is Mobile m && m.InRange(this,3)) 
			{
				CoupBatonAction(m);
				Emote($"*Donne un bon coup de baton à {Combatant.Name}*");
				
				DelayCoupBaton = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 60));
			}
		}

		public void CoupBatonAction(Mobile m)
		{
				int dmg = 25;

				Combatant.FixedParticles(0x374A, 10, 30, 5013, 1153, 2, EffectLayer.Waist);

				AOS.Damage(Combatant, this, dmg, 100, 0, 0, 0, 0); // C'est un coup de vent, donc rien d'electrique...

				

				KnockBack(this.Location, Combatant as Mobile, 5);

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

		#endregion

	#region PrisondeGlace

		public void PrisondeGlace()
		{


			if (Combatant != null &&  Combatant is Mobile Caster && DelayPrisonGlace < DateTime.UtcNow)
			{

				if (Caster.InRange(this,4))
				{
					CoupBatonAction(Caster);
					Emote($"*Donne un coup de baton à {Combatant.Name} et ce concentre sur celui-ci'*");
				}
				else
				{
					Emote($"*Ce concentre sur {Combatant.Name}.'*");
				}


				IPoint3D p = Caster.Location;

				SpellHelper.GetSurfaceTop(ref p);

				Effects.PlaySound(p, Combatant.Map, 0x222);

				Point3D loc = new Point3D(p.X, p.Y, p.Z);
				int mushx;
				int mushy;
				int mushz;

				InternalItem firstFlamea = new InternalItem(Combatant.Location, Combatant.Map);
				mushx = loc.X - 2;
				mushy = loc.Y - 2;
				mushz = loc.Z;
				Point3D mushxyz = new Point3D(mushx, mushy, mushz);
				firstFlamea.MoveToWorld(mushxyz, Caster.Map);

				InternalItem firstFlamec = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X;
				mushy = loc.Y - 3;
				mushz = loc.Z;
				Point3D mushxyzb = new Point3D(mushx, mushy, mushz);
				firstFlamec.MoveToWorld(mushxyzb, Caster.Map);

				InternalItem firstFlamed = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X + 2;
				mushy = loc.Y - 2;
				mushz = loc.Z;
				Point3D mushxyzc = new Point3D(mushx, mushy, mushz);
				firstFlamed.MoveToWorld(mushxyzc, Caster.Map);

				InternalItem hiddenflame = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X + 2;
				mushy = loc.Y - 1;
				mushz = loc.Z;
				Point3D mushxyzhid = new Point3D(mushx, mushy, mushz);
				hiddenflame.MoveToWorld(mushxyzhid, Caster.Map);
				InternalItem hiddenrock = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X + 2;
				mushy = loc.Y + 1;
				mushz = loc.Z;
				Point3D rockaxyz = new Point3D(mushx, mushy, mushz);
				hiddenrock.MoveToWorld(rockaxyz, Caster.Map);
				InternalItem hiddenflamea = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X - 2;
				mushy = loc.Y - 1;
				mushz = loc.Z;
				Point3D mushxyzhida = new Point3D(mushx, mushy, mushz);
				hiddenflamea.MoveToWorld(mushxyzhida, Caster.Map);
				InternalItem hiddenrocks = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X - 2;
				mushy = loc.Y + 1;
				mushz = loc.Z;
				Point3D rocksaxyz = new Point3D(mushx, mushy, mushz);
				hiddenrocks.MoveToWorld(rocksaxyz, Caster.Map);
				InternalItem hiddenrocka = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X + 1;
				mushy = loc.Y + 2;
				mushz = loc.Z;
				Point3D rockbxyz = new Point3D(mushx, mushy, mushz);
				hiddenrocka.MoveToWorld(rockbxyz, Caster.Map);
				InternalItem hiddenrockb = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X + 1;
				mushy = loc.Y - 2;
				mushz = loc.Z;
				Point3D rockcxyz = new Point3D(mushx, mushy, mushz);
				hiddenrockb.MoveToWorld(rockcxyz, Caster.Map);
				InternalItem hiddenrockc = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X - 1;
				mushy = loc.Y - 2;
				mushz = loc.Z;
				Point3D rockdxyz = new Point3D(mushx, mushy, mushz);
				hiddenrockc.MoveToWorld(rockdxyz, Caster.Map);
				InternalItem hiddenrockd = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X - 1;
				mushy = loc.Y + 2;
				mushz = loc.Z;
				Point3D rockexyz = new Point3D(mushx, mushy, mushz);
				hiddenrockd.MoveToWorld(rockexyz, Caster.Map);
				InternalItem firstFlamee = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X + 3;
				mushy = loc.Y;
				mushz = loc.Z;
				Point3D mushxyzd = new Point3D(mushx, mushy, mushz);
				firstFlamee.MoveToWorld(mushxyzd, Caster.Map);
				InternalItem firstFlamef = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X + 2;
				mushy = loc.Y + 2;
				mushz = loc.Z;
				Point3D mushxyze = new Point3D(mushx, mushy, mushz);
				firstFlamef.MoveToWorld(mushxyze, Caster.Map);
				InternalItem firstFlameg = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X;
				mushy = loc.Y + 3;
				mushz = loc.Z;
				Point3D mushxyzf = new Point3D(mushx, mushy, mushz);
				firstFlameg.MoveToWorld(mushxyzf, Caster.Map);
				InternalItem firstFlameh = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X - 2;
				mushy = loc.Y + 2;
				mushz = loc.Z;
				Point3D mushxyzg = new Point3D(mushx, mushy, mushz);
				firstFlameh.MoveToWorld(mushxyzg, Caster.Map);
				InternalItem firstFlamei = new InternalItem(Caster.Location, Caster.Map);
				mushx = loc.X - 3;
				mushy = loc.Y;
				mushz = loc.Z;
				Point3D mushxyzh = new Point3D(mushx, mushy, mushz);
				firstFlamei.MoveToWorld(mushxyzh, Caster.Map);

			//	this.Location = Caster.Location;
	

				DelayPrisonGlace = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(90, 180));
			}
			

			


		}


		[DispellableField]
		private class InternalItem : Item
		{
			private Timer m_Timer;
			private DateTime m_End;
			private ArrayList frozen;

			public override bool CanBeLock => false;

			public override bool BlocksFit { get { return true; } }

			public InternalItem(Point3D loc, Map map) : base(0x08E2)
			{
				Visible = true;
				Movable = false;
				ItemID = Utility.RandomList(2274, 2275, 2272, 2273, 2279, 2280);
				Name = "Morceau de glace";
				Hue = 1972;
				MoveToWorld(loc, map);



				if (Deleted)
					return;

				m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(30.0));
				m_Timer.Start();

				m_End = DateTime.Now + TimeSpan.FromSeconds(30.0);
			}

			public InternalItem(Serial serial) : base(serial)
			{
			}
			public override bool OnMoveOver(Mobile m)
			{
				m.SendMessage("La magie sur la pierre vous empêche de passé.");
				return false;
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);

				writer.Write((int)1); // version

				writer.Write(m_End - DateTime.Now);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);

				int version = reader.ReadInt();

				switch (version)
				{
					case 1:
						{
							TimeSpan duration = reader.ReadTimeSpan();

							m_Timer = new InternalTimer(this, duration);
							m_Timer.Start();

							m_End = DateTime.Now + duration;

							break;
						}
					case 0:
						{
							TimeSpan duration = TimeSpan.FromSeconds(10.0);

							m_Timer = new InternalTimer(this, duration);
							m_Timer.Start();

							m_End = DateTime.Now + duration;

							break;
						}
				}
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if (m_Timer != null)
					m_Timer.Stop();
			}

			private class InternalTimer : Timer
			{
				private InternalItem m_Item;

				public InternalTimer(InternalItem item, TimeSpan duration) : base(duration)
				{
					m_Item = item;
				}

				protected override void OnTick()
				{
					m_Item.Delete();
				}
			}
		}

		#endregion

	





        public override int Meat => 1;
        public override int TreasureMapLevel => 3;

        public override TribeType Tribe => TribeType.Ophidian;

		public override int Hides => 6;
		public override HideType HideType => HideType.Ophidien;

		public override int Bones => 6;
		public override BoneType BoneType => BoneType.Ophidien;


		public override void GenerateLoot()
        {
   			AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
            AddLoot(LootPack.MageryRegs, 5, 15);
            AddLoot(LootPack.NecroRegs, 5, 15);
			AddLoot(LootPack.LootItem<Items.Gold>(500,1000));
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
			writer.Write(m_Stone);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

			switch (version)
			{
				case 1:
				{
				    m_Stone = (WallControlerStone)reader.ReadItem();
					break;
				}
				default:
					break;
			}

		}
    }










}
