using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Mobiles;

namespace Server
{

 

    public class Nom
    { 
        private Mobile m_Mobile;
        private int m_Identite;

        private string m_Name;





        public Mobile Mobile { get => m_Mobile; set => m_Mobile = value; }      
     
        public string Name { get => m_Name; set => m_Name = value; }     
        public int Identite { get => m_Identite; set => m_Identite = value; }

        public Nom()
        {

        }
        public Nom(Mobile player)
        {
            if (player is CustomPlayerMobile cp)
            {

                m_Mobile = cp;
                m_Identite = cp.IdentiteID;
                m_Name = cp.Name;
            }
            else
            {
                m_Mobile = player;
                m_Identite = 0;
                m_Name = player.Name;
            }


           
        }

        public bool Equal(Nom autre)
        {
            if (autre.Mobile == m_Mobile && autre.Identite == m_Identite && autre.Name == m_Name)           
                return true;
            
            return false;

        }


        #region Seriliazer

        // Je veux pas me faire chier a tout marquer dans le sérializer du mobile, alors c'est plus simple de juste tout mettre ici, et de faire les rappels du mobiles.

        public void Serialize(GenericWriter writer)
        {
            writer.Write((int)0);
            writer.Write(m_Mobile);          
            writer.Write(m_Identite);
            writer.Write(m_Name);
        }

        public static Nom Deserialize(GenericReader reader)
        {
            Nom identite = new Nom();

            int version = reader.ReadInt();

            switch (version)
            {              
                case 0:
                    {
                        
                        identite.Mobile = reader.ReadMobile();
                        identite.Identite = reader.ReadInt();
                        identite.Name = reader.ReadString();    
                       
                        break;
                    }
            }

            return identite;
        }
        #endregion
    }
}
