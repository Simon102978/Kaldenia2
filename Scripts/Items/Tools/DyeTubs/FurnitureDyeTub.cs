using System;
using System.Collections.Generic;

namespace Server.Items
{
    public class FurnitureDyeTub : DyeTub
    {
        private bool m_IsRewardItem;
        [Constructable]
        public FurnitureDyeTub()
        {
			Name = "Bac de Teinture (Meubles)";
			Charges = 5;
		}

        public FurnitureDyeTub(Serial serial)
            : base(serial)
        {
        }
		public override bool CanDyeItem(Item item)
		{
			return AllowedTypes.Contains(item.GetType()) ||
				   FurnitureAttribute.Check(item) ||
				   item is AddonComponent;
		}
		public override bool AllowDyables => false;
        public override bool AllowFurniture => true;
        public override int TargetMessage => 501019;// Select the furniture to dye.
        public override int FailMessage => 501021;// That is not a piece of furniture.
        public override int LabelNumber => 1041246;// Furniture Dye Tub

        private static Type[] _Dyables = new[]
        {
            typeof(PotionKeg), typeof(CustomizableSquaredDoorMatDeed), typeof(OrnateBedDeed),
            typeof(FourPostBedDeed), typeof(FormalDiningTableDeed)
        };

        public override Type[] ForcedDyables => _Dyables;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsRewardItem
        {
            get
            {
                return m_IsRewardItem;
            }
            set
            {
                m_IsRewardItem = value;
            }
        }
        public override void OnDoubleClick(Mobile from)
        {
                  base.OnDoubleClick(from);
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version

            writer.Write(m_IsRewardItem);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_IsRewardItem = reader.ReadBool();
                        break;
                    }
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

	};

	}
}

