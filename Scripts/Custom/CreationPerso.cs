#region References
using System;
using Server.Mobiles;
using System.Reflection;
using Server.Items;
using Server.Misc;
using System.Collections.Generic;
using Server.Accounting;

#endregion

namespace Server
{
    [Parsable]
    public class CreationPerso
    {
      
        private BaseRace m_Race;
        private int m_Hue;
        private bool m_female;
		private string m_Name;
		private StatutSocialEnum m_Statut = StatutSocialEnum.Peregrin;


		private int m_Str = 25;
		private int m_Dex = 25;
		private int m_Int = 25;

		private CustomPlayerMobile m_Player;

		private Reroll m_Reroll;


		private AppearanceEnum m_Appearance = (AppearanceEnum)(-1);
		private GrandeurEnum m_Grandeur = (GrandeurEnum)(-1);
		private GrosseurEnum m_Grosseur = (GrosseurEnum)(-1);
		private int m_Classe = 0;
		private int m_Metier = 0;

		private God m_God = null;

		public BaseRace Race
        {
            get => m_Race;
            set
            {
                if (m_Race != value)
                {
                    ChangeRace();
                }
                m_Race = value;
            }
             
        }

		public int Classe
		{
			get => m_Classe;
			set
			{
				if (m_Classe != value)
				{
					ChangeClasse();
				}

				m_Classe= value;
			}

		}


		public int Metier
		{
			get => m_Metier;
			set
			{
				if (Server.Classe.GetClasse(value).Metier)  //    Juste pour s'assurer que personne le fasse... Genre utiliser razor pour choisir une classe combat au lieux de metier.
				{
					if (m_Metier != value)
					{
						ChangeMetier();
					}
					m_Metier = value;
				}
			
			}

		}

		public int Str
		{
			get => m_Str;
			set
			{
				if (value + m_Dex + m_Int > 225)
				{
					
				}
				else if (value < 25)
				{

				}
				else if (value > 100)
				{

				}
				else
				{
					m_Str = value;
				}
			}
		}

		public int Dex
		{
			get => m_Dex;
			set
			{
				if (m_Str + value + m_Int > 225)
				{

				}
				else if (value < 25)
				{

				}
				else if (value > 100)
				{

				}
				else
				{
					m_Dex = value;
				}
			}
		}

		public int Int
		{
			get => m_Int;
			set
			{
				if (m_Str + m_Dex + value > 225)
				{

				}
				else if (value < 25)
				{

				}
				else if (value > 100)
				{

				}
				else
				{
					m_Int = value;
				}
			}
		}

		public string Name { get => m_Name; set => m_Name = value; }

		public int Hue { get => m_Hue; set => m_Hue = value; }

		public God God { get => m_God; set => m_God = value; }

		public StatutSocialEnum Statut { get => m_Statut; set => m_Statut = value; }

		public AppearanceEnum Appearance { get => m_Appearance; set => m_Appearance = value; }

		public GrandeurEnum Grandeur { get => m_Grandeur; set => m_Grandeur = value; }

		public GrosseurEnum Grosseur { get => m_Grosseur; set => m_Grosseur = value; }

		public bool Female
        {
            get => m_female;
            set
            {
                if (m_female != value)
                {
                    ChangeSexe();
                }


                m_female = value;
            }
       }

		public Reroll Reroll { get => m_Reroll; set => m_Reroll = value; }

		public CreationPerso(CustomPlayerMobile player)
        {
			m_female = player.Female;
			m_Name = player.Name;
			m_Player = player;
        }
    
        public void ChangeRace()
        {
            m_Hue = -1;
			m_Appearance = (AppearanceEnum)(-1);
			m_Grandeur = (GrandeurEnum)(-1);
			m_Grosseur = (GrosseurEnum)(-1);


		}

        public void ChangeSexe()
        {
            
        }

		public void ChangeClasse()
		{		
				m_Classe = 0;			
				m_Metier = 0;
		}

		public void ChangeMetier()
		{
				m_Metier = 0;
		}

