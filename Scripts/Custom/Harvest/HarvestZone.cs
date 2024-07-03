using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server
{
    public enum ZoneType
    {
        Fishing,
    }
}

namespace Server.Engines.Harvest
{
    public class HarvestZone
    {
        private ZoneType m_ZoneType;
        private ArrayList m_Area;
        private HarvestVein[] m_Veins;
        private static ArrayList m_HarvestZones = new ArrayList();
        private Type m_RequiredTool;

        public ZoneType ZoneType
        {
            get { return m_ZoneType; }
            set { m_ZoneType = value; }
        }

        public ArrayList Area
        {
            get { return m_Area; }
            set { m_Area = value; }
        }

        public HarvestVein[] Veins
        {
            get { return m_Veins; }
            set { m_Veins = value; }
        }

        public Type RequiredTool
        {
            get { return m_RequiredTool; }
            set { m_RequiredTool = value; }
        }

        public static ArrayList HarvestZones
        {
            get { return m_HarvestZones; }
            set { m_HarvestZones = value; }
        }

        public static HarvestResource[] Resources
        {
            get { return m_Resources; }
            set { m_Resources = value; }
        }

        public HarvestZone(ZoneType zoneType)
		{
            m_ZoneType = zoneType;
			m_Area = new ArrayList();
		}

        public HarvestZone(ZoneType zoneType, Type tool)
        {
            m_ZoneType = zoneType;
            m_Area = new ArrayList();
            m_RequiredTool = tool;
        }

        private static HarvestResource[] m_Resources = new HarvestResource[]
			{

				new HarvestResource(00.0, 00.0, 120.0, 1043297, typeof(Fish)),
				new HarvestResource( 00.0, -70.0, 100.0, "Vous pêchez un Dragon Fish Automnale et la déposez dans votre sac.", typeof( AutumnDragonfish ) ), //24
				new HarvestResource( 00.0, -70.0, 100.0, "Vous pêchez un Homard Bleu et le déposez dans votre sac.", typeof( BlueLobster ) ), //25
				new HarvestResource( 35.0, -35.0, 100.0, "Vous pêchez un Poisson Boeuf et la déposez dans votre sac.", typeof( BullFish ) ), //26
				new HarvestResource( 00.0, -70.0, 100.0, "Vous pêchez un Poisson Cristal et la déposez dans votre sac.", typeof( CrystalFish ) ), //27
				new HarvestResource( 35.0, -35.0, 100.0, "Vous pêchez un Saumon et le déposez dans votre sac.", typeof( FairySalmon ) ), //28
				new HarvestResource( 35.0, -35.0, 100.0, "Vous pêchez un Koi Geant et le déposez dans votre sac.", typeof( GiantKoi ) ), //29
				new HarvestResource( 40.0, -30.0, 100.0, "Vous pêchez un grand barracuda et la déposez dans votre sac.", typeof( GreatBarracuda ) ), //30
				new HarvestResource( 40.0, -30.0, 100.0, "Vous pêchez un holy mackerel et le déposez dans votre sac.", typeof( HolyMackerel ) ), //31
				new HarvestResource( 60.0,  10.0, 100.0, "Vous pêchez un poisson lave et la déposez dans votre sac.", typeof( LavaFish ) ), //32
				new HarvestResource( 40.0, -30.0, 100.0, "Vous pêchez un poisson reaper et le déposez dans votre sac.", typeof( ReaperFish ) ), //33
				new HarvestResource( 60.0,  10.0, 100.0, "Vous pêchez un crabe araignée et le déposez dans votre sac.", typeof( SpiderCrab ) ), //34
				new HarvestResource( 60.0,  10.0, 100.0, "Vous pêchez un crabe roche et le déposez dans votre sac.", typeof( StoneCrab ) ), //35
				new HarvestResource( 60.0,  10.0, 100.0, "Vous pêchez un dragon fish de l'été et la déposez dans votre sac.", typeof( SummerDragonfish ) ), //36
				new HarvestResource( 60.0,  10.0, 100.0, "Vous pêchez un poisson licorne et le déposez dans votre sac.", typeof( UnicornFish ) ), //37
				new HarvestResource( 60.0,  10.0, 100.0, "Vous pêchez un saumon et le déposez dans votre sac.", typeof( YellowtailBarracuda ) ), //38
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez un grand brochet et le déposez dans votre sac.", typeof( AbyssalDragonfish ) ), //39
				new HarvestResource( 35.0, -35.0, 100.0, "Vous pêchez une truite sauvage et la déposez dans votre sac.", typeof( BlackMarlin ) ), //40
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez un grand doré et le déposez dans votre sac.", typeof( BloodLobster ) ), //41
				new HarvestResource( 60.0,  10.0, 100.0, "Vous pêchez une truite de mer et la déposez dans votre sac.", typeof( BlueMarlin ) ), //42
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez un esturgeon de mer et le déposez dans votre sac.", typeof( DreadLobster ) ), //43
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez un grand saumon et le déposez dans votre sac.", typeof( DungeonPike ) ), //44
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez une raie et la déposez dans votre sac.", typeof( GiantSamuraiFish ) ), //45
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez un espadon et le déposez dans votre sac.", typeof( GoldenTuna ) ), //46
				new HarvestResource( 90.0,  85.0, 300.0, "Vous pêchez un requin gris.", typeof( Kingfish ) ), //47
				new HarvestResource( 90.0,  85.0, 300.0, "Vous pêchez un requin blanc.", typeof( LanternFish ) ), //48
				new HarvestResource( 40.0, -30.0, 100.0, "Vous pêchez une huître et la déposez dans votre sac.", typeof( RainbowFish ) ), //49
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez un calmar et le déposez dans votre sac.", typeof( SeekerFish ) ), //50
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez une pieuvre et la déposez dans votre sac.", typeof( SpringDragonfish ) ), //51			};
				new HarvestResource( 40.0, -30.0, 100.0, "Vous pêchez une huître et la déposez dans votre sac.", typeof( StoneFish ) ), //49
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez un calmar et le déposez dans votre sac.", typeof( TunnelCrab ) ), //50
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez une pieuvre et la déposez dans votre sac.", typeof( VoidCrab ) ), //51							
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez une pieuvre et la déposez dans votre sac.", typeof( VoidLobster ) ), //51			};
				new HarvestResource( 40.0, -30.0, 100.0, "Vous pêchez une huître et la déposez dans votre sac.", typeof( WinterDragonfish ) ), //49
				new HarvestResource( 75.0,  25.0, 110.0, "Vous pêchez un calmar et le déposez dans votre sac.", typeof( ZombieFish ) ), //50

				
		};

        public static void AddZone(HarvestZone zone)
        {
            if (!m_HarvestZones.Contains(zone))
                m_HarvestZones.Add(zone);
        }

        public static void RemoveZone(HarvestZone zone)
        {
            if (m_HarvestZones.Contains(zone))
                m_HarvestZones.Remove(zone);
        }
    }
} 