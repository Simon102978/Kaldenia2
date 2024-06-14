using Server.Engines.Craft;

namespace Server.Items
{
	public class BazPantalon1 :  BasePants
    {
        [Constructable]
	public BazPantalon1()
			: this(0)
	{
	}

	[Constructable]
	public BazPantalon1(int hue)
		: base(0xA4A0, hue)
	{
		Weight = 2.0;
		Name = "Pantalon";
	}

	public BazPantalon1(Serial serial)
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

public class BazPantalon2 :  BasePants

	{
	[Constructable]
	public BazPantalon2()
            : this(0)

		{
	}

	[Constructable]
	public BazPantalon2(int hue)
            : base(0xA4A1, hue)

		{
		Weight = 2.0;
		Name = "Pantalon";
	}

	public BazPantalon2(Serial serial)
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
public class BazPantalon3 :  BasePants

	{
	[Constructable]
	public BazPantalon3()
            : this(0)

		{
	}

	[Constructable]
	public BazPantalon3(int hue)
            : base(0xA4B9, hue)

		{
		Weight = 2.0;
		Name = "Pantalon";
	}

	public BazPantalon3(Serial serial)
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
public class BazPantalon4 :  BasePants

	{
	[Constructable]
	public BazPantalon4()
            : this(0)

		{
	}

	[Constructable]
	public BazPantalon4(int hue)
            : base(0xA4BA, hue)

		{
		Weight = 2.0;
		Name = "Pantalon";
	}

	public BazPantalon4(Serial serial)
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
public class BazPantalon5 :  BasePants

	{
	[Constructable]
	public BazPantalon5()
            : this(0)

		{
	}

	[Constructable]
	public BazPantalon5(int hue)
            : base(0xA4BB, hue)

		{
		Weight = 2.0;
		Name = "Pantalon";
	}

	public BazPantalon5(Serial serial)
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

	public class BazPagne : BasePants

	{
		[Constructable]
		public BazPagne()
				: this(0)

		{
		}

		[Constructable]
		public BazPagne(int hue)
				: base(0xA4BC, hue)

		{
			Weight = 2.0;
			Name = "Pagne";
		}

		public BazPagne(Serial serial)
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






