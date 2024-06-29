using Server.Engines.Craft;

namespace Server.Items
{
	public class BrassardDaedric : BaseArmor
	{
		[Constructable]
		public BrassardDaedric()
			: base(0xA486)
		{
			Weight = 5.0;
			Name = "Brassard Daedric";
		}

		public BrassardDaedric(Serial serial)
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

	public class CasqueDaedric : BaseArmor
	{
	//	public override bool Anonymous => true;

		[Constructable]
		public CasqueDaedric()
			: base(0xA487)
		{
			Weight = 5.0;
			Name = "Casque Daedric";
		}

		public CasqueDaedric(Serial serial)
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

	public class PlastronDaedric : BaseArmor
	{
		[Constructable]
		public PlastronDaedric()
			: base(0xA48B)
		{
			Weight = 10.0;
			Name = "Plastron Daedric";
		}

		public PlastronDaedric(Serial serial)
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

	

	public class JambiereDaedric : BaseArmor
	{
		[Constructable]
		public JambiereDaedric()
			: base(0xA48A)
		{
			Weight = 7.0;
			Name = "Jambière Daedric";
		}

		public JambiereDaedric(Serial serial)
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

	

	public class GantsDaedric : BaseArmor
	{
		[Constructable]
		public GantsDaedric()
			: base(0xA488)
		{
			Weight = 2.0;
			Name = "Gants Daedric";
		}

		public GantsDaedric(Serial serial)
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

	public class GorgetDaedric : BaseArmor
	{
		[Constructable]
		public GorgetDaedric()
			: base(0xA489)
		{
			Weight = 2.0;
			Name = "Gorget Daedric";
		}

		public GorgetDaedric(Serial serial)
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






