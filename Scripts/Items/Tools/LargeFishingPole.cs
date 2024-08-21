using Server.ContextMenus;
using Server.Engines.Harvest;
using System.Collections.Generic;
using System;
using Server.Mobiles;
using Server.Items;
using System.Reflection;
using Server.Scripts.Commands;
using Server.Targeting;

namespace Server.Items
{
	public enum Bait
	{
		Aucun,
		Fish,
		AutumnDragonfish,
		BlueLobster,
		BullFish,
		CrystalFish,
		FairySalmon,
		FireFish,
		GiantKoi,
		GreatBarracuda,
		HolyMackerel,
		LavaFish,
		ReaperFish,
		SpiderCrab,
		StoneCrab,
		SummerDragonfish,
		UnicornFish,
		YellowtailBarracuda,
		AbyssalDragonfish,
		BlackMarlin,
		BloodLobster,
		BlueMarlin,
		DreadLobster,
		DungeonPike,
		GiantSamuraiFish,
		GoldenTuna,
		Kingfish,
		LanternFish,
		RainbowFish,
		SeekerFish,
		SpringDragonfish,
		StoneFish,
		TunnelCrab,
		VoidCrab,
		VoidLobster,
		WinterDragonfish,
		ZombieFish
	}

	public class LargeFishingPole : Item
	{
		private Bait m_Bait;
		private int m_Charge;

		private static readonly Dictionary<Bait, Type[]> baitFishMap = new Dictionary<Bait, Type[]>
		{
			{ Bait.Fish, new Type[] { typeof(Fish) } },
			{ Bait.AutumnDragonfish, new Type[] { typeof(AutumnDragonfish) } },
			{ Bait.BlueLobster, new Type[] { typeof(BlueLobster) } },
			{ Bait.BullFish, new Type[] { typeof(BullFish) } },
			{ Bait.CrystalFish, new Type[] { typeof(CrystalFish) } },
			{ Bait.FairySalmon, new Type[] { typeof(FairySalmon) } },
			{ Bait.FireFish, new Type[] { typeof(FireFish) } },
			{ Bait.GiantKoi, new Type[] { typeof(GiantKoi) } },
			{ Bait.GreatBarracuda, new Type[] { typeof(GreatBarracuda) } },
			{ Bait.HolyMackerel, new Type[] { typeof(HolyMackerel) } },
			{ Bait.LavaFish, new Type[] { typeof(LavaFish) } },
			{ Bait.ReaperFish, new Type[] { typeof(ReaperFish) } },
			{ Bait.SpiderCrab, new Type[] { typeof(SpiderCrab) } },
			{ Bait.StoneCrab, new Type[] { typeof(StoneCrab) } },
			{ Bait.SummerDragonfish, new Type[] { typeof(SummerDragonfish) } },
			{ Bait.UnicornFish, new Type[] { typeof(UnicornFish) } },
			{ Bait.YellowtailBarracuda, new Type[] { typeof(YellowtailBarracuda) } },
			{ Bait.AbyssalDragonfish, new Type[] { typeof(AbyssalDragonfish) } },
			{ Bait.BlackMarlin, new Type[] { typeof(BlackMarlin) } },
			{ Bait.BloodLobster, new Type[] { typeof(BloodLobster) } },
			{ Bait.BlueMarlin, new Type[] { typeof(BlueMarlin) } },
			{ Bait.DreadLobster, new Type[] { typeof(DreadLobster) } },
			{ Bait.DungeonPike, new Type[] { typeof(DungeonPike) } },
			{ Bait.GiantSamuraiFish, new Type[] { typeof(GiantSamuraiFish) } },
			{ Bait.GoldenTuna, new Type[] { typeof(GoldenTuna) } },
			{ Bait.Kingfish, new Type[] { typeof(Kingfish) } },
			{ Bait.LanternFish, new Type[] { typeof(LanternFish) } },
			{ Bait.RainbowFish, new Type[] { typeof(RainbowFish) } },
			{ Bait.SeekerFish, new Type[] { typeof(SeekerFish) } },
			{ Bait.SpringDragonfish, new Type[] { typeof(SpringDragonfish) } },
			{ Bait.StoneFish, new Type[] { typeof(StoneFish) } },
			{ Bait.TunnelCrab, new Type[] { typeof(TunnelCrab) } },
			{ Bait.VoidCrab, new Type[] { typeof(VoidCrab) } },
			{ Bait.VoidLobster, new Type[] { typeof(VoidLobster) } },
			{ Bait.WinterDragonfish, new Type[] { typeof(WinterDragonfish) } },
			{ Bait.ZombieFish, new Type[] { typeof(ZombieFish) } }
            // Ajoutez d'autres mappages pour d'autres types d'appâts si nécessaire
        };

