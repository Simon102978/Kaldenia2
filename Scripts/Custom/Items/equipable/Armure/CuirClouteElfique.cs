using Server.Engines.Craft;

namespace Server.Items
{

	public class BrassardClouteElfique : BaseArmor
	{
		[Constructable]
		public BrassardClouteElfique()
			: base(0xA43B)
		{
			Weight = 4.0;
			Name = "Brassard Clouté Elfique";
		}

		public BrassardClouteElfique(Serial serial)
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

	
	public class PlastronClouteElfique : BaseArmor
	{
		[Constructable]
		public PlastronClouteElfique()
			: base(0xA43F)
		{
			Weight = 8.0;
			Name = "Plastron Clouté Elfique";
		}

		public PlastronClouteElfique(Serial serial)
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

	public class PantalonsClouteElfique : BaseArmor
	{
		[Constructable]
		public PantalonsClouteElfique()
			: base(0xA43E)
		{
			Weight = 6.0;
			Name = "Pantalons Clouté Elfique";
			Layer = Layer.Pants;

		}

		public PantalonsClouteElfique(Serial serial)
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

	public class GorgetClouteElfique : BaseArmor
	{
		[Constructable]
		public GorgetClouteElfique()
			: base(0xA43D)
		{
			Weight = 3.0;
			Name = "Gorgerin Clouté Elfique";
		}

		public GorgetClouteElfique(Serial serial)
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

	public class GantClouteElfique : BaseArmor
	{
		[Constructable]
		public GantClouteElfique()
			: base(0xA43C)
		{
			Weight = 2.0;
			Name = "Gants Clouté Elfique";
		}

		public GantClouteElfique(Serial serial)
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

	public class CasqueClouteElfique : BaseArmor
	{
		[Constructable]
		public CasqueClouteElfique()
			: base(0xA440)
		{
			Weight = 3.0;
			Name = "Casque Clouté Elfique";
		}

		public CasqueClouteElfique(Serial serial)
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






