using System;
using System.Collections;
using System.Reflection;

namespace Server.Items
{
	public enum CraftResource
	{
		None = 0,
		[Description("Cuivre mat")]
		DullCopper,
		[Description("Fer de l'ombre")]
		ShadowIron,
		//Copper,
		//Bronze,
		//Gold,
		[Description("Agapite")]
		Agapite,
		[Description("Vérite")]
		Verite,
		[Description("Valorite")]
		Valorite,
		//Mytheril,

		[Description("Fer")]
		Iron = 101,
		[Description("Bronze")]
		Bronze,
		[Description("Cuivre")]
		Copper,
		[Description("Sonne")]
		Sonne,
		[Description("Argent")]
		Argent,
		[Description("Boréale")]
		Boreale,
		[Description("Chrysteliar")]
		Chrysteliar,
		[Description("Glacias")]
		Glacias,
		[Description("Lithiar")]
		Lithiar,
		[Description("Acier")]
		Acier,
		[Description("Durian")]
		Durian,
		[Description("Equilibrum")]
		Equilibrum,
		[Description("Or")]
		Gold,
		[Description("Jolinar")]
		Jolinar,
		[Description("Justicium")]
		Justicium,
		[Description("Abyssium")]
		Abyssium,
		[Description("Bloodirium")]
		Bloodirium,
		[Description("Herbrosite")]
		Herbrosite,
		[Description("Khandarium")]
		Khandarium,
		[Description("Mytheril")]
		Mytheril,
		[Description("Sombralir")]
		Sombralir,
		[Description("Draconyr")]
		Draconyr,
		[Description("Heptazion")]
		Heptazion,
		[Description("Oceanis")]
		Oceanis,
		[Description("Brazium")]
		Brazium,
		[Description("Lunerium")]
		Lunerium,
		[Description("Marinar")]
		Marinar,
		[Description("Nostalgium")]
		Nostalgium,

		[Description("Regulier")]
		RegularLeather = 201,
		[Description("Lupus")]
		LupusLeather,
		[Description("Reptilien")]
		ReptilienLeather,
		[Description("Géant")]
		GeantLeather,
		[Description("Ophidien")]
		OphidienLeather,
		[Description("Arachnide")]
		ArachnideLeather,
		[Description("Dragonique")]
		DragoniqueLeather,
		[Description("Demoniaque")]
		DemoniaqueLeather,
		[Description("Ancien")]
		AncienLeather,

		RedScales = 301,
		YellowScales,
		BlackScales,
		GreenScales,
		WhiteScales,
		BlueScales,

		[Description("Palmier")]
		PalmierWood = 401,
		[Description("Érable")]
		ErableWood,
		[Description("Chêne")]
		CheneWood,
		[Description("Cèdre")]
		CedreWood,
		[Description("Cyprès")]
		CypresWood,
		[Description("Saule")]
		SauleWood,
		[Description("Acajou")]
		AcajouWood,
		[Description("Ébène")]
		EbeneWood,
		[Description("Amarante")]
		AmaranteWood,
		[Description("Pin")]
		PinWood,
		[Description("Ancien")]
		AncienWood,
		OakWood,
		AshWood,
		YewWood,
		Heartwood,
		Bloodwood,
		Frostwood,

		[Description("Regulier")]
		RegularBone = 501,
		[Description("Lupus")]
		LupusBone,
		[Description("Reptilien")]
		ReptilienBone,
		[Description("Géant")]
		GeantBone,
		[Description("Ophidien")]
		OphidienBone,
		[Description("Arachnide")]
		ArachnideBone,
		[Description("Dragonique")]
		DragoniqueBone,
		[Description("Demoniaque")]
		DemoniaqueBone,
		[Description("Ancien")]
		AncienBone,
		
				Fish = 601,
				AutumnDragonfish,
				BlueLobster,
				BullFish,
				CrystalFish,
				FairySalmon,
				GiantKoi,
				GreatBarracuda,
				HolyMackerel,
				LavaFish,
				ReaperFish,
				SpiderCrab,
				StoneCrab,
				SummerDragonfish,
				UnicornFish,
				YellowtailBarracuda,
				AbyssalDragonfish,
				BlackMarlin,
				BloodLobster,
				BlueMarlin,
				DreadLobster,
				DungeonPike,
				GiantSamuraiFish,
				GoldenTuna,
				Kingfish,
				LanternFish,
				RainbowFish,
				SeekerFish,
				SpringDragonfish,
				StoneFish,
				TunnelCrab,
				VoidCrab,
				VoidLobster,
				WinterDragonfish,
				ZombieFish
			}
		
	
	public enum CraftResourceType
	{
		None,
		Metal,
		Leather,
		Scales,
		Wood,
		Bone,
		Fish 
	}

	public class CraftAttributeInfo
	{
		private int m_WeaponFireDamage;
		private int m_WeaponColdDamage;
		private int m_WeaponPoisonDamage;
		private int m_WeaponEnergyDamage;
		private int m_WeaponChaosDamage;
		private int m_WeaponDirectDamage;
		private int m_WeaponDurability;
		private int m_WeaponLuck;
		private int m_WeaponGoldIncrease;
		private int m_WeaponLowerRequirements;
		private int m_WeaponDamage;
		private int m_WeaponHitChance;
		private int m_WeaponHitLifeLeech;
		private int m_WeaponRegenHits;
		private int m_WeaponSwingSpeed;

		private int m_ArmorPhysicalResist;
		private int m_ArmorFireResist;
		private int m_ArmorColdResist;
		private int m_ArmorPoisonResist;
		private int m_ArmorEnergyResist;
		private int m_ArmorDurability;
		private int m_ArmorLuck;
		private int m_ArmorGoldIncrease;
		private int m_ArmorLowerRequirements;
		private int m_ArmorDamage;
		private int m_ArmorHitChance;
		private int m_ArmorRegenHits;
		private int m_ArmorMage;

		private int m_ShieldPhysicalResist;
		private int m_ShieldFireResist;
		private int m_ShieldColdResist;
		private int m_ShieldPoisonResist;
		private int m_ShieldEnergyResist;
		private int m_ShieldPhysicalRandom;
		private int m_ShieldColdRandom;
		private int m_ShieldSpellChanneling;
		private int m_ShieldLuck;
		private int m_ShieldLowerRequirements;
		private int m_ShieldRegenHits;
		private int m_ShieldBonusDex;
		private int m_ShieldBonusStr;
		private int m_ShieldReflectPhys;
		private int m_SelfRepair;

		private int m_OtherSpellChanneling;
		private int m_OtherLuck;
		private int m_OtherRegenHits;
		private int m_OtherLowerRequirements;

		private int m_RunicMinAttributes;
		private int m_RunicMaxAttributes;
		private int m_RunicMinIntensity;
		private int m_RunicMaxIntensity;

		public int WeaponFireDamage { get { return m_WeaponFireDamage; } set { m_WeaponFireDamage = value; } }
		public int WeaponColdDamage { get { return m_WeaponColdDamage; } set { m_WeaponColdDamage = value; } }
		public int WeaponPoisonDamage { get { return m_WeaponPoisonDamage; } set { m_WeaponPoisonDamage = value; } }
		public int WeaponEnergyDamage { get { return m_WeaponEnergyDamage; } set { m_WeaponEnergyDamage = value; } }
		public int WeaponChaosDamage { get { return m_WeaponChaosDamage; } set { m_WeaponChaosDamage = value; } }
		public int WeaponDirectDamage { get { return m_WeaponDirectDamage; } set { m_WeaponDirectDamage = value; } }
		public int WeaponDurability { get { return m_WeaponDurability; } set { m_WeaponDurability = value; } }
		public int WeaponLuck { get { return m_WeaponLuck; } set { m_WeaponLuck = value; } }
		public int WeaponGoldIncrease { get { return m_WeaponGoldIncrease; } set { m_WeaponGoldIncrease = value; } }
		public int WeaponLowerRequirements { get { return m_WeaponLowerRequirements; } set { m_WeaponLowerRequirements = value; } }
		public int WeaponDamage { get { return m_WeaponDamage; } set { m_WeaponDamage = value; } }
		public int WeaponHitChance { get { return m_WeaponHitChance; } set { m_WeaponHitChance = value; } }
		public int WeaponHitLifeLeech { get { return m_WeaponHitLifeLeech; } set { m_WeaponHitLifeLeech = value; } }
		public int WeaponRegenHits { get { return m_WeaponRegenHits; } set { m_WeaponRegenHits = value; } }
		public int WeaponSwingSpeed { get { return m_WeaponSwingSpeed; } set { m_WeaponSwingSpeed = value; } }

		public int ArmorPhysicalResist { get { return m_ArmorPhysicalResist; } set { m_ArmorPhysicalResist = value; } }
		public int ArmorFireResist { get { return m_ArmorFireResist; } set { m_ArmorFireResist = value; } }
		public int ArmorColdResist { get { return m_ArmorColdResist; } set { m_ArmorColdResist = value; } }
		public int ArmorPoisonResist { get { return m_ArmorPoisonResist; } set { m_ArmorPoisonResist = value; } }
		public int ArmorEnergyResist { get { return m_ArmorEnergyResist; } set { m_ArmorEnergyResist = value; } }
		public int ArmorDurability { get { return m_ArmorDurability; } set { m_ArmorDurability = value; } }
		public int ArmorLuck { get { return m_ArmorLuck; } set { m_ArmorLuck = value; } }
		public int ArmorGoldIncrease { get { return m_ArmorGoldIncrease; } set { m_ArmorGoldIncrease = value; } }
		public int ArmorLowerRequirements { get { return m_ArmorLowerRequirements; } set { m_ArmorLowerRequirements = value; } }
		public int ArmorDamage { get { return m_ArmorDamage; } set { m_ArmorDamage = value; } }
		public int ArmorHitChance { get { return m_ArmorHitChance; } set { m_ArmorHitChance = value; } }
		public int ArmorRegenHits { get { return m_ArmorRegenHits; } set { m_ArmorRegenHits = value; } }
		public int ArmorMage { get { return m_ArmorMage; } set { m_ArmorMage = value; } }

