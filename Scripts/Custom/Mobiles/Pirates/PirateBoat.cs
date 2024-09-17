#region References
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Server
{

	[Parsable]
	public class PirateBoat
	{

				public static void Configure()
				{
			//		Metier.RegisterMetier(new Metier(0, "Aucune",0,0,false, new List<int>(){1,2,3,4}, new Dictionary<SkillName, double>() { },new List<int>(){} )); 

					
					PirateBoat.RegisterPirateBoat(new PirateBoat(0,"de La ","Kerpie",1324, 1328));
					PirateBoat.RegisterPirateBoat(new PirateBoat(1,"de L'","Insoumise",2098, 1785));
					PirateBoat.RegisterPirateBoat(new PirateBoat(2,"du ","Courroux",1940, 1779));
				}
		

		private static readonly List<PirateBoat> m_AllPirateBoat = new List<PirateBoat>();

		public static List<PirateBoat> AllPirateBoat => m_AllPirateBoat;

		private readonly int m_PirateBoatID;
		private string m_Name;
		private string m_Pronom;

		private int m_MainHue;

		private int m_AltHue;
		public static PirateBoat GetPirateBoat(int Id)
		{
			foreach (PirateBoat item in m_AllPirateBoat)
			{
				if (item.PirateBoatID == Id)
				{
					return item;
				}
			}

			return null;
		}

		public int PirateBoatID => m_PirateBoatID;

		public string Name { get => m_Name; set => m_Name = value; }

		public string Pronom { get => m_Pronom; set => m_Pronom = value; }


		public int MainHue { get => m_MainHue; set => m_MainHue = value; }
		public int AltHue { get => m_AltHue; set => m_AltHue = value; }


		public override string ToString()
		{
			return m_Name;
		}

		public string ToStringWithPronom()
		{
			return m_Pronom + m_Name;
		}



		public PirateBoat(
			int PirateBoatID,
			string pronom,
			string name,
			int mainHue,
			int altHue

			)
		{
			m_PirateBoatID = PirateBoatID;

			m_Name = name;
			m_MainHue = mainHue;
			m_AltHue = altHue;
			m_Pronom = pronom;

		}
		public static void RegisterPirateBoat(PirateBoat PirateBoat)
		{
			PirateBoat.AllPirateBoat.Add(PirateBoat);
		}



		public static string[] GetPirateBoatsNames()
		{

			List<string> PirateBoatName = new List<string>();

			foreach (PirateBoat item in AllPirateBoat)
			{
				PirateBoatName.Add(item.Name);
			}

			return PirateBoatName.ToArray();
		}

	}
}
