using System;
using System.IO;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Gumps;
using System.Collections.Generic;
using Server.Custom;

namespace Server
{
  public class XPLevel
  {
		public static Dictionary<int, XPLevel> XpTable = new Dictionary<int, XPLevel>()
		{ 
			{0, new XPLevel(0,30)}, // Classe 1
			{1, new XPLevel(3,35)},
			{2, new XPLevel(6,40)},
			{3, new XPLevel(9,45)},
			{4, new XPLevel(12,47)},
			{5, new XPLevel(16,50)}, //Classe2
			{6, new XPLevel(21,52)},
			{7, new XPLevel(26,54)}, 
			{8, new XPLevel(31,56)},
			{9, new XPLevel(37,58)},
			{10, new XPLevel(45,60)}, 
			{11, new XPLevel(52,63)},
			{12, new XPLevel(61,66)},
			{13, new XPLevel(70,69)},
			{14, new XPLevel(81,72)},
			{15, new XPLevel(91,75)},// classe 3
			{16, new XPLevel(103,77)},
			{17, new XPLevel(115,79)},
			{18, new XPLevel(129,81)},
			{19, new XPLevel(142,83)},
			{20, new XPLevel(156,85)},// Classe 4
			{21, new XPLevel(171,87)},
			{22, new XPLevel(186,89)},
			{23, new XPLevel(201,91)}, 
			{24, new XPLevel(216,93)},
			{25, new XPLevel(231,95)},
			{26, new XPLevel(246,96)},
			{27, new XPLevel(262,97)},
			{28, new XPLevel(279,98)},
			{29, new XPLevel(297,99)},
			{30, new XPLevel(315,100)}, // Classe 5
			{31, new XPLevel(333,100)},
			{32, new XPLevel(351,100)},
			{33, new XPLevel(369,100)},
			{34, new XPLevel(387,100)},
			{35, new XPLevel(405,100)},
			{36, new XPLevel(423,100)},			
			{37, new XPLevel(441,100)},
			{38, new XPLevel(459,100)},
			{39, new XPLevel(477,100)},
			{40, new XPLevel(495,100)},			
		};

		private int m_FeRequis = 0;
		private double m_MaxSkill = 0;

		public int FeRequis { get => m_FeRequis; set => m_FeRequis = value; }
		public double MaxSkill { get => m_MaxSkill; set => m_MaxSkill = value; }

		public XPLevel(int feRequis, double maxSkill)
		{
				m_FeRequis = feRequis;
				m_MaxSkill = maxSkill;
		}

		public static int CalculateLevel(int feTotal)
		{
			int level = 0;
			foreach (var entry in XpTable)
			{
				if (feTotal >= entry.Value.FeRequis)
				{
					level = entry.Key;
				}
				else
				{
					break;
				}
			}
			return level;
		}

		public static XPLevel GetLevel(int niveau)
		{
			int lvl = niveau;

			if (lvl > 40)
			{
				lvl = 40;
			}
			else if(lvl < 0)
			{
				lvl = 0;
			}

			return XpTable[lvl];		
		}





  }
}