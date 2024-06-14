using Server.Engines.Craft;

namespace Server.Items
{
	public class BrassardVieillit : BaseArmor
	{
		[Constructable]
		public BrassardVieillit()
			: base(0xA482)
		{
			Weight = 5.0;
			Name = "Brassard Ancien";
		}

		public BrassardVieillit(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 6;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 2;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 50;
		public override int InitMaxHits => 65;
		public override int StrReq => 80;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
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

	public class CasqueVieillit : BaseArmor
	{
	//	public override bool Anonymous => true;

		[Constructable]
		public CasqueVieillit()
			: base(0xA485)
		{
			Weight = 5.0;
			Name = "Casque Ancien";
		}

		public CasqueVieillit(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 6;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 2;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 50;
		public override int InitMaxHits => 65;
		public override int StrReq => 80;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
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

	public class PlastronViellit : BaseArmor
	{
		[Constructable]
		public PlastronViellit()
			: base(0xA481)
		{
			Weight = 10.0;
			Name = "Plastron Ancien";
		}

		public PlastronViellit(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 6;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 2;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 50;
		public override int InitMaxHits => 65;
		public override int StrReq => 95;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
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

	

	public class JambiereViellit : BaseArmor
	{
		[Constructable]
		public JambiereViellit()
			: base(0xA480)
		{
			Weight = 7.0;
			Name = "Jambière Ancien";
		}

		public JambiereViellit(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 6;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 2;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 50;
		public override int InitMaxHits => 65;
		public override int StrReq => 90;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
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

	

	public class GantsVieillit : BaseArmor
	{
		[Constructable]
		public GantsVieillit()
			: base(0xA483)
		{
			Weight = 2.0;
			Name = "Gants Anciens";
		}

		public GantsVieillit(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 6;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 2;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 50;
		public override int InitMaxHits => 65;
		public override int StrReq => 70;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

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

	public class GorgetVieillit : BaseArmor
	{
		[Constructable]
		public GorgetVieillit()
			: base(0xA484)
		{
			Weight = 2.0;
			Name = "Gorget Ancien";
		}

		public GorgetVieillit(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 6;
		public override int BaseFireResistance => 3;
		public override int BaseColdResistance => 2;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 50;
		public override int InitMaxHits => 65;
		public override int StrReq => 45;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
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






