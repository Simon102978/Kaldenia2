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

			CraftAttributeInfo bronze = Bronze = new CraftAttributeInfo();

			bronze.ArmorPhysicalResist = 1;
			bronze.ArmorColdResist = 1;
			bronze.ArmorPoisonResist = 1;

			bronze.WeaponColdDamage = 10;
			bronze.WeaponPoisonDamage = 10;

			CraftAttributeInfo cuivre = Cuivre = new CraftAttributeInfo();

			cuivre.ArmorPhysicalResist = 1;
			cuivre.ArmorFireResist = 1;
			cuivre.ArmorEnergyResist = 1;

			cuivre.WeaponFireDamage = 10;
			cuivre.WeaponEnergyDamage = 10;

			CraftAttributeInfo sonne = Sonne = new CraftAttributeInfo();

			sonne.ArmorPhysicalResist = 2;
			sonne.ArmorFireResist = 2;
			sonne.ArmorPoisonResist = 2;

			sonne.WeaponFireDamage = 20;
			sonne.WeaponPoisonDamage = 20;

			CraftAttributeInfo argent = Argent = new CraftAttributeInfo();

			argent.ArmorPhysicalResist = 2;
			argent.ArmorFireResist = 2;
			argent.ArmorEnergyResist = 2;

			argent.WeaponFireDamage = 20;
			argent.WeaponEnergyDamage = 20;

			CraftAttributeInfo boreale = Boreale = new CraftAttributeInfo();

			boreale.ArmorPhysicalResist = 2;
			boreale.ArmorFireResist = 2;
			boreale.ArmorColdResist = 2;

			boreale.WeaponFireDamage = 20;
			boreale.WeaponColdDamage = 20;

			CraftAttributeInfo chrysteliar = Chrysteliar = new CraftAttributeInfo();

			chrysteliar.ArmorPhysicalResist = 2;
			chrysteliar.ArmorColdResist = 2;
			chrysteliar.ArmorPoisonResist = 2;

			chrysteliar.WeaponColdDamage = 20;
			chrysteliar.WeaponPoisonDamage = 20;

			CraftAttributeInfo glacias = Glacias = new CraftAttributeInfo();

			glacias.ArmorPhysicalResist = 2;
			glacias.ArmorColdResist = 2;
			glacias.ArmorEnergyResist = 2;

			glacias.WeaponDamage = 20;
			glacias.WeaponDamage = 20;

			CraftAttributeInfo lithiar = Lithiar = new CraftAttributeInfo();

			lithiar.ArmorPhysicalResist = 2;
			lithiar.ArmorPoisonResist = 2;
			lithiar.ArmorEnergyResist = 2;

			lithiar.WeaponPoisonDamage = 20;
			lithiar.WeaponEnergyDamage = 20;

			CraftAttributeInfo acier = Acier = new CraftAttributeInfo();

			acier.ArmorPhysicalResist = 3;
			acier.ArmorFireResist = 3;
			acier.ArmorPoisonResist = 3;

			acier.WeaponFireDamage = 30;
			acier.WeaponPoisonDamage = 30;

			CraftAttributeInfo durian = Durian = new CraftAttributeInfo();

			durian.ArmorPhysicalResist = 3;
			durian.ArmorFireResist = 3;
			durian.ArmorEnergyResist = 3;

			durian.WeaponFireDamage = 30;
			durian.WeaponEnergyDamage = 30;

			CraftAttributeInfo equilibrum = Equilibrum = new CraftAttributeInfo();

			equilibrum.ArmorPhysicalResist = 3;
			equilibrum.ArmorFireResist = 3;
			equilibrum.ArmorColdResist = 3;

			equilibrum.WeaponFireDamage = 30;
			equilibrum.WeaponColdDamage = 30;

			CraftAttributeInfo or = Or = new CraftAttributeInfo();

			or.ArmorPhysicalResist = 3;
			or.ArmorColdResist = 3;
			or.ArmorPoisonResist = 3;

			or.WeaponColdDamage = 30;
			or.WeaponPoisonDamage = 30;

			CraftAttributeInfo jolinar = Jolinar = new CraftAttributeInfo();

			jolinar.ArmorPhysicalResist = 3;
			jolinar.ArmorColdResist = 3;
			jolinar.ArmorEnergyResist = 3;

			jolinar.WeaponColdDamage = 30;
			jolinar.WeaponEnergyDamage = 30;

			CraftAttributeInfo justicium = Justicium = new CraftAttributeInfo();

			justicium.ArmorPhysicalResist = 3;
			justicium.ArmorPoisonResist = 3;
			justicium.ArmorEnergyResist = 3;

			justicium.WeaponPoisonDamage = 30;
			justicium.WeaponEnergyDamage = 30;

			CraftAttributeInfo abyssium = Abyssium = new CraftAttributeInfo();

			abyssium.ArmorPhysicalResist = 4;
			abyssium.ArmorFireResist = 4;
			abyssium.ArmorPoisonResist = 4;

			abyssium.WeaponFireDamage = 40;
			abyssium.WeaponPoisonDamage = 40;

			CraftAttributeInfo bloodirium = Bloodirium = new CraftAttributeInfo();

			bloodirium.ArmorPhysicalResist = 4;
			bloodirium.ArmorFireResist = 4;
			bloodirium.ArmorEnergyResist = 4;

			bloodirium.WeaponFireDamage = 40;
			bloodirium.WeaponEnergyDamage = 40;

			CraftAttributeInfo herbrosite = Herbrosite = new CraftAttributeInfo();

			herbrosite.ArmorPhysicalResist = 4;
			herbrosite.ArmorFireResist = 4;
			herbrosite.ArmorColdResist = 4;

			herbrosite.WeaponFireDamage = 40;
			herbrosite.WeaponColdDamage = 40;

			CraftAttributeInfo khandarium = Khandarium = new CraftAttributeInfo();

			khandarium.ArmorPhysicalResist = 4;
			khandarium.ArmorColdResist = 4;
			khandarium.ArmorPoisonResist = 4;

			khandarium.WeaponColdDamage = 40;
			khandarium.WeaponPoisonDamage = 40;

			CraftAttributeInfo mytheril = Mytheril = new CraftAttributeInfo();

			mytheril.ArmorPhysicalResist = 4;
			mytheril.ArmorColdResist = 4;
			mytheril.ArmorEnergyResist = 4;

			mytheril.WeaponColdDamage = 40;
			mytheril.WeaponEnergyDamage = 40;

			CraftAttributeInfo sombralir = Sombralir = new CraftAttributeInfo();

			sombralir.ArmorPhysicalResist = 4;
			sombralir.ArmorPoisonResist = 4;
			sombralir.ArmorEnergyResist = 4;

			sombralir.WeaponPoisonDamage = 40;
			sombralir.WeaponEnergyDamage = 40;

			CraftAttributeInfo draconyr = Draconyr = new CraftAttributeInfo();

			draconyr.ArmorPhysicalResist = 5;
			draconyr.ArmorFireResist = 7;
			draconyr.ArmorColdResist = 2;
			draconyr.ArmorPoisonResist = 7;
			draconyr.ArmorEnergyResist = 2;

			draconyr.WeaponFireDamage = 50;
			draconyr.WeaponPoisonDamage = 50;

			CraftAttributeInfo heptazion = Heptazion = new CraftAttributeInfo();

			heptazion.ArmorPhysicalResist = 5;
			heptazion.ArmorFireResist = 7;
			heptazion.ArmorColdResist = 2;
			heptazion.ArmorPoisonResist = 2;
			heptazion.ArmorEnergyResist = 7;

			heptazion.WeaponFireDamage = 50;
			heptazion.WeaponEnergyDamage = 50;

			CraftAttributeInfo oceanis = Oceanis = new CraftAttributeInfo();

			oceanis.ArmorPhysicalResist = 5;
			oceanis.ArmorFireResist = 7;
			oceanis.ArmorColdResist = 7;
			oceanis.ArmorPoisonResist = 2;
			oceanis.ArmorEnergyResist = 2;

			oceanis.WeaponFireDamage = 50;
			oceanis.WeaponColdDamage = 50;

			CraftAttributeInfo brazium = Brazium = new CraftAttributeInfo();

			brazium.ArmorPhysicalResist = 5;
			brazium.ArmorFireResist = 2;
			brazium.ArmorColdResist = 7;
			brazium.ArmorPoisonResist = 7;
			brazium.ArmorEnergyResist = 2;

			brazium.WeaponColdDamage = 50;
			brazium.WeaponPoisonDamage = 50;

			CraftAttributeInfo lunerium = Lunerium = new CraftAttributeInfo();

			lunerium.ArmorPhysicalResist = 5;
			lunerium.ArmorFireResist = 2;
			lunerium.ArmorColdResist = 7;
			lunerium.ArmorPoisonResist = 2;
			lunerium.ArmorEnergyResist = 7;

			lunerium.WeaponColdDamage = 50;
			lunerium.WeaponEnergyDamage = 50;

			CraftAttributeInfo marinar = Marinar = new CraftAttributeInfo();

			marinar.ArmorPhysicalResist = 5;
			marinar.ArmorFireResist = 2;
			marinar.ArmorColdResist = 2;
			marinar.ArmorPoisonResist = 7;
			marinar.ArmorEnergyResist = 7;

			marinar.ArmorPoisonResist = 50;
			marinar.WeaponEnergyDamage = 50;

			CraftAttributeInfo nostalgium = Nostalgium = new CraftAttributeInfo();

			nostalgium.ArmorPhysicalResist = 6;
			nostalgium.ArmorFireResist = 8;
			nostalgium.ArmorColdResist = 8;
			nostalgium.ArmorPoisonResist = 8;
			nostalgium.ArmorEnergyResist = 8;

			nostalgium.WeaponFireDamage = 20;
			nostalgium.WeaponColdDamage = 20;
			nostalgium.WeaponPoisonDamage = 20;
			nostalgium.WeaponEnergyDamage = 20;
			nostalgium.WeaponDamage = 20;

			// Cuir
			CraftAttributeInfo lupusLeather = LupusLeather = new CraftAttributeInfo();

			lupusLeather.ArmorPhysicalResist = 0;
			lupusLeather.ArmorDurability = 50;
			lupusLeather.ArmorLowerRequirements = 20;
			lupusLeather.WeaponDurability = 100;
			lupusLeather.WeaponLowerRequirements = 50;
			lupusLeather.RunicMinAttributes = 1;
			lupusLeather.RunicMaxAttributes = 2;

			lupusLeather.RunicMinIntensity = 40;
			lupusLeather.RunicMaxIntensity = 100;

			CraftAttributeInfo reptilienLeather = ReptilienLeather = new CraftAttributeInfo();

			reptilienLeather.ArmorPhysicalResist = 0;
			reptilienLeather.ArmorFireResist = 2;
			reptilienLeather.ArmorEnergyResist = 7;
			reptilienLeather.ArmorDurability = 100;

			reptilienLeather.WeaponColdDamage = 20;
			reptilienLeather.WeaponDurability = 50;

			reptilienLeather.RunicMinAttributes = 2;
			reptilienLeather.RunicMaxAttributes = 2;

			reptilienLeather.RunicMinIntensity = 45;
			reptilienLeather.RunicMaxIntensity = 100;

			CraftAttributeInfo geantLeather = GeantLeather = new CraftAttributeInfo();

			geantLeather.ArmorPhysicalResist = 0;
			geantLeather.ArmorFireResist = 2;
			geantLeather.ArmorPoisonResist = 7;
			geantLeather.ArmorEnergyResist = 2;
			geantLeather.WeaponPoisonDamage = 10;
			geantLeather.WeaponEnergyDamage = 20;
			geantLeather.RunicMinAttributes = 2;
			geantLeather.RunicMaxAttributes = 3;

			geantLeather.RunicMinIntensity = 50;
			geantLeather.RunicMaxIntensity = 100;

			CraftAttributeInfo ophidienLeather = OphidienLeather = new CraftAttributeInfo();

			ophidienLeather.ArmorPhysicalResist = 0;
			ophidienLeather.ArmorColdResist = 7;
			ophidienLeather.ArmorPoisonResist = 2;
			ophidienLeather.ArmorEnergyResist = 2;
			ophidienLeather.WeaponFireDamage = 40;
			ophidienLeather.RunicMinAttributes = 3;
			ophidienLeather.RunicMaxAttributes = 3;

			ophidienLeather.RunicMinIntensity = 55;
			ophidienLeather.RunicMaxIntensity = 100;

			CraftAttributeInfo arachnideLeather = ArachnideLeather = new CraftAttributeInfo();

			arachnideLeather.ArmorPhysicalResist = 0;
			arachnideLeather.ArmorFireResist = 2;
			arachnideLeather.ArmorColdResist = 2;
			arachnideLeather.ArmorEnergyResist = 3;
			arachnideLeather.ArmorLuck = 40;
			arachnideLeather.ArmorLowerRequirements = 30;
			arachnideLeather.WeaponLuck = 40;
			arachnideLeather.WeaponLowerRequirements = 50;
			arachnideLeather.RunicMinAttributes = 3;
			arachnideLeather.RunicMaxAttributes = 4;

			arachnideLeather.RunicMinIntensity = 60;
			arachnideLeather.RunicMaxIntensity = 100;

			CraftAttributeInfo dragoniqueLeather = DragoniqueLeather = new CraftAttributeInfo();

			dragoniqueLeather.ArmorPhysicalResist = 1;
			dragoniqueLeather.ArmorFireResist = 7;
			dragoniqueLeather.ArmorColdResist = 2;
			dragoniqueLeather.ArmorPoisonResist = 2;
			dragoniqueLeather.ArmorEnergyResist = 2;
			dragoniqueLeather.WeaponColdDamage = 30;
			dragoniqueLeather.WeaponEnergyDamage = 20;
			dragoniqueLeather.RunicMinAttributes = 4;
			dragoniqueLeather.RunicMaxAttributes = 4;

			dragoniqueLeather.RunicMinIntensity = 65;
			dragoniqueLeather.RunicMaxIntensity = 100;

			CraftAttributeInfo demoniaqueLeather = DemoniaqueLeather = new CraftAttributeInfo();

			demoniaqueLeather.ArmorPhysicalResist = 1;
			demoniaqueLeather.ArmorFireResist = 4;
			demoniaqueLeather.ArmorColdResist = 3;
			demoniaqueLeather.ArmorPoisonResist = 4;
			demoniaqueLeather.ArmorEnergyResist = 1;
			demoniaqueLeather.WeaponPoisonDamage = 40;
			demoniaqueLeather.WeaponEnergyDamage = 20;
			demoniaqueLeather.RunicMinAttributes = 4;
			demoniaqueLeather.RunicMaxAttributes = 5;

			demoniaqueLeather.RunicMinIntensity = 70;
			demoniaqueLeather.RunicMaxIntensity = 100;

			CraftAttributeInfo ancienLeather = AncienLeather = new CraftAttributeInfo();

			ancienLeather.ArmorPhysicalResist = 1;
			ancienLeather.ArmorColdResist = 4;
			ancienLeather.ArmorPoisonResist = 4;
			ancienLeather.ArmorEnergyResist = 4;
			ancienLeather.ArmorDurability = 50;
			ancienLeather.WeaponFireDamage = 10;
			ancienLeather.WeaponColdDamage = 20;
			ancienLeather.WeaponPoisonDamage = 10;
			ancienLeather.WeaponEnergyDamage = 20;
			ancienLeather.RunicMinAttributes = 5;
			ancienLeather.RunicMaxAttributes = 5;

			ancienLeather.RunicMinIntensity = 85;
			ancienLeather.RunicMaxIntensity = 100;

			// Os

			CraftAttributeInfo lupusBone = LupusBone = new CraftAttributeInfo();

			lupusBone.ArmorPhysicalResist = 1;
			lupusBone.ArmorDurability = 50;
			lupusBone.ArmorLowerRequirements = 20;
			lupusBone.WeaponDurability = 100;
			lupusBone.WeaponLowerRequirements = 50;
			lupusBone.RunicMinAttributes = 1;
			lupusBone.RunicMaxAttributes = 2;

			lupusBone.RunicMinIntensity = 40;
			lupusBone.RunicMaxIntensity = 100;

			CraftAttributeInfo reptilienBone = ReptilienBone = new CraftAttributeInfo();

			reptilienBone.ArmorPhysicalResist = 3;
			reptilienBone.ArmorFireResist = 2;
			reptilienBone.ArmorEnergyResist = 7;
			reptilienBone.ArmorDurability = 100;

			reptilienBone.WeaponColdDamage = 20;
			reptilienBone.WeaponDurability = 50;

			reptilienBone.RunicMinAttributes = 2;
			reptilienBone.RunicMaxAttributes = 2;

			reptilienBone.RunicMinIntensity = 45;
			reptilienBone.RunicMaxIntensity = 100;

			CraftAttributeInfo geantBone = GeantBone = new CraftAttributeInfo();

			geantBone.ArmorPhysicalResist = 2;
			geantBone.ArmorFireResist = 2;
			geantBone.ArmorPoisonResist = 7;
			geantBone.ArmorEnergyResist = 2;
			geantBone.WeaponPoisonDamage = 10;
			geantBone.WeaponEnergyDamage = 20;
			geantBone.RunicMinAttributes = 2;
			geantBone.RunicMaxAttributes = 3;

			geantBone.RunicMinIntensity = 50;
			geantBone.RunicMaxIntensity = 100;

			CraftAttributeInfo ophidienBone = OphidienBone = new CraftAttributeInfo();

			ophidienBone.ArmorPhysicalResist = 3;
			ophidienBone.ArmorColdResist = 7;
			ophidienBone.ArmorPoisonResist = 2;
			ophidienBone.ArmorEnergyResist = 2;
			ophidienBone.WeaponFireDamage = 40;
			ophidienBone.RunicMinAttributes = 3;
			ophidienBone.RunicMaxAttributes = 3;

			ophidienBone.RunicMinIntensity = 55;
			ophidienBone.RunicMaxIntensity = 100;

			CraftAttributeInfo arachnideBone = ArachnideBone = new CraftAttributeInfo();

			arachnideBone.ArmorPhysicalResist = 2;
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
			arachnideBone.RunicMaxIntensity = 100;

			CraftAttributeInfo dragoniqueBone = DragoniqueBone = new CraftAttributeInfo();

			dragoniqueBone.ArmorPhysicalResist = 2;
			dragoniqueBone.ArmorFireResist = 7;
			dragoniqueBone.ArmorColdResist = 2;
			dragoniqueBone.ArmorPoisonResist = 2;
			dragoniqueBone.ArmorEnergyResist = 2;
			dragoniqueBone.WeaponColdDamage = 30;
			dragoniqueBone.WeaponEnergyDamage = 20;
			dragoniqueBone.RunicMinAttributes = 4;
			dragoniqueBone.RunicMaxAttributes = 4;

			dragoniqueBone.RunicMinIntensity = 65;
			dragoniqueBone.RunicMaxIntensity = 100;

			CraftAttributeInfo demoniaqueBone = DemoniaqueBone = new CraftAttributeInfo();

			demoniaqueBone.ArmorPhysicalResist = 4;
			demoniaqueBone.ArmorFireResist = 4;
			demoniaqueBone.ArmorColdResist = 3;
			demoniaqueBone.ArmorPoisonResist = 4;
			demoniaqueBone.ArmorEnergyResist = 1;
			demoniaqueBone.WeaponPoisonDamage = 40;
			demoniaqueBone.WeaponEnergyDamage = 20;
			demoniaqueBone.RunicMinAttributes = 4;
			demoniaqueBone.RunicMaxAttributes = 5;

			demoniaqueBone.RunicMinIntensity = 70;
			demoniaqueBone.RunicMaxIntensity = 100;

			CraftAttributeInfo ancienBone = AncienBone = new CraftAttributeInfo();

			ancienBone.ArmorPhysicalResist = 5;
			ancienBone.ArmorColdResist = 4;
			ancienBone.ArmorPoisonResist = 4;
			ancienBone.ArmorEnergyResist = 4;
			ancienBone.ArmorDurability = 50;
			ancienBone.WeaponFireDamage = 10;
			ancienBone.WeaponColdDamage = 20;
			ancienBone.WeaponPoisonDamage = 10;
			ancienBone.WeaponEnergyDamage = 20;
			ancienBone.RunicMinAttributes = 5;
			ancienBone.RunicMaxAttributes = 5;

			ancienBone.RunicMinIntensity = 85;
			ancienBone.RunicMaxIntensity = 100;


			// Os
			CraftAttributeInfo ErableWood = ErableWood = new CraftAttributeInfo();

			ErableWood.ArmorPhysicalResist = 1;

			CraftAttributeInfo CheneWood = CheneWood = new CraftAttributeInfo();

			CheneWood.ArmorPhysicalResist = 2;
			CheneWood.ArmorPoisonResist = 2;
			CheneWood.ArmorColdResist = 2;

			CheneWood.WeaponPoisonDamage = 20;
			CheneWood.WeaponColdDamage = 20;

			CraftAttributeInfo CedreWood = CedreWood = new CraftAttributeInfo();

			CedreWood.ArmorPhysicalResist = 2;
			CedreWood.ArmorColdResist = 2;
			CedreWood.ArmorEnergyResist = 2;

			CedreWood.WeaponPoisonDamage = 20;
			CedreWood.WeaponEnergyDamage = 20;

			CraftAttributeInfo CypresWood = CypresWood = new CraftAttributeInfo();

			CypresWood.ArmorPhysicalResist = 3;
			CypresWood.ArmorFireResist = 3;
			CypresWood.ArmorPoisonResist = 3;

			CypresWood.WeaponFireDamage = 30;
			CypresWood.WeaponPoisonDamage = 30;

			CraftAttributeInfo SauleWood = SauleWood = new CraftAttributeInfo();

			SauleWood.ArmorPhysicalResist = 3;
			SauleWood.ArmorFireResist = 3;
			SauleWood.ArmorEnergyResist = 3;

			SauleWood.WeaponFireDamage = 30;
			SauleWood.WeaponEnergyDamage = 30;

			CraftAttributeInfo PinWood = PinWood = new CraftAttributeInfo();

			PinWood.ArmorPhysicalResist = 4;
			PinWood.ArmorPoisonResist = 4;
			PinWood.ArmorColdResist = 4;

			PinWood.WeaponPoisonDamage = 40;
			PinWood.WeaponColdDamage = 40;

			CraftAttributeInfo AmaranteWood = AmaranteWood = new CraftAttributeInfo();

			AmaranteWood.ArmorPhysicalResist = 4;
			AmaranteWood.ArmorFireResist = 4;
			AmaranteWood.ArmorEnergyResist = 4;

			AmaranteWood.WeaponFireDamage = 40;
			AmaranteWood.WeaponEnergyDamage = 40;

			CraftAttributeInfo AcajouWood = AcajouWood = new CraftAttributeInfo();

			AcajouWood.ArmorPhysicalResist = 5;
			AcajouWood.ArmorColdResist = 5;
			AcajouWood.ArmorPoisonResist = 5;

			AcajouWood.WeaponColdDamage = 50;
			AcajouWood.WeaponPoisonDamage = 50;

			CraftAttributeInfo EbeneWood = EbeneWood = new CraftAttributeInfo();

			EbeneWood.ArmorPhysicalResist = 5;
			EbeneWood.ArmorFireResist = 5;
			EbeneWood.ArmorEnergyResist = 5;

			EbeneWood.WeaponFireDamage = 50;
			EbeneWood.WeaponEnergyDamage = 50;

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
			new CraftResourceInfo( 1767,    0, "Boréale",       1, CraftAttributeInfo.Boreale,      CraftResource.Boreale,      typeof( BorealeIngot ),     typeof( BorealeOre ) ),
			new CraftResourceInfo( 1759,    0, "Chrysteliar",   1, CraftAttributeInfo.Chrysteliar,  CraftResource.Chrysteliar,  typeof( ChrysteliarIngot ), typeof( ChrysteliarOre ) ),
			new CraftResourceInfo( 1365,    0, "Glacias",       1, CraftAttributeInfo.Glacias,      CraftResource.Glacias,      typeof( GlaciasIngot ),     typeof( GlaciasOre ) ),
			new CraftResourceInfo( 1448,    0, "Lithiar",       1, CraftAttributeInfo.Lithiar,      CraftResource.Lithiar,      typeof( LithiarIngot ),     typeof( LithiarOre ) ),
			new CraftResourceInfo( 1102,    0, "Acier",         2, CraftAttributeInfo.Acier,        CraftResource.Acier,        typeof( AcierIngot ),       typeof( AcierOre ) ),
			new CraftResourceInfo( 1160,    0, "Durian",        2, CraftAttributeInfo.Durian,       CraftResource.Durian,       typeof( DurianIngot ),      typeof( DurianOre ) ),
			new CraftResourceInfo( 2212,    0, "Equilibrum",    2, CraftAttributeInfo.Equilibrum,   CraftResource.Equilibrum,   typeof( EquilibrumIngot ),  typeof( EquilibrumOre ) ),
			new CraftResourceInfo( 2125,    0, "Or",            2, CraftAttributeInfo.Or,           CraftResource.Gold,         typeof( GoldIngot ),        typeof( GoldOre ) ),
			new CraftResourceInfo( 2205,    0, "Jolinar",       2, CraftAttributeInfo.Jolinar,      CraftResource.Jolinar,      typeof( JolinarIngot ),     typeof( JolinarOre ) ),
			new CraftResourceInfo( 2219,    0, "Justicium",     2, CraftAttributeInfo.Justicium,    CraftResource.Justicium,    typeof( JusticiumIngot ),   typeof( JusticiumOre ) ),
			new CraftResourceInfo( 1149,    0, "Abyssium",      3, CraftAttributeInfo.Abyssium,     CraftResource.Abyssium,     typeof( AbyssiumIngot ),    typeof( AbyssiumOre ) ),
			new CraftResourceInfo( 1777,    0, "Bloodirium",    3, CraftAttributeInfo.Bloodirium,   CraftResource.Bloodirium,   typeof( BloodiriumIngot ),  typeof( BloodiriumOre ) ),
			new CraftResourceInfo( 1445,    0, "Herbrosite",    3, CraftAttributeInfo.Herbrosite,   CraftResource.Herbrosite,   typeof( HerbrositeIngot ),  typeof( HerbrositeOre ) ),
			new CraftResourceInfo( 1746,    0, "Khandarium",    3, CraftAttributeInfo.Khandarium,   CraftResource.Khandarium,   typeof( KhandariumIngot ),  typeof( KhandariumOre ) ),
			new CraftResourceInfo( 1757,    0, "Mytheril",      3, CraftAttributeInfo.Mytheril,     CraftResource.Mytheril,     typeof( MytherilIngot ),    typeof( MytherilOre ) ),
			new CraftResourceInfo( 2051,    0, "Sombralir",     3, CraftAttributeInfo.Sombralir,    CraftResource.Sombralir,    typeof( SombralirIngot ),   typeof( SombralirOre ) ),
			new CraftResourceInfo( 2591,    0, "Draconyr",      4, CraftAttributeInfo.Draconyr,     CraftResource.Draconyr,     typeof( DraconyrIngot ),    typeof( DraconyrOre ) ),
			new CraftResourceInfo( 2130,    0, "Heptazion",     4, CraftAttributeInfo.Heptazion,    CraftResource.Heptazion,    typeof( HeptazionIngot ),   typeof( HeptazionOre ) ),
			new CraftResourceInfo( 1770,    0, "Oceanis",       4, CraftAttributeInfo.Oceanis,      CraftResource.Oceanis,      typeof( OceanisIngot ),     typeof( OceanisOre ) ),
			new CraftResourceInfo( 1509,    0, "Brazium",       4, CraftAttributeInfo.Brazium,      CraftResource.Brazium,      typeof( BraziumIngot ),     typeof( BraziumOre ) ),
			new CraftResourceInfo( 2656,    0, "Lunerium",      4, CraftAttributeInfo.Lunerium,     CraftResource.Lunerium,     typeof( LuneriumIngot ),    typeof( LuneriumOre ) ),
			new CraftResourceInfo( 1411,    0, "Marinar",       4, CraftAttributeInfo.Marinar,      CraftResource.Marinar,      typeof( MarinarIngot ),     typeof( MarinarOre ) ),
			new CraftResourceInfo( 1755,    0, "Nostalgium",    5, CraftAttributeInfo.Nostalgium,   CraftResource.Nostalgium,   typeof( NostalgiumIngot ),  typeof( NostalgiumOre ) ),

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
			new CraftResourceInfo( 1635, 0, "Ophidien", 2,   CraftAttributeInfo.OphidienLeather,       CraftResource.OphidienLeather,  typeof( OphidienLeather ),  typeof( OphidienHides ) ),
			new CraftResourceInfo( 2128, 0, "Arachnide", 3,  CraftAttributeInfo.ArachnideLeather,      CraftResource.ArachnideLeather, typeof( ArachnideLeather ), typeof( ArachnideHides ) ),
			new CraftResourceInfo( 2174, 0, "Dragonique", 3, CraftAttributeInfo.DragoniqueLeather,     CraftResource.DragoniqueLeather,typeof( DragoniqueLeather ),typeof( DragoniqueHides ) ),
			new CraftResourceInfo( 2118, 0, "Demoniaque", 4, CraftAttributeInfo.DemoniaqueLeather,     CraftResource.DemoniaqueLeather,typeof( DemoniaqueLeather ),typeof( DemoniaqueHides ) ),
			new CraftResourceInfo( 1940, 0, "Ancien", 5,	 CraftAttributeInfo.AncienLeather,         CraftResource.AncienLeather,    typeof( AncienLeather ),    typeof( AncienHides ) ),


		};

		private static readonly CraftResourceInfo[] m_BoneInfo = new[]
	   {
			new CraftResourceInfo( 0000, 0, "Normal", 0, CraftAttributeInfo.Blank, CraftResource.RegularBone, typeof(Bone)),
			new CraftResourceInfo( 1106, 0, "Lupus", 1, CraftAttributeInfo.LupusBone,            CraftResource.LupusBone,     typeof( LupusBone )),
			new CraftResourceInfo( 1438, 0, "Reptilien", 1, CraftAttributeInfo.ReptilienBone,      CraftResource.ReptilienBone, typeof( ReptilienBone ) ),
			new CraftResourceInfo( 1711, 0, "Geant", 2, CraftAttributeInfo.GeantBone,          CraftResource.GeantBone,     typeof( GeantBone ) ),
			new CraftResourceInfo( 1635, 0, "Ophidien", 2, CraftAttributeInfo.OphidienBone,       CraftResource.OphidienBone,  typeof( OphidienBone ) ),
			new CraftResourceInfo( 2128, 0, "Arachnide", 3, CraftAttributeInfo.ArachnideBone,      CraftResource.ArachnideBone, typeof( ArachnideBone ) ),
			new CraftResourceInfo( 2174, 0, "Dragonique", 3, CraftAttributeInfo.DragoniqueBone,     CraftResource.DragoniqueBone,typeof( DragoniqueBone ) ),
			new CraftResourceInfo( 2118, 0, "Demoniaque", 4, CraftAttributeInfo.DemoniaqueBone,     CraftResource.DemoniaqueBone,typeof( DemoniaqueBone ) ),
			new CraftResourceInfo( 1940, 0, "Ancien", 5, CraftAttributeInfo.AncienBone,         CraftResource.AncienBone,    typeof( AncienBone )),
		};

		private static readonly CraftResourceInfo[] m_WoodInfo = new[]
		{
			new CraftResourceInfo(0000, 0, "Palmier",      0,    CraftAttributeInfo.Blank,               CraftResource.PalmierWood,      typeof( PalmierBoard ),			   typeof(PalmierLog)),
			new CraftResourceInfo(1355, 0, "Érable",    0,    CraftAttributeInfo.Blank,               CraftResource.ErableWood,     typeof( ErableBoard ),    typeof(ErableLog)),
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
