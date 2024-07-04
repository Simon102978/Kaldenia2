namespace Server.Engines.ArenaSystem
{
    [PropertyObject]
    public class ArenaDefinition
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public string Name { get; private set; }

  /*      [CommandProperty(AccessLevel.GameMaster)]
        public Point3D StoneLocation { get; private set; }
  */
        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D ManagerLocation { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D BannerLocation1 { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D BannerLocation2 { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D GateLocation { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BannerID1 { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BannerID2 { get; private set; }

		public int ArenaZ { get; private set; }

		public int DeadZ { get; private set; }

		public Rectangle2D[] EffectAreas { get; private set; }
        public Rectangle2D[] RegionBounds { get; private set; }
   
        public Rectangle2D[] StartLocations { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Rectangle2D StartLocation1 { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Rectangle2D StartLocation2 { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Rectangle2D EjectLocation { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Rectangle2D DeadEjectLocation { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MapIndex { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Map Map => Map.Maps[MapIndex];

        public ArenaDefinition(
            string name,
            int mapIndex,
    //        Point3D stoneLoc,
            Point3D manLoc,
            Point3D banloc1,
            Point3D banloc2,
            int id1,
            int id2,
			int z,
            Rectangle2D[] effectAreas,
            Rectangle2D[] startLocs,
            Point3D gateLoc,
            Rectangle2D[] bounds,
            Rectangle2D eject,
            Rectangle2D deadEject,
			int zDead)
        {
            Name = name;
            MapIndex = mapIndex;
     //       StoneLocation = stoneLoc;
            ManagerLocation = manLoc;
            BannerLocation1 = banloc1;
            BannerLocation2 = banloc2;
            BannerID1 = id1;
            BannerID2 = id2;
			ArenaZ = z;
            EffectAreas = effectAreas;
            StartLocations = startLocs;
            StartLocation1 = startLocs[0];
            StartLocation2 = startLocs[1];
            GateLocation = gateLoc;
            RegionBounds = bounds;
         
            EjectLocation = eject;
            DeadEjectLocation = deadEject;
			DeadZ = zDead;
        }

  //      public static ArenaDefinition Alverton { get; set; }

        public static ArenaDefinition Mirage { get; set; }

        public static ArenaDefinition[] Definitions => _Definitions;
        private static readonly ArenaDefinition[] _Definitions = new ArenaDefinition[1];

        static ArenaDefinition()
        {

 /*
            Alverton = new ArenaDefinition("Alverton", 0,
                new Point3D(1378, 687, -35),
                new Point3D(1373, 671, 25),
                new Point3D(1373, 668, 25),
                17102,
                17100,
				5,// fait
                new Rectangle2D[]
                {
                    new Rectangle2D(1373, 666, 13, 1),
                    new Rectangle2D(1373, 673, 13, 1),
                    new Rectangle2D(1376, 659, 1, 23),
                    new Rectangle2D(1379, 659, 1, 23),
                    new Rectangle2D(1382, 659, 1, 23),
					new Rectangle2D(1376, 658, 2, 1),
					new Rectangle2D(1380, 658, 2, 1),
				},
                new Rectangle2D[]
                {
                    new Rectangle2D(1377, 659, 2, 7),
                    new Rectangle2D(1377, 674, 2, 8),
                    new Rectangle2D(1383, 659, 3, 7),
                    new Rectangle2D(1383, 667, 3, 6),
                    new Rectangle2D(6076, 3724, 5, 4),
                    new Rectangle2D(1373, 659, 3, 7),
                    new Rectangle2D(1380, 659, 2, 7),
                    new Rectangle2D(1373, 667, 3, 6),
                    new Rectangle2D(1377, 667, 2, 6),
                    new Rectangle2D(1380, 667, 2, 6),
					new Rectangle2D(1373, 674, 3, 8),
					new Rectangle2D(1380, 674, 2, 8),
					new Rectangle2D(1383, 674, 3, 8),
				},
                new Point3D(1378, 682, -35), // fait
                new Rectangle2D[]
                {
                    new Rectangle2D(1359, 2726, 12, 6)
                  //  new Rectangle2D(1373, 659, 13, 23)
                },
                new Rectangle2D(1375, 680, 5, 4),// fait
                new Rectangle2D(1370, 655, 2, 2),  // fait?
				-15); // fait?
      */    

               Mirage = new ArenaDefinition("Mirage", 0,
                new Point3D(1371, 2743, 1), // fait - Lucia -- Good
                new Point3D(1361, 2727, 20),   // banner 1 -- Good
                new Point3D(1369, 2727, 18),   // banner 2 -- Good
                17101, // Banner ID
                17099, // Banner Id
				0, // Z arene fait
                new Rectangle2D[]
                {

                    new Rectangle2D(1363, 2727, 1, 9),
                    new Rectangle2D(1368, 2727, 1, 9),
                    new Rectangle2D(1358, 2729, 15, 1),
                    new Rectangle2D(1358, 2731, 15, 1),
                    new Rectangle2D(1358, 2733, 15, 1),
					new Rectangle2D(1357, 2731, 1, 1),
					new Rectangle2D(1373, 2731, 1, 1),
                
				},
                new Rectangle2D[]
                {
                    new Rectangle2D(1358, 2730, 4, 1),
                    new Rectangle2D(1369, 2730, 4, 1),
                    new Rectangle2D(1364, 2734, 4, 2),
                    new Rectangle2D(1364, 2727, 4, 2),
                    new Rectangle2D(1358, 2727, 5, 2),    
                    new Rectangle2D(1369, 2734, 4, 2),
                    new Rectangle2D(1358, 2732, 4, 1),
                    new Rectangle2D(1369, 2732, 4, 1),
                    new Rectangle2D(1364, 2730, 4, 1),
                    new Rectangle2D(1358, 2734, 4, 2),
                    new Rectangle2D(1364, 2732, 4, 1),
                    new Rectangle2D(1369, 2727, 4, 2),
				},
                new Point3D(1371, 2739, 0), // Sorti gagnant  - fait -- good
                new Rectangle2D[]
                {
                    new Rectangle2D(1358, 2727, 15, 10) // arene   == 1372 2735 fait
                },
                new Rectangle2D(1367, 2737, 5, 4),// Eject fait  -- Good
                new Rectangle2D(1370, 2719,10 ,1),  //  Fait Dead eject  -- Good
				0); // fait - Dead Z  -- Good

            _Definitions[0] = Mirage;
      //      _Definitions[1] = Alverton;

        }
    }
}