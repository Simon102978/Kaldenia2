using System;
using Server;
using Server.Items;
using static Server.HueData;

namespace Server.Items
{
	public class GantsArchimage : LeatherGloves
	{

		public override int BasePhysicalResistance => 1;
		public override int BaseFireResistance => 4;
		public override int BaseColdResistance => 3;
		public override int BasePoisonResistance => 3;
		public override int BaseEnergyResistance => 3;

		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;



		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
		public override CraftResource DefaultResource => CraftResource.RegularLeather;
		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;




		[Constructable]
		public GantsArchimage() : base(0x13C6)
		{
			Name = "Gant De L'Archimage";
			Weight = 1.0;
			Hue = 0; // Couleur magique, ajustez selon vos préférences
		}

		public GantsArchimage(Serial serial) : base(serial)
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
}
