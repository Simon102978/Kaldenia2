using Server.Engines.Craft;

namespace Server.Items
{

	public class BazDoubletCuir : BaseMiddleTorso
	{
		[Constructable]
		public BazDoubletCuir()
			: this(0)
		{
		}

		[Constructable]
		public BazDoubletCuir(int hue)
			: base(0xA492, hue)
		{
			Weight = 2.0;
			Name = "Doublet de Cuir";
		}

		public BazDoubletCuir(Serial serial)
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

	public class BazTuniqueCuirLong : BaseMiddleTorso

	{
		[Constructable]
		public BazTuniqueCuirLong()
				: this(0)

		{
		}

		[Constructable]
		public BazTuniqueCuirLong(int hue)
				: base(0xA498, hue)

		{
			Weight = 2.0;
			Name = "Tunique de Cuir longue";
		}

		public BazTuniqueCuirLong(Serial serial)
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

	public class BazGiletPropre : BaseMiddleTorso

	{
		[Constructable]
		public BazGiletPropre()
				: this(0)

		{
		}

		[Constructable]
		public BazGiletPropre(int hue)
				: base(0xA49C, hue)

		{
			Weight = 2.0;
			Name = "Gilet Propre";
		}

		public BazGiletPropre(Serial serial)
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
	public class BazPonchoLong : BaseMiddleTorso

	{
		[Constructable]
		public BazPonchoLong()
				: this(0)

		{
		}

		[Constructable]
		public BazPonchoLong(int hue)
				: base(0xA49D, hue)

		{
			Weight = 2.0;
			Name = "Poncho Long";
		}

		public BazPonchoLong(Serial serial)
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