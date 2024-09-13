using Server.Items;
using Server.Spells;
using System.Collections.Generic;
using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System.Linq;
using Server.Custom;

namespace Server.Mobiles
{
	public class PirateBase : BaseCreature
	{


		public static List<string> ParolePirate = new List<string>()
		{
			"Yaarrg !"
		};


		public static List<CustomPlayerMobile> EnCapture = new List<CustomPlayerMobile>();

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

		public override void OnKill(Mobile killed)
		{
			if (killed is CustomPlayerMobile cp && cp.Vulnerability)
			{
				JailPerso(cp);
			}
			base.OnKill(killed);
		}


#region Jail


		public void JailPerso(CustomPlayerMobile cp)
		{
			CustomPersistence.PirateJail(cp);

			Timer.DelayCall(TimeSpan.FromSeconds(2), new TimerStateCallback(Ressurect_Callback), cp);
			
		}

		private void Ressurect_Callback(object state)
		{
			CustomPlayerMobile cp = (CustomPlayerMobile)state;

			cp.Resurrect();


		}





#endregion


		public override bool CanRummageCorpses => true;

		public override bool AlwaysMurderer => true;

		public PirateBase(AIType aiType, FightMode fightMode, int rangePerception, int rangeFight, double activeSpeed, double passiveSpeed)
			: base(aiType, fightMode, rangePerception, rangeFight, activeSpeed, passiveSpeed)
		{

		}


#region throwing


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
			if (ThrowingPotion > 0 )
			{
     	        DoHarmful(m);

				MovingParticles(m, ExplosifItemId, 1, 0, false, true, 0, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0);

				new ThrowingTimer(m, this).Start();
			}
         
        }
 		private class ThrowingTimer : Timer
        {
            private readonly Mobile m_Mobile;
            private readonly PirateBase m_From;
            public ThrowingTimer(Mobile m, PirateBase from)
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
