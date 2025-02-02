﻿using System;
using System.Collections.Generic;

namespace Server.Items.Crops
{
	public class DrugSeed : BaseSeed
	{
		public override bool CanGrowGarden { get { return true; } }
		public override double MinSkill { get { return 50.0; } }
		public override double MaxSkill { get { return 100.0; } }

		[Constructable]
		public DrugSeed() : this(1) { }

		[Constructable]
		public DrugSeed(int amount) : base(0xF27)
		{
			Stackable = true;
			Weight = .1;
			Hue = 0x5E2;
			Movable = true;
			Amount = amount;
			Name = "Graine douteuse";
		}

		public override void OnDoubleClick(Mobile from)
		{
			Sow(from, typeof(DrugSeedling));
		}

		public DrugSeed(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class DrugSeedling : BaseSeedling
	{
		private DrugPlantType m_PlantType;
		private DrugType m_DrugType;

		[Constructable]
		public DrugSeedling(Mobile sower) : base(0xCB5)
		{
			Movable = false;
			Name = "Pousse douteuse";
			Sower = sower;
			m_PlantType = (DrugPlantType)Utility.Random(Enum.GetValues(typeof(DrugPlantType)).Length);
			m_DrugType = (DrugType)Utility.Random(Enum.GetValues(typeof(DrugType)).Length);
			Init(this, typeof(DrugCrop));
		}

		public DrugSeedling(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
			writer.Write((int)m_PlantType);
			writer.Write((int)m_DrugType);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_PlantType = (DrugPlantType)reader.ReadInt();
			m_DrugType = (DrugType)reader.ReadInt();
			Init(this, typeof(DrugCrop));
		}
	}

	public class DrugCrop : BaseCrop
	{
		private static Dictionary<DrugPlantType, int> PlantTypeToItemID = new Dictionary<DrugPlantType, int>
	{
		{ DrugPlantType.Shimyshisha, 0x0C51 },
		{ DrugPlantType.Shenyr, 0x0C52 },
		{ DrugPlantType.Amaesha, 0x0C53 },
		{ DrugPlantType.Astishys, 0x0C54 },
		{ DrugPlantType.Gwelalith, 0x0C55 },
		{ DrugPlantType.Frilar, 0x0C56 },
		{ DrugPlantType.Thomahar, 0x0C57 },
		{ DrugPlantType.Thiseth, 0x0C58 },
		{ DrugPlantType.Etherawin, 0x0C59 },
		{ DrugPlantType.Eralirid, 0x0C5A }
	};

		private DrugPlantType m_PlantType;
		private DrugType m_DrugType;

		public override double MinSkill { get { return 50.0; } }
		public override double MaxSkill { get { return 100.0; } }

		[Constructable]
		public DrugCrop() : this(null) { }

		[Constructable]
		public DrugCrop(Mobile sower) : base(0xC61)
		{
			Movable = false;
			Name = "Plante douteuse";
			Hue = 0x000;
			Sower = sower;

			m_PlantType = (DrugPlantType)Utility.Random(Enum.GetValues(typeof(DrugPlantType)).Length);
			m_DrugType = (DrugType)Utility.Random(Enum.GetValues(typeof(DrugType)).Length);

			Init(this, 4, 0xC61, PlantTypeToItemID[m_PlantType], false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (ItemID == PlantTypeToItemID[m_PlantType])  // Vérifie si la plante est mature
			{
				int amount = 1;
				for (int i = 0; i < amount; i++)
				{
					DrugBase drug = new DrugBase(m_PlantType, m_DrugType);
					from.AddToBackpack(drug);
				}
				from.SendMessage($"Vous récoltez {amount} pousse{(amount > 1 ? "s" : "")}.");
				this.Delete();
			}
			else
			{
				from.SendMessage("Cette plante n'est pas encore mature.");
			}
		}

		public DrugCrop(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
			writer.Write((int)m_PlantType);
			writer.Write((int)m_DrugType);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_PlantType = (DrugPlantType)reader.ReadInt();
			m_DrugType = (DrugType)reader.ReadInt();
			Init(this, 4, 0xC61, PlantTypeToItemID[m_PlantType], true);
		}
	}
}
