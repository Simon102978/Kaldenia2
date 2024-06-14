using Server.Engines.Craft;

namespace Server.Items
{

	public class BazToge : BaseOuterTorso
	{
		[Constructable]
		public BazToge()
			: this(0)
		{
		}

		[Constructable]
		public BazToge(int hue)
			: base(0xA4A2, hue)
		{
			Weight = 2.0;
			Name = "Toge";
		}

		public BazToge(Serial serial)
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

	public class BazRobe2 : BaseOuterTorso

	{
		[Constructable]
		public BazRobe2()
				: this(0)

		{
		}

		[Constructable]
		public BazRobe2(int hue)
				: base(0xA4A4, hue)

		{
			Weight = 2.0;
			Name = "Robe";
		}

		public BazRobe2(Serial serial)
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