		public int ShieldPhysicalResist { get { return m_ShieldPhysicalResist; } set { m_ShieldPhysicalResist = value; } }
		public int ShieldFireResist { get { return m_ShieldFireResist; } set { m_ShieldFireResist = value; } }
		public int ShieldColdResist { get { return m_ShieldColdResist; } set { m_ShieldColdResist = value; } }
		public int ShieldPoisonResist { get { return m_ShieldPoisonResist; } set { m_ShieldPoisonResist = value; } }
		public int ShieldEnergyResist { get { return m_ShieldEnergyResist; } set { m_ShieldEnergyResist = value; } }
		public int ShieldPhysicalRandom { get { return m_ShieldPhysicalRandom; } set { m_ShieldPhysicalRandom = value; } }
		public int ShieldColdRandom { get { return m_ShieldColdRandom; } set { m_ShieldColdRandom = value; } }
		public int ShieldSpellChanneling { get { return m_ShieldSpellChanneling; } set { m_ShieldSpellChanneling = value; } }
		public int ShieldLuck { get { return m_ShieldLuck; } set { m_ShieldLuck = value; } }
		public int ShieldLowerRequirements { get { return m_ShieldLowerRequirements; } set { m_ShieldLowerRequirements = value; } }
		public int ShieldRegenHits { get { return m_ShieldRegenHits; } set { m_ShieldRegenHits = value; } }
		public int ShieldBonusDex { get { return m_ShieldBonusDex; } set { m_ShieldBonusDex = value; } }
		public int ShieldBonusStr { get { return m_ShieldBonusStr; } set { m_ShieldBonusStr = value; } }
		public int ShieldReflectPhys { get { return m_ShieldReflectPhys; } set { m_ShieldReflectPhys = value; } }
		public int ShieldSelfRepair { get { return m_SelfRepair; } set { m_SelfRepair = value; } }

		public int OtherSpellChanneling { get { return m_OtherSpellChanneling; } set { m_OtherSpellChanneling = value; } }
		public int OtherLuck { get { return m_OtherLuck; } set { m_OtherLuck = value; } }
		public int OtherRegenHits { get { return m_OtherRegenHits; } set { m_OtherRegenHits = value; } }
		public int OtherLowerRequirements { get { return m_OtherLowerRequirements; } set { m_OtherLowerRequirements = value; } }

		public int RunicMinAttributes { get { return m_RunicMinAttributes; } set { m_RunicMinAttributes = value; } }
		public int RunicMaxAttributes { get { return m_RunicMaxAttributes; } set { m_RunicMaxAttributes = value; } }
		public int RunicMinIntensity { get { return m_RunicMinIntensity; } set { m_RunicMinIntensity = value; } }
		public int RunicMaxIntensity { get { return m_RunicMaxIntensity; } set { m_RunicMaxIntensity = value; } }

		public static readonly CraftAttributeInfo Blank;
		public static readonly CraftAttributeInfo DullCopper, ShadowIron, /*Copper, Bronze, Golden, */Agapite, Verite, Valorite/*, Mytheril*/;
		public static readonly CraftAttributeInfo Bronze, Cuivre, Sonne, Argent, Boreale, Chrysteliar, Glacias, Lithiar, Acier, Durian, Equilibrum, Or, Jolinar, Justicium, Abyssium;
		public static readonly CraftAttributeInfo Bloodirium, Herbrosite, Khandarium, Mytheril, Sombralir, Draconyr, Heptazion, Oceanis, Brazium, Lunerium, Marinar, Nostalgium;
		public static readonly CraftAttributeInfo LupusLeather, ReptilienLeather, GeantLeather, OphidienLeather, ArachnideLeather, DragoniqueLeather, DemoniaqueLeather, AncienLeather;
		public static readonly CraftAttributeInfo RedScales, YellowScales, BlackScales, GreenScales, WhiteScales, BlueScales;
		public static readonly CraftAttributeInfo OakWood, AshWood, YewWood, Heartwood, Bloodwood, Frostwood;
		public static readonly CraftAttributeInfo PalmierWood, ErableWood, CheneWood, CypresWood, CedreWood, SauleWood, AcajouWood, EbeneWood, AmaranteWood, PinWood, AncienWood;
		public static readonly CraftAttributeInfo LupusBone, ReptilienBone, GeantBone, OphidienBone, ArachnideBone, DragoniqueBone, DemoniaqueBone, AncienBone;

