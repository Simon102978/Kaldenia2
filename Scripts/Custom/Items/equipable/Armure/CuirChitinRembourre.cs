using Server.Engines.Craft;

namespace Server.Items
{

    public class CasqueChitinCuir : BaseArmor
    {
        [Constructable]
        public CasqueChitinCuir()
            : base(0xA42E)
        {
            Weight = 2.0;
			Name = "Casque de Chitin";

		}

        public CasqueChitinCuir(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance => 1;
        public override int BaseFireResistance => 4;
        public override int BaseColdResistance => 3;
        public override int BasePoisonResistance => 3;
        public override int BaseEnergyResistance => 3;
        public override int InitMinHits => 30;
        public override int InitMaxHits => 40;
        public override int StrReq => 20;
        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class JambiereChitinCuir : BaseArmor
	{
		[Constructable]
		public JambiereChitinCuir()
			: base(0xA430)
		{
			Weight = 4.0;
			Name = "Jambiere de Chitin";
		}

		public JambiereChitinCuir(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 20;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class PlastronChitinCuir : BaseArmor
	{
		[Constructable]
		public PlastronChitinCuir()
			: base(0xA42D)
		{
			Name = "Plastron de Chitin";
			Weight = 6.0;
		}

		public PlastronChitinCuir(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 25;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class BrassardChitinCuir : BaseArmor
	{
		[Constructable]
		public BrassardChitinCuir()
			: base(0xA42C)
		{
			Weight = 4.0;
			Name = "Brassard de Chitin";
		}

		public BrassardChitinCuir(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 20;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class GantChitinCuir : BaseArmor
	{
		[Constructable]
		public GantChitinCuir()
			: base(0xA42F)
		{
			Weight = 4.0;
			Name = "Gants de Chitin";
		}

		public GantChitinCuir(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 20;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class GorgetChitinCuir : BaseArmor
	{
		[Constructable]
		public GorgetChitinCuir()
			: base(0xA431)
		{
			Weight = 4.0;
			Name = "Gorgerin de Chitin";
		}

		public GorgetChitinCuir(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 20;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class CasqueRembourre1 : BaseArmor
	{
		[Constructable]
		public CasqueRembourre1()
			: base(0xA432)
		{
			Weight = 2.0;
			Name = "Casque Rembourré";
		}

		public CasqueRembourre1(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 20;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class CasqueRembourre2 : BaseArmor
	{
		[Constructable]
		public CasqueRembourre2()
			: base(0xA433)
		{
			Weight = 2.0;
			Name = "Casque Rembourré";

		}

		public CasqueRembourre2(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 20;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class CasqueRembourre3 : BaseArmor
	{
		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;

		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;

		public override int StrReq => 20;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
		public override CraftResource DefaultResource => CraftResource.RegularLeather;

		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;

		[Constructable]
		public CasqueRembourre3()
			: base(0xA434)
		{
			Weight = 1.0;
			Name = "Casque Rembourré";
		}

		public CasqueRembourre3(Serial serial)
			: base(serial)
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

	public class GorgetRembourre : BaseArmor
	{
		[Constructable]
		public GorgetRembourre()
			: base(0xA438)
		{
			Weight = 1.0;
			Name = "Gorget Rembourré";
		}

		public GorgetRembourre(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 20;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class JambiereRembourree : BaseArmor
	{
		[Constructable]
		public JambiereRembourree()
			: base(0xA439)
		{
			Name = "Jambière Rembourrée";
			Weight = 6.0;
		}

		public JambiereRembourree(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 25;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class PlastronRembourre : BaseArmor
	{
		[Constructable]
		public PlastronRembourre()
			: base(0xA43A)
		{
			Name = "Plastron Rembourré";
			Weight = 6.0;
		}

		public PlastronRembourre(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 25;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class CasqueRembourre : BaseArmor
	{
		[Constructable]
		public CasqueRembourre()
			: base(0xA435)
		{
			Name = "Casque Rembourré";
			Weight = 2.0;
		}

		public CasqueRembourre(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 25;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class BrassardRemb : BaseArmor
	{
		[Constructable]
		public BrassardRemb()
			: base(0xA436)
		{
			Name = "Epaulettes Rembourrées";
			Weight = 6.0;
		}

		public BrassardRemb(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 25;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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

	public class GantRembourre : BaseArmor
	{
		[Constructable]
		public GantRembourre()
			: base(0xA437)
		{
			Name = "Gants Rembourrés";
			Weight = 6.0;
		}

		public GantRembourre(Serial serial)
			: base(serial)
		{
		}

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;
		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override int StrReq => 25;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
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


