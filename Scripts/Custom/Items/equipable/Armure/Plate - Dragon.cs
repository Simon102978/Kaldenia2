using Server.Engines.Craft;

namespace Server.Items
{
	public class BrassardDragon : BaseArmor
	{
		[Constructable]
		public BrassardDragon()
			: base(0xA48C)
		{
			Weight = 5.0;
			Name = "Brassard Dragonique";
		}

		public BrassardDragon(Serial serial)
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

	public class CasqueDragon : BaseArmor
	{
//		public override bool Anonymous => true;

		[Constructable]
		public CasqueDragon()
			: base(0xA48D)
		{
			Weight = 5.0;
			Name = "Casque Dragonique";
		}

		public CasqueDragon(Serial serial)
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

	public class PlastronDragon : BaseArmor
	{
		[Constructable]
		public PlastronDragon()
			: base(0xA461)
		{
			Weight = 10.0;
			Name = "Plastron Dragonique";
		}

		public PlastronDragon(Serial serial)
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

	

	public class JambiereDragon : BaseArmor
	{
		[Constructable]
		public JambiereDragon()
			: base(0xA48F)
		{
			Weight = 7.0;
			Name = "Jambière Dragonique";
		}

		public JambiereDragon(Serial serial)
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

	

	public class GantsDragon : BaseArmor
	{
		[Constructable]
		public GantsDragon()
			: base(0xA48E)
		{
			Weight = 2.0;
			Name = "Gants Dragonique";
		}

		public GantsDragon(Serial serial)
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

	public class GorgetDragon : BaseArmor
	{
		[Constructable]
		public GorgetDragon()
			: base(0xA462)
		{
			Weight = 2.0;
			Name = "Gorget Dragonique";
		}

		public GorgetDragon(Serial serial)
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






