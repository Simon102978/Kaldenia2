using System;

namespace Server.Items
{
	[Furniture]
[Flipable(0xA5C0, 0xA5C1)]
public class MachineCoudre : CraftableFurniture
{
	[Constructable]
	public MachineCoudre()
		: base(0xA5C0)
	{
		Weight = 15.0;
		Name = "Machine à Coudre";
	}

	public MachineCoudre(Serial serial)
		: base(serial)
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

[Furniture]
[Flipable(0xA5C2, 0xA5C2)]
public class BancMachineCoudre : CraftableFurniture
{
	[Constructable]
	public BancMachineCoudre()
		: base(0xA5C2)
	{
		Weight = 5.0;
		Name = "Banc Machine à Coudre";
	}

	public BancMachineCoudre(Serial serial)
		: base(serial)
	{
	}
		public override int LabelNumber => 1015082;  // Wooden Throne
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

[Furniture]
[Flipable(0xA5C5, 0xA5E3)]
public class TinkerTable : CraftableFurniture
{
	[Constructable]
	public TinkerTable()
		: base(0xA5C5)
	{
		Weight = 5.0;
		Name = "Bureau de travail";
	}

	public TinkerTable(Serial serial)
		: base(serial)
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
	public class Tableronde1 : CraftableFurniture
	{
		[Constructable]
		public Tableronde1()
			: base(0xA587)
		{
			Weight = 5.0;
			Name = "Table Ronde";
		}

		public Tableronde1(Serial serial)
			: base(serial)
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

	public class TableRonde2 : CraftableFurniture
	{
		[Constructable]
		public TableRonde2()
			: base(0xA588)
		{
			Weight = 5.0;
			Name = "Table Ronde";
		}

		public TableRonde2(Serial serial)
			: base(serial)
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

	public class TableRonde3 : CraftableFurniture
	{
		[Constructable]
		public TableRonde3()
			: base(0xA589)
		{
			Weight = 5.0;
			Name = "Table Ronde";
		}

		public TableRonde3(Serial serial)
			: base(serial)
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
	[Flipable(0xA58B, 0xA58C)]
	public class TableGrise : CraftableFurniture
	{
		[Constructable]
		public TableGrise()
			: base(0xA58B)
		{
			Weight = 5.0;
			Name = "Table Grise";
		}

		public TableGrise(Serial serial)
			: base(serial)
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
	[Furniture]
[Flipable(0xA024, 0xA025)]
public class RepairTable : CraftableFurniture
{
	[Constructable]
	public RepairTable()
		: base(0xA024)
	{
		Weight = 5.0;
		Name = "Bureau de travail";
	}

	public RepairTable(Serial serial)
		: base(serial)
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
[Furniture]
	[Flipable(0xA026, 0xA027)]
	public class FeedingThrough : CraftableFurniture
	{
		[Constructable]
		public FeedingThrough()
			: base(0xA026)
		{
			Weight = 5.0;
			Name = "Mangeoir";
		}

		public FeedingThrough(Serial serial)
			: base(serial)
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
[Furniture]
[Flipable(0xA5EA, 0xA5EB)]
public class Puit : CraftableFurniture, IWaterSource
	{
	[Constructable]
	public Puit()
		: base(0xA5EA)
	{
		Weight = 100.0;
		Name = "Un Puit";
	}

