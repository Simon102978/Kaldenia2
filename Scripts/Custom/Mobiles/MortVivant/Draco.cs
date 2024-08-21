using Server.Items;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Server.Mobiles
{


    [CorpseName("Corps de Draco")]
    public class Draco : BaseCreature
    {
		public DateTime DelayWarCrie;
		public DateTime DelayMaladies;
		public DateTime m_NextDiscordTime;
		public DateTime TuerSummoneur;
		private DateTime m_GlobalTimer;
		public DateTime m_LastParole = DateTime.MinValue;

		public DateTime m_LastBlockParole = DateTime.MinValue;

		private bool m_InHere;

		public static List<string> ParoleListe = new List<string>()
		{
			"Vien t'amuser dans la mort, on rit beaucoup, vous savez ?",
			"C'est vilain !",
			"MAAMAAN ! Le vilain humanoide me fait mal !",
		};

		[Constructable]
        public Draco()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Draco";
            Title = "un bebe dragon necrotique";
            Body = 104;
            Hue = 2299;
            BaseSoundID = 0x488;

            SetStr(136, 206);
            SetDex(123, 222);
            SetInt(118, 127);

            SetHits(409, 842);

			SetDamage(19, 28);

			SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 46, 47);
            SetResistance(ResistanceType.Fire, 32, 40);
            SetResistance(ResistanceType.Cold, 34, 49);
            SetResistance(ResistanceType.Poison, 40, 48);
            SetResistance(ResistanceType.Energy, 35, 39);
			
			SetSkill(SkillName.EvalInt, 45, 60);
            SetSkill(SkillName.Magery, 50, 65);
            SetSkill(SkillName.Wrestling, 106.4, 128.8);
            SetSkill(SkillName.Tactics, 129.9, 141.0);
            SetSkill(SkillName.MagicResist, 84.3, 90.1);
        }

        public Draco(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);

            AddLoot(LootPack.Parrot);
			AddLoot(LootPack.LootItem<Items.Gold>(100, 175));
			AddLoot(LootPack.RandomLootItem(new System.Type[] { typeof(SilverRing), typeof(Necklace), typeof(SilverNecklace), typeof(Collier), typeof(Collier2),  typeof(Collier3), typeof(Couronne3),  typeof(Collier4), typeof(Tiare), }, 10.0, 1, false, true));
			AddLoot(LootPack.LootItem<Items.Gemme>(), (double) 5);



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
						switch (Utility.Random(3))
						{
							case 0:
								WarCrie();
								break;
							case 1:
								Maladies();
								break;
							case 2:
								if (Combatant is Mobile m)
								{
									Discord(m);
								}	
								break;
							default:
								break;
						}
					}
						

					m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
				}
			

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

		#region Unholy Touch
		private static readonly Dictionary<Mobile, Timer> m_UnholyTouched = new Dictionary<Mobile, Timer>();

		public void Discord(Mobile target)
		{
			if (Utility.RandomDouble() < 0.9 && !m_UnholyTouched.ContainsKey(target))
			{
				double scalar = -((20 - (target.Skills[SkillName.MagicResist].Value / 10)) / 100);

				ArrayList mods = new ArrayList();

				if (target.PhysicalResistance > 0)
				{
					mods.Add(new ResistanceMod(ResistanceType.Physical, (int)(target.PhysicalResistance * scalar)));
				}

				if (target.FireResistance > 0)
				{
					mods.Add(new ResistanceMod(ResistanceType.Fire, (int)(target.FireResistance * scalar)));
				}

				if (target.ColdResistance > 0)
				{
					mods.Add(new ResistanceMod(ResistanceType.Cold, (int)(target.ColdResistance * scalar)));
				}

				if (target.PoisonResistance > 0)
				{
					mods.Add(new ResistanceMod(ResistanceType.Poison, (int)(target.PoisonResistance * scalar)));
				}

				if (target.EnergyResistance > 0)
				{
					mods.Add(new ResistanceMod(ResistanceType.Energy, (int)(target.EnergyResistance * scalar)));
				}

				for (int i = 0; i < target.Skills.Length; ++i)
				{
					if (target.Skills[i].Value > 0)
					{
						mods.Add(new DefaultSkillMod((SkillName)i, true, target.Skills[i].Value * scalar));
					}
				}

				target.PlaySound(0x458);

				ApplyMods(target, mods);

				Emote($"*Utilise son touché glaciale contre {target.Name}*");

				m_UnholyTouched[target] = Timer.DelayCall(TimeSpan.FromSeconds(30), delegate
				{
					ClearMods(target, mods);

					m_UnholyTouched.Remove(target);
				});
			}

			m_NextDiscordTime = DateTime.UtcNow + TimeSpan.FromSeconds(5 + Utility.RandomDouble() * 22);
			m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
		}

		private static void ApplyMods(Mobile from, ArrayList mods)
		{
			for (int i = 0; i < mods.Count; ++i)
			{
				object mod = mods[i];

				if (mod is ResistanceMod)
					from.AddResistanceMod((ResistanceMod)mod);
				else if (mod is StatMod)
					from.AddStatMod((StatMod)mod);
				else if (mod is SkillMod)
					from.AddSkillMod((SkillMod)mod);
			}
		}

		private static void ClearMods(Mobile from, ArrayList mods)
		{
			for (int i = 0; i < mods.Count; ++i)
			{
				object mod = mods[i];

				if (mod is ResistanceMod)
					from.RemoveResistanceMod((ResistanceMod)mod);
				else if (mod is StatMod)
					from.RemoveStatMod(((StatMod)mod).Name);
				else if (mod is SkillMod)
					from.RemoveSkillMod((SkillMod)mod);
			}
		}
		#endregion



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


		public void WarCrie()
		{


			if (DelayWarCrie < DateTime.UtcNow)
			{
					int Range = 10;
					List<Mobile> targets = new List<Mobile>();

					IPooledEnumerable eable = this.GetMobilesInRange(Range);

					foreach (Mobile m in eable)
					{
						if (this != m && !(m is Brigand) &&  !(m is BrigandArcher) && !(m is BrigandAmbusher) && !(m is BrigandApprenti) && !(m is Courtisane) &&  !m.IsStaff())
						{
							if (Core.AOS && !InLOS(m))
								continue;

							targets.Add(m);
						}
					}

					eable.Free();

					Emote("CHIICK !");

					if (targets.Count > 0)
					{
                        Hits += targets.Count * 10;
			
						for (int i = 0; i < targets.Count; ++i)
						{
							Mobile m = targets[i];

							DoHarmful(m);

                            if (m is CustomPlayerMobile cp)
                            {
                                if (Combatant is BaseCreature)
                                {
                                    Combatant = cp;
                                }

                                DoDisarm(cp);
                                
                            }
                            else if (m is BaseCreature bc)
                            {
                                DoProvoke(bc);
                            }


						}
					}

				DelayWarCrie = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 60));

			}
		}

        public virtual void DoProvoke(BaseCreature creature)
		{
                    if (creature == null || !creature.Alive )
                    {
                       return; 
                    }
                    else if (creature.ControlMaster != null)
                    {
                        return;
                    }
                     
            
                    Mobile target = creature.ControlMaster;

                    if (creature.Unprovokable)
                    {
                       return;
                    }

                    else if (creature != target)
                    {
                        if ((CanBeHarmful(creature, true, false, true) && CanBeHarmful(target, true, false, true)))
                        {                                    
                                    creature.Provoke(this, target, true);                                          
                        }
                    }		
		}

        public virtual void DoDisarm(Mobile to)
        {
                 Item toDisarm = to.FindItemOnLayer(Layer.OneHanded);

                    if (toDisarm == null || !toDisarm.Movable)
                        toDisarm = to.FindItemOnLayer(Layer.TwoHanded);

                    Container pack = to.Backpack;

                    if (pack == null || (toDisarm != null && !toDisarm.Movable))
                    {
                        to.SendLocalizedMessage(1004001); // You cannot disarm your opponent.
                    }
                    else if (toDisarm == null || toDisarm is BaseShield)
                    {
                        to.SendLocalizedMessage(1060849); // Your target is already unarmed!
                    }
                
                SendLocalizedMessage(1060092); // You disarm their weapon!
                to.SendLocalizedMessage(1060093); // Your weapon has been disarmed!

                to.PlaySound(0x3B9);
                to.FixedParticles(0x37BE, 232, 25, 9948, EffectLayer.LeftHand);

                pack.DropItem(toDisarm);

                BuffInfo.AddBuff(to, new BuffInfo(BuffIcon.NoRearm, 1075637, TimeSpan.FromSeconds(5.0), to));

                BaseWeapon.BlockEquip(to, TimeSpan.FromSeconds(5.0));

                if (to is BaseCreature)
                {
                    Timer.DelayCall(TimeSpan.FromSeconds(5.0) + TimeSpan.FromSeconds(Utility.RandomMinMax(3, 10)), () =>
                    {
                        if (toDisarm != null && !toDisarm.Deleted && toDisarm.IsChildOf(to.Backpack))
                            to.EquipItem(toDisarm);
                    });
                }

             	try
					{
						Disarm.AddImmunity(to, TimeSpan.FromSeconds(10));
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Erreur lors de l'appel � Disarm.AddImmunity : {ex.Message}");
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


        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (from != null && from != this && !m_InHere)
            {
                m_InHere = true;
                AOS.Damage(from, this, Utility.RandomMinMax(8, 20), 100, 0, 0, 0, 0);

                MovingEffect(from, 0xECA, 10, 0, false, false, 0, 0);
                PlaySound(0x491);

                if (0.05 > Utility.RandomDouble())
                    Timer.DelayCall(TimeSpan.FromSeconds(1.0), new TimerStateCallback(CreateBones_Callback), from);

                m_InHere = false;
            }

           	Parole();
        }

        public virtual void CreateBones_Callback(object state)
        {
            Mobile from = (Mobile)state;
            Map map = from.Map;

            if (map == null)
                return;

            int count = Utility.RandomMinMax(1, 3);

            for (int i = 0; i < count; ++i)
            {
                int x = from.X + Utility.RandomMinMax(-1, 1);
                int y = from.Y + Utility.RandomMinMax(-1, 1);
                int z = from.Z;

                if (!map.CanFit(x, y, z, 16, false, true))
                {
                    z = map.GetAverageZ(x, y);

                    if (z == from.Z || !map.CanFit(x, y, z, 16, false, true))
                        continue;
                }

                UnholyBone bone = new UnholyBone
                {
                    Hue = 0,
                    Name = "Os Maudit",
                    ItemID = Utility.Random(0xECA, 9)
                };

                bone.MoveToWorld(new Point3D(x, y, z), map);
            }
        }
	
			
		

    	public override bool CanFlee => false;

        public override bool IgnoreYoungProtection => true;
        public override bool BardImmune => false;
        public override bool Unprovokable => true;
        public override bool AreaPeaceImmune => true;
        public override Poison PoisonImmune => Poison.Lethal;
        public override int TreasureMapLevel => 3;
    	public override TribeType Tribe => TribeType.Undead;

        public override bool AutoDispel => true;
		
        public override bool BleedImmune => true;

		public override bool IsScaryToPets => true;

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
