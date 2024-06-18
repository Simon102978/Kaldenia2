using System;

namespace Server.Items.Crops
{
	public class BaseSeedling : Item, IChopable
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

		public BaseSeedling(int itemID) : base(itemID)
		{
		}

		public virtual void OnChop(Mobile from)
		{
		}

		public BaseSeedling(Serial serial) : base(serial)
		{
		}

		public static void Init(BaseSeedling plant, Type type)
		{
			plant.GrowingTimer = new CropHelper.GrowTimer(plant, type, plant.Sower);
			plant.GrowingTimer.Start();
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (from.Mounted && !CropHelper.CanWorkMounted) { from.SendMessage("Le plant est trop petit pour pouvoir être récolté sur votre monture."); return; }
			else from.SendMessage("Votre pousse est trop jeune pour être récoltée.");
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
