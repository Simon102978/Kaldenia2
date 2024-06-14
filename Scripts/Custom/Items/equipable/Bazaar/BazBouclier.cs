using Server.Engines.Craft;

namespace Server.Items
{
	public class BazBouclierSerpentin : BaseShield // MetalKiteShield
	{
		[Constructable]
		public BazBouclierSerpentin()
			: base(0xA499)
		{
			Weight = 7.0;
			Name = "Bouclier Serpentin";
		}

		public BazBouclierSerpentin(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 7;
		public override int BaseFireResistance => 0;
		public override int BaseColdResistance => 0;
		public override int BasePoisonResistance => 0;
		public override int BaseEnergyResistance => 1;
		public override int InitMinHits => 45;
		public override int InitMaxHits => 60;
		public override int StrReq => 45;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);//version
		}
	}
	public class BazBouclierHeraldique : BaseShield // MetalKiteShield
	{
		[Constructable]
		public BazBouclierHeraldique()
			: base(0xA49A)
		{
			Weight = 7.0;
			Name = "Bouclier Heraldique";
		}

		public BazBouclierHeraldique(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 7;
		public override int BaseFireResistance => 0;
		public override int BaseColdResistance => 0;
		public override int BasePoisonResistance => 0;
		public override int BaseEnergyResistance => 1;
		public override int InitMinHits => 45;
		public override int InitMaxHits => 60;
		public override int StrReq => 45;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);//version
		}
	}
	public class BazBouclierEmeraude : BaseShield // MetalKiteShield
	{
		[Constructable]
		public BazBouclierEmeraude()
			: base(0xA4A6)
		{
			Weight = 7.0;
			Name = "Bouclier Emeraude";
		}

		public BazBouclierEmeraude(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 7;
		public override int BaseFireResistance => 0;
		public override int BaseColdResistance => 0;
		public override int BasePoisonResistance => 0;
		public override int BaseEnergyResistance => 1;
		public override int InitMinHits => 45;
		public override int InitMaxHits => 60;
		public override int StrReq => 45;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);//version
		}
	}
	public class BazBouclierElfique : BaseShield // MetalKiteShield
	{
		[Constructable]
		public BazBouclierElfique()
			: base(0xA4A5)
		{
			Weight = 7.0;
			Name = "Bouclier Elfique";
		}

		public BazBouclierElfique(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 7;
		public override int BaseFireResistance => 0;
		public override int BaseColdResistance => 0;
		public override int BasePoisonResistance => 0;
		public override int BaseEnergyResistance => 1;
		public override int InitMinHits => 45;
		public override int InitMaxHits => 60;
		public override int StrReq => 45;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);//version
		}
	}

}






