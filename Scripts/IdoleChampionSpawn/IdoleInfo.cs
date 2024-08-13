using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.IO;

namespace Server.Engines.Idole
{
   public class IdoleInfo
    {

        public static List<IdoleInfo> Table => m_Table;

        private static List<IdoleInfo> m_Table = new List<IdoleInfo>();

        public static string FilePath = Path.Combine("Saves/", "Idole.bin");

        	public static void Configure()
        {
            EventSink.WorldSave += OnSave;
            EventSink.WorldLoad += OnLoad;



		}

       public static void OnSave(WorldSaveEventArgs e)
        {
            Persistence.Serialize(
                FilePath,
                writer =>
                {
                    writer.Write(0);

                    writer.Write(Table.Count);

                    foreach (IdoleInfo item in Table)
                    {
                        item.Serialize(writer);
                    }

				

					
                });
        }

       public static void OnLoad()
        {
            Persistence.Deserialize(
                FilePath,
                reader =>
                {
                    int version = reader.ReadInt();

					switch (version)
					{					
						case 0:
							{
								m_Table = new List<IdoleInfo>();

                                int count = reader.ReadInt();

                                for (int i = 0; i < count; i++)
                                {
                                    m_Table.Add(IdoleInfo.Deserialize(reader));
                                }
								break;
							}
					}


				});
        }
    


        private int m_AltarItemId = 0;
        public int AltarItemId
        {
            get { return m_AltarItemId; }
            set { m_AltarItemId = value; }
        }

        private string m_InfoName =  "Sans Nom";
        public string InfoName
        {
            get { return m_InfoName; }
            set { m_InfoName = value; }
        }

        private string m_IdoleName =  "Idole";
        public string IdoleName
        {
            get { return m_IdoleName; }
            set { m_IdoleName= value; }
        }

         public  List<IdoleLevelInfo> Levels => m_Levels;

        private  List<IdoleLevelInfo> m_Levels = new List<IdoleLevelInfo>();


        public int MaxLevel => Levels == null ? 0 : Levels.Count;


        public IdoleInfo()
        {             
        }

        public IdoleInfo(List<IdoleLevelInfo> levels)
        {
          
            m_Levels = levels;
          
        }

        public IdoleLevelInfo GetLevelInfo(int level)
        {
            level--;

            if (level >= 0 && level < Levels.Count)
                return Levels[level];

            return null;
        }
        public static IdoleInfo GetInfo(int type)
        {
            if (m_Table.Count == 0)
            {
                return null;
            }
            
            int v = type;

            if (v < 0 || v >= m_Table.Count)
                v = 0;

            return m_Table[v];
        }
        public void AddLevel()
        {
            m_Levels.Add(new IdoleLevelInfo());
        }

        public void DeleteLvl(int LevelToDelete)
        {
            int i = 0;

            bool lvldeleted = false;

            List<IdoleLevelInfo> newLevelList = new List<IdoleLevelInfo>();

            foreach (IdoleLevelInfo item in m_Levels)
            {
                if (item.Niveau != LevelToDelete || lvldeleted)
                {
                    item.Niveau = i;
                    newLevelList.Add(item);
                    i++;
                }
                else
                {
                    lvldeleted = true;
                }          
            }
            m_Levels = newLevelList;
        }

        public void AddLevel(IdoleLevelInfo newlevel)
        {

            m_Levels.Add(newlevel);

        }
    
        public static int CreateIdoleInfo()
        {
            int count = m_Table.Count;

            m_Table.Add(new IdoleInfo());

            return count;

        }    
    
        public void Serialize(GenericWriter writer)
		{

			writer.Write((int)1);

            writer.Write(IdoleName);
            writer.Write(InfoName);
            writer.Write(AltarItemId);
            writer.Write(Levels.Count);

            foreach (IdoleLevelInfo item in Levels)
            {
                item.Serialize(writer);
            }

		}


		public static IdoleInfo Deserialize(GenericReader reader)
		{
			IdoleInfo idoleinfo = new IdoleInfo();


			int version = reader.ReadInt();

			switch (version)
			{
                case 1:
                    {
                        idoleinfo.IdoleName = reader.ReadString();
                        idoleinfo.InfoName = reader.ReadString();
                        goto case 0;
                    }
			
				case 0:
					{
                        idoleinfo.AltarItemId = reader.ReadInt();

                        int count = reader.ReadInt();   

                        for (int i = 0; i < count; i++)
                        {
                            idoleinfo.AddLevel(IdoleLevelInfo.Deserialize(reader));
                        }

						break;
					}
			}
			return idoleinfo;
		}

    
    
    
    
    }
}