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
	public class PlainoisBoisDyeTub : FurnitureDyeTub
    {
        [Constructable]
        public PlainoisBoisDyeTub()
        {
            Hue = DyedHue = 1355;
            Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Plainois";
		}

        public PlainoisBoisDyeTub(Serial serial)
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
	public class CollinoisBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public CollinoisBoisDyeTub()
		{
			Hue = DyedHue = 1191;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Collinois";
		}

		public CollinoisBoisDyeTub(Serial serial)
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
	public class ForestierBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public ForestierBoisDyeTub()
		{
			Hue = DyedHue = 1411;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Forestier";
		}

		public ForestierBoisDyeTub(Serial serial)
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
	public class SavanoisBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public SavanoisBoisDyeTub()
		{
			Hue = DyedHue = 1008;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Savanois";
		}

		public SavanoisBoisDyeTub(Serial serial)
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
	public class DesertiqueBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public DesertiqueBoisDyeTub()
		{
			Hue = DyedHue = 1126;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Désertique";
		}

		public DesertiqueBoisDyeTub(Serial serial)
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
	public class MontagnardBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public MontagnardBoisDyeTub()
		{
			Hue = DyedHue = 2219;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Montagnard";
		}

		public MontagnardBoisDyeTub(Serial serial)
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
	public class VolcaniqueBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public VolcaniqueBoisDyeTub()
		{
			Hue = DyedHue = 1109;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Volcanique";
		}

		public VolcaniqueBoisDyeTub(Serial serial)
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
	public class TropicauxBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public TropicauxBoisDyeTub()
		{
			Hue = DyedHue = 2210;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Tropicaux";
		}

		public TropicauxBoisDyeTub(Serial serial)
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


	public class ToundroisBoisDyeTub : FurnitureDyeTub
	{
		[Constructable]
		public ToundroisBoisDyeTub()
		{
			Hue = DyedHue = 2500;
			Redyable = false;
			Charges = 1;
			Name = "Teinture Bois Toundrois";
		}

		public ToundroisBoisDyeTub(Serial serial)
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