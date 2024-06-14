using Server.Engines.Harvest;

namespace Server.Items
{
	public class CustomMiningStone : Item
	{
		private HarvestSystem m_HarvestSystem;
		public HarvestSystem HarvestSystem
		{
			get
			{
				if (m_HarvestSystem == null)
				{
					var system = new CustomMining();

					var res = system.OreAndStone.Resources;

					var veins = new HarvestVein[]
					{
						new HarvestVein(FerDropChance,          0.0, res[0],  null),   // Fer
						new HarvestVein(BronzeDropChance,       0.0, res[1],  res[0]), // Bronze
						new HarvestVein(CuivreDropChance,       0.0, res[2],  res[0]), // Cuivre
						new HarvestVein(SonneDropChance,        0.0, res[3],  res[0]), // Sonne
						new HarvestVein(ArgentDropChance,       0.0, res[4],  res[0]), // Argent
						new HarvestVein(BorealeDropChance,      0.0, res[5],  res[0]), // Boréale
						new HarvestVein(ChrysteliarDropChance,  0.0, res[6],  res[0]), // Chrysteliar
						new HarvestVein(GlaciasDropChance,      0.0, res[7],  res[0]), // Glacias
						new HarvestVein(LithiarDropChance,      0.0, res[8],  res[0]), // Lithiar
						new HarvestVein(AcierDropChance,        0.0, res[9],  res[0]), // Acier
						new HarvestVein(DurianDropChance,       0.0, res[10], res[0]), // Durian
						new HarvestVein(EquilibrumDropChance,   0.0, res[11], res[0]), // Équilibrum
						new HarvestVein(OrDropChance,           0.0, res[12], res[0]), // Or
						new HarvestVein(JolinarDropChance,      0.0, res[13], res[0]), // Jolinar
						new HarvestVein(JusticiumDropChance,    0.0, res[14], res[0]), // Justicium
						new HarvestVein(AbyssiumDropChance,     0.0, res[15], res[0]), // Abyssium
						new HarvestVein(BloodiriumDropChance,   0.0, res[16], res[0]), // Bloodirium
						new HarvestVein(HerbrositeDropChance,   0.0, res[17], res[0]), // Herbrosite
						new HarvestVein(KhandariumDropChance,   0.0, res[18], res[0]), // Khandarium
						new HarvestVein(MytherilDropChance,     0.0, res[19], res[0]), // Mytheril
						new HarvestVein(SombralirDropChance,    0.0, res[20], res[0]), // Sombralir
					};

					system.OreAndStone.Veins = veins;

					m_HarvestSystem = system;
				}

				return m_HarvestSystem;
			}
		}

