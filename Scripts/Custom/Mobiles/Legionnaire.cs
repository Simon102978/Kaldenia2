using Server.ContextMenus;
using Server.Items;
using Server.Regions;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Legionnaire : BaseHire
    {
		public DateTime DelayCharge;
		public DateTime TuerSummoneur;

		public override bool CanDetectHidden => true;
     	public override bool BardImmune => true;


		[Constructable]
        public Legionnaire()
                   : base(AIType.LegionnaireAi)
    //        : base()
        {
            SpeechHue = Utility.RandomDyedHue();
				IsBonded = false;
           

            if (Utility.RandomBool())
            {
                 Title = "Legionnaire";
            }
            else
            {
                 Title = "Legionnaire 1ere classe";
            }


            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                
            }

			Race = BaseRace.GetRace(Utility.Random(4));

			SetStr(386, 400);
            SetDex(151, 165);
            SetInt(161, 175);

            SetDamage(8, 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 25, 30);
            SetResistance(ResistanceType.Cold, 25, 30);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Anatomy, 125.0);
            SetSkill(SkillName.Fencing, 46.0, 77.5);
            SetSkill(SkillName.Macing, 35.0, 57.5);
            SetSkill(SkillName.Poisoning, 60.0, 82.5);
            SetSkill(SkillName.MagicResist, 83.5, 92.5);
            SetSkill(SkillName.Swords, 125.0);
            SetSkill(SkillName.Tactics, 125.0);
            SetSkill(SkillName.Lumberjacking, 125.0);

            Fame = 5000;
            Karma = -5000;

            AddItem(new Backpack());

            AddItem(new Cape7
			{
				Name = "Cape brodee d'un aigle d'or",
				Hue = 2252,
				LootType = LootType.Blessed,
				Layer = Layer.Cloak,
			});
			AddItem(new JambiereMailleRenforce
			{
				Name = "Jambière Broigne",
				Hue = 1102,
				LootType = LootType.Blessed,
				Layer = Layer.Pants,
				Quality = ItemQuality.Exceptional,
				Resource = CraftResource.Acier,
			});
			AddItem(new Boots
			{
				Name = "Bottes simples",
				Hue = 1889,
				LootType = LootType.Blessed,
				Layer = Layer.Shoes,
			});
			AddItem(new PlastronMailleRenforce
			{
				Name = "Plastron Broigne",
				Hue = 1102,
				LootType = LootType.Blessed,
				Layer = Layer.InnerTorso,
				Quality = ItemQuality.Exceptional,
				Resource = CraftResource.Acier,
			});
			AddItem(new BrassardMaillerRenforce
			{
				Name = "Brassard Broigne",
				Hue = 1102,
				LootType = LootType.Blessed,
				Layer = Layer.Arms,
				Quality = ItemQuality.Exceptional,
				Resource = CraftResource.Acier,
			});
			AddItem(new GantMailleRenforce
			{
				Name = "Gants Broigne",
				Hue = 1102,
				LootType = LootType.Blessed,
				Layer = Layer.Gloves,
				Quality = ItemQuality.Exceptional,
				Resource = CraftResource.Acier,
			});
			AddItem(new CasqueKorain
			{
				Name = "Casque Korain",
				LootType = LootType.Blessed,
				Layer = Layer.Helm,
				Quality = ItemQuality.Exceptional,
				Resource = CraftResource.Iron,
			});
			AddItem(new Pavois2
			{
				Name = "Pavois Décoratif",
				Hue = 1102,
				LootType = LootType.Blessed,
				Layer = Layer.TwoHanded,
				Quality = ItemQuality.Epic,
				Resource = CraftResource.Acier,
			});
			AddItem(new VikingSword
			{
				LootType = LootType.Blessed,
				Layer = Layer.FirstValid,
				Quality = ItemQuality.Epic,
				Resource = CraftResource.Iron,
			});
			AddItem(new Ceinture
			{
				Name = "Ceinture boucle ronde",
				LootType = LootType.Blessed,
				Layer = Layer.Bracelet,
			});
			AddItem(new Cocarde
			{
				Name = "Cocarde",
				Hue = 2252,
				LootType = LootType.Blessed,
				Layer = Layer.Waist,
			});

            Utility.AssignRandomHair(this);
        }

        public Legionnaire(Serial serial)
            : base(serial)
        {
        }



		public bool BlockReflect { get; set; }

		public override void OnThink()
		{
			base.OnThink();

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

		
		public override TribeType Tribe => TribeType.Legion;
		
		public override bool GetControlled()
		{
			return true; 
		}

		public override bool IsPetFriend(Mobile m)
		{
			if (m is CustomPlayerMobile cp && cp.GetTribeValue(TribeType.Legion) > 90)
			{
				return true;
			}


			return base.IsFriend(m);
		}


		public override bool CanBeControlledBy(Mobile m)
		{
			if (m is CustomPlayerMobile cp && cp.GetTribeValue(TribeType.Legion) > 90)
			{
				return true;
			}


			return base.CanBeControlledBy(m);
		}


		public override bool AllowEquipFrom(Mobile from)
		{
			if (from.IsStaff())
			{
				return true;
			}
			else
			{
				return false;
			}
	
		}


		public override void AggressiveAction(Mobile aggressor, bool criminal)
		{
			 if (aggressor is BaseCreature)
			{
				var pm = ((BaseCreature)aggressor).GetMaster() as PlayerMobile;

				if (pm != null)
				{
					AggressiveAction(pm, criminal);
				}
			}

			base.AggressiveAction(aggressor, criminal);

			OrderType ct = ControlOrder;

			if (AIObject != null)
			{
		
					AIObject.OnAggressiveAction(aggressor);
				
				
			}

			//StopFlee();
			ForceReacquire();

			if (aggressor.ChangingCombatant &&
				(ct == OrderType.Come || ct == OrderType.Stay || ct == OrderType.Stop || ct == OrderType.None ||
				 ct == OrderType.Follow))
			{
				ControlTarget = aggressor;
				ControlOrder = OrderType.Attack;
			}
			else if (Combatant == null)
			{
				Warmode = true;
				Combatant = aggressor;
			}
		}


	
			
		public override void OnExitCity()
		{
			if (Spawner != null )
			{
				
				World.Broadcast(37, false, AccessLevel.Counselor, $"Un légionnaire essait de sortir de Mirage à {X},{Y},{Z}");

				this.Delete();

			}


		}


		public override bool CheckControlChance(Mobile m)
		{
			
			if (m is CustomPlayerMobile cp && cp.GetTribeValue(TribeType.Legion) > 90)
			{
				return true;
			}

			return base.CheckControlChance(m);
		}

        public override void GenerateLoot()
        {
            // Pas un monstre bande de tordus..
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
