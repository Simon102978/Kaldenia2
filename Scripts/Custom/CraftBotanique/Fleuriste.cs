namespace Server.Items
{
	public class Potdefleur : Item
{
	[Constructable]
	public Potdefleur() : base(0x11C6)
	{
		Weight = 1.0;
		Name = "Plantes en Pot";
	}

	public Potdefleur(Serial serial) : base(serial) { }

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

public class LargePotdeFleur : Item
{
	[Constructable]
	public LargePotdeFleur() : base(0x11C7)
	{
		Weight = 2.0;
		Name = "Grand pot de fleur";
	}

	public LargePotdeFleur(Serial serial) : base(serial) { }

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

public class Arbrenepot : Item
{
	[Constructable]
	public Arbrenepot() : base(0x11C8)
	{
		Weight = 3.0;
		Name = "Arbre en pot";
	}

	public Arbrenepot(Serial serial) : base(serial) { }

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

public class LargeArbreenpot : Item
{
	[Constructable]
	public LargeArbreenpot() : base(0x11C9)
	{
		Weight = 4.0;
		Name = "Grand arbre en pot";
	}

	public LargeArbreenpot(Serial serial) : base(serial) { }

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

public class PotdeFleur1 : Item
{
	[Constructable]
	public PotdeFleur1() : base(0x11CA)
	{
		Weight = 1.0;
		Name = "Pot de fleur 1";
	}

	public PotdeFleur1(Serial serial) : base(serial) { }

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

public class Potdefleur2 : Item
{
	[Constructable]
	public Potdefleur2() : base(0x11CB)
	{
		Weight = 1.0;
		Name = "Pot de fleur 2";
	}

	public Potdefleur2(Serial serial) : base(serial) { }

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

public class PotdeFleur3 : Item
{
	[Constructable]
	public PotdeFleur3() : base(0x11CC)
	{
		Weight = 1.0;
		Name = "Pot de fleur 3";
	}

	public PotdeFleur3(Serial serial) : base(serial) { }

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

public class PotArbreMort1 : Item
{
	[Constructable]
	public PotArbreMort1() : base(0x42B9)
	{
		Weight = 2.0;
		Name = "Pot avec arbre mort 1";
	}

	public PotArbreMort1(Serial serial) : base(serial) { }

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

public class PotArbreMort2 : Item
{
	[Constructable]
	public PotArbreMort2() : base(0x42BA)
	{
		Weight = 2.0;
		Name = "Pot avec arbre mort 2";
	}

	public PotArbreMort2(Serial serial) : base(serial) { }

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

public class FleurMariage1 : Item
{
	[Constructable]
	public FleurMariage1() : base(0x9EA3)
	{
		Weight = 1.0;
		Name = "Fleur de mariage 1";
	}

	public FleurMariage1(Serial serial) : base(serial) { }

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

public class FleurMariage2 : Item
{
	[Constructable]
	public FleurMariage2() : base(0x9EA4)
	{
		Weight = 1.0;
		Name = "Fleur de mariage 2";
	}

	public FleurMariage2(Serial serial) : base(serial) { }

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

public class FleurMariage3 : Item
{
	[Constructable]
	public FleurMariage3() : base(0x4C0B)
	{
		Weight = 1.0;
		Name = "Fleur de mariage 3";
	}

	public FleurMariage3(Serial serial) : base(serial) { }

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

[Flipable(0x9EA5, 0x9EA6, 0x9EA7, 0x9EA8, 0x9EA9)]
public class ArcheMariageSouth1 : Item
{
	[Constructable]
	public ArcheMariageSouth1() : base(0x9EA5)
	{
		Weight = 10.0;
		Name = "Arche de mariage Sud 1";
	}

	public ArcheMariageSouth1(Serial serial) : base(serial) { }

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

[Flipable(0x9E98, 0x9E99, 0x9E9A, 0x9E9B)]
public class ArcheMariageSouth2 : Item
{
	[Constructable]
	public ArcheMariageSouth2() : base(0x9E98)
	{
		Weight = 10.0;
		Name = "Arche de mariage Sud 2";
	}

	public ArcheMariageSouth2(Serial serial) : base(serial) { }

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

[Flipable(0x9E89, 0x9E8A, 0x9E8B, 0x9E8C)]
public class ArcheMariageEast1 : Item
{
	[Constructable]
	public ArcheMariageEast1() : base(0x9E89)
	{
		Weight = 10.0;
		Name = "Arche de mariage Est 1";
	}

	public ArcheMariageEast1(Serial serial) : base(serial) { }

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

[Flipable(0x9EC9, 0x9ECA, 0x9ECB, 0x9ECC, 0x9ECD)]
public class ArcheMariageEast2 : Item
{
	[Constructable]
	public ArcheMariageEast2() : base(0x9EC9)
	{
		Weight = 10.0;
		Name = "Arche de mariage Est 2";
	}

	public ArcheMariageEast2(Serial serial) : base(serial) { }

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

[Flipable(0x9EC5, 0x9EC6, 0x9EC7, 0x9EC8, 0x9ED7, 0x9ED8, 0x9ED9, 0x9EDA)]
public class ArchFeuilles : Item
{
	[Constructable]
	public ArchFeuilles() : base(0x9EC5)
	{
		Weight = 8.0;
		Name = "Arche de feuilles";
	}

	public ArchFeuilles(Serial serial) : base(serial) { }

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

public class Bonsai1 : Item
{
	[Constructable]
	public Bonsai1() : base(0xA021)
	{
		Weight = 2.0;
		Name = "Bonsai 1";
	}

	public Bonsai1(Serial serial) : base(serial) { }

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

public class Bonsai2 : Item
{
	[Constructable]
	public Bonsai2() : base(0xA022)
	{
		Weight = 2.0;
		Name = "Bonsai 2";
	}

	public Bonsai2(Serial serial) : base(serial) { }

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

public class Bonsai3 : Item
{
	[Constructable]
	public Bonsai3() : base(0xA023)
	{
		Weight = 2.0;
		Name = "Bonsai 3";
	}

	public Bonsai3(Serial serial) : base(serial) { }

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

public class PlantePiquante : Item
{
	[Constructable]
	public PlantePiquante() : base(0xA562)
	{
		Weight = 1.0;
		Name = "Plante piquante";
	}

	public PlantePiquante(Serial serial) : base(serial) { }

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

[Flipable(0xA563, 0xA563)]
public class SoleilMural : Item
{
	[Constructable]
	public SoleilMural() : base(0xA563)
	{
		Weight = 5.0;
		Name = "Soleil mural";
	}

	public SoleilMural(Serial serial) : base(serial) { }

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

[Flipable(0xA593, 0xA594)]
public class Monstera : Item
{
	[Constructable]
	public Monstera() : base(0xA593)
	{
		Weight = 3.0;
		Name = "Monstera";
	}

	public Monstera(Serial serial) : base(serial) { }

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

public class BacVideTerre : Item
{
	[Constructable]
	public BacVideTerre() : base(0xA595)
	{
		Weight = 1.0;
		Name = "Bac Vide";
	}

	public BacVideTerre(Serial serial) : base(serial) { }

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

public class BacVideSable : Item
{
	[Constructable]
	public BacVideSable() : base(0xA596)
	{
		Weight = 1.0;
		Name = "Bac Vide Sable";
	}

	public BacVideSable(Serial serial) : base(serial) { }

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

public class BacArbre : Item
{
	[Constructable]
	public BacArbre() : base(0xA597)
	{
		Weight = 1.0;
		Name = "Bac Arbre";
	}

	public BacArbre(Serial serial) : base(serial) { }

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

public class Cactus1 : Item
{
	[Constructable]
	public Cactus1() : base(0xA598)
	{
		Weight = 1.0;
		Name = "Cactus 1";
	}

	public Cactus1(Serial serial) : base(serial) { }

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

public class Cactus2 : Item
{
	[Constructable]
	public Cactus2() : base(0xA599)
	{
		Weight = 1.0;
		Name = "Cactus 2";
	}

	public Cactus2(Serial serial) : base(serial) { }

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

public class Cactus3 : Item
{
	[Constructable]
	public Cactus3() : base(0xA59A)
	{
		Weight = 1.0;
		Name = "Cactus 3";
	}

	public Cactus3(Serial serial) : base(serial) { }

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

public class Cactus4 : Item
{
	[Constructable]
	public Cactus4() : base(0xA59B)
	{
		Weight = 1.0;
		Name = "Cactus 4";
	}

	public Cactus4(Serial serial) : base(serial) { }

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

public class Cactus5 : Item
{
	[Constructable]
	public Cactus5() : base(0xA59C)
	{
		Weight = 1.0;
		Name = "Cactus 5";
	}

	public Cactus5(Serial serial) : base(serial) { }

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

public class Cactus6 : Item
{
	[Constructable]
	public Cactus6() : base(0xA59D)
	{
		Weight = 1.0;
		Name = "Cactus 6";
	}

	public Cactus6(Serial serial) : base(serial) { }

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

public class BacArbutre1 : Item
{
	[Constructable]
	public BacArbutre1() : base(0xA59D)
	{
		Weight = 1.0;
		Name = "Bac Arbuste 1";
	}

	public BacArbutre1(Serial serial) : base(serial) { }

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

public class BacArbutre2 : Item
{
	[Constructable]
	public BacArbutre2() : base(0xA59F)
	{
		Weight = 1.0;
		Name = "Bac Arbuste 2";
	}

	public BacArbutre2(Serial serial) : base(serial) { }

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

public class BacArbutre3 : Item
{
	[Constructable]
	public BacArbutre3() : base(0xA5A0)
	{
		Weight = 1.0;
		Name = "Bac Arbuste 3";
	}

	public BacArbutre3(Serial serial) : base(serial) { }

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

public class BacArbutre4 : Item
{
	[Constructable]
	public BacArbutre4() : base(0xA5A1)
	{
		Weight = 1.0;
		Name = "Bac Arbuste 4";
	}

	public BacArbutre4(Serial serial) : base(serial) { }

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

public class BacArbutre5 : Item
{
	[Constructable]
	public BacArbutre5() : base(0xA5A2)
	{
		Weight = 1.0;
		Name = "Bac Arbuste 5";
	}

	public BacArbutre5(Serial serial) : base(serial) { }

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

public class BacArbutre6 : Item
{
	[Constructable]
	public BacArbutre6() : base(0xA5A3)
	{
		Weight = 1.0;
		Name = "Bac Arbuste 6";
	}

	public BacArbutre6(Serial serial) : base(serial) { }

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

public class BacArbutre7 : Item
{
	[Constructable]
	public BacArbutre7() : base(0xA5A4)
	{
		Weight = 1.0;
		Name = "Bac Arbuste 7";
	}

	public BacArbutre7(Serial serial) : base(serial) { }

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

[Flipable(0x9A20, 0x9A21, 0x9A22, 0x9A23)]
public class BacArbutre8 : Item
{
	[Constructable]
	public BacArbutre8() : base(0x9A20)
	{
		Weight = 1.0;
		Name = "Bac Arbuste 8";
	}

	public BacArbutre8(Serial serial) : base(serial) { }

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

[Flipable(0xA5A5, 0xA5A6, 0xA5A7, 0xA5A8)]
public class CrochetPlante : Item
{
	[Constructable]
	public CrochetPlante() : base(0xA5A5)
	{
		Weight = 1.0;
		Name = "Crochet Plante";
	}

	public CrochetPlante(Serial serial) : base(serial) { }

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

public class Plantesuspendue1 : Item
{
	[Constructable]
	public Plantesuspendue1() : base(0xA5A9)
	{
		Weight = 1.0;
		Name = "Plante suspendue 1";
	}

	public Plantesuspendue1(Serial serial) : base(serial) { }

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

public class Plantesuspendue2 : Item
{
	[Constructable]
	public Plantesuspendue2() : base(0xA5AB)
	{
		Weight = 1.0;
		Name = "Plante suspendue 2";
	}

	public Plantesuspendue2(Serial serial) : base(serial) { }

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

public class Plantesuspendue3 : Item
{
	[Constructable]
	public Plantesuspendue3() : base(0xA5AC)
	{
		Weight = 1.0;
		Name = "Plante suspendue 3";
	}

	public Plantesuspendue3(Serial serial) : base(serial) { }

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

public class Plantesuspendue4 : Item
{
	[Constructable]
	public Plantesuspendue4() : base(0xA5AD)
	{
		Weight = 1.0;
		Name = "Plante suspendue 4";
	}

	public Plantesuspendue4(Serial serial) : base(serial) { }

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

public class Plantesuspendue5 : Item
{
	[Constructable]
	public Plantesuspendue5() : base(0xA5AA)
	{
		Weight = 1.0;
		Name = "Plante suspendue 5";
	}

	public Plantesuspendue5(Serial serial) : base(serial) { }

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

public class Plantesuspendue6 : Item
{
	[Constructable]
	public Plantesuspendue6() : base(0xA5AE)
	{
		Weight = 1.0;
		Name = "Plante suspendue 6";
	}

	public Plantesuspendue6(Serial serial) : base(serial) { }

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

public class Plantesuspendue7 : Item
{
	[Constructable]
	public Plantesuspendue7() : base(0xA5AF)
	{
		Weight = 1.0;
		Name = "Plante suspendue 7";
	}

	public Plantesuspendue7(Serial serial) : base(serial) { }

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

public class Plantesuspendue8 : Item
{
	[Constructable]
	public Plantesuspendue8() : base(0xA5B0)
	{
		Weight = 1.0;
		Name = "Plante suspendue 8";
	}

	public Plantesuspendue8(Serial serial) : base(serial) { }

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

public class Plantesuspendue9 : Item
{
	[Constructable]
	public Plantesuspendue9() : base(0xA5B1)
	{
		Weight = 1.0;
		Name = "Plante suspendue 9";
	}

	public Plantesuspendue9(Serial serial) : base(serial) { }

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

public class PlanteVase1 : Item
{
	[Constructable]
	public PlanteVase1() : base(0xA5B2)
	{
		Weight = 1.0;
		Name = "Plante Vase 1";
	}

	public PlanteVase1(Serial serial) : base(serial) { }

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

public class PlanteVase2 : Item
{
	[Constructable]
	public PlanteVase2() : base(0xA5B3)
	{
		Weight = 1.0;
		Name = "Plante Vase 2";
	}

	public PlanteVase2(Serial serial) : base(serial) { }

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

public class PlanteVase3 : Item
{
	[Constructable]
	public PlanteVase3() : base(0xA5B4)
	{
		Weight = 1.0;
		Name = "Plante Vase 3";
	}

	public PlanteVase3(Serial serial) : base(serial) { }

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

public class PlanteVase4 : Item
{
	[Constructable]
	public PlanteVase4() : base(0xA5B5)
	{
		Weight = 1.0;
		Name = "Plante Vase 4";
	}

	public PlanteVase4(Serial serial) : base(serial) { }

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

public class PlanteVase5 : Item
{
	[Constructable]
	public PlanteVase5() : base(0xA5B6)
	{
		Weight = 1.0;
		Name = "Plante Vase 5";
	}

	public PlanteVase5(Serial serial) : base(serial) { }

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

public class PlanteVase6 : Item
{
	[Constructable]
	public PlanteVase6() : base(0xA5B7)
	{
		Weight = 1.0;
		Name = "Plante Vase 6";
	}

	public PlanteVase6(Serial serial) : base(serial) { }

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

public class PlanteVase7 : Item
{
	[Constructable]
	public PlanteVase7() : base(0xA5B8)
	{
		Weight = 1.0;
		Name = "Plante Vase 7";
	}

	public PlanteVase7(Serial serial) : base(serial) { }

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

[Flipable(0xA5B9, 0xA5BA)]
public class PlanteVase8 : Item
{
	[Constructable]
	public PlanteVase8() : base(0xA5B9)
	{
		Weight = 1.0;
		Name = "Plante Vase 8";
	}

	public PlanteVase8(Serial serial) : base(serial) { }

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

public class PlanteVase9 : Item
{
	[Constructable]
	public PlanteVase9() : base(0xA5BB)
	{
		Weight = 1.0;
		Name = "Plante Vase 9";
	}

	public PlanteVase9(Serial serial) : base(serial) { }

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

public class PlanteVase10 : Item
{
	[Constructable]
	public PlanteVase10() : base(0xA5BC)
	{
		Weight = 1.0;
		Name = "Plante Vase 10";
	}

	public PlanteVase10(Serial serial) : base(serial) { }

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

[Flipable(0xA5D2, 0xA5D3, 0xA5D4, 0xA5D5)]
public class Presentoir : Item
{
	[Constructable]
	public Presentoir() : base(0xA5D2)
	{
		Weight = 1.0;
		Name = "Presentoir";
	}

	public Presentoir(Serial serial) : base(serial) { }

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

public class Bouquet1 : Item
{
	[Constructable]
	public Bouquet1() : base(0xA3E7)
	{
		Weight = 1.0;
		Name = "Bouquet 1";
		Layer = Layer.OneHanded;
	}

	public Bouquet1(Serial serial) : base(serial) { }

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

public class Bouquet2 : Item
{
	[Constructable]
	public Bouquet2() : base(0xA3E8)
	{
		Weight = 1.0;
		Name = "Bouquet 2";
		Layer = Layer.OneHanded;

		}

		public Bouquet2(Serial serial) : base(serial) { }

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

public class Bouquet3 : Item
{
	[Constructable]
	public Bouquet3() : base(0xA3F2)
	{
		Weight = 1.0;
		Name = "Bouquet 3";
		Layer = Layer.OneHanded;

		}

		public Bouquet3(Serial serial) : base(serial) { }

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