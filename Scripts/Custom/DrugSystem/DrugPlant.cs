using System;

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
		private DrugPlantType m_PlantType;
		private DrugType m_DrugType;

		public override double MinSkill { get { return 50.0; } }
		public override double MaxSkill { get { return 100.0; } }

		[Constructable]
		public DrugCrop() : this(null) { }

		[Constructable]
		public DrugCrop(Mobile sower) : base(0xC7C)
		{
			Movable = false;
			Name = "Plante douteuse";
			Hue = 0x000;
			Sower = sower;

			// Générer aléatoirement le type de plante et le type de drogue
			m_PlantType = (DrugPlantType)Utility.Random(Enum.GetValues(typeof(DrugPlantType)).Length);
			m_DrugType = (DrugType)Utility.Random(Enum.GetValues(typeof(DrugType)).Length);

			Init(this, 1, 0xC62, 0xCC7, false);
		}

		public override void OnDoubleClick(Mobile from)
		{
			int amount = Utility.RandomMinMax(1, 5);
			for (int i = 0; i < amount; i++)
			{
				DrugBase drug = new DrugBase(m_PlantType, m_DrugType);
				from.AddToBackpack(drug);
			}
			from.SendMessage($"Vous récoltez {amount} pousse{(amount > 1 ? "s" : "")}.");
			this.Delete();
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
			Init(this, 1, 0xC61, 0xC7C, true);
		}
	}
}