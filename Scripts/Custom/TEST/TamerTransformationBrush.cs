using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using System.Linq;

namespace Server.Items
{
	public class TamerTransformBrush : Item
	{
		private static Dictionary<string, int[]> AnimalPacks = new Dictionary<string, int[]>
		{
			{ "P1", new int[] { 5, 217, 726, 25 } },
			{ "P2", new int[] { 58, 254, 729, 29 } },
			{ "P3", new int[] { 81, 302, 731, 74 } },
			{ "P4", new int[] { 88, 317, 734, 234 } },
			{ "P5", new int[] { 201, 269, 715 , 133 } },
			{ "P6", new int[] { 203, 736, 737, 293 } },
			{ "P7", new int[] { 215, 716, 739, 80 } },
			{ "M1", new int[] { 11, 63, 211, 233, 273 } },
			{ "M2", new int[] { 20, 74, 212, 234, 786 } },
			{ "M3", new int[] { 21, 80, 213, 237, 788 } },
			{ "M4", new int[] { 25, 133, 216, 242, 293 } },
			{ "M5", new int[] { 28, 134, 225, 246, 714 } },
			{ "M6", new int[] { 29, 157, 231, 248, 720 } },
			{ "M7", new int[] { 48, 167, 232, 251, 730 } },
			{ "G1", new int[] { 246, 248, 60, 315 } },
			{ "G2", new int[] { 242, 251, 61, 244 } },
			{ "G3", new int[] { 273, 786, 62, 715 } },
			{ "G4", new int[] { 720, 788, 104, 787 } },
			{ "G5", new int[] { 730, 293, 265, 832 } },
			{ "TG1", new int[] { 832, 12, 173, 798 } },
			{ "TG2", new int[] { 244, 59, 735, 197 } },
			{ "TG3", new int[] { 104, 103, 796, 198 } },
			{ "TG4", new int[] { 832, 172, 260, 826 } }
		};

		private static HashSet<int> NonTransformableBodyValues = new HashSet<int>
		{
			203, 254, 269, 715, 734
		};

		[Constructable]
		public TamerTransformBrush() : base(0x1372)
		{
			Name = "Brosse de Transformation";
			Weight = 1.0;
		}

		public TamerTransformBrush(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
				return;
			}

			from.SendMessage("Choisissez la créature à transformer.");
			from.Target = new InternalTarget(this);
		}

		private class InternalTarget : Target
		{
			private TamerTransformBrush m_Brush;

