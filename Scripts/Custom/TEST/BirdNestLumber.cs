
using Server;
using Server.Items;
public class BirdNest : Item
{
	[Constructable]
	public BirdNest() : base(0x1AD4) // ID graphique du nid plein
	{
		Name = "un nid d'oiseau";
	}

	public override void OnDoubleClick(Mobile from)
	{
		if (!IsChildOf(from.Backpack))
		{
			from.SendLocalizedMessage(1042001); // Cela doit être dans votre sac pour l'utiliser.
			return;
		}

		from.SendMessage("Vous fouillez dans le nid...");

		// 100% de chance d'obtenir 1 à 3 types d'herbes, chacune en quantité 1-3
		int herbTypeCount = Utility.RandomMinMax(1, 3);
		for (int i = 0; i < herbTypeCount; i++)
		{
			Item herb = GetRandomHerb();
			int quantity = Utility.RandomMinMax(1, 3);
			herb.Amount = quantity;
			from.AddToBackpack(herb);
			from.SendMessage($"Vous avez trouvé {quantity} {herb.Name}!");
		}

		// 0.3% de chance d'obtenir une carte au trésor
		if (Utility.RandomDouble() < 0.003)
		{
			 TreasureMap map = new TreasureMap(Utility.RandomMinMax(1, 3), Map.Felucca);
			from.AddToBackpack(map);
			from.SendMessage("Vous avez trouvé une carte au trésor!");
		}

		// Le nid devient vide
		this.ItemID = 0x1AD5; // ID graphique du nid vide
		this.Name = "un nid d'oiseau vide";
	}

	private Item GetRandomHerb()
	{
		switch (Utility.Random(37))
		{
			case 0: return new Kindling();
			case 1: return new Acacia();
			case 2: return new Anise();
			case 3: return new Basil();
			case 4: return new BayLeaf();
			case 5: return new Chamomile();
			case 6: return new Caraway();
			case 7: return new Cilantro();
			case 8: return new Cinnamon();
			case 9: return new Clove();
			case 10: return new Copal();
			case 11: return new Coriander();
			case 12: return new Dill();
			case 13: return new Dragonsblood();
			case 14: return new Frankincense();
			case 15: return new Lavender();
			case 16: return new Marjoram();
			case 17: return new Meadowsweet();
			case 18: return new Mint();
			case 19: return new Mugwort();
			case 20: return new Mustard();
			case 21: return new Myrrh();
			case 22: return new Olive();
			case 23: return new Oregano();
			case 24: return new Orris();
			case 25: return new Patchouli();
			case 26: return new Peppercorn();
			case 27: return new RoseHerb();
			case 28: return new Rosemary();
			case 29: return new Saffron();
			case 30: return new Sandelwood();
			case 31: return new SlipperyElm();
			case 32: return new Thyme();
			case 33: return new Valerian();
			case 34: return new WillowBark();
			case 35: return new TribalBerry();
			case 36: return new Sage();

			default: return new Kindling(); // Par défaut, retournez de la sauge
		}
	}

	public BirdNest(Serial serial) : base(serial)
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