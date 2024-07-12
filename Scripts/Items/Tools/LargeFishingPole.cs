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
            // Ajoutez d'autres mappages pour d'autres types d'app�ts si n�cessaire
        };

		public Bait CurrentBait { get { return m_Bait; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public Bait Bait
		{
			get { return m_Bait; }
			set { m_Bait = value; InvalidateProperties(); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int Charge
		{
			get { return m_Charge; }
			set
			{
				m_Charge = value;
				InvalidateProperties();

			}
		}

		[Constructable]
		public LargeFishingPole() : base(0x0DC0)
		{
			Name = "canne � p�che";
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

		public override void OnDoubleClick(Mobile from)
		{
			if (Bait == Bait.Aucun || Charge <= 0)
			{
				from.Target = new BaitTarget(this);
				from.SendMessage("S�lectionnez l'app�t que vous voulez utiliser.");
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
						from.SendMessage($"Vous avez appliqu� {bait.Name} � votre canne � p�che.");
					}
					else
					{
						from.SendMessage("Vous ne pouvez pas appliquer cet app�t.");
					}
				}
				else
				{
					from.SendMessage("Ce n'est pas un app�t valide.");
				}
			}
		}


		public void FinishFishing(Mobile from)
		{
			Type fishType = null;
			double chance = Utility.RandomDouble();

			// V�rifier la comp�tence de p�che
			from.CheckSkill(SkillName.Fishing, 0, 100);

			double fishingSkill = from.Skills[SkillName.Fishing].Value;

			// Calculer les chances bas�es sur le skill
			double chanceSpecificFish = (fishingSkill / 10.0) + 5.0;
			double chanceNormalFish = (fishingSkill / 5.0) + 20.0;

			// V�rifier si le joueur peut p�cher une carte au tr�sor
			bool canFishTreasureMap = fishingSkill >= 50 && from.Map == Map.Felucca;
			if (canFishTreasureMap && chance < 0.005) // 0.5% de chance
			{
				// P�cher une carte au tr�sor
				int level = Utility.RandomMinMax(1, 3); // Niveau 1, 2 ou 3
				TreasureMap treasureMap = new TreasureMap(level, from.Map);
				if (from.AddToBackpack(treasureMap))
				{
					from.SendMessage("Vous avez p�ch� une carte au tr�sor !");
					return; // Terminer la m�thode ici, pas besoin de p�cher un poisson
				}
				else
				{
					treasureMap.Delete(); // Supprimer la carte si elle ne peut pas �tre ajout�e au sac
					from.SendMessage("Vous avez p�ch� une carte au tr�sor, mais votre sac est plein. La carte est perdue.");
				}
			}

			// Logique pour p�cher un poisson normal
			if (m_Bait != Bait.Aucun && m_Charge > 0)
			{
				if (chance * 100 < chanceSpecificFish) // Chance bas�e sur le skill de p�cher le poisson sp�cifique
				{
					if (baitFishMap.TryGetValue(m_Bait, out Type[] fishTypes))
					{
						fishType = fishTypes[0];
					}
				}
				else if (chance * 100 < (chanceSpecificFish + chanceNormalFish)) // Chance additionnelle de p�cher un poisson normal
				{
					fishType = typeof(Fish);
				}
				// Le reste r�sulte en aucune prise
				// Consommer l'app�t
				m_Charge--;
				if (m_Charge == 0)
				{
					m_Bait = Bait.Aucun;
				}
			}
			else
			{
				// Sans app�t, uniquement la chance de p�cher un poisson normal
				if (chance * 100 < chanceNormalFish)
				{
					fishType = typeof(Fish);
				}
			}

			if (fishType != null)
			{
				// Cr�er le poisson
				Item harvested = Activator.CreateInstance(fishType) as Item;
				if (harvested != null)
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
					// Nous ne d�finissons plus de poids al�atoire pour les poissons non-rares

					// V�rifier si le joueur peut porter le poisson
					int currentWeight = from.TotalWeight;
					int maxWeight = from.MaxWeight;
					if (currentWeight + weight <= maxWeight)
					{
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
							from.SendMessage($"Votre sac est plein. Le {fishType.Name} tombe � vos pieds.");
						}
					}
					else
					{
						harvested.MoveToWorld(from.Location, from.Map);
						from.SendMessage($"Le {fishType.Name} est trop lourd pour vous. Il tombe � vos pieds.");
					}
				}
			}
			else
			{
				from.SendMessage("Vous n'avez rien attrapé cette fois-ci.");
			}

			// Notifier le joueur du statut de l'app�t et de la charge
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
				list.Add("[ aucun app�t ]");
			}
}
	


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
			Name = "canne � p�che"; // Nom correct pour une canne � p�che
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

			m_From.SendMessage("Vous enlevez l'app�t.");
		}
	}
}