		static CraftAttributeInfo()
		{
			Blank = new CraftAttributeInfo();

			CraftAttributeInfo dullCopper = DullCopper = new CraftAttributeInfo();

			dullCopper.ArmorPhysicalResist = 0;
			dullCopper.ArmorDurability = 50;
			dullCopper.ArmorLowerRequirements = 20;
			dullCopper.WeaponDurability = 100;
			dullCopper.WeaponLowerRequirements = 50;
			dullCopper.RunicMinAttributes = 1;
			dullCopper.RunicMaxAttributes = 2;

			dullCopper.RunicMinIntensity = 40;
			dullCopper.RunicMaxIntensity = 100;

			CraftAttributeInfo shadowIron = ShadowIron = new CraftAttributeInfo();

			shadowIron.ArmorPhysicalResist = 0;
			shadowIron.ArmorFireResist = 2;
			shadowIron.ArmorEnergyResist = 7;
			shadowIron.ArmorDurability = 100;

			shadowIron.WeaponColdDamage = 20;
			shadowIron.WeaponDurability = 50;

			shadowIron.RunicMinAttributes = 2;
			shadowIron.RunicMaxAttributes = 2;

			shadowIron.RunicMinIntensity = 45;
			shadowIron.RunicMaxIntensity = 100;

			CraftAttributeInfo agapite = Agapite = new CraftAttributeInfo();

			agapite.ArmorPhysicalResist = 1;
			agapite.ArmorFireResist = 7;
			agapite.ArmorColdResist = 2;
			agapite.ArmorPoisonResist = 2;
			agapite.ArmorEnergyResist = 2;
			agapite.WeaponColdDamage = 30;
			agapite.WeaponEnergyDamage = 20;
			agapite.RunicMinAttributes = 4;
			agapite.RunicMaxAttributes = 4;

			agapite.RunicMinIntensity = 65;
			agapite.RunicMaxIntensity = 100;

			CraftAttributeInfo verite = Verite = new CraftAttributeInfo();

			verite.ArmorPhysicalResist = 1;
			verite.ArmorFireResist = 4;
			verite.ArmorColdResist = 3;
			verite.ArmorPoisonResist = 4;
			verite.ArmorEnergyResist = 1;
			verite.WeaponPoisonDamage = 40;
			verite.WeaponEnergyDamage = 20;
			verite.RunicMinAttributes = 4;
			verite.RunicMaxAttributes = 5;

			verite.RunicMinIntensity = 70;
			verite.RunicMaxIntensity = 100;

			CraftAttributeInfo valorite = Valorite = new CraftAttributeInfo();

			valorite.ArmorPhysicalResist = 1;
			valorite.ArmorColdResist = 4;
			valorite.ArmorPoisonResist = 4;
			valorite.ArmorEnergyResist = 4;
			valorite.ArmorDurability = 50;
			valorite.WeaponFireDamage = 10;
			valorite.WeaponColdDamage = 20;
			valorite.WeaponPoisonDamage = 10;
			valorite.WeaponEnergyDamage = 20;
			valorite.RunicMinAttributes = 5;
			valorite.RunicMaxAttributes = 5;

			valorite.RunicMinIntensity = 85;
			valorite.RunicMaxIntensity = 100;

			// Métaux

			CraftAttributeInfo bronze = Bronze = new CraftAttributeInfo();
			bronze.ArmorPhysicalResist = 2;
			bronze.ArmorColdResist = 2;
			bronze.ArmorPoisonResist = 2;
			bronze.WeaponColdDamage = 15;
			bronze.WeaponPoisonDamage = 15;

			CraftAttributeInfo cuivre = Cuivre = new CraftAttributeInfo();
			cuivre.ArmorPhysicalResist = 2;
			cuivre.ArmorFireResist = 2;
			cuivre.ArmorEnergyResist = 2;
			cuivre.WeaponFireDamage = 15;
			cuivre.WeaponEnergyDamage = 15;

			CraftAttributeInfo sonne = Sonne = new CraftAttributeInfo();
			sonne.ArmorPhysicalResist = 3;
			sonne.ArmorFireResist = 3;
			sonne.ArmorPoisonResist = 3;
			sonne.WeaponFireDamage = 25;
			sonne.WeaponPoisonDamage = 25;

			CraftAttributeInfo argent = Argent = new CraftAttributeInfo();
			argent.ArmorPhysicalResist = 3;
			argent.ArmorFireResist = 3;
			argent.ArmorEnergyResist = 3;
			argent.WeaponFireDamage = 25;
			argent.WeaponEnergyDamage = 25;

			CraftAttributeInfo boreale = Boreale = new CraftAttributeInfo();
			boreale.ArmorPhysicalResist = 3;
			boreale.ArmorFireResist = 3;
			boreale.ArmorColdResist = 3;
			boreale.WeaponFireDamage = 25;
			boreale.WeaponColdDamage = 25;

			CraftAttributeInfo chrysteliar = Chrysteliar = new CraftAttributeInfo();
			chrysteliar.ArmorPhysicalResist = 3;
			chrysteliar.ArmorColdResist = 3;
			chrysteliar.ArmorPoisonResist = 3;
			chrysteliar.WeaponColdDamage = 25;
			chrysteliar.WeaponPoisonDamage = 25;

			CraftAttributeInfo glacias = Glacias = new CraftAttributeInfo();
			glacias.ArmorPhysicalResist = 3;
			glacias.ArmorColdResist = 3;
			glacias.ArmorEnergyResist = 3;
			glacias.WeaponColdDamage = 25;
			glacias.WeaponEnergyDamage = 25;

			CraftAttributeInfo lithiar = Lithiar = new CraftAttributeInfo();
			lithiar.ArmorPhysicalResist = 3;
			lithiar.ArmorPoisonResist = 3;
			lithiar.ArmorEnergyResist = 3;
			lithiar.WeaponPoisonDamage = 25;
			lithiar.WeaponEnergyDamage = 25;

			CraftAttributeInfo acier = Acier = new CraftAttributeInfo();
			acier.ArmorPhysicalResist = 4;
			acier.ArmorFireResist = 4;
			acier.ArmorPoisonResist = 4;
			acier.WeaponFireDamage = 35;
			acier.WeaponPoisonDamage = 35;

			CraftAttributeInfo durian = Durian = new CraftAttributeInfo();
			durian.ArmorPhysicalResist = 4;
			durian.ArmorFireResist = 4;
			durian.ArmorEnergyResist = 4;
			durian.WeaponFireDamage = 35;
			durian.WeaponEnergyDamage = 35;

			CraftAttributeInfo equilibrum = Equilibrum = new CraftAttributeInfo();
			equilibrum.ArmorPhysicalResist = 4;
			equilibrum.ArmorFireResist = 4;
			equilibrum.ArmorColdResist = 4;
			equilibrum.WeaponFireDamage = 35;
			equilibrum.WeaponColdDamage = 35;

			CraftAttributeInfo or = Or = new CraftAttributeInfo();
			or.ArmorPhysicalResist = 4;
			or.ArmorColdResist = 4;
			or.ArmorPoisonResist = 4;
			or.WeaponColdDamage = 35;
			or.WeaponPoisonDamage = 35;

			CraftAttributeInfo jolinar = Jolinar = new CraftAttributeInfo();
			jolinar.ArmorPhysicalResist = 4;
			jolinar.ArmorColdResist = 4;
			jolinar.ArmorEnergyResist = 4;
			jolinar.WeaponColdDamage = 35;
			jolinar.WeaponEnergyDamage = 35;

			CraftAttributeInfo justicium = Justicium = new CraftAttributeInfo();
			justicium.ArmorPhysicalResist = 4;
			justicium.ArmorPoisonResist = 4;
			justicium.ArmorEnergyResist = 4;
			justicium.WeaponPoisonDamage = 35;
			justicium.WeaponEnergyDamage = 35;

			CraftAttributeInfo abyssium = Abyssium = new CraftAttributeInfo();
			abyssium.ArmorPhysicalResist = 5;
			abyssium.ArmorFireResist = 5;
			abyssium.ArmorPoisonResist = 5;
			abyssium.WeaponFireDamage = 45;
			abyssium.WeaponPoisonDamage = 45;

			CraftAttributeInfo bloodirium = Bloodirium = new CraftAttributeInfo();
			bloodirium.ArmorPhysicalResist = 5;
			bloodirium.ArmorFireResist = 5;
			bloodirium.ArmorEnergyResist = 5;
			bloodirium.WeaponFireDamage = 45;
			bloodirium.WeaponEnergyDamage = 45;

			CraftAttributeInfo herbrosite = Herbrosite = new CraftAttributeInfo();
			herbrosite.ArmorPhysicalResist = 5;
			herbrosite.ArmorFireResist = 5;
			herbrosite.ArmorColdResist = 5;
			herbrosite.WeaponFireDamage = 45;
			herbrosite.WeaponColdDamage = 45;

			CraftAttributeInfo khandarium = Khandarium = new CraftAttributeInfo();
			khandarium.ArmorPhysicalResist = 5;
			khandarium.ArmorColdResist = 5;
			khandarium.ArmorPoisonResist = 5;
			khandarium.WeaponColdDamage = 45;
			khandarium.WeaponPoisonDamage = 45;

			CraftAttributeInfo mytheril = Mytheril = new CraftAttributeInfo();
			mytheril.ArmorPhysicalResist = 5;
			mytheril.ArmorColdResist = 5;
			mytheril.ArmorEnergyResist = 5;
			mytheril.WeaponColdDamage = 45;
			mytheril.WeaponEnergyDamage = 45;

			CraftAttributeInfo sombralir = Sombralir = new CraftAttributeInfo();
			sombralir.ArmorPhysicalResist = 5;
			sombralir.ArmorPoisonResist = 5;
			sombralir.ArmorEnergyResist = 5;
			sombralir.WeaponPoisonDamage = 45;
			sombralir.WeaponEnergyDamage = 45;

			CraftAttributeInfo draconyr = Draconyr = new CraftAttributeInfo();
			draconyr.ArmorPhysicalResist = 6;
			draconyr.ArmorFireResist = 8;
			draconyr.ArmorColdResist = 3;
			draconyr.ArmorPoisonResist = 8;
			draconyr.ArmorEnergyResist = 3;
			draconyr.WeaponFireDamage = 55;
			draconyr.WeaponPoisonDamage = 55;

			CraftAttributeInfo heptazion = Heptazion = new CraftAttributeInfo();
			heptazion.ArmorPhysicalResist = 6;
			heptazion.ArmorFireResist = 8;
			heptazion.ArmorColdResist = 3;
			heptazion.ArmorPoisonResist = 3;
			heptazion.ArmorEnergyResist = 8;
			heptazion.WeaponFireDamage = 55;
			heptazion.WeaponEnergyDamage = 55;

			CraftAttributeInfo oceanis = Oceanis = new CraftAttributeInfo();
			oceanis.ArmorPhysicalResist = 6;
			oceanis.ArmorFireResist = 8;
			oceanis.ArmorColdResist = 8;
			oceanis.ArmorPoisonResist = 3;
			oceanis.ArmorEnergyResist = 3;
			oceanis.WeaponFireDamage = 55;
			oceanis.WeaponColdDamage = 55;

			CraftAttributeInfo brazium = Brazium = new CraftAttributeInfo();
			brazium.ArmorPhysicalResist = 6;
			brazium.ArmorFireResist = 3;
			brazium.ArmorColdResist = 8;
			brazium.ArmorPoisonResist = 8;
			brazium.ArmorEnergyResist = 3;
			brazium.WeaponColdDamage = 55;
			brazium.WeaponPoisonDamage = 55;

			CraftAttributeInfo lunerium = Lunerium = new CraftAttributeInfo();
			lunerium.ArmorPhysicalResist = 6;
			lunerium.ArmorFireResist = 3;
			lunerium.ArmorColdResist = 8;
			lunerium.ArmorPoisonResist = 3;
			lunerium.ArmorEnergyResist = 8;
			lunerium.WeaponColdDamage = 55;
			lunerium.WeaponEnergyDamage = 55;

			CraftAttributeInfo marinar = Marinar = new CraftAttributeInfo();
			marinar.ArmorPhysicalResist = 6;
			marinar.ArmorFireResist = 3;
			marinar.ArmorColdResist = 3;
			marinar.ArmorPoisonResist = 8;
			marinar.ArmorEnergyResist = 8;
			marinar.WeaponPoisonDamage = 55;
			marinar.WeaponEnergyDamage = 55;

			CraftAttributeInfo nostalgium = Nostalgium = new CraftAttributeInfo();
			nostalgium.ArmorPhysicalResist = 7;
			nostalgium.ArmorFireResist = 9;
			nostalgium.ArmorColdResist = 9;
			nostalgium.ArmorPoisonResist = 9;
			nostalgium.ArmorEnergyResist = 9;
			nostalgium.WeaponFireDamage = 25;
			nostalgium.WeaponColdDamage = 25;
			nostalgium.WeaponPoisonDamage = 25;
			nostalgium.WeaponEnergyDamage = 25;
			nostalgium.WeaponDamage = 25;

			// Cuirs

			CraftAttributeInfo lupusLeather = LupusLeather = new CraftAttributeInfo();
			lupusLeather.ArmorPhysicalResist = 1;
			lupusLeather.ArmorDurability = 40;
			lupusLeather.ArmorLowerRequirements = 15;
			lupusLeather.WeaponDurability = 80;
			lupusLeather.WeaponLowerRequirements = 40;
			lupusLeather.RunicMinAttributes = 1;
			lupusLeather.RunicMaxAttributes = 2;
			lupusLeather.RunicMinIntensity = 35;
			lupusLeather.RunicMaxIntensity = 70;

			CraftAttributeInfo reptilienLeather = ReptilienLeather = new CraftAttributeInfo();
			reptilienLeather.ArmorPhysicalResist = 1;
			reptilienLeather.ArmorFireResist = 2;
			reptilienLeather.ArmorEnergyResist = 6;
			reptilienLeather.ArmorDurability = 90;
			reptilienLeather.WeaponColdDamage = 20;
			reptilienLeather.WeaponDurability = 50;
			reptilienLeather.RunicMinAttributes = 1;
			reptilienLeather.RunicMaxAttributes = 2;
			reptilienLeather.RunicMinIntensity = 40;
			reptilienLeather.RunicMaxIntensity = 75;

			CraftAttributeInfo geantLeather = GeantLeather = new CraftAttributeInfo();
			geantLeather.ArmorPhysicalResist = 2;
			geantLeather.ArmorFireResist = 2;
			geantLeather.ArmorPoisonResist = 6;
			geantLeather.ArmorEnergyResist = 2;
			geantLeather.WeaponPoisonDamage = 10;
			geantLeather.WeaponEnergyDamage = 20;
			geantLeather.RunicMinAttributes = 2;
			geantLeather.RunicMaxAttributes = 2;
			geantLeather.RunicMinIntensity = 45;
			geantLeather.RunicMaxIntensity = 80;

			CraftAttributeInfo ophidienLeather = OphidienLeather = new CraftAttributeInfo();
			ophidienLeather.ArmorPhysicalResist = 2;
			ophidienLeather.ArmorColdResist = 6;
			ophidienLeather.ArmorPoisonResist = 2;
			ophidienLeather.ArmorEnergyResist = 2;
			ophidienLeather.WeaponFireDamage = 35;
			ophidienLeather.RunicMinAttributes = 2;
			ophidienLeather.RunicMaxAttributes = 3;
			ophidienLeather.RunicMinIntensity = 50;
			ophidienLeather.RunicMaxIntensity = 85;

			CraftAttributeInfo arachnideLeather = ArachnideLeather = new CraftAttributeInfo();
			arachnideLeather.ArmorPhysicalResist = 2;
			arachnideLeather.ArmorFireResist = 2;
			arachnideLeather.ArmorColdResist = 2;
			arachnideLeather.ArmorEnergyResist = 3;
			arachnideLeather.ArmorLuck = 35;
			arachnideLeather.ArmorLowerRequirements = 30;
			arachnideLeather.WeaponLuck = 35;
			arachnideLeather.WeaponLowerRequirements = 45;
			arachnideLeather.RunicMinAttributes = 2;
			arachnideLeather.RunicMaxAttributes = 3;
			arachnideLeather.RunicMinIntensity = 55;
			arachnideLeather.RunicMaxIntensity = 90;

			CraftAttributeInfo dragoniqueLeather = DragoniqueLeather = new CraftAttributeInfo();
			dragoniqueLeather.ArmorPhysicalResist = 3;
			dragoniqueLeather.ArmorFireResist = 6;
			dragoniqueLeather.ArmorColdResist = 2;
			dragoniqueLeather.ArmorPoisonResist = 2;
			dragoniqueLeather.ArmorEnergyResist = 2;
			dragoniqueLeather.WeaponColdDamage = 30;
			dragoniqueLeather.WeaponEnergyDamage = 20;
			dragoniqueLeather.RunicMinAttributes = 3;
			dragoniqueLeather.RunicMaxAttributes = 3;
			dragoniqueLeather.RunicMinIntensity = 60;
			dragoniqueLeather.RunicMaxIntensity = 90;

			CraftAttributeInfo demoniaqueLeather = DemoniaqueLeather = new CraftAttributeInfo();
			demoniaqueLeather.ArmorPhysicalResist = 3;
			demoniaqueLeather.ArmorFireResist = 4;
			demoniaqueLeather.ArmorColdResist = 3;
			demoniaqueLeather.ArmorPoisonResist = 4;
			demoniaqueLeather.ArmorEnergyResist = 1;
			demoniaqueLeather.WeaponPoisonDamage = 40;
			demoniaqueLeather.WeaponEnergyDamage = 20;
			demoniaqueLeather.RunicMinAttributes = 3;
			demoniaqueLeather.RunicMaxAttributes = 4;
			demoniaqueLeather.RunicMinIntensity = 65;
			demoniaqueLeather.RunicMaxIntensity = 95;

			CraftAttributeInfo ancienLeather = AncienLeather = new CraftAttributeInfo();
			ancienLeather.ArmorPhysicalResist = 4;
			ancienLeather.ArmorFireResist = 4;
			ancienLeather.ArmorColdResist = 4;
			ancienLeather.ArmorPoisonResist = 4;
			ancienLeather.ArmorEnergyResist = 4;
			ancienLeather.ArmorDurability = 60;
			ancienLeather.WeaponFireDamage = 15;
			ancienLeather.WeaponColdDamage = 25;
			ancienLeather.WeaponPoisonDamage = 15;
			ancienLeather.WeaponEnergyDamage = 25;
			ancienLeather.RunicMinAttributes = 4;
			ancienLeather.RunicMaxAttributes = 4;
			ancienLeather.RunicMinIntensity = 80;
			ancienLeather.RunicMaxIntensity = 95;


			// Os

			CraftAttributeInfo lupusBone = LupusBone = new CraftAttributeInfo();
			lupusBone.ArmorPhysicalResist = 2;
			lupusBone.ArmorDurability = 55;
			lupusBone.ArmorLowerRequirements = 20;
			lupusBone.WeaponDurability = 100;
			lupusBone.WeaponLowerRequirements = 50;
			lupusBone.RunicMinAttributes = 1;
			lupusBone.RunicMaxAttributes = 2;
			lupusBone.RunicMinIntensity = 40;
			lupusBone.RunicMaxIntensity = 80;

			CraftAttributeInfo reptilienBone = ReptilienBone = new CraftAttributeInfo();
			reptilienBone.ArmorPhysicalResist = 3;
			reptilienBone.ArmorFireResist = 2;
			reptilienBone.ArmorEnergyResist = 7;
			reptilienBone.ArmorDurability = 100;
			reptilienBone.WeaponColdDamage = 20;
			reptilienBone.WeaponDurability = 55;
			reptilienBone.RunicMinAttributes = 2;
			reptilienBone.RunicMaxAttributes = 2;
			reptilienBone.RunicMinIntensity = 45;
			reptilienBone.RunicMaxIntensity = 85;

			CraftAttributeInfo geantBone = GeantBone = new CraftAttributeInfo();
			geantBone.ArmorPhysicalResist = 3;
			geantBone.ArmorFireResist = 2;
			geantBone.ArmorPoisonResist = 7;
			geantBone.ArmorEnergyResist = 2;
			geantBone.WeaponPoisonDamage = 10;
			geantBone.WeaponEnergyDamage = 20;
			geantBone.RunicMinAttributes = 2;
			geantBone.RunicMaxAttributes = 3;
			geantBone.RunicMinIntensity = 50;
			geantBone.RunicMaxIntensity = 85;

			CraftAttributeInfo ophidienBone = OphidienBone = new CraftAttributeInfo();
			ophidienBone.ArmorPhysicalResist = 4;
			ophidienBone.ArmorColdResist = 7;
			ophidienBone.ArmorPoisonResist = 2;
			ophidienBone.ArmorEnergyResist = 2;
			ophidienBone.WeaponFireDamage = 40;
			ophidienBone.RunicMinAttributes = 3;
			ophidienBone.RunicMaxAttributes = 3;
			ophidienBone.RunicMinIntensity = 55;
			ophidienBone.RunicMaxIntensity = 90;

			CraftAttributeInfo arachnideBone = ArachnideBone = new CraftAttributeInfo();
			arachnideBone.ArmorPhysicalResist = 3;
			arachnideBone.ArmorFireResist = 2;
			arachnideBone.ArmorColdResist = 2;
			arachnideBone.ArmorEnergyResist = 3;
			arachnideBone.ArmorLuck = 40;
			arachnideBone.ArmorLowerRequirements = 30;
			arachnideBone.WeaponLuck = 40;
			arachnideBone.WeaponLowerRequirements = 50;
			arachnideBone.RunicMinAttributes = 3;
			arachnideBone.RunicMaxAttributes = 4;
			arachnideBone.RunicMinIntensity = 60;
			arachnideBone.RunicMaxIntensity = 90;

			CraftAttributeInfo dragoniqueBone = DragoniqueBone = new CraftAttributeInfo();
			dragoniqueBone.ArmorPhysicalResist = 4;
			dragoniqueBone.ArmorFireResist = 7;
			dragoniqueBone.ArmorColdResist = 2;
			dragoniqueBone.ArmorPoisonResist = 2;
			dragoniqueBone.ArmorEnergyResist = 2;
			dragoniqueBone.WeaponColdDamage = 30;
			dragoniqueBone.WeaponEnergyDamage = 20;
			dragoniqueBone.RunicMinAttributes = 4;
			dragoniqueBone.RunicMaxAttributes = 4;
			dragoniqueBone.RunicMinIntensity = 65;
			dragoniqueBone.RunicMaxIntensity = 95;

			CraftAttributeInfo demoniaqueBone = DemoniaqueBone = new CraftAttributeInfo();
			demoniaqueBone.ArmorPhysicalResist = 5;
			demoniaqueBone.ArmorFireResist = 4;
			demoniaqueBone.ArmorColdResist = 3;
			demoniaqueBone.ArmorPoisonResist = 4;
			demoniaqueBone.ArmorEnergyResist = 1;
			demoniaqueBone.WeaponPoisonDamage = 40;
			demoniaqueBone.WeaponEnergyDamage = 20;
			demoniaqueBone.RunicMinAttributes = 4;
			demoniaqueBone.RunicMaxAttributes = 5;
			demoniaqueBone.RunicMinIntensity = 70;
			demoniaqueBone.RunicMaxIntensity = 95;

			CraftAttributeInfo ancienBone = AncienBone = new CraftAttributeInfo();
			ancienBone.ArmorPhysicalResist = 6;
			ancienBone.ArmorFireResist = 5;
			ancienBone.ArmorColdResist = 5;
			ancienBone.ArmorPoisonResist = 5;
			ancienBone.ArmorEnergyResist = 5;
			ancienBone.ArmorDurability = 70;
			ancienBone.WeaponFireDamage = 20;
			ancienBone.WeaponColdDamage = 30;
			ancienBone.WeaponPoisonDamage = 20;
			ancienBone.WeaponEnergyDamage = 30;
			ancienBone.RunicMinAttributes = 5;
			ancienBone.RunicMaxAttributes = 5;
			ancienBone.RunicMinIntensity = 85;
			ancienBone.RunicMaxIntensity = 100;


			// Bois
			CraftAttributeInfo erableWood = ErableWood = new CraftAttributeInfo();

			erableWood.ArmorPhysicalResist = 1;

			CraftAttributeInfo cheneWood = CheneWood = new CraftAttributeInfo();

			cheneWood.ArmorPhysicalResist = 2;
			cheneWood.ArmorPoisonResist = 2;
			cheneWood.ArmorColdResist = 2;

			cheneWood.WeaponPoisonDamage = 20;
			cheneWood.WeaponColdDamage = 20;

			CraftAttributeInfo cedreWood = CedreWood = new CraftAttributeInfo();

			cedreWood.ArmorPhysicalResist = 2;
			cedreWood.ArmorColdResist = 2;
			cedreWood.ArmorEnergyResist = 2;

			cedreWood.WeaponPoisonDamage = 20;
			cedreWood.WeaponEnergyDamage = 20;

			CraftAttributeInfo cypresWood = CypresWood = new CraftAttributeInfo();

			cypresWood.ArmorPhysicalResist = 3;
			cypresWood.ArmorFireResist = 3;
			cypresWood.ArmorPoisonResist = 3;

			cypresWood.WeaponFireDamage = 30;
			cypresWood.WeaponPoisonDamage = 30;

			CraftAttributeInfo sauleWood = SauleWood = new CraftAttributeInfo();

			sauleWood.ArmorPhysicalResist = 3;
			sauleWood.ArmorFireResist = 3;
			sauleWood.ArmorEnergyResist = 3;

			sauleWood.WeaponFireDamage = 30;
			sauleWood.WeaponEnergyDamage = 30;

			CraftAttributeInfo pinWood = PinWood = new CraftAttributeInfo();

			pinWood.ArmorPhysicalResist = 4;
			pinWood.ArmorPoisonResist = 4;
			pinWood.ArmorColdResist = 4;

			pinWood.WeaponPoisonDamage = 40;
			pinWood.WeaponColdDamage = 40;

			CraftAttributeInfo amaranteWood = AmaranteWood = new CraftAttributeInfo();

			amaranteWood.ArmorPhysicalResist = 4;
			amaranteWood.ArmorFireResist = 4;
			amaranteWood.ArmorEnergyResist = 4;

			amaranteWood.WeaponFireDamage = 40;
			amaranteWood.WeaponEnergyDamage = 40;

			CraftAttributeInfo acajouWood = AcajouWood = new CraftAttributeInfo();

			acajouWood.ArmorPhysicalResist = 5;
			acajouWood.ArmorColdResist = 5;
			acajouWood.ArmorPoisonResist = 5;

			acajouWood.WeaponColdDamage = 50;
			acajouWood.WeaponPoisonDamage = 50;

			CraftAttributeInfo ebeneWood = EbeneWood = new CraftAttributeInfo();

			ebeneWood.ArmorPhysicalResist = 5;
			ebeneWood.ArmorFireResist = 5;
			ebeneWood.ArmorEnergyResist = 5;

			ebeneWood.WeaponFireDamage = 50;
			ebeneWood.WeaponEnergyDamage = 50;

			CraftAttributeInfo ancienWood = AncienWood = new CraftAttributeInfo();

			ancienWood.ArmorPhysicalResist = 6;
			ancienWood.ArmorFireResist = 8;
			ancienWood.ArmorColdResist = 8;
			ancienWood.ArmorPoisonResist = 8;
			ancienWood.ArmorEnergyResist = 8;

			ancienWood.WeaponFireDamage = 20;
			ancienWood.WeaponColdDamage = 20;
			ancienWood.WeaponPoisonDamage = 20;
			ancienWood.WeaponEnergyDamage = 20;
			ancienWood.WeaponDamage = 20;

			//Scales

			CraftAttributeInfo red = RedScales = new CraftAttributeInfo();
			red.ArmorPhysicalResist = 1;
			red.ArmorFireResist = 11;
			red.ArmorColdResist = -3;
			red.ArmorPoisonResist = 1;
			red.ArmorEnergyResist = 1;

			CraftAttributeInfo yellow = YellowScales = new CraftAttributeInfo();

			yellow.ArmorPhysicalResist = -3;
			yellow.ArmorFireResist = 1;
			yellow.ArmorColdResist = 1;
			yellow.ArmorPoisonResist = 1;
			yellow.ArmorPoisonResist = 1;
			yellow.ArmorLuck = 20;

			CraftAttributeInfo black = BlackScales = new CraftAttributeInfo();

			black.ArmorPhysicalResist = 11;
			black.ArmorEnergyResist = -3;
			black.ArmorFireResist = 1;
			black.ArmorPoisonResist = 1;
			black.ArmorColdResist = 1;

			CraftAttributeInfo green = GreenScales = new CraftAttributeInfo();

			green.ArmorFireResist = -3;
			green.ArmorPhysicalResist = 1;
			green.ArmorColdResist = 1;
			green.ArmorEnergyResist = 1;
			green.ArmorPoisonResist = 11;

			CraftAttributeInfo white = WhiteScales = new CraftAttributeInfo();

			white.ArmorPhysicalResist = -3;
			white.ArmorFireResist = 1;
			white.ArmorEnergyResist = 1;
			white.ArmorPoisonResist = 1;
			white.ArmorColdResist = 11;

			CraftAttributeInfo blue = BlueScales = new CraftAttributeInfo();

			blue.ArmorPhysicalResist = 1;
			blue.ArmorFireResist = 1;
			blue.ArmorColdResist = 1;
			blue.ArmorPoisonResist = -3;
			blue.ArmorEnergyResist = 11;

			#region Mondain's Legacy
			CraftAttributeInfo oak = OakWood = new CraftAttributeInfo();

			oak.ArmorPhysicalResist = 3;
			oak.ArmorFireResist = 3;
			oak.ArmorPoisonResist = 2;
			oak.ArmorEnergyResist = 3;
			oak.ArmorLuck = 40;

			oak.ShieldPhysicalResist = 1;
			oak.ShieldFireResist = 1;
			oak.ShieldColdResist = 1;
			oak.ShieldPoisonResist = 1;
			oak.ShieldEnergyResist = 1;

			oak.WeaponLuck = 40;
			oak.WeaponDamage = 5;

			oak.RunicMinAttributes = 1;
			oak.RunicMaxAttributes = 2;
			oak.RunicMinIntensity = 1;
			oak.RunicMaxIntensity = 50;

			CraftAttributeInfo ash = AshWood = new CraftAttributeInfo();

			ash.ArmorPhysicalResist = 2;
			ash.ArmorColdResist = 4;
			ash.ArmorPoisonResist = 1;
			ash.ArmorEnergyResist = 6;
			ash.ArmorLowerRequirements = 20;

			ash.ShieldEnergyResist = 3;
			ash.ShieldLowerRequirements = 3;

			ash.WeaponSwingSpeed = 10;
			ash.WeaponLowerRequirements = 20;

			ash.OtherLowerRequirements = 20;

			ash.RunicMinAttributes = 2;
			ash.RunicMaxAttributes = 3;
			ash.RunicMinIntensity = 35;
			ash.RunicMaxIntensity = 75;

			CraftAttributeInfo yew = YewWood = new CraftAttributeInfo();

			yew.ArmorPhysicalResist = 6;
			yew.ArmorFireResist = 3;
			yew.ArmorColdResist = 3;
			yew.ArmorEnergyResist = 3;
			yew.ArmorRegenHits = 1;

			yew.ShieldPhysicalResist = 3;
			yew.ShieldRegenHits = 1;

			yew.WeaponHitChance = 5;
			yew.WeaponDamage = 10;

			yew.OtherRegenHits = 2;

			yew.RunicMinAttributes = 3;
			yew.RunicMaxAttributes = 3;
			yew.RunicMinIntensity = 40;
			yew.RunicMaxIntensity = 90;

			CraftAttributeInfo heartwood = Heartwood = new CraftAttributeInfo();

			heartwood.ArmorPhysicalResist = 2;
			heartwood.ArmorFireResist = 3;
			heartwood.ArmorColdResist = 2;
			heartwood.ArmorPoisonResist = 7;
			heartwood.ArmorEnergyResist = 2;

			// one of below
			heartwood.ArmorDamage = 10;
			heartwood.ArmorHitChance = 5;
			heartwood.ArmorLuck = 40;
			heartwood.ArmorLowerRequirements = 20;
			heartwood.ArmorMage = 1;

			// one of below
			heartwood.WeaponDamage = 10;
			heartwood.WeaponHitChance = 5;
			heartwood.WeaponHitLifeLeech = 13;
			heartwood.WeaponLuck = 40;
			heartwood.WeaponLowerRequirements = 20;
			heartwood.WeaponSwingSpeed = 10;

			heartwood.ShieldBonusDex = 2;
			heartwood.ShieldBonusStr = 2;
			heartwood.ShieldPhysicalRandom = 5;
			heartwood.ShieldReflectPhys = 5;
			heartwood.ShieldSelfRepair = 2;
			heartwood.ShieldColdRandom = 3;
			heartwood.ShieldSpellChanneling = 1;

			heartwood.RunicMinAttributes = 4;
			heartwood.RunicMaxAttributes = 4;
			heartwood.RunicMinIntensity = 50;
			heartwood.RunicMaxIntensity = 100;

			CraftAttributeInfo bloodwood = Bloodwood = new CraftAttributeInfo();

			bloodwood.ArmorPhysicalResist = 3;
			bloodwood.ArmorFireResist = 8;
			bloodwood.ArmorColdResist = 1;
			bloodwood.ArmorPoisonResist = 3;
			bloodwood.ArmorEnergyResist = 3;
			bloodwood.ArmorRegenHits = 2;

			bloodwood.ShieldFireResist = 3;
			bloodwood.ShieldLuck = 40;
			bloodwood.ShieldRegenHits = 2;

			bloodwood.WeaponRegenHits = 2;
			bloodwood.WeaponHitLifeLeech = 16;

			bloodwood.OtherLuck = 20;
			bloodwood.OtherRegenHits = 2;

			CraftAttributeInfo frostwood = Frostwood = new CraftAttributeInfo();

			frostwood.ArmorPhysicalResist = 2;
			frostwood.ArmorFireResist = 1;
			frostwood.ArmorColdResist = 8;
			frostwood.ArmorPoisonResist = 3;
			frostwood.ArmorEnergyResist = 4;

			frostwood.ShieldColdResist = 3;
			frostwood.ShieldSpellChanneling = 1;

			frostwood.WeaponColdDamage = 40;
			frostwood.WeaponDamage = 12;

			frostwood.OtherSpellChanneling = 1;
			#endregion
		}
	}

