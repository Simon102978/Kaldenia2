using Server.Engines.Craft;

namespace Server.Items
{
	public class BrassardEmbellit : BaseArmor
	{
		[Constructable]
		public BrassardEmbellit()
			: base(0xA449)
		{
			Weight = 5.0;
			Name = "Brassard de demi-plaque";
		}

		public BrassardEmbellit(Serial serial)
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
		public override int StrReq => 65;
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

	public class CasqueEmbellit : BaseArmor
	{
public override bool Disguise { get { return true; } }

		[Constructable]
		public CasqueEmbellit()
			: base(0xA44A)
		{
			Weight = 5.0;
			Name = "Casque de demi-plaque";
		}

		public CasqueEmbellit(Serial serial)
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
		public override int StrReq => 65;
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

	public class PlastronEmbellit : BaseArmor
	{
		[Constructable]
		public PlastronEmbellit()
			: base(0xA44E)
		{
			Weight = 10.0;
			Name = "Plastron de demi-plaque";
		}

		public PlastronEmbellit(Serial serial)
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
		public override int StrReq => 65;
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

	

	public class JambiereEmbellit : BaseArmor
	{
		[Constructable]
		public JambiereEmbellit()
			: base(0xA44D)
		{
			Weight = 7.0;
			Name = "Jambière de demi-plaque";
		}

		public JambiereEmbellit(Serial serial)
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
		public override int StrReq => 65;
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

	

	public class GantsEmbellit : BaseArmor
	{
		[Constructable]
		public GantsEmbellit()
			: base(0xA44B)
		{
			Weight = 2.0;
			Name = "Gants de demi-plaque";
		}

		public GantsEmbellit(Serial serial)
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
		public override int StrReq => 65;
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

	public class GorgetEmbellit : BaseArmor
	{
		[Constructable]
		public GorgetEmbellit()
			: base(0xA44C)
		{
			Weight = 2.0;
			Name = "Gorget de demi-plaque";
		}

		public GorgetEmbellit(Serial serial)
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






