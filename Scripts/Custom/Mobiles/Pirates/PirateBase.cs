using Server.Items;
using Server.Spells;
using System.Collections.Generic;
using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System.Linq;
using Server.Custom;
using System.Diagnostics;
using Server.Commands;

namespace Server.Mobiles
{
	public class PirateBase : BaseCreature
	{


		public static List<string> ParolePirate = new List<string>()
		{
			"Yaarrg !"
		};

		public DateTime DelayMortalStrike;

		private DateTime m_GlobalTimer;

		public DateTime DelayCharge;


		public DateTime DelayChangeOpponent;

		public virtual int StrikingRange => 12;


		public override TribeType Tribe => TribeType.Pirate;

		public override bool CanBeParagon => false;

		public int ThrowingPotion = 3;
		public virtual int ExplosifRange => 3;

		public virtual int ExplosifItemId => 0x1C19;

		public DateTime m_LastParole = DateTime.MinValue;

		private int m_PirateBoatID;



		[CommandProperty(AccessLevel.GameMaster)]
		public int PirateBoatID
		{
			get => m_PirateBoatID;
			set
			{
				if (m_PirateBoatID != value && value != null)
				{
					ChangeBoat(value);
				}

				m_PirateBoatID = value;
			}
		}


		// ChangeBoat(int boatID)

		public virtual bool AllFemale => false;

		public virtual bool AllowCharge => true;



		public PirateBoat GetPirateBoat()
		{

			return PirateBoat.GetPirateBoat(PirateBoatID);
		}




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



					if (!this.InRange(Combatant.Location, 3) && InLOS(Combatant))
					{
						switch (Utility.Random(3))
						{
							case 0:
								ThrowBomb((Mobile)Combatant);
								break;
							case 1:
								Charge();
								break;
							case 2:
								ChangeOpponent();
								break;
							default:
								break;
						}
					}


