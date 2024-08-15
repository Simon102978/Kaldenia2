using Server.Items;
using Server.Spells;
using System.Collections.Generic;
using System;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	public class KepushBase : BaseCreature, IDevourer
	{

	//	public DateTime DelayCharge;

		public DateTime DelayBleeding;

		public DateTime LastTrackSanguinaire;

		public bool m_TraqueSanguinaire = false;
	
		[CommandProperty(AccessLevel.GameMaster)]
		public bool TraqueSanguinaire
		{
			get => m_TraqueSanguinaire;
			set
			{
				if (value && !m_TraqueSanguinaire)
				{
					m_TraqueSanguinaire = value;
					ActiverTraqueSanguinaire();
				}
				else if (!value && m_TraqueSanguinaire )
				{
					m_TraqueSanguinaire = value;
					DesactiverTraqueSanguinaire();
				}
				else
				{
					m_TraqueSanguinaire = value;
				}
			}
		}

		public static List<string> ParoleKepush = new List<string>()
		{
			"Keos referme ton oeil sur le monde !",
			"Enfin libérer ! Affronte les enfants des titans",
			"Nous voulons ton sang pour l offrande",
			"Mon corps est Dévoué au père",
			"Brûler moi pour nourrir l unique"

		};

		private const double MinutesToNextEatMin = 1.0;
        private const double MinutesToNextEatMax = 4.0;

        private const double MinutesToNextEatChanceMin = 0.25;
        private const double MinutesToNextEatChanceMax = 0.75;

	    private const double ChanceToEat = 0.5; // 50%



        private long m_NextEatTime;


		public override TribeType Tribe => TribeType.Kepush;

		public override bool CanBeParagon => false;

		public DateTime m_LastParole = DateTime.MinValue;

		public KepushBase(Serial serial)
			: base(serial)
		{
		}

		public override void OnThink()
		{
			base.OnThink();
			Parole();

			 long tc = Core.TickCount;

			if (!Controlled && tc >= m_NextEatTime)
            {
                double min, max;

                if (ChanceToEat > Utility.RandomDouble() && Eat())
                {
                    min = MinutesToNextEatMin;
                    max = MinutesToNextEatMax;
                }
                else
                {
                    min = MinutesToNextEatChanceMin;
                    max = MinutesToNextEatChanceMax;
                }

                double delay = min + (Utility.RandomDouble() * (max - min));
                m_NextEatTime = tc + (int)TimeSpan.FromMinutes(delay).TotalMilliseconds;
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

			if (DelayBleeding < DateTime.UtcNow)
			{
				MakeBleeding(to);

				DelayBleeding = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
			}


			base.AlterMeleeDamageTo(to, ref damage);
		}

		public override bool IsEnemy(Mobile m)
		{
			if (m is CustomPlayerMobile cp && cp.Race.RaceID == 7 && cp.TribeRelation.Kepush > 40)
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

			if (TraqueSanguinaire && LastTrackSanguinaire.AddMinutes(1) > DateTime.Now )
			{
				TraqueSanguinaire = false;
			}

			if (m_LastParole < DateTime.Now && Combatant != null)
			{
				if (Combatant is CustomPlayerMobile cp)
						Say(ParoleKepush[Utility.Random(ParoleKepush.Count)]);

				m_LastParole = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
			}

		}

		public override bool CanRummageCorpses => true;

		public override bool AlwaysMurderer => true;

		public KepushBase(AIType aiType, FightMode fightMode, int rangePerception, int rangeFight, double activeSpeed, double passiveSpeed)
			: base(aiType, fightMode, rangePerception, rangeFight, activeSpeed, passiveSpeed)
		{

		}

		public void MakeBleeding(Mobile m)
		{
  			BleedAttack.BeginBleed(m, this, true);

			 IPooledEnumerable eable = m.GetMobilesInRange(5); // Get all mobile

            foreach (Mobile item in eable)
            {
                if (item is KepushBase kep) // For each Corpse
                {
                   kep.CheckTraqueSanguinaire(m);
                }
            }
            eable.Free();
		}


		public void CheckTraqueSanguinaire(Mobile m)
		{
			if (LastTrackSanguinaire.AddMinutes(5) < DateTime.Now && !TraqueSanguinaire && IsEnemy(m))
			{
				Combatant = m;
				TraqueSanguinaire = true;
			}

		}

		public void ActiverTraqueSanguinaire()
		{
			AdjustSpeeds();
			Say("DU SANG !!!");

		}

		
		public void DesactiverTraqueSanguinaire()
		{
			AdjustSpeeds();
			Emote("*Se calme*");

		}


		public override void AdjustSpeeds()
		{
			double activeSpeed = 0.0;
			double passiveSpeed = 0.0;


			if (TraqueSanguinaire)
			{
				SpeedInfo.GetCustomSpeeds(this, ref activeSpeed, ref passiveSpeed);
			}
			else
			{
				SpeedInfo.GetSpeeds(this, ref activeSpeed, ref passiveSpeed);
			}

			ActiveSpeed = activeSpeed;
			PassiveSpeed = passiveSpeed;
			CurrentSpeed = passiveSpeed;
		}


		public virtual bool Eat()
        {
			   // Check to see if we need to devour any corpses
            IPooledEnumerable eable = GetItemsInRange(3); // Get all corpses in range

            foreach (Item item in eable)
            {
                if (item is Corpse) // For each Corpse
                {
                    Corpse corpse = item as Corpse;

                    // Ensure that the corpse was killed by us
                    if (corpse != null && corpse.Killer == this && corpse.Owner != null)
                    {
                        if (!corpse.DevourCorpse() && !corpse.Devoured)
                            PublicOverheadMessage(MessageType.Emote, 0x3B2, 1053032); // * The plague beast attempts to absorb the remains, but cannot! *
                    }
                }
            }
            eable.Free();

            return false;
        }

		public bool Devour(Corpse corpse)
        {
            if (corpse == null || corpse.Owner == null) // sorry we can't devour because the corpse's owner is null
                return false;

            if (corpse.Owner.Body.IsHuman)
                corpse.TurnToBones(); // Not bones yet, and we are a human body therefore we turn to bones.

            Hits += ((int)Math.Ceiling(corpse.Owner.HitsMax * 0.75));
 
            PublicOverheadMessage(MessageType.Emote, 0x3B2,false, $"Ce fait un snack avec le corps de {corpse.Owner.Name}"); 



            return true;
        }


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(0); // version

			writer.Write(m_TraqueSanguinaire);
			writer.Write(LastTrackSanguinaire);


		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			var version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_TraqueSanguinaire = reader.ReadBool();
					LastTrackSanguinaire = reader.ReadDateTime();
					break;

				}
				
			}


		}

	}
}
