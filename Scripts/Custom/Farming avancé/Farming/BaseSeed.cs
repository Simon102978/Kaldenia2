using System.Collections;
using System;

namespace Server.Items.Crops
{
	public class BaseSeed : Item, IChopable
	{
		public virtual bool CanGrowFarm { get { return Config.Get("Farming.CanGrowFarm", true); } }
		public virtual bool CanGrowHouseTiles { get { return Config.Get("Farming.CanGrowHouseTiles", true); } }
		public virtual bool CanGrowDirt { get { return Config.Get("Farming.CanGrowDirt", true); } }
		public virtual bool CanGrowGround { get { return Config.Get("Farming.CanGrowGround", true); } }
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

		public BaseSeed(int itemID) : base(itemID)
		{
		}

		public virtual void OnChop(Mobile from)
		{
		}

		public BaseSeed(Serial serial) : base(serial)
		{
		}

		public static void InitSeedling(BaseSeed plant, Type type)
		{
			plant.GrowingTimer = new CropHelper.GrowTimer(plant, type, plant.Sower);
			plant.GrowingTimer.Start();
		}

		public void Sow(Mobile from, Type seedType)
		{
			if (from.Mounted && !CropHelper.CanWorkMounted)
			{
				from.SendMessage("Vous ne pouvez pas planter une graine lorsque vous êtes sur votre monture.");
				return;
			}
			Point3D m_pnt = from.Location;
			Map m_map = from.Map;

			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042010);
				return;
			}
			else if (!CropHelper.CheckCanGrow(this, m_map, m_pnt.X, m_pnt.Y))
			{
				from.SendMessage("Cette graine ne poussera pas ici.");
				return;
			}
			ArrayList Seedshere = CropHelper.CheckCrop(m_pnt, m_map, 0);
			if (Seedshere.Count > 0)
			{
				from.SendMessage("Il y a déjà un plant qui pousse ici.");
				return;
			}
			ArrayList Seedsnear = CropHelper.CheckCrop(m_pnt, m_map, 1);
			if ((Seedsnear.Count > 1))
			{
				from.SendMessage("Il y a trop de plants à proximité.");
				return;
			}
			if (this.BumpZ)
				++m_pnt.Z;
			if (!from.Mounted)
				from.Animate(32, 5, 1, true, false, 0);
			from.SendMessage("Vous plantez la graine.");
			this.Consume();

			Item item = (BaseSeedling)Activator.CreateInstance(seedType, new object[] { from });
			item.Location = m_pnt;
			item.Map = m_map;
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
