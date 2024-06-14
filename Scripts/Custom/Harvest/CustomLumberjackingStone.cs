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
						new HarvestVein(CollinoisDropChance,    0.0, res[3],  res[0]), // Collinois
						new HarvestVein(DesertiqueDropChance,   0.0, res[4],  res[0]), // Désertique
						new HarvestVein(SavanoisDropChance,     0.0, res[5],  res[0]), // Savanois
						new HarvestVein(MontagnardDropChance,	0.0, res[6],  res[0]), // Montagnard
						new HarvestVein(VolcaniqueDropChance,   0.0, res[7],  res[0]), // Volcanique
						new HarvestVein(TropicauxDropChance,    0.0, res[8],  res[0]), // Tropicaux
						new HarvestVein(ToundroisDropChance,    0.0, res[9],  res[0]), // Toundrois
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
		private double m_CollinoisDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double CollinoisDropChance
		{
			get { return m_CollinoisDropChance; }
			set { m_CollinoisDropChance = value; m_HarvestSystem = null; }
		}
		private double m_DesertiqueDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double DesertiqueDropChance
		{
			get { return m_DesertiqueDropChance; }
			set { m_DesertiqueDropChance = value; m_HarvestSystem = null; }
		}
		private double m_SavanoisDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double SavanoisDropChance
		{
			get { return m_SavanoisDropChance; }
			set { m_SavanoisDropChance = value; m_HarvestSystem = null; }
		}
		private double m_MontagnardDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double MontagnardDropChance
		{
			get { return m_MontagnardDropChance; }
			set { m_MontagnardDropChance = value; m_HarvestSystem = null; }
		}
		private double m_VolcaniqueDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double VolcaniqueDropChance
		{
			get { return m_VolcaniqueDropChance; }
			set { m_VolcaniqueDropChance = value; m_HarvestSystem = null; }
		}
		private double m_TropicauxDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double TropicauxDropChance
		{
			get { return m_TropicauxDropChance; }
			set { m_TropicauxDropChance = value; m_HarvestSystem = null; }
		}
		private double m_ToundroisDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double ToundroisDropChance
		{
			get { return m_ToundroisDropChance; }
			set { m_ToundroisDropChance = value; m_HarvestSystem = null; }
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
			writer.Write(CollinoisDropChance);
			writer.Write(DesertiqueDropChance);
			writer.Write(SavanoisDropChance);
			writer.Write(MontagnardDropChance);
			writer.Write(VolcaniqueDropChance);
			writer.Write(TropicauxDropChance);
			writer.Write(ToundroisDropChance);
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
						CollinoisDropChance = reader.ReadDouble();
						DesertiqueDropChance = reader.ReadDouble();
						SavanoisDropChance = reader.ReadDouble();
						MontagnardDropChance = reader.ReadDouble();
						VolcaniqueDropChance = reader.ReadDouble();
						TropicauxDropChance = reader.ReadDouble();
						ToundroisDropChance = reader.ReadDouble();
						AncienDropChance = reader.ReadDouble();
						break;
					}
			}
		}
	}
}
