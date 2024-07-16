using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Custom
{
	public class GolemCrystal : Item
	{
		private int m_SuccessChance;
		private GolemAsh.AshType m_AshType;
		private int m_AshQuantity;
		private CreatureSpirit m_Spirit;

		[CommandProperty(AccessLevel.GameMaster)]
		public int SuccessChance { get { return m_SuccessChance; } set { m_SuccessChance = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.GameMaster)]
		public GolemAsh.AshType AshType { get { return m_AshType; } set { m_AshType = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int AshQuantity { get { return m_AshQuantity; } set { m_AshQuantity = value; InvalidateProperties(); } }

		[Constructable]
		public GolemCrystal(int successChance) : base(0x1F1C)
		{
			Name = "Cristal de Golem";
			m_SuccessChance = successChance;
		}

		public GolemCrystal(Serial serial) : base(serial) { }

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add($"Chance de réussite: {m_SuccessChance}%");
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
				return;
			}

			from.SendGump(new GolemCreationGump(from, this));
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write(m_SuccessChance);
			writer.Write((int)m_AshType);
			writer.Write(m_AshQuantity);
			writer.Write(m_Spirit);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_SuccessChance = reader.ReadInt();
			m_AshType = (GolemAsh.AshType)reader.ReadInt();
			m_AshQuantity = reader.ReadInt();
			m_Spirit = reader.ReadItem() as CreatureSpirit;
		}

		public class GolemCreationGump : Gump
		{
			private Mobile m_From;
			private GolemCrystal m_Crystal;

			public GolemCreationGump(Mobile from, GolemCrystal crystal) : base(50, 50)
			{
				m_From = from;
				m_Crystal = crystal;

				AddPage(0);
				AddBackground(0, 0, 600, 450, 9270);
				AddImageTiled(10, 10, 580, 430, 2624);
				AddAlphaRegion(10, 10, 580, 430);

				AddImage(0, 0, 10440);
				AddImage(554, 0, 10441);
				AddImage(0, 405, 10442);
				AddImage(554, 405, 10443);

				AddHtml(10, 12, 580, 20, "<CENTER><BASEFONT COLOR=#FFFFFF>Création de Golem</BASEFONT></CENTER>", false, false);
				AddHtml(10, 40, 580, 20, $"<CENTER><BASEFONT COLOR=#FFFFFF>Chance de réussite : {crystal.SuccessChance}%</BASEFONT></CENTER>", false, false);

				// Cendres
				AddImage(20, 70, 2086);
				AddButton(60, 75, 4005, 4007, 1, GumpButtonType.Reply, 0);
				AddHtml(95, 75, 300, 20, m_Crystal.AshQuantity == 0 ? "<BASEFONT COLOR=#FFFFFF>Choisissez les cendres à utiliser</BASEFONT>" : $"<BASEFONT COLOR=#FFFFFF>Cendres sélectionnées : {m_Crystal.AshQuantity} {m_Crystal.AshType}</BASEFONT>", false, false);

				if (m_Crystal.AshQuantity > 0)
				{
					AddImageTiled(20, 100, 560, 100, 3004);
					AddAlphaRegion(20, 100, 560, 100);
					AddHtml(30, 105, 540, 20, $"<BASEFONT COLOR=#FFFFFF>Energie: {m_Crystal.AshQuantity} cendre * 7 energie = {m_Crystal.AshQuantity * 7}</BASEFONT>", false, false);
					AddHtml(30, 125, 540, 20, $"<BASEFONT COLOR=#FFFFFF>Pouvoir: {m_Crystal.AshType}</BASEFONT>", false, false);
					AddHtml(30, 145, 540, 50, $"<BASEFONT COLOR=#FFFFFF>{GetAshBonusDescription(m_Crystal.AshType)}</BASEFONT>", false, false);
				}

				// Esprit
				AddImage(20, 210, 2086);
				AddButton(60, 215, 4005, 4007, 2, GumpButtonType.Reply, 0);
				AddHtml(95, 215, 300, 20, m_Crystal.m_Spirit == null ? "<BASEFONT COLOR=#FFFFFF>Choisissez l'esprit à utiliser</BASEFONT>" : "<BASEFONT COLOR=#FFFFFF>Esprit sélectionné</BASEFONT>", false, false);

				if (m_Crystal.m_Spirit != null)
				{
					AddImageTiled(20, 240, 560, 120, 3004);
					AddAlphaRegion(20, 240, 560, 120);
					AddHtml(30, 245, 540, 110, $"<BASEFONT COLOR=#FFFFFF>{GetSpiritDescription(m_Crystal.m_Spirit)}</BASEFONT>", false, false);
				}

				if (m_Crystal.AshQuantity > 0 && m_Crystal.m_Spirit != null)
				{
					AddButton(250, 380, 4005, 4007, 3, GumpButtonType.Reply, 0);
					AddHtml(285, 380, 300, 20, "<BASEFONT COLOR=#FFFFFF>Construire le Golem</BASEFONT>", false, false);
				}
			}

			private string GetAshBonusDescription(GolemAsh.AshType ashType)
			{
				// Implémentez cette méthode pour décrire les bonus de chaque type de cendre
				return "Description des bonus de cendres";
			}

			private string GetSpiritDescription(CreatureSpirit spirit)
			{
				// Implémentez cette méthode pour décrire les attributs de l'esprit
				return "Description de l'esprit";
			}

			public override void OnResponse(NetState sender, RelayInfo info)
			{
				Mobile from = sender.Mobile;
				switch (info.ButtonID)
				{
					case 1: // Sélection des cendres
						from.SendMessage("Sélectionnez les cendres à utiliser.");
						from.Target = new AshSelection(this);
						break;
					case 2: // Sélection de l'esprit
						from.SendMessage("Sélectionnez l'esprit à utiliser.");
						from.Target = new SpiritSelection(this);
						break;
					case 3: // Création du Golem
						if (m_Crystal.AshQuantity > 0 && m_Crystal.m_Spirit != null)
						{
							CreateGolem(from);
						}
						break;
				}
			}

			private void CreateGolem(Mobile from)
			{
				if (Utility.RandomDouble() * 100 < m_Crystal.SuccessChance)
				{
					Golem golem = new Golem(m_Crystal.AshType, from);  // Passez 'from' comme propriétaire
					GolemAsh.ApplyAshBonuses(golem, m_Crystal.AshType, m_Crystal.AshQuantity, (int)from.Skills[SkillName.Inscribe].Value);

					// Appliquer les bonus de l'esprit ici

					MiniGolem miniGolem = new MiniGolem(golem, m_Crystal.AshType);
					golem.MiniGolem = miniGolem;

					if (from.AddToBackpack(miniGolem))
					{
						from.SendMessage("Vous avez créé un Golem avec succès !");
						m_Crystal.Delete();
					}
					else
					{
						golem.Delete();
						miniGolem.Delete();
						from.SendMessage("Vous n'avez pas assez de place dans votre sac pour le Mini Golem.");
					}
				}
				else
				{
					from.SendMessage("La création du Golem a échoué. Le cristal a été détruit.");
					m_Crystal.Delete();
				}
			}

			private class AshSelection : Target
			{
				private GolemCreationGump m_Gump;

				public AshSelection(GolemCreationGump gump) : base(-1, false, TargetFlags.None)
				{
					m_Gump = gump;
				}

				protected override void OnTarget(Mobile from, object targeted)
				{
					if (targeted is GolemAsh ash)
					{
						m_Gump.m_Crystal.AshType = ash.Type;
						m_Gump.m_Crystal.AshQuantity = ash.Amount;

						if (ash.Amount > 1)
						{
							ash.Amount--;
						}
						else
						{
							ash.Delete();
						}

						from.SendGump(new GolemCreationGump(from, m_Gump.m_Crystal));
					}
					else
					{
						from.SendMessage("Ceci n'est pas des cendres de Golem.");
						from.SendGump(m_Gump);
					}
				}
			}

			private class SpiritSelection : Target
			{
				private GolemCreationGump m_Gump;

				public SpiritSelection(GolemCreationGump gump) : base(-1, false, TargetFlags.None)
				{
					m_Gump = gump;
				}

				protected override void OnTarget(Mobile from, object targeted)
				{
					if (targeted is CreatureSpirit spirit && spirit != m_Gump.m_Crystal.m_Spirit)
					{
						m_Gump.m_Crystal.m_Spirit = spirit;
						from.SendGump(new GolemCreationGump(from, m_Gump.m_Crystal));
					}
					else if (targeted == m_Gump.m_Crystal.m_Spirit)
					{
						from.SendMessage("Cet esprit est déjà sélectionné.");
						from.SendGump(m_Gump);
					}
					else
					{
						from.SendMessage("Ceci n'est pas un esprit de créature valide.");
						from.SendGump(m_Gump);
					}
				}
			}
		}
	}
}
