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

		[CommandProperty(AccessLevel.GameMaster)]
		public int SuccessChance { get { return m_SuccessChance; } set { m_SuccessChance = value; InvalidateProperties(); } }

		[Constructable]
		public GolemCrystal(int successChance) : base(0x1F1C) // magical crystal
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
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_SuccessChance = reader.ReadInt();
		}
	}

	public class GolemCreationGump : Gump
	{
		private Mobile m_From;
		private GolemCrystal m_Crystal;
		private GolemAsh m_SelectedAsh;
		private CreatureSpirit m_SelectedSpirit;

		public GolemCreationGump(Mobile from, GolemCrystal crystal) : base(50, 50)
		{
			m_From = from;
			m_Crystal = crystal;

			AddPage(0);
			AddBackground(0, 0, 400, 400, 9270);

			AddHtml(10, 10, 380, 20, $"<CENTER>Création de Golem : {crystal.SuccessChance}% chance de réussite</CENTER>", false, false);

			AddButton(10, 40, 4005, 4007, 1, GumpButtonType.Reply, 0);
			AddHtml(45, 40, 300, 20, m_SelectedAsh == null ? "Choisissez les cendres à utiliser" : $"Cendres sélectionnées : {m_SelectedAsh.Quantity} {m_SelectedAsh.Type}", false, false);

			AddButton(10, 70, 4005, 4007, 2, GumpButtonType.Reply, 0);
			AddHtml(45, 70, 300, 20, m_SelectedSpirit == null ? "Choisissez l'esprit à utiliser" : "Esprit sélectionné", false, false);

			if (m_SelectedAsh != null && m_SelectedSpirit != null)
			{
				AddButton(10, 350, 4005, 4007, 3, GumpButtonType.Reply, 0);
				AddHtml(45, 350, 300, 20, "Lancer la création du Golem", false, false);
			}
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
					if (m_SelectedAsh != null && m_SelectedSpirit != null)
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
				Golem golem = new Golem(m_SelectedAsh.Type);
				GolemAsh.ApplyAshBonuses(golem, m_SelectedAsh.Type, m_SelectedAsh.Quantity, (int)from.Skills[SkillName.Inscribe].Value);
				// Appliquer les bonus de l'esprit ici

				MiniGolem miniGolem = new MiniGolem(golem, m_SelectedAsh.Type);
				golem.MiniGolem = miniGolem;

				if (from.AddToBackpack(miniGolem))
				{
					from.SendMessage("Vous avez créé un Golem avec succès !");
					m_Crystal.Delete();
					m_SelectedAsh.Delete();
					m_SelectedSpirit.Delete();
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
				from.SendMessage("La création du Golem a échoué. Tous les matériaux ont été détruits.");
				m_Crystal.Delete();
				m_SelectedAsh.Delete();
				m_SelectedSpirit.Delete();
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
					m_Gump.m_SelectedAsh = ash;
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
				if (targeted is CreatureSpirit spirit)
				{
					m_Gump.m_SelectedSpirit = spirit;
					from.SendGump(new GolemCreationGump(from, m_Gump.m_Crystal));
				}
				else
				{
					from.SendMessage("Ceci n'est pas un esprit de créature.");
					from.SendGump(m_Gump);
				}
			}
		}
	}
}