	public class CraftResourceInfo
	{
		public int Hue { get; set; }
		public int Number { get; set; }
		public string Name { get; set; }
		public int Level { get; set; }
		public CraftAttributeInfo AttributeInfo { get; set; }
		public CraftResource Resource { get; set; }
		public Type[] ResourceTypes { get; set; }

		public CraftResourceInfo(int hue, int number, string name, int level, CraftAttributeInfo attributeInfo, CraftResource resource, params Type[] resourceTypes)
		{
			Hue = hue;
			Number = number;
			Name = name;
			Level = level;
			AttributeInfo = attributeInfo;
			Resource = resource;
			ResourceTypes = resourceTypes;

			for (int i = 0; i < resourceTypes.Length; ++i)
				CraftResources.RegisterType(resourceTypes[i], resource);
		}
	}

	public class CraftResources
	{
		private static readonly CraftResourceInfo[] m_MetalInfo = new[]
		{
			new CraftResourceInfo( 0,       0, "Fer",           0, CraftAttributeInfo.Blank,        CraftResource.Iron,         typeof( IronIngot ),        typeof( IronOre ) ),
			new CraftResourceInfo( 1122,    0, "Bronze",        0, CraftAttributeInfo.Bronze,       CraftResource.Bronze,       typeof( BronzeIngot ),      typeof( BronzeOre ) ),
			new CraftResourceInfo( 1134,    0, "Cuivre",        0, CraftAttributeInfo.Cuivre,       CraftResource.Copper,       typeof( CopperIngot ),      typeof( CopperOre ) ),
			new CraftResourceInfo( 1124,    0, "Sonne",         1, CraftAttributeInfo.Sonne,        CraftResource.Sonne,        typeof( SonneIngot ),       typeof( SonneOre ) ),
			new CraftResourceInfo( 2500,    0, "Argent",        1, CraftAttributeInfo.Argent,       CraftResource.Argent,       typeof( ArgentIngot ),      typeof( ArgentOre ) ),
			new CraftResourceInfo( 2731,    0, "Boréale",       1, CraftAttributeInfo.Boreale,      CraftResource.Boreale,      typeof( BorealeIngot ),     typeof( BorealeOre ) ),
			new CraftResourceInfo( 1759,    0, "Chrysteliar",   1, CraftAttributeInfo.Chrysteliar,  CraftResource.Chrysteliar,  typeof( ChrysteliarIngot ), typeof( ChrysteliarOre ) ),
			new CraftResourceInfo( 1365,    0, "Glacias",       1, CraftAttributeInfo.Glacias,      CraftResource.Glacias,      typeof( GlaciasIngot ),     typeof( GlaciasOre ) ),
			new CraftResourceInfo( 1448,    0, "Lithiar",       1, CraftAttributeInfo.Lithiar,      CraftResource.Lithiar,      typeof( LithiarIngot ),     typeof( LithiarOre ) ),
			new CraftResourceInfo( 1102,    0, "Acier",         2, CraftAttributeInfo.Acier,        CraftResource.Acier,        typeof( AcierIngot ),       typeof( AcierOre ) ),
			new CraftResourceInfo( 1160,    0, "Durian",        2, CraftAttributeInfo.Durian,       CraftResource.Durian,       typeof( DurianIngot ),      typeof( DurianOre ) ),
			new CraftResourceInfo( 2212,    0, "Equilibrum",    2, CraftAttributeInfo.Equilibrum,   CraftResource.Equilibrum,   typeof( EquilibrumIngot ),  typeof( EquilibrumOre ) ),
			new CraftResourceInfo( 2886,    0, "Or",            2, CraftAttributeInfo.Or,           CraftResource.Gold,         typeof( GoldIngot ),        typeof( GoldOre ) ),
			new CraftResourceInfo( 2205,    0, "Jolinar",       2, CraftAttributeInfo.Jolinar,      CraftResource.Jolinar,      typeof( JolinarIngot ),     typeof( JolinarOre ) ),
			new CraftResourceInfo( 2219,    0, "Justicium",     2, CraftAttributeInfo.Justicium,    CraftResource.Justicium,    typeof( JusticiumIngot ),   typeof( JusticiumOre ) ),
			new CraftResourceInfo( 1800,    0, "Abyssium",      3, CraftAttributeInfo.Abyssium,     CraftResource.Abyssium,     typeof( AbyssiumIngot ),    typeof( AbyssiumOre ) ),
			new CraftResourceInfo( 2299,    0, "Bloodirium",    3, CraftAttributeInfo.Bloodirium,   CraftResource.Bloodirium,   typeof( BloodiriumIngot ),  typeof( BloodiriumOre ) ),
			new CraftResourceInfo( 2831,    0, "Herbrosite",    3, CraftAttributeInfo.Herbrosite,   CraftResource.Herbrosite,   typeof( HerbrositeIngot ),  typeof( HerbrositeOre ) ),
			new CraftResourceInfo( 1746,    0, "Khandarium",    3, CraftAttributeInfo.Khandarium,   CraftResource.Khandarium,   typeof( KhandariumIngot ),  typeof( KhandariumOre ) ),
			new CraftResourceInfo( 2432,    0, "Mytheril",      3, CraftAttributeInfo.Mytheril,     CraftResource.Mytheril,     typeof( MytherilIngot ),    typeof( MytherilOre ) ),
			new CraftResourceInfo( 2856,    0, "Sombralir",     3, CraftAttributeInfo.Sombralir,    CraftResource.Sombralir,    typeof( SombralirIngot ),   typeof( SombralirOre ) ),
			new CraftResourceInfo( 1411,    0, "Draconyr",      4, CraftAttributeInfo.Draconyr,     CraftResource.Draconyr,     typeof( DraconyrIngot ),    typeof( DraconyrOre ) ),
			new CraftResourceInfo( 2130,    0, "Heptazion",     4, CraftAttributeInfo.Heptazion,    CraftResource.Heptazion,    typeof( HeptazionIngot ),   typeof( HeptazionOre ) ),
			new CraftResourceInfo( 2591,    0, "Oceanis",       4, CraftAttributeInfo.Oceanis,      CraftResource.Oceanis,      typeof( OceanisIngot ),     typeof( OceanisOre ) ),
			new CraftResourceInfo( 1509,    0, "Brazium",       4, CraftAttributeInfo.Brazium,      CraftResource.Brazium,      typeof( BraziumIngot ),     typeof( BraziumOre ) ),
			new CraftResourceInfo( 2656,    0, "Lunerium",      4, CraftAttributeInfo.Lunerium,     CraftResource.Lunerium,     typeof( LuneriumIngot ),    typeof( LuneriumOre ) ),
			new CraftResourceInfo( 2246,    0, "Marinar",       4, CraftAttributeInfo.Marinar,      CraftResource.Marinar,      typeof( MarinarIngot ),     typeof( MarinarOre ) ),
			new CraftResourceInfo( 1940,    0, "Nostalgium",    5, CraftAttributeInfo.Nostalgium,   CraftResource.Nostalgium,   typeof( NostalgiumIngot ),  typeof( NostalgiumOre ) ),

			new CraftResourceInfo(0x973, 0, "Dull Copper",    99, CraftAttributeInfo.DullCopper,    CraftResource.DullCopper, typeof(DullCopperIngot),  typeof(DullCopperOre),  typeof(DullCopperGranite)),
			new CraftResourceInfo(0x966, 0, "Shadow Iron",    99, CraftAttributeInfo.ShadowIron,    CraftResource.ShadowIron, typeof(ShadowIronIngot),  typeof(ShadowIronOre),  typeof(ShadowIronGranite)),
			new CraftResourceInfo(1980,  0, "Agapite",        99, CraftAttributeInfo.Agapite,       CraftResource.Agapite, typeof(AgapiteIngot), typeof(AgapiteOre), typeof(AgapiteGranite)),
			new CraftResourceInfo(2841,  0, "Verite",         99, CraftAttributeInfo.Verite,        CraftResource.Verite, typeof(VeriteIngot), typeof(VeriteOre), typeof(VeriteGranite)),
			new CraftResourceInfo(2867,  0, "Valorite",       99, CraftAttributeInfo.Valorite,      CraftResource.Valorite, typeof(ValoriteIngot),  typeof(ValoriteOre), typeof(ValoriteGranite)),
		};

