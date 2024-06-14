using Server.Engines.Craft;

namespace Server.Items
{

	public class BazBottes1 :  BaseShoes
    {
        [Constructable]
	public BazBottes1()
			: this(0)
	{
	}

	[Constructable]
	public BazBottes1(int hue)
		: base(0xA493	, hue)
	{
		Weight = 2.0;
		Name = "Bottes";
	}

	public BazBottes1(Serial serial)
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

public class BazBottes2 :  BaseShoes

	{
	[Constructable]
	public BazBottes2()
            : this(0)

		{
	}

	[Constructable]
	public BazBottes2(int hue)
            : base(0xA494, hue)

		{
		Weight = 2.0;
		Name = "Bottes";
	}

	public BazBottes2(Serial serial)
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
public class BazBottes3 :  BaseShoes

	{
	[Constructable]
	public BazBottes3()
            : this(0)

		{
	}

	[Constructable]
	public BazBottes3(int hue)
            : base(0xA495, hue)

		{
		Weight = 2.0;
		Name = "Bottes";
	}

	public BazBottes3(Serial serial)
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
public class BazBottes4 :  BaseShoes

	{
	[Constructable]
	public BazBottes4()
            : this(0)

		{
	}

	[Constructable]
	public BazBottes4(int hue)
            : base(0xA496, hue)

		{
		Weight = 2.0;
		Name = "Bottes";
	}

	public BazBottes4(Serial serial)
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






