using Server.Engines.Harvest;

namespace Server.Items
{
	public class CustomLumberjackingStone : Item
	{
		private HarvestSystem m_HarvestSystem;
		public HarvestSystem HarvestSystem
		{
			get
			{
				if (m_HarvestSystem == null)
				{
					var system = new CustomLumberjacking();

					var res = system.Definition.Resources;

					var veins = new HarvestVein[]
					{
						new HarvestVein(NormalDropChance,       0.0, res[0],  null),   // Normal
						new HarvestVein(PlainoisDropChance,     0.0, res[1],  res[0]), // Plainois
						new HarvestVein(ForestierDropChance,    0.0, res[2],  res[0]), // Forestier
						new HarvestVein(CèdreDropChance,    0.0, res[3],  res[0]), // Cèdre
						new HarvestVein(CyprèsDropChance,   0.0, res[4],  res[0]), // Cyprès
						new HarvestVein(SauleDropChance,     0.0, res[5],  res[0]), // Saule
						new HarvestVein(AcajouDropChance,	0.0, res[6],  res[0]), // Acajou
						new HarvestVein(ÉbèneDropChance,   0.0, res[7],  res[0]), // Ébène
						new HarvestVein(AmaranteDropChance,    0.0, res[8],  res[0]), // Amarante
						new HarvestVein(PinDropChance,    0.0, res[9],  res[0]), // Pin
						new HarvestVein(AncienDropChance,       0.0, res[10], res[0]), // Ancien
					};

					system.Definition.Veins = veins;

					m_HarvestSystem = system;
				}

				return m_HarvestSystem;
			}
		}

		private double m_NormalDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double NormalDropChance
		{
			get { return m_NormalDropChance; }
			set { m_NormalDropChance = value; m_HarvestSystem = null; }
		}
		private double m_PlainoisDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double PlainoisDropChance
		{
			get { return m_PlainoisDropChance; }
			set { m_PlainoisDropChance = value; m_HarvestSystem = null; }
		}
		private double m_ForestierDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double ForestierDropChance
		{
			get { return m_ForestierDropChance; }
			set { m_ForestierDropChance = value; m_HarvestSystem = null; }
		}
		private double m_CèdreDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double CèdreDropChance
		{
			get { return m_CèdreDropChance; }
			set { m_CèdreDropChance = value; m_HarvestSystem = null; }
		}
		private double m_CyprèsDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double CyprèsDropChance
		{
			get { return m_CyprèsDropChance; }
			set { m_CyprèsDropChance = value; m_HarvestSystem = null; }
		}
		private double m_SauleDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double SauleDropChance
		{
			get { return m_SauleDropChance; }
			set { m_SauleDropChance = value; m_HarvestSystem = null; }
		}
		private double m_AcajouDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double AcajouDropChance
		{
			get { return m_AcajouDropChance; }
			set { m_AcajouDropChance = value; m_HarvestSystem = null; }
		}
		private double m_ÉbèneDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double ÉbèneDropChance
		{
			get { return m_ÉbèneDropChance; }
			set { m_ÉbèneDropChance = value; m_HarvestSystem = null; }
		}
		private double m_AmaranteDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double AmaranteDropChance
		{
			get { return m_AmaranteDropChance; }
			set { m_AmaranteDropChance = value; m_HarvestSystem = null; }
		}
		private double m_PinDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double PinDropChance
		{
			get { return m_PinDropChance; }
			set { m_PinDropChance = value; m_HarvestSystem = null; }
		}
		private double m_AncienDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double AncienDropChance
		{
			get { return m_AncienDropChance; }
			set { m_AncienDropChance = value; m_HarvestSystem = null; }
		}

		[Constructable]
		public CustomLumberjackingStone() : base(0xED4)
		{
			Name = "Pierre de buches";
			Visible = false;
			Movable = false;

			NormalDropChance = 100;
		}

		public override void OnDoubleClick(Mobile from)
		{
		}

		public CustomLumberjackingStone(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version

			//Version 0
			writer.Write(NormalDropChance);
			writer.Write(PlainoisDropChance);
			writer.Write(ForestierDropChance);
			writer.Write(CèdreDropChance);
			writer.Write(CyprèsDropChance);
			writer.Write(SauleDropChance);
			writer.Write(AcajouDropChance);
			writer.Write(ÉbèneDropChance);
			writer.Write(AmaranteDropChance);
			writer.Write(PinDropChance);
			writer.Write(AncienDropChance);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch(version)
			{
				case 0:
					{
						NormalDropChance = reader.ReadDouble();
						PlainoisDropChance = reader.ReadDouble();
						ForestierDropChance = reader.ReadDouble();
						CèdreDropChance = reader.ReadDouble();
						CyprèsDropChance = reader.ReadDouble();
						SauleDropChance = reader.ReadDouble();
						AcajouDropChance = reader.ReadDouble();
						ÉbèneDropChance = reader.ReadDouble();
						AmaranteDropChance = reader.ReadDouble();
						PinDropChance = reader.ReadDouble();
						AncienDropChance = reader.ReadDouble();
						break;
					}
			}
		}
	}
}
