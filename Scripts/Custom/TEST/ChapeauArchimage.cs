using System;
using Server;
using Server.Items;

namespace Server.Items
{
	[Flipable(0x1718, 0x1540, 0x171A, 0x1717, 0xA3BE, 0xA350, 0xA34B, 0xA468, 0x278F)]
	public class ChapeauArchimage : BaseHat
	{
		public override int BasePhysicalResistance => 0;
		public override int BaseFireResistance => 0;
		public override int BaseColdResistance => 0;
		public override int BasePoisonResistance => 0;
		public override int BaseEnergyResistance => 0;

		public override int InitMinHits => 20;
		public override int InitMaxHits => 30;


		[Constructable]
		public ChapeauArchimage() : this(0)
		{
		}

		[Constructable]
		public ChapeauArchimage(int hue) : base(0x1718, hue)
		{
			Name = "Chapeau De L'Archimage";
			Weight = 1.0;
		}

		public ChapeauArchimage(Serial serial) : base(serial)
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
