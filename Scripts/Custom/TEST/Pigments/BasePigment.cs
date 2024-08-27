using System;
using Server;
using Server.Targeting;

namespace Server.Items
{
	public abstract class BasePigment : Item
	{
		private int m_Hue;
		private string m_PigmentName;

		public BasePigment(int hue, string name) : base(0x0E2C)
		{
			m_Hue = hue;
			m_PigmentName = name;
			Name = $"Pigment: {m_PigmentName}";
			Hue = m_Hue;
		}

		public BasePigment(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001); // Cet objet doit être dans votre sac pour que vous puissiez l'utiliser.
				return;
			}

			from.SendMessage("Sélectionnez une cuve de teinture pour appliquer ce pigment.");
			from.Target = new InternalTarget(this);
		}

		private class InternalTarget : Target
		{
			private BasePigment m_Pigment;

			public InternalTarget(BasePigment pigment) : base(1, false, TargetFlags.None)
			{
				m_Pigment = pigment;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is SpecialDyeTub specialTub)
				{
					if (specialTub.Charges > 0)
					{
						specialTub.DyedHue = m_Pigment.m_Hue;
						specialTub.Charges--;
						from.SendMessage($"Vous avez appliqué le pigment {m_Pigment.m_PigmentName} à la cuve de teinture spéciale. Il reste {specialTub.Charges} charges.");
						m_Pigment.Delete();
					}
					else
					{
						from.SendMessage("Cette cuve de teinture spéciale n'a plus de charges.");
					}
				}
				else if (targeted is DyeTub regularTub)
				{
					if (regularTub.Charges > 1)
					{
						regularTub.DyedHue = m_Pigment.m_Hue;
						regularTub.Charges = 5;
						regularTub.Name = $"Bac de Teinture ({m_Pigment.m_PigmentName}) ";
						from.SendMessage($"Vous avez appliqué le pigment {m_Pigment.m_PigmentName} à la cuve de teinture. Il reste {regularTub.Charges} charges.");
						m_Pigment.Delete();
					}
					else if (regularTub.Charges == 1)
					{
						regularTub.DyedHue = m_Pigment.m_Hue;
						regularTub.Charges = 1;
						regularTub.Name = $"Bac de Teinture ({m_Pigment.m_PigmentName}) ";
						from.SendMessage($"Vous avez appliqué le pigment {m_Pigment.m_PigmentName} à la cuve de teinture. La cuve n'a plus de charges.");
						m_Pigment.Delete();
					}
					else
					{
						from.SendMessage("Cette cuve de teinture n'a plus de charges.");
					}
				}
				else
				{
					from.SendMessage("Ce n'est pas une cuve de teinture.");
				}
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0); // version
			writer.Write(m_Hue);
			writer.Write(m_PigmentName);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Hue = reader.ReadInt();
			m_PigmentName = reader.ReadString();
		}
	}




