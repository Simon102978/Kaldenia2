using Server.Items;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Joseph : BaseCreature
    {
		public DateTime DelayCharge;
		public DateTime TuerSummoneur;

        public DateTime DelayWarCrie;

        public DateTime m_LastParole = DateTime.MinValue;

		public DateTime m_LastBlockParole = DateTime.MinValue;

        public DateTime m_NextSpawn;

        public DateTime m_GlobalTimer;

        public static List<string> ParoleListe = new List<string>()
		{
			"Ca se fait pas de me déranger quand je m'amuse avec ma plus belle courtisane !",
			"Vous avez pas autre chose a faire ?!",
			"Je sais qu'on a les plus belle courtisane, mais c'est pas une raison !",
            "À voir vos femmes, je comprend pourquoi vous venez ici ...",
            "*Soupire* C'est bon, amene toi...",      
		};

		[Constructable]
        public Joseph()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Joseph";
            SpeechHue = Utility.RandomDyedHue();
            Title = "Chef des brigands";
   
            Body = 0x190;

            AddItem(new ShortPants(Utility.RandomRedHue()));

            Female = false;
            

			Race = BaseRace.GetRace(1);

			SetStr(175, 225);
            SetDex(151, 165);
            SetInt(161, 175);

            SetDamage(8, 15);

			SetHits(300);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 25, 30);
            SetResistance(ResistanceType.Cold, 25, 30);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Anatomy, 75.0);
            SetSkill(SkillName.Fencing, 46.0, 77.5);
            SetSkill(SkillName.Macing, 35.0, 57.5);
            SetSkill(SkillName.Poisoning, 60.0, 82.5);
            SetSkill(SkillName.MagicResist, 83.5, 92.5);
            SetSkill(SkillName.Swords, 100);
            SetSkill(SkillName.Tactics, 100.0);


            Fame = 3000;
            Karma = -3000;

            AddItem(new ThighBoots(Utility.RandomRedHue()));
            AddItem(new Surcoat(Utility.RandomRedHue()));
            AddItem(new ExecutionersAxe());

            Utility.AssignRandomHair(this);
        }

        public Joseph(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer => true;

		public override TribeType Tribe => TribeType.Brigand;
		public bool BlockReflect { get; set; }


		public override void OnThink()
		{
			base.OnThink();
            Parole();

            if (m_GlobalTimer < DateTime.UtcNow && Warmode)
			{

					switch (Utility.Random(3))
					{
						case 0:
							{
                            	if (Combatant != null)
                                {

                                        if (Combatant is BaseCreature bc)
                                        {
                                            AntiSummon();
                                        }

                                        if (!this.InRange(Combatant.Location, 3) && this.InRange(Combatant.Location, 10))
                                        {
                                            Charge();
                                        }
                                }
                                break;
                            }
							
						case 1:
							Spawn();
							break;
						case 2:
							WarCrie();
							break;
						default:
							break;
					}	

					m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));			
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

					Emote("*Lance un crie de guerre* WAAARRGGG !");

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

                Disarm.AddImmunity(to, TimeSpan.FromSeconds(10));
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

       	public override void Attack(IDamageable e)
		{
			Parole();
			base.Attack(e);
		}

        public void Spawn()
		{
			if (m_NextSpawn < DateTime.UtcNow)
			{

				Emote("*Allez les gars !*");

				int Nombre = 0;

				while(Nombre < 3)
				{
					int monstre = Utility.Random(5);

					switch(monstre)
					{
						case 0:
						{
							SpawnHelper( new BrigandAgile (	), Location);	
							break;
						}
						case 1:
						{
							SpawnHelper( new BrigandAmbusher (	), Location);	
							break;
						}
						case 2:
						{
							SpawnHelper( new BrigandApprenti (	), Location);
							break;
						}
						case 3:
						{
							SpawnHelper( new Courtisane(	), Location);
							break;	
						}
						case 4:
						{
							SpawnHelper( new Brigand	(), Location);
							break;	
						}
						default:
						{	
							SpawnHelper( new BrigandArcher (	), Location);	
							break;
						}
					}
					Nombre++;			
				}

				m_NextSpawn = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 120));

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

        public void Parole()
		{
			if (m_LastParole < DateTime.Now && Combatant != null)
			{	
				Say(ParoleListe[Utility.Random(ParoleListe.Count)]);
			
				m_LastParole = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
			}
		}



		public void Charge()
		{
			if (DelayCharge < DateTime.UtcNow)
			{
					if (Combatant is CustomPlayerMobile cp)
					{

						Emote($"*Effectue une charge vers {cp.Name}*");

						cp.Damage(15);

						cp.Freeze(TimeSpan.FromSeconds(3));

						this.Location = cp.Location;
					}
				

				DelayCharge = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 50));
			}
		}

		public void AntiSummon()
		{
			if (TuerSummoneur < DateTime.UtcNow)
			{
				if (Combatant is BaseCreature bc)
				{

						if (bc.ControlMaster is CustomPlayerMobile cp)
						{
							Combatant = cp;

							Emote($"*Effectue une charge vers {cp.Name}*");

							cp.Damage(15);

							cp.Freeze(TimeSpan.FromSeconds(3));

							this.Location = cp.Location;

						}
					}
				
				TuerSummoneur = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 50));
			}
		}



		public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            int dam = base.Damage(amount, from, informMount, checkDisrupt);

            if (!BlockReflect && from != null && dam > 0)
            {
                BlockReflect = true;
                AOS.Damage(from, this, dam, 0, 0, 0, 0, 0, 0, 100);
                BlockReflect = false;

                from.PlaySound(0x1F1);
            }

            return dam;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Meager);
			AddLoot(LootPack.Others, Utility.RandomMinMax(5, 6));
			AddLoot(LootPack.LootItem<Items.Gold>(50, 100));

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
