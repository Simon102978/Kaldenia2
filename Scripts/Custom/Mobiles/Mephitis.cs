using Server.Engines.CannedEvil;
using Server.Items;
using Server.Misc;
using Server.Spells;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Mephitis : BaseCreature
    {
    	public DateTime m_GlobalTimer;
		public DateTime m_NextSpawn;

        public DateTime DelayAoe1;

       	public DateTime DelayAoe2;

        public DateTime m_ParalyseZone;

		public override bool IsScaryToPets => true;

        [Constructable]
        public Mephitis()
                  : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 173;
            Name = "Mephitis";

            BaseSoundID = 0x183;

            SetStr(300, 500);
            SetDex(102, 200);
            SetInt(402, 600);

            SetHits(1200);
            SetStam(105, 600);

            SetDamage(10, 20);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 75, 80);
            SetResistance(ResistanceType.Fire, 60, 70);
            SetResistance(ResistanceType.Cold, 60, 70);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 60, 70);

            SetSkill(SkillName.MagicResist, 70.7, 140.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 97.6, 100.0);
            SetSkill(SkillName.EvalInt, 90.0, 100.0);
            SetSkill(SkillName.Magery, 90.0, 100.0);
            SetSkill(SkillName.Meditation, 80.0, 100.0);
            SetSkill(SkillName.Poisoning, 100.0);

            Fame = 2250;
            Karma = -2250;

       

            ForceActiveSpeed = 0.3;
            ForcePassiveSpeed = 0.6;
        }

        public Mephitis(Serial serial)
            : base(serial)
        {
        }

       	public override void OnThink()
		{
			base.OnThink();

			if (m_GlobalTimer < DateTime.UtcNow && Warmode)
			{

				
					switch (Utility.Random(3))
					{
						case 0:
							ParalyzeZone();
							break;
						case 1:
							PrisonDeTerre();
							break;
                        case 2:
                            VagueIncendiaire();
                            break;
						default:
							break;
					}				
							
				m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
			}
		}


        private void ParalyzeZone()
		{

			if (m_ParalyseZone < DateTime.UtcNow)
			{
                List<Mobile> list = new List<Mobile>();
                IPooledEnumerable eable = GetMobilesInRange(12);

                foreach (Mobile m in eable)
                {
                    if (AreaEffect.ValidTarget(this, m))
                        list.Add(m);
                }

                eable.Free();

                if (list.Count > 0)
                {
                    Mobile m = list[Utility.Random(list.Count)];

                    DoHarmful(m, false);
                    Direction = GetDirectionTo(m);

                    SpiderWebbing web = new SpiderWebbing(m);
                    Effects.SendMovingParticles(this, m, web.ItemID, 12, 0, false, false, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
                    Timer.DelayCall(TimeSpan.FromSeconds(0.5), () => web.MoveToWorld(m.Location, m.Map));
                }
		
				m_ParalyseZone = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 60));
			}
			
		}
	#region PrisondeTerre

		public void PrisonDeTerre()
		{


			if (Combatant != null &&  Combatant is Mobile Caster && DelayAoe1 < DateTime.UtcNow)
			{
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

				this.Location = Caster.Location;
				Emote("*Saute dans les aires et frappe le sol de toute ses forces*");

				DelayAoe1 = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(90, 180));
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
				Name = "Pierre";
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


	#region FlameWave


		public void VagueIncendiaire()
		{

			if (Combatant != null && Combatant is Mobile Caster && DelayAoe2 < DateTime.UtcNow)
			{
				new FlameWaveTimer(this).Start();

				DelayAoe2 = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(90, 180));
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
							Effects.SendLocationParticles(EffectItem.Create(m_Point, m_Map, EffectItem.DefaultDuration), 0x3709, 10, 30,1167, 0, 5052,0);
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



			

					// It looked like it delt 67 damage, presuming 70% fire res thats about 223 damage delt before resistance.
				AOS.Damage(m, m_From, dmg, 0, 0, 0, 100, 0);

                m.ApplyPoison(m_From,Poison.DarkGlow);

				


			}
			private double GetDist(Point3D start, Point3D end)
			{
				int xdiff = start.X - end.X;
				int ydiff = start.Y - end.Y;
				return Math.Sqrt((xdiff * xdiff) + (ydiff * ydiff));
			}
		}

		#endregion








        public override Poison PoisonImmune => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 3);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