		private double m_FerDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double FerDropChance
		{
			get { return m_FerDropChance; }
			set { m_FerDropChance = value; m_HarvestSystem = null; }
		}
		private double m_BronzeDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double BronzeDropChance
		{
			get { return m_BronzeDropChance; }
			set { m_BronzeDropChance = value; m_HarvestSystem = null; }
		}
		private double m_CuivreDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double CuivreDropChance
		{
			get { return m_CuivreDropChance; }
			set { m_CuivreDropChance = value; m_HarvestSystem = null; }
		}
		private double m_SonneDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double SonneDropChance
		{
			get { return m_SonneDropChance; }
			set { m_SonneDropChance = value; m_HarvestSystem = null; }
		}
		private double m_ArgentDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double ArgentDropChance
		{
			get { return m_ArgentDropChance; }
			set { m_ArgentDropChance = value; m_HarvestSystem = null; }
		}
		private double m_BorealeDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double BorealeDropChance
		{
			get { return m_BorealeDropChance; }
			set { m_BorealeDropChance = value; m_HarvestSystem = null; }
		}
		private double m_ChrysteliarDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double ChrysteliarDropChance
		{
			get { return m_ChrysteliarDropChance; }
			set { m_ChrysteliarDropChance = value; m_HarvestSystem = null; }
		}
		private double m_GlaciasDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double GlaciasDropChance
		{
			get { return m_GlaciasDropChance; }
			set { m_GlaciasDropChance = value; m_HarvestSystem = null; }
		}
		private double m_LithiarDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double LithiarDropChance
		{
			get { return m_LithiarDropChance; }
			set { m_LithiarDropChance = value; m_HarvestSystem = null; }
		}
		private double m_AcierDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double AcierDropChance
		{
			get { return m_AcierDropChance; }
			set { m_AcierDropChance = value; m_HarvestSystem = null; }
		}
		private double m_DurianDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double DurianDropChance
		{
			get { return m_DurianDropChance; }
			set { m_DurianDropChance = value; m_HarvestSystem = null; }
		}
		private double m_EquilibrumDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double EquilibrumDropChance
		{
			get { return m_EquilibrumDropChance; }
			set { m_EquilibrumDropChance = value; m_HarvestSystem = null; }
		}
		private double m_OrDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double OrDropChance
		{
			get { return m_OrDropChance; }
			set { m_OrDropChance = value; m_HarvestSystem = null; }
		}
		private double m_JolinarDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double JolinarDropChance
		{
			get { return m_JolinarDropChance; }
			set { m_JolinarDropChance = value; m_HarvestSystem = null; }
		}
		private double m_JusticiumDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double JusticiumDropChance
		{
			get { return m_JusticiumDropChance; }
			set { m_JusticiumDropChance = value; m_HarvestSystem = null; }
		}
		private double m_AbyssiumDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double AbyssiumDropChance
		{
			get { return m_AbyssiumDropChance; }
			set { m_AbyssiumDropChance = value; m_HarvestSystem = null; }
		}
		private double m_BloodiriumDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double BloodiriumDropChance
		{
			get { return m_BloodiriumDropChance; }
			set { m_BloodiriumDropChance = value; m_HarvestSystem = null; }
		}
		private double m_HerbrositeDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double HerbrositeDropChance
		{
			get { return m_HerbrositeDropChance; }
			set { m_HerbrositeDropChance = value; m_HarvestSystem = null; }
		}
		private double m_KhandariumDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double KhandariumDropChance
		{
			get { return m_KhandariumDropChance; }
			set { m_KhandariumDropChance = value; m_HarvestSystem = null; }
		}
		private double m_MytherilDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double MytherilDropChance
		{
			get { return m_MytherilDropChance; }
			set { m_MytherilDropChance = value; m_HarvestSystem = null; }
		}
		private double m_SombralirDropChance = 0;
		[CommandProperty(AccessLevel.Administrator)]
		public double SombralirDropChance
		{
			get { return m_SombralirDropChance; }
			set { m_SombralirDropChance = value; m_HarvestSystem = null; }
		}

		[Constructable]
		public CustomMiningStone() : base(0xED4)
		{
			Name = "Pierre de minerais";
			Visible = false;
			Movable = false;

			FerDropChance = 100;
		}

		public override void OnDoubleClick(Mobile from)
		{
		}

		public CustomMiningStone(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version

			//Version 0
			writer.Write(FerDropChance);
			writer.Write(BronzeDropChance);
			writer.Write(CuivreDropChance);
			writer.Write(SonneDropChance);
			writer.Write(ArgentDropChance);
			writer.Write(BorealeDropChance);
			writer.Write(ChrysteliarDropChance);
			writer.Write(GlaciasDropChance);
			writer.Write(LithiarDropChance);
			writer.Write(AcierDropChance);
			writer.Write(DurianDropChance);
			writer.Write(EquilibrumDropChance);
			writer.Write(OrDropChance);
			writer.Write(JolinarDropChance);
			writer.Write(JusticiumDropChance);
			writer.Write(AbyssiumDropChance);
			writer.Write(BloodiriumDropChance);
			writer.Write(HerbrositeDropChance);
			writer.Write(KhandariumDropChance);
			writer.Write(MytherilDropChance);
			writer.Write(SombralirDropChance);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch(version)
			{
				case 0:
					{
						FerDropChance = reader.ReadDouble();
						BronzeDropChance = reader.ReadDouble();
						CuivreDropChance = reader.ReadDouble();
						SonneDropChance = reader.ReadDouble();
						ArgentDropChance = reader.ReadDouble();
						BorealeDropChance = reader.ReadDouble();
						ChrysteliarDropChance = reader.ReadDouble();
						GlaciasDropChance = reader.ReadDouble();
						LithiarDropChance = reader.ReadDouble();
						AcierDropChance = reader.ReadDouble();
						DurianDropChance = reader.ReadDouble();
						EquilibrumDropChance = reader.ReadDouble();
						OrDropChance = reader.ReadDouble();
						JolinarDropChance = reader.ReadDouble();
						JusticiumDropChance = reader.ReadDouble();
						AbyssiumDropChance = reader.ReadDouble();
						BloodiriumDropChance = reader.ReadDouble();
						HerbrositeDropChance = reader.ReadDouble();
						KhandariumDropChance = reader.ReadDouble();
						MytherilDropChance = reader.ReadDouble();
						SombralirDropChance = reader.ReadDouble();
						break;
					}
			}
		}
	}
}
