using Server.ContextMenus;
using Server.Gumps;
using Server.Multis;
using Server.Targeting;
using System.Collections.Generic;

using System;
using System.Linq;

namespace Server.Items
{
    public interface IDyable
    {
        bool Dye(Mobile from, DyeTub sender);
    }

    public class DyeTub : Item, ISecurable
    {
        private bool m_Redyable;
        private int m_DyedHue;
        private SecureLevel m_SecureLevel;
		private int m_Charges;
		private bool m_IsInUse;

		[Constructable]
        public DyeTub()
            : base(0xFAB)
        {
            Weight = 10.0;
            m_Redyable = true;
			m_Charges = 10;
			Name = "Bac de teinture";

		}

        public DyeTub(Serial serial)
            : base(serial)
        {
        }
		public override void GetProperties(ObjectPropertyList list)
		{
				base.GetProperties(list);
				list.Add(String.Format("[{0} charge{1}]", m_Charges, m_Charges > 1 ? "s" : ""));
		}
		

	
		public virtual CustomHuePicker CustomHuePicker => null;
        public virtual bool AllowRunebooks => false;
        public virtual bool AllowFurniture => false;
        public virtual bool AllowStatuettes => false;
        public virtual bool AllowLeather => false;
        public virtual bool AllowDyables => true;
        public virtual bool AllowMetal => false;
		public virtual bool AllowWeapons => false;
		public virtual bool AllowJewels => false;

		public virtual bool AllowStatuetteCire => true;







