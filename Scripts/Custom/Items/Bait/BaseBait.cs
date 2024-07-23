using System;
using Server;
using Server.Items;
using Server.ContextMenus;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
	public abstract class BaseBait : Item
	{
		private Bait m_Bait;
		private int m_Charge;

		[CommandProperty(AccessLevel.GameMaster)]
		public Bait Bait
		{
			get { return m_Bait; }
			set { m_Bait = value; SetNewName(); InvalidateProperties(); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int Charge
		{
			get { return m_Charge; }
			set
			{
				m_Charge = value;

				if (m_Charge <= 0)
					Delete();
			}
		}

		[Constructable]
		public BaseBait(Bait bait, int charge) : base(0x4B46)
		{
			Name = "Appât";
			Weight = 0.1;

			m_Bait = bait;
			m_Charge = charge;
			Stackable = true;
		}

		public static string[] m_Material = new string[]
		{
			"Aucun",
			"Fish",
			"AutumnDragonfish",
			"BlueLobster",
			"BullFish",
			"CrystalFish",
			"FairySalmon",
			"FireFish",
			"GiantKoi",
			"GreatBarracuda",
			"HolyMackerel",
			"LavaFish",
			"ReaperFish",
			"SpiderCrab",
			"StoneCrab",
			"SummerDragonfish",
			"UnicornFish",
			"YellowtailBarracuda",
			"AbyssalDragonfish",
			"BlackMarlin",
			"BloodLobster",
			"BlueMarlin",
			"DreadLobster",
			"DungeonPike",
			"GiantSamuraiFish",
			"GoldenTuna",
			"Kingfish",
			"LanternFish",
			"RainbowFish",
			"SeekerFish",
			"SpringDragonfish",
			"StoneFish",
			"TunnelCrab",
			"VoidCrab",
			"VoidLobster",
			"WinterDragonfish",
			"ZombieFish"
		};
		public bool ApplyTo(LargeFishingPole pole)
		{
			if (pole == null)
				return false;

			// Si l'appât actuel est épuisé ou différent, on le remplace
			if (pole.Charge == 0 || pole.Bait != this.Bait)
			{
				pole.Bait = this.Bait;
				pole.Charge = this.Charge;
				this.Consume(); // On supprime l'objet appât une fois appliqué
				pole.InvalidateProperties();
				return true;
			}
			// Si c'est le même type d'appât, on ajoute les charges sans limite
			else if (pole.Bait == this.Bait)
			{
				pole.Charge += this.Charge;
				this.Consume();
				pole.InvalidateProperties();
				return true;
			}

			return false;
		}

		public virtual string GetMaterial()
		{
			string value = "Aucun";

			try
			{
				value = m_Material[(int)m_Bait];
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return value;
		}

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			if (from.Alive && IsChildOf(from.Backpack))
			{
				list.Add(new ApplyBaitEntry(from, this));
			}

			base.GetContextMenuEntries(from, list);
		}

		public void SetNewName()
		{
			Name = "Appât : " + GetMaterial();
		}

		public static BaseBait CreateBait(Bait bait, int charge)
		{
			switch (bait)
			{
				case Bait.Fish: return new BaitFish(charge);
				case Bait.AutumnDragonfish: return new BaitAutumnDragonfish(charge);
				case Bait.BlueLobster: return new BaitBlueLobster(charge);
				case Bait.BullFish: return new BaitBullFish(charge);
				case Bait.CrystalFish: return new BaitCrystalFish(charge);
				case Bait.FairySalmon: return new BaitFairySalmon(charge);
				case Bait.FireFish: return new BaitFireFish(charge);
				case Bait.GiantKoi: return new BaitGiantKoi(charge);
				case Bait.GreatBarracuda: return new BaitGreatBarracuda(charge);
				case Bait.HolyMackerel: return new BaitHolyMackerel(charge);
				case Bait.LavaFish: return new BaitLavaFish(charge);
				case Bait.ReaperFish: return new BaitReaperFish(charge);
				case Bait.SpiderCrab: return new BaitSpiderCrab(charge);
				case Bait.StoneCrab: return new BaitStoneCrab(charge);
				case Bait.SummerDragonfish: return new BaitSummerDragonfish(charge);
				case Bait.UnicornFish: return new BaitUnicornFish(charge);
				case Bait.YellowtailBarracuda: return new BaitYellowtailBarracuda(charge);
				case Bait.AbyssalDragonfish: return new BaitAbyssalDragonfish(charge);
				case Bait.BlackMarlin: return new BaitBlackMarlin(charge);
				case Bait.BloodLobster: return new BaitBloodLobster(charge);
				case Bait.BlueMarlin: return new BaitBlueMarlin(charge);
				case Bait.DreadLobster: return new BaitDreadLobster(charge);
				case Bait.DungeonPike: return new BaitDungeonPike(charge);
				case Bait.GiantSamuraiFish: return new BaitGiantSamuraiFish(charge);
				case Bait.GoldenTuna: return new BaitGoldenTuna(charge);
				case Bait.Kingfish: return new BaitKingfish(charge);
				case Bait.LanternFish: return new BaitLanternFish(charge);
				case Bait.RainbowFish: return new BaitRainbowFish(charge);
				case Bait.SeekerFish: return new BaitSeekerFish(charge);
				case Bait.SpringDragonfish: return new BaitSpringDragonfish(charge);
				case Bait.StoneFish: return new BaitStoneFish(charge);
				case Bait.TunnelCrab: return new BaitTunnelCrab(charge);
				case Bait.VoidCrab: return new BaitVoidCrab(charge);
				case Bait.VoidLobster: return new BaitVoidLobster(charge);
				case Bait.WinterDragonfish: return new BaitWinterDragonfish(charge);
				case Bait.ZombieFish: return new BaitZombieFish(charge);
				default: return null;
			}
		}

		public override void AddNameProperty(ObjectPropertyList list)
		{
			if (Amount > 1)
				list.Add(1050039, "{0}\t{1}", Amount.ToString(), String.Format("Appâts [{0}]", GetMaterial())); // ~1_NUMBER~ ~2_ITEMNAME~
			else
				list.Add(String.Format("Appât [{0}]", GetMaterial()));
		}

		public BaseBait(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)1); // version

			writer.Write((int)m_Bait);
			writer.Write(m_Charge);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 1:
					{
						m_Bait = (Bait)reader.ReadInt();
						m_Charge = reader.ReadInt();
						break;
					}
				case 0:
					{
						break;
					}
			}
		}
	}
}

namespace Server.ContextMenus
{
	public class ApplyBaitEntry : ContextMenuEntry
	{
		private Mobile m_From;
		private BaseBait m_Bait;

		public ApplyBaitEntry(Mobile from, BaseBait bait) : base(90, 1)
		{
			m_From = from;
			m_Bait = bait;
		}

		public override void OnClick()
		{
			if (m_Bait.Deleted || !m_Bait.Movable || !m_From.CheckAlive())
				return;

			m_From.SendMessage("Sur quelle canne à pêche souhaitez-vous appliquer cet appât?");
			m_From.BeginTarget(1, false, Server.Targeting.TargetFlags.None, new TargetStateCallback(Bait_OnApply), m_Bait);
		}

		private void Bait_OnApply(Mobile from, object targeted, object state)
		{
			if (targeted is LargeFishingPole pole)
			{
				BaseBait bait = state as BaseBait;

				if (bait.ApplyTo(pole))
				{
					from.SendMessage("Vous attachez l'appât à la canne à pêche.");
				}
				else
				{
					from.SendMessage("Vous ne pouvez pas appliquer cet appât à cette canne à pêche.");
				}
			}
			else
			{
				from.SendMessage("Vous devez choisir une canne à pêche.");
			}
		}
	}
}