	public Puit(Serial serial)
		: base(serial)
	{
	}
		public int Quantity
		{
			get
			{
				return 500;
			}
			set
			{
			}
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
	[Flipable(0xA58F, 0xA590, 0xA591, 0xA592)]
	public class NormDresser : CraftableFurniture
	{
		[Constructable]
		public NormDresser()
			: base(0xA58F)
		{
			Weight = 15.0;
			Name = "Coiffeuse";
		}

		public NormDresser(Serial serial)
			: base(serial)
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

	[Flipable(0xA585, 0xA586)]
	public class CommodeFoncee : CraftableFurniture
	{
		[Constructable]
		public CommodeFoncee()
			: base(0xA585)
		{
			Weight = 15.0;
			Name = "Commode Foncee";
		}

		public CommodeFoncee(Serial serial)
			: base(serial)
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

	[Flipable(0xA58D, 0xA58E)]
	public class CommodeHaute : CraftableFurniture
	{
		[Constructable]
		public CommodeHaute()
			: base(0xA58D)
		{
			Weight = 15.0;
			Name = "Commode Haute";
		}

		public CommodeHaute(Serial serial)
			: base(serial)
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
	[Flipable(0xA58F, 0xA591)]
	public class GardeRobeFermer  : CraftableFurniture
	{
		[Constructable]
		public GardeRobeFermer()
			: base(0xA58F)
		{
			Weight = 15.0;
			Name = "Garde Robe Fermé";
		}

		public GardeRobeFermer(Serial serial)
			: base(serial)
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
	[Flipable(0xA590, 0xA592)]
	public class GardeRobeOuvert : CraftableFurniture
	{
		[Constructable]
		public GardeRobeOuvert()
			: base(0xA590)
		{
			Weight = 15.0;
			Name = "Garde Robe Ouvert";
		}

		public GardeRobeOuvert(Serial serial)
			: base(serial)
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
	[Furniture]
[Flipable(0xA018, 0xA018)]
public class Ancre : CraftableFurniture
{
	[Constructable]
	public Ancre()
		: base(0xA018)
	{
		Weight = 25.0;
		Name = "Une Ancre";
	}

	public Ancre(Serial serial)
		: base(serial)
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
	[Furniture]
	[Flipable(0xA574, 0xA575, 0xA576, 0xA577)]
	public class ChaiseLuxe : CraftableFurniture
	{
		[Constructable]
		public ChaiseLuxe()
			: base(0xA574)
		{
			Weight = 25.0;
			Name = "Chaise de Luxe";
		}

		public ChaiseLuxe(Serial serial)
			: base(serial)
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
	[Furniture]
	[Flipable(0xA578, 0xA579)]
	public class BancGris : CraftableFurniture
	{
		[Constructable]
		public BancGris()
			: base(0xA578)
		{
			Weight = 25.0;
			Name = "Banc Gris";
		}

		public BancGris(Serial serial)
			: base(serial)
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
	[Furniture]
	[Flipable(0xA57A, 0xA57B)]
	public class BancFer : CraftableFurniture
	{
		[Constructable]
		public BancFer()
			: base(0xA57A)
		{
			Weight = 25.0;
			Name = "Banc Fer";
		}

		public BancFer(Serial serial)
			: base(serial)
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
	[Furniture]
	[Flipable(0xA57C, 0xA57D, 0xA57E, 0xA57F)]
	public class ChaiseRembourer : CraftableFurniture
	{
		[Constructable]
		public ChaiseRembourer()
			: base(0xA57C)
		{
			Weight = 25.0;
			Name = "Chaise Rembourée";
		}

		public ChaiseRembourer(Serial serial)
			: base(serial)
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
	[Furniture]
	[Flipable(0xA580, 0xA581, 0xA582, 0xA583)]
	public class ChaiseVerte : CraftableFurniture
	{
		[Constructable]
		public ChaiseVerte()
			: base(0xA580)
		{
			Weight = 25.0;
			Name = "Chaise Verte";
		}

		public ChaiseVerte(Serial serial)
			: base(serial)
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
	[Furniture]
[Flipable(0xA019, 0xA01A)]
public class RackaVin : CraftableFurniture
{
	[Constructable]
	public RackaVin()
		: base(0xA019)
	{
		Weight = 25.0;
		Name = "Cellier";
	}

	public RackaVin(Serial serial)
		: base(serial)
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
	[Furniture]
	[Flipable(0xA609, 0xA60A)]
	public class PresentoireVide : CraftableFurniture
	{
		[Constructable]
		public PresentoireVide()
			: base(0xA609)
		{
			Weight = 25.0;
			Name = "Presentoir Vide";
		}

		public PresentoireVide(Serial serial)
			: base(serial)
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
	[Furniture]
	[Flipable(0xA5D2, 0xA5D3)]
	public class PresentoirePlein1 : CraftableFurniture
	{
		[Constructable]
		public PresentoirePlein1()
			: base(0xA5D2)
		{
			Weight = 25.0;
			Name = "Presentoir Plein 1";
		}

		public PresentoirePlein1(Serial serial)
			: base(serial)
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
	[Furniture]
	[Flipable(0xA5D4, 0xA5D5)]
	public class PresentoirePlein2 : CraftableFurniture
	{
		[Constructable]
		public PresentoirePlein2()
			: base(0xA5D4)
		{
			Weight = 25.0;
			Name = "Presentoir Plein 2";
		}

		public PresentoirePlein2(Serial serial)
			: base(serial)
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

	[Furniture]
	[Flipable(0xA5D0, 0xA5D1, 0xA601, 0xA602)]
	public class TableApothicaire  : FurnitureContainer
	{
		[Constructable]
		public TableApothicaire()
			: base(0xA5D0)
		{
			Weight = 25.0;
			Name = "Table Apothicaire";
		}

		public TableApothicaire(Serial serial)
			: base(serial)
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
	[Furniture]
[Flipable(0xA028, 0xA029)]
public class RangementAlchimie : FurnitureContainer
	{
	[Constructable]
	public RangementAlchimie()
		: base(0xA028)
	{
		Weight = 25.0;
		Name = "Armoire Alchimiste";
	}

	public RangementAlchimie(Serial serial)
		: base(serial)
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
	[Furniture]
	[Flipable(0x9F52, 0x9F53, 0x9F54, 0x9F58, 0x9F59, 0x9F5A)]
	public class TableBrasseur : CraftableFurniture
	{
		[Constructable]
		public TableBrasseur()
			: base(0x9F52)
		{
			Weight = 10.0;
			Name = "Table de Brasseur";
		}

		public TableBrasseur(Serial serial)
			: base(serial)
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

	[Flipable(0x1945, 0x1946)]
	public class Paravent : CraftableFurniture
	{
		[Constructable]
		public Paravent()
			: base(0x1945)
		{
			Weight = 10.0;
			Name = "Paravent de bois";
		}

		public Paravent(Serial serial)
			: base(serial)
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
	[Flipable(0xA5C6, 0xA5C7)]
	public class ItemAlchimie : CraftableFurniture
	{
		[Constructable]
		public ItemAlchimie()
			: base(0xA5C6)
		{
			Weight = 10.0;
			Name = "Nécessaire d'alchimie";
		}

		public ItemAlchimie(Serial serial)
			: base(serial)
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
	public class GoldPile1 : CraftableFurniture
	{
		[Constructable]
		public GoldPile1()
			: base(0xA5F3)
		{
			Weight = 15.0;
			Name = "Trésor";
			Movable = false;
		}

		public GoldPile1(Serial serial)
			: base(serial)
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

		internal void MoveToWorld()
		{
			throw new NotImplementedException();
		}
	}
	public class GoldPile2 : CraftableFurniture
	{
		[Constructable]
		public GoldPile2()
			: base(0xA5F4)
		{
			Weight = 15.0;
			Name = "Trésor";
			Movable = false;
		}

		public GoldPile2(Serial serial)
			: base(serial)
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
	public class PoteauChaine : CraftableFurniture
	{
		[Constructable]
		public PoteauChaine()
			: base(0x166D)
		{
			Weight = 15.0;
			Name = "Poteau avec chaines";
			
		}

		public PoteauChaine(Serial serial)
			: base(serial)
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

	public class BacVide : Item
	{
		[Constructable]
		public BacVide()
			: base(0x0E83)
		{
			Weight = 1.0;
			Name = "Un bac Vide";

		}

		public BacVide(Serial serial)
			: base(serial)
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
}