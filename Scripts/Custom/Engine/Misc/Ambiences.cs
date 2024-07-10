using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Movement;
using Server.Network;
using Server.Items;

namespace Server
{
    public class Ambiences
    {
        public static void Initialize()
        {
            Timer.DelayCall(TimeSpan.FromSeconds(90 + Utility.Random(30)), new TimerCallback(Ambiences_Callback));
        }

		public static bool IsNight(Mobile m)
		{
			if (m == null || m.Map == null)
				return false; // ou true, selon votre logique par défaut

			int hours, minutes;
			Server.Items.Clock.GetTime(m.Map, m.X, m.Y, out hours, out minutes);
			return hours >= 22 || hours < 6;
		}

		public static void PlaySound(Mobile from, int[] table)
        {
            if (table != null && table.Length > 0)
            {
                int random = Utility.Random(table.Length);
                from.PlaySound(table[random]);
            }
        }

		private static void Ambiences_Callback()
		{
			try
			{
				foreach (NetState ns in NetState.Instances)
				{
					Mobile m = ns.Mobile;
					if (m == null)
						continue;

					bool isNight = IsNight(m);

					switch (Deplacement.GetTileType(m))
					{
						case TileType.Desert:
							{
								break;
							}
						case TileType.Jungle:
							{
								PlaySound(m, isNight ? m_JungleNight : m_JungleDay);
								break;
							}
						case TileType.Forest:
							{
								PlaySound(m, isNight ? m_ForestNight : m_ForestDay);
								break;
							}
						case TileType.Snow:
							{
								PlaySound(m, isNight ? m_SnowNight : m_SnowDay);
								break;
							}
						case TileType.Swamp:
							{
								PlaySound(m, isNight ? m_SwampNight : m_SwampDay);
								break;
							}
						default:
							{
								break;
							}
					}
				}

				Timer.DelayCall(TimeSpan.FromSeconds(90 + Utility.Random(30)), new TimerCallback(Ambiences_Callback));
			}
			catch (Exception ex)
			{
				Console.WriteLine("Erreur dans Ambiences_Callback: " + ex.Message);
			}
		}
		public static int[] m_JungleDay = new int[] { 3, 4, 5, 691, 692, 693 };
        public static int[] m_JungleNight = new int[] { 12, 13 };

        public static int[] m_ForestDay = new int[] { 0, 1, 2 };
        public static int[] m_ForestNight = new int[] { 8, 9, 10, 11 };

        public static int[] m_SnowDay = new int[] { 20, 21, 22 };
        public static int[] m_SnowNight = new int[] { 20, 21, 22 };

        public static int[] m_SwampDay = new int[] { 6, 7, 694, 695 };
        public static int[] m_SwampNight = new int[] { 14, 15 };
    }
}