			public InternalTarget(TamerTransformBrush brush) : base(10, false, TargetFlags.None)
			{
				m_Brush = brush;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is BaseCreature creature)
				{
					if (creature.Controlled && creature.ControlMaster == from)
					{
						if (creature is IMount)
						{
							from.SendMessage("Les montures ne peuvent pas être transformées.");
							return;
						}

						if (NonTransformableBodyValues.Contains(creature.BodyValue))
						{
							from.SendMessage("Cette créature ne peut pas être transformée.");
							return;
						}

						int[] bodyValues = GetAvailableBodyValues(creature.BodyValue);
						if (bodyValues != null)
						{
							from.SendGump(new TransformGump(from, creature, bodyValues, m_Brush));
						}
						else
						{
							from.SendMessage("Cette créature ne peut pas être transformée.");
						}
					}
					else
					{
						from.SendMessage("Vous ne pouvez transformer que vos propres créatures apprivoisées.");
					}
				}
				else
				{
					from.SendMessage("Vous ne pouvez transformer que des créatures apprivoisées.");
				}
			}
		}

		private static int[] GetAvailableBodyValues(int currentBodyValue)
		{
			foreach (var pack in AnimalPacks)
			{
				if (pack.Value.Contains(currentBodyValue))
				{
					return pack.Value;
				}
			}
			return null;
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class TransformGump : Gump
	{

		private static Dictionary<int, string> CreatureNames = new Dictionary<int, string>
	{
		{5, "Aigle"}, {6, "Oiseau"}, {11, "Araignée bleu"}, {12, "Dragon gris"}, {20, "Araignée violette"},
		{21, "Serpent géant"}, {25, "Loup gris"}, {28, "Araignée brune"}, {29, "Gorille"}, {48, "Scorpion"},
		{52, "Serpent"}, {58, "Wisp"}, {59, "Dragon rouge"}, {60, "Dragonneau gris"}, {61, "Dragon rouge"},
		{62, "Wyvern"}, {63, "Cougar"}, {74, "Diablotin"}, {80, "Grenouille géante"}, {81, "Grenouille"},
		{88, "Chèvre"}, {103, "Dragon serpentin"}, {104, "Dragon squelette"}, {133, "Alligator"},
		{134, "Crocodile"}, {157, "Araignée noire"}, {167, "Ours brun"}, {172, "Dragon orange"},
		{173, "Araignée gigantesque"}, {197, "Dragon cornu rouge"}, {198, "Dragon épineux noir"},
		{201, "Chat"}, {203, "Cochon"}, {205, "Lapin"}, {207, "Mouton"}, {208, "Poule"}, {211, "Ours noir"},
		{212, "Grizzly"}, {213, "Ours polaire"}, {215, "Rat géant"}, {216, "Vache blanc/noir"},
		{217, "Chien"}, {225, "Loup brun"}, {231, "Vache blanc/brun"}, {232, "Boeuf brun"},
		{233, "Boeuf tache"}, {234, "Cerf"}, {237, "Faon"}, {238, "Rat"}, {242, "Insecte"},
		{244, "Scarabé runique"}, {246, "Renard 9 queues"}, {248, "Yak"}, {251, "Bull"}, {254, "Dodo"},
		{260, "Elephant"}, {265, "Hydre"}, {269, "Bébé tigre"}, {273, "Tigre"}, {278, "Écureuil"},
		{279, "Belette"}, {282, "Perroquet"}, {283, "Corbeau"}, {293, "Dragonnet"}, {302, "Skittering"},
		{315, "Insecte plat"}, {317, "Chauve souris"}, {714, "Crabe"}, {715, "Mini raptor"},
		{716, "Mini scorpion"}, {717, "Lézard volant"}, {720, "Salamendre"}, {726, "Serpent cornu"},
		{727, "Petit serpent cornu"}, {729, "Crabe rouge"}, {730, "Raptor"}, {731, "Limace"},
		{733, "Lézard ailé"}, {734, "Basilic"}, {735, "Araignée velue gigantesque"}, {736, "Araignée loup"},
		{737, "Tarentule dodue"}, {738, "Mythe"}, {739, "Chien du désert"}, {786, "Lion"}, {787, "Fourmillon"},
		{788, "Husky"}, {796, "Giga bleu"}, {798, "Wyrm"}, {826, "Dragon légendaire"}, {831, "Perroquet"},
		{832, "Phoenix"}
	};
		private Mobile m_From;
		private BaseCreature m_Creature;
		private int[] m_BodyValues;
		private TamerTransformBrush m_Brush;

		public TransformGump(Mobile from, BaseCreature creature, int[] bodyValues, TamerTransformBrush brush)
		   : base(50, 50)
		{
			m_From = from;
			m_Creature = creature;
			m_BodyValues = bodyValues;
			m_Brush = brush;

			AddPage(0);
			AddBackground(0, 0, 450, 450, 5054);
			AddBackground(10, 10, 430, 430, 3000);

			AddHtml(20, 20, 410, 25, "<CENTER><BIG>Menu de transformation</BIG></CENTER>", false, false);


			AddHtmlLocalized(20, 30, 400, 25, 1154037, false, false); // Choose a new form:

			AddButton(20, 400, 4005, 4007, 0, GumpButtonType.Reply, 0);
			AddHtmlLocalized(55, 400, 200, 25, 1011012, false, false); // CANCEL

			AddPage(1);

			int[] tamingRequirements = GetTamingRequirements(bodyValues.Length);

			for (int i = 0; i < bodyValues.Length; i++)
			{
				int x = 30 + ((i % 2) * 200);
				int y = 85 + ((i / 2) * 100);

				AddRadio(x, y, 210, 211, false, i + 1);

				int shrinkID = ShrinkTable.Lookup(bodyValues[i]);
				AddItem(x + 40, y, shrinkID);

				string creatureName = CreatureNames.TryGetValue(bodyValues[i], out string name) ? name : "Inconnu";
				AddLabel(x + 80, y - 20, 0, creatureName);
				AddLabel(x + 80, y, 0, $"Body ID: {bodyValues[i]}");
				AddLabel(x + 80, y + 20, 0, $"Taming requis: {tamingRequirements[i]}");
			}

			AddButton(180, 400, 4005, 4007, 1, GumpButtonType.Reply, 0);
			AddHtmlLocalized(215, 400, 200, 25, 1011036, false, false); // OKAY
		}

		private int[] GetTamingRequirements(int packSize)
		{
			if (packSize == 4)
				return new int[] { 30, 50, 70, 80 };
			else if (packSize == 5)
				return new int[] { 30, 45, 60, 70, 80 };
			else
				return Enumerable.Repeat(30, packSize).ToArray(); // Default to 30 for all if pack size is unexpected
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;
			if (info.ButtonID == 0)
				return;

			int[] switches = info.Switches;
			if (switches.Length > 0)
			{
				int index = switches[0] - 1;
				if (index >= 0 && index < m_BodyValues.Length)
				{
					int[] tamingRequirements = GetTamingRequirements(m_BodyValues.Length);
					int requiredSkill = tamingRequirements[index];

					if (from.Skills[SkillName.AnimalTaming].Base >= requiredSkill)
					{
						m_Creature.BodyValue = m_BodyValues[index];
						from.SendMessage($"Vous avez transformé votre créature en Body ID {m_BodyValues[index]}.");
						m_Brush.Delete();
					}
					else
					{
						from.SendMessage($"Vous avez besoin de {requiredSkill} en Taming pour effectuer cette transformation.");
					}
				}
			}
		}
	}

	public static class ShrinkTableCustom
	{
		private static Dictionary<int, int> m_Table = new Dictionary<int, int>();

		static ShrinkTableCustom()
		{
			m_Table[5] = 0x211D;    // Aigle
			m_Table[11] = 0x25C4;   // Ours
			m_Table[12] = 0x20D6;   // Humain
			m_Table[20] = 0x25C5;   // Orque
			m_Table[21] = 0x25BF;   // Gorille
			m_Table[25] = 0x25D1;   // Ours brun
			m_Table[28] = 0x25C6;   // Panthère
			m_Table[29] = 0x2592;   // Rat
			m_Table[48] = 0x25B9;   // Scorpion
			m_Table[58] = 0x2100;   // Cheval
			m_Table[59] = 0x20D6;   // Humain
			m_Table[60] = 0x20D6;   // Humain
			m_Table[61] = 0x20D6;   // Humain
			m_Table[62] = 0x20D6;   // Humain
			m_Table[63] = 0x2583;   // Ours
			m_Table[74] = 0x259F;   // Chat
			m_Table[80] = 0x258C;   // Alligator
			m_Table[81] = 0x2130;   // Cochon
			m_Table[88] = 0x2108;   // Cheval
			m_Table[103] = 0x20D6;  // Humain
			m_Table[104] = 0x20D6;  // Humain
			m_Table[133] = 0x2131;  // Lama
			m_Table[134] = 0x25A1;  // Loup
			m_Table[157] = 0x25C3;  // Loup
			m_Table[167] = 0x2118;  // Ours
			m_Table[172] = 0x20D6;  // Humain
			m_Table[173] = 0x25C3;  // Loup
			m_Table[197] = 0x20D6;  // Humain
			m_Table[198] = 0x20D6;  // Humain
			m_Table[201] = 0x211B;  // Aigle
			m_Table[211] = 0x2118;  // Ours
			m_Table[212] = 0x211E;  // Ours polaire
			m_Table[213] = 0x20E1;  // Ours polaire
			m_Table[215] = 0x20D0;  // Lapin
			m_Table[216] = 0x2103;  // Mouton
			m_Table[217] = 0x2588;  // Chien
			m_Table[225] = 0x25D3;  // Loup
			m_Table[231] = 0x20EF;  // Loup
			m_Table[232] = 0x20EF;  // Loup
			m_Table[233] = 0x20F0;  // Loup
			m_Table[234] = 0x20D4;  // Loup
			m_Table[237] = 0x20D4;  // Loup
			m_Table[242] = 0x2765;  // Aigle
			m_Table[244] = 0x276F;  // Aigle
			m_Table[246] = 0x2763;  // Aigle
			m_Table[248] = 0x2768;  // Aigle
			m_Table[251] = 0x276E;  // Aigle
			m_Table[254] = 0x2764;  // Aigle
			m_Table[265] = 0x2765;  // Aigle (même que 242)
			m_Table[269] = 0x2131;  // Lama
			m_Table[273] = 0x2583;  // Ours
			m_Table[293] = 0x2131;  // Lama
			m_Table[302] = 0x2622;  // Dragon
			m_Table[315] = 0x262F;  // Dragon
			m_Table[317] = 0x2631;  // Dragon
			m_Table[714] = 0x4288;  // Créature spéciale
			m_Table[715] = 0x4289;  // Créature spéciale
			m_Table[716] = 0x428A;  // Créature spéciale
			m_Table[720] = 0x428E;  // Créature spéciale
			m_Table[726] = 0x4296;  // Créature spéciale
			m_Table[729] = 0x4299;  // Créature spéciale
			m_Table[730] = 0x429B;  // Créature spéciale
			m_Table[731] = 0x429D;  // Créature spéciale
			m_Table[734] = 0x42A1;  // Créature spéciale
			m_Table[735] = 0x42A2;  // Créature spéciale
			m_Table[736] = 0x42A3;  // Créature spéciale
			m_Table[737] = 0x42A4;  // Créature spéciale
			m_Table[739] = 0x42A9;  // Créature spéciale
			m_Table[786] = 0x260A;  // Créature spéciale
			m_Table[787] = 0x260F;  // Créature spéciale
			m_Table[788] = 0x2618;  // Créature spéciale
			m_Table[796] = 0x2611;  // Créature spéciale
			m_Table[798] = 0x20D6;  // Humain
			m_Table[826] = 0x42A6;  // Créature spéciale
			m_Table[832] = 0x211A;  // Aigle
		}

		public static int Lookup(int bodyValue)
		{
			if (m_Table.TryGetValue(bodyValue, out int shrinkID))
				return shrinkID;

			return 0; // Retournez une valeur par défaut si le BodyValue n'est pas trouvé
		}
	}

}
