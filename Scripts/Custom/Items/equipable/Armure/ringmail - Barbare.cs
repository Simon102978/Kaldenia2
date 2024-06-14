using Server.Engines.Craft;

namespace Server.Items
{

	public class BrassardMailleBarbare : BaseArmor
	{
		[Constructable]
		public BrassardMailleBarbare()
			: base(0xA456)
		{
			Weight = 7.0;
			Name = "Brassard Barbaresque";
		}

		public BrassardMailleBarbare(Serial serial)
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

	public class GantMailleBarbare : BaseArmor
	{
		[Constructable]
		public GantMailleBarbare()
			: base(0xA457)
		{
			Weight = 5.0;
			Name = "Gants Barbaresque";
		}

		public GantMailleBarbare(Serial serial)
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

	public class GorgetMailleBarbare : BaseArmor
	{
		[Constructable]
		public GorgetMailleBarbare()
			: base(0xA458)
		{
			Weight = 4.0;
			Name = "Gorgerin Barbaresque";
		}

		public GorgetMailleBarbare(Serial serial)
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

	public class CasqueMailleBarbare : BaseArmor
	{
		[Constructable]
		public CasqueMailleBarbare()
			: base(0xA45A)
		{
			Weight = 4.0;
			Name = "Casque Barbaresque";
		}

		public CasqueMailleBarbare(Serial serial)
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
	

	public class PlastronMailleBarbare : BaseArmor
	{
		[Constructable]
		public PlastronMailleBarbare()
			: base(0xA455)
		{
			Weight = 10.0;
			Name = "Plastron Barbaresque";
		}

		public PlastronMailleBarbare(Serial serial)
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

	public class JambiereMailleBarbare : BaseArmor
	{
		[Constructable]
		public JambiereMailleBarbare()
			: base(0xA459)
		{
			Weight = 9.0;
			Name = "Jambière Barbaresque";
		}

		public JambiereMailleBarbare(Serial serial)
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
