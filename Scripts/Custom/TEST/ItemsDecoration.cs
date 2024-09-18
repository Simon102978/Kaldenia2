using System;
using Server;
using Server.Items;

namespace Server.Items
{
	[Flipable(0x18C8, 0x18C9)]
	public class Vaisselle : Item
	{
		[Constructable]
		public Vaisselle() : base(0x18C8)
		{
			Name = "Vaisselle";
			Weight = 5.0;
		}

		public Vaisselle(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	[Flipable(0x18BC, 0x18BD)]
	public class FiolesMultiples : Item
	{
		[Constructable]
		public FiolesMultiples() : base(0x18BC)
		{
			Name = "Fioles Multiples";
			Weight = 2.0;
		}

		public FiolesMultiples(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	[Flipable(0x18C6, 0x18C7)]
	public class ChaudronsMultiples : Item
	{
		[Constructable]
		public ChaudronsMultiples() : base(0x18C6)
		{
			Name = "Chaudrons Multiples";
			Weight = 5.0;
		}

		public ChaudronsMultiples(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	[Flipable(0x18ED, 0x18EE, 0x18EF)]
	public class LivresMultiples : Item
	{
		[Constructable]
		public LivresMultiples() : base(0x18ED)
		{
			Name = "Livres Multiples";
			Weight = 3.0;
		}

		public LivresMultiples(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class PlateauDeCoupes : Item
	{
		[Constructable]
		public PlateauDeCoupes() : base(0x1942)
		{
			Name = "Plateau de Coupes";
			Weight = 3.0;
		}

		public PlateauDeCoupes(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	[Flipable(0x1A9E, 0x1A9F, 0x1AA0, 0x1AA1)]
	public class VignesPendantes : Item
	{
		[Constructable]
		public VignesPendantes() : base(0x1A9E)
		{
			Name = "Vignes Pendantes";
			Weight = 1.0;
		}

		public VignesPendantes(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	[Flipable(0x1A0F, 0x1A10, 0x1A11, 0x1A12, 0x1A13, 0x1A14, 0x1A15, 0x1A16)]
	public class ClotureDeMetal : Item
	{
		[Constructable]
		public ClotureDeMetal() : base(0x1A0F)
		{
			Name = "Cloture de Métal";
			Weight = 10.0;
		}

		public ClotureDeMetal(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	[Flipable(0x187D, 0x188C, 0x188D)]
	public class TableRonde : Item
	{
		[Constructable]
		public TableRonde() : base(0x187D)
		{
			Name = "Table Ronde";
			Weight = 10.0;
		}

		public TableRonde(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	[Flipable(0x178D, 0x178E, 0x178F)]
	public class LampadaireDeRue : Item
	{
		[Constructable]
		public LampadaireDeRue() : base(0x178D)
		{
			Name = "Lampadaire de Rue";
			Weight = 15.0;
		}

		public LampadaireDeRue(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class FourABois : Item
	{
		[Constructable]
		public FourABois() : base(0x178B)
		{
			Name = "Four à Bois";
			Weight = 20.0;
		}

		public FourABois(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	[Flipable(0x188E, 0x188F)]
	public class Vaissellier : Item
	{
		[Constructable]
		public Vaissellier() : base(0x188E)
		{
			Name = "Vaissellier";
			Weight = 15.0;
		}

		public Vaissellier(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class PanierPlein : Item
	{
		[Constructable]
		public PanierPlein() : base(0x1BD0)
		{
			Name = "Panier Plein";
			Weight = 5.0;
		}

		public PanierPlein(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	[Flipable(0x1BDF, 0x1BE2)]
	public class PileDeBois : Item
	{
		[Constructable]
		public PileDeBois() : base(0x1BDF)
		{
			Name = "Pile de Bois";
			Weight = 10.0;
		}

		public PileDeBois(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	[Flipable(0x1E24, 0x1E25, 0x1E23, 0x1E22)]
	public class OutilEcriture : Item
	{
		[Constructable]
		public OutilEcriture() : base(0x1E24)
		{
			Name = "Outil d'Écriture";
			Weight = 1.0;
		}

		public OutilEcriture(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	[Flipable(0x1E60, 0x1E67)]
	public class TropheOursBrun : Item
	{
		[Constructable]
		public TropheOursBrun() : base(0x1E60)
		{
			Name = "Trophé d'Ours Brun";
			Weight = 10.0;
		}

		public TropheOursBrun(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

		[Flipable(0x1E61, 0x1E68)]
		public class TropheeCerf : Item
		{
			[Constructable]
			public TropheeCerf() : base(0x1E61)
			{
				Name = "Trophée de Cerf";
				Weight = 10.0;
			}

			public TropheeCerf(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x1E63, 0x1E6A)]
		public class TropheeTroll : Item
		{
			[Constructable]
			public TropheeTroll() : base(0x1E63)
			{
				Name = "Trophé de Troll";
				Weight = 10.0;
			}

			public TropheeTroll(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x1E62, 0x1E69)]
		public class TropheePeche : Item
		{
			[Constructable]
			public TropheePeche() : base(0x1E62)
			{
				Name = "Trophé de Pêche";
				Weight = 5.0;
			}

			public TropheePeche(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class SceauEau : Item
		{
			[Constructable]
			public SceauEau() : base(0x2004)
			{
				Name = "Sceau d'Eau";
				Weight = 5.0;
			}

			public SceauEau(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x1EDE, 0x1EF2)]
		public class CraneVerre : Item
		{
			[Constructable]
			public CraneVerre() : base(0x1EDE)
			{
				Name = "Crâne en Verre";
				Weight = 5.0;
			}

			public CraneVerre(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x2206, 0x2207, 0x2208, 0x220A, 0x2213, 0x2211, 0x2218)]
		public class CrystalVerre : Item
		{
			[Constructable]
			public CrystalVerre() : base(0x2206)
			{
				Name = "Crystal de Verre";
				Weight = 5.0;
			}

			public CrystalVerre(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x2234, 0x2235)]
		public class TropheeDragon : Item
		{
			[Constructable]
			public TropheeDragon() : base(0x2234)
			{
				Name = "Trophée de Dragon";
				Weight = 15.0;
			}

			public TropheeDragon(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x231A, 0x231B)]
		public class FleursBlanches : Item
		{
			[Constructable]
			public FleursBlanches() : base(0x231A)
			{
				Name = "Fleurs Blanches Suspendues";
				Weight = 1.0;
			}

			public FleursBlanches(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x231C, 0x231D)]
		public class MargueritesSuspendues : Item
		{
			[Constructable]
			public MargueritesSuspendues() : base(0x231C)
			{
				Name = "Marguerites Suspendues";
				Weight = 1.0;
			}

			public MargueritesSuspendues(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x231E, 0x231F)]
		public class FleursRosesSuspendues : Item
		{
			[Constructable]
			public FleursRosesSuspendues() : base(0x231E)
			{
				Name = "Fleurs Roses Suspendues";
				Weight = 1.0;
			}

			public FleursRosesSuspendues(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x2342, 0x2343)]
		public class PetiteArmoire : Container
		{

		public override int DefaultGumpID => 0x4B;
		public override int DefaultDropSound => 0x42;


		[Constructable]
			public PetiteArmoire() : base(0x2342)
			{
				Name = "Petite Armoire";
				Weight = 20.0;
			}

			public PetiteArmoire(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x3066, 0x3067, 0x3068, 0x3069, 0x306A, 0x306B)]
		public class PresentoireVerre : Item
		{
			[Constructable]
			public PresentoireVerre() : base(0x3066)
			{
				Name = "Présentoire de Verre";
				Weight = 10.0;
			}

			public PresentoireVerre(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x3138, 0x3139, 0x313A)]
		public class FleurRose : Item
		{
			[Constructable]
			public FleurRose() : base(0x3138)
			{
				Name = "Fleur Rose";
				Weight = 1.0;
			}

			public FleurRose(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x3C2D, 0x3C2E, 0x3C2F, 0x3C30, 0x3C31, 0x3C32)]
		public class BarilPresentoire : Item
		{
			[Constructable]
			public BarilPresentoire() : base(0x3C2D)
			{
				Name = "Baril Présentoire";
				Weight = 15.0;
			}

			public BarilPresentoire(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x3C33, 0x3C34, 0x3C35, 0x3C36, 0x3C37, 0x3C38, 0x3C39, 0x3C3A)]
		public class BarilPresentoireSol : Item
		{
			[Constructable]
			public BarilPresentoireSol() : base(0x3C33)
			{
				Name = "Baril Présentoire au Sol";
				Weight = 15.0;
			}

			public BarilPresentoireSol(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x3C5E, 0x3C5F, 0x3C60, 0x3C61, 0x3C62, 0x3C63, 0x3C64, 0x3C65, 0x3C66, 0x3C67)]
		public class CaissePresentoire : Item
		{
			[Constructable]
			public CaissePresentoire() : base(0x3C5E)
			{
				Name = "Caisse Présentoire";
				Weight = 15.0;
			}

			public CaissePresentoire(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x3D3B, 0x3D3C, 0x3D3F, 0x3D40)]
		public class CitrouilleDecorative : Item
		{
			[Constructable]
			public CitrouilleDecorative() : base(0x3D3B)
			{
				Name = "Citrouille Décorative";
				Weight = 5.0;
			}

			public CitrouilleDecorative(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x3BE2, 0x3BE3)]
		public class HerbesSuspendues : Item
		{
			[Constructable]
			public HerbesSuspendues() : base(0x3BE2)
			{
				Name = "Herbes Suspendues";
				Weight = 1.0;
			}

			public HerbesSuspendues(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x3BDE, 0x3BDF)]
		public class PorteManteau : Item
		{
			[Constructable]
			public PorteManteau() : base(0x3BDE)
			{
				Name = "Porte Manteau";
				Weight = 10.0;
			}

			public PorteManteau(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
	}

		[Flipable(0x3CAA, 0x3CAB)]
		public class Charette : Item
		{
			[Constructable]
			public Charette() : base(0x3CAA)
			{
				Name = "Charette";
				Weight = 20.0;
			}

			public Charette(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class FleursRougesPot : Item
		{
			[Constructable]
			public FleursRougesPot() : base(0x3D71)
			{
				Name = "Fleurs Rouges en Pot";
				Weight = 5.0;
			}

			public FleursRougesPot(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class FleursRosesPot : Item
		{
			[Constructable]
			public FleursRosesPot() : base(0x3D73)
			{
				Name = "Fleurs Roses en Pot";
				Weight = 5.0;
			}

			public FleursRosesPot(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class PetitesFleursFushiaPot : Item
		{
			[Constructable]
			public PetitesFleursFushiaPot() : base(0x3D76)
			{
				Name = "Petites Fleurs Fushia en Pot";
				Weight = 5.0;
			}

			public PetitesFleursFushiaPot(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class MargueritesPot : Item
		{
			[Constructable]
			public MargueritesPot() : base(0x3D77)
			{
				Name = "Marguerites en Pot";
				Weight = 5.0;
			}

			public MargueritesPot(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class ArbreFleurRougePot : Item
		{
			[Constructable]
			public ArbreFleurRougePot() : base(0x3D79)
			{
				Name = "Arbre à Fleurs Rouges en Pot";
				Weight = 10.0;
			}

			public ArbreFleurRougePot(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class FleurBlancheVase : Item
		{
			[Constructable]
			public FleurBlancheVase() : base(0x3DB8)
			{
				Name = "Fleur Blanche en Vase";
				Weight = 3.0;
			}

			public FleurBlancheVase(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class FleurRoseVase : Item
		{
			[Constructable]
			public FleurRoseVase() : base(0x3DB9)
			{
				Name = "Fleur Rose en Vase";
				Weight = 3.0;
			}

			public FleurRoseVase(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class BrancheVase : Item
		{
			[Constructable]
			public BrancheVase() : base(0x3DBA)
			{
				Name = "Branche en Vase";
				Weight = 3.0;
			}

			public BrancheVase(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x4792, 0x4794)]
		public class VignesPendantesFeuilleLarge : Item
		{
			[Constructable]
			public VignesPendantesFeuilleLarge() : base(0x4792)
			{
				Name = "Vignes Pendantes à Feuilles Larges";
				Weight = 1.0;
			}

			public VignesPendantesFeuilleLarge(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x4793, 0x4795)]
		public class VignesPendantesFeuilleBlanches : Item
		{
			[Constructable]
			public VignesPendantesFeuilleBlanches() : base(0x4793)
			{
				Name = "Vignes Pendantes à Feuilles Blanches";
				Weight = 1.0;
			}

			public VignesPendantesFeuilleBlanches(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x4691, 0x4695, 0x4698)]
		public class CitrouilleDecoupe : Item
		{
			[Constructable]
			public CitrouilleDecoupe() : base(0x4691)
			{
				Name = "Citrouille Découpée";
				Weight = 5.0;
			}

			public CitrouilleDecoupe(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x46A0, 0x46A1, 0x4BD9, 0x4BDA)]
		public class CorneDAbondance : Item
		{
			[Constructable]
			public CorneDAbondance() : base(0x46A0)
			{
				Name = "Corne d'Abondance";
				Weight = 5.0;
			}

			public CorneDAbondance(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x4C80, 0x4C81)]
		public class FauteuilUnePlace : Item
		{
			[Constructable]
			public FauteuilUnePlace() : base(0x4C80)
			{
				Name = "Fauteuil Une Place";
				Weight = 15.0;
			}

			public FauteuilUnePlace(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x4C24, 0x4C25)]
		public class BibliothequeDAlchimie : Item
		{
			[Constructable]
			public BibliothequeDAlchimie() : base(0x4C24)
			{
				Name = "Bibliothèque d'Alchimie";
				Weight = 20.0;
			}

			public BibliothequeDAlchimie(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
	}

		[Flipable(0x4C38, 0x4C39)]
		public class TabletteEnMetal : Item
		{
			[Constructable]
			public TabletteEnMetal() : base(0x4C38)
			{
				Name = "Tablette en Métal";
				Weight = 4.0;
			}

			public TabletteEnMetal(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x4C40, 0x4C41)]
		public class LampeHuileDragon : Item
		{
			[Constructable]
			public LampeHuileDragon() : base(0x4C40)
			{
				Name = "Lampe à l'Huile avec Dragon";
				Weight = 5.0;
				Light = LightType.Circle300;
			}

			public LampeHuileDragon(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9A70, 0x9A66)]
		public class FeuAvecPlaque : Item
		{
			[Constructable]
			public FeuAvecPlaque() : base(0x9A70)
			{
				Name = "Feu avec une Plaque";
				Weight = 6.0;
			}

			public FeuAvecPlaque(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9A81, 0x9A90)]
		public class MachineDeForge : Item
		{
			[Constructable]
			public MachineDeForge() : base(0x9A83)
			{
				Name = "Machine de Forge";
				Weight = 25.0;
			}

			public MachineDeForge(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9C27, 0x9C22)]
		public class MachineDMenuiserie : Item
		{
			[Constructable]
			public MachineDMenuiserie() : base(0x9C27)
			{
				Name = "Machine de Menuiserie";
				Weight = 25.0;
			}

			public MachineDMenuiserie(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9C30, 0x9C3B)]
		public class MachineCreationArc : Item
		{
			[Constructable]
			public MachineCreationArc() : base(0x9C30)
			{
				Name = "Machine de Création d'Arc";
				Weight = 25.0;
			}

			public MachineCreationArc(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9D30, 0x9D31)]
		public class PlateauDeSoupe : Item
		{
			[Constructable]
			public PlateauDeSoupe() : base(0x9D30)
			{
				Name = "Plateau de Soupe";
				Weight = 4.0;
			}

			public PlateauDeSoupe(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class LumiereDeMariage : Item
		{
			[Constructable]
			public LumiereDeMariage() : base(0x9E93)
			{
				Name = "Lumière de Mariage";
				Weight = 10.0;
				Light = LightType.Circle300;
			}

			public LumiereDeMariage(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class FontaineDeChocolat : Item
		{
			[Constructable]
			public FontaineDeChocolat() : base(0x9EB7)
			{
				Name = "Fontaine de Chocolat";
				Weight = 15.0;
			}

			public FontaineDeChocolat(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9EF1, 0x9EF4)]
		public class ChandelierACinqChandelles : Item
		{
			[Constructable]
			public ChandelierACinqChandelles() : base(0x9EF1)
			{
				Name = "Chandelier à 5 Chandelles";
				Weight = 10.0;
				Light = LightType.Circle300;
			}

			public ChandelierACinqChandelles(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0xA004, 0xA005, 0xA006, 0xA007, 0xA008, 0xA009)]
		public class BoiteRessourceDecoration : Item
		{
			[Constructable]
			public BoiteRessourceDecoration() : base(0xA004)
			{
				Name = "Boîte de Ressource Décoration";
				Weight = 5.0;
			}

			public BoiteRessourceDecoration(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0xA012, 0xA013)]
		public class CoffreDore : Container
		{
		public override int DefaultGumpID => 0x4B;
		public override int DefaultDropSound => 0x42;

		[Constructable]
			public CoffreDore() : base(0xA012)
			{
				Name = "Coffre Doré";
				Weight = 10.0;
			}

			public CoffreDore(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class PuitEau : Item
		{
			[Constructable]
			public PuitEau() : base(0x3890)
			{
				Name = "Puits";
				Weight = 15.0;
			}

			public PuitEau(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x1508, 0x151C)]
		public class ChevalierDecoratif : Item
		{
			[Constructable]
			public ChevalierDecoratif() : base(0x1508)
			{
				Name = "Chevalier Décoratif";
				Weight = 20.0;
			}

			public ChevalierDecoratif(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x0E40, 0x0E41)]
		public class CoffreEnFer : Container
	{
		public override int DefaultGumpID => 0x42;
		public override int DefaultDropSound => 0x42;


		[Constructable]
			public CoffreEnFer() : base(0x0E40)
			{
				Name = "Coffre en Fer";
				Weight = 10.0;
			}

			public CoffreEnFer(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class FourALaBroche : Item
		{
			[Constructable]
			public FourALaBroche() : base(0x9989)
			{
				Name = "Four à la Broche";
				Weight = 10.0;
			}

			public FourALaBroche(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class CarreDeFeuDeSol : Item
		{
			[Constructable]
			public CarreDeFeuDeSol() : base(0x29FE)
			{
				Name = "Carré de Feu de Sol";
				Weight = 15.0;
				Light = LightType.Circle300;
			}

			public CarreDeFeuDeSol(Serial serial) : base(serial) { }

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
	}

		[Flipable(0x4C3A, 0x4C3B)]
		public class TabletteEnBois : Item
		{
			[Constructable]
			public TabletteEnBois() : base(0x4C3A)
			{
				Weight = 4.0;
				Name = "Tablette en bois";
			}

			public TabletteEnBois(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x63D6, 0x63DC, 0x63E2, 0x63D9, 0x63DF, 0x63E5)]
		public class SculptureDeChat : Item
		{
			[Constructable]
			public SculptureDeChat() : base(0x63D6)
			{
				Weight = 2.0;
				Name = "Sculpture de chat";
			}

			public SculptureDeChat(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x63E8, 0x63EE, 0x63F4, 0x63F1, 0x63F8)]
		public class SculptureDeChien : Item
		{
			[Constructable]
			public SculptureDeChien() : base(0x63E8)
			{
				Weight = 2.0;
				Name = "Sculpture de chien";
			}

			public SculptureDeChien(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9D88, 0x9D8E)]
		public class GrandeTableAlchimie : Item
		{
			[Constructable]
			public GrandeTableAlchimie() : base(0x9D88)
			{
				Weight = 10.0;
				Name = "Grande table d'alchimie";
			}

			public GrandeTableAlchimie(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9D8C, 0x9D97)]
		public class PetiteTableAlchimiePortative : Item
		{
			[Constructable]
			public PetiteTableAlchimiePortative() : base(0x9D8C)
			{
				Weight = 5.0;
				Name = "Petite table d'alchimie portative";
			}

			public PetiteTableAlchimiePortative(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9E36, 0x9E37)]
		public class BarilSurPied : Item
		{
			[Constructable]
			public BarilSurPied() : base(0x9E36)
			{
				Weight = 7.0;
				Name = "Baril sur pied";
			}

			public BarilSurPied(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9E84, 0x9E85)]
		public class ArmoireEnCoeur : Item
		{
			[Constructable]
			public ArmoireEnCoeur() : base(0x9E84)
			{
				Weight = 10.0;
				Name = "Armoire en coeur";
			}

			public ArmoireEnCoeur(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		public class TableDeMariage : Item
		{
			[Constructable]
			public TableDeMariage() : base(0x9E8D)
			{
				Weight = 10.0;
				Name = "Table de mariage";
			}

			public TableDeMariage(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9E8E, 0x9E8F, 0x9E90, 0x9E91)]
		public class ChaiseDeMariage : Item
		{
			[Constructable]
			public ChaiseDeMariage() : base(0x9E8E)
			{
				Weight = 5.0;
				Name = "Chaise de mariage";
			}

			public ChaiseDeMariage(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9ECE, 0x9ECF, 0x9ED0, 0x9ED1, 0x9ED2, 0x9ED3)]
		public class ComptoireBuffet : Item
		{
			[Constructable]
			public ComptoireBuffet() : base(0x9ECE)
			{
				Weight = 10.0;
				Name = "Comptoire buffet";
			}

			public ComptoireBuffet(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9EE3, 0x9EE4)]
		public class PetiteBibliothequeDeTable : Item
		{
			[Constructable]
			public PetiteBibliothequeDeTable() : base(0x9EE3)
			{
				Weight = 10.0;
				Name = "Petite bibliothèque de table";
			}

			public PetiteBibliothequeDeTable(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9F1C, 0x9F1D)]
		public class PetiteVanite : Item
		{
			[Constructable]
			public PetiteVanite() : base(0x9F1C)
			{
				Weight = 10.0;
				Name = "Petite vanité";
			}

			public PetiteVanite(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9FEC, 0x9FF1)]
		public class BureauDecritureFourni : Item
		{
			[Constructable]
			public BureauDecritureFourni() : base(0x9FEC)
			{
				Weight = 12.0;
				Name = "Bureau d'écriture fourni";
			}

			public BureauDecritureFourni(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0xA019, 0xA01A)]
		public class Cellier : Container
		{

		public override int DefaultGumpID => 0x4B;
		public override int DefaultDropSound => 0x42;


		[Constructable]
			public Cellier() : base(0xA019)
			{
				Weight = 15.0;
				Name = "Cellier";
			}

			public Cellier(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0xA028, 0xA029)]
		public class EtagereAlchimique : Item
		{
			[Constructable]
			public EtagereAlchimique() : base(0xA028)
			{
				Weight = 15.0;
				Name = "Étagère alchimique";
			}

			public EtagereAlchimique(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0xA57C, 0xA57D, 0xA57E, 0xA57F)]
		public class ChaiseElegante : Item
		{
			[Constructable]
			public ChaiseElegante() : base(0xA57C)
			{
				Weight = 6.0;
				Name = "Chaise élégante";
			}

			public ChaiseElegante(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x1534, 0x1533)]
		public class ClotureDeBois : Item
		{
			[Constructable]
			public ClotureDeBois() : base(0x1534)
			{
				Weight = 6.0;
				Name = "Clôture de bois";
			}

			public ClotureDeBois(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

		[Flipable(0x9F33, 0x9F39)]
		public class Epouvantail : Item
		{
			[Constructable]
			public Epouvantail() : base(0x9F33)
			{
				Weight = 12.0;
				Name = "Épouvantail";
			}

			public Epouvantail(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}

			// Tailoring
			[Flipable(0x1627, 0x1628, 0x162A, 0x1629)]
			public class DrapeauxMultiples : Item
			{
				[Constructable]
				public DrapeauxMultiples() : base(0x1627)
				{
					Weight = 6.0;
					Name = "Drapeaux multiples";
				}

				public DrapeauxMultiples(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x15EE, 0x15EF)]
			public class DrapeauRouge : Item
			{
				[Constructable]
				public DrapeauRouge() : base(0x15EE)
				{
					Weight = 6.0;
					Name = "Drapeau rouge";
				}

				public DrapeauRouge(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x295C, 0x295D, 0x295E, 0x295F)]
			public class PetitLitDeSol : Item
			{
				[Constructable]
				public PetitLitDeSol() : base(0x295C)
				{
					Weight = 14.0;
					Name = "Petit lit de sol";
				}

				public PetitLitDeSol(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			// Maçonnerie
			[Flipable(0x9A18, 0x9A19, 0x9A14, 0x9A15)]
			public class EvierEnMarbre : Item
			{
				[Constructable]
				public EvierEnMarbre() : base(0x9A18)
				{
					Weight = 5.0;
					Name = "Évier en marbre";
				}

				public EvierEnMarbre(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x9CA1, 0x9CA4)]
			public class PetiteFontaine : Item
			{
				[Constructable]
				public PetiteFontaine() : base(0x9CA1)
				{
					Weight = 5.0;
					Name = "Petite fontaine";
				}

				public PetiteFontaine(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x9D98, 0x9D9E)]
			public class FourDeBriqueEteint : Item
			{
				[Constructable]
				public FourDeBriqueEteint() : base(0x9D98)
				{
					Weight = 6.0;
					Name = "Four de brique éteint";
				}

				public FourDeBriqueEteint(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x9D99, 0x9D9F)]
			public class FourDeBriqueAllume : Item
			{
				[Constructable]
				public FourDeBriqueAllume() : base(0x9D99)
				{
					Weight = 6.0;
					Name = "Four de brique allumé";
				}

				public FourDeBriqueAllume(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			// Botanique
			[Flipable(0x9D2C, 0x9D2B, 0x9D2D)]
			public class ImmenseCitrouille : Item
			{
				[Constructable]
				public ImmenseCitrouille() : base(0x9D2C)
				{
					Weight = 10.0;
					Name = "Immense citrouille";
				}

				public ImmenseCitrouille(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			public class CitrouilleDecorative1 : Item
			{
				[Constructable]
				public CitrouilleDecorative1() : base(0x9F51)
				{
					Weight = 4.0;
					Name = "Citrouille décorative";
				}

				public CitrouilleDecorative1(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x9F23, 0x9F24, 0x9F27, 0x9F28)]
			public class CitrouilleSculptee : Item
			{
				[Constructable]
				public CitrouilleSculptee() : base(0x9F23)
				{
					Weight = 5.0;
					Name = "Citrouille sculptée";
				}

				public CitrouilleSculptee(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			public class FleurSolitaireEnVase : Item
			{
				[Constructable]
				public FleurSolitaireEnVase() : base(0x08FE)
				{
					Weight = 2.0;
					Name = "Une fleur solitaire en vase";
				}

				public FleurSolitaireEnVase(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			public class PetitBouquetEnVase : Item
			{
				[Constructable]
				public PetitBouquetEnVase() : base(0x08FF)
				{
					Weight = 2.0;
					Name = "Un petit bouquet en vase";
				}

				public PetitBouquetEnVase(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x0CD1, 0x0CD4, 0x0CDE, 0x0D02)]
			public class Feuilles : Item
			{
				[Constructable]
				public Feuilles() : base(0x0CD1)
				{
					Weight = 4.0;
					Name = "Feuilles";
				}

				public Feuilles(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x0C8F, 0x0C90, 0x0C91, 0x0C92)]
			public class HaieDeCedre : Item
			{
				[Constructable]
				public HaieDeCedre() : base(0x0C8F)
				{
					Weight = 4.0;
					Name = "Haie de cèdre";
				}

				public HaieDeCedre(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x0C83, 0x0C86, 0x0C87, 0x0C89, 0x0C8E, 0x0CBE, 0x0CC1)]
			public class FleursAuSol : Item
			{
				[Constructable]
				public FleursAuSol() : base(0x0C83)
				{
					Weight = 2.0;
					Name = "Fleurs au sol";
				}

				public FleursAuSol(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x1F0D, 0x1F0E, 0x1F0F, 0x1F10, 0x1F11, 0x1F12)]
			public class FeuillesAuSol : Item
			{
				[Constructable]
				public FeuillesAuSol() : base(0x1F0D)
				{
					Weight = 2.0;
					Name = "Feuilles au sol";
				}

				public FeuillesAuSol(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x9967, 0x9968)]
			public class PlanteCarnivor : Item
			{
				[Constructable]
				public PlanteCarnivor() : base(0x9967)
				{
					Weight = 3.0;
					Name = "Plante Carnivore";
				}

				public PlanteCarnivor(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x3CAD, 0x3CAC)]
			public class FeuillageAuSol : Item
			{
				[Constructable]
				public FeuillageAuSol() : base(0x3CAD)
				{
					Weight = 4.0;
					Name = "Feuillage au sol";
				}

				public FeuillageAuSol(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x28DC, 0x28DD, 0x28DE)]
			public class PetitBonzaiVert : Item
			{
				[Constructable]
				public PetitBonzaiVert() : base(0x28DC)
				{
					Weight = 2.0;
					Name = "Petit bonzai vert";
				}

				public PetitBonzaiVert(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();
				}
			}

			[Flipable(0x28DF, 0x28E0, 0x28E1)]
			public class PetitBonzaiRose : Item
			{
				[Constructable]
				public PetitBonzaiRose() : base(0x28DF)
				{
					Weight = 2.0;
					Name = "Petit bonzai rose";
				}

				public PetitBonzaiRose(Serial serial) : base(serial) { }

				public override void Serialize(GenericWriter writer)
				{
					base.Serialize(writer);
					writer.Write(0);
				}

				public override void Deserialize(GenericReader reader)
				{
					base.Deserialize(reader);
					int version = reader.ReadInt();

        }
	}

// Cuisine
				[Flipable(0x9EB0, 0x9EB1, 0x9EB2, 0x9EB3, 0x9EB4, 0x9EB5)]
			public class GateauDeMariage : Item
{
				[Constructable]
				public GateauDeMariage() : base(0x9EB0)
				{
					Weight = 2.0;
					Name = "Gâteau de Mariage";
				}

	public GateauDeMariage(Serial serial) : base(serial) { }

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write(0);
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
	}
}

[Flipable(0x9EF5, 0x9EF6)]
public class PartDeGateau : Item
{
	[Constructable]
	public PartDeGateau() : base(0x9EF5)
	{
		Weight = 1.0;
		Name = "Part de gâteau";
	}

	public PartDeGateau(Serial serial) : base(serial) { }

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write(0);
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
	}
}

// Soufflage de verre
[Flipable(0x3892, 0x389D, 0x38A9, 0x38C4, 0x38B8, 0x38AD)]
public class AquariumEnVerre : Item
{
	[Constructable]
	public AquariumEnVerre() : base(0x3892)
	{
		Weight = 10.0;
		Name = "Aquarium en verre";
	}

	public AquariumEnVerre(Serial serial) : base(serial) { }

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write(0);
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
	}
}

// Carpentry
[Flipable(0x4C80, 0x4C81)]
public class FauteuilUnePlace1 : Item
{
	[Constructable]
	public FauteuilUnePlace1() : base(0x4C80)
	{
		Weight = 5.0;
		Name = "Fauteuil une place";
	}

	public FauteuilUnePlace1(Serial serial) : base(serial) { }

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write(0);
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
	}
}
}




