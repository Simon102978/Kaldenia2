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
	}