		public Bait CurrentBait { get { return m_Bait; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public Bait Bait
		{
			get { return m_Bait; }
			set
			{
				m_Bait = value;
				InvalidateProperties();
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int Charge
		{
			get { return m_Charge; }
			set
			{
				m_Charge = value;
				if (m_Charge <= 0)
				{
					m_Charge = 0;
					m_Bait = Bait.Aucun;
				}
				InvalidateProperties();
			}
		}

		[Constructable]
		public LargeFishingPole() : base(0x0DC0)
		{
			Name = "canne à pêche";
			Layer = Layer.OneHanded;
		}

		public override void OnAosSingleClick(Mobile from)
		{
			base.OnAosSingleClick(from);

			if (m_Bait != Bait.Aucun && m_Charge > 0)
			{
				from.SendMessage($"[{m_Bait} / {m_Charge} charge{(m_Charge > 1 ? "s" : "")}]");
			}
		}

		private Timer m_UpdateTimer;

		public override void OnDoubleClick(Mobile from)
		{
			base.OnDoubleClick(from);

			if (m_UpdateTimer == null)
			{
				m_UpdateTimer = Timer.DelayCall(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5), new TimerCallback(UpdateProperties));
			}
			if (Bait == Bait.Aucun || Charge <= 0)
			{
				from.Target = new BaitTarget(this);
				from.SendMessage("Sélectionnez l'appât que vous voulez utiliser.");
				return;
			}

			CustomFishing.System.BeginHarvesting(from, this);
		}
		private class BaitTarget : Target
		{
			private LargeFishingPole m_Pole;

			public BaitTarget(LargeFishingPole pole) : base(1, false, TargetFlags.None)
			{
				m_Pole = pole;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is BaseBait bait)
				{
					if (bait.ApplyTo(m_Pole))
					{
						from.SendMessage($"Vous avez appliqué {bait.Name} à votre canne à pêche.");
					}
					else
					{
						from.SendMessage("Vous ne pouvez pas appliquer cet appât.");
					}
				}
				else
				{
					from.SendMessage("Ce n'est pas un appât valide.");
				}
			}
		}

		private void UpdateProperties()
		{
			InvalidateProperties();
		}

		public override void OnDelete()
		{
			if (m_UpdateTimer != null)
			{
				m_UpdateTimer.Stop();
				m_UpdateTimer = null;
			}

			base.OnDelete();
		}

		public void UseBait()
		{
			if (m_Charge > 0)
			{
				m_Charge--;
				if (m_Charge == 0)
				{
					m_Bait = Bait.Aucun;
				}
				InvalidateProperties();
			}
		}

		public void FinishFishing(Mobile from)
		{
			Type fishType = null;
			double chance = Utility.RandomDouble();

			from.CheckSkill(SkillName.Fishing, 0, 100);

			double fishingSkill = from.Skills[SkillName.Fishing].Value;

			double chanceSpecificFish = (fishingSkill / 10.0) + 5.0;
			double chanceNormalFish = (fishingSkill / 5.0) + 20.0;

			// Logique pour pêcher un poisson
			if (m_Bait != Bait.Aucun && m_Charge > 0)
			{
				if (chance * 100 < chanceSpecificFish)
				{
					if (baitFishMap.TryGetValue(m_Bait, out Type[] fishTypes))
					{
						fishType = fishTypes[0];
					}
				}
				else if (chance * 100 < (chanceSpecificFish + chanceNormalFish))
				{
					fishType = typeof(Fish);
				}
				UseBait();
			}
			else
			{
				if (chance * 100 < chanceNormalFish)
				{
					fishType = typeof(Fish);
				}
			}

			// Pêcher le poisson
			if (fishType != null)
			{
				Item harvested = Activator.CreateInstance(fishType) as Item;
				if (harvested != null)
				{
					AddFishToInventory(from, harvested, fishType);
				}
			}
			else
			{
				from.SendMessage("Vous n'avez rien attrapé cette fois-ci.");
			}

			// Chance de pêcher des objets bonus
			TryFishBonusItem(from, fishingSkill);

			// Notifier le joueur du statut de l'appât et de la charge
			NotifyBaitStatus(from);
		}

		private void AddFishToInventory(Mobile from, Item harvested, Type fishType)
		{
			int weight = 1;

			if (harvested is RareFish rarefish && FishInfo.IsRareFish(rarefish.GetType()))
			{
				rarefish.Fisher = from;
				rarefish.DateCaught = DateTime.Now;
				rarefish.Stackable = false;
				rarefish.Weight = Math.Max(1, 200 - (int)Math.Sqrt(Utility.RandomMinMax(0, 40000)));
				weight = (int)rarefish.Weight;
			}

			if (from.Backpack.TryDropItem(from, harvested, false))
			{
				if (harvested is RareFish)
				{
					from.SendMessage($"Vous attrapez un {fishType.Name} pesant {weight} stones!");
				}
				else
				{
					from.SendMessage($"Vous attrapez un {fishType.Name}!");
				}
				from.CheckSkill(SkillName.Fishing, 0, 100);
				if (harvested is RareFish)
				{
					from.CheckSkill(SkillName.Fishing, 0, 100);
				}
			}
			else
			{
				harvested.MoveToWorld(from.Location, from.Map);
				from.SendMessage($"Votre sac est plein. Le {fishType.Name} tombe à vos pieds.");
			}
		}

		private void TryFishBonusItem(Mobile from, double fishingSkill)
		{
			// Vérifier si le joueur peut pêcher une carte au trésor
			if (fishingSkill >= 50 && from.Map == Map.Felucca && Utility.RandomDouble() < 0.005)
			{
				FishTreasureMap(from);
			}

			// Pêcher une skillcard
			if (fishingSkill >= 50 && from.Map == Map.Felucca && Utility.RandomDouble() < 0.05)
			{
				FishSkillCard(from);
			}

			// Pêcher un objet aléatoire avec 10% de chance
			if (Utility.RandomDouble() < 0.10)
			{
				FishRandomItem(from);
			}
		}

		private void FishTreasureMap(Mobile from)
		{
			int level = Utility.RandomMinMax(1, 3);
			TreasureMap treasureMap = new TreasureMap(level, from.Map);
			if (from.AddToBackpack(treasureMap))
			{
				from.SendMessage("Vous avez aussi pêché une carte au trésor !");
			}
			else
			{
				treasureMap.Delete();
				from.SendMessage("Vous avez aussi pêché une carte au trésor, mais votre sac est plein. La carte est perdue.");
			}
		}

		private void FishSkillCard(Mobile from)
		{
			SkillCard skillCard = new SkillCard();
			if (from.AddToBackpack(skillCard))
			{
				from.SendMessage("Vous avez aussi pêché une carte mystérieuse !");
			}
			else
			{
				skillCard.Delete();
				from.SendMessage("Vous avez aussi pêché une carte mystérieuse, mais votre sac est plein. La skillcard est perdue.");
			}
		}

		private void FishRandomItem(Mobile from)
		{
			var (itemType, itemName) = possibleItems[Utility.Random(possibleItems.Length)];
			Item item = null;

			try
			{
				// Essayez d'abord de créer l'objet avec un constructeur sans paramètres
				item = (Item)Activator.CreateInstance(itemType);
			}
			catch (MissingMethodException)
			{
				try
				{
					// Si cela échoue, essayez de créer l'objet avec un constructeur qui prend un seul paramètre int
					item = (Item)Activator.CreateInstance(itemType, new object[] { 1 });
				}
				catch
				{
					// Si cela échoue aussi, loggez l'erreur et passez à l'item suivant
					Console.WriteLine($"Impossible de créer un objet de type {itemType.Name}");
					return;
				}
			}

			if (item != null)
			{
				if (from.AddToBackpack(item))
				{
					from.SendMessage($"Vous avez aussi pêché un(e) {itemName} !");
				}
				else
				{
					item.Delete();
					from.SendMessage($"Vous avez aussi pêché un(e) {itemName}, mais votre sac est plein. L'objet est perdu.");
				}
			}
		}

		private void NotifyBaitStatus(Mobile from)
		{
			if (m_Bait != Bait.Aucun && m_Charge > 0)
			{
				from.SendMessage($"[{m_Bait} / {m_Charge} charge{(m_Charge > 1 ? "s" : "")}]");
			}
			else
			{
				from.SendMessage("[ aucun appât ]");
			}
		}



		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			if (from.Alive && m_Bait != Bait.Aucun && m_Charge > 0)
			{
				list.Add(new RemoveBaitEntry(from, this));
			}
			base.GetContextMenuEntries(from, list);

			BaseHarvestTool.AddContextMenuEntries(from, this, list, CustomFishing.System);
		}

		public override void AddCraftedProperties(ObjectPropertyList list)
		{
			base.AddCraftedProperties(list);

			if (m_Bait != Bait.Aucun && m_Charge > 0)
			{
				list.Add($"[{m_Bait} / {m_Charge} charge{(m_Charge > 1 ? "s" : "")}]");
			}
			else
			{
				list.Add("[ aucun appât ]");
			}
}
		private static readonly (Type, string)[] possibleItems = new[]
	{
	(typeof(CoquillageHautsFonds), "Coquillage des hauts fonds"),
	(typeof(CoquillageArcEnCiel), "Coquillage arc-en-ciel"),
	(typeof(CoquilleDoree), "Coquille dorée"),
	(typeof(CoquilleEcarlate), "Coquille écarlate"),
	(typeof(CoquillePlate), "Coquille plate"),
	(typeof(CoquilleTachetee), "Coquille tachetée"),
	(typeof(DentRequin), "Dent de requin"),
	(typeof(DentAlligator), "Dent d'alligator"),
	(typeof(GraisseSole), "Graisse de sole"),
	(typeof(OeilRaie), "Oeil de raie"),
	(typeof(OeufThon), "Oeuf de thon"),
	(typeof(SangAnguille), "Sang d'anguille"),
	(typeof(UnfinishedBarrel), "Baril inachevé"),
	(typeof(Stool), "Tabouret"),
	(typeof(ClockFrame), "Horloge cassée"),
	(typeof(Globe), "Globe terrestre"),
	(typeof(BarrelLid), "Tonnelier"),
	(typeof(PrizedFish), "Poisson primé"),
	(typeof(WondrousFish), "Poisson merveilleux"),
	(typeof(TrulyRareFish), "Poisson vraiment rare"),
	(typeof(PeculiarFish), "Poisson particulier"),
	(typeof(Boots), "Bottes"),
	(typeof(Shoes), "Chaussures"),
	(typeof(Sandals), "Sandales"),
	(typeof(ThighBoots), "Cuissardes"),
	(typeof(BlackPearl), "Perle noire"),
	(typeof(WhitePearl), "Perle blanche"),
	(typeof(BaseHighseasFish), "Poisson des bas fonds")
};



		public LargeFishingPole(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)2); // version
			writer.Write((int)m_Bait);
			writer.Write(m_Charge);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Bait = (Bait)reader.ReadInt();
			m_Charge = reader.ReadInt();
			Name = "canne à pêche"; // Nom correct pour une canne à péche
		}
	}

	public class RemoveBaitEntry : ContextMenuEntry
	{
		private Mobile m_From;
		private LargeFishingPole m_Pole;

		public RemoveBaitEntry(Mobile from, LargeFishingPole pole) : base(163, 1)
		{
			m_From = from;
			m_Pole = pole;
		}

		public override void OnClick()
		{
			if (!m_From.CheckAlive())
				return;

			if (m_Pole.Bait == Bait.Aucun || m_Pole.Charge <= 0)
				return;

			BaseBait newBait = BaseBait.CreateBait(m_Pole.Bait, m_Pole.Charge);

			if (newBait != null)
				m_From.AddToBackpack(newBait);

			m_Pole.Bait = Bait.Aucun;
			m_Pole.Charge = 0;

			m_From.SendMessage("Vous enlevez l'appât.");
		}
	}
}
