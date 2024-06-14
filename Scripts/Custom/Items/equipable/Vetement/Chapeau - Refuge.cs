using Server.Engines.Craft;

namespace Server.Items
{

	public class Capuche1 :  BaseHat
    {
        [Constructable]
	public Capuche1()
			: this(0)
	{
	}

	[Constructable]
	public Capuche1(int hue)
		: base(0xA463, hue)
	{
		Weight = 2.0;
		Name ="Capuche";
	}

	public Capuche1(Serial serial)
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

public class ChapeauPlume1 :  BaseHat

	{
	[Constructable]
	public ChapeauPlume1()
            : this(0)

		{
	}

	[Constructable]
	public ChapeauPlume1(int hue)
            : base(0xA464, hue)

		{
		Weight = 2.0;
		Name = "Chapeau à Plume";
	}

	public ChapeauPlume1(Serial serial)
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
public class ChapeauPlume2 :  BaseHat

	{
	[Constructable]
	public ChapeauPlume2()
            : this(0)

		{
	}

	[Constructable]
	public ChapeauPlume2(int hue)
            : base(0xA465, hue)

		{
		Weight = 2.0;
		Name ="Chapeau à Plume";
	}

	public ChapeauPlume2(Serial serial)
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
public class ChapeauToc :  BaseHat

	{
	[Constructable]
	public ChapeauToc()
            : this(0)

		{
	}

	[Constructable]
	public ChapeauToc(int hue)
            : base(0xA466, hue)

		{
		Weight = 2.0;
		Name ="Chapeau Toque";
	}

	public ChapeauToc(Serial serial)
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
public class ToquePlume :  BaseHat

	{
	[Constructable]
	public ToquePlume()
            : this(0)

		{
	}

	[Constructable]
	public ToquePlume(int hue)
            : base(0xA467, hue)

		{
		Weight = 2.0;
		Name ="Toque à plume";
	}

	public ToquePlume(Serial serial)
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
public class Chale1 :  BaseHat

	{
	[Constructable]
	public Chale1()
            : this(0)

		{
	}

	[Constructable]
	public Chale1(int hue)
            : base(0xA468, hue)

		{
		Weight = 2.0;
		Name ="Grand Châle";
	}

	public Chale1(Serial serial)
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
public class ChapeauPlume3 :  BaseHat

	{
	[Constructable]
	public ChapeauPlume3()
            : this(0)

		{
	}

	[Constructable]
	public ChapeauPlume3(int hue)
            : base(0xA469, hue)

		{
		Weight = 2.0;
		Name ="Chapeau à Plume";
	}

	public ChapeauPlume3(Serial serial)
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
public class ChapeauMage :  BaseHat

	{
	[Constructable]
	public ChapeauMage()
            : this(0)

		{
	}

	[Constructable]
	public ChapeauMage(int hue)
            : base(0xA46A, hue)

		{
		Weight = 2.0;
		Name ="Chapeau Mage";
	}

	public ChapeauMage(Serial serial)
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






