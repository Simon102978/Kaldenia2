#region References
using Server.Network;
using System.Reflection;
using System;
using System.Collections.Generic;
using Server.Items;
using Server.Custom.Misc;
using System.Collections;
using Server.Custom;
using Server.Movement;
using Server.Gumps;
using Server.Multis;


#endregion

namespace Server.Mobiles
{


	public partial class CustomPlayerMobile : PlayerMobile
	{
		public static List<SkillName> SkillGeneral = new List<SkillName>() { SkillName.Mining, SkillName.Lumberjacking, SkillName.Fishing, SkillName.MagicResist };

		private GrandeurEnum m_Grandeur;
		private GrosseurEnum m_Grosseur;
		private AppearanceEnum m_Beaute;

		private Classe m_ClassePrimaire = Classe.GetClasse(-1);
		private Classe m_ClasseSecondaire = Classe.GetClasse(-1);
		private Classe m_Metier = Classe.GetClasse(-1);

		private List<Mobile> m_Esclaves = new List<Mobile>();
		private CustomPlayerMobile m_Maitre;

		private StatutSocialEnum m_StatutSocial = StatutSocialEnum.Aucun;

		private Container m_Corps;
		private int m_StatAttente;
		private int m_fe;
		private int m_feAttente;
		private int m_TotalNormalFE;
		private int m_TotalRPFE;

		private int m_feDay;

		private DateTime m_lastLoginTime;
		private TimeSpan m_nextFETime;
		private DateTime m_LastFERP;

		private DateTime m_LastNormalFE;


		private DateTime m_LastPay;
		private int m_Salaire;


		private God m_God = God.GetGod(-1);
		private AffinityDictionary m_MagicAfinity;
		private List<int> m_QuickSpells = new List<int>();


		private int m_IdentiteId;
		private Dictionary<int, Deguisement> m_Deguisement = new Dictionary<int, Deguisement>();

		private TribeRelation m_TribeRelation;

		private bool m_Masque = false;

		private Race m_BaseRace;
		private bool m_BaseFemale;
		private int m_BaseHue;

		public bool Hallucinating;

		public bool m_Vulnerability;

		private List<MissiveContent> m_MissiveEnAttente = new List<MissiveContent>();

		#region Possess
		private Mobile m_Possess;
		private Mobile m_PossessStorage;

		public Mobile Possess
		{
			get { return m_Possess; }
			set { m_Possess = value; }
		}

