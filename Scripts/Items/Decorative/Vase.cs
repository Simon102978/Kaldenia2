﻿namespace Server.Items
{
    public class Vase : Item
    {
        [Constructable]
        public Vase()
            : base(0xB46)
        {
            Weight = 1.0;
        }

        public Vase(Serial serial)
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

	[Flipable(0x1949, 0x194A)]
	public class StatueDon : Item
	{
		[Constructable]
		public StatueDon() : base(0x1949)
		{
			Weight = 1.0;
			Name = "Statue Don";
		}

		public StatueDon(Serial serial) : base(serial) { }

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

	public class StatueMeduse : Item
	{
		[Constructable]
		public StatueMeduse() : base(0x40BC)
		{
			Weight = 1.0;
			Name = "Statue Meduse";
		}

		public StatueMeduse(Serial serial) : base(serial) { }

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

	public class StatueCheval : Item
	{
		[Constructable]
		public StatueCheval() : base(0x40BD)
		{
			Weight = 1.0;
			Name = "Statue Cheval";
		}

		public StatueCheval(Serial serial) : base(serial) { }

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

	public class StatueGuerrier : Item
	{
		[Constructable]
		public StatueGuerrier() : base(0x42C2)
		{
			Weight = 1.0;
			Name = "Statue Guerrier";
		}

		public StatueGuerrier(Serial serial) : base(serial) { }

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

	[Flipable(0x9F0F, 0x9F10)]
	public class StatueSwan : Item
	{
		[Constructable]
		public StatueSwan() : base(0x9F0F)
		{
			Weight = 1.0;
			Name = "Statue Swan";
		}

		public StatueSwan(Serial serial) : base(serial) { }

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

	[Flipable(0x9F11, 0x9F12)]
	public class StatueSwanNoir : Item
	{
		[Constructable]
		public StatueSwanNoir() : base(0x9F11)
		{
			Weight = 1.0;
			Name = "Statue Swan Noir";
		}

		public StatueSwanNoir(Serial serial) : base(serial) { }

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

	[Flipable(0xA55B, 0xA55C)]
	public class StatueEgypte : Item
	{
		[Constructable]
		public StatueEgypte() : base(0xA55B)
		{
			Weight = 1.0;
			Name = "Statue Egypte";
		}

		public StatueEgypte(Serial serial) : base(serial) { }

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

	[Flipable(0xA605, 0xA606)]
	public class StatueDieu : Item
	{
		[Constructable]
		public StatueDieu() : base(0xA605)
		{
			Weight = 1.0;
			Name = "Statue Dieu";
		}

		public StatueDieu(Serial serial) : base(serial) { }

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

	[Flipable(0xA55D, 0xA55E)]
	public class StatueJackal : Item
	{
		[Constructable]
		public StatueJackal() : base(0xA55D)
		{
			Weight = 1.0;
			Name = "Statue Jackal";
		}

		public StatueJackal(Serial serial) : base(serial) { }

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

	[Flipable(0xA559, 0xA55A)]
	public class FauconDore : Item
	{
		[Constructable]
		public FauconDore() : base(0xA559)
		{
			Weight = 1.0;
			Name = "Faucon Dore";
		}

		public FauconDore(Serial serial) : base(serial) { }

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

	[Flipable(0xA56E, 0xA56F)]
	public class StatueAnge : Item
	{
		[Constructable]
		public StatueAnge() : base(0xA56E)
		{
			Weight = 1.0;
			Name = "Statue Ange";
		}

		public StatueAnge(Serial serial) : base(serial) { }

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

	public class ColonneMarbre : Item
	{
		[Constructable]
		public ColonneMarbre() : base(0xA584)
		{
			Weight = 1.0;
			Name = "Colonne Marbre";
		}

		public ColonneMarbre(Serial serial) : base(serial) { }

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

	[Flipable(0xA5CE, 0xA5CF)]
	public class StatueLion : Item
	{
		[Constructable]
		public StatueLion() : base(0xA5CE)
		{
			Weight = 1.0;
			Name = "Statue Lion";
		}

		public StatueLion(Serial serial) : base(serial) { }

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

	[Flipable(0xA5FD, 0xA5FE, 0xA5FF, 0xA600)]
	public class LutrinMarbre : Item
	{
		[Constructable]
		public LutrinMarbre() : base(0xA5FD)
		{
			Weight = 1.0;
			Name = "Lutrin Marbre";
		}

		public LutrinMarbre(Serial serial) : base(serial) { }

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

	[Flipable(0xA572, 0xA573)]
	public class AltarBlanc : Item
	{
		[Constructable]
		public AltarBlanc() : base(0xA572)
		{
			Weight = 1.0;
			Name = "Altar Blanc";
		}

		public AltarBlanc(Serial serial) : base(serial) { }

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
	public class VaseTresor : Item
	{
		[Constructable]
		public VaseTresor() : base(0xA5BD)
		{
			Weight = 1.0;
			Name = "Vase Tresor";
		}

		public VaseTresor(Serial serial) : base(serial) { }

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

	public class GrandeUrne : Item
	{
		[Constructable]
		public GrandeUrne() : base(0xA555)
		{
			Weight = 1.0;
			Name = "Grande Urne";
		}

		public GrandeUrne(Serial serial) : base(serial) { }

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

	public class UrneBleue : Item
	{
		[Constructable]
		public UrneBleue() : base(0xA5D6)
		{
			Weight = 1.0;
			Name = "Urne Bleue";
		}

		public UrneBleue(Serial serial) : base(serial) { }

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

	public class UrneEvasee : Item
	{
		[Constructable]
		public UrneEvasee() : base(0xA5D7)
		{
			Weight = 1.0;
			Name = "Urne Evasee";
		}

		public UrneEvasee(Serial serial) : base(serial) { }

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

	public class Urne3 : Item
	{
		[Constructable]
		public Urne3() : base(0xA5BE)
		{
			Weight = 1.0;
			Name = "Urne 3";
		}

		public Urne3(Serial serial) : base(serial) { }

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
	public class LargeVase : Item
	{
		[Constructable]
		public LargeVase() : base(0x0B45)
		{
			Weight = 1.0;
			Name = "Large Vase";
		}

		public LargeVase(Serial serial) : base(serial) { }

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

	public class LargeVase2 : Item
	{
		[Constructable]
		public LargeVase2() : base(0x0B47)
		{
			Weight = 1.0;
			Name = "Large Vase 2";
		}

		public LargeVase2(Serial serial) : base(serial) { }

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

	public class VaseDore : Item
	{
		[Constructable]
		public VaseDore() : base(0x3D7C)
		{
			Weight = 1.0;
			Name = "Vase Dore";
		}

		public VaseDore(Serial serial) : base(serial) { }

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

	public class VaseAnthracite : Item
	{
		[Constructable]
		public VaseAnthracite() : base(0x3D7D)
		{
			Weight = 1.0;
			Name = "Vase Anthracite";
		}

		public VaseAnthracite(Serial serial) : base(serial) { }

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

	public class PetitVaseLigne : Item
	{
		[Constructable]
		public PetitVaseLigne() : base(0x3D7E)
		{
			Weight = 1.0;
			Name = "Petit Vase Ligne";
		}

		public PetitVaseLigne(Serial serial) : base(serial) { }

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

	public class GrandVaseLigne : Item
	{
		[Constructable]
		public GrandVaseLigne() : base(0x3D7F)
		{
			Weight = 1.0;
			Name = "Grand Vase Ligne";
		}

		public GrandVaseLigne(Serial serial) : base(serial) { }

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

	[Flipable(0x4042, 0x4043)]
	public class VaseTravaille : Item
	{
		[Constructable]
		public VaseTravaille() : base(0x4042)
		{
			Weight = 1.0;
			Name = "Vase Travaille";
		}

		public VaseTravaille(Serial serial) : base(serial) { }

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

	public class VaseRunique : Item
	{
		[Constructable]
		public VaseRunique() : base(0x42B2)
		{
			Weight = 1.0;
			Name = "Vase Runique";
		}

		public VaseRunique(Serial serial) : base(serial) { }

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

	public class VaseRunique2 : Item
	{
		[Constructable]
		public VaseRunique2() : base(0x42B3)
		{
			Weight = 1.0;
			Name = "Vase Runique 2";
		}

		public VaseRunique2(Serial serial) : base(serial) { }

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

	public class VaseEgyptien : Item
	{
		[Constructable]
		public VaseEgyptien() : base(0x9BC7)
		{
			Weight = 1.0;
			Name = "Vase Egyptien";
		}

		public VaseEgyptien(Serial serial) : base(serial) { }

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

	public class VaseEgyptien2 : Item
	{
		[Constructable]
		public VaseEgyptien2() : base(0x9BCA)
		{
			Weight = 1.0;
			Name = "Vase Egyptien 2";
		}

		public VaseEgyptien2(Serial serial) : base(serial) { }

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

	public class VaseGobelet : Item
	{
		[Constructable]
		public VaseGobelet() : base(0xA55F)
		{
			Weight = 1.0;
			Name = "Vase Gobelet";
		}

		public VaseGobelet(Serial serial) : base(serial) { }

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

	public class VaseDecoratif : Item
	{
		[Constructable]
		public VaseDecoratif() : base(0xA560)
		{
			Weight = 1.0;
			Name = "Vase Decoratif";
		}

		public VaseDecoratif(Serial serial) : base(serial) { }

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

	public class VaseEndommage : Item
	{
		[Constructable]
		public VaseEndommage() : base(0xA561)
		{
			Weight = 1.0;
			Name = "Vase Endommage";
		}

		public VaseEndommage(Serial serial) : base(serial) { }

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
	public class Vase1 : Item
	{
		[Constructable]
		public Vase1()
			: base(0x999E)
		{
			Weight = 1.0;
		}

		public Vase1(Serial serial)
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

	public class Vase2 : Item
	{
		[Constructable]
		public Vase2()
			: base(0x999F)
		{
			Weight = 1.0;
		}

		public Vase2(Serial serial)
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

	public class Vase3 : Item
	{
		[Constructable]
		public Vase3()
			: base(0x99A0)
		{
			Weight = 1.0;
		}

		public Vase3(Serial serial)
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

	public class Vase4 : Item
	{
		[Constructable]
		public Vase4()
			: base(0x99A1)
		{
			Weight = 1.0;
		}

		public Vase4(Serial serial)
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

	public class Vase5 : Item
	{
		[Constructable]
		public Vase5()
			: base(0x99A2)
		{
			Weight = 1.0;
		}

		public Vase5(Serial serial)
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



	public class Urne : Item
	{
		[Constructable]
		public Urne()
			: base(0x9A00)
		{
			Weight = 1.0;
			Name = "Une Urne";
		}

		public Urne(Serial serial)
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


	public class Urne2 : Item
	{
		[Constructable]
		public Urne2()
			: base(0x9A00)
		{
			Weight = 1.0;
			Name = "Une Urne";
		}

		public Urne2(Serial serial)
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


	

    public class SmallUrn : Item
    {
        [Constructable]
        public SmallUrn()
            : base(0x241C)
        {
            Weight = 1.0;
        }

        public SmallUrn(Serial serial)
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
	[Flipable(0x9992, 0x9993)]
	public class TeteBuffle : Item
	{
		[Constructable]
		public TeteBuffle()
			: base(0x9992)
		{
			Weight = 1.0;
			Name = "Tête de Buffle";
		}

		public TeteBuffle(Serial serial)
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
	public class MiniCherryTree1 : Item
	{
		[Constructable]
		public MiniCherryTree1()
			: base(0x9952)
		{
			Weight = 1.0;
			Name = "Arbre en pot";
		}

		public MiniCherryTree1(Serial serial)
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
	public class Orchid1 : Item
	{
		[Constructable]
		public Orchid1()
			: base(0x9953)
		{
			Weight = 1.0;
			Name = "Orchidée";
		}

		public Orchid1(Serial serial)
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
	public class Orchid2 : Item
	{
		[Constructable]
		public Orchid2()
			: base(0x9954)
		{
			Weight = 1.0;
			Name = "Orchidée";
		}

		public Orchid2(Serial serial)
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

	
	public class BacFleurVide1 : Item
	{
		[Constructable]
		public BacFleurVide1()
			: base(0x99D6)
		{
			Weight = 1.0;
			Name = "Bac Fleur Vide 1";
		}

		public BacFleurVide1(Serial serial)
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
	public class BacFleurVide2 : Item
	{
		[Constructable]
		public BacFleurVide2()
			: base(0x99D7)
		{
			Weight = 1.0;
			Name = "Bac Fleur Vide 2";
		}

		public BacFleurVide2(Serial serial)
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
	public class BacFleurArbre : Item
	{
		[Constructable]
		public BacFleurArbre()
			: base(0x99D8)
		{
			Weight = 1.0;
			Name = "Bac Fleur Arbre";
		}

		public BacFleurArbre(Serial serial)
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
	public class BacFleurCactus1 : Item
	{
		[Constructable]
		public BacFleurCactus1()
			: base(0x99D9)
		{
			Weight = 1.0;
			Name = "Bac Fleur Cactus 1 ";
		}

		public BacFleurCactus1(Serial serial)
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
	public class BacFleurCactus2 : Item
	{
		[Constructable]
		public BacFleurCactus2()
			: base(0x99DA)
		{
			Weight = 1.0;
			Name = "Bac Fleur Cactus 2";
		}

		public BacFleurCactus2(Serial serial)
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

	public class BacFleurCactus3 : Item
	{
		[Constructable]
		public BacFleurCactus3()
			: base(0x99DB)
		{
			Weight = 1.0;
			Name = "Bac Fleur Cactus 3";
		}

		public BacFleurCactus3(Serial serial)
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

	public class BacFleurCactus4 : Item
	{
		[Constructable]
		public BacFleurCactus4()
			: base(0x99DC)
		{
			Weight = 1.0;
			Name = "Bac Fleur Cactus 4";
		}

		public BacFleurCactus4(Serial serial)
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

	public class BacFleurCactus5 : Item
	{
		[Constructable]
		public BacFleurCactus5()
			: base(0x99DD)
		{
			Weight = 1.0;
			Name = "Bac Fleur Cactus 2";
		}

		public BacFleurCactus5(Serial serial)
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

	public class BacFleurCactus6 : Item
	{
		[Constructable]
		public BacFleurCactus6()
			: base(0x99DE)
		{
			Weight = 1.0;
			Name = "Bac Fleur Cactus 6";
		}

		public BacFleurCactus6(Serial serial)
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

	public class BacArbustre1 : Item
	{
		[Constructable]
		public BacArbustre1()
			: base(0x99DF)
		{
			Weight = 1.0;
			Name = "Bac Arbustre 1";
		}

		public BacArbustre1(Serial serial)
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

	public class BacArbustre2 : Item
	{
		[Constructable]
		public BacArbustre2()
			: base(0x99E0)
		{
			Weight = 1.0;
			Name = "Bac Arbustre 2";
		}

		public BacArbustre2(Serial serial)
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

	public class BacArbustre3 : Item
	{
		[Constructable]
		public BacArbustre3()
			: base(0x99E1)
		{
			Weight = 1.0;
			Name = "Bac Arbustre 3";
		}

		public BacArbustre3(Serial serial)
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

	public class BacArbustre4 : Item
	{
		[Constructable]
		public BacArbustre4()
			: base(0x99E2)
		{
			Weight = 1.0;
			Name = "Bac Arbustre 4";
		}

		public BacArbustre4(Serial serial)
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

	public class BacArbustre5 : Item
	{
		[Constructable]
		public BacArbustre5()
			: base(0x99E3)
		{
			Weight = 1.0;
			Name = "Bac Arbustre 5";
		}

		public BacArbustre5(Serial serial)
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

	public class BacArbustre6 : Item
	{
		[Constructable]
		public BacArbustre6()
			: base(0x99E4)
		{
			Weight = 1.0;
			Name = "Bac Arbustre 6";
		}

		public BacArbustre6(Serial serial)
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

	public class BacArbustre7 : Item
	{
		[Constructable]
		public BacArbustre7()
			: base(0x99E5)
		{
			Weight = 1.0;
			Name = "Bac Arbustre 7";
		}

		public BacArbustre7(Serial serial)
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
	public class PlanteSuspendu1 : Item
	{
		[Constructable]
		public PlanteSuspendu1()
			: base(0x99EA)
		{
			Weight = 1.0;
			Name = "Plante Suspendu 1";
		}

		public PlanteSuspendu1(Serial serial)
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
	public class PlanteSuspendu2 : Item
	{
		[Constructable]
		public PlanteSuspendu2()
			: base(0x99EB)
		{
			Weight = 1.0;
			Name = "Plante Suspendue 2";
		}

		public PlanteSuspendu2(Serial serial)
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
	public class PlanteSuspendu3 : Item
	{
		[Constructable]
		public PlanteSuspendu3()
			: base(0x99EC)
		{
			Weight = 1.0;
			Name = "Plante Suspendue 3";
		}

		public PlanteSuspendu3(Serial serial)
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
	public class PlanteSuspendu4 : Item
	{
		[Constructable]
		public PlanteSuspendu4()
			: base(0x99ED)
		{
			Weight = 1.0;
			Name = "Plante Suspendue 4";
		}

		public PlanteSuspendu4(Serial serial)
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
	public class PlanteSuspendu5 : Item
	{
		[Constructable]
		public PlanteSuspendu5()
			: base(0x99EE)
		{
			Weight = 1.0;
			Name = "Plante Suspendue 5";
		}

		public PlanteSuspendu5(Serial serial)
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
	public class PlanteSuspendu6 : Item
	{
		[Constructable]
		public PlanteSuspendu6()
			: base(0x99EF)
		{
			Weight = 1.0;
			Name = "Plante Suspendue 6";
		}

		public PlanteSuspendu6(Serial serial)
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
	public class PlanteSuspendu7 : Item
	{
		[Constructable]
		public PlanteSuspendu7()
			: base(0x99F0)
		{
			Weight = 1.0;
			Name = "Plante Suspendue 7";
		}

		public PlanteSuspendu7(Serial serial)
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
	public class PlanteSuspendu8 : Item
	{
		[Constructable]
		public PlanteSuspendu8()
			: base(0x99F1)
		{
			Weight = 1.0;
			Name = "Plante Suspendue 8";
		}

		public PlanteSuspendu8(Serial serial)
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
	public class PlanteSuspendu9 : Item
	{
		[Constructable]
		public PlanteSuspendu9()
			: base(0x99F2)
		{
			Weight = 1.0;
			Name = "Plante Suspendue 9";
		}

		public PlanteSuspendu9(Serial serial)
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
	

	[Flipable(0x99A4, 0x99A5)]
	public class DecoSoleil : Item
	{
		[Constructable]
		public DecoSoleil()
			: base(0x99A4)
		{
			Weight = 1.0;
			Name = "Deco Soleil";
		}

		public DecoSoleil(Serial serial)
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

	[Flipable(0x99E6, 0x99E7)]
	public class CrochetAPlante1 : Item
	{
		[Constructable]
		public CrochetAPlante1()
			: base(0x99E6)
		{
			Weight = 1.0;
			Name = "Crochet A Plante 1";
		}

		public CrochetAPlante1(Serial serial)
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

	[Flipable(0x99E8, 0x99E9)]
	public class CrochetAPlante2 : Item
	{
		[Constructable]
		public CrochetAPlante2()
			: base(0x99E8)
		{
			Weight = 1.0;
			Name = "Crochet A Plante 2";
		}

		public CrochetAPlante2(Serial serial)
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


	public class 	BouquetFleurRouge  : Item
	{
		[Constructable]
	public 	BouquetFleurRouge()
			: base(0xA3E7)
		{
			Weight = 1.0;
			Name = "Bouquet de fleur Rouge";
			Layer = Layer.OneHanded;
		}

	public 	BouquetFleurRouge(Serial serial)
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
	public class BouquetFleurBlanche : Item
	{
		[Constructable]
	public BouquetFleurBlanche()
			: base(0xA3E8)
		{
			Weight = 1.0;
			Name = "Bouquet Fleur Blanche";
			Layer = Layer.OneHanded;
		}

	public BouquetFleurBlanche(Serial serial)
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
	public class BouquetFleur : Item
	{
		[Constructable]
		public BouquetFleur()
			: base(0xA3F2)
		{
			Weight = 1.0;
			Name = "Bouquet Fleur";
			Layer = Layer.OneHanded;
		}

		public BouquetFleur(Serial serial)
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
	[Flipable(0x997C, 0x997D)]
	public class 	AnkhKershe : Item
	{
		[Constructable]
		public AnkhKershe()
			: base(0x997C)
		{
			Weight = 10.0;
			Name = "Ankh Kershe";
		}

		public AnkhKershe(Serial serial)
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
	[Flipable(0x997E, 0x997F)]
	public class PetiteAnkhKershe : Item
	{
		[Constructable]
		public PetiteAnkhKershe()
			: base(0x997E)
		{
			Weight = 10.0;
			Name = "Petite Ankh Kershe";
		}

		public PetiteAnkhKershe(Serial serial)
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
	[Flipable(0x998A, 0x998B)]
	public class BouteilleBateau : Item
	{
		[Constructable]
		public BouteilleBateau()
			: base(0x998A)
		{
			Weight = 2.0;
			Name = "Bateau dans une Bouteille";
		}

		public BouteilleBateau(Serial serial)
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

	

	[Flipable(0x9A01, 0x9A02)]
	public class RackPelle : Item
	{
		[Constructable]
		public RackPelle()
			: base(0x9A01)
		{
			Weight = 2.0;
			Name = "Rack à Pelles";
		}

		public RackPelle(Serial serial)
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
	
	[Flipable(0x9A07, 0x9A08)]
	public class JackalDore : Item
	{
		[Constructable]
		public JackalDore()
			: base(0x9A07)
		{
			Weight = 2.0;
			Name = "Jackal Doré";
		}

		public JackalDore(Serial serial)
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
	
	public class Orbe : Item
	{
		[Constructable]
		public Orbe()
			: base(0x9A09)
		{
			Weight = 2.0;
			Name = "Orbe";
		}

		public Orbe(Serial serial)
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

	[Flipable(0x9A0A, 0x9A0B)]
	public class PentacleMural : Item
	{
		[Constructable]
		public PentacleMural()
			: base(0x9A0A)
		{
			Weight = 2.0;
			Name = "Pentacle Mural";
		}

		public PentacleMural(Serial serial)
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
	[Flipable(0x9980, 0x9981)]
	public class AvisRecherche : Item
	{
		[Constructable]
		public AvisRecherche()
			: base(0x9980)
		{
			Weight = 1.0;
			Name = "Avis de Recherche";
		}

		public AvisRecherche(Serial serial)
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
	[Flipable(0x9986, 0x9987, 0x9988, 0x9989)]
	public class Lutrin : Item
	{
		[Constructable]
		public Lutrin()
			: base(0x9986)
		{
			Weight = 12.0;
			Name = "Un Lutrin";
		}

		public Lutrin(Serial serial)
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