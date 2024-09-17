using System;
using Server;
using Server.Items;

public class CapeOfKnowledge : BaseArmor
{
	public override int BasePhysicalResistance => 0;
	public override int BaseFireResistance => 2;
	public override int BaseColdResistance => 2;
	public override int BasePoisonResistance => 2;
	public override int BaseEnergyResistance => 2;

	public override int InitMinHits => 50;
	public override int InitMaxHits => 60;



	public override ArmorMaterialType MaterialType => ArmorMaterialType.Cloth;
	public override CraftResource DefaultResource => CraftResource.None;

	[Constructable]
	public CapeOfKnowledge() : base(0x1515)
	{
		Weight = 4.0;
		Name = "Cape Du Savoir";
		Hue = 1365; // Un bleu fonc�, vous pouvez ajuster selon vos pr�f�rences

		Attributes.BonusMana = 10;
	}

	public override void ResetArmor()
    {
		Attributes.BonusMana = 10;
    }



	public CapeOfKnowledge(Serial serial) : base(serial)
	{
	}

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write(0); // version
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
	}
}

public class CapeOfCourage : BaseArmor
{
	public override int BasePhysicalResistance => 2;
	public override int BaseFireResistance => 2;
	public override int BaseColdResistance => 2;
	public override int BasePoisonResistance => 2;
	public override int BaseEnergyResistance => 0;

	public override int InitMinHits => 50;
	public override int InitMaxHits => 60;

	
	public override ArmorMaterialType MaterialType => ArmorMaterialType.Cloth;
	public override CraftResource DefaultResource => CraftResource.None;

	[Constructable]
	public CapeOfCourage() : base(0x1515)
	{
		Weight = 4.0;
		Name = "Cape Du Courage";
		Hue = 1645; // Un rouge, vous pouvez ajuster selon vos pr�f�rences

		Attributes.BonusHits = 10;
	}

	public override void ResetArmor()
    {
		Attributes.BonusHits = 10;
    }

	public CapeOfCourage(Serial serial) : base(serial)
	{
	}

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write(0); // version
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
	}
}

public class CapeOfDetermination : BaseArmor
{
	public override int BasePhysicalResistance => 2;
	public override int BaseFireResistance => 0;
	public override int BaseColdResistance => 2;
	public override int BasePoisonResistance => 2;
	public override int BaseEnergyResistance => 2;

	public override int InitMinHits => 50;
	public override int InitMaxHits => 60;

	

	public override ArmorMaterialType MaterialType => ArmorMaterialType.Cloth;
	public override CraftResource DefaultResource => CraftResource.None;

	[Constructable]
	public CapeOfDetermination() : base(0x1515)
	{
		Weight = 4.0;
		Name = "Cape De Détermination";
		Hue = 2213; // Un vert, vous pouvez ajuster selon vos pr�f�rences

		Attributes.BonusStam = 10;
	}

	public override void ResetArmor()
    {
		Attributes.BonusStam = 10;
    }


	public CapeOfDetermination(Serial serial) : base(serial)
	{
	}

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write(0); // version
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
	}
}
