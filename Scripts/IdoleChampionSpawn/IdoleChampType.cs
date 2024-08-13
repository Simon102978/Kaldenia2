using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Server.Engines.Idole
{
  
    public class IdoleTypeInfo
    {
        private int m_Required = 0;

        public int Required
        {
            get { return m_Required; }
            set { m_Required = value; }
        }
        private string m_SpawnType = "Ratman" ;


        public Type SpawnType
        {
            get { return ScriptCompiler.FindTypeByName(m_SpawnType) ; }
            set { m_SpawnType = value.Name; }
        }


        public IdoleTypeInfo(int required, Type spawnType)
        {
            Required = required;
            SpawnType = spawnType;
         
        }

        public IdoleTypeInfo()
        {
           
        }

        public void Serialize(GenericWriter writer)
		{

			writer.Write((int)0);
            writer.Write(Required);

          
            writer.Write(m_SpawnType);
	

		}


		public static IdoleTypeInfo Deserialize(GenericReader reader)
		{
			IdoleTypeInfo idoleTypeInfo = new IdoleTypeInfo();


			int version = reader.ReadInt();

			switch (version)
			{
			
				case 0:
					{
                        idoleTypeInfo.Required = reader.ReadInt();

                        idoleTypeInfo.m_SpawnType = reader.ReadString();

						break;
					}
			}
			return idoleTypeInfo;
		}


     

    }

    public class IdoleLevelInfo
    {

        private string m_Name = "";

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private bool m_Boss = false;

        public bool Boss
        {
            get { return m_Boss; }
            set { m_Boss = value; }
        }
     
        public int Niveau { get; set; }
        public int AltarHue { get; set; }

        public string Parole { get; set; }

        public List<IdoleTypeInfo> m_Types = new List<IdoleTypeInfo>();

        public List<IdoleTypeInfo> Types => m_Types;


        public IdoleLevelInfo()
        {
           
        }

        public IdoleLevelInfo(int niveau, int altarHue, string parole,  List<IdoleTypeInfo> types)
        {
            AltarHue=altarHue;
            m_Types = types;
            Parole = parole;
            Niveau = niveau;
        }

        public void AddLevel()
        {
            AddLevel(new IdoleTypeInfo());
        }

        public void AddLevel(IdoleTypeInfo level)
        {
            Types.Add(level);
        }

        public void DeleteLevel( int IDtoDelete)
        {
            int i = 0;

            bool lvldeleted = false;

            List<IdoleTypeInfo> newLevelList = new List<IdoleTypeInfo>();

            foreach (IdoleTypeInfo item in Types)
            {
                if (IDtoDelete != i || lvldeleted)
                {
               
                    newLevelList.Add(item);
                    i++;
                }
                else
                {
                    lvldeleted = true;
                }
                
            }

            m_Types = newLevelList;


        }


        public void Serialize(GenericWriter writer)
		{

			writer.Write((int)3);

            writer.Write(m_Boss);

            writer.Write(Name);

            writer.Write(Niveau);

            writer.Write(AltarHue);
            writer.Write(Parole);
            writer.Write(Types.Count);

            foreach (IdoleTypeInfo type in Types)
            {
                type.Serialize(writer);
            }			

		}


		public static IdoleLevelInfo Deserialize(GenericReader reader)
		{
			IdoleLevelInfo Idoleinfo = new IdoleLevelInfo();


			int version = reader.ReadInt();

			switch (version)
			{
                case 3:
                {
                    Idoleinfo.Boss = reader.ReadBool(); 
                    goto case 2;
                }
                case 2:
                {
                    Idoleinfo.Name = reader.ReadString();
                    goto case 1;
                }
                case 1:
                {
                      Idoleinfo.Niveau = reader.ReadInt();
                      goto case 0;
                }
			
				case 0:
					{
                        Idoleinfo.AltarHue = reader.ReadInt();
                        Idoleinfo.Parole = reader.ReadString();

                        int count = reader.ReadInt();

                        for (int i = 0; i < count; i++)
                        {               
                            Idoleinfo.AddLevel(IdoleTypeInfo.Deserialize(reader));
                        }

						break;
					}
			}
			return Idoleinfo;
		}








    }

   
}
