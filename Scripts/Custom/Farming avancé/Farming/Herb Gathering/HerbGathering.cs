using System;
using Server;
using Server.Items;

namespace Server.Engines.Harvest
{
	public class HerbGathering : HarvestSystem
	{
		private static HerbGathering m_System;
		public static HerbGathering System { get { if ( m_System == null ) m_System = new HerbGathering(); return m_System; } }

		private HarvestDefinition m_Definition;

		public HarvestDefinition Definition { get{ return m_Definition; } }

		private HerbGathering()
		{
			HarvestResource[] res;
			HarvestVein[] veins;

			#region HerbGathering
			HarvestDefinition herb = new HarvestDefinition();

			herb.BankWidth = 1;
			herb.BankHeight = 1;

			herb.MinTotal = 1;
			herb.MaxTotal = 3;

			herb.MinRespawn = TimeSpan.FromMinutes( 5.0 );
			herb.MaxRespawn = TimeSpan.FromMinutes( 15.0 );

			herb.Skill = SkillName.Cooking;

			herb.Tiles = m_PlantTiles;

			herb.MaxRange = 2;

			herb.ConsumedPerHarvest = 1;
			herb.ConsumedPerFeluccaHarvest = 1;

			herb.EffectActions = new int[]{ 9 };
			herb.EffectSounds = new int[]{ 0x4F };
			herb.EffectCounts = new int[]{ 1, 1, 1, 1, 2 };
			herb.EffectDelay = TimeSpan.FromSeconds( 5.0 );
			herb.EffectSoundDelay = TimeSpan.FromSeconds( 0.7 );

			herb.NoResourcesMessage = "Il n'y a rien à récolter ici.";
			herb.FailMessage = "Vous ne retirez aucune herbe ici.";
			herb.OutOfRangeMessage = 500446;
			herb.PackFullMessage = "Votre sac est plein.";
			herb.ToolBrokeMessage = "Vous brisez votre serpe.";

			res = new HarvestResource[]
			{
				new HarvestResource( 10.0, 10.0, 10.0,  "Vous déposez un peu de sauge dans votre sac.", typeof( Sage ) ),
				new HarvestResource( 30.0, 30.0, 30.0, "Vous déposez un peu de acacia dans votre sac.", typeof( Acacia ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de anis dans votre sac.", typeof( Anise ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de basilic dans votre sac.", typeof( Basil ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de feuille de laurier dans votre sac.", typeof( BayLeaf ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de camomille dans votre sac.", typeof( Chamomile ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de cumin dans votre sac.", typeof( Caraway ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de coriandre dans votre sac.", typeof( Cilantro ) ),
				new HarvestResource( 60.0, 60.0, 60.0, "Vous déposez un peu de cannelle dans votre sac.", typeof( Cinnamon ) ),
				new HarvestResource( 60.0, 60.0, 60.0, "Vous déposez un peu de clou de girofle dans votre sac.", typeof( Clove ) ),
				new HarvestResource( 30.0, 30.0, 30.0, "Vous déposez un peu de résine dans votre sac.", typeof( Copal ) ),
				new HarvestResource( 60.0, 60.0, 60.0, "Vous déposez un peu de cerfeuil dans votre sac.", typeof( Coriander ) ),
				new HarvestResource( 20.0, 20.0, 20.0, "Vous déposez un peu de aneth dans votre sac.", typeof( Dill ) ),
				new HarvestResource( 30.0, 30.0, 30.0, "Vous déposez un peu de sang de dragon dans votre sac.", typeof( Dragonsblood ) ),
				new HarvestResource( 30.0, 30.0, 30.0, "Vous déposez un peu de encens dans votre sac.", typeof( Frankincense ) ),
				new HarvestResource( 20.0, 20.0, 20.0, "Vous déposez un peu de lavande dans votre sac.", typeof( Lavender ) ),
				new HarvestResource( 30.0, 30.0, 30.0, "Vous déposez un peu de marjolaine dans votre sac.", typeof( Marjoram ) ),
				new HarvestResource( 20.0, 20.0, 20.0, "Vous déposez un peu de reine-des-prés dans votre sac.", typeof( Meadowsweet ) ),
				new HarvestResource( 10.0, 10.0, 10.0,  "Vous déposez un peu de menthe dans votre sac.", typeof( Mint ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de armoise commune dans votre sac.", typeof( Mugwort ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de graine de moutarde dans votre sac.", typeof( Mustard ) ),
				new HarvestResource( 30.0, 30.0, 30.0, "Vous déposez un peu de gomme d'épinette dans votre sac.", typeof( Myrrh ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de olive dans votre sac.", typeof( Olive ) ),
				new HarvestResource( 10.0, 10.0, 10.0,  "Vous déposez un peu de origan dans votre sac.", typeof( Oregano ) ),
				new HarvestResource( 30.0, 30.0, 30.0, "Vous déposez un peu de racine d'orris dans votre sac.", typeof( Orris ) ),
				new HarvestResource( 30.0, 30.0, 30.0, "Vous déposez un peu de patchouli dans votre sac.", typeof( Patchouli ) ),
				new HarvestResource( 10.0, 10.0, 10.0,  "Vous déposez un peu de grain de poivre dans votre sac.", typeof( Peppercorn ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de rose dans votre sac.", typeof( RoseHerb ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de romarin dans votre sac.", typeof( Rosemary ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de safran dans votre sac.", typeof( Saffron ) ),
				new HarvestResource( 30.0, 30.0, 30.0, "Vous déposez un peu de bois de santal dans votre sac.", typeof( Sandelwood ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de orme glissante dans votre sac.", typeof( SlipperyElm ) ),
				new HarvestResource( 10.0, 10.0, 10.0,  "Vous déposez un peu de thym dans votre sac.", typeof( Thyme ) ),
				new HarvestResource( 30.0, 30.0, 30.0, "Vous déposez un peu de valériane dans votre sac.", typeof( Valerian ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de écorce de saule dans votre sac.", typeof( WillowBark ) ),
				new HarvestResource( 50.0, 50.0, 50.0, "Vous déposez un peu de baie tribale dans votre sac.", typeof( TribalBerry ) )
			};

			veins = new HarvestVein[]
			{
				new HarvestVein( 5.0, 0.0, res[0], null ),
				new HarvestVein( 5.0, 0.1, res[1], res[0] ),
				new HarvestVein( 5.0, 0.1, res[2], res[0] ),
				new HarvestVein( 5.0, 0.1, res[3], res[0] ),
				new HarvestVein( 5.0, 0.1, res[4], res[0] ),
				new HarvestVein( 5.0, 0.1, res[5], res[0] ),
				new HarvestVein( 5.0, 0.1, res[6], res[0] ),
				new HarvestVein( 5.0, 0.1, res[7], res[0] ),
				new HarvestVein( 5.0, 0.1, res[8], res[0] ),
				new HarvestVein( 5.0, 0.1, res[9], res[0] ),
				new HarvestVein( 5.0, 0.1, res[10], res[0] ),
				new HarvestVein( 5.0, 0.1, res[11], res[0] ),
				new HarvestVein( 5.0, 0.1, res[12], res[0] ),
				new HarvestVein( 5.0, 0.1, res[13], res[0] ),
				new HarvestVein( 5.0, 0.1, res[14], res[0] ),
				new HarvestVein( 5.0, 0.1, res[15], res[0] ),
				new HarvestVein( 5.0, 0.1, res[16], res[0] ),
				new HarvestVein( 5.0, 0.1, res[17], res[0] ),
				new HarvestVein( 5.0, 0.1, res[18], res[0] ),
				new HarvestVein( 5.0, 0.1, res[19], res[0] ),
				new HarvestVein( 5.0, 0.1, res[20], res[0] ),
				new HarvestVein( 5.0, 0.1, res[21], res[0] ),
				new HarvestVein( 5.0, 0.1, res[22], res[0] ),
				new HarvestVein( 5.0, 0.1, res[23], res[0] ),
				new HarvestVein( 5.0, 0.1, res[24], res[0] ),
				new HarvestVein( 5.0, 0.1, res[25], res[0] ),
				new HarvestVein( 5.0, 0.1, res[26], res[0] ),
				new HarvestVein( 5.0, 0.1, res[27], res[0] ),
				new HarvestVein( 5.0, 0.1, res[28], res[0] ),
				new HarvestVein( 5.0, 0.1, res[29], res[0] ),
				new HarvestVein( 5.0, 0.1, res[30], res[0] ),
				new HarvestVein( 5.0, 0.1, res[31], res[0] ),
				new HarvestVein( 5.0, 0.1, res[32], res[0] ),
				new HarvestVein( 5.0, 0.1, res[33], res[0] ),
				new HarvestVein( 5.0, 0.1, res[34], res[0] ),
				new HarvestVein( 5.0, 0.1, res[35], res[0] ),
			};

			herb.Resources = res;
			herb.Veins = veins;

			m_Definition = herb;
			Definitions.Add( herb );
			#endregion
		}

		public override bool CheckHarvest( Mobile from, Item tool )
		{
			if ( !base.CheckHarvest( from, tool ) ) return false;
			if ( tool.Parent != from ) { from.SendMessage( "Vous devez avoir votre serpe en main pour récolter les herbes." ); return false; }
			return true;
		}

		public override bool CheckHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			if ( !base.CheckHarvest( from, tool, def, toHarvest ) ) return false;
			if ( tool.Parent != from ) { from.SendMessage("Vous devez avoir votre serpe en main pour récolter les herbes."); return false; }
			return true;
		}

		public override void OnBadHarvestTarget( Mobile from, Item tool, object toHarvest )
		{
			from.SendMessage( "Vous ne pouvez utiliser votre Serpe sur cela." );
		}

		public static void Initialize()
		{
			Array.Sort( m_PlantTiles );
		}

		#region Tile lists
		private static int[] m_PlantTiles = new int[]
		{
			0x4CCA, 0x4CCB, 0x4CCC, 0x4CCD, 0x4CD0, 0x4CD3, 0x4CD6, 0x4CD8,
			0x4CDA, 0x4CDD, 0x4CE0, 0x4CE3, 0x4CE6, 0x4CF8, 0x4CFB, 0x4CFE,
			0x4D01, 0x4D41, 0x4D42, 0x4D43, 0x4D44, 0x4D57, 0x4D58, 0x4D59,
			0x4D5A, 0x4D5B, 0x4D6E, 0x4D6F, 0x4D70, 0x4D71, 0x4D72, 0x4D84,
			0x4D85, 0x4D86, 0x52B5, 0x52B6, 0x52B7, 0x52B8, 0x52B9, 0x52BA,
			0x52BB, 0x52BC, 0x52BD,

			0x4CCE, 0x4CCF, 0x4CD1, 0x4CD2, 0x4CD4, 0x4CD5, 0x4CD7, 0x4CD9,
			0x4CDB, 0x4CDC, 0x4CDE, 0x4CDF, 0x4CE1, 0x4CE2, 0x4CE4, 0x4CE5,
			0x4CE7, 0x4CE8, 0x4CF9, 0x4CFA, 0x4CFC, 0x4CFD, 0x4CFF, 0x4D00,
			0x4D02, 0x4D03, 0x4D45, 0x4D46, 0x4D47, 0x4D48, 0x4D49, 0x4D4A,
			0x4D4B, 0x4D4C, 0x4D4D, 0x4D4E, 0x4D4F, 0x4D50, 0x4D51, 0x4D52,
			0x4D53, 0x4D5C, 0x4D5D, 0x4D5E, 0x4D5F, 0x4D60, 0x4D61, 0x4D62,
			0x4D63, 0x4D64, 0x4D65, 0x4D66, 0x4D67, 0x4D68, 0x4D69, 0x4D73,
			0x4D74, 0x4D75, 0x4D76, 0x4D77, 0x4D78, 0x4D79, 0x4D7A, 0x4D7B,
			0x4D7C, 0x4D7D, 0x4D7E, 0x4D7F, 0x4D87, 0x4D88, 0x4D89, 0x4D8A,
			0x4D8B, 0x4D8C, 0x4D8D, 0x4D8E, 0x4D8F, 0x4D90, 0x4D95, 0x4D96,
			0x4D97, 0x4D99, 0x4D9A, 0x4D9B, 0x4D9D, 0x4D9E, 0x4D9F, 0x4DA1,
			0x4DA2, 0x4DA3, 0x4DA5, 0x4DA6, 0x4DA7, 0x4DA9, 0x4DAA, 0x4DAB,
			0x52BE, 0x52BF, 0x52C0, 0x52C1, 0x52C2, 0x52C3, 0x52C4, 0x52C5,
			0x52C6, 0x52C7, 0x647D, 0x647E, 0x6476, 0x6477, 0x624A, 0x624B,
			0x624C, 0x624D, 0x4D94, 0x4D98, 0x4D9C, 0x4DA4, 0x4DA8, 0x70A1,
			0x709C, 0x70BD, 0x70C3, 0x70D4, 0x70DA, 0xDA0,

			0x4C85, 0x4C83, 0x4C84, 0x4C86, 0x4C87, 0x4C88, 0x4C89, 0x4C8A,
			0x4C8B, 0x4C8C, 0x4C8D, 0x4C8E, 0x4C8F, 0x4C90, 0x4C91, 0x4C92,
			0x4C93, 0x4C94, 0x4C95, 0x4C96, 0x4C97, 0x4C98, 0x4C99, 0x4C9A,
			0x4C9B, 0x4C9C, 0x4C9D, 0x4C9E, 0x4C9F, 0x4CA0, 0x4CA1, 0x4CA2,
			0x4CA3, 0x4CA4, 0x4CA5, 0x4CA6, 0x4CA7, 0x4CA8, 0x4CA9, 0x4CAA,
			0x4CAB, 0x4CAC, 0x4CAD, 0x4CAE, 0x4CAF, 0x4CB0, 0x4CB1, 0x4CB2,
			0x4CB3, 0x4CB4, 0x4CB5, 0x4CB6, 0x4CB7, 0x4CB8, 0x4CB9, 0x4CBA,
			0x4CBB, 0x4CBC, 0x4CBD, 0x4CBE, 0x4CBF, 0x4CC0, 0x4CC1, 0x4CC3,
			0x4CC4, 0x4CC5, 0x4CC6, 0x4CC7, 0x4CC8, 0x4CC9, 0x4CE9, 0x4CEA,
			0x4CEB, 0x4CEC, 0x4CED, 0x4CEE, 0x4CEF, 0x4CF0, 0x4CF1, 0x4CF2,
			0x4CF3, 0x4CF4, 0x4CF5, 0x4CF6, 0x4CF7, 0x4D04, 0x4D05, 0x4D06,
			0x4D07, 0x4D08, 0x4D09, 0x4D0A, 0x4D0B, 0x4D30, 0x4D31, 0x4D32,
			0x4D33, 0x4D34, 0x4D37, 0x4D38, 0x4D3F, 0x4D40, 0x4DBC, 0x4DBD,
			0x4DBE, 0x4DC1, 0x4DC2, 0x4DC3
		};
		#endregion
	}
}