		public bool InfoGeneral()
		{

			if (m_Appearance == (AppearanceEnum)(-1) || m_Grandeur == (GrandeurEnum)(-1) || m_Grosseur == (GrosseurEnum)(-1))
			{
				return false;
			}
			else if (Name.Length < 3)
			{
				return false;
			}
			else
			{
				return true;
			}			
		}

		public bool Statistique()
		{

			if (m_Int + m_Dex + m_Str == 225)
			{
				return true;
			}

			return false;


		}

		public bool ClasseSelection( int Stade)
		{
			if (Stade == 0 && Classe != 0)
			{
				return true;
			}
			
			else if (Stade == 1 && Metier != 0)
			{
				return true;
			}


			return false;
			
		}

		public string Title(int stade)
		{
			switch (stade)
			{
				case 0:
					{
						return "Classe";
					}
				case 1:
					{
						return "Metier";
					}
				default:
					return "Classe";
			}



		}

		public string GetApparence()
		{
			if (m_Appearance != (AppearanceEnum)(-1))
			{
				var type = typeof(AppearanceEnum);
				MemberInfo[] memberInfo = type.GetMember(m_Appearance.ToString());
				Attribute attribute = memberInfo[0].GetCustomAttribute(typeof(AppearanceAttribute), false);
				return (Female ? ((AppearanceAttribute)attribute).FemaleAdjective : ((AppearanceAttribute)attribute).MaleAdjective);
			}

			return "";
		}

		public string GetGrosseur()
		{
			if (Grosseur != (GrosseurEnum)(-1))
			{
				var type = typeof(GrosseurEnum);
				MemberInfo[] memberInfo = type.GetMember(m_Grosseur.ToString());
				Attribute attribute = memberInfo[0].GetCustomAttribute(typeof(AppearanceAttribute), false);
				return (Female ? ((AppearanceAttribute)attribute).FemaleAdjective : ((AppearanceAttribute)attribute).MaleAdjective);
			}

			return "";
		}

		public string GetGrandeur()
		{
			if (Grandeur != (GrandeurEnum)(-1))
			{
				var type = typeof(GrandeurEnum);
				MemberInfo[] memberInfo = type.GetMember(m_Grandeur.ToString());
				Attribute attribute = memberInfo[0].GetCustomAttribute(typeof(AppearanceAttribute), false);
				return (Female ? ((AppearanceAttribute)attribute).FemaleAdjective : ((AppearanceAttribute)attribute).MaleAdjective);
			}

			return "";
		}

		public int PointRestant()
		{
			return (225 - m_Str - m_Dex - m_Int);
		}

		public void Valide()
        {
			m_Player.BaseFemale = m_female;
			m_Player.BaseRace = m_Race;
			m_Player.Race.RemoveRace(m_Player);	
			Race.AddRace(m_Player, m_Hue);
			m_Player.Name = m_Name;
			m_Player.Beaute = m_Appearance;
			m_Player.Grandeur = m_Grandeur;
			m_Player.Grosseur = m_Grosseur;
			m_Player.BaseHue = m_Hue;



			m_Player.InitStats(m_Str, m_Dex, m_Int);

			m_Player.Classe = Server.Classe.GetClasse(m_Classe);
			m_Player.Metier = Server.Metier.GetMetier(Metier);

			m_Player.God = God;

			m_Player.StatutSocial = m_Statut;

			m_Player.ChangeTribeValue(TribeType.Legion, 25);

	//		Dictionary<SkillName, int> m_skills = new Dictionary<SkillName, int>();



			if (m_Reroll != null)
			{
				m_Player.FENormalTotal += (int)m_Reroll.ExperienceNormal;
				m_Player.FERPTotal += (int)Math.Round(m_Reroll.ExperienceRP * 0.5);

				Account acc = (Account)m_Player.Account;

				acc.RemoveReroll(m_Reroll);


				
			}



			int GoldNumber = 5000;
			

			m_Player.AddToBackpack(new Gold(GoldNumber));
			

			m_Player.MoveToWorld(new Point3D(6132, 2566, 55), Map.Felucca);
			m_Player.Blessed = false;
			Robe robe = new Robe();

			m_Player.AddItem(robe);

		}

    }


}
