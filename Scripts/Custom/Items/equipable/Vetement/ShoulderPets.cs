using Server.Engines.Craft;

namespace Server.Items
{

	public class ShoulderMonkey : BaseOuterTorso
	{
		[Constructable]
		public ShoulderMonkey()
			: this(0)
		{
		}

		[Constructable]
		public ShoulderMonkey(int hue)
			: base(0xA53B, hue)
		{
			Weight = 3.0;
			Name = "Un Singe";
			Layer = Layer.Neck;
			
		}

		public ShoulderMonkey(Serial serial)
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




	public class ShoulderEagle : BaseOuterTorso
	{
		[Constructable]
		public ShoulderEagle()
			: this(0)
		{
		}

		[Constructable]
		public ShoulderEagle(int hue)
			: base(0xA53C, hue)
		{
			Weight = 3.0;
			Name = "Un Aigle";
			Layer = Layer.Neck;


		}

		public ShoulderEagle(Serial serial)
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


	public class ShoulderAraignee : BaseOuterTorso
	{
	[Constructable]
	public ShoulderAraignee()
		: this(0)
	{
	}

	[Constructable]
	public ShoulderAraignee(int hue)
		: base(0xA53D, hue)
	{
		Weight = 3.0;
		Name = "Une Araignée";
		Layer = Layer.Neck;


		}

		public ShoulderAraignee(Serial serial)
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



	public class ShoulderChouette : BaseOuterTorso
	{
	[Constructable]
	public ShoulderChouette()
		: this(0)
	{
	}

	[Constructable]
	public ShoulderChouette(int hue)
		: base(0xA53E, hue)
	{
		Weight = 3.0;
		Name = "Une Chouette";
		Layer = Layer.Neck;


		}

		public ShoulderChouette(Serial serial)
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



	public class ShoulderCorneille : BaseOuterTorso
	{
	[Constructable]
	public ShoulderCorneille()
		: this(0)
	{
	}

	[Constructable]
	public ShoulderCorneille(int hue)
		: base(0xA53F, hue)
	{
		Weight = 3.0;
		Name = "Un Corbeau";
		Layer = Layer.Neck;


		}

		public ShoulderCorneille(Serial serial)
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



	public class ShoulderLezard : BaseOuterTorso
	{
	[Constructable]
	public ShoulderLezard()
		: this(0)
	{
	}

	[Constructable]
	public ShoulderLezard(int hue)
		: base(0xA540, hue)
	{
		Weight = 3.0;
		Name = "Un Lézard";
		Layer = Layer.Neck;


		}

		public ShoulderLezard(Serial serial)
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



	public class ShoulderSerpent : BaseOuterTorso
	{
	[Constructable]
	public ShoulderSerpent()
		: this(0)
	{
	}

	[Constructable]
	public ShoulderSerpent(int hue)
		: base(0xA536, hue)
	{
		Weight = 3.0;
		Name = "Un Serpent";
		Layer = Layer.Neck;


		}

		public ShoulderSerpent(Serial serial)
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