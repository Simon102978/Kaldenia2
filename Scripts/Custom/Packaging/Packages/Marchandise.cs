﻿namespace Server.Custom.Packaging.Packages
{
	public  class Marchandise : Item
	{
		[Constructable]
		public Marchandise() : base(0xA5D8)
		{
			Name = "Marchandises";
			Hue = 542;
			Weight = 0.1;
			Stackable = true;
		}

		public Marchandise(Serial serial)
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
}