		private static readonly CraftResourceInfo[] m_ScaleInfo = new[]
		{
			new CraftResourceInfo(0x66D, 0, "Red Scales",       0, CraftAttributeInfo.RedScales, CraftResource.RedScales, typeof(RedScales)),
			new CraftResourceInfo(0x8A8, 0, "Yellow Scales",  1, CraftAttributeInfo.YellowScales,    CraftResource.YellowScales, typeof(YellowScales)),
			new CraftResourceInfo(0x455, 0, "Black Scales",   2, CraftAttributeInfo.BlackScales, CraftResource.BlackScales, typeof(BlackScales)),
			new CraftResourceInfo(0x851, 0, "Green Scales",   3, CraftAttributeInfo.GreenScales, CraftResource.GreenScales, typeof(GreenScales)),
			new CraftResourceInfo(0x8FD, 0, "White Scales",   4, CraftAttributeInfo.WhiteScales, CraftResource.WhiteScales, typeof(WhiteScales)),
			new CraftResourceInfo(0x8B0, 0, "Blue Scales",    5, CraftAttributeInfo.BlueScales, CraftResource.BlueScales, typeof(BlueScales)),
		};

		private static readonly CraftResourceInfo[] m_AOSLeatherInfo = new[]
		 {
			new CraftResourceInfo( 0000, 0, "Normal", 0,	 CraftAttributeInfo.Blank, CraftResource.RegularLeather, typeof(Leather), typeof(Hides)),
			new CraftResourceInfo( 1106, 0, "Lupus",  1,	 CraftAttributeInfo.LupusLeather,            CraftResource.LupusLeather,     typeof( LupusLeather ),     typeof( LupusHides ) ),
			new CraftResourceInfo( 1438, 0, "Reptilien", 1,  CraftAttributeInfo.ReptilienLeather,      CraftResource.ReptilienLeather, typeof( ReptilienLeather ), typeof( ReptilienHides ) ),
			new CraftResourceInfo( 1711, 0, "Geant", 2,		 CraftAttributeInfo.GeantLeather,          CraftResource.GeantLeather,     typeof( GeantLeather ),     typeof( GeantHides ) ),
			new CraftResourceInfo( 2494, 0, "Ophidien", 2,   CraftAttributeInfo.OphidienLeather,       CraftResource.OphidienLeather,  typeof( OphidienLeather ),  typeof( OphidienHides ) ),
			new CraftResourceInfo( 2143, 0, "Arachnide", 3,  CraftAttributeInfo.ArachnideLeather,      CraftResource.ArachnideLeather, typeof( ArachnideLeather ), typeof( ArachnideHides ) ),
			new CraftResourceInfo( 2132, 0, "Dragonique", 3, CraftAttributeInfo.DragoniqueLeather,     CraftResource.DragoniqueLeather,typeof( DragoniqueLeather ),typeof( DragoniqueHides ) ),
			new CraftResourceInfo( 2667, 0, "Demoniaque", 4, CraftAttributeInfo.DemoniaqueLeather,     CraftResource.DemoniaqueLeather,typeof( DemoniaqueLeather ),typeof( DemoniaqueHides ) ),
			new CraftResourceInfo( 1798, 0, "Ancien", 5,	 CraftAttributeInfo.AncienLeather,         CraftResource.AncienLeather,    typeof( AncienLeather ),    typeof( AncienHides ) ),


		};

