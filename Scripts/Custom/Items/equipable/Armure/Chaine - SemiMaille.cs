using Server.Engines.Craft;

namespace Server.Items
{

	public class CasqueSemiMaille : BaseArmor
	{
		[Constructable]
		public CasqueSemiMaille()
			: base(0xA44F)
		{
			Weight = 4.0;
			Name = "Casque de Cuirasse"; 
		}

		public CasqueSemiMaille(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 5;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 4;
		public override int BasePoisonResistance => 1;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 35;
		public override int InitMaxHits => 60;
		public override int StrReq => 60;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;
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

	public class BrassardSemiMaille : BaseArmor
	{
		[Constructable]
		public BrassardSemiMaille()
			: base(0xA450)
		{
			Weight = 4.0;
			Name = "Brassard de Cuirasse";
		}

		public BrassardSemiMaille(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 5;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 4;
		public override int BasePoisonResistance => 1;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 35;
		public override int InitMaxHits => 60;
		public override int StrReq => 60;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;
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


	public class GantsSemiMaille : BaseArmor
	{
		public override int BasePhysicalResistance => 5;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 4;
		public override int BasePoisonResistance => 1;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 45;
		public override int InitMaxHits => 60;
		public override int StrReq => 60;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;

		[Constructable]
		public GantsSemiMaille()
			: base(0xA452)
		{
			Weight = 3.0;
			Name = "Gants de Cuirasse";
		}

		public GantsSemiMaille(Serial serial)
			: base(serial)
		{
		}

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

	public class GorgetSemiMaille : BaseArmor
	{
		public override int BasePhysicalResistance => 5;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 4;
		public override int BasePoisonResistance => 1;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 45;
		public override int InitMaxHits => 60;
		public override int StrReq => 60;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;

		[Constructable]
		public GorgetSemiMaille()
			: base(0xA453)
		{
			Weight = 3.0;
			Name = "Gorgerin de Cuirasse";
		}

		public GorgetSemiMaille(Serial serial)
			: base(serial)
		{
		}

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
	public class JambiereSemiMaille : BaseArmor
	{
		[Constructable]
		public JambiereSemiMaille()
			: base(0xA454)
		{
			Weight = 7.0;
			Name = "Jambière de Cuirasse";
		}

		public JambiereSemiMaille(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 5;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 4;
		public override int BasePoisonResistance => 1;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 45;
		public override int InitMaxHits => 60;
		public override int StrReq => 60;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;
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

	public class PlastronSemiMaille : BaseArmor
	{
		[Constructable]
		public PlastronSemiMaille()
			: base(0xA451)
		{
			Weight = 7.0;
			Name = "Plastron de Cuirasse";
		}

		public PlastronSemiMaille(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 5;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 4;
		public override int BasePoisonResistance => 1;
		public override int BaseEnergyResistance => 2;
		public override int InitMinHits => 45;
		public override int InitMaxHits => 60;
		public override int StrReq => 60;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;
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






