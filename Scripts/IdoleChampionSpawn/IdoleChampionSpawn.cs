using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using System;
using System.Collections.Generic;

namespace Server.Engines.Idole
{
    public class IdoleChampionSpawn : Item
    {

        private static readonly List<IdoleChampionSpawn> Controllers = new List<IdoleChampionSpawn>();

        private bool m_Active;
        private int m_Type = -1;
        private List<IdoleSpawnInfo> Spawn;
        public List<Mobile> Despawns;
        private int m_Level;
        private int m_SpawnRange;
        private TimeSpan m_RestartDelay;
        private Timer m_Timer, m_RestartTimer;
        private Item m_Altar;

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D BossSpawnPoint { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastChange { get; set; }

        [Constructable]
        public IdoleChampionSpawn()
            : base(0xBD2)
        {
            Movable = false;
            Visible = false;
            Name = "Idole Controller";

            Despawns = new List<Mobile>();
            Spawn = new List<IdoleSpawnInfo>();
            m_RestartDelay = TimeSpan.FromMinutes(5.0);
            m_SpawnRange = 30;
            BossSpawnPoint = Point3D.Zero;

            Controllers.Add(this);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SpawnRange
        {
            get { return m_SpawnRange; }
            set { m_SpawnRange = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan RestartDelay
        {
            get { return m_RestartDelay; }
            set { m_RestartDelay = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Type
        {
            get { return m_Type; }
            set
            {

           
                    m_Type = value;
                    InvalidateProperties();
                    ChangeType();
                




            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Active
        {
            get { return m_Active; }
            set
            {
                if (value)
                    Start();
                else
                    Stop();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Item Altar
        {
            get { return m_Altar; }
            set { m_Altar = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Level
        {
            get { return m_Level; }
            set { m_Level = value; InvalidateProperties(); }
        }

        public IdoleInfo GetIdoleInfo()
        {
            if (Type == -1)
            {
                return null;
            }
            else
            {

                return IdoleInfo.GetInfo(Type);
            }

        }

        public void Start()
        {

            if (m_Active || Deleted)
                return;

            m_Active = true;
 
            LastChange = DateTime.Now;

            if (m_Timer != null)
                m_Timer.Stop();

            m_Timer = new SliceTimer(this);
            m_Timer.Start();

            if (m_RestartTimer != null)
                m_RestartTimer.Stop();

            m_RestartTimer = null;
    
            AdvanceLevel();
            InvalidateProperties();
        }

        public void Stop()
        {
            if (!m_Active || Deleted)
                return;

            m_Active = false;
            m_Level = 0;

            ClearSpawn();
            Despawn();

            if (m_Timer != null)
                m_Timer.Stop();

            m_Timer = null;

            if (m_RestartTimer != null)
                m_RestartTimer.Stop();

            m_RestartTimer = null;
            InvalidateProperties();
        }

        public void ChangeType()
        {
            RefreshIdole();

            ClearSpawn();
        }

        public void RefreshIdole()
        {
            if (m_Altar != null)
            {
                IdoleInfo info = IdoleInfo.GetInfo(m_Type);


                if (info != null && info.Levels != null)
                {
                    m_Altar.ItemID = info.AltarItemId;

                    if (info.IdoleName != "")
                    {
                         m_Altar.Name = info.IdoleName;
                    }
                    else
                    {
                        m_Altar.Name = "Idole";
                    }

                    if (info.Levels.Count != 0)
                    {
                        m_Altar.Hue = info.Levels[0].AltarHue;
                    }
                }
            }
            else
            {
                IdoleInfo info = IdoleInfo.GetInfo(m_Type);

                if (info != null && info.Levels != null )
                {
                    int hue = 0;

                    if (info.Levels.Count > 0)
                    {
                        hue = info.Levels[0].AltarHue;
                    }

                    Idole iw = new Idole(info.AltarItemId, hue);

                    iw.MoveToWorld(Location, Map);

                    m_Altar = iw;
                }
            }
        }

        public void Despawn()
        {
            foreach (Mobile toDespawn in Despawns)
            {
                toDespawn.Delete();
            }

            Despawns.Clear();
        }

        public void OnSlice()
        {
            if (!m_Active || Deleted)
                return;

            bool changed = false;
            bool done = true;

            foreach (IdoleSpawnInfo spawn in Spawn)
            {
                if (spawn.Slice() && !changed)
                {
                    changed = true;
                }

                if (!spawn.Done && done)
                {
                    done = false;
                }
            }

            if (done)
            {
                AdvanceLevel();
            }

            if (changed)
            {
                LastChange = DateTime.Now;
            }

            if (m_Level > 1 && LastChange.AddMinutes(30) < DateTime.Now)
            {
                        
                ClearSpawn();
                Despawn();
                m_Level = 0;  

            }

            if (m_Active)
            {
                foreach (IdoleSpawnInfo spawn in Spawn)
                {
                    if (spawn.Respawn() && !changed)
                    {
                        changed = true;
                    }
                }
            }

            if (done || changed)
            {
                InvalidateProperties();
            }
        }

        public void ClearSpawn()
        {
            foreach (IdoleSpawnInfo spawn in Spawn)
            {
                foreach (Mobile creature in spawn.Creatures)
                {
                    Despawns.Add(creature);
                }
            }

            Spawn.Clear();
        }

        public void AdvanceLevel()
        {
            Level++;

            LastChange = DateTime.Now;

            IdoleInfo info = IdoleInfo.GetInfo(m_Type);

            if (info == null)
            {
                Active = false;
                return;
            }

             if (Level > info.MaxLevel)
             {

                 Stop();

                    m_RestartTimer = Timer.DelayCall(m_RestartDelay, Start);

                    return;
             }

            IdoleLevelInfo levelInfo = info.GetLevelInfo(Level);

            if (levelInfo == null || levelInfo.Types == null || levelInfo.Types.Count == 0)
            {
               
                Active = false;
                return;            
            }

  
          

   

                if (m_Altar != null && !m_Altar.Deleted && levelInfo.AltarHue != null)
                {
                    m_Altar.Hue = levelInfo.AltarHue;
                }

                ClearSpawn();


                IPooledEnumerable eable = GetMobilesInRange(m_SpawnRange);

                if (levelInfo.Parole != null)
                {
                    foreach (Mobile x in eable)
                    {
                        if (x is CustomPlayerMobile)
                            x.SendMessage(43, levelInfo.Parole);
                    }
                }            

                eable.Free();

                int totalMonsterToKill = 0;

                foreach (IdoleTypeInfo type in levelInfo.Types)
                {
                    totalMonsterToKill += type.Required;
                }

              
               foreach (IdoleTypeInfo type in levelInfo.Types)
               {
                        Spawn.Add(new IdoleSpawnInfo(this, type, levelInfo.Boss));
               }             
            
        }


        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1060658, "Type\t{0}", m_Type); // ~1_val~: ~2_val~
            list.Add(1060661, "Spawn Range\t{0}", m_SpawnRange); // ~1_val~: ~2_val~

            if (m_Active)
            {
                IdoleInfo info = IdoleInfo.GetInfo(m_Type);

                list.Add(1060742); // active
                list.Add("Level {0} / {1}", Level, info != null ? info.MaxLevel.ToString() : "???"); // ~1_val~: ~2_val~

                for (int i = 0; i < Spawn.Count; i++)
                {
                    Spawn[i].AddProperties(list, i + 1150301);
                }
            }
            else
            {
                list.Add(1060743); // inactive
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from is CustomPlayerMobile cp)
            {
                 from.SendGump(new IdoleMainGump(cp,this));
            }          
        }

        public override void OnDelete()
        {
            Controllers.Remove(this);
            Stop();

            if (m_Altar != null && m_Altar.Deleted)
            {
                m_Altar.Delete();
            }


            base.OnDelete();
        }

        public override void OnMapChange()
        {
            if (Deleted)
                return;

            base.OnMapChange();

            IdoleInfo info = IdoleInfo.GetInfo(m_Type);

            if (info != null && info.Levels != null && info.Levels.Count > 0)
            {
                Idole iw = new Idole(info.AltarItemId, info.Levels[0].AltarHue);

                iw.MoveToWorld(Location, Map);

                m_Altar = iw;
            }
        }

     
        public IdoleChampionSpawn(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.Write(m_Altar);
            writer.Write(BossSpawnPoint);
            writer.Write(m_Active);
            writer.Write(m_Type);
            writer.Write(m_Level);
            writer.Write(m_SpawnRange);
            writer.Write(m_RestartDelay);

            writer.Write(Spawn.Count);

            for (int i = 0; i < Spawn.Count; i++)
            {
                Spawn[i].Serialize(writer);
            }

            writer.Write(Despawns, true);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Altar = reader.ReadItem();
                        Spawn = new List<IdoleSpawnInfo>();

                        BossSpawnPoint = reader.ReadPoint3D();
                        m_Active = reader.ReadBool();
                        m_Type =reader.ReadInt();
                        m_Level = reader.ReadInt();
                        m_SpawnRange = reader.ReadInt();
                        m_RestartDelay = reader.ReadTimeSpan();

                        int spawnCount = reader.ReadInt();

                        for (int i = 0; i < spawnCount; i++)
                        {
                            Spawn.Add(new IdoleSpawnInfo(reader));
                        }

                        Despawns = reader.ReadStrongMobileList();

                        if (m_Active)
                        {
                            m_Timer = new SliceTimer(this);
                            m_Timer.Start();
                        }
                        else
                        {
                            m_RestartTimer = Timer.DelayCall(m_RestartDelay, Start);
                        }

                        break;
                    }
            }

            Controllers.Add(this);
        }
    }

    public class SliceTimer : Timer
    {
        private readonly IdoleChampionSpawn m_Controller;

        public SliceTimer(IdoleChampionSpawn controller)
            : base(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
        {
            m_Controller = controller;
        }

        protected override void OnTick()
        {
            m_Controller.OnSlice();
        }
    }
}
