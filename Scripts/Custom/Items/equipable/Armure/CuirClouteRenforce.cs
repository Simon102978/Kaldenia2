using Server.Engines.Craft;

namespace Server.Items
{

	public class BrassardClouteRenforce : BaseArmor
	{
		[Constructable]
		public BrassardClouteRenforce()
			: base(0xA441)
		{
			Weight = 4.0;
			Name = "Brassard Clouté Renforcé";
		}

		public BrassardClouteRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 2;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 4;
		public override int InitMinHits => 35;
		public override int InitMaxHits => 45;
		public override int StrReq => 25;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
		public override CraftResource DefaultResource => CraftResource.RegularLeather;
		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;
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

	
	public class PlastronClouteRenforce : BaseArmor
	{
		[Constructable]
		public PlastronClouteRenforce()
			: base(0xA446)
		{
			Weight = 8.0;
			Name = "Plastron Clouté Renforcé";
		}

		public PlastronClouteRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 2;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 4;
		public override int InitMinHits => 35;
		public override int InitMaxHits => 45;
		public override int StrReq => 35;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
		public override CraftResource DefaultResource => CraftResource.RegularLeather;
		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;
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

	public class PantalonsClouteRenforce : BaseArmor
	{
		[Constructable]
		public PantalonsClouteRenforce()
			: base(0xA445)
		{
			Weight = 6.0;
			Name = "Pantalons Clouté Renforcé";
			Layer = Layer.Pants;
		}

		public PantalonsClouteRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 2;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 4;
		public override int InitMinHits => 35;
		public override int InitMaxHits => 45;
		public override int StrReq => 35;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
		public override CraftResource DefaultResource => CraftResource.RegularLeather;
		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;
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

	public class GorgetClouteRenforce : BaseArmor
	{
		[Constructable]
		public GorgetClouteRenforce()
			: base(0xA444)
		{
			Weight = 3.0;
			Name = "Gorgerin Clouté Renforcé";
		}

		public GorgetClouteRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 2;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 4;
		public override int InitMinHits => 35;
		public override int InitMaxHits => 45;
		public override int StrReq => 35;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
		public override CraftResource DefaultResource => CraftResource.RegularLeather;
		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;
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

	public class GantClouteRenforce : BaseArmor
	{
		[Constructable]
		public GantClouteRenforce()
			: base(0xA443)
		{
			Weight = 2.0;
			Name = "Gants Clouté Renforcé";
		}

		public GantClouteRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 2;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 4;
		public override int InitMinHits => 35;
		public override int InitMaxHits => 45;
		public override int StrReq => 35;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
		public override CraftResource DefaultResource => CraftResource.RegularLeather;
		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;
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

	public class CasqueClouteRenforce : BaseArmor
	{
		[Constructable]
		public CasqueClouteRenforce()
			: base(0xA442)
		{
			Weight = 3.0;
			Name = "Casque Clouté Renforcé";
		}

		public CasqueClouteRenforce(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 2;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 4;
		public override int InitMinHits => 35;
		public override int InitMaxHits => 45;
		public override int StrReq => 35;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
		public override CraftResource DefaultResource => CraftResource.RegularLeather;
		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;
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






