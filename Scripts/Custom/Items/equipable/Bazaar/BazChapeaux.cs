using Server.Engines.Craft;

namespace Server.Items
{

	public class BazChapeau1 : BaseHat
	{
		[Constructable]
		public BazChapeau1()
			: this(0)
		{
		}

		[Constructable]
		public BazChapeau1(int hue)
			: base(0xA4A3, hue)
		{
			Weight = 2.0;
			Name = "Chapeau Haute-Forme";
		}

		public BazChapeau1(Serial serial)
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