					m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
				}


			}


		}



		public void Charge()
		{
			if (DelayCharge < DateTime.UtcNow && AllowCharge)
			{
				if (Combatant is CustomPlayerMobile cp)
				{

					Emote($"*Effectue une charge vers {cp.Name}*");

					cp.Damage(15);

					cp.Freeze(TimeSpan.FromSeconds(4));

					this.Location = cp.Location;
				}


				DelayCharge = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(20, 30));
			}
		}

		public void ChangeOpponent()
		{
			if (DelayChangeOpponent < DateTime.UtcNow)
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
				DelayChangeOpponent = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(20, 30));
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



		public override void OnDeath(Container c)
		{
			base.OnDeath(c);
			Parole();
		}

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			base.OnDamage(amount, from, willKill);

			Parole();

			if (from == null || Combatant == null || !this.InRange(Combatant.Location, 3))
			{
				return;
			}

			if (Utility.Random(10) < 2)
			{
				Item toDisarm = from.FindItemOnLayer(Layer.OneHanded);
				if (toDisarm == null || !toDisarm.Movable)
					toDisarm = from.FindItemOnLayer(Layer.TwoHanded);

				Container pack = from.Backpack;

				if (pack == null || (toDisarm != null && !toDisarm.Movable))
				{
					from.SendLocalizedMessage(1004001); // You cannot disarm your opponent.
				}
				else if (toDisarm == null || toDisarm is BaseShield)
				{
					from.SendLocalizedMessage(1060849); // Your target is already unarmed!
				}
				else
				{
					SendLocalizedMessage(1060092); // You disarm their weapon!
					from.SendLocalizedMessage(1060093); // Your weapon has been disarmed!
					from.PlaySound(0x3B9);
					from.FixedParticles(0x37BE, 232, 25, 9948, EffectLayer.LeftHand);

					pack.DropItem(toDisarm);

					BuffInfo.AddBuff(from, new BuffInfo(BuffIcon.NoRearm, 1075637, TimeSpan.FromSeconds(5.0), from));
					BaseWeapon.BlockEquip(from, TimeSpan.FromSeconds(5.0));

					if (from is BaseCreature)
					{
						Timer.DelayCall(TimeSpan.FromSeconds(5.0) + TimeSpan.FromSeconds(Utility.RandomMinMax(3, 10)), () =>
						{
							if (from != null && !from.Deleted && toDisarm != null && !toDisarm.Deleted && toDisarm.IsChildOf(from.Backpack))
								from.EquipItem(toDisarm);
						});
					}

					Disarm.AddImmunity(from, TimeSpan.FromSeconds(10));
				}
			}
		}


		public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{
			Parole();

			if (to != null && to is CustomPlayerMobile cp && DelayMortalStrike < DateTime.UtcNow)
			{
				if (Spells.SkillMasteries.ResilienceSpell.UnderEffects(to)) //Halves time
					MortalStrike.BeginWound(to, to.Player ? TimeSpan.FromSeconds(3.0) : TimeSpan.FromSeconds(6));
				else
					MortalStrike.BeginWound(to, TimeSpan.FromSeconds(6));


				DelayMortalStrike = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 15));
			}

			base.AlterMeleeDamageTo(to, ref damage);
		}

		public override void AlterRangedDamageTo(Mobile to, ref int damage)
		{
			Parole();

			if (to != null && to is CustomPlayerMobile cp && DelayMortalStrike < DateTime.UtcNow)
			{
				if (Spells.SkillMasteries.ResilienceSpell.UnderEffects(to)) //Halves time
					MortalStrike.BeginWound(to, to.Player ? TimeSpan.FromSeconds(3.0) : TimeSpan.FromSeconds(6));
				else
					MortalStrike.BeginWound(to, TimeSpan.FromSeconds(6));


				DelayMortalStrike = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 15));
			}

			base.AlterMeleeDamageTo(to, ref damage);
		}



		public override void GenerateLoot()
		{
			AddLoot(LootPack.Rich, 3);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));

			if (ThrowingPotion > 0)
			{
				AddLoot(LootPack.LootItem<ExplosionPotion>(ThrowingPotion));
			}

		}


		public override bool IsEnemy(Mobile m)
		{
			if (m is CustomPlayerMobile cp && cp.TribeRelation.Pirate > 75)
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
			if (helper == null || this == null || this.Deleted)
				return;

			helper.Home = location;
			helper.RangeHome = 4;

			if (Combatant != null)
			{
				helper.Warmode = true;
				helper.Combatant = Combatant;
			}
			if (this != null && !this.Deleted) // Ajoutez cette vérification
			{
				BaseCreature.Summon(helper, false, this, this.Location, -1, TimeSpan.FromMinutes(2));
				helper.MoveToWorld(location, Map);
			}
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
			if (killed != null && killed is CustomPlayerMobile cp && cp.Vulnerability)
			{
				JailPerso(cp);
			}
			base.OnKill(killed);
		}


		#region Jail


		public void JailPerso(CustomPlayerMobile cp)
		{
			CustomPersistence.PirateJail(cp);
			Emote($"*Capture {cp.Name}.*");
			Timer.DelayCall(TimeSpan.FromSeconds(2), new TimerStateCallback(Ressurect_Callback), cp);


		}

		private void Ressurect_Callback(object state)
		{
			CustomPlayerMobile cp = (CustomPlayerMobile)state;

			cp.Resurrect();

			if (this != null && this.Alive)
			{
				Delete();
			}
		}


		#endregion


		public override bool CanRummageCorpses => true;

		public override bool AlwaysMurderer => true;

		public PirateBase(AIType aiType, FightMode fightMode, int rangePerception, int rangeFight, double activeSpeed, double passiveSpeed, int pirateBoatId = 0)
			: base(aiType, fightMode, rangePerception, rangeFight, activeSpeed, passiveSpeed)
		{
			if (pirateBoatId == -1)
			{
				PirateBoatID = Utility.Random(PirateBoat.AllPirateBoat.Count());
			}
			else
			{
				PirateBoatID = pirateBoatId;
			}

			SpeechHue = Utility.RandomDyedHue();
			Race = BaseRace.GetRace(1);
			Title = "Un Pirate de " + GetPirateBoat().ToStringWithPronom();



			if (Female = Utility.RandomBool() || AllFemale)
			{
				Female = true;
				Body = 0x191;
				Name = NameList.RandomName("female");
				FemaleCloth();


			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName("male");
				MaleCloth();

			}


			HairItemID = Race.RandomHair(Female);
			HairHue = Race.RandomHairHue();

			FacialHairItemID = Race.RandomFacialHair(Female);
			if (FacialHairItemID != 0)
			{
				FacialHairHue = Race.RandomHairHue();
			}
			else
			{
				FacialHairHue = 0;
			}


		}

		public void ChangeBoat(int boatID)
		{
			Mobile from = this;

			PirateBoat boat = PirateBoat.GetPirateBoat(boatID);

			Title = "Un Pirate de " + boat.ToStringWithPronom();

			var items = from.Items;

			for (int i = from.Items.Count - 1; i >= 0; i--)
			{
				Item item = null;

				try
				{
					item = from.Items[i];
				}
				catch
				{
					from.SendMessage("Erreur dans la recherche de l'item #{0}", i);
					continue;
				}

				try
				{
					if ((item.Movable))
					{

						if (item.Hue == GetPirateBoat().MainHue)
						{
							item.Hue = boat.MainHue;
						}
						else if (item.Hue == GetPirateBoat().AltHue)
						{
							item.Hue = boat.AltHue;
						}


					}
				}
				catch
				{
					from.SendMessage("L'item {0} n'a pas été déplacé dans votre sac à cause d'une erreur.");
				}
			}
		}


		public virtual void FemaleCloth()
		{




			// haut // robe
			switch (Utility.Random(6))
			{
				case 0:
					AddItem(new TuniqueCeinture());
					break;
				case 1:
					AddItem(new TuniqueCombat(GetPirateBoat().MainHue));
					break;
				case 2:
					AddItem(new CorsetTissus(GetPirateBoat().MainHue));
					break;
				case 3:
					AddItem(new Robe15(GetPirateBoat().MainHue));
					break;
				case 4:
					AddItem(new CorsetEpaule(GetPirateBoat().MainHue));
					break;
				case 5:
					AddItem(new RobeCourteLacet(GetPirateBoat().MainHue));
					break;
				default:
					AddItem(new RobeCourteLacet(GetPirateBoat().MainHue));
					break;
			}

			// bas 
			switch (Utility.Random(2))
			{
				case 0:
					AddItem(new Pantalon7(GetPirateBoat().AltHue));
					break;
				case 1:
					AddItem(new ShortPants(GetPirateBoat().AltHue));
					break;
				default:
					AddItem(new ShortPants(GetPirateBoat().AltHue));
					break;
			}



			//Chapeau

			GenerateChapeau();

			AddItem(new Sandals(GetPirateBoat().MainHue));



		}

		public virtual void MaleCloth()
		{
			// haut
			switch (Utility.Random(4))
			{
				case 0:
					AddItem(new TuniqueCeinture());
					break;
				case 1:
					AddItem(new Chandail4(GetPirateBoat().MainHue));
					break;
				case 2:
					AddItem(new TuniqueCombat(GetPirateBoat().MainHue));
					break;
				case 3:
					AddItem(new Shirt(GetPirateBoat().MainHue));
					break;
				default:
					break;
			}

			// bas 
			switch (Utility.Random(2))
			{
				case 0:
					AddItem(new Pantalon7(GetPirateBoat().AltHue));
					break;
				default:
					AddItem(new ShortPants(GetPirateBoat().AltHue));
					break;
			}
			//Chapeau
			GenerateChapeau();


			AddItem(new Sandals(GetPirateBoat().MainHue));

		}

		public virtual void GenerateChapeau()
		{

			switch (Utility.Random(6))
			{
				case 0:
					AddItem(new TricorneHat(GetPirateBoat().AltHue));
					break;
				case 1:
					AddItem(new SkullCap(GetPirateBoat().AltHue));
					break;
				case 2:
					AddItem(new Bandana(GetPirateBoat().AltHue));
					break;
				default:
					// pas de chapeau.
					break;
			}


		}


		#region throwing


		public void Explode(Point3D loc, Map map)
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


		public virtual void ThrowingDetonate(Mobile m)
		{
			DoHarmful(m);

			int damage = Utility.RandomMinMax(20, 30);


			AOS.Damage(m, this, damage, 0, 100, 0, 0, 0, Server.DamageType.SpellAOE);


		}

		public void ThrowBomb(Mobile m)
		{
			if (ThrowingPotion > 0)
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
				m_From.Explode(m_Mobile.Location, m_Mobile.Map);
			}
		}

		#endregion

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(1); // version
			writer.Write(m_PirateBoatID);

		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			var version = reader.ReadInt();

			switch (version)
			{
				case 1:
					{
						m_PirateBoatID = reader.ReadInt();

						goto case 0;
					}
				case 0:
					{
						break;

					}

			}


		}

	}
}
