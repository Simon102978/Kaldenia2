#region References
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Server
{

	[Parsable]
	public class Metier
	{
		

		private static readonly List<Metier> m_AllMetier = new List<Metier>();

		public static List<Metier> AllMetier => m_AllMetier;

		private readonly int m_MetierID;
		private string m_Name;
		private bool m_Hidden;
		private int m_MetierLvl;
		private int m_Armor;

		private List<int> m_Evolution = new List<int>();

		private List<int> m_ClasseIncompatible = new List<int>();
		private Dictionary<SkillName, double> m_Skill = new Dictionary<SkillName, double>();

		private Dictionary<MagieType, int> m_MagicAffinity  = new Dictionary<MagieType, int>();
		public static Metier GetMetier(int Id)
		{
			foreach (Metier item in m_AllMetier)
			{
				if (item.MetierID == Id)
				{
					return item;
				}
			}

			return null;
		}

		public int MetierID => m_MetierID;

		public string Name { get => m_Name; set => m_Name = value; }
		public bool Hidden { get => m_Hidden; set => m_Hidden = value; }

		public int MetierLvl { get => m_MetierLvl; set => m_MetierLvl = value; }
		public int Armor { get => m_Armor; set => m_Armor = value; }

		public Dictionary<SkillName, double> Skill { get => m_Skill; set => m_Skill = value; }

		public List<int> Evolution { get => m_Evolution; set => m_Evolution = value; }

		public List<int> ClasseIncompatible { get => m_ClasseIncompatible; set => m_ClasseIncompatible = value; }

		public Dictionary<MagieType, int> MagicAffinity { get => m_MagicAffinity; set => m_MagicAffinity = value; }

		public int NiveauRequis 
		{ 
			get 
			{
				return LevelToEvolve(MetierLvl);
			} 
			
		}

		public override string ToString()
		{
			return m_Name;
		}





		public Metier(
			int MetierID,
			string name,
			int classLvl,
			int armor,
			bool hidden,
			List<int> evolution,
			Dictionary<SkillName, double> skill, 
			List<int> ClasseIncompatible
			)
		{
			m_MetierID = MetierID;
			m_Hidden = hidden;
			m_Name = name;
			m_Skill = skill;
			m_MetierLvl = classLvl;
			m_Armor = armor;
			m_Evolution = evolution;
			m_ClasseIncompatible = ClasseIncompatible;

		}
		public static void RegisterMetier(Metier Metier)
		{
			Metier.AllMetier.Add(Metier);
		}



		public double GetSkillValue(SkillName sname)
		{
			if (Skill.ContainsKey(sname))
			{
				return Skill[sname];
			}
			return 0;
		}

		public static string[] GetMetiersNames()
		{

			List<string> MetierName = new List<string>();

			foreach (Metier item in AllMetier)
			{
				MetierName.Add(item.Name);
			}

			return MetierName.ToArray();
		}

		public static int LevelToEvolve(int MetierLvl)
		{		 
				switch (MetierLvl)
				{
					
					case 0:					
						return 0; 					
					case 1:
						return 10;
					case 2:				
						return 20; 
					default:
						return 99;
				}	
		}




		public string MetierDescription()
		{
			string description = "";	

			description = Name + "\n";
	
			description = description + "\nArmure: " + Armor;

			description = description + "\n\nNiveau de classe / Point d'évolution requis: " + MetierLvl;

			description = description + "\n\n" + SkillsDescription();

			description = description + "\n\n" ;


			return description;
		}

		public string SkillsDescription()
		{
			string description = "Compétences: \n" ;

			var sortedDict = from entry in Skill orderby entry.Value descending select entry;

			foreach (KeyValuePair<SkillName,double> item in sortedDict)
			{
				description = description + "  -" + item.Key.ToString() + ": " + item.Value+ "\n";
			}

			return description;
		}

		public string DevotionDescription()
		{
			string description = "Dévotions: \n";

			var sortedDict2 = from entry in MagicAffinity orderby entry.Value descending select entry;

			foreach (KeyValuePair<MagieType, int> item in sortedDict2)
			{
				description = description + "  -" + item.Key.ToString() + ": " + item.Value.ToString() + "\n";
			}

			return description;
		}





	}
}
