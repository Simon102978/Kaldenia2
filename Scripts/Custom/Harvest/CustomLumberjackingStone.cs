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
						new HarvestVein(PalmierDropChance,       0.0, res[0],  null),   // Normal
						new HarvestVein(ErableDropChance,     0.0, res[1],  res[0]), // Plainois
						new HarvestVein(CheneDropChance,    0.0, res[2],  res[0]), // Forestier
						new HarvestVein(CedreDropChance,    0.0, res[3],  res[0]), // Cèdre
						new HarvestVein(CypresDropChance,   0.0, res[4],  res[0]), // Cypres
						new HarvestVein(SauleDropChance,     0.0, res[5],  res[0]), // Saule
						new HarvestVein(AcajouDropChance,	0.0, res[6],  res[0]), // Acajou
						new HarvestVein(EbeneDropChance,   0.0, res[7],  res[0]), // Ébène
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

		private double m_PalmierDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double PalmierDropChance
		{
			get { return m_PalmierDropChance; }
			set { m_PalmierDropChance = value; m_HarvestSystem = null; }
		}
		private double m_ErableDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double ErableDropChance
		{
			get { return m_ErableDropChance; }
			set { m_ErableDropChance = value; m_HarvestSystem = null; }
		}
		private double m_CheneDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double CheneDropChance
		{
			get { return m_CheneDropChance; }
			set { m_CheneDropChance = value; m_HarvestSystem = null; }
		}
		private double m_CedreDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double CedreDropChance
		{
			get { return m_CedreDropChance; }
			set { m_CedreDropChance = value; m_HarvestSystem = null; }
		}
		private double m_CypresDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double CypresDropChance
		{
			get { return m_CypresDropChance; }
			set { m_CypresDropChance = value; m_HarvestSystem = null; }
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
		private double m_EbeneDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double EbeneDropChance
		{
			get { return m_EbeneDropChance; }
			set { m_EbeneDropChance = value; m_HarvestSystem = null; }
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

			PalmierDropChance = 100;
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
			writer.Write(PalmierDropChance);
			writer.Write(ErableDropChance);
			writer.Write(CheneDropChance);
			writer.Write(CedreDropChance);
			writer.Write(CypresDropChance);
			writer.Write(SauleDropChance);
			writer.Write(AcajouDropChance);
			writer.Write(EbeneDropChance);
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
						PalmierDropChance = reader.ReadDouble();
						ErableDropChance = reader.ReadDouble();
						CheneDropChance = reader.ReadDouble();
						CedreDropChance = reader.ReadDouble();
						CypresDropChance = reader.ReadDouble();
						SauleDropChance = reader.ReadDouble();
						AcajouDropChance = reader.ReadDouble();
						EbeneDropChance = reader.ReadDouble();
						AmaranteDropChance = reader.ReadDouble();
						PinDropChance = reader.ReadDouble();
						AncienDropChance = reader.ReadDouble();
						break;
					}
			}
		}
	}
}
