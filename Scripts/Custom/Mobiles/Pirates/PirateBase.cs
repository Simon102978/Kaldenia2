using Server.Items;
using Server.Spells;
using System.Collections.Generic;
using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System.Linq;

namespace Server.Mobiles
{
	public class PirateBase : BaseCreature
	{


		public static List<string> ParolePirate = new List<string>()
		{
			"Yaarrg !"
		};

		private DateTime m_GlobalTimer;


		public override TribeType Tribe => TribeType.Pirate;

		public override bool CanBeParagon => false;

		public int ThrowingPotion = 3;
		public virtual int  ExplosifRange => 3;

		public virtual int  ExplosifItemId => 0x1C19;

		public DateTime m_LastParole = DateTime.MinValue;

		private Timer m_PotionTimer;

		public PirateBase(Serial serial)
			: base(serial)
		{
		}

		public override void OnThink()
		{
			base.OnThink();
			Parole();

			if (Combatant != null)
			{
				
				if (m_GlobalTimer < DateTime.UtcNow)
				{

                    

					if (!this.InRange(Combatant.Location,3) && InLOS(Combatant))
					{
						switch (Utility.Random(0))
						{
							case 0:
								ThrowBomb((Mobile)Combatant);
								break;
							default:							
								break;
						}
					}
						

					m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
				}
			

			}


		}

		public override void OnDeath(Container c)
		{
			base.OnDeath(c);
			Parole();
		}

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			Parole();

			base.OnDamage(amount, from, willKill);
		}

		public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{

			Parole();



			base.AlterMeleeDamageTo(to, ref damage);
		}

		public override bool IsEnemy(Mobile m)
		{
			if (m is CustomPlayerMobile cp && cp.Race.RaceID == 1 && cp.TribeRelation.Pirate > 40)
			{
				return false;
			}

			return base.IsEnemy(m);
		}

		public override void Attack(IDamageable e)
		{
			Parole();
			base.Attack(e);
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

		public override void DoHarmful(IDamageable target, bool indirect)
		{
			Parole();

			base.DoHarmful(target, indirect);
		}

		public void Parole()
		{

			//  Mis la parce que presque tout call ca.


			if (m_LastParole < DateTime.Now && Combatant != null)
			{
				if (Combatant is CustomPlayerMobile cp)
						Say(ParolePirate[Utility.Random(ParolePirate.Count)]);

				m_LastParole = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
			}

		}

		public override bool CanRummageCorpses => true;

		public override bool AlwaysMurderer => true;

		public PirateBase(AIType aiType, FightMode fightMode, int rangePerception, int rangeFight, double activeSpeed, double passiveSpeed)
			: base(aiType, fightMode, rangePerception, rangeFight, activeSpeed, passiveSpeed)
		{

		}


#region throwing
/*
        public virtual void ThrowExplosive ()
        {
            if (Paralyzed || Frozen || (Spell != null && Spell.IsCasting) || ThrowingPotion < 0 )
            {
                SendLocalizedMessage(1062725); // You can not use a purple potion while paralyzed.
                return;
            }
         
            RevealingAction();

            if (m_PotionTimer == null)
            {
                SendLocalizedMessage(500236); // You should throw it now!

                m_PotionTimer = Timer.DelayCall(
                        TimeSpan.FromSeconds(1.0),
                        TimeSpan.FromSeconds(1.25),
                        5,
                        new TimerStateCallback(Detonate_OnTick),
                        new object[] { this, 3 }); // 3.6 seconds explosion delay

            }
        }

        public void Explode( Point3D loc, Map map)
        {
            if (Deleted || this == null)
            {
                return;
            }

            ThrowingPotion--;

			ThrowingEffect(this.Location, this.Map);
	
          	List<Mobile> list = SpellHelper.AcquireIndirectTargets(this, loc, map, ExplosifRange, false).OfType<Mobile>().ToList();
           

            foreach (Mobile m in list)
            {
              ThrowingDetonate(m);
            }

            list.Clear();
        }

		public virtual void ThrowingEffect(Point3D loc, Map map)
		{
			Effects.PlaySound(loc, map, 0x307);

            Effects.SendLocationEffect(loc, map, 0x36B0, 9, 10, 0, 0);
		}


        private void Detonate_OnTick(object state)
        {
            if (Deleted)
            {
                return;
            }

            object[] states = (object[])state;
            Mobile from = (Mobile)states[0];
            int timer = (int)states[1];      

            if (timer == 0)
            {
                Point3D loc;
                Map map;

                loc = Location;
                map = Map;
 
                Explode( loc, map);
                m_PotionTimer = null;
            }
            else
            {
                this.PublicOverheadMessage(MessageType.Regular, 0x22, false, timer.ToString());
                states[1] = timer - 1;
            }
        }

 
*/

        public void Explode( Point3D loc, Map map)
        {
            if (Deleted || this == null)
            {
                return;
            }

            ThrowingPotion--;

          	List<Mobile> list = SpellHelper.AcquireIndirectTargets(this, loc, map, ExplosifRange, false).OfType<Mobile>().ToList();
           

            foreach (Mobile m in list)
            {
              ThrowingDetonate(m);
            }

            list.Clear();
        }


		public virtual void ThrowingDetonate (Mobile m)
		{
 				DoHarmful(m);

                int damage = Utility.RandomMinMax(20, 30);

            
                AOS.Damage(m, this, damage, 0, 100, 0, 0, 0, Server.DamageType.SpellAOE);


		}

        public void ThrowBomb(Mobile m)
        {
			if (ThrowingPotion < 0 )
			{
     	        DoHarmful(m);

				MovingParticles(m, ExplosifItemId, 1, 0, false, true, 0, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0);

				new InternalTimer(m, this).Start();
			}
         
        }
 		private class InternalTimer : Timer
        {
            private readonly Mobile m_Mobile;
            private readonly PirateBase m_From;
            public InternalTimer(Mobile m, PirateBase from)
                : base(TimeSpan.FromSeconds(1.0))
            {
                m_Mobile = m;
                m_From = from;
                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
              m_From.Explode(m_Mobile.Location,m_Mobile.Map);
        	}
		}
      
#endregion

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			var version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					break;

				}
				
			}


		}

	}
}
