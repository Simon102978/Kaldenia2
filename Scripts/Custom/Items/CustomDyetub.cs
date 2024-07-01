namespace Server.Items
{
	public class OrSombreDyeTub : DyeTub
	{
		[Constructable]
		public OrSombreDyeTub()
		{
			Hue = DyedHue = 2389;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Or Sombre";
		}

		public OrSombreDyeTub(Serial serial)
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
	public class CitronAbrasifDyeTub : DyeTub
	{
		[Constructable]
		public CitronAbrasifDyeTub()
		{
			Hue = DyedHue = 2943;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Citron Abrasif";
		}

		public CitronAbrasifDyeTub(Serial serial)
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
	public class OrDouxDyeTub : DyeTub
	{
		[Constructable]
		public OrDouxDyeTub()
		{
			Hue = DyedHue = 2946;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Or Doux";
		}

		public OrDouxDyeTub(Serial serial)
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
	public class BeurreDouxDyeTub : DyeTub
	{
		[Constructable]
		public BeurreDouxDyeTub()
		{
			Hue = DyedHue = 2885;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Beurre Doux";
		}

		public BeurreDouxDyeTub(Serial serial)
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
	public class CafeBruleDyeTub : DyeTub
	{
		[Constructable]
		public CafeBruleDyeTub()
		{
			Hue = DyedHue = 2832;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Cafe Brule";
		}

		public CafeBruleDyeTub(Serial serial)
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
	public class RouilleDyeTub : DyeTub
	{
		[Constructable]
		public RouilleDyeTub()
		{
			Hue = DyedHue = 1922;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Rouille";
		}

		public RouilleDyeTub(Serial serial)
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
	public class BronzeDyeTub : DyeTub
	{
		[Constructable]
		public BronzeDyeTub()
		{
			Hue = DyedHue = 1798;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Bronze";
		}

		public BronzeDyeTub(Serial serial)
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
	public class CuivreDyeTub : DyeTub
	{
		[Constructable]
		public CuivreDyeTub()
		{
			Hue = DyedHue = 1955;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Cuivre";
		}

		public CuivreDyeTub(Serial serial)
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
	public class RougeSanguinDyeTub : DyeTub
	{
		[Constructable]
		public RougeSanguinDyeTub()
		{
			Hue = DyedHue = 2252;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Rouge Sanguin";
		}

		public RougeSanguinDyeTub(Serial serial)
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
	public class BordeauDyeTub : DyeTub
	{
		[Constructable]
		public BordeauDyeTub()
		{
			Hue = DyedHue = 2327;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Bordeau";
		}

		public BordeauDyeTub(Serial serial)
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
	public class CorailDyeTub : DyeTub
	{
		[Constructable]
		public CorailDyeTub()
		{
			Hue = DyedHue = 2190;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Corail";
		}

		public CorailDyeTub(Serial serial)
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
	public class RougeObscureDyeTub : DyeTub
	{
		[Constructable]
		public RougeObscureDyeTub()
		{
			Hue = DyedHue = 1461;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Rouge Obscure";
		}

		public RougeObscureDyeTub(Serial serial)
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
	public class BleuGlacierDyeTub : DyeTub
	{
		[Constructable]
		public BleuGlacierDyeTub()
		{
			Hue = DyedHue = 2362;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Bleu Glacier";
		}

		public BleuGlacierDyeTub(Serial serial)
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
	public class BleuProfondDyeTub : DyeTub
	{
		[Constructable]
		public BleuProfondDyeTub()
		{
			Hue = DyedHue = 1790;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Bleu Profond";
		}

		public BleuProfondDyeTub(Serial serial)
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
	public class BleuCielDyeTub : DyeTub
	{
		[Constructable]
		public BleuCielDyeTub()
		{
			Hue = DyedHue = 2251;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Bleu Ciel";
		}

		public BleuCielDyeTub(Serial serial)
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
	public class BleuSombreDyeTub : DyeTub
	{
		[Constructable]
		public BleuSombreDyeTub()
		{
			Hue = DyedHue = 1916;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Bleu Sombre";
		}

		public BleuSombreDyeTub(Serial serial)
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
	public class VertSombreDyeTub : DyeTub
	{
		[Constructable]
		public VertSombreDyeTub()
		{
			Hue = DyedHue = 1436;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Vert Sombre";
		}

		public VertSombreDyeTub(Serial serial)
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
	public class VertOliveDyeTub : DyeTub
	{
		[Constructable]
		public VertOliveDyeTub()
		{
			Hue = DyedHue = 1267;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Vert Olive";
		}

		public VertOliveDyeTub(Serial serial)
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
	public class VertPrintanierDyeTub : DyeTub
	{
		[Constructable]
		public VertPrintanierDyeTub()
		{
			Hue = DyedHue = 2100;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Vert Printanier";
		}

		public VertPrintanierDyeTub(Serial serial)
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
	public class VertIridescentDyeTub : DyeTub
	{
		[Constructable]
		public VertIridescentDyeTub()
		{
			Hue = DyedHue = 2367;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Vert Iridescent";
		}

		public VertIridescentDyeTub(Serial serial)
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
	public class TurquoisePlumePaon : DyeTub
	{
		[Constructable]
		public TurquoisePlumePaon()
		{
			Hue = DyedHue = 2282;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Plume Paon";
		}

		public TurquoisePlumePaon(Serial serial)
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
	public class TurquoiseDyeTub : DyeTub
	{
		[Constructable]
		public TurquoiseDyeTub()
		{
			Hue = DyedHue = 2176;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Turquoise";
		}

		public TurquoiseDyeTub(Serial serial)
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
	public class EcaillePoissonDyeTub : DyeTub
	{
		[Constructable]
		public EcaillePoissonDyeTub()
		{
			Hue = DyedHue = 2587;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Ecaille de Poisson";
		}

		public EcaillePoissonDyeTub(Serial serial)
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
	public class AquaMarineDyeTub : DyeTub
	{
		[Constructable]
		public AquaMarineDyeTub()
		{
			Hue = DyedHue = 2084;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Aqua Marine";
		}

		public AquaMarineDyeTub(Serial serial)
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
	public class MauveIndigoDyeTub : DyeTub
	{
		[Constructable]
		public MauveIndigoDyeTub()
		{
			Hue = DyedHue = 2370;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Indigo";
		}

		public MauveIndigoDyeTub(Serial serial)
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
	public class RosePerleDyeTub : DyeTub
	{
		[Constructable]
		public RosePerleDyeTub()
		{
			Hue = DyedHue = 2163;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Rose Perle";
		}

		public RosePerleDyeTub(Serial serial)
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
	public class PruneDyeTub : DyeTub
	{
		[Constructable]
		public PruneDyeTub()
		{
			Hue = DyedHue = 2981;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Prune";
		}

		public PruneDyeTub(Serial serial)
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
	public class MauveVelourDyeTub : DyeTub
	{
		[Constructable]
		public MauveVelourDyeTub()
		{
			Hue = DyedHue = 2074;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Mauve Velour";
		}

		public MauveVelourDyeTub(Serial serial)
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
	public class GrisAntraciteDyeTub : DyeTub
	{
		[Constructable]
		public GrisAntraciteDyeTub()
		{
			Hue = DyedHue = 2119;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Antracite";
		}

		public GrisAntraciteDyeTub(Serial serial)
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
	public class GrisArgenteDyeTub : DyeTub
	{
		[Constructable]
		public GrisArgenteDyeTub()
		{
			Hue = DyedHue = 2796;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Argenté";
		}

		public GrisArgenteDyeTub(Serial serial)
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
	public class NoirDyeTub : DyeTub
	{
		[Constructable]
		public NoirDyeTub()
		{
			Hue = DyedHue = 2051;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Noire";
		}

		public NoirDyeTub(Serial serial)
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

	public class BlancDyeTub : DyeTub
	{
		[Constructable]
		public BlancDyeTub()
		{
			Hue = DyedHue = 2498;
			Redyable = false;
			Charges = 5;
			Name = "Teinture Blanc Pure";
		}

		public BlancDyeTub(Serial serial)
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