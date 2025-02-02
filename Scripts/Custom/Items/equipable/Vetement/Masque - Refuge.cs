using Server.Engines.Craft;

namespace Server.Items
{

	public class Masque1 :  BaseHat
    {

	//	public override bool Disguise { get { return true; } }

		[Constructable]
	public Masque1()
			: this(0)
	{
	}

	[Constructable]
	public Masque1(int hue)
		: base(0xA46B, hue)
	{
		Weight = 2.0;
		Name ="Masque ossement de cerf";
	}

	public Masque1(Serial serial)
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

public class Masque2 :  BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
	public Masque2()
            : this(0)

		{
	}

	[Constructable]
	public Masque2(int hue)
            : base(0xA46C, hue)

		{
		Weight = 2.0;
		Name = "Masque Ossement d'�lan";
	}

	public Masque2(Serial serial)
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
public class Masque3 :  BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
	public Masque3()
            : this(0)

		{
	}

	[Constructable]
	public Masque3(int hue)
            : base(0xA46D, hue)

		{
		Weight = 2.0;
		Name = "Masque Cr�ne";
	}

	public Masque3(Serial serial)
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
public class Masque4 :  BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
	public Masque4()
            : this(0)

		{
	}

	[Constructable]
	public Masque4(int hue)
            : base(0xA46E, hue)

		{
		Weight = 2.0;
			Name = "Masque Cr�ne � piques";
		}

	public Masque4(Serial serial)
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
public class Masque5 :  BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
	public Masque5()
            : this(0)

		{
	}

	[Constructable]
	public Masque5(int hue)
            : base(0xA46F, hue)

		{
		Weight = 2.0;
			Name = "Masque du Sage � cornes";
		}

	public Masque5(Serial serial)
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
public class Masque6 :  BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
	public Masque6()
            : this(0)

		{
	}

	[Constructable]
	public Masque6(int hue)
            : base(0xA470, hue)

		{
		Weight = 2.0;
			Name = "Masque de plumes fines";
		}

	public Masque6(Serial serial)
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
public class Masque7 :  BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
	public Masque7()
            : this(0)

		{
	}

	[Constructable]
	public Masque7(int hue)
            : base(0xA471, hue)

		{
		Weight = 2.0;
			Name = "Masque simple";
		}

	public Masque7(Serial serial)
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
public class Masque8 :  BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
	public Masque8()
            : this(0)

		{
	}

	[Constructable]
	public Masque8(int hue)
            : base(0xA472, hue)

		{
		Weight = 2.0;
			Name = "Masque V�nitien";
		}

	public Masque8(Serial serial)
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
	public class Masque9 : BaseHat

	{

		public override bool Disguise { get { return true; } }

		[Constructable]
		public Masque9()
				: this(0)

		{
		}

		[Constructable]
		public Masque9(int hue)
				: base(0xA473, hue)

		{
			Weight = 2.0;
			Name = "Masque-foulard";
		}

		public Masque9(Serial serial)
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

	public class Masque10 : BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
		public Masque10()
				: this(0)

		{
		}

		[Constructable]
		public Masque10(int hue)
				: base(0xA474, hue)

		{
			Weight = 2.0;
			Name = "Lunettes d'aveugle";
		}

		public Masque10(Serial serial)
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
	public class Masque11 : BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
		public Masque11()
				: this(0)

		{
		}

		[Constructable]
		public Masque11(int hue)
				: base(0xA475, hue)

		{
			Weight = 2.0;
			Name = "Bandeau oeil droit";
		}

		public Masque11(Serial serial)
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

	public class Masque12 : BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
		public Masque12()
				: this(0)

		{
		}

		[Constructable]
		public Masque12(int hue)
				: base(0xA476, hue)

		{
			Weight = 2.0;
			Name = "Masque Cr�ne � foulard";
		}

		public Masque12(Serial serial)
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

	public class Masque13 : BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
		public Masque13()
				: this(0)

		{
		}

		[Constructable]
		public Masque13(int hue)
				: base(0xA477, hue)

		{
			Weight = 2.0;
			Name = "Masque de soir�e";
		}

		public Masque13(Serial serial)
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
	public class Masque14 : BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
		public Masque14()
				: this(0)

		{
		}

		[Constructable]
		public Masque14(int hue)
				: base(0xA478, hue)

		{
			Weight = 2.0;
			Name = "Masque Festif";
		}

		public Masque14(Serial serial)
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
	public class Masque15 : BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
		public Masque15()
				: this(0)

		{
		}

		[Constructable]
		public Masque15(int hue)
				: base(0xA479, hue)

		{
			Weight = 2.0;
			Name = "Masque du ph�nix";
		}

		public Masque15(Serial serial)
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
	public class Masque16 : BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
		public Masque16()
				: this(0)

		{
		}

		[Constructable]
		public Masque16(int hue)
				: base(0xA47A, hue)

		{
			Weight = 2.0;
			Name = "Masque simple � foulard";
		}

		public Masque16(Serial serial)
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

	public class Masque17 : BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
		public Masque17()
				: this(0)

		{
		}

		[Constructable]
		public Masque17(int hue)
				: base(0xA47B, hue)

		{
			Weight = 2.0;
			Name = "Masque dor�";
		}

		public Masque17(Serial serial)
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

	public class Masque18 : BaseHat

	{
		public override bool Disguise { get { return true; } }

		[Constructable]
		public Masque18()
				: this(0)

		{
		}

		[Constructable]
		public Masque18(int hue)
				: base(0xA47C, hue)

		{
			Weight = 2.0;
			Name = "Masque partiel orn�";
		}

		public Masque18(Serial serial)
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
	public class Masque19 : BaseHat

	{
		[Constructable]
		public Masque19()
				: this(0)

		{
		}
		public override bool Disguise { get { return true; } }

		[Constructable]
		public Masque19(int hue)
				: base(0xA47D, hue)

		{
			Weight = 2.0;
			Name = "Masque";
		}

		public Masque19(Serial serial)
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






