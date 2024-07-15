#region References
using Server.Items;
using System;
#endregion

namespace Server.Engines.Craft
{

	#region Mondain's Legacy
	public enum SmithRecipes
	{
		// magical
		TrueSpellblade = 300,
		IcySpellblade = 301,
		FierySpellblade = 302,
		SpellbladeOfDefense = 303,
		TrueAssassinSpike = 304,
		ChargedAssassinSpike = 305,
		MagekillerAssassinSpike = 306,
		WoundingAssassinSpike = 307,
		TrueLeafblade = 308,
		Luckblade = 309,
		MagekillerLeafblade = 310,
		LeafbladeOfEase = 311,
		KnightsWarCleaver = 312,
		ButchersWarCleaver = 313,
		SerratedWarCleaver = 314,
		TrueWarCleaver = 315,
		AdventurersMachete = 316,
		OrcishMachete = 317,
		MacheteOfDefense = 318,
		DiseasedMachete = 319,
		Runesabre = 320,
		MagesRuneBlade = 321,
		RuneBladeOfKnowledge = 322,
		CorruptedRuneBlade = 323,
		TrueRadiantScimitar = 324,
		DarkglowScimitar = 325,
		IcyScimitar = 326,
		TwinklingScimitar = 327,
		GuardianAxe = 328,
		SingingAxe = 329,
		ThunderingAxe = 330,
		HeavyOrnateAxe = 331,
		RubisMace = 332, //good
		EmeraudeMace = 333, //good
		SapphireMace = 334, //good
		SilverEtchedMace = 335, //good
		BoneMachete = 336,

		// arties
		RuneCarvingKnife = 350,
		ColdForgedBlade = 351,
		OverseerSunderedBlade = 352,
		LuminousRuneBlade = 353,
		ShardTrasher = 354, //good

		// doom
		BritchesOfWarding = 355,
		GlovesOfFeudalGrip = 356,

		//K2
		[Description("Forge")]
		ForgeCust = 80011,
		[Description("Enclume")]
		EnclumeCust = 80010,

		[Description("Coffre Fort")]
		CoffreFort = 80003,
		[Description("Coffre Metal Visqueux")]
		CoffreMetalVisqueux = 80004,

		[Description("Coffre Metal Rouillé")]
		CoffreMetalRouille = 80005,
		[Description("Coffre Metal Doré")]
		CoffreMetalDore = 80006,


	

	}
	#endregion

	public class DefBlacksmithy : CraftSystem
	{
		public override SkillName MainSkill => SkillName.Blacksmith;

		//    public override int GumpTitleNumber => 1044002;

		public override string GumpTitleString => "Forge";



		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem => m_CraftSystem ?? (m_CraftSystem = new DefBlacksmithy());

		public override CraftECA ECA => CraftECA.Chance3Max;

		public override double GetChanceAtMin(CraftItem item)
		{
			if (item.NameNumber == 1157349 || item.NameNumber == 1157345) // Gloves Of FeudalGrip and Britches Of Warding
				return 0.05; // 5%

			return 0.0; // 0%
		}

		private DefBlacksmithy()
			: base(1, 1, 1.25) // base( 1, 2, 1.7 )
		{
			/*
            base( MinCraftEffect, MaxCraftEffect, Delay )
            MinCraftEffect    : The minimum number of time the mobile will play the craft effect
            MaxCraftEffect    : The maximum number of time the mobile will play the craft effect
            Delay            : The delay between each craft effect
            Example: (3, 6, 1.7) would make the mobile do the PlayCraftEffect override
            function between 3 and 6 time, with a 1.7 second delay each time.
            */
		}

		private static readonly Type typeofAnvil = typeof(AnvilAttribute);
		private static readonly Type typeofForge = typeof(ForgeAttribute);

		public static void CheckAnvilAndForge(Mobile from, int range, out bool anvil, out bool forge)
		{
			anvil = false;
			forge = false;

			Map map = from.Map;

			if (map == null)
			{
				return;
			}

			IPooledEnumerable eable = map.GetItemsInRange(from.Location, range);

			foreach (Item item in eable)
			{
				Type type = item.GetType();

				bool isAnvil = (type.IsDefined(typeofAnvil, false) || item.ItemID == 4015 || item.ItemID == 4016 ||
								item.ItemID == 0x2DD5 || item.ItemID == 0x2DD6 || (item.ItemID >= 0xA102 && item.ItemID <= 0xA10D));
				bool isForge = (type.IsDefined(typeofForge, false) || item.ItemID == 4017 ||
								(item.ItemID >= 6522 && item.ItemID <= 6569) || item.ItemID == 0x2DD8) ||
								item.ItemID == 0xA531 || item.ItemID == 0xA535;

				if (!isAnvil && !isForge)
				{
					continue;
				}

				if ((from.Z + 16) < item.Z || (item.Z + 16) < from.Z || !from.InLOS(item))
				{
					continue;
				}

				anvil = anvil || isAnvil;
				forge = forge || isForge;

				if (anvil && forge)
				{
					break;
				}
			}

			eable.Free();

			for (int x = -range; (!anvil || !forge) && x <= range; ++x)
			{
				for (int y = -range; (!anvil || !forge) && y <= range; ++y)
				{
					StaticTile[] tiles = map.Tiles.GetStaticTiles(from.X + x, from.Y + y, true);

					for (int i = 0; (!anvil || !forge) && i < tiles.Length; ++i)
					{
						int id = tiles[i].ID;

						bool isAnvil = (id == 4015 || id == 4016 || id == 0x2DD5 || id == 0x2DD6);
						bool isForge = (id == 4017 || (id >= 6522 && id <= 6569) || id == 0x2DD8);

						if (!isAnvil && !isForge)
						{
							continue;
						}

						if ((from.Z + 16) < tiles[i].Z || (tiles[i].Z + 16) < from.Z ||
							!from.InLOS(new Point3D(from.X + x, from.Y + y, tiles[i].Z + (tiles[i].Height / 2) + 1)))
						{
							continue;
						}

						anvil = anvil || isAnvil;
						forge = forge || isForge;
					}
				}
			}
		}