		public Mobile PossessStorage
		{
			get { return m_PossessStorage; }
			set { m_PossessStorage = value; }
		}
		#endregion

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Journaliste { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime LastDeathTime { get; private set; }
		public double DeathDuration => 1; //minutes

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime EndOfVulnerabilityTime { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Vulnerability
		{
			get
			{
				if (m_Vulnerability && EndOfVulnerabilityTime <= DateTime.Now)
				{
					m_Vulnerability = false;
					SendMessage(HueManager.GetHue(HueManagerList.Green), "Vous n'êtes plus vulnérable. La prochaine fois que vous tomberez au combat, vous serez assomé.");
				}

				return m_Vulnerability;
			}
			set
			{
				m_Vulnerability = value;
			}

		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool DeathShot { get; set; }
		public int VulnerabilityDuration => 5; //minutes

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime PreventPvpAttackTime { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public bool PreventPvpAttack { get; set; }
		public int PreventPvpAttackDuration => 1; //minutes

		[CommandProperty(AccessLevel.GameMaster)]
		public GrosseurEnum Grosseur { get => m_Grosseur; set => m_Grosseur = value; }

		[CommandProperty(AccessLevel.GameMaster)]
		public GrandeurEnum Grandeur { get => m_Grandeur; set => m_Grandeur = value; }

		[CommandProperty(AccessLevel.GameMaster)]
		public AppearanceEnum Beaute { get => m_Beaute; set => m_Beaute = value; }

		[CommandProperty(AccessLevel.GameMaster)]
		public Classe ClassePrimaire
		{
			get => m_ClassePrimaire;
			set
			{
				if (CheckClassePrimaire(value))
				{
					RecalculeClasse(value, 1);

					m_ClassePrimaire = value; // S'assurer que le metier, soit un metier...
				}
			}

		}


		[CommandProperty(AccessLevel.GameMaster)]
		public string BaseName
		{
			get => GetBaseName();
		}


		[CommandProperty(AccessLevel.Administrator)]
		public bool IsHallucinating
		{
			get { return Hallucinating; }
			set { Hallucinating = value; InvalidateProperties(); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public Classe ClasseSecondaire {
			get => m_ClasseSecondaire;
			set
			{
				if (CheckClasseSecondaire(value))
				{
					RecalculeClasse(value, 2);

					m_ClasseSecondaire = value; // S'assurer que le metier, soit un metier...

				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public Classe Metier
		{
			get => m_Metier;
			set
			{
				if (value.ClasseType == ClasseType.Metier || value.ClasseType == ClasseType.None)
				{
					if (CheckMetier(value))
					{
						RecalculeClasse(value, 3);
						m_Metier = value;  // S'assurer que le metier, soit un metier...
					}

					// S'assurer que le metier, soit un metier...
				}
			}

		}

		[CommandProperty(AccessLevel.Owner)]
		public StatutSocialEnum StatutSocial
		{
			get => m_StatutSocial;
			set
			{
				if (m_StatutSocial == StatutSocialEnum.Possession && value >= StatutSocialEnum.Peregrin && m_Maitre != null)
				{
					m_Maitre.RemoveEsclave(this, false);
				}
				m_StatutSocial = value;
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime LastLoginTime
		{
			get { return m_lastLoginTime; }
			set { m_lastLoginTime = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime LastPay
		{
			get { return m_LastPay; }
			set { m_LastPay = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int Salaire { get { return m_Salaire; } set { m_Salaire = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int StatAttente { get { return m_StatAttente; } set { m_StatAttente = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public TimeSpan NextFETime
		{
			get { return m_nextFETime; }
			set { m_nextFETime = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime LastFERP
		{
			get { return m_LastFERP; }
			set { m_LastFERP = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime LastNormalFE
		{
			get { return m_LastNormalFE; }
			set { m_LastNormalFE = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int FE { get { return m_fe; } set { m_fe = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int FEDay { get { return m_feDay; } set { m_feDay = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int FENormalTotal { get { return m_TotalNormalFE; } set { m_TotalNormalFE = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int FERPTotal { get { return m_TotalRPFE; } set { m_TotalRPFE = value; } }


		[CommandProperty(AccessLevel.GameMaster)]
		public int FETotal { get { return m_TotalNormalFE + m_TotalRPFE; } }



		[CommandProperty(AccessLevel.GameMaster)]
		public int FEAttente { get { return m_feAttente; } set { m_feAttente = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int Armure { get => m_ClassePrimaire.Armor + m_ClasseSecondaire.Armor; }

		[CommandProperty(AccessLevel.GameMaster)]
		public Container Corps { get { return m_Corps; } set { m_Corps = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public God God
		{
			get => m_God;
			set
			{

				//MagicAfinity.ChangeGod(value);


				m_God = value; // S'assurer que le metier, soit un metier...


			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public AffinityDictionary MagicAfinity { get { return m_MagicAfinity; } set { m_MagicAfinity = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public TribeRelation TribeRelation { get { return m_TribeRelation; } set { m_TribeRelation = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public Item ChosenSpellbook { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public Dictionary<int, Deguisement> Deguisement { get { return m_Deguisement; } set { m_Deguisement = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int IdentiteID
		{
			get => m_IdentiteId;
			set
			{
				if (!m_Deguisement.ContainsKey(value))
				{
					m_Deguisement.Add(value, new Deguisement(this));
				}

				m_IdentiteId = value;
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public Race BaseRace
		{
			get
			{
				return m_BaseRace;
			}
			set
			{
				m_BaseRace = value;
				Race = value;
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool BaseFemale
		{
			get
			{
				return m_BaseFemale;
			}
			set
			{
				m_BaseFemale = value;
				Female = value;
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int BaseHue
		{
			get
			{
				return m_BaseHue;
			}
			set
			{
				m_BaseHue = value;
				Hue = value;
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int TitleCycle { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public string customTitle { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public QuiOptions QuiOptions
		{
			get;
			set;
		}
		public List<MissiveContent> MissiveEnAttente { get { return m_MissiveEnAttente; } set { m_MissiveEnAttente = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Masque
		{
			get
			{
				return m_Masque;
			}
			set
			{
				if (m_Masque)
				{
					NameMod = null;
					m_Masque = value;
					SendMessage("Votre identité est revelée.");
				}
				else if (NameMod != null)
				{

				}
				else
				{
					NameMod = "Identite masquee";
					m_Masque = value;
				}
			}
		}

		public List<int> QuickSpells
		{
			get { return m_QuickSpells; }
			set { m_QuickSpells = value; }
		}


		public List<Mobile> Esclaves { get { return m_Esclaves; } set { m_Esclaves = value; } }

		[CommandProperty(AccessLevel.Owner)]
		public CustomPlayerMobile Maitre { get { return m_Maitre; } set { m_Maitre = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime JailTime { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public Point3D JailLocation { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public Map JailMap { get; set; }

		private bool m_Jail = false;

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Jail
		{
			get
			{

				if (m_Jail && DateTime.Now >= JailTime)
				{
					JailRelease();
				}


				return m_Jail;
			}
			set
			{
				if (!m_Jail && value)
				{
					JailP(null,TimeSpan.FromDays(7));
				}
				else
				{
					JailRelease();
				}

				m_Jail = value;

			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile JailBy { get; set; }

		public DateTime NextFaimMessage { get; set; }

		public DateTime NextSoifMessage { get; set; }

		public DateTime NextFrapper { get; set; }
		public CustomPlayerMobile()
		{
			MagicAfinity = new AffinityDictionary(this);
			TribeRelation = new TribeRelation(this);
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			if (Vulnerability)
			{
				list.Add(1050045, "<\th3><basefont color=#FF8000>" + (Female ? "ASSOMÉE" : "ASSOMÉ") + "</basefont></h3>\t");
			}

			if (NameMod == null)
			{
				list.Add(1050045, "{0}, \t{1}\t", Race.Name, Apparence()); // ~1_PREFIX~~2_NAME~~3_SUFFIX~
				list.Add(1050045, "{0}, \t{1}\t", GrandeurString(), GrosseurString());
			}
		}

		#region Missive
		public virtual void AddMissive(Missive missive)
		{
			if (missive == null || missive.Deleted)
				return;

			MissiveEnAttente.Add(missive.Content);

			missive.Delete();

			SendMessage("Vous avez reçu une missive.");
		}

		public virtual void GetMissive()
		{
			for (int i = 0; i < MissiveEnAttente.Count; ++i)
			{
				MissiveContent entry = MissiveEnAttente[i];

				if (entry != null)
				{
					AddToBackpack(new Missive(entry));
				}
			}
			MissiveEnAttente = new List<MissiveContent>();
		}

		#endregion

		private static void OnLogin(LoginEventArgs e)
		{

		}


		public bool AddEsclave(Mobile m)
		{
			if (RoomForSlave())
			{
				if (m is CustomPlayerMobile cp)
				{

					cp.Maitre = this;
					cp.StatutSocial = StatutSocialEnum.Possession;
					m_Esclaves.Add(m);

				}
				else
				{
					m_Esclaves.Add(m);
				}

				return true;
			}
			else
			{
				SendMessage("Vous avez déjà atteint votre maximum d'esclaves.");
				return false;
			}
		}

		public void RemoveEsclave(Mobile m, bool affranchir = false)
		{
			if (m is CustomPlayerMobile cp)
			{
				if (affranchir)
				{
					cp.Maitre = null;
					cp.StatutSocial = StatutSocialEnum.Peregrin;
					cp.SendMessage("Vous êtes maintenenant un Pérégrin.");
				}
				else
				{
					cp.Maitre = null;
					cp.StatutSocial = StatutSocialEnum.Dechet;
					cp.SendMessage("Vous êtes maintenenant un Déchet.");
				}
			}

			List<Mobile> newEsclave = new List<Mobile>();

			foreach (Mobile item in m_Esclaves)
			{
				if (m != item)
				{
					newEsclave.Add(item);
				}
			}
			m_Esclaves = newEsclave;

		}

		public bool RoomForSlave()
		{
			if (AccessLevel > AccessLevel.Player)
			{
				return true;
			}
			return MaxEsclave() >= m_Esclaves.Count + 1;
		}

		public int MaxEsclave()
		{
			if (AccessLevel > AccessLevel.Player)
			{
				return 1000;
			}

			switch (StatutSocial)
			{
				case StatutSocialEnum.Aucun:
					return 0;

				case StatutSocialEnum.Dechet:
					return 0;
				case StatutSocialEnum.Possession:
					return 0;
				case StatutSocialEnum.Peregrin:
					return 0;
				case StatutSocialEnum.Civenien:
					return 2;
				case StatutSocialEnum.Equite:
					return 5;
				case StatutSocialEnum.Patre:
					return 10;
				case StatutSocialEnum.Magistrat:
					return 30;
				case StatutSocialEnum.Empereur:
					return 1000;
				default:
					return 0;
			}


		}


		#region Hiding


		public override void Reveal(Mobile m)
		{
			if (m is CustomPlayerMobile)
			{
				if (VisibilityList.Contains(m))
				{

				}
				else
				{
					VisibilityList.Add(m);
					m.SendMessage("Vous avez detecté " + Name + ".");
				}

				if (Utility.InUpdateRange(m, this))
				{
					NetState ns = m.NetState;

					if (ns != null)
					{
						if (m.CanSee(this))
						{
							ns.Send(new MobileIncoming(m, this));

							ns.Send(this.OPLPacket);

							foreach (Item item in this.Items)
								ns.Send(item.OPLPacket);
						}
						else
						{
							ns.Send(this.RemovePacket);
						}
					}
				}
			}
			else if (m is BaseCreature && ((BaseCreature)m).IsBonded) // Grosso modo ici, c'est pour permettre de detecter sans revealer avec les familiers, car certains on du DH.
			{

				BaseCreature sd = (BaseCreature)m;

				Mobile cm = sd.ControlMaster;

				if (VisibilityList.Contains(cm))
				{

				}
				else
				{
					VisibilityList.Add(cm);
					cm.SendMessage(sd.Name + " vous indique la présence de " + Name + ".");
				}

				if (Utility.InUpdateRange(cm, this))
				{
					NetState ns = cm.NetState;

					if (ns != null)
					{
						if (cm.CanSee(this))
						{
							ns.Send(new MobileIncoming(cm, this));

							ns.Send(this.OPLPacket);

							foreach (Item item in this.Items)
								ns.Send(item.OPLPacket);
						}
						else
						{
							ns.Send(this.RemovePacket);
						}
					}
				}
			}
			else
			{
				RevealingAction();
			}
		}

		public override void RevealingAction()
		{
			if (Hidden)
			{
				VisibilityList = new List<Mobile>();
			}
			base.RevealingAction();
		}


		public override int GetHideBonus()
		{
			int bonus = 0;

			double chance = 0.80 * GetBagFilledRatio(this);

			if (chance >= Utility.RandomDouble())
				bonus -= 10;

			int ar = Server.SkillHandlers.Hiding.GetArmorRating(this);


			if (ar >= 90)
			{
				bonus -= 50;
			}
			else if (ar >= 75)
			{
				bonus -= 40;
			}
			else if (ar >= 60)
			{
				bonus -= 30;
			}
			else if (ar >= 40)
			{
				bonus -= 20;
			}
			else if (ar >= 20)
			{
				bonus -= 10;
			}

			return base.GetHideBonus() + bonus;
		}


		public override int GetDetectionBonus(Mobile mobile)
		{
			int bonus = 0;

			if (FindItemOnLayer(Layer.TwoHanded) is BaseEquipableLight)
			{
				BaseEquipableLight Light = (BaseEquipableLight)FindItemOnLayer(Layer.TwoHanded);

				ComputeLightLevels(out int global, out int personal);

				int lightLevel = global + personal;


				if (lightLevel >= 20 && Light.Burning)
				{
					bonus += 10;
				}
			}


			//	bonus += GetAptitudeValue(AptitudeEnum.Predation) * 3;
			// Mettre l'aptitude de rodeur ici

			return base.GetDetectionBonus(mobile) + bonus;
		}

		public static double GetBagFilledRatio(CustomPlayerMobile pm)
		{
			Container pack = pm.Backpack;

			if (pm.AccessLevel >= AccessLevel.GameMaster)
				return 0;

			if (pack != null)
			{
				//        int maxweight = WeightOverloading.GetMaxWeight(pm);

				int maxweight = pm.MaxWeight;

				double value = (pm.TotalWeight / maxweight) - 0.50;

				if (value < 0)
					value = 0;

				if (value > 0.50)
					value = 0.50;

				return value;
			}

			return 0;
		}

		#endregion

		#region statutsocial	

		public string StatutSocialString()
		{

			StatutSocialEnum gros = m_StatutSocial;

			if (Deguise)
			{
				gros = GetDeguisement().StatutSocial;
			}


			if (gros < 0)
			{
				gros = 0;
			}
			else if ((int)gros > 8)
			{
				gros = (StatutSocialEnum)8;
			}

			var type = typeof(StatutSocialEnum);
			MemberInfo[] memberInfo = type.GetMember(gros.ToString());
			Attribute attribute = memberInfo[0].GetCustomAttribute(typeof(AppearanceAttribute), false);
			return (Female ? ((AppearanceAttribute)attribute).FemaleAdjective : ((AppearanceAttribute)attribute).MaleAdjective);
		}

		public int GainGold(int gold, bool bank = false)
		{
			int GainGold = gold;
			int taxesGold = 0;


			if (Race.RaceID != 0)
			{
				taxesGold = (int)Math.Round((GainGold * 0.1), 0, MidpointRounding.AwayFromZero);
			}

			switch (m_StatutSocial)
			{
				case StatutSocialEnum.Aucun:
					break;
				case StatutSocialEnum.Dechet:
					taxesGold = GainGold;
					GainGold -= taxesGold;
					break;
				case StatutSocialEnum.Possession:
					taxesGold += (int)Math.Round((GainGold * 0.5), 0, MidpointRounding.AwayFromZero);
					GainGold -= taxesGold;
					break;
				case StatutSocialEnum.Peregrin:
					taxesGold += (int)Math.Round((GainGold * 0.5), 0, MidpointRounding.AwayFromZero);
					GainGold -= taxesGold;
					break;
				case StatutSocialEnum.Civenien:
					taxesGold += (int)Math.Round((GainGold * 0.4), 0, MidpointRounding.AwayFromZero);
					GainGold -= taxesGold;
					break;
				case StatutSocialEnum.Equite:
					taxesGold += (int)Math.Round((GainGold * 0.25), 0, MidpointRounding.AwayFromZero);
					GainGold -= taxesGold;
					break;
				case StatutSocialEnum.Patre:
					taxesGold += (int)Math.Round((GainGold * 0.15), 0, MidpointRounding.AwayFromZero);
					GainGold -= taxesGold;
					break;
				case StatutSocialEnum.Magistrat:
					break;
				case StatutSocialEnum.Empereur:
					break;
				default:
					break;
			}

			if (bank)
			{
				if (Banker.Deposit(this, GainGold))
				{
					SendMessage(HueManager.GetHue(HueManagerList.Green), "Votre guilde a déposé votre salaire de " + GainGold + " pièces d'or dans votre coffre de banque.");
				}
			}
			else
			{
				while (GainGold > 60000)
				{
					AddToBackpack(new Gold(60000));
					GainGold -= 60000;
				}

				AddToBackpack(new Gold(GainGold));

				PlaySound(0x0037); //Gold dropping sound
			}


			if (AccessLevel == AccessLevel.Player)
			{

				CustomPersistence.TaxesMoney += taxesGold;
			}



			return GainGold;




		}


		public void GainSalaire(CustomGuildMember cgm)
		{

			int gold = cgm.Salaire;


			if (m_LastPay.Day != DateTime.Now.Day)
			{
				Salaire = 0;
				m_LastPay = DateTime.Now;

			}


			if (gold > Salaire)
			{
				int Payment = gold - Salaire;

				Server.Custom.System.GuildRecruter.PayLog(cgm, Payment);
				GainGold(Payment, true);

				if (AccessLevel == AccessLevel.Player)
				{
					CustomPersistence.Salaire += Payment;
				}



				Salaire = Payment;
			}
		}

		#endregion

		#region Apparence

		public string Apparence()
		{
			AppearanceEnum gros = m_Beaute;

			if (Deguise)
			{
				gros = GetDeguisement().Appearance;
			}

			if (gros < 0)
			{
				gros = 0;
			}
			else if ((int)gros > 19)
			{
				gros = (AppearanceEnum)19;
			}

			var type = typeof(AppearanceEnum);
			MemberInfo[] memberInfo = type.GetMember(gros.ToString());
			Attribute attribute = memberInfo[0].GetCustomAttribute(typeof(AppearanceAttribute), false);
			return (Female ? ((AppearanceAttribute)attribute).FemaleAdjective : ((AppearanceAttribute)attribute).MaleAdjective);
		}


		public string GrosseurString()
		{

			GrosseurEnum gros = m_Grosseur;

			if (Deguise)
			{
				gros = GetDeguisement().Grosseur;
			}


			if (gros < 0)
			{
				gros = 0;
			}
			else if ((int)gros > 8)
			{
				gros = (GrosseurEnum)8;
			}

			var type = typeof(GrosseurEnum);
			MemberInfo[] memberInfo = type.GetMember(gros.ToString());
			Attribute attribute = memberInfo[0].GetCustomAttribute(typeof(AppearanceAttribute), false);
			return (Female ? ((AppearanceAttribute)attribute).FemaleAdjective : ((AppearanceAttribute)attribute).MaleAdjective);
		}

		public string GrandeurString()
		{
			/*        Identite id = GetIdentite();

                    if (id != null && id.Grandeur != GrandeurEnum.None)
                    {
                        return id.Grandeur.ToString();
                    }*/

			GrandeurEnum gros = m_Grandeur;

			if (Deguise)
			{
				gros = GetDeguisement().Grandeur;
			}

			var type = typeof(GrandeurEnum);
			MemberInfo[] memberInfo = type.GetMember(gros.ToString());
			Attribute attribute = memberInfo[0].GetCustomAttribute(typeof(AppearanceAttribute), false);
			return (Female ? ((AppearanceAttribute)attribute).FemaleAdjective : ((AppearanceAttribute)attribute).MaleAdjective);

		}
		#endregion

		#region Déguisement

		public override string DeguisementName()
		{
			if (Deguise) // Devrais pas etre autre choses
			{
				return GetDeguisement().Name;

			}
			return base.DeguisementName();
		}

		public override string DeguisementProfile()
		{
			if (Deguise) // Devrais pas etre autre choses
			{
				return GetDeguisement().GetProfile();
			}
			return base.DeguisementName();
		}

		public bool DeguisementAction(DeguisementAction action)
		{
			double skill = Skills[SkillName.Hiding].Value;

			switch (action)
			{
				case Server.DeguisementAction.Nom:
					{
						if (skill > 50)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				case Server.DeguisementAction.Titre:
					{
						if (skill > 30)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				case Server.DeguisementAction.Sexe:
					{
						if (skill > 75)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				case Server.DeguisementAction.Race:
					{
						if (skill > 80)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				case Server.DeguisementAction.Racehue:
					{
						if (skill > 80)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				case Server.DeguisementAction.Apparence:
					{
						if (skill > 60)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				case Server.DeguisementAction.Grandeur:
					{
						if (skill > 65)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				case Server.DeguisementAction.Grosseur:
					{
						if (skill > 65)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				case Server.DeguisementAction.Paperdoll:
					{
						if (skill > 70)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				case Server.DeguisementAction.StatutSocial:
					{
						if (skill > 90)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				case Server.DeguisementAction.Identite:
					{
						if (skill > 80)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				default:
					return true;
			}






		}

		public bool CacheIdentite()
		{
			foreach (Layer Laitem in Enum.GetValues(typeof(Layer)))
			{
				Item item = FindItemOnLayer(Laitem);

				if (item is BaseClothing)
				{
					BaseClothing bc = (BaseClothing)item;

					if (bc.Disguise)
					{
						return true;
					}
				}
				else if (item is BaseArmor)
				{
					BaseArmor bc = (BaseArmor)item;

					if (bc.Disguise)
					{
						return true;
					}
				}


			}
			return false;
		}


		public Deguisement GetDeguisement()
		{
			if (m_Deguisement.ContainsKey(IdentiteID))
			{
				return m_Deguisement[IdentiteID];
			}

			return new Deguisement(this);
		}

		public Deguisement GetDeguisement(int Identite)
		{
			if (m_Deguisement.ContainsKey(Identite))
			{
				return m_Deguisement[Identite];
			}

			return new Deguisement(this);
		}

		public void SetDeguisement(Deguisement deg)
		{
			if (m_Deguisement.ContainsKey(IdentiteID))
			{
				m_Deguisement[IdentiteID] = deg;
			}
			else
			{
				m_Deguisement.Add(IdentiteID, deg);
			}


		}


		#endregion

		public CustomPlayerMobile(Serial s)
			: base(s)
		{
			MagicAfinity = new AffinityDictionary(this);
			TribeRelation = new TribeRelation(this);
		}

		public virtual void Tip(Mobile m, string tip)
		{
			SendGump(new TipGump(this, m, tip, true));

			SendMessage("Un maître de jeu vous a envoyé un message, double cliquez le b pour le lire.");
		}

		public override bool OnEquip(Item item)
		{
			if (this.AccessLevel > AccessLevel.Player)
			{
				return true;
			}




			if (item is BaseArmor)
			{
				int req = 10;

				BaseArmor armor = (BaseArmor)item;

				switch (armor.MaterialType)
				{
					case ArmorMaterialType.Cloth:
						req = 0;
						break;
					case ArmorMaterialType.Leather:
						req = 1;
						break;
					case ArmorMaterialType.Studded:
						req = 2;
						break;
					case ArmorMaterialType.Bone:
						req = 3;
						break;
					case ArmorMaterialType.Ringmail:
						req = 4;
						break;
					case ArmorMaterialType.Chainmail:
						req = 5;
						break;
					case ArmorMaterialType.Plate:
						req = 6;
						break;
					case ArmorMaterialType.Dragon:
						req = 6;
						break;
					case ArmorMaterialType.Wood:
						req = 4;
						break;
					case ArmorMaterialType.Stone:
						req = 4;
						break;
					default:
						req = 10;
						break;
				}

				if (Armure < req)
				{
					SendMessage("Armure requise : " + req);
					return false;
				}
			}

			return base.OnEquip(item);
		}

		#region equitation
		public virtual bool CheckEquitation(EquitationType type)
		{
			return CheckEquitation(type, Location);
		}

		public int TileToDontFall { get; set; }
		// 0   1    2    3    4    5    6    7    8    9    10   11   12
		private static int[] m_RunningTable = new int[] { 100, 100, 100, 050, 025, 000, 000, 000, 000, 000, 000, 000 };
		private static int[] m_BeingAttackedTable = new int[] { 100, 100, 100, 100, 100, 100, 100, 100, 075, 050, 005 };
		private static int[] m_MeleeAttackingTable = new int[] { 100, 100, 100, 100, 100, 075, 050, 025, 005, 000, 000 };
		private static int[] m_CastAttackingTable = new int[] { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
		private static int[] m_RangedAttackingTable = new int[] { 100, 100, 100, 100, 100, 075, 050, 025, 005, 000, 000 };
		private static int[] m_DismountTable = new int[] { 100, 100, 100, 090, 080, 075, 070, 065, 060, 055, 050 };

		public virtual bool CheckEquitation(EquitationType type, Point3D oldLocation)
		{
			//true s'il ne tombe pas, false s'il tombe

			if (!Mounted)
				return true;

			if (Mount is Server.Multis.RowBoat)
				return true;

			if (AccessLevel >= AccessLevel.GameMaster)
				return true;

			if (Map == null)
				return true;

			int chanceToFall = 0;
			int equitation = (int)(Skills[SkillName.Equitation].Value / 10);

			if (equitation < 0)
				equitation = 0;

			if (equitation > 10)
				equitation = 10;

			switch (type)
			{
				case EquitationType.Running: chanceToFall = m_RunningTable[equitation]; break;
				case EquitationType.BeingAttacked: chanceToFall = m_BeingAttackedTable[equitation]; break;
				case EquitationType.MeleeAttacking: chanceToFall = m_MeleeAttackingTable[equitation]; break;
				case EquitationType.CastAttacking: chanceToFall = m_CastAttackingTable[equitation]; break;
				case EquitationType.RangedAttacking: chanceToFall = m_RangedAttackingTable[equitation]; break;
				case EquitationType.Dismount: chanceToFall = m_DismountTable[equitation]; break;
			}

			if (chanceToFall < 0)
				chanceToFall = 0;

			if (chanceToFall <= Utility.RandomMinMax(0, 100))
				return true;

			if (type == EquitationType.Running)
			{
				//				if (TileToDontFall > 0)
				//					return true;

				TileType tile = Deplacement.GetTileType(this);

				if (tile == TileType.Other || tile == TileType.Dirt)
					return true;
			}

			BaseMount mount = (BaseMount)Mount;

			mount.Rider = null;
			mount.Location = oldLocation;

			TileToDontFall = 3;

			Timer.DelayCall(TimeSpan.FromSeconds(0.3), new TimerStateCallback(Equitation_Callback), mount);

			BeginAction(typeof(BaseMount));
			double seconds = 12.0 - (Skills[SkillName.Equitation].Value / 10);

			SetMountBlock(BlockMountType.DismountRecovery, TimeSpan.FromSeconds(seconds), false);


			return false;
		}

		private void Equitation_Callback(object state)
		{
			BaseMount mount = (BaseMount)state;

			mount.Animate(5, 5, 1, true, false, 0);
			Animate(22, 5, 1, true, false, 0);

			Damage(Utility.RandomMinMax(10, 20));
		}

		#endregion

		public HashSet<SkillName> SkillDisponible()
		{
			HashSet<SkillName> list = new HashSet<SkillName>();

			foreach (SkillName item in ClassePrimaire.Skill)
			{
				list.Add(item);
			}

			foreach (SkillName item in ClasseSecondaire.Skill)
			{
				list.Add(item);
			}
			foreach (SkillName item in Metier.Skill)
			{
				list.Add(item);
			}

			foreach (SkillName item in SkillGeneral)
			{
				list.Add(item);
			}
			return list;
		}

		public int GetAffinityValue(MagieType affinity)
		{
			return MagicAfinity.GetValue(affinity);
		}

		

		public int GetTribeValue(TribeType tribe)
		{
			return TribeRelation.GetValue(tribe);
		}

		public void ChangeTribeValue(TribeType tribe, int value)
		{

			TribeRelation.SetValue(tribe, TribeRelation.GetValue(tribe) + value);
		}

		#region Classe

		public bool ClassSkill(SkillName skills)
		{

			foreach (SkillName item in ClassePrimaire.Skill)
			{
				if (item == skills)
				{
					return true;
				}
			}

			foreach (SkillName item in ClasseSecondaire.Skill)
			{
				if (item == skills)
				{
					return true;
				}
			}


			foreach (SkillName item in Metier.Skill)
			{
				if (item == skills)
				{
					return true;
				}
			}

			foreach (SkillName item in SkillGeneral)
			{
				if (item == skills)
				{
					return true;
				}
			}

			return false;

		}

		public bool CheckClassePrimaire(Classe cl)
		{

			if (cl == Classe.GetClasse(-1))
			{
				return true;
			}
			else if (cl == ClasseSecondaire)
			{

				m_ClassePrimaire = cl;

				foreach (SkillName item in cl.Skill)
				{
					Skills[item].Base += 20;

					if (Skills[item].Base > 100)
					{
						m_feAttente += (int)Math.Round(Skills[item].Base) - 100;
						Skills[item].Base = 100;
					}
				}

				ClasseSecondaire = Classe.GetClasse(-1);
				return false;
			}
			else if (cl == Metier)
			{
				m_ClassePrimaire = cl;
				Metier = Classe.GetClasse(-1);
				return false;
			}

			return true;
		}

		public bool CheckClasseSecondaire(Classe cl)
		{
			if (cl == Classe.GetClasse(-1))
			{
				return true;
			}
			else if (cl == ClassePrimaire)
			{
				m_ClasseSecondaire = cl;

				foreach (SkillName item in cl.Skill)
				{
					Skills[item].Base -= 20;
				}

				ClassePrimaire = Classe.GetClasse(-1);
				return false;
			}
			else if (cl == Metier)
			{
				m_ClasseSecondaire = cl;

				Metier = Classe.GetClasse(-1);
				return false;
			}

			return true;
		}

		public bool CheckMetier(Classe cl)
		{
			if (cl == Classe.GetClasse(-1))
			{
				return true;
			}
			else if (cl == ClassePrimaire)
			{
				m_Metier = cl;
				ClassePrimaire = Classe.GetClasse(-1);
				return false;
			}
			else if (cl == ClasseSecondaire)
			{
				m_Metier = cl;

				foreach (SkillName item in cl.Skill)
				{
					Skills[item].Base += 20;

					if (Skills[item].Base > 100)
					{
						m_feAttente += (int)Math.Round(Skills[item].Base) - 100;
						Skills[item].Base = 100;
					}
				}

				ClasseSecondaire = Classe.GetClasse(-1);
				return false;
			}

			return true;
		}

		public void RecalculeClasse(Classe NewClass, int type)
		{




			switch (type)
			{
				case 1:
					{
						if (ClassePrimaire != null)
						{
							ChangeAffinity(NewClass, ClassePrimaire);

							foreach (SkillName item in ClassePrimaire.Skill)
							{
								if (Metier.ContainSkill(item))
								{
									// rien a faire, tout deux a 50...
								}
								else if (NewClass.ContainSkill(item))
								{
									// rien a faire, tout deux a 50...
								}
								else if (ClasseSecondaire.ContainSkill(item))
								{
									// Secondaire a 30, et primaire à 50, donc perte de 20 de skills.
									Skills[item].Base -= 20;
								}
								else
								{
									int Arecuperer = (int)(Skills[item].Base - 50);
									Skills[item].Base = 0;
									m_feAttente += Arecuperer;
								}
							}

							foreach (SkillName item in NewClass.Skill)
							{
								if (Metier.ContainSkill(item))
								{
									// rien a faire, tout deux a 50...
								}
								else if (ClassePrimaire.ContainSkill(item))
								{
									// rien a faire, tout deux a 50...
								}
								else if (ClasseSecondaire.ContainSkill(item))
								{
									// Secondaire a 30, et primaire à 50, donc perte de 20 de skills.
									Skills[item].Base += 20;

									if (Skills[item].Base > 100)
									{
										m_feAttente += (int)Math.Round(Skills[item].Base) - 100;
										Skills[item].Base = 100;
									}
								}
								else
								{
									Skills[item].Base = 50;
								}
							}
						}

						// classe primaire

						break;
					}
				case 2:
					{
						// Classe secondaire

						if (ClasseSecondaire != null)
						{

							ChangeAffinity(NewClass, ClasseSecondaire);

							foreach (SkillName item in ClasseSecondaire.Skill)
							{
								if (ClassePrimaire.ContainSkill(item))
								{
									// rien a faire, classe primaire est plus elevés.
								}
								else if (NewClass.ContainSkill(item))
								{
									// rien a faire, tout deux a 30...
								}
								else if (Metier.ContainSkill(item))
								{
									// rien a faire, metier à 50...
								}
								else
								{
									int Arecuperer = (int)(Skills[item].Value - 30);
									Skills[item].Base = 0;
									m_feAttente += Arecuperer;
								}
							}

							foreach (SkillName item in NewClass.Skill)
							{
								if (ClassePrimaire.ContainSkill(item))
								{
									// rien a faire, classe primaire est plus elevés.
								}
								else if (ClasseSecondaire.ContainSkill(item))
								{
									// rien a faire, tout deux a 30...
								}
								else if (Metier.ContainSkill(item))
								{
									// rien a faire, metier à 50...
								}
								else
								{
									Skills[item].Base = 30;
								}
							}
						}


						break;
					}
				case 3:
					{
						// Metier

						if (Metier != null)
						{
							foreach (SkillName item in Metier.Skill)
							{
								if (ClassePrimaire.ContainSkill(item))
								{
									// rien a faire, tout deux a 50...
								}
								else if (NewClass.ContainSkill(item))
								{
									// rien a faire, tout deux a 50...
								}
								else if (ClasseSecondaire.ContainSkill(item))
								{
									// Secondaire a 30, et primaire à 50, donc perte de 20 de skills.
									Skills[item].Base -= 20;
								}
								else
								{
									int Arecuperer = (int)(Skills[item].Base - 50);
									Skills[item].Base = 0;
									m_feAttente += Arecuperer;
								}
							}

							foreach (SkillName item in NewClass.Skill)
							{
								if (ClassePrimaire.ContainSkill(item))
								{
									// rien a faire, tout deux a 50...
								}
								else if (Metier.ContainSkill(item))
								{
									// rien a faire, tout deux a 50...
								}
								else if (ClasseSecondaire.ContainSkill(item))
								{

									Skills[item].Base += 20;


									if (Skills[item].Base > 100)
									{
										m_feAttente += (int)Math.Round(Skills[item].Base) - 100;
										Skills[item].Base = 100;
									}
								}
								else
								{
									Skills[item].Base = 50;
								}
							}
						}




						break;
					}

				default:
					break;
			}
		}


		public void ChangeAffinity(Classe NewClass, Classe OldClass)
		{
			int feAredonner = 0; 
				
				
				foreach (KeyValuePair<MagieType, int> item in OldClass.Magie)
				{
					if (MagicAfinity.ContainsKey(item.Key))
					{
						MagicAfinity[item.Key] -= item.Value;
					}
					else
					{
						MagicAfinity.Add(item.Key, -item.Value);
					}

					if (!NewClass.ContainAffinity(item.Key))
					{
		

						int affValue = GetAffinityValue(item.Key);

						if (affValue > 0)
						{
							feAredonner += affValue * 5;

							MagicAfinity[item.Key] = 0;						
						}
							
					}
				}

				foreach (KeyValuePair<MagieType, int> item in NewClass.Magie)
				{
					if (MagicAfinity.ContainsKey(item.Key))
					{
						MagicAfinity[item.Key] += item.Value;
					}
					else
					{
						MagicAfinity.Add(item.Key, item.Value);
					}
					
				}

				m_feAttente += feAredonner;



		}

		public void SetClasseSkills(double skills)
		{
			double principal = skills < 50 ? 50 : skills;
			double secondaire = skills < 30 ? 30 : skills;


			foreach (SkillName item in ClasseSecondaire.Skill)
			{
				Skills[item].Base = secondaire;
			}

			foreach (SkillName item in ClassePrimaire.Skill)
			{
				Skills[item].Base = principal;
			}

			foreach (SkillName item in Metier.Skill)
			{
				Skills[item].Base = principal;
			}


		}


		#endregion

		#region Stats

		public bool CanDecreaseStat(StatType stats)
		{
			if (StatAttente >= 10)
			{
				return false;
			}

			switch (stats)
			{
				case StatType.Str:
					if (RawStr == 25)
					{
						return false;
					}
					else
					{
						return true;
					}
				case StatType.Dex:
					if (RawDex == 25)
					{
						return false;
					}
					else
					{
						return true;
					}
				case StatType.Int:
					if (RawInt == 25)
					{
						return false;
					}
					else
					{
						return true;
					}
				default:
					return false;
			}
		}

		public bool CanIncreaseStat(StatType stats)
		{

			if (RawDex + RawStr + RawInt + StatAttente >= 225)
			{
				return false;
			}


			switch (stats)
			{
				case StatType.Str:
					{
						if (RawStr == 100)
						{
							return false;
						}
						else
						{
							return true;
						}
					}
				case StatType.Dex:
					{
						if (RawDex == 100)
						{
							return false;
						}
						else
						{
							return true;
						}
					}
				case StatType.Int:
					{
						if (RawInt == 100)
						{
							return false;
						}
						else
						{
							return true;
						}
					}
				case StatType.All:
					return false;
				default:
					return false;
			}
		}

		public void IncreaseStat(StatType stats)
		{
			if (CanIncreaseStat(stats))
			{
				switch (stats)
				{
					case StatType.Str:
						RawStr++;
						break;
					case StatType.Dex:
						RawDex++;
						break;
					case StatType.Int:
						RawInt++;
						break;
				}
			}
		}

		public void DecreaseStat(StatType stats)
		{
			if (CanDecreaseStat(stats))
			{
				switch (stats)
				{
					case StatType.Str:
						RawStr--;
						m_StatAttente++;
						break;
					case StatType.Dex:
						RawDex--;
						m_StatAttente++;
						break;
					case StatType.Int:
						RawInt--;
						m_StatAttente++;
						break;
				}
			}
		}


		#endregion

		#region Skills
		public bool CanIncreaseSkill(SkillName skills)
		{
			if (m_fe == 0)
			{
				return false;
			}
			else if (Skills[skills].Value == 100)
			{
				return false;
			}
			if (!ClassSkill(skills))
			{
				return false;
			}
			return true;
		}

		public void IncreaseSkills(SkillName skills)
		{

			if (CanIncreaseSkill(skills))
			{
				Skills[skills].Base += 1;
				m_fe--;
			}



		}

		public void DecreaseSkills(SkillName skills)
		{
			if (CanDecreaseSkill(skills))
			{
				Skills[skills].Base -= 1;
				m_feAttente++;
			}
		}

		public bool CanDecreaseSkill(SkillName skills)
		{

			if (Skills[skills].Base > 50) // sert à rien de calculer ca..
			{
				return true;
			}
			else if (Skills[skills].Base == 0) // sert à rien de calculer ca..
			{
				return false;
			}

			foreach (SkillName item in ClassePrimaire.Skill)
			{

				if (item == skills)
				{
					return false;
				}
			}

			foreach (SkillName item in Metier.Skill)
			{

				if (item == skills)
				{
					return false;
				}
			}

			foreach (SkillName item in ClasseSecondaire.Skill)
			{

				if (item == skills && Skills[skills].Base == 30)
				{
					return false;
				}
			}

			foreach (SkillName item in SkillGeneral)
			{
				if (item == skills && Skills[skills].Base == 30)
				{
					return false;
				}
			}
			return true;
		}

		#endregion


		#region Affinity 

		public bool CanIncreaseAffinity(MagieType mtype)
		{
			if (m_fe < 5)
			{
				return false;
			}
			else if (MagicAfinity[mtype] == 16)
			{
				return false;
			}
			if (!ClassAffinity(mtype))
			{
				return false;
			}
			return true;
		}
		public void IncreaseAffinity(MagieType mtype)
		{
			if (CanIncreaseAffinity(mtype))
			{
				MagicAfinity[mtype] += 1;
				m_fe -= 5;
			}
		}

		public bool CanDecreaseAffinity(MagieType mtype)
		{

			if (MagicAfinity[mtype] <= 8) 
			{
				return false;
			}

			return true;
		}

		public void DecreaseAffinity(MagieType mtype)
		{
			if (CanDecreaseAffinity(mtype))
			{
				MagicAfinity[mtype] -= 1;

				m_feAttente += 5;
			}
		}



		public bool ClassAffinity(MagieType mtype)
		{

			foreach (KeyValuePair<MagieType, int> item in ClassePrimaire.Magie)
			{
				if (item.Key == mtype)
				{
					return true;
				}
			}

			foreach (KeyValuePair<MagieType, int> item in ClasseSecondaire.Magie)
			{
				if (item.Key == mtype)
				{
					return true;
				}
			}


			return false;

		}




		#endregion

		#region Mort

		public override bool OnBeforeDeath()
		{
			if (Server.Commands.ControlCommand.UncontrolDeath((Mobile)this))
			{
				return base.OnBeforeDeath();
			}
			else
			{
				return false;
			}
			if (m_PossessStorage != null)
			{
				Server.Possess.CopySkills(this, m_Possess);
				Server.Possess.CopyProps(this, m_Possess);
				Server.Possess.MoveItems(this, m_Possess);

				m_Possess.Location = Location;
				m_Possess.Direction = Direction;
				m_Possess.Map = Map;
				m_Possess.Frozen = false;

				Server.Possess.CopySkills(m_PossessStorage, this);
				Server.Possess.CopyProps(m_PossessStorage, this);
				Server.Possess.MoveItems(m_PossessStorage, this);

				m_PossessStorage.Delete();
				m_PossessStorage = null;
				m_Possess.Kill();
				m_Possess = null;
				Hidden = true;
				return false;
			}

		}

		public override void OnDeath(Container c)
		{
			base.OnDeath(c);

			LastDeathTime = DateTime.Now;

			bool Assomage = false;

			if (!Vulnerability && !DeathShot)
			{
				Frozen = true;
				Assomage = true;
				Timer.DelayCall(TimeSpan.FromMinutes(DeathDuration), new TimerStateCallback(RessuciterOverTime_Callback), this);
			}
			else
			{
				/*	if (m_StatutSocial < StatutSocialEnum.Possession)
					{
						MoveToWorld(new Point3D(5340,2861,0), Map.Felucca);
						return;
					}*/




				if (Deguise)
				{
					Server.Deguisement.RemoveDeguisement(this);
				}

				if (Masque)
				{
					Masque = false;
				}

			}

			Vulnerability = true;
			EndOfVulnerabilityTime = DateTime.Now + TimeSpan.FromMinutes(DeathDuration + VulnerabilityDuration * (Assomage ? 1 : 5));

			Timer.DelayCall(TimeSpan.FromMinutes(DeathDuration + VulnerabilityDuration * (Assomage ? 1 : 5)), new TimerStateCallback(RemoveVulnerability_Callback), this);

			PreventPvpAttack = true;
			PreventPvpAttackTime = DateTime.Now + TimeSpan.FromMinutes(DeathDuration + PreventPvpAttackDuration);
			Timer.DelayCall(TimeSpan.FromMinutes(DeathDuration + PreventPvpAttackDuration), new TimerStateCallback(RetourCombatPvP_Callback), this);

			Server.Spells.Fifth.IncognitoSpell.StopTimer(this);
		}


		public override bool CanHeal()
		{
			if (Vulnerability || Server.Items.MortalStrike.IsWounded(this))
			{
				return false;
			}
			return base.CanHeal();
		}

		public void TrapDamage(int damage, int phys, int fire, int cold, int pois, int nrgy)
		{
			DeathShot = true;

			AOS.Damage(this, damage, phys, fire, cold, pois, nrgy);

			DeathShot = false;

		}


		public override void OnAfterResurrect()
		{
			base.OnAfterResurrect();

			Frozen = false;

			if (Corpse != null)
			{
				ArrayList list = new ArrayList();

				foreach (Item item in Corpse.Items)
				{
					list.Add(item);
				}

				foreach (Item item in list)
				{
					if (item.Layer == Layer.Hair || item.Layer == Layer.FacialHair)
						item.Delete();

					if (item is BaseRaceGumps || (Corpse is Corpse && ((Corpse)Corpse).EquipItems.Contains(item)))
					{
						if (!EquipItem(item))
							AddToBackpack(item);
					}
					else
					{
						AddToBackpack(item);
					}
				}

				Corpse.Delete();
			}


			SendMessage(HueManager.GetHue(HueManagerList.Red), "Vous vous relevez péniblement.", VulnerabilityDuration);
			SendMessage(HueManager.GetHue(HueManagerList.Red), "Vous êtes vulnérable pendant les {0} prochaines minutes.", VulnerabilityDuration);
			SendMessage(HueManager.GetHue(HueManagerList.Red), "Si vous tombez au combat, vous serez envoyé{0} dans le monde des esprits.", Female ? "e" : "");

		}

		private static void RetourCombatPvP_Callback(object state)
		{
			if ((Mobile)state is CustomPlayerMobile)
			{
				var pm = (CustomPlayerMobile)state;

				if (pm.PreventPvpAttack && pm.PreventPvpAttackTime <= DateTime.Now)
				{
					pm.PreventPvpAttack = false;
					pm.SendMessage(HueManager.GetHue(HueManagerList.Green), "Vous pouvez maintenant attaquer d'autres joueurs.");
				}
			}
		}

		private static void RessuciterOverTime_Callback(object state)
		{
			if ((Mobile)state is CustomPlayerMobile)
			{
				var pm = (CustomPlayerMobile)state;

				if (!pm.Alive)
				{

					pm.Resurrect();

				}
			}
		}

		private static void RemoveVulnerability_Callback(object state)
		{
			if ((Mobile)state is CustomPlayerMobile)
			{
				var pm = (CustomPlayerMobile)state;

				if (pm.Vulnerability && pm.EndOfVulnerabilityTime <= DateTime.Now)
				{
					pm.Vulnerability = false;
					pm.SendMessage(HueManager.GetHue(HueManagerList.Green), "Vous n'êtes plus vulnérable. La prochaine fois que vous tomberez au combat, vous serez assomé.");
				}
			}
		}

		public override bool CanBeHarmful(IDamageable damageable, bool message, bool ignoreOurBlessedness, bool ignorePeaceCheck)
		{
			if (PreventPvpAttack && damageable is CustomPlayerMobile)
			{
				SendMessage("Vous ne pouvez pas attaquer un joueur pendant encore {0} minute{1}.", (PreventPvpAttackTime - DateTime.Now).Minutes, (PreventPvpAttackTime - DateTime.Now).Minutes > 1 ? "s" : "");
				return false;
			}

			return base.CanBeHarmful(damageable, message, ignoreOurBlessedness, ignorePeaceCheck);
		}
		#endregion

		public override void OnDelete()
		{
			Reroll(); // Ok, c'est un peu bizard de faire quand on delete le perso, que sa reroll automatique, mais ca facilite la pierre de reroll (fait juste deleter le personnage) et ca diminue aussi l'impacte d'un Rage Quit, puisque si le joueur a deleter son perso, il va automatiquement recevoir l'experience et va pouvoir revenir en rerollant.

			if (Maitre != null)
			{
				Maitre.RemoveEsclave(this);
			}








			base.OnDelete();
		}

		public void Reroll()
		{
			Server.Accounting.Account accFrom = (Server.Accounting.Account)this.Account;

			if (accFrom != null)
			{
				if (accFrom.AccessLevel == AccessLevel.Player)
				{
					accFrom.AddReroll(new Reroll(this));
				}


			}

		}

		public override bool CheckPackage()
		{
			Item package = (Item)Backpack.FindItemByType(typeof(Server.Custom.Packaging.Packages.CustomPackaging));

			if (package != null)
			{
				return true;
			}

			return false;
		}

		public bool CheckRoux()
		{
			if ((HairHue >= 1602 && HairHue < 1655) || (HairHue >= 1502 && HairHue < 1534) || (HairHue >= 1202 && HairHue < 1226))
			{
				return true;
			}


			return false;
		}


		public void JailP(Mobile Jailor, TimeSpan Time)
		{

			if (!m_Jail)
			{
				JailLocation = Location;
				JailMap = Map;
				m_Jail = true;
			}

			JailTime = DateTime.Now.Add(Time);		

			if (Jailor != null)
			{
				Say($"Mis en tole par {Jailor.Name}. Ne passez pas go et ne reclamez pas 200$.");

				JailBy = Jailor;
			}

			switch (Utility.Random(9))
			{
				case 0:
					Location = new Point3D(5276, 1164, 0);
					Map = Map.Trammel;
					break;
				case 1:
					Location = new Point3D(5286, 1164, 0);
					Map = Map.Trammel;
					break;
				case 2:
					Location = new Point3D(5296, 1164, 0);
					Map = Map.Trammel;
					break;
				case 3:
					Location = new Point3D(5306, 1164, 0);
					Map = Map.Trammel;
					break;
				case 4:
					Location = new Point3D(5276, 1174, 0);
					Map = Map.Trammel;
					break;
				case 5:
					Location = new Point3D(5276, 1174, 0);
					Map = Map.Trammel;
					break;
				case 6:
					Location = new Point3D(5286, 1174, 0);
					Map = Map.Trammel;
					break;
				case 7:
					Location = new Point3D(5306, 1174, 0);
					Map = Map.Trammel;
					break;
				case 8:
					Location = new Point3D(5283, 1184, 0);
					Map = Map.Trammel;
					break;
				case 9:
					Location = new Point3D(5304, 1184, 0);
					Map = Map.Trammel;
					break;
				default:
					break;
			}

			if (Time.TotalMinutes <= 60)
			{
				Timer.DelayCall(Time.Add(TimeSpan.FromSeconds(15)), new TimerStateCallback(ReleaseJail_Callback), this);
			}
		}
		private static void ReleaseJail_Callback(object state)
		{
			if ((Mobile)state is CustomPlayerMobile cp)
			{				
				if (cp.Jail)
				{

				}
			}
		}

		public void JailRelease()
		{
			if (m_Jail)
			{
				Say($"Vous venez d'être libéré.");

				Location = JailLocation;
				Map = JailMap;
				JailTime = DateTime.MinValue;
				m_Jail = false;
				JailBy = null;
			}
			
		}



		public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();


			switch (version)
			{

				case 31:
					{
						Journaliste = reader.ReadBool();
						goto case 0;
					}

				case 30:
				{
					LastNormalFE = reader.ReadDateTime();
					FEDay = reader.ReadInt();
					goto case 29;
				}
				case 29:
					{
						JailLocation = reader.ReadPoint3D();
						JailMap =  reader.ReadMap();
						JailTime = reader.ReadDateTime();
						m_Jail =  reader.ReadBool();
						JailBy =  reader.ReadMobile();


						goto case 28;
					}
				case 28:
				case 27:
				case 26:
					{
						m_LastFERP = reader.ReadDateTime();

						goto case 24;
					}
				case 25:
				case 24:
					{
						m_Maitre = (CustomPlayerMobile)reader.ReadMobile();
						goto case 23;
					}

				case 23:
					{
						int count = reader.ReadInt();

						for (int i = 0; i < count; i++)
						{
							m_Esclaves.Add(reader.ReadMobile());
						}

						goto case 22;
					}
				case 22:
					{
						m_TotalRPFE = reader.ReadInt();

						if (version >= 27)
						{
							goto case 20;
						}
						else
						{
							goto case 21;
						}					
					}
				case 21:
					{
						reader.ReadTimeSpan();

						goto case 20;
					}
				case 20:
					{
						m_Salaire = reader.ReadInt();
						m_LastPay = reader.ReadDateTime();


						goto case 19;
					}
				case 19:
					{
						m_IdentiteId = reader.ReadInt();

						m_Deguisement = new Dictionary<int, Deguisement>();

						int count = reader.ReadInt();

						for (int i = 0; i < count; i++)
						{
							m_Deguisement.Add(reader.ReadInt(), Server.Deguisement.Deserialize(reader));
						}

						goto case 18;
					}
				case 18:
					{
						TribeRelation = new TribeRelation(this, reader);

						goto case 17;
					}
				case 17:
					{
						NameMod = reader.ReadString();
						goto case 16;
					}
				case 16:
					{
						m_Masque = reader.ReadBool();
						goto case 15;
					}
				case 15:
					{
						StatutSocial = (StatutSocialEnum)reader.ReadInt();
						goto case 14;
					}
				case 14:
					{
						MissiveEnAttente = new List<MissiveContent>();

						int count = reader.ReadInt();

						for (int i = 0; i < count; i++)
						{
							MissiveEnAttente.Add(MissiveContent.Deserialize(reader));
						}
						goto case 13;
					}
				case 13:
					{
						QuiOptions = (QuiOptions)reader.ReadInt();

						goto case 12;
					}
				case 12:
					{
						TitleCycle = reader.ReadInt();
						customTitle = reader.ReadString();

						goto case 11;
					}
				case 11:
					{
						m_BaseHue = reader.ReadInt();
						goto case 10;

					}
				case 10:
					{
						m_BaseRace = Server.BaseRace.GetRace(reader.ReadInt());
						m_BaseFemale = reader.ReadBool();

						if (version <= 18)
						{
							goto case 9;
						}
						else
						{
							goto case 8;
						}	
					}
				case 9:
					{	
						Deguisement.Add(0,Server.Deguisement.Deserialize(reader));
						goto case 8;
					}
				case 8:
					{
						ChosenSpellbook = reader.ReadItem();
						goto case 7;
					}

				case 7:
					{
						QuickSpells = new List<int>();
						int count = reader.ReadInt();
						for (int i = 0; i < count; i++)
							QuickSpells.Add(reader.ReadInt());
						goto case 6;
					}
				case 6:
					{
						God = God.GetGod(reader.ReadInt());

						MagicAfinity = new AffinityDictionary(this, reader);					
						goto case 5;
					}
				case 5:
					{
						m_feAttente = reader.ReadInt();

						goto case 4;

					}
				case 4:
					{
						m_feAttente = reader.ReadInt();

						goto case 3;
					}
				case 3:
					{
						m_fe = reader.ReadInt();
						m_lastLoginTime = reader.ReadDateTime();
						m_nextFETime = reader.ReadTimeSpan();
						m_TotalNormalFE = reader.ReadInt();


						goto case 2;
					}
				case 2:
					{
						m_ClassePrimaire = Classe.GetClasse(reader.ReadInt());
						m_ClasseSecondaire = Classe.GetClasse(reader.ReadInt());
						m_Metier = Classe.GetClasse(reader.ReadInt());
						goto case 1;
					}
				case 1:
					{
						m_Grandeur = (GrandeurEnum)reader.ReadInt();
						m_Grosseur = (GrosseurEnum)reader.ReadInt();
						m_Beaute = (AppearanceEnum)reader.ReadInt();
						goto case 0;
					}
				case 0:
                    {

                        break;
                    }
            }

		}

		public override void Serialize(GenericWriter writer)
        {        
            base.Serialize(writer);

            writer.Write(31); // version

			
			writer.Write(Journaliste);

			writer.Write(LastNormalFE);
			writer.Write(FEDay);

			writer.Write(JailLocation);
			writer.Write(JailMap);
			writer.Write(JailTime);
			writer.Write(m_Jail);
			writer.Write(JailBy);
		

			writer.Write(LastFERP);

			writer.Write(m_Maitre);

			writer.Write(m_Esclaves.Count);


			foreach (Mobile item in m_Esclaves)
			{
				writer.Write(item);			
			}


			writer.Write(m_TotalRPFE); 

	//		m_TotalGameTime += DateTime.Now - m_LastCountGameTime;
	//		m_LastCountGameTime = DateTime.Now;


	//		writer.Write(m_TotalGameTime);

			writer.Write(m_Salaire);
			writer.Write(m_LastPay);


			writer.Write(m_IdentiteId);

			writer.Write(m_Deguisement.Count);

			foreach (KeyValuePair<int,Deguisement> item in m_Deguisement)
			{
				writer.Write(item.Key);
				item.Value.Serialize(writer);
			}


			m_TribeRelation.Serialize(writer);

			writer.Write(NameMod);
			writer.Write(m_Masque);

			writer.Write((int)m_StatutSocial);

			writer.Write(MissiveEnAttente.Count);

			foreach (MissiveContent item in MissiveEnAttente)
			{
				item.Serialize(writer);
			}

			writer.Write((int)QuiOptions);

			writer.Write(TitleCycle);
			writer.Write(customTitle);

			if (m_BaseHue == null)
			{
				m_BaseHue = Hue;
			}

			writer.Write(Hue);


			if (m_BaseRace == null)
			{
				m_BaseRace = Race;
			}

			if (m_BaseFemale == null)
			{
				m_BaseFemale = Female;
			}

			writer.Write(m_BaseRace.RaceID);
			writer.Write(m_BaseFemale);

/*

			if (m_Deguisement == null)
			{
				m_Deguisement = new Deguisement(this);
			}

			
			m_Deguisement.Serialize(writer);
			
			*/

			

			writer.Write(ChosenSpellbook);

			writer.Write(QuickSpells.Count);
			for (int i = 0; i < QuickSpells.Count; i++)
				writer.Write((int)QuickSpells[i]);

			writer.Write(God.GodID);

			m_MagicAfinity.Serialize(writer);

			writer.Write(m_StatAttente);
			writer.Write(m_feAttente);
			writer.Write(m_fe);
			writer.Write(m_lastLoginTime);
			writer.Write(m_nextFETime);
			writer.Write(m_TotalNormalFE);



			writer.Write((int)m_ClassePrimaire.ClasseID);
			writer.Write((int)m_ClasseSecondaire.ClasseID);
			writer.Write((int)m_Metier.ClasseID);



			writer.Write((int)m_Grandeur);
			writer.Write((int)m_Grosseur);
			writer.Write((int)m_Beaute);


		}
	}
}
