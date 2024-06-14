using Server.Engines.Craft;

namespace Server.Items
{

	public class BrassardMaillerRenforce : BaseArmor
	{
		[Constructable]
		public BrassardMaillerRenforce()
			: base(0xA45B)
		{
			Weight = 7.0;
			Name = "Brassard Broigne";
		}

		public BrassardMaillerRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 4;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 1;
		public override int BasePoisonResistance => 5;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 40;
		public override int InitMaxHits => 50;
		public override int StrReq => 40;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class GantMailleRenforce : BaseArmor
	{
		[Constructable]
		public GantMailleRenforce()
			: base(0xA45C)
		{
			Weight = 5.0;
			Name = "Gants Broigne";
		}

		public GantMailleRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 4;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 1;
		public override int BasePoisonResistance => 5;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 40;
		public override int InitMaxHits => 50;
		public override int StrReq => 40;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class GorgetMailleRenforce : BaseArmor
	{
		[Constructable]
		public GorgetMailleRenforce()
			: base(0xA45E)
		{
			Weight = 4.0;
			Name = "Gorgerin Broigne";
		}

		public GorgetMailleRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 4;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 1;
		public override int BasePoisonResistance => 5;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 40;
		public override int InitMaxHits => 50;
		public override int StrReq => 40;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class CasqueMailleRenforce : BaseArmor
	{
		[Constructable]
		public CasqueMailleRenforce()
			: base(0xA45D)
		{
			Weight = 4.0;
			Name = "Casque Broigne";
		}

		public CasqueMailleRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 4;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 1;
		public override int BasePoisonResistance => 5;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 40;
		public override int InitMaxHits => 50;
		public override int StrReq => 40;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	

	public class PlastronMailleRenforce : BaseArmor
	{
		[Constructable]
		public PlastronMailleRenforce()
			: base(0xA460)
		{
			Weight = 10.0;
			Name = "Plastron Broigne";
		}



		public PlastronMailleRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 4;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 1;
		public override int BasePoisonResistance => 5;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 40;
		public override int InitMaxHits => 50;
		public override int StrReq => 40;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class JambiereMailleRenforce: BaseArmor
	{
		[Constructable]
		public JambiereMailleRenforce()
			: base(0xA45F)
		{
			Weight = 9.0;
			Name = "Jambière Broigne";
		}

		public JambiereMailleRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 4;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 1;
		public override int BasePoisonResistance => 5;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 40;
		public override int InitMaxHits => 50;
		public override int StrReq => 40;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

		public class PlastronFemmeMailleRenforce : BaseArmor
		{
			[Constructable]
			public PlastronFemmeMailleRenforce()
				: base(0xA47E)
			{
				Weight = 8.0;
				Name = "Plastron Féminin Renforcé";
			}

			public PlastronFemmeMailleRenforce(Serial serial)
				: base(serial)
			{
			}

			public override int BasePhysicalResistance => 4;
			public override int BaseFireResistance => 3;
			public override int BaseColdResistance => 1;
			public override int BasePoisonResistance => 5;
			public override int BaseEnergyResistance => 3;
			public override int InitMinHits => 40;
			public override int InitMaxHits => 50;
			public override int StrReq => 40;
			public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;
			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class JupeMailleRenforce : BaseArmor
		{
			[Constructable]
			public JupeMailleRenforce()
				: base(0xA47F)
			{
				Weight = 7.0;
				Name = "Jupe Renforcée";
			}

			public JupeMailleRenforce(Serial serial)
				: base(serial)
			{
			}

			public override int BasePhysicalResistance => 4;
			public override int BaseFireResistance => 3;
			public override int BaseColdResistance => 1;
			public override int BasePoisonResistance => 5;
			public override int BaseEnergyResistance => 3;
			public override int InitMinHits => 40;
			public override int InitMaxHits => 50;
			public override int StrReq => 40;
			public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;
			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}



	}