		public override int CanCraft(Mobile from, ITool tool, Type itemType)
		{
			int num = 0;

			if (tool == null || tool.Deleted || tool.UsesRemaining <= 0)
			{
				return 1044038; // You have worn out your tool!
			}

			if (tool is Item && !BaseTool.CheckTool((Item)tool, from))
			{
				return 1048146; // If you have a tool equipped, you must use that tool.
			}

			else if (!tool.CheckAccessible(from, ref num))
			{
				return num; // The tool must be on your person to use.
			}

			if (tool is AddonToolComponent && from.InRange(((AddonToolComponent)tool).GetWorldLocation(), 2))
			{
				return 0;
			}

			bool anvil, forge;
			CheckAnvilAndForge(from, 2, out anvil, out forge);

			if (anvil && forge)
			{
				return 0;
			}

			return 1044267; // You must be near an anvil and a forge to smith items.
		}

		public override void PlayCraftEffect(Mobile from)
		{
			// no animation, instant sound
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//    from.Animate( 9, 5, 1, true, false, 0 );
			//new InternalTimer( from ).Start();
			from.PlaySound(0x2A);
		}

		// Delay to synchronize the sound with the hit on the anvil
		private class InternalTimer : Timer
		{
			private readonly Mobile m_From;

			public InternalTimer(Mobile from)
				: base(TimeSpan.FromSeconds(0.7))
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.PlaySound(0x2A);
			}
		}

		public override int PlayEndingEffect(
		Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
		{
			if (toolBroken)
			{
				from.SendLocalizedMessage(1044038); // You have worn out your tool
			}

			if (failed)
			{
				if (lostMaterial)
				{
					return 1044043; // You failed to create the item, and some of your materials are lost.
				}

				return 1044157; // You failed to create the item, but no materials were lost.
			}

			if (quality == 0)
			{
				return 502785; // You were barely able to make this item.  It's quality is below average.
			}

			if (makersMark && quality == 2)
			{
				return 1044156; // You create an exceptional quality item and affix your maker's mark.
			}

			if (quality == 2)
			{
				return 1044155; // You create an exceptional quality item.
			}
			if (quality == 3)
			{
				from.SendMessage("Vous créez un item de qualité Épique."); ; // You create an epic quality item.
			}
			if (quality == 4)
			{
				from.SendMessage("Vous créez un item de qualité Légendaire."); ; // You create a legendary quality item.
			}
			return 1044154; // You create the item.
		}

		public override void InitCraftList()
		{
			/*
            Synthax for a SIMPLE craft item
            AddCraft( ObjectType, Group, MinSkill, MaxSkill, ResourceType, Amount, Message )
            ObjectType        : The type of the object you want to add to the build list.
            Group            : The group in wich the object will be showed in the craft menu.
            MinSkill        : The minimum of skill value
            MaxSkill        : The maximum of skill value
            ResourceType    : The type of the resource the mobile need to create the item
            Amount            : The amount of the ResourceType it need to create the item
            Message            : String or Int for Localized.  The message that will be sent to the mobile, if the specified resource is missing.
            Synthax for a COMPLEXE craft item.  A complexe item is an item that need either more than
            only one skill, or more than only one resource.
            Coming soon....
            */

			int index;
			#region "Armure Légère"
			AddCraft(typeof(RingmailGloves), "Armure Légère", "Gants d’anneaux", 20.0, 40.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(RingmailGorget), "Armure Légère", "Gorgerin d’anneaux", 22.0, 42.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(RingmailLegs), "Armure Légère", "Jambes d’anneaux", 28.0, 48.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(RingmailArms), "Armure Légère", "Brassard d’anneaux", 25.0, 45.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(RingmailChest), "Armure Légère", "Torse d’anneaux", 32.0, 52.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronMaille2), "Armure Légère", "Torse d’anneaux fins", 32.0, 52.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(JambiereMaille2), "Armure Légère", "Jambière d’anneaux fins", 28.0, 48.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(BrassardMaille), "Armure Légère", "Brassard d’anneaux fins", 25.0, 45.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Bascinet), "Armure Légère", "Bascinet", 40.0, 60.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Helmet), "Armure Légère", "Casque", 35.0, 55.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(NorseHelm), "Armure Légère", "Haume Nordique", 40.0, 60.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion


			#region "Armure Barbaresque"
			AddCraft(typeof(CasqueMailleBarbare), "Armure Légère", "Casque Barbaresque", 20.0, 40.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GorgetMailleBarbare), "Armure Légère", "Gorgerin Barbaresque", 22.0, 42.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GantMailleBarbare), "Armure Légère", "Gants Barbaresque", 25.0, 45.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(BrassardMailleBarbare), "Armure Légère", "Brassard Barbaresque", 25.0, 45.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(JambiereMailleBarbare), "Armure Légère", "Jambes Barbaresque", 28.0, 48.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronMailleBarbare), "Armure Légère", "Torse Barbaresque", 32.0, 52.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Armure Broigne"
			AddCraft(typeof(CasqueMailleRenforce), "Armure Légère", "Casque Broigne", 20.0, 40.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GorgetMailleRenforce), "Armure Légère", "Gorgerin Broigne", 22.0, 42.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GantMailleRenforce), "Armure Légère", "Gants Broigne", 25.0, 45.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(BrassardMaillerRenforce), "Armure Légère", "Brassard Broigne", 25.0, 45.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(JambiereMailleRenforce), "Armure Légère", "Jambes Broigne", 28.0, 48.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronMailleRenforce), "Armure Légère", "Torse Broigne", 32.0, 52.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion


			#region "Armure Intermédiaire"
			AddCraft(typeof(ChainCoif), "Armure Intermédiaire", "Coiffe de mailles", 40.0, 60.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(CasqueKorain), "Armure Intermédiaire", "Casque Korain", 40.0, 60.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(ChainGorget), "Armure Intermédiaire", "Gorgerin de mailles", 42.0, 62.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(ChainmailArms), "Armure Intermédiaire", "Brassards de mailles", 45.0, 65.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(ChainLegs), "Armure Intermédiaire", "Jambes de mailles", 48.0, 68.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(ChainChest), "Armure Intermédiaire", "Tunique de maille", 50.0, 70.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(CloseHelm), "Armure Intermédiaire", "Casque fermé", 45.0, 65.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");


			AddCraft(typeof(CasqueMaille), "Armure Intermédiaire", "Coiffe de mailles matelassée", 40.0, 60.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GantsMaille), "Armure Intermédiaire", "Gants de mailles matelassées", 43.0, 63.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(JambiereMaille), "Armure Intermédiaire", "Jambière de mailles matelassée", 48.0, 68.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronMaille), "Armure Intermédiaire", "Tunique de mailles matelassée", 50.0, 70.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Armure Lourde"
			AddCraft(typeof(PlateArms), "Armure Lourde", "Brassards de plaque", 45.0, 65.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlateGloves), "Armure Lourde", "Gants de plaque", 42.0, 62.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlateGorget), "Armure Lourde", "Gorgerin de plaque", 39.0, 59.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlateLegs), "Armure Lourde", "Jambières de plaque", 48.0, 68.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlateChest), "Armure Lourde", "Torse de plaque", 52.0, 72.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(FemalePlateChest), "Armure Lourde", "Torse de plaque femme", 51.0, 71.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(BrassardChaos), "Armure Lourde", "Brassard du Chaos", 45.0, 65.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronChaos), "Armure Lourde", "Plastron du Chaos", 52.0, 72.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(BrassardDecoratif), "Armure Lourde", "Brassard Décoratif", 45.0, 65.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(JambiereDecoratif), "Armure Lourde", "Jambière Décoratif", 48.0, 68.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronDecoratif), "Armure Lourde", "Plastron Décoratif", 52.0, 72.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(BottesElfique), "Armure Lourde", "Bottes Elfique", 41.0, 61.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GantsElfique), "Armure Lourde", "Gants Elfique", 42.0, 62.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GorgetElfique), "Armure Lourde", "Gorget Elfique", 39.0, 59.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronElfique), "Armure Lourde", "Plastron Elfique", 52.0, 72.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronPlaque), "Armure Lourde", "Harnois", 52.0, 72.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronPlaqueDoree), "Armure Lourde", "Plastron de plaque Dorée", 52.0, 72.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlateHelm), "Armure Lourde", "Casque de Plaque", 50.0, 70.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(CasqueChaos), "Armure Lourde", "Casque du Chaos", 50.0, 70.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Armure demi-plaque"
			AddCraft(typeof(BrassardEmbellit), "Armure Intermédiaire", "Brassards de demi-plaque", 35.0, 55.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GantsEmbellit), "Armure Intermédiaire", "Gants de demi-plaque", 32.0, 52.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GorgetEmbellit), "Armure Intermédiaire", "Gorgerin de demi-plaque", 29.0, 49.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(JambiereEmbellit), "Armure Intermédiaire", "Jambières de demi-plaque", 38.0, 58.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(CasqueEmbellit), "Armure Intermédiaire", "Casque de demi-plaque", 40.0, 60.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronEmbellit), "Armure Intermédiaire", "Torse de demi-plaque", 42.0, 62.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Armure Cuirasse"
			AddCraft(typeof(BrassardSemiMaille), "Armure Intermédiaire", "Brassards de Cuirasse", 35.0, 55.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GantsSemiMaille), "Armure Intermédiaire", "Gants de Cuirasse", 32.0, 52.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GorgetSemiMaille), "Armure Intermédiaire", "Gorgerin de Cuirasse", 29.0, 49.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(JambiereSemiMaille), "Armure Intermédiaire", "Jambières de Cuirasse", 38.0, 58.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(CasqueSemiMaille), "Armure Intermédiaire", "Casque de Cuirasse", 40.0, 60.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronSemiMaille), "Armure Intermédiaire", "Torse de Cuirasse", 42.0, 62.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Armure Dragon"
			AddCraft(typeof(BrassardDragon), "Armure Lourde", "Brassards Draconique", 45.0, 65.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GantsDragon), "Armure Lourde", "Gants Draconique", 42.0, 62.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GorgetDragon), "Armure Lourde", "Gorgerin Draconique", 39.0, 59.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(JambiereDragon), "Armure Lourde", "Jambières Draconique", 48.0, 68.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(CasqueDragon), "Armure Lourde", "Casque Draconique", 50.0, 70.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronDragon), "Armure Lourde", "Torse Draconique", 52.0, 72.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Armure Daedric"
			AddCraft(typeof(BrassardDaedric), "Armure Lourde", "Brassards Daedric", 45.0, 65.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GantsDaedric), "Armure Lourde", "Gants Daedric", 42.0, 62.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GorgetDaedric), "Armure Lourde", "Gorgerin Daedric", 39.0, 59.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(JambiereDaedric), "Armure Lourde", "Jambières Daedric", 48.0, 68.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(CasqueDaedric), "Armure Lourde", "Casque Daedric", 50.0, 70.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronDaedric), "Armure Lourde", "Torse Daedric", 52.0, 72.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Armure Vieillie"
			AddCraft(typeof(BrassardVieillit), "Armure Lourde", "Brassards Ancien", 45.0, 65.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GantsVieillit), "Armure Lourde", "Gants Ancien", 42.0, 62.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GorgetVieillit), "Armure Lourde", "Gorgerin Ancien", 39.0, 59.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(JambiereViellit), "Armure Lourde", "Jambières Ancien", 48.0, 68.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(CasqueVieillit), "Armure Lourde", "Casque Ancien", 50.0, 70.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(PlastronViellit), "Armure Lourde", "Torse Ancien", 52.0, 72.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion


			#region "Boucliers"
			AddCraft(typeof(Buckler), "Boucliers", "Bouclier (0)", 32.0, 52.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(SmallPlateShield), "Boucliers", "Targe (1)", 32.0, 52.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(WoodenKiteShield), "Boucliers", "La pointe (2)", 41.0, 61.0, typeof(IronIngot), "Fer", 8, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(MediumPlateShield), "Boucliers", "Rondache (2)", 49.0, 69.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(BronzeShield), "Boucliers", "Rondache résonnante (3)", 36.0, 56.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(EcuBois), "Boucliers", "Écu de bois (3)", 32.0, 52.0, typeof(IronIngot), "Fer", 8, "Vous n'avez pas suffisament de lingot de Fer");
			//AddCraft(typeof(BouclierRond2), "Boucliers", "Bouclier Rond (3)", 32.0, 52.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Targe3), "Boucliers", "Targe renforcée (3)", 41.0, 61.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Rondache), "Boucliers", "Rondache (4)", 41.0, 41.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(MetalKiteShield), "Boucliers", "Blason (4)", 45.0, 65.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(BouclierRond), "Boucliers", "Bouclier Rond (4)", 41.0, 61.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(ChaosShield), "Boucliers", "Targe décorée (4)", 41.0, 61.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(HeaterShield), "Boucliers", "Muraille (5)", 49.0, 49.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Pavois), "Boucliers", "Pavois (5)", 32.0, 52.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Targe), "Boucliers", "Targe Bicolore (5)", 41.0, 61.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(OrderShield), "Boucliers", "Égide (5)", 41.0, 61.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(MetalShield), "Boucliers", "Rampart (5)", 39.0, 59.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");

			AddCraft(typeof(EcuLong), "Boucliers", "Écu Long (6)", 41.0, 61.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Pavois2), "Boucliers", "Pavois Décoratif (6)", 41.0, 61.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Targe2), "Boucliers", "Rondache Colimaçon (6)", 41.0, 61.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Armes de poings"

			AddCraft(typeof(DoubleLames), "Armes de poings", "Double Lames de poing", 0.0, 25.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Sai), "Armes de poings", "Sai", 10.0, 35.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Kama), "Armes de poings", "Kama", 10.0, 35.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Nunchakuonehand), "Armes de poings", "Baton à une main", 20.0, 55.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Tekagi), "Armes de poings", "Griffes", 20.0, 55.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(AnneauxCombat), "Armes de poings", "Anneaux de Combat", 20.0, 55.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(LameAffutee), "Armes de poings", "Lame Afutée 1 main", 35.0, 65.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GriffesCombat), "Armes de poings", "Griffes de Combat", 35.0, 65.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(KamaKuya), "Armes de poings", "Kama Kuya", 35.0, 65.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(LameCirculaire), "Armes de poings", "Lame Circulaire", 50.0, 75.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Kama1), "Armes de poings", "Kama", 50.0, 75.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion


			#region "Dagues"
			AddCraft(typeof(Dagger), "Dagues", "Dague", 0.0, 40.0, typeof(IronIngot), "Fer", 3, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(ElvenSpellblade), "Dagues", "Égorgeuse", 20.0, 40.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(AssassinSpike), "Dagues", "Épineuse", 25.0, 45.0, typeof(IronIngot), "Fer", 9, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Leafblade), "Dagues", "Coupe-gorge", 30.0, 50.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(SkinningKnife), "Dagues", "Couteau à dépecer", 0.0, 20.0, typeof(IronIngot), "Fer", 2, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Cleaver), "Dagues", "Couperet", 0.0, 20.0, typeof(IronIngot), "Fer", 3, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(ButcherKnife), "Dagues", "Couteau de boucher", 0.0, 20.0, typeof(IronIngot), "Fer", 2, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Épées"
			AddCraft(typeof(BoneHarvester), "Épées", "Serpe", 10.0, 35.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Broadsword), "Épées", "Épée courte", 10.0, 35.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Cutlass), "Épées", "Sabre", 10.0, 35.0, typeof(IronIngot), "Fer", 8, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Katana), "Épées", "Katana", 10.0, 35.0, typeof(IronIngot), "Fer", 8, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Longsword), "Épées", "Épée longue", 10.0, 35.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Scimitar), "Épées", "Cimeterre", 10.0, 35.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(VikingSword), "Épées", "Épée Lourde", 10.0, 35.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(EpeeCourte), "Épées", "Épée Courte", 10.0, 35.0, typeof(IronIngot), "Fer", 8, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(SabreLuxe), "Épées", "Sabre de Luxe", 30.0, 55.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(EpeeBatardeLuxe), "Épées", "Épée bâtarde de luxe", 30.0, 55.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(EpeeDoubleTranchant), "Épées", "Épée à Double Tranchants", 30.0, 55.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(EpeeLongue), "Épées", "Épée Afuitée", 30.0, 55.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(EpeeBatarde), "Épées", "Épée bâtarde", 45.0, 70.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(EpeeDeuxMains), "Épées", "Épée Deux Mains", 45.0, 70.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Runire), "Épées", "Runire", 45.0, 70.0, typeof(IronIngot), "Fer", 8, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(NoDachi), "Épées", "Éclat solaire", 45.0, 70.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Wakizashi), "Épées", "Surineur", 45.0, 70.0, typeof(IronIngot), "Fer", 8, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(RadiantScimitar), "Épées", "Cimeterre infini", 45.0, 70.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(RuneBlade), "Épées", "Lame vorpal", 45.0, 70.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(ElvenMachete), "Épées", "Machette runique", 45.0, 70.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(DoubleEpee), "Épées", "Double épée", 45.0, 70.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(CrescentBlade), "Épées", "Épée Croissant", 45.0, 70.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(WakizashiLong), "Épées", "Wakizashi Long", 45.0, 70.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Daisho), "Épées", "Les jumelles", 45.0, 70.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Haches"
			AddCraft(typeof(Axe), "Haches", "Hache simple", 10.0, 35.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(BattleAxe), "Haches", "Hache de guerre", 10.0, 35.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(DoubleAxe), "Haches", "Hache double", 10.0, 35.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(ExecutionersAxe), "Haches", "Hachette", 30.0, 55.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(LargeBattleAxe), "Haches", "Hache de bataille", 30.0, 55.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(TwoHandedAxe), "Haches", "Hache à deux mains", 30.0, 55.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(WarAxe), "Haches", "Tranchar", 30.0, 55.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GrandeHache), "Haches", "Éventreuse", 45.0, 70.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GrandeHacheDouble), "Haches", "Francisque", 45.0, 70.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(HacheDouble), "Haches", "Trombe", 45.0, 70.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(HAchePique), "Haches", "Barbelé", 45.0, 70.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(HacheDoublePiques), "Haches", "Exécutrice", 45.0, 70.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(HacheDoubleNaine), "Haches", "Gardienne", 45.0, 70.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(OrnateAxe), "Haches", "Hache ornée", 45.0, 70.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(DualShortAxes), "Haches", "Double hache courte", 45.0, 70.0, typeof(IronIngot), "Fer", 24, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(DoubleHachette), "Haches", "Double Hachette", 45.0, 70.0, typeof(IronIngot), "Fer", 15, "Vous n'avez pas suffisament de lingot de Fer");


			#endregion

			#region Hallebarde
			index = AddCraft(typeof(Pitchfork), "Hallebardes", "Fourche", 10.0, 35.0, typeof(IronIngot), "Iron ingot", 5, "You do not have enough iron ingots to make that.");
			AddCraft(typeof(Bardiche), "Hallebardes", "Bardiche", 40.0, 60.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Hellebarde), "Hallebardes", "Hallebarde simple", 50.0, 70.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(BladedStaff), "Hallebardes", "Bardiche Simple", 55.0, 75.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(DoubleBladedStaff), "Hallebardes", "Bardiche double lames", 80.0, 100.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Halberd), "Hallebardes", "Hallebarde", 85.0, 105.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Lancer"
			AddCraft(typeof(Shuriken), "Lancer", "Shuriken", 40.0, 80.0, typeof(IronIngot), 1044036, 5, 1044037);
			AddCraft(typeof(Boomerang), "Lancer", "Boomerang", 40.0, 80.0, typeof(IronIngot), 1044036, 5, 1044037);
			AddCraft(typeof(Cyclone), "Lancer", "Cyclone", 40.0, 80.0, typeof(IronIngot), 1044036, 9, 1044037);
			AddCraft(typeof(SoulGlaive), "Lancer", "Étoile", 40.0, 80.0, typeof(IronIngot), 1044036, 9, 1044037);

			#endregion

			#region "Lances"
			AddCraft(typeof(Lance), "Lances", "Lance", 10.0, 35.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Pike), "Lances", "Pique", 10.0, 35.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(ShortSpear), "Lances", "Lance courte", 20.0, 45.0, typeof(IronIngot), "Fer", 6, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Scythe), "Lances", "Scythe", 30.0, 55.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Spear), "Lances", "Lance de guerre", 35.0, 60.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Epieu), "Lances", "Épieu", 40.0, 65.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GrandeFourche), "Lances", "Grande Fourche", 43.0, 75.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(JavelotLuxe), "Lances", "Javelot de Guerre", 43.0, 75.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Trident), "Lances", "Trident", 43.0, 75.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(WarFork), "Lances", "Fourche de guerre", 43.0, 75.0, typeof(IronIngot), "Fer", 12, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion


			#region "Masses et marteaux"
			AddCraft(typeof(HammerPick), "Masses et marteaux", "Marteau à pointes", 10.0, 35.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Mace), "Masses et marteaux", "Masse", 10.0, 35.0, typeof(IronIngot), "Fer", 6, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Maul), "Masses et marteaux", "Maul", 10.0, 35.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Scepter), "Masses et marteaux", "Sceptre", 30.0, 55.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(WarMace), "Masses et marteaux", "Masse de guerre", 30.0, 55.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(GrandeMasse), "Masses et marteaux", "Grande Masse", 30.0, 55.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(MassuePointes), "Masses et marteaux", "Massue à Pointes", 30.0, 55.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Massue), "Masses et marteaux", "Massue", 30.0, 55.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(MorgensternBoules), "Masses et marteaux", "Morgenstern à Boules", 30.0, 55.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(MorgensternPointes), "Masses et marteaux", "Morgenstern à Pointes", 30.0, 55.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Tessen), "Masses et marteaux", "Tessen", 40.0, 70.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(DiamantMace), "Masses et marteaux", "Masse diamant", 40.0, 70.0, typeof(IronIngot), "Fer", 20, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(WarHammer), "Masses et marteaux", "Dispenseur", 40.0, 70.0, typeof(IronIngot), "Fer", 16, "Vous n'avez pas suffisament de lingot de Fer");
			//AddCraft(typeof(Maul), "Masses et marteaux", "Ogrillonne", 40.0, 70.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(MarteauPointes), "Masses et marteaux", "Étoile du matin", 45.0, 70.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Marteau), "Masses et marteaux", "Marteau", 45.0, 70.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(MassueClous), "Masses et marteaux", "Massue à Clous", 45.0, 70.0, typeof(IronIngot), "Fer", 14, "Vous n'avez pas suffisament de lingot de Fer");



			#endregion

			#region "Rapières et Estoc"
			AddCraft(typeof(Rapiere), "Rapières et Estoc", "Rapière", 10.0, 35.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(RapiereLuxe), "Rapières et Estoc", "Rapière de Luxe", 20.0, 50.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(RapiereDecoree), "Rapières et Estoc", "Rapière Décorée", 30.0, 60.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Astoria), "Rapières et Estoc", "Astoria", 30.0, 60.0, typeof(IronIngot), "Fer", 10, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Kryss), "Rapières et Estoc", "Kryss", 40.0, 70.0, typeof(IronIngot), "Fer", 8, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(WarCleaver), "Rapières et Estoc", "Éclat lunaire", 40.0, 70.0, typeof(IronIngot), "Fer", 18, "Vous n'avez pas suffisament de lingot de Fer");
			AddCraft(typeof(Lajatang), "Rapières et Estoc", "Croissant de lune", 40.0, 70.0, typeof(IronIngot), "Fer", 25, "Vous n'avez pas suffisament de lingot de Fer");
			#endregion

			#region "Divers"
			index = AddCraft(typeof(IronIngotResourceCrate), "Divers", "Caisse de ressource", 10.0, 60.0, typeof(IronIngot), 1044036, 150, 1044037);

			index = AddCraft(typeof(Cannonball), "Divers", "Boulet de Canon", 10.0, 60.0, typeof(IronIngot), 1044036, 5, 1044037);
			index = AddCraft(typeof(Grapeshot), "Divers", "Boulet Avancé", 15.0, 70.0, typeof(IronIngot), 1044036, 5, 1044037);
			AddRes(index, typeof(Cloth), 1044286, 2, 1044287);
			index = AddCraft(typeof(LightShipCannonDeed), "Divers", "Canon Léger", 75.0, 110.0, typeof(IronIngot), 1044036, 500, 1044037);
			index = AddCraft(typeof(HeavyShipCannonDeed), "Divers", "Canon Lourd", 90.0, 110.0, typeof(IronIngot), 1044036, 800, 1044037);
			index = AddCraft(typeof(Ancre), "Divers", "Ancre", 90.0, 110.0, typeof(IronIngot), 1044036, 15, 1044037);
			index = AddCraft(typeof(CoffreFort), "Divers", "Coffre Fort", 80.0, 115.0, typeof(IronIngot), 1044036, 25, 1044037);
			AddRecipe(index, (int)SmithRecipes.CoffreFort);


			index = AddCraft(typeof(AnvilEastDeed), "Divers", "Enclume (Est)", 52.0, 100.0, typeof(IronIngot), "Fer", 100, "Vous n'avez pas suffisament de lingot de Fer");
			//AddRecipe(index, (int)SmithRecipes.EnclumeCust);


			index = AddCraft(typeof(AnvilSouthDeed), "Divers", "Enclume (Sud)", 52.0, 100.0, typeof(IronIngot), "Fer", 100, "Vous n'avez pas suffisament de lingot de Fer");
			//AddRecipe(index, (int)SmithRecipes.EnclumeCust);


			index = AddCraft(typeof(SmallForgeDeed), "Divers", "Petite Forge", 52.0, 100.0, typeof(IronIngot), "Fer", 150, "Vous n'avez pas suffisament de lingot de Fer");
			//AddRecipe(index, (int)SmithRecipes.ForgeCust);


			index = AddCraft(typeof(CoffreMetalVisqueux), "Divers", "Coffre Métal Visqueux", 55.0, 85.0, typeof(IronIngot), "Fer", 30, "Vous n'avez pas suffisament de lingot de Fer");
			AddRecipe(index, (int)SmithRecipes.CoffreMetalVisqueux);


			index = AddCraft(typeof(CoffreMetalRouille), "Divers", "Coffre Métal Rouillé", 60.0, 90.0, typeof(IronIngot), "Fer", 30, "Vous n'avez pas suffisament de lingot de Fer");
			AddRecipe(index, (int)SmithRecipes.CoffreMetalRouille);


			index = AddCraft(typeof(CoffreMetalDore), "Divers", "Coffre Metal Doré", 65.0, 95.0, typeof(IronIngot), "Fer", 30, "Vous n'avez pas suffisament de lingot de Fer");
			AddRecipe(index, (int)SmithRecipes.CoffreMetalDore);


			index = AddCraft(typeof(LargeForgeEastDeed), "Divers", "Grande Forge (Est)", 72.0, 120.0, typeof(IronIngot), "Fer", 200, "Vous n'avez pas suffisament de lingot de Fer");
			//AddRecipe(index, (int)SmithRecipes.ForgeCust);


			index = AddCraft(typeof(LargeForgeSouthDeed), "Divers", "Grande Forge (Sud)", 72.0, 120.0, typeof(IronIngot), "Fer", 200, "Vous n'avez pas suffisament de lingot de Fer");
			//AddRecipe(index, (int)SmithRecipes.ForgeCust);

			#endregion

			#region Alliages


			index = AddCraft(typeof(DraconyrIngot), "Alliages", "Lingot de Draconyr", 80, 110, typeof(IronIngot), "Lingot de Fer", 4, "You do not have enough iron ingots to make that.");
			AddRes(index, typeof(CopperIngot), "Lingot de Cuivre", 3, "You do not have enough Cuivre ingot to make that.");
			AddRes(index, typeof(SonneIngot), "Lingot de Sonne", 2, "You do not have enough Sonne ingot to make that.");
			AddRes(index, typeof(AcierIngot), "Lingot d'Acier", 1, "You do not have enough Acier ingot to make that.");



			index = AddCraft(typeof(HeptazionIngot), "Alliages", "Lingot d'Heptazion", 80, 110, typeof(IronIngot), "Lingot de Fer", 4, "You do not have enough iron ingots to make that.");
			AddRes(index, typeof(CopperIngot), "Lingot de Cuivre", 3, "You do not have enough Cuivre ingot to make that.");
			AddRes(index, typeof(ArgentIngot), "Lingot d'Argent", 2, "You do not have enough Argent ingot to make that.");
			AddRes(index, typeof(DurianIngot), "Lingot de Durian", 1, "You do not have enough Durian ingot to make that.");


			index = AddCraft(typeof(OceanisIngot), "Alliages", "Lingot d'Océanis", 80, 110, typeof(IronIngot), "Lingot de Fer", 4, "You do not have enough iron ingots to make that.");
			AddRes(index, typeof(CopperIngot), "Lingot de Cuivre", 3, "You do not have enough Cuivre ingot to make that.");
			AddRes(index, typeof(BorealeIngot), "Lingot de Boréale", 2, "You do not have enough Boreale ingot to make that.");
			AddRes(index, typeof(JolinarIngot), "Lingot de Équilibrum", 1, "You do not have enough Equilibrum ingot to make that.");

			index = AddCraft(typeof(BraziumIngot), "Alliages", "Lingot de Brazium", 80, 110, typeof(IronIngot), "Lingot de Fer", 4, "You do not have enough bloodirium ingots to make that.");
			AddRes(index, typeof(BronzeIngot), "Lingot de Bronze", 3, "You do not have enough Bronze ingot to make that.");
			AddRes(index, typeof(ChrysteliarIngot), "Lingot de Chrysteliar", 2, "You do not have enough Chrysteliar ingot to make that.");
			AddRes(index, typeof(GoldIngot), "Lingot d'Or", 1, "You do not have enough Or ingot to make that.");


			index = AddCraft(typeof(LuneriumIngot), "Alliages", "Lingot de Lunérium", 80, 110, typeof(IronIngot), "Lingot de Fer", 4, "You do not have enough herbrosite ingots to make that.");
			AddRes(index, typeof(BronzeIngot), "Lingot Bronze", 3, "You do not have enough Bronze ingot to make that.");
			AddRes(index, typeof(GlaciasIngot), "Lingot de Glacias", 2, "You do not have enough Glacias ingot to make that.");
			AddRes(index, typeof(JolinarIngot), "Lingot de Jolinar", 1, "You do not have enough Jolinar ingot to make that.");


			index = AddCraft(typeof(MarinarIngot), "Alliages", "Lingot de Marinar", 80, 110, typeof(IronIngot), "Lingot de Fer", 4, "You do not have enough maritium ingots to make that.");
			AddRes(index, typeof(BronzeIngot), "Lingot de Bronze", 3, "You do not have enough Bronze ingot to make that.");
			AddRes(index, typeof(LithiarIngot), "Lingot de Lithiar", 2, "You do not have enough Lithiar ingot to make that.");
			AddRes(index, typeof(JusticiumIngot), "Lingot de Justicium", 1, "You do not have enough Justicium ingot to make that.");

			index = AddCraft(typeof(NostalgiumIngot), "Alliages", "Lingots de Nostalgium", 90, 120, typeof(IronIngot), "Lingot de Fer", 4, "You do not have enough brazium ingots to make that.");
			AddRes(index, typeof(DraconyrIngot), "Lingot de Draconyr", 3, "You do not have enough Draconyr ingot to make that.");
			AddRes(index, typeof(BraziumIngot), "Lingot de Brazium", 2, "You do not have enough Brazium ingot to make that.");
			AddRes(index, typeof(MarinarIngot), "Lingot de Marinar", 1, "You do not have enough Marinar ingot to make that.");


			#endregion


			// Set the overridable material
			SetSubRes(typeof(IronIngot), 1044022);

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes(typeof(IronIngot), "Fer", 0.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(BronzeIngot), "Bronze", 0.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(CopperIngot), "Copper", 0.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(SonneIngot), "Sonne", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(ArgentIngot), "Argent", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(BorealeIngot), "Boréale", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(ChrysteliarIngot), "Chrysteliar", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(GlaciasIngot), "Glacias", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(LithiarIngot), "Lithiar", 20.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(AcierIngot), "Acier", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(DurianIngot), "Durian", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(EquilibrumIngot), "Équilibrum", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(GoldIngot), "Or", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(JolinarIngot), "Jolinar", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(JusticiumIngot), "Justicium", 40.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(AbyssiumIngot), "Abyssium", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(BloodiriumIngot), "Bloodirium", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(HerbrositeIngot), "Herbrosite", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(KhandariumIngot), "Khandarium", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(MytherilIngot), "Mytheril", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(SombralirIngot), "Sombralir", 60.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(DraconyrIngot), "Draconyr", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(HeptazionIngot), "Heptazion", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(OceanisIngot), "Océanis", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(BraziumIngot), "Brazium", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(LuneriumIngot), "Lunerium", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(MarinarIngot), "Marinar", 80.0, "Vous n'avez pas les compétences requises pour forger ce métal.");
			AddSubRes(typeof(NostalgiumIngot), "Nostalgium", 100.0, "Vous n'avez pas les compétences requises pour forger ce métal.");

			Resmelt = true;
			Repair = true;
			MarkOption = true;
			CanEnhance = true;
			CanAlter = true;
		}
	}

	public class ForgeAttribute : Attribute
	{ }

	public class AnvilAttribute : Attribute
	{ }
}