		private static readonly CraftResourceInfo[] m_BoneInfo = new[]
	   {
			new CraftResourceInfo( 0000, 0, "Normal", 0, CraftAttributeInfo.Blank, CraftResource.RegularBone, typeof(Bone)),
			new CraftResourceInfo( 1106, 0, "Lupus", 1, CraftAttributeInfo.LupusBone,            CraftResource.LupusBone,     typeof( LupusBone )),
			new CraftResourceInfo( 1438, 0, "Reptilien", 1, CraftAttributeInfo.ReptilienBone,      CraftResource.ReptilienBone, typeof( ReptilienBone ) ),
			new CraftResourceInfo( 1711, 0, "Geant", 2, CraftAttributeInfo.GeantBone,          CraftResource.GeantBone,     typeof( GeantBone ) ),
			new CraftResourceInfo( 2494, 0, "Ophidien", 2, CraftAttributeInfo.OphidienBone,       CraftResource.OphidienBone,  typeof( OphidienBone ) ),
			new CraftResourceInfo( 2143, 0, "Arachnide", 3, CraftAttributeInfo.ArachnideBone,      CraftResource.ArachnideBone, typeof( ArachnideBone ) ),
			new CraftResourceInfo( 2132, 0, "Dragonique", 3, CraftAttributeInfo.DragoniqueBone,     CraftResource.DragoniqueBone,typeof( DragoniqueBone ) ),
			new CraftResourceInfo( 2667, 0, "Demoniaque", 4, CraftAttributeInfo.DemoniaqueBone,     CraftResource.DemoniaqueBone,typeof( DemoniaqueBone ) ),
			new CraftResourceInfo( 1798, 0, "Ancien", 5, CraftAttributeInfo.AncienBone,         CraftResource.AncienBone,    typeof( AncienBone )),
		};

