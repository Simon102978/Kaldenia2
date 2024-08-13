using Server.Mobiles;
using System;
using System.Collections.Generic;

namespace Server.Engines.Idole
{
    public class IdoleSpawnInfo
    {
        private readonly IdoleChampionSpawn Owner;
        public List<Mobile> Creatures;

        public Type MonsterType { get; set; }

        private int m_Killed = 0;
        public int Killed
        {
            get { return m_Killed; }
            set { m_Killed = value; }
        }

        private int m_Spawned = 0;
        public int Spawned
        {
            get { return m_Spawned; }
            set { m_Spawned = value; }
        }


        private bool m_Boss = false;
        public bool Boss
        {
            get { return m_Boss; }
            set { m_Boss = value; }
        }
        
        
        private int m_Required = 0;

        public int Required
        {
            get { return m_Required; }
            set { m_Required = value; }
        }
        public int MaxSpawned => (Required * 2) - 1;
        public bool Done => Killed >= Required;




        public IdoleSpawnInfo(IdoleChampionSpawn controller, IdoleTypeInfo typeInfo, bool boss)
        {
            Owner = controller;

            Required = typeInfo.Required;
            MonsterType = typeInfo.SpawnType;

            Creatures = new List<Mobile>();
            Killed = 0;
            Spawned = 0;
            Boss = boss;

           
        }

        public bool Slice()
        {
            bool killed = false;
            List<Mobile> list = new List<Mobile>(Creatures);

            for (int i = 0; i < list.Count; i++)
            {
                Mobile creature = list[i];

                if (creature == null || creature.Deleted)
                {
                    Creatures.Remove(creature);
                    Killed++;

                    killed = true;
                }
                else if (!creature.InRange(Owner.Location, Owner.SpawnRange + 10))
                {
                    // bring to home
                    Map map = Owner.Map;
                    Point3D loc = map.GetSpawnPosition(Owner.Location, Owner.SpawnRange);

                    creature.MoveToWorld(loc, map);
                }
            }

            ColUtility.Free(list);
            return killed;
        }

        public bool Respawn()
        {
            bool spawned = false;

           Rectangle2D rec = new Rectangle2D(Owner.Location.X - Owner.SpawnRange / 2,Owner.Location.Y - Owner.SpawnRange / 2,Owner.SpawnRange,Owner.SpawnRange );
                

            while (Creatures.Count < Required && Spawned < MaxSpawned)
            {
                BaseCreature bc = Activator.CreateInstance(MonsterType) as BaseCreature;

                Map map = Owner.Map;
//              Point3D loc = map.GetSpawnPosition(Owner.Location, Owner.SpawnRange);

                Point3D loc = map.GetRandomSpawnPoint(rec);
                
                if (Boss)
                {
                    loc = Owner.BossSpawnPoint;              
                }

                Server.Spells.SpellHelper.FindValidSpawnLocation(map, ref loc, false);       

                bc.Home = Owner.Location;
                bc.RangeHome = Owner.SpawnRange;
         //       bc.Tamable = false;
                bc.OnBeforeSpawn(loc, map);
                bc.MoveToWorld(loc, map);

                Creatures.Add(bc);

                ++Spawned;

                spawned = true;
            }

            return spawned;
        }

     

        public void AddProperties(ObjectPropertyList list, int cliloc)
        {
            list.Add(cliloc, "{0}: Killed {1}/{2}, Spawned {3}/{4}",
                MonsterType.Name, Killed, Required, Spawned, MaxSpawned);
        }

        public void Serialize(GenericWriter writer)
        {
            writer.WriteItem(Owner);
            writer.Write(Killed);
            writer.Write(Spawned);
            writer.Write(Required);
            writer.Write(MonsterType.FullName);
            writer.Write(Creatures);
        }

        public IdoleSpawnInfo(GenericReader reader)
        {
            Creatures = new List<Mobile>();

            Owner = reader.ReadItem<IdoleChampionSpawn>();
            Killed = reader.ReadInt();
            Spawned = reader.ReadInt();
            Required = reader.ReadInt();
            MonsterType = ScriptCompiler.FindTypeByFullName(reader.ReadString());
            Creatures = reader.ReadStrongMobileList();
        }
    }
}
