using System;
using Server;
using Server.Items;
using static Server.HueData;

namespace Server.Items
{
	public class GantsArchimage : BaseArmor
	{

		public override int InitMinHits => 30;
		public override int InitMaxHits => 40;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Cloth;
		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;

		[Constructable]
		public GantsArchimage() : base(0x13C6)
		{
			Name = "Gant De L'Archimage";
			Weight = 1.0;
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
