using Server.Engines.Craft;

namespace Server.Items
{
	public class Earrings : BaseArmor, IRepairable
	{
		public CraftSystem RepairSystem => DefTinkering.CraftSystem;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;

		private int m_PhysicalResistance;
		private int m_FireResistance;
		private int m_ColdResistance;
		private int m_PoisonResistance;
		private int m_EnergyResistance;

		public override int BasePhysicalResistance => m_PhysicalResistance;
		public override int BaseFireResistance => m_FireResistance;
		public override int BaseColdResistance => m_ColdResistance;
		public override int BasePoisonResistance => m_PoisonResistance;
		public override int BaseEnergyResistance => m_EnergyResistance;

		public override int InitMinHits => 20;
		public override int InitMaxHits => 50;

		[Constructable]
		public Earrings() : base(0x4213)
		{
			Name = "Boucles d'oreilles pendantes";
			Layer = Layer.Earrings;

			GenerateResistances();
		}

		private void GenerateResistances()
		{
			m_PhysicalResistance = Utility.RandomMinMax(0, 3);
			m_FireResistance = Utility.RandomMinMax(0, 3);
			m_ColdResistance = Utility.RandomMinMax(0, 3);
			m_PoisonResistance = Utility.RandomMinMax(0, 3);
			m_EnergyResistance = Utility.RandomMinMax(0, 3);
		}

		public Earrings(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(1); // version

			writer.Write(m_PhysicalResistance);
			writer.Write(m_FireResistance);
			writer.Write(m_ColdResistance);
			writer.Write(m_PoisonResistance);
			writer.Write(m_EnergyResistance);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if (version >= 1)
			{
				m_PhysicalResistance = reader.ReadInt();
				m_FireResistance = reader.ReadInt();
				m_ColdResistance = reader.ReadInt();
				m_PoisonResistance = reader.ReadInt();
				m_EnergyResistance = reader.ReadInt();
			}
			else
			{
				GenerateResistances();
			}
		}
	}
}
