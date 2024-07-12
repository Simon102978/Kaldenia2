using Server.Items;
using Server;

public class Geode : Item
{
	[Constructable]
	public Geode() : base(19274) // ItemID 19274 pour la g�ode
	{
		Name = "G�ode";
	}

	public override void OnDoubleClick(Mobile from)
	{
		if (!IsChildOf(from.Backpack))
		{
			from.SendLocalizedMessage(1042001); // Cela doit �tre dans votre sac pour l'utiliser.
			return;
		}

		from.SendMessage("Vous brisez la g�ode...");

		double chance = Utility.RandomDouble();

		if (chance < 0.5) // 50.0% de chance d'obtenir une gemme 
		{
			Item gem = GetRandomGem();
			from.AddToBackpack(gem);
			from.SendMessage($"Vous avez trouv� un(e) {gem.Name}!");
		}
		else if (Utility.RandomDouble() < 0.003)
		{
			TreasureMap map = new TreasureMap(Utility.RandomMinMax(1, 3), Map.Felucca);
			from.AddToBackpack(map);
			from.SendMessage("Vous avez trouv� une carte au tr�sor!");
		}
		else // Sinon, la g�ode est d�truite et donne 1-5 minerai de fer
		{
			Item ironOre = new IronOre(Utility.RandomMinMax(1, 5));
			from.AddToBackpack(ironOre);
			from.SendMessage("La g�ode se brise en morceaux. Vous trouvez un peu de minerai de fer.");
		}

		this.Delete(); // La g�ode est d�truite apr�s utilisation
	}

	private Item GetRandomGem()
	{
		switch (Utility.Random(9))
		{
			case 0: return new Ambre();
			case 1: return new Amethyste();
			case 2: return new Citrine();
			case 3: return new Diamant();
			case 4: return new Emeraude();
			case 5: return new Rubis();
			case 6: return new Sapphire();
			case 7: return new SaphirEtoile();
			case 8: return new Tourmaline();
			default: return new Ambre(); // Par d�faut, retournez de l'ambre
		}
	}

	public Geode(Serial serial) : base(serial)
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