		private static readonly CraftResourceInfo[] m_WoodInfo = new[]
		{
			new CraftResourceInfo(0000, 0, "Palmier",      0,    CraftAttributeInfo.Blank,               CraftResource.PalmierWood,      typeof( PalmierBoard ),			   typeof(PalmierLog)),
			new CraftResourceInfo(1355, 0, "Érable",    0,    CraftAttributeInfo.ErableWood,               CraftResource.ErableWood,     typeof( ErableBoard ),    typeof(ErableLog)),
			new CraftResourceInfo(1411, 0, "Chêne",   1,    CraftAttributeInfo.CheneWood,       CraftResource.CheneWood,    typeof( CheneBoard ),   typeof(CheneLog)),
			new CraftResourceInfo(1191, 0, "Cèdre",   1,    CraftAttributeInfo.CedreWood,       CraftResource.CedreWood,    typeof( CedreBoard ),   typeof(CedreLog)),
			new CraftResourceInfo(1126, 0, "Cyprès",  2,    CraftAttributeInfo.CypresWood,      CraftResource.CypresWood,   typeof( CypresBoard ),  typeof(CypresLog)),
			new CraftResourceInfo(1008, 0, "Saule",    2,    CraftAttributeInfo.SauleWood,        CraftResource.SauleWood,     typeof( SauleBoard ),    typeof(SauleLog)),
			new CraftResourceInfo(2219, 0, "Acajou",  3,    CraftAttributeInfo.AcajouWood,      CraftResource.AcajouWood,   typeof( AcajouBoard ),  typeof(AcajouLog)),
			new CraftResourceInfo(1109, 0, "Ébène",  3,    CraftAttributeInfo.EbeneWood,      CraftResource.EbeneWood,   typeof( EbeneBoard ),  typeof(EbeneLog)),
			new CraftResourceInfo(2210, 0, "Amarante",   4,    CraftAttributeInfo.AmaranteWood,       CraftResource.AmaranteWood,    typeof( AmaranteBoard ),   typeof(AmaranteLog)),
			new CraftResourceInfo(2500, 0, "Pin",   4,    CraftAttributeInfo.PinWood,       CraftResource.PinWood,    typeof( PinBoard ),   typeof(PinLog)),
			new CraftResourceInfo(1779, 0, "Ancien",      5,    CraftAttributeInfo.AncienWood,          CraftResource.AncienWood,       typeof( AncienBoard ),      typeof(AncienLog)),

			new CraftResourceInfo(0x7DA, 0, "Oak",        99, CraftAttributeInfo.OakWood,      CraftResource.OakWood,      typeof(OakLog),         typeof(OakBoard)),
			new CraftResourceInfo(0x4A7, 0, "Ash",        99, CraftAttributeInfo.AshWood,      CraftResource.AshWood,      typeof(AshLog),         typeof(AshBoard)),
			new CraftResourceInfo(0x4A8, 0, "Yew",        99, CraftAttributeInfo.YewWood,      CraftResource.YewWood,      typeof(YewLog),         typeof(YewBoard)),
			new CraftResourceInfo(0x4A9, 0, "Heartwood",  99, CraftAttributeInfo.Heartwood,    CraftResource.Heartwood,    typeof(HeartwoodLog),   typeof(HeartwoodBoard)),
			new CraftResourceInfo(0x4AA, 0, "Bloodwood",  99, CraftAttributeInfo.Bloodwood,    CraftResource.Bloodwood,    typeof(BloodwoodLog),   typeof(BloodwoodBoard)),
			new CraftResourceInfo(0x47F, 0, "Frostwood",  99, CraftAttributeInfo.Frostwood,    CraftResource.Frostwood,    typeof(FrostwoodLog),   typeof(FrostwoodBoard)),
		};

	private static CraftResourceInfo[] m_FishInfo = new CraftResourceInfo[]


		  {
				new CraftResourceInfo( 0, 0, "Fish",				 0,     null,        CraftResource.Fish,					 typeof( Fish )/*,	    typeof( RawTruiteFishSteak ),	    typeof( TruiteFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Autumn Dragon fish",   0,		null,        CraftResource.AutumnDragonfish,         typeof( AutumnDragonfish )/*,	    typeof( RawTruiteFishSteak ),	    typeof( TruiteFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Blue Lobster",         0,		null,        CraftResource.BlueLobster,				 typeof( BlueLobster )/*,	        typeof( RawDoreFishSteak ),	        typeof( DoreFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Bull Fish",            0,		null,		 CraftResource.BullFish,				 typeof( BullFish )/*,	    typeof( RawCarpeFishSteak ),	    typeof( CarpeFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Crystal Fish",         0,		null,		 CraftResource.CrystalFish,				 typeof( CrystalFish )/*,	    typeof( RawAnguilleFishSteak ),	    typeof( AnguilleFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Fairy Salmon",         0,		null,		 CraftResource.FairySalmon,				 typeof( FairySalmon )/*,	typeof( RawEsturgeonFishSteak ),	typeof( EsturgeonFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Giant Koi",            0,		null,		 CraftResource.GiantKoi,				 typeof( GiantKoi )/*,	    typeof( RawBrochetFishSteak ),	    typeof( BrochetFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Great Barracuda",      0,	    null,        CraftResource.GreatBarracuda,			 typeof( GreatBarracuda )/*,	    typeof( RawSardineFishSteak ),	    typeof( SardineFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Holy Mackerel",        0,	    null,		 CraftResource.HolyMackerel,			 typeof( HolyMackerel )/*,	    typeof( RawAnchoieFishSteak ),	    typeof( AnchoieFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Lava Fish",            0,	    null,		 CraftResource.LavaFish,				 typeof( LavaFish )/*,	    typeof( RawMorueFishSteak ),	    typeof( MorueFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Reaper Fish",          0,     null,		 CraftResource.ReaperFish,				 typeof( ReaperFish )/*,	    typeof( RawHarengFishSteak ),	    typeof( HarengFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Spider Crab",          0,     null,        CraftResource.SpiderCrab,				 typeof( SpiderCrab )/*,	    typeof( RawFletanFishSteak ),	    typeof( FletanFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Stone Crab",           0,     null,		 CraftResource.StoneCrab,				 typeof( StoneCrab )/*,	typeof( RawMaquereauFishSteak ),	typeof( MaquereauFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Summer Dragon fish",   0,     null,        CraftResource.SummerDragonfish,         typeof( SummerDragonfish )/*,	        typeof( RawSoleFishSteak ),	        typeof( SoleFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Unicorn Fish",         0,     null,		 CraftResource.UnicornFish,				 typeof( UnicornFish )/*,	        typeof( RawThonFishSteak ),	        typeof( ThonFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Yellowtail Barracuda", 0,     null,        CraftResource.YellowtailBarracuda,      typeof( YellowtailBarracuda )/*,	    typeof( RawSaumonFishSteak ),	    typeof( SaumonFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Abyssal Dragonfish",   0,     null,		 CraftResource.AbyssalDragonfish,	     typeof( AbyssalDragonfish )/*,	typeof( RawGrandBrochetFishSteak ),	typeof( GrandBrochetFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Black Marlin",         0,		null,		 CraftResource.BlackMarlin,				 typeof( BlackMarlin )/*,typeof( RawTruiteSauvageFishSteak ),typeof( TruiteSauvageFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Blood Lobster",        0,		null,		 CraftResource.BloodLobster,			 typeof( BloodLobster )/*,	typeof( RawGrandDoreFishSteak ),	typeof( GrandDoreFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Blue Marlin",          0,		null,		 CraftResource.BlueMarlin,				 typeof( BlueMarlin )/*,	typeof( RawTruiteMerFishSteak ),	typeof( TruiteMerFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Dread Lobster",        0,     null,		 CraftResource.DreadLobster,			 typeof( DreadLobster )/*,	typeof( RawEsturgeonMerFishSteak ),	typeof( EsturgeonMerFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Dungeon Pike",         0,     null,		 CraftResource.DungeonPike,				 typeof( DungeonPike )/*,	typeof( RawGrandSaumonFishSteak ),	typeof( GrandSaumonFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Giant Samurai Fish",   0,     null,        CraftResource.GiantSamuraiFish,         typeof( GiantSamuraiFish )/*,	        typeof( RawRaieFishSteak ),	        typeof( RaieFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Golden Tuna",          0,		null,		 CraftResource.GoldenTuna,				 typeof( GoldenTuna )/*,	    typeof( RawEspadonFishSteak ),	    typeof( EspadonFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "King fish",            0,     null,		 CraftResource.Kingfish,				 typeof( Kingfish )/*,	typeof( RawRequinGrisFishSteak ),	typeof( RequinGrisFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Lantern Fish",         0,		null,        CraftResource.LanternFish,				 typeof( LanternFish )/*,	typeof( RawRequinBlancFishSteak ),	typeof( RequinBlancFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Rainbow Fish",         0,		null,		 CraftResource.RainbowFish,				 typeof( SeekerFish )	   /* typeof( RawHuitreFishSteak ),       typeof( HuitreFishSteak ) */),
				new CraftResourceInfo( 0, 0, "Seeker Fish",          0,		null,		 CraftResource.SeekerFish,				 typeof( SeekerFish )/*,	    typeof( RawCalmarFishSteak ),	    typeof( CalmarFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Spring Dragon fish",   0,		null,        CraftResource.SpringDragonfish,		 typeof( SpringDragonfish )     /* typeof( RawPieuvreFishSteak ),      typeof( PieuvreFishSteak ) */),
				new CraftResourceInfo( 0, 0, "Stone Fish",           0,		null,		 CraftResource.StoneFish,				 typeof( StoneFish )/*,	    typeof( RawEspadonFishSteak ),	    typeof( EspadonFishSteak )*/ ),
			    new CraftResourceInfo( 0, 0, "Tunnel Crab",          0,		null,        CraftResource.TunnelCrab,				 typeof( TunnelCrab )/*,	typeof( RawRequinGrisFishSteak ),	typeof( RequinGrisFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Void Crab",            0,		null,		 CraftResource.VoidCrab,				 typeof( VoidCrab )/*,	typeof( RawRequinBlancFishSteak ),	typeof( RequinBlancFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Void Lobster",		 0,		null,        CraftResource.VoidLobster,				 typeof( VoidLobster )	 /*   typeof( RawHuitreFishSteak ),       typeof( HuitreFishSteak ) */),
				new CraftResourceInfo( 0, 0, "Winter Dragon fish",   0,		null,        CraftResource.WinterDragonfish,	     typeof( WinterDragonfish )/*,	    typeof( RawCalmarFishSteak ),	    typeof( CalmarFishSteak )*/ ),
				new CraftResourceInfo( 0, 0, "Zombie Fish",          0,		null,		 CraftResource.ZombieFish,				 typeof( ZombieFish )      /* typeof( RawPieuvreFishSteak ),      typeof( PieuvreFishSteak )*/ ),
	  };