// Bleu
public class BleuCobaltPigment : BasePigment
	{
		[Constructable]
		public BleuCobaltPigment() : base(2432, "Bleu Cobalt") { }
		public BleuCobaltPigment(Serial serial) : base(serial) { }
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

	public class BleuElectriquePigment : BasePigment
	{
		[Constructable]
		public BleuElectriquePigment() : base(2482, "Bleu électrique") { }
		public BleuElectriquePigment(Serial serial) : base(serial) { }
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

	public class BleuFruitePigment : BasePigment
	{
		[Constructable]
		public BleuFruitePigment() : base(2883, "Bleu fruité") { }
		public BleuFruitePigment(Serial serial) : base(serial) { }
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

	public class BleuAzurPigment : BasePigment
	{
		[Constructable]
		public BleuAzurPigment() : base(2937, "Bleu azur") { }
		public BleuAzurPigment(Serial serial) : base(serial) { }
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

	public class BleuOceanPigment : BasePigment
	{
		[Constructable]
		public BleuOceanPigment() : base(2967, "Bleu océan") { }
		public BleuOceanPigment(Serial serial) : base(serial) { }
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

	// Vert
	public class VertBouteillePigment : BasePigment
	{
		[Constructable]
		public VertBouteillePigment() : base(2364, "Bouteille") { }
		public VertBouteillePigment(Serial serial) : base(serial) { }
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

	public class VertForestierPigment : BasePigment
	{
		[Constructable]
		public VertForestierPigment() : base(2822, "Vert forestier") { }
		public VertForestierPigment(Serial serial) : base(serial) { }
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

	public class VertMoussePigment : BasePigment
	{
		[Constructable]
		public VertMoussePigment() : base(2824, "Vert mousse") { }
		public VertMoussePigment(Serial serial) : base(serial) { }
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

	public class VertJadePigment : BasePigment
	{
		[Constructable]
		public VertJadePigment() : base(2874, "Vert jade") { }
		public VertJadePigment(Serial serial) : base(serial) { }
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

	public class VertLimePigment : BasePigment
	{
		[Constructable]
		public VertLimePigment() : base(2971, "Vert lime") { }
		public VertLimePigment(Serial serial) : base(serial) { }
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

	// Jaune
	public class JauneImperialPigment : BasePigment
	{
		[Constructable]
		public JauneImperialPigment() : base(2354, "Jaune impérial") { }
		public JauneImperialPigment(Serial serial) : base(serial) { }
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

	public class JauneSablePigment : BasePigment
	{
		[Constructable]
		public JauneSablePigment() : base(2821, "Jaune sable") { }
		public JauneSablePigment(Serial serial) : base(serial) { }
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

	public class JauneMaisPigment : BasePigment
	{
		[Constructable]
		public JauneMaisPigment() : base(2873, "Jaune maïs") { }
		public JauneMaisPigment(Serial serial) : base(serial) { }
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

	public class JauneDOrPigment : BasePigment
	{
		[Constructable]
		public JauneDOrPigment() : base(2907, "Jaune d'or") { }
		public JauneDOrPigment(Serial serial) : base(serial) { }
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

	public class JauneCanariPigment : BasePigment
	{
		[Constructable]
		public JauneCanariPigment() : base(2968, "Jaune canari") { }
		public JauneCanariPigment(Serial serial) : base(serial) { }
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

	// Orange
	public class OrangeEpicePigment : BasePigment
	{
		[Constructable]
		public OrangeEpicePigment() : base(1786, "Orange épicé") { }
		public OrangeEpicePigment(Serial serial) : base(serial) { }
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

	public class OrangeRustiquePigment : BasePigment
	{
		[Constructable]
		public OrangeRustiquePigment() : base(2143, "Orange rustique") { }
		public OrangeRustiquePigment(Serial serial) : base(serial) { }
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

	public class OrangeCendrePigment : BasePigment
	{
		[Constructable]
		public OrangeCendrePigment() : base(2350, "Orange cendré") { }
		public OrangeCendrePigment(Serial serial) : base(serial) { }
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

	public class OrangeBrulePigment : BasePigment
	{
		[Constructable]
		public OrangeBrulePigment() : base(2957, "Orange brûlé") { }
		public OrangeBrulePigment(Serial serial) : base(serial) { }
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

	public class OrangeSanguinePigment : BasePigment
	{
		[Constructable]
		public OrangeSanguinePigment() : base(2963, "Orange sanguine") { }
		public OrangeSanguinePigment(Serial serial) : base(serial) { }
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

	// Rouge
	public class RougeGrenatPigment : BasePigment
	{
		[Constructable]
		public RougeGrenatPigment() : base(2098, "Rouge grenat") { }
		public RougeGrenatPigment(Serial serial) : base(serial) { }
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

	public class RougeCerisePigment : BasePigment
	{
		[Constructable]
		public RougeCerisePigment() : base(2376, "Rouge cerise") { }
		public RougeCerisePigment(Serial serial) : base(serial) { }
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

	public class RougeEcarlatePigment : BasePigment
	{
		[Constructable]
		public RougeEcarlatePigment() : base(2252, "Rouge écarlate") { }
		public RougeEcarlatePigment(Serial serial) : base(serial) { }
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

	public class RougeAntiquePigment : BasePigment
	{
		[Constructable]
		public RougeAntiquePigment() : base(2639, "Rouge antique") { }
		public RougeAntiquePigment(Serial serial) : base(serial) { }
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

	public class RougeBriquePigment : BasePigment
	{
		[Constructable]
		public RougeBriquePigment() : base(2741, "Rouge brique") { }
		public RougeBriquePigment(Serial serial) : base(serial) { }
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

	// Mauve
	public class VieuxMauvePigment : BasePigment
	{
		[Constructable]
		public VieuxMauvePigment() : base(2731, "Vieux mauve") { }
		public VieuxMauvePigment(Serial serial) : base(serial) { }
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

	public class MauveIntensePigment : BasePigment
	{
		[Constructable]
		public MauveIntensePigment() : base(2748, "Mauve intense") { }
		public MauveIntensePigment(Serial serial) : base(serial) { }
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

	public class MauveLavandePigment : BasePigment
	{
		[Constructable]
		public MauveLavandePigment() : base(2749, "Mauve lavande") { }
		public MauveLavandePigment(Serial serial) : base(serial) { }
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

	public class MauveGlacePigment : BasePigment
	{
		[Constructable]
		public MauveGlacePigment() : base(2896, "Mauve glacé") { }
		public MauveGlacePigment(Serial serial) : base(serial) { }
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

	public class MauvePolairePigment : BasePigment
	{
		[Constructable]
		public MauvePolairePigment() : base(2972, "Mauve polaire") { }
		public MauvePolairePigment(Serial serial) : base(serial) { }
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

	// Rose
	public class FuchsiaPigment : BasePigment
	{
		[Constructable]
		public FuchsiaPigment() : base(1376, "Fuchsia") { }
		public FuchsiaPigment(Serial serial) : base(serial) { }
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

	public class RoseTenebreuxPigment : BasePigment
	{
		[Constructable]
		public RoseTenebreuxPigment() : base(2622, "Rose ténébreux") { }
		public RoseTenebreuxPigment(Serial serial) : base(serial) { }
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

	public class RoseFoncePigment : BasePigment
	{
		[Constructable]
		public RoseFoncePigment() : base(2818, "Rose foncé") { }
		public RoseFoncePigment(Serial serial) : base(serial) { }
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

	public class RoseTremierePigment : BasePigment
	{
		[Constructable]
		public RoseTremierePigment() : base(2922, "Rose trémière") { }
		public RoseTremierePigment(Serial serial) : base(serial) { }
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

	public class RoseThePigment : BasePigment
	{
		[Constructable]
		public RoseThePigment() : base(2994, "Rose thé") { }
		public RoseThePigment(Serial serial) : base(serial) { }
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

	// Brun/Beige
	public class BeigeChampagnePigment : BasePigment
	{
		[Constructable]
		public BeigeChampagnePigment() : base(1800, "Beige champagne") { }
		public BeigeChampagnePigment(Serial serial) : base(serial) { }
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

	public class BeigePoussiereuxPigment : BasePigment
	{
		[Constructable]
		public BeigePoussiereuxPigment() : base(2778, "Beige poussiéreux") { }
		public BeigePoussiereuxPigment(Serial serial) : base(serial) { }
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

	public class BeigeNaturelPigment : BasePigment
	{
		[Constructable]
		public BeigeNaturelPigment() : base(2814, "Beige naturel") { }
		public BeigeNaturelPigment(Serial serial) : base(serial) { }
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

	public class BrunTaupePigment : BasePigment
	{
		[Constructable]
		public BrunTaupePigment() : base(2815, "Brun taupe") { }
		public BrunTaupePigment(Serial serial) : base(serial) { }
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

	public class BrunDeSiennePigment : BasePigment
	{
		[Constructable]
		public BrunDeSiennePigment() : base(2964, "Brun de sienne") { }
		public BrunDeSiennePigment(Serial serial) : base(serial) { }
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

	// Noir/Blanc
	public class BlancPerlePigment : BasePigment
	{
		[Constructable]
		public BlancPerlePigment() : base(2491, "Blanc perle") { }
		public BlancPerlePigment(Serial serial) : base(serial) { }
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

	public class ChauxPigment : BasePigment
	{
		[Constructable]
		public ChauxPigment() : base(2493, "Chaux") { }
		public ChauxPigment(Serial serial) : base(serial) { }
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

	public class NeigeViolaceePigment : BasePigment
	{
		[Constructable]
		public NeigeViolaceePigment() : base(2804, "Neige violacée") { }
		public NeigeViolaceePigment(Serial serial) : base(serial) { }
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

	public class NoirPrunePigment : BasePigment
	{
		[Constructable]
		public NoirPrunePigment() : base(2856, "Noir prune") { }
		public NoirPrunePigment(Serial serial) : base(serial) { }
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

	public class AlbatrePigment : BasePigment
	{
		[Constructable]
		public AlbatrePigment() : base(2960, "Albâtre") { }
		public AlbatrePigment(Serial serial) : base(serial) { }
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

	public class IvoirePigment : BasePigment
	{
		[Constructable]
		public IvoirePigment() : base(2961, "Ivoire") { }
		public IvoirePigment(Serial serial) : base(serial) { }
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
	public class BlancVieillitPigment : BasePigment
	{
		[Constructable]
		public BlancVieillitPigment() : base(2969, "Blanc vieillit") { }
		public BlancVieillitPigment(Serial serial) : base(serial) { }
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

	// Bicolor
	public class TerreDeSiennePigment : BasePigment
	{
		[Constructable]
		public TerreDeSiennePigment() : base(2351, "Terre de sienne") { }
		public TerreDeSiennePigment(Serial serial) : base(serial) { }
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

	public class FinDuMondePigment : BasePigment
	{
		[Constructable]
		public FinDuMondePigment() : base(2900, "Fin du monde") { }
		public FinDuMondePigment(Serial serial) : base(serial) { }
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

	public class AntiquePigment : BasePigment
	{
		[Constructable]
		public AntiquePigment() : base(2908, "Antique") { }
		public AntiquePigment(Serial serial) : base(serial) { }
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

	public class AuroreBorealePigment : BasePigment
	{
		[Constructable]
		public AuroreBorealePigment() : base(2921, "Aurore boréale") { }
		public AuroreBorealePigment(Serial serial) : base(serial) { }
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

	public class ChartreusePigment : BasePigment
	{
		[Constructable]
		public ChartreusePigment() : base(2949, "Chartreuse") { }
		public ChartreusePigment(Serial serial) : base(serial) { }
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

