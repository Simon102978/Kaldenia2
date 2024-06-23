
using Server.Mobiles;
using System;
using System.Collections.Generic;


namespace Server
{
    // Créer pour passer d'un objet IG, a un truc Intangible, puis pouvoir retourner en un objet plutard.

    public class Perfume
    {
        private string m_Nom;
        private string m_HtmlHue;
        private int m_Hue;
        private DateTime m_DateFin;
        private TimeSpan m_Duration;
 
        public string Nom { get => m_Nom; set => m_Nom = value; }
        public int Hue { get => m_Hue; set => m_Hue = value; }
        public DateTime DateFin { get => m_DateFin; set => m_DateFin = value; }

        public TimeSpan Duration { get => m_Duration; set => m_Duration = value; }

        public string HtmlHue { get => m_HtmlHue; set => m_HtmlHue = value; }

        public Perfume()
        {
            m_Hue = 0x3B2;
            m_HtmlHue = "#FFFFFF";
            m_Duration = TimeSpan.FromDays(3);

           // m_Notoriety = Notoriety.CanBeAttacked;
        }

        public Perfume(string name, int hue, string htmlHue, TimeSpan duration)
        {
            m_Hue = hue;
            m_Nom = name;
            m_HtmlHue = htmlHue;
            m_Duration = duration;
        }

        #region Seriliazer

        // Je veux pas me faire chier a tout marquer dans le sérializer du mobile, alors c'est plus simple de juste tout mettre ici, et de faire les rappels du mobiles.

        public void Serialize(GenericWriter writer)
        {

            writer.Write((int)2);

            writer.Write(m_Duration);

            writer.Write(m_HtmlHue);
            writer.Write(m_Nom);
            writer.Write(m_Hue);
            writer.Write(m_DateFin);
        }

        public static Perfume Deserialize(GenericReader reader)
        {
            Perfume mc = new Perfume();


            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    {
                        mc.Duration = reader.ReadTimeSpan();
                        goto case 1;
                    }
                case 1:
                    {
                        mc.m_HtmlHue = reader.ReadString();
                        goto case 0;
                    }

                case 0:
                    {
                        mc.Nom = reader.ReadString();
                        mc.Hue = reader.ReadInt();
                        mc.DateFin = reader.ReadDateTime();                      
                        break;
                    }
            }
            return mc;
        }
        #endregion
    }
}