		public static int GetLevel(CraftResource resource)
		{
			CraftResourceInfo info = GetInfo(resource);

			return info != null ? info.Level : 0;
		}

		/// <summary>
		/// Returns true if '<paramref name="resource"/>' is None, Iron, RegularLeather or PalmierWood. False if otherwise.
		/// </summary>
		public static bool IsStandard(CraftResource resource)
		{
			return (resource == CraftResource.None || resource == CraftResource.Iron || resource == CraftResource.RegularLeather || resource == CraftResource.PalmierWood);
		}

		private static Hashtable m_TypeTable;

		/// <summary>
		/// Registers that '<paramref name="resourceType"/>' uses '<paramref name="resource"/>' so that it can later be queried by <see cref="GetFromType"/>
		/// </summary>
		public static void RegisterType(Type resourceType, CraftResource resource)
		{
			if (m_TypeTable == null)
				m_TypeTable = new Hashtable();

			m_TypeTable[resourceType] = resource;
		}

		/// <summary>
		/// Returns the <see cref="CraftResource"/> value for which '<paramref name="resourceType"/>' uses -or- CraftResource.None if an unregistered type was specified.
		/// </summary>
		public static CraftResource GetFromType(Type resourceType)
		{
			if (m_TypeTable == null)
				return CraftResource.None;

			object obj = m_TypeTable[resourceType];

			if (!(obj is CraftResource))
				return CraftResource.None;

			return (CraftResource)obj;
		}

		/// <summary>
		/// Returns a <see cref="CraftResourceInfo"/> instance describing '<paramref name="resource"/>' -or- null if an invalid resource was specified.
		/// </summary>
		public static CraftResourceInfo GetInfo(CraftResource resource)
		{
			CraftResourceInfo[] list = null;

			switch (GetType(resource))
			{
				case CraftResourceType.Metal:
					list = m_MetalInfo;
					break;
				case CraftResourceType.Leather:
					list = m_AOSLeatherInfo;
					break;
				case CraftResourceType.Scales:
					list = m_ScaleInfo;
					break;
				case CraftResourceType.Wood:
					list = m_WoodInfo;
					break;
				case CraftResourceType.Bone:
					list = m_BoneInfo;
					break;
				case CraftResourceType.Fish:
					list = m_FishInfo;
					break;
			}

			if (list != null)
			{
				int index = GetIndex(resource);

				if (index >= 0 && index < list.Length)
					return list[index];
			}

			return null;
		}

		/// <summary>
		/// Returns a <see cref="CraftResourceType"/> value indiciating the type of '<paramref name="resource"/>'.
		/// </summary>
		public static CraftResourceType GetType(CraftResource resource)
		{
			if (resource >= CraftResource.Iron && resource <= CraftResource.Nostalgium)
				return CraftResourceType.Metal;

			if (resource >= CraftResource.RegularLeather && resource <= CraftResource.AncienLeather)
				return CraftResourceType.Leather;

			if (resource >= CraftResource.RedScales && resource <= CraftResource.BlueScales)
				return CraftResourceType.Scales;

			if (resource >= CraftResource.PalmierWood && resource <= CraftResource.Frostwood)
				return CraftResourceType.Wood;


			if (resource >= CraftResource.RegularBone && resource <= CraftResource.AncienBone)
				return CraftResourceType.Bone;

			if (resource >= CraftResource.Fish && resource <= CraftResource.ZombieFish)
				return CraftResourceType.Fish;

			return CraftResourceType.None;
		}

		/// <summary>
		/// Returns the first <see cref="CraftResource"/> in the series of resources for which '<paramref name="resource"/>' belongs.
		/// </summary>
		public static CraftResource GetStart(CraftResource resource)
		{
			switch (GetType(resource))
			{
				case CraftResourceType.Metal:
					return CraftResource.Iron;
				case CraftResourceType.Leather:
					return CraftResource.RegularLeather;
				case CraftResourceType.Scales:
					return CraftResource.RedScales;
				case CraftResourceType.Wood:
					return CraftResource.PalmierWood;
				case CraftResourceType.Bone:
					return CraftResource.RegularBone;
			}

			return CraftResource.None;
		}

		/// <summary>
		/// Returns the index of '<paramref name="resource"/>' in the seriest of resources for which it belongs.
		/// </summary>
		public static int GetIndex(CraftResource resource)
		{
			CraftResource start = GetStart(resource);

			if (start == CraftResource.None)
				return 0;

			return resource - start;
		}

		/// <summary>
		/// Returns the <see cref="CraftResourceInfo.Number"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
		/// </summary>
		public static int GetLocalizationNumber(CraftResource resource)
		{
			CraftResourceInfo info = GetInfo(resource);

			return (info == null ? 0 : info.Number);
		}

		/// <summary>
		/// Returns the <see cref="CraftResourceInfo.Hue"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
		/// </summary>
		public static int GetHue(CraftResource resource)
		{
			CraftResourceInfo info = GetInfo(resource);

			return (info == null ? 0 : info.Hue);
		}

		/// <summary>
		/// Returns the <see cref="CraftResourceInfo.Name"/> property of '<paramref name="resource"/>' -or- an empty string if the resource specified was invalid.
		/// </summary>
		public static string GetName(CraftResource resource)
		{
			CraftResourceInfo info = GetInfo(resource);

			return (info == null ? string.Empty : info.Name);
		}


		public static string GetDescription(CraftResource resource)
		{
			FieldInfo field = resource.GetType().GetField(resource.ToString());
			DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
			return attribute == null ? resource.ToString() : attribute.Description;
		}



		/// <summary>
		/// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>' -or- CraftResource.None if unable to convert.
		/// </summary>
		public static CraftResource GetFromOreInfo(OreInfo info)
		{
			if (info.Name.IndexOf("Lupus") >= 0)
				return CraftResource.LupusLeather;
			else if (info.Name.IndexOf("Reptilien") >= 0)
				return CraftResource.ReptilienLeather;
			else if (info.Name.IndexOf("Geant") >= 0)
				return CraftResource.GeantLeather;

			else if (info.Name.IndexOf("Ophidien") >= 0)
				return CraftResource.OphidienLeather;
			else if (info.Name.IndexOf("Arachnide") >= 0)
				return CraftResource.ArachnideLeather;
			else if (info.Name.IndexOf("Dragonique") >= 0)
				return CraftResource.DragoniqueLeather;
			else if (info.Name.IndexOf("Demoniaque") >= 0)
				return CraftResource.DemoniaqueLeather;
			else if (info.Name.IndexOf("Ancien") >= 0)
				return CraftResource.AncienLeather;


			else if (info.Name.IndexOf("Leather") >= 0)
				return CraftResource.RegularLeather;




			if (info.Level == 0)
				return CraftResource.Iron;
			else if (info.Level == 1)
				return CraftResource.DullCopper;
			else if (info.Level == 2)
				return CraftResource.ShadowIron;
			else if (info.Level == 3)
				return CraftResource.Copper;
			else if (info.Level == 4)
				return CraftResource.Bronze;
			else if (info.Level == 5)
				return CraftResource.Gold;
			else if (info.Level == 6)
				return CraftResource.Agapite;
			else if (info.Level == 7)
				return CraftResource.Verite;
			else if (info.Level == 8)
				return CraftResource.Valorite;
			else if (info.Level == 16)
				return CraftResource.Mytheril;

			return CraftResource.None;
		}

		/// <summary>
		/// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>', using '<paramref name="material"/>' to help resolve leather OreInfo instances.
		/// </summary>
		public static CraftResource GetFromOreInfo(OreInfo info, ArmorMaterialType material)
		{
			if (material == ArmorMaterialType.Studded || material == ArmorMaterialType.Leather)
			{
				if (info.Level == 0)
					return CraftResource.RegularLeather;
				else if (info.Level == 1)
					return CraftResource.LupusLeather;
				else if (info.Level == 2)
					return CraftResource.ReptilienLeather;
				else if (info.Level == 3)
					return CraftResource.GeantLeather;
				else if (info.Level == 4)
					return CraftResource.OphidienLeather;
				else if (info.Level == 5)
					return CraftResource.ArachnideLeather;
				else if (info.Level == 6)
					return CraftResource.DragoniqueLeather;
				else if (info.Level == 7)
					return CraftResource.DemoniaqueLeather;
				else if (info.Level == 8)
					return CraftResource.AncienLeather;
				else if (info.Level == 9)

					return CraftResource.None;
			}

			return GetFromOreInfo(info);
		}
	}

	// NOTE: This class is only for compatability with very old RunUO versions.
	// No changes to it should be required for custom resources.
	public class OreInfo
	{
		public static readonly OreInfo Iron = new OreInfo(0, 0x000, "Iron");
		public static readonly OreInfo DullCopper = new OreInfo(1, 0x973, "Dull Copper");
		public static readonly OreInfo ShadowIron = new OreInfo(2, 0x966, "Shadow Iron");
		public static readonly OreInfo Copper = new OreInfo(3, 0x96D, "Copper");
		public static readonly OreInfo Bronze = new OreInfo(4, 0x972, "Bronze");
		public static readonly OreInfo Gold = new OreInfo(5, 0x8A5, "Gold");
		public static readonly OreInfo Agapite = new OreInfo(6, 0x979, "Agapite");
		public static readonly OreInfo Verite = new OreInfo(7, 0x89F, "Verite");
		public static readonly OreInfo Valorite = new OreInfo(8, 0x8AB, "Valorite");
		public static readonly OreInfo Mytheril = new OreInfo(16, 1342, "Mytheril");

		private readonly int m_Level;
		private readonly int m_Hue;
		private readonly string m_Name;

		public OreInfo(int level, int hue, string name)
		{
			m_Level = level;
			m_Hue = hue;
			m_Name = name;
		}

		public int Level => m_Level;

		public int Hue => m_Hue;

		public string Name => m_Name;
	}
}
