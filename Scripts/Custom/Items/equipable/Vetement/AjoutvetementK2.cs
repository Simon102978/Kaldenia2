using Server.Engines.Craft;
namespace Server.Items
{
	public class ChemiseDecoree : BaseMiddleTorso
	{
		[Constructable]
		public ChemiseDecoree() : this(0)
		{
		}

		[Constructable]
		public ChemiseDecoree(int hue) : base(0xA4D8, hue)
		{
			Weight = 2.0;
			Name = "Chemise Décorée";
		}

		public ChemiseDecoree(Serial serial) : base(serial)
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

	public class ManteauBourgeois : BaseOuterTorso
	{
		[Constructable]
		public ManteauBourgeois() : this(0)
		{
		}

		[Constructable]
		public ManteauBourgeois(int hue) : base(0xA4D9, hue)
		{
			Weight = 3.0;
			Name = "Manteau Bourgeois";
		}

		public ManteauBourgeois(Serial serial) : base(serial)
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

	public class ManteauVoyageur : BaseOuterTorso
	{
		[Constructable]
		public ManteauVoyageur() : this(0)
		{
		}

		[Constructable]
		public ManteauVoyageur(int hue) : base(0xA4DA, hue)
		{
			Weight = 3.0;
			Name = "Manteau Voyageur";
		}

		public ManteauVoyageur(Serial serial) : base(serial)
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

	public class PareoTissuSimple : BaseOuterLegs
	{
		[Constructable]
		public PareoTissuSimple() : this(0)
		{
		}

		[Constructable]
		public PareoTissuSimple(int hue) : base(0xA4DB, hue)
		{
			Weight = 1.0;
			Name = "Paréo en Tissu Simple";
		}

		public PareoTissuSimple(Serial serial) : base(serial)
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

	public class RobeVoyage : BaseOuterTorso
	{
		[Constructable]
		public RobeVoyage() : this(0)
		{
		}

		[Constructable]
		public RobeVoyage(int hue) : base(0xA4DC, hue)
		{
			Weight = 3.0;
			Name = "Robe de Voyage";
		}

		public RobeVoyage(Serial serial) : base(serial)
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

	public class SoutienGorgeEchancre : BasePants
	{
		[Constructable]
		public SoutienGorgeEchancre() : this(0)
		{
		}

		[Constructable]
		public SoutienGorgeEchancre(int hue) : base(0xA4DD, hue)
		{
			Weight = 0.5;
			Name = "Soutien-Gorge Échancré";
		}

		public SoutienGorgeEchancre(Serial serial) : base(serial)
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

	public class SoutienGorgeTissu : BasePants
	{
		[Constructable]
		public SoutienGorgeTissu() : this(0)
		{
		}

		[Constructable]
		public SoutienGorgeTissu(int hue) : base(0xA4DE, hue)
		{
			Weight = 0.5;
			Name = "Soutien-Gorge en Tissu";
		}

		public SoutienGorgeTissu(Serial serial) : base(serial)
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

	public class TuniqueDoree : BaseOuterTorso
	{
		[Constructable]
		public TuniqueDoree() : this(0)
		{
		}

		[Constructable]
		public TuniqueDoree(int hue) : base(0xA4DF, hue)
		{
			Weight = 2.5;
			Name = "Tunique Dorée";
		}

		public TuniqueDoree(Serial serial) : base(serial)
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

	public class TuniqueCombat : BaseOuterTorso
	{
		[Constructable]
		public TuniqueCombat() : this(0)
		{
		}

		[Constructable]
		public TuniqueCombat(int hue) : base(0xA4E0, hue)
		{
			Weight = 3.0;
			Name = "Tunique de Combat";
		}

		public TuniqueCombat(Serial serial) : base(serial)
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

	public class RobeVoyage2 : BaseOuterTorso
	{
		[Constructable]
		public RobeVoyage2() : this(0)
		{
		}

		[Constructable]
		public RobeVoyage2(int hue) : base(0xA4E1, hue)
		{
			Weight = 3.0;
			Name = "Robe de Voyage";
		}

		public RobeVoyage2(Serial serial) : base(serial)
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

	public class Corsage : BaseMiddleTorso
	{
		[Constructable]
		public Corsage() : this(0)
		{
		}

		[Constructable]
		public Corsage(int hue) : base(0xA4E2, hue)
		{
			Weight = 1.0;
			Name = "Corsage";
		}

		public Corsage(Serial serial) : base(serial)
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