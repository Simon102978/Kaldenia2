using Server.Engines.Craft;

namespace Server.Items
{
	public class BazCapePlume :  BaseCloak
    {
        [Constructable]
	public BazCapePlume()
			: this(0)
	{
	}

	[Constructable]
	public BazCapePlume(int hue)
		: base(0xA491, hue)
	{
		Weight = 2.0;
		Name = "Cape à Plumes";
	}

	public BazCapePlume(Serial serial)
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

public class AileDraco :  BaseCloak

	{
	[Constructable]
	public AileDraco()
            : this(0)

		{
	}

	[Constructable]
	public AileDraco(int hue)
            : base(0xA497, hue)

		{
		Weight = 2.0;
		Name = "";
	}

	public AileDraco(Serial serial)
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

	public class BazCapeCourte : BaseCloak

	{
		[Constructable]
		public BazCapeCourte()
				: this(0)

		{
		}

		[Constructable]
		public BazCapeCourte(int hue)
				: base(0xA49E, hue)

		{
			Weight = 2.0;
			Name = "Cape Courte";
		}

		public BazCapeCourte(Serial serial)
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






