using System;
using static Server.Items.Crops.CropHelper;

namespace Server.Items.Crops
{
	public class BaseCrop : Item
	{
		public virtual bool CanGrowFarm { get { return Config.Get("Farming.CanGrowFarm", true); } }
		public virtual bool CanGrowHouseTiles { get { return Config.Get("Farming.CanGrowHouseTiles", true); } }
		public virtual bool CanGrowDirt { get { return Config.Get("Farming.CanGrowDirt", true); } }
		public virtual bool CanGrowGround { get { return Config.Get("Farming.CanGrowGround", false); } }
		public virtual bool CanGrowSwamp { get { return Config.Get("Farming.CanGrowSwamp", false); } }
		public virtual bool CanGrowSand { get { return Config.Get("Farming.CanGrowSand", false); } }
		public virtual bool CanGrowGarden { get { return Config.Get("Farming.CanGrowGarden", true); } }

		public virtual TimeSpan SowerPickTime { get { return TimeSpan.FromDays(Config.Get("Farming.SowerPickTime", (14))); } }

		public virtual bool PlayerCanDestroy { get { return Config.Get("Farming.PlayerCanDestroy", true); } }
		private bool i_bumpZ = false;

		public bool BumpZ { get { return i_bumpZ; } set { i_bumpZ = value; } }

		private static Mobile m_Sower;

		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile Sower { get { return m_Sower; } set { m_Sower = value; } }

		public Timer GrowingTimer;

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime LastSowerVisit { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Growing { get { return GrowingTimer.Running; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int Yield { get; set; }

		public int Capacity { get; set; }
		public int FullGraphic { get; set; }
		public int PickGraphic { get; set; }
		public DateTime LastPick { get; set; }

		public BaseCrop(int itemID) : base(itemID)
		{
		}

		public BaseCrop(Serial serial) : base(serial)
		{
		}

		private class CropTimer : Timer
		{
			private BaseCrop i_plant;
			public CropTimer(BaseCrop plant) : base(TimeSpan.FromSeconds(450), TimeSpan.FromSeconds(15))
			{
				Priority = TimerPriority.OneSecond;
				i_plant = plant;
			}
			protected override void OnTick()
			{
				if (Utility.RandomBool())
				{
					if ((i_plant != null) && (!i_plant.Deleted))
					{
						int current = i_plant.Yield;
						if (++current >= i_plant.Capacity)
						{
							current = i_plant.Capacity;
							((Item)i_plant).ItemID = i_plant.FullGraphic;
							Stop();
						}
						else if (current <= 0) current = 1;
						i_plant.Yield = current;
					}
					else Stop();
				}
			}
		}

		public static void Init(BaseCrop plant, int capacity, int pickGraphic, int fullGraphic, bool full)
		{
			plant.Capacity = capacity;
			plant.LastSowerVisit = DateTime.UtcNow;
			plant.PickGraphic = pickGraphic;
			plant.FullGraphic = fullGraphic;
			plant.LastPick = DateTime.UtcNow;
			plant.GrowingTimer = new CropTimer(plant);

			if (full) 
			{ 
				plant.Yield = plant.Capacity; 
				plant.ItemID = plant.FullGraphic; 
			}
			else 
			{ 
				plant.Yield = 0; 
				plant.ItemID = plant.PickGraphic; 
				plant.GrowingTimer.Start(); 
			}
		}

		public void Gather(Mobile from, Type result)
		{
			if (Sower == null || Sower.Deleted) 
				Sower = from;

			if (from.Mounted && !CanWorkMounted) 
			{ 
				from.SendMessage("Vous ne pouvez récolter sur une monture."); 
				return; 
			}

			if (DateTime.UtcNow > LastPick.AddSeconds(3))
			{
				LastPick = DateTime.UtcNow;

				int cookValue = (int)from.Skills[SkillName.Cooking].Value / 20;

				if (cookValue == 0) 
				{ 
					from.SendMessage("Vous ignorez comment récolter cette pousse."); 
					return; 
				}

				if (!from.InRange(GetWorldLocation(), 1))
				{
					from.SendMessage("Vous êtes trop loin pour récolter quelque chose.");
					return;
				}

				if (Yield < 1) 
				{ 
					from.SendMessage("Il n'y a rien à récolter ici.");
					return;
				}
					
				from.Direction = from.GetDirectionTo(this);
				from.Animate(from.Mounted ? 29 : 32, 5, 1, true, false, 0);
				LastSowerVisit = DateTime.UtcNow;

				if (cookValue > Yield) 
					cookValue = Yield + 1;

				int amount = Utility.RandomMinMax(cookValue - 4, cookValue);

				if (amount <= 0)
				{
					from.SendMessage("Votre récolte ne porte pas fruit.");
					return;
				}

				Yield -= amount;

				Item item = (Item)Activator.CreateInstance(result);
				item.Amount = amount;
				from.AddToBackpack(item);
				from.SendMessage("Vous récoltez {0} {1}{2}!", amount, item.Name, amount == 1 ? "" : "s");

				if (Yield < 1)
					Delete();
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);

			writer.Write(Sower);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			Sower = reader.ReadMobile();
		}
	}
}