		[CommandProperty(AccessLevel.GameMaster)]
        public bool Redyable
        {
            get { return m_Redyable; }
            set { m_Redyable = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DyedHue
        {
            get { return m_DyedHue; }
            set
            {
                if (m_Redyable)
                {
                    m_DyedHue = value;
                    Hue = value;
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level
        {
            get { return m_SecureLevel; }
            set { m_SecureLevel = value; }
        }

		[CommandProperty(AccessLevel.GameMaster)]
		public int Charges
		{
			get
			{ 
				return m_Charges; }
			set { m_Charges = value; }
		}

		public virtual int TargetMessage => 500859;  // Select the clothing to dye.        
        public virtual int FailMessage => 1042083;  // You can not dye that.

        public virtual Type[] ForcedDyables => new Type[0];

        public virtual bool CanForceDye(Item item)
        {
            return ForcedDyables != null && ForcedDyables.Any(t => t == item.GetType());
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1); // version

            writer.Write((int)m_SecureLevel);
            writer.Write(m_Redyable);
            writer.Write(m_DyedHue);
			writer.Write((int)m_Charges);
		}

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
			
			m_SecureLevel = (SecureLevel)reader.ReadInt();
			m_Redyable = reader.ReadBool();
            m_DyedHue = reader.ReadInt();
			m_Charges = reader.ReadInt();
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            SetSecureLevelEntry.AddTo(from, this, list);
        }

		public override void OnDoubleClick(Mobile from)
		{
			if (m_IsInUse)
			{
				from.SendMessage("Ce bac de teinture est déjà en cours d'utilisation.");
				return;
			}

			if (from.InRange(GetWorldLocation(), 1))
			{
				m_IsInUse = true;
				from.SendLocalizedMessage(TargetMessage);
				from.Target = new InternalTarget(this);
			}
			else
			{
				from.SendLocalizedMessage(500446); // That is too far away.
			}
		}

		private class InternalTarget : Target
		{
			private readonly DyeTub m_Tub;

			public InternalTarget(DyeTub tub) : base(1, false, TargetFlags.None)
			{
				m_Tub = tub;
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				base.OnTargetCancel(from, cancelType);
				m_Tub.m_IsInUse = false;
			}

			protected override void OnTarget(Mobile from, object targeted)
            {
				if (targeted is SpecialDyeTub)
				{
					from.SendMessage("Vous ne pouvez pas teindre un bac de teinture spécial avec un autre bac de teinture.");
					m_Tub.m_IsInUse = false;
					return;
				}

				if (targeted is Item item)
				{
					BacVide bacvide = new BacVide();

					var armor = item as BaseArmor;
					var clothing = item as BaseClothing;

					if (armor != null && armor.MaterialType == ArmorMaterialType.Cloth)
					{
						if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
						{
							from.SendLocalizedMessage(500446); // That is too far away.
						}
						else if (!item.Movable)
						{
							from.SendLocalizedMessage(1042419); // You may not dye items which are locked down.
						}
						else if (item.Parent is Mobile)
						{
							from.SendLocalizedMessage(500861); // Can't Dye armor that is being worn.
						}
						else
						{
							item.Hue = m_Tub.DyedHue;
							from.PlaySound(0x23E);

							if (m_Tub.Charges > 1)
							{
								m_Tub.Charges -= 1;
							}
							else
							{
								m_Tub.Delete();
								from.AddToBackpack(bacvide);
								from.SendMessage("Votre bac de teinture n'a plus de charge.");
							}
						}
					}


					else if (item is IDyable && m_Tub.AllowDyables)
					{
						if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
							from.SendLocalizedMessage(500446); // That is too far away.
						else if (item.IsLockedDown)
							from.SendLocalizedMessage(1061637); // You are not allowed to access this.
						else if (item.Parent is Mobile)
							from.SendLocalizedMessage(500861); // Can't Dye clothing that is being worn.
						else if (((IDyable)item).Dye(from, m_Tub))
							from.PlaySound(0x23E);

						if (targeted is IDyable && m_Tub.AllowDyables)
						{
							if (targeted is SpecialDyeTub specialTub)
							{
								if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(specialTub.GetWorldLocation(), 1))
								{
									from.SendLocalizedMessage(500446); // That is too far away.
								}
								else if (specialTub.Dye(from, m_Tub))
								{
									from.PlaySound(0x23E);
									if (m_Tub.Charges > 1)
									{
										m_Tub.Charges -= 1;
									}
									else
									{
										m_Tub.Delete();
										from.AddToBackpack(new BacVide());
										from.SendMessage("Votre bac de teinture n'a plus de charge.");
									}
								}
							}




							if (m_Tub.Charges > 1)
						{
							m_Tub.Charges -= 1;
						}

						else
						{
							m_Tub.Delete();
							from.AddToBackpack(bacvide);
							from.SendMessage("Votre bac de teinture n'a plus de charge.");

						}
					}
						else if (m_Tub.AllowFurniture && (FurnitureAttribute.Check(item) || m_Tub.CanForceDye(item) || item is AddonComponent || AllowedTypes.Contains(item.GetType())))
						{
							if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
							{
								from.SendLocalizedMessage(500446); // That is too far away.
							}
							else
							{
								bool okay = (item.IsChildOf(from.Backpack));

								if (!okay)
								{
									if (item.Parent == null)
									{
										BaseHouse house = BaseHouse.FindHouseAt(item);

										if (!house.IsCoOwner(from))
											from.SendLocalizedMessage(501023); // You must be the owner to use this item.
										else if (house == null || (!house.IsLockedDown(item) && !house.IsSecure(item)) && (!(item is AddonComponent) || !house.Addons.ContainsKey(((AddonComponent)item).Addon)))
											from.SendLocalizedMessage(501022); // Furniture must be locked down to paint it.
										else
											okay = true;
									}
									else
									{
										from.SendLocalizedMessage(1048135); // The furniture must be in your backpack to be painted.
									}
								}

								if (okay)
								{
									item.Hue = m_Tub.DyedHue;
									from.PlaySound(0x23E);

									if (m_Tub.Charges > 1)
									{
										m_Tub.Charges -= 1;
									}
									else
									{
										m_Tub.Delete();
										from.AddToBackpack(bacvide);
										from.SendMessage("Votre bac de teinture n'a plus de charge.");
									}
								}
							}
						}

						else if (item is BaseClothing || item is BaseHat || item is Chapelet || item is BaseBook)
						{
							if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
							{
								from.SendLocalizedMessage(500446); // That is too far away.
							}
							else if (!item.Movable)
							{
								from.SendLocalizedMessage(1042419); // You may not dye items which are locked down.
							}
							else if (item.Parent is Mobile)
							{
								from.SendLocalizedMessage(500861); // Can't Dye clothing that is being worn.
							}
							else
							{
								item.Hue = m_Tub.DyedHue;
								from.PlaySound(0x23E);

								if (m_Tub.Charges > 1)
								{
									m_Tub.Charges -= 1;
								}
								else
								{
									m_Tub.Delete();
									from.AddToBackpack(new BacVide());
									from.SendMessage("Votre bac de teinture n'a plus de charge.");
								}
							}
						}

						else if	(item is FurnitureDyeTub fdye )
					{
						if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
						{
							from.SendLocalizedMessage(500446); // That is too far away.
						}
						else if (!item.Movable)
						{
							from.SendMessage("Vous ne pouvez teindre les objets qui ne sont pas verouillées.");
						}
						else
						{

							fdye.DyedHue = m_Tub.DyedHue;
							from.PlaySound(0x23E);


							if (m_Tub.Charges > 1)
							{
								m_Tub.Charges -= 1;
							}

							else
							{
								m_Tub.Delete();
								from.AddToBackpack(bacvide);
								from.SendMessage("Votre bac de teinture n'a plus de charge.");

							}
						}

					}
						else if (m_Tub.AllowStatuetteCire && AllowedTypes.Contains(item.GetType()) ||
								 item is PileOfBlankCandles ||
								 item is SomeBlankCandles ||
								 item is OfficialSealingWax ||
								 item is RawWaxBust ||
								 item is OrigamiButterfly ||
								 item is OrigamiSwan ||
								 item is OrigamiFrog ||
								 item is OrigamiShape ||
								 item is OrigamiSongbird ||
								 item is OrigamiFish ||
								 item is OrigamiDragon ||
								 item is OrigamiBunny)
						{
							if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
							{
								from.SendLocalizedMessage(500446); // That is too far away.
							}
							else if (!item.Movable)
							{
								from.SendLocalizedMessage(1042419); // You may not dye items which are locked down.
							}
							else
							{
								item.Hue = m_Tub.DyedHue;
								from.PlaySound(0x23E);

								if (m_Tub.Charges > 1)
								{
									m_Tub.Charges -= 1;
								}
								else
								{
									m_Tub.Delete();
									from.AddToBackpack(new BacVide());
									from.SendMessage("Votre bac de teinture n'a plus de charge.");
								}
							}
						}


						else if (m_Tub.AllowRunebooks && (item is Runebook || item is RecallRune || item is BaseBook || item is Spellbook || m_Tub.CanForceDye(item)))
					{
						if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
						{
							from.SendLocalizedMessage(500446); // That is too far away.
						}
						else if (!item.Movable)
						{
							from.SendLocalizedMessage(1049776); // You cannot dye runes or runebooks that are locked down.
						}
						else
						{
							item.Hue = m_Tub.DyedHue;
							from.PlaySound(0x23E);


							if (m_Tub.Charges > 1)
							{
								m_Tub.Charges -= 1;
							}

							else
							{
								m_Tub.Delete();
								from.AddToBackpack(bacvide);
								from.SendMessage("Votre bac de teinture n'a plus de charge.");

							}
						}
					}
						else if (m_Tub.AllowJewels && (item is BaseJewel || item is Chapelet || item is BaseRing || item is BaseBracelet || item is BaseNecklace || m_Tub.CanForceDye(item)))
						{
							if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
							{
								from.SendMessage("C'est trop loin!"); 
							}
							else if (!item.Movable)
							{
								from.SendMessage("Vous ne pouvez pas teindre un item verouillé!"); 
							}
							else
							{
								item.Hue = m_Tub.DyedHue;
								from.PlaySound(0x23E);


								if (m_Tub.Charges > 1)
								{
									m_Tub.Charges -= 1;
								}

								else
								{
									m_Tub.Delete();
									from.AddToBackpack(bacvide);
									from.SendMessage("Votre bac de teinture n'a plus de charge.");

								}
							}
						}
						else if (m_Tub.AllowStatuettes && (item is MonsterStatuette || m_Tub.CanForceDye(item)))
					{
						if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
						{
							from.SendLocalizedMessage(500446); // That is too far away.
						}
						else if (!item.Movable)
						{
							from.SendLocalizedMessage(1049779); // You cannot dye statuettes that are locked down.
						}
						else
						{
							item.Hue = m_Tub.DyedHue;
							from.PlaySound(0x23E);


							if (m_Tub.Charges > 1)
							{
								m_Tub.Charges -= 1;
							}

							else
							{
								m_Tub.Delete();
								from.AddToBackpack(bacvide);
								from.SendMessage("Votre bac de teinture n'a plus de charge.");

							}
						}
					}
					else if (m_Tub.AllowLeather)
					{
					

						if ((armor != null && (armor.MaterialType == ArmorMaterialType.Leather ||
							armor.MaterialType == ArmorMaterialType.Studded)) ||
							(clothing != null && (clothing.DefaultResource == CraftResource.RegularLeather)) ||
							m_Tub.CanForceDye(item))
						{
							if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
							{
								from.SendLocalizedMessage(500446); // That is too far away.
							}
							else if (!item.Movable)
							{
								from.SendLocalizedMessage(1042419); // You may not dye leather items which are locked down.
							}
							else if (item.Parent is Mobile)
							{
								from.SendLocalizedMessage(500861); // Can't Dye clothing that is being worn.
							}
							else
							{
								item.Hue = m_Tub.DyedHue;
								from.PlaySound(0x23E);


								if (m_Tub.Charges > 1)
								{
									m_Tub.Charges -= 1;
								}

								else
								{
									m_Tub.Delete();
									from.AddToBackpack(bacvide);
									from.SendMessage("Votre bac de teinture n'a plus de charge.");
								}
							}
						}
						else
						{
							from.SendLocalizedMessage(m_Tub.FailMessage);
						}
					}
					else if (m_Tub.AllowMetal)
					{
						

                        if ((armor != null && armor.MaterialType >= ArmorMaterialType.Ringmail && armor.MaterialType <= ArmorMaterialType.Plate) || m_Tub.CanForceDye(item))
                        {
                            if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                            {
                                from.SendLocalizedMessage(500446); // That is too far away.
                            }
                            else if (!item.Movable)
                            {
                                from.SendLocalizedMessage(1080395); // You may not dye metal items which are locked down.
                            }
                            else if (item.Parent is Mobile)
                            {
                                from.SendLocalizedMessage(500861); // Can't Dye clothing that is being worn.
                            }
                            else
                            {
                                item.Hue = m_Tub.DyedHue;
                                from.PlaySound(0x23E);


								if (m_Tub.Charges > 1)
								{
									m_Tub.Charges -= 1;
								}

								else
								{
									m_Tub.Delete();
									from.AddToBackpack(bacvide);
									from.SendMessage("Votre bac de teinture n'a plus de charge.");

								}


                            }
                        }
						else if (m_Tub.AllowWeapons && (item is BaseWeapon || m_Tub.CanForceDye(item)))
						{
							if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
							{
								from.SendLocalizedMessage(500446); // That is too far away.
							}
							else if (!item.Movable)
							{
								from.SendMessage("Vous devez sélectionner une arme"); // You cannot dye runes or runebooks that are locked down.
							}
							else
							{
								item.Hue = m_Tub.DyedHue;
								from.PlaySound(0x23E);



								if (m_Tub.Charges > 1)
								{
									m_Tub.Charges -= 1;
								}

								else
								{
									m_Tub.Delete();
									from.AddToBackpack(bacvide);
									from.SendMessage("Votre bac de teinture n'a plus de charge.");

								}

							}
						}
						else
                        {
                            from.SendLocalizedMessage(m_Tub.FailMessage);
                        }

                    }
                    else
                    {
                        from.SendLocalizedMessage(m_Tub.FailMessage);
                    }
                }
                else
                {
                    from.SendLocalizedMessage(m_Tub.FailMessage);
                }
					m_Tub.m_IsInUse = false;
				}
        }
			private static readonly HashSet<Type> AllowedTypes = new HashSet<Type>
	{
		typeof(MatchLight),
		typeof(BacVide),
		typeof(PalmierWoodResourceCrate),
		typeof(BarrelStaves),
		typeof(BarrelLid),
		typeof(BarrelHoops),
		typeof(LargeFishingPole),
		typeof(PipeCourte),
		typeof(PipeLongue),
		typeof(PipeCourbee),
		typeof(Keg),
		typeof(ShortMusicStandLeft),
		typeof(TrainingDummyEastDeed),
		typeof(TrainingDummySouthDeed),
		typeof(PickpocketDipEastDeed),
		typeof(PickpocketDipSouthDeed),
		typeof(AdvancedTrainingDummySouthDeed),
		typeof(AdvancedTrainingDummyEastDeed),
		typeof(LiquorBarrel),
		typeof(Watertub),
		typeof(PlayerBBEast),
		typeof(PlayerBBSouth),
		typeof(TallMusicStandLeft),
		typeof(EasleSouth),
		typeof(ElvenPodium),
		typeof(CopyToolbox),
		typeof(TrainingSword),
		typeof(TrainingKryss),
		typeof(TrainingMace),
		typeof(TrainingDoublelames),
		typeof(ShepherdsCrook),
		typeof(QuarterStaff),
		typeof(GnarledStaff),
		typeof(Bokuto),
		typeof(Fukiya),
		typeof(Tetsubo),
		typeof(WildStaff),
		typeof(Club),
		typeof(BlackStaff),
		typeof(BatonDragonique),
		typeof(BatonErmite),
		typeof(Eterfer),
		typeof(CanneSapphire),
		typeof(Crochire),
		typeof(BatonVagabond),
		typeof(WoodenShield),
		typeof(LapHarp),
		typeof(RuneLute),
		typeof(Harp),
		typeof(Drums),
		typeof(Lute),
		typeof(Tambourine),
		typeof(TambourineTassel),
		typeof(BambooFlute),
		typeof(AudChar),
		typeof(GuitareSolo),
		typeof(HarpeLongue),
		typeof(CelloDeed),
		typeof(TrumpetDeed),
		typeof(pianomodernAddonDeed),
		typeof(pianomodern2AddonDeed),
		typeof(FireHorn),
		typeof(WoodenBox),
		typeof(SmallCrate),
		typeof(MediumCrate),
		typeof(LargeCrate),
		typeof(WoodenChest),
		typeof(PlainWoodenChest),
		typeof(OrnateWoodenChest),
		typeof(GildedWoodenChest),
		typeof(WoodenFootLocker),
		typeof(CoffreMaritime),
		typeof(FinishedWoodenChest),
		typeof(OrnateElvenChestSouthDeed),
		typeof(OrnateElvenChestEastDeed),
		typeof(RarewoodChest),
		typeof(DecorativeBox),
		typeof(Chest),
		typeof(FootStool),
		typeof(Stool),
		typeof(BambooChair),
		typeof(WoodenChair),
		typeof(FancyWoodenChairCushion),
		typeof(WoodenChairCushion),
		typeof(Throne),
		typeof(OrnateElvenChair),
		typeof(BigElvenChair),
		typeof(ElvenReadingChair),
		typeof(ElvenLoveseatSouthDeed),
		typeof(ElvenLoveseatEastDeed),
		typeof(FancyCouchEastDeed),
		typeof(FancyCouchWestDeed),
		typeof(FancyCouchSouthDeed),
		typeof(FancyCouchNorthDeed),
		typeof(Nightstand),
		typeof(WritingTable),
		typeof(LargeTable),
		typeof(YewWoodTable),
		typeof(TableNappe),
		typeof(TableNappe2),
		typeof(ComptoirNappe),
		typeof(ElegantLowTable),
		typeof(PlainLowTable),
		typeof(ShortCabinet),
		typeof(OrnateElvenTableSouthDeed),
		typeof(OrnateElvenTableEastDeed),
		typeof(FancyElvenTableSouthDeed),
		typeof(FancyElvenTableEastDeed),
		typeof(BarComptoir),
		typeof(Comptoir),
		typeof(LongTableSouthDeed),
		typeof(LongTableEastDeed),
		typeof(AlchemistTableSouthDeed),
		typeof(AlchemistTableEastDeed),
		typeof(EmptyBookcase),
		typeof(FullBookcase),
		typeof(FancyArmoire),
		typeof(Armoire),
		typeof(TallCabinet),
		typeof(RedArmoire),
		typeof(ElegantArmoire),
		typeof(MapleArmoire),
		typeof(CherryArmoire),
		typeof(ArcaneBookShelfDeedSouth),
		typeof(ArcaneBookShelfDeedEast),
		typeof(AcademicBookCase),
		typeof(ElvenWashBasinSouthWithDrawerDeed),
		typeof(ElvenWashBasinEastWithDrawerDeed),
		typeof(ElvenDresserDeedSouth),
		typeof(ElvenDresserDeedEast),
		typeof(FancyElvenArmoire),
		typeof(SimpleElvenArmoire),
		typeof(Drawer),
		typeof(FancyDrawer),
		typeof(TerMurDresserEastDeed),
		typeof(TerMurDresserSouthDeed),
		typeof(NormDresser),
		typeof(SmallBedSouthDeed),
		typeof(SmallBedEastDeed),
		typeof(LargeBedSouthDeed),
		typeof(LargeBedEastDeed),
		typeof(TallElvenBedSouthDeed),
		typeof(TallElvenBedEastDeed),
		typeof(ElvenBedSouthDeed),
		typeof(ElvenBedEastDeed),
		typeof(RedHangingLantern),
		typeof(WhiteHangingLantern),
		typeof(ShojiScreen),
		typeof(BambooScreen),
		typeof(Paravent),
		typeof(Incubator),
		typeof(ChickenCoop),
		typeof(DartBoardSouthDeed),
		typeof(DartBoardEastDeed),
		typeof(VanityDeed),
		typeof(PoteauChaine),
		typeof(ArcanistStatueSouthDeed),
		typeof(ArcanistStatueEastDeed),
		typeof(WarriorStatueSouthDeed),
		typeof(WarriorStatueEastDeed),
		typeof(SquirrelStatueSouthDeed),
		typeof(SquirrelStatueEastDeed),
		typeof(GiantReplicaAcorn),
		typeof(MountedDreadHorn),
		typeof(SewingMachineDeed),
		typeof(SpinningwheelEastDeed),
		typeof(SpinningwheelSouthDeed),
		typeof(ElvenSpinningwheelEastDeed),
		typeof(ElvenSpinningwheelSouthDeed),
		typeof(LoomEastDeed),
		typeof(LoomSouthDeed),
		typeof(DressformFront),
		typeof(DressformSide),
		typeof(FlourMillEastDeed),
		typeof(FlourMillSouthDeed),
		typeof(WaterTroughEastDeed),
		typeof(WaterTroughSouthDeed),

	typeof(PileOfBlankCandles),
	typeof(SomeBlankCandles),
	typeof(OfficialSealingWax),
	typeof(RawWaxBust),
	typeof(OrigamiButterfly),
	typeof(OrigamiSwan),
	typeof(OrigamiFrog),
	typeof(OrigamiShape),
	typeof(OrigamiSongbird),
	typeof(OrigamiFish),
	typeof(OrigamiDragon),
	typeof(OrigamiBunny)
	};

		}
}
}
