namespace Server.Items
{

	public class BlancBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public BlancBoisDyeTub()
		{
			Hue = DyedHue = 0;
			Redyable = true;
			Charges = 1;
			Name = "Teinture Bois Vierge";
		}

		public BlancBoisDyeTub(Serial serial)
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
	public class ÉrableBoisDyeTub : FurnitureDyeTub
    {
        [Constructable]
        public ÉrableBoisDyeTub()
        {
            Hue = DyedHue = 1355;
            Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Érable";
		}

        public ÉrableBoisDyeTub(Serial serial)
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
	public class CèdreBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public CèdreBoisDyeTub()
		{
			Hue = DyedHue = 1191;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Cèdre";
		}

		public CèdreBoisDyeTub(Serial serial)
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
	public class ChêneBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public ChêneBoisDyeTub()
		{
			Hue = DyedHue = 1411;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Chêne";
		}

		public ChêneBoisDyeTub(Serial serial)
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
	public class SauleBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public SauleBoisDyeTub()
		{
			Hue = DyedHue = 1008;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Saule";
		}

		public SauleBoisDyeTub(Serial serial)
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
	public class CyprèsBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public CyprèsBoisDyeTub()
		{
			Hue = DyedHue = 1126;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Cyprès";
		}

		public CyprèsBoisDyeTub(Serial serial)
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
	public class AcajouBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public AcajouBoisDyeTub()
		{
			Hue = DyedHue = 2219;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Acajou";
		}

		public AcajouBoisDyeTub(Serial serial)
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
	public class ÉbèneBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public ÉbèneBoisDyeTub()
		{
			Hue = DyedHue = 1109;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Ébène";
		}

		public ÉbèneBoisDyeTub(Serial serial)
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
	public class AmaranteBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public AmaranteBoisDyeTub()
		{
			Hue = DyedHue = 2210;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Amarante";
		}

		public AmaranteBoisDyeTub(Serial serial)
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


	public class PinBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public PinBoisDyeTub()
		{
			Hue = DyedHue = 2500;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Pin";
		}

		public PinBoisDyeTub(Serial serial)
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
	public class AncienBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public AncienBoisDyeTub()
		{
			Hue = DyedHue = 1779;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Ancien";
		}

		public AncienBoisDyeTub(Serial serial)
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