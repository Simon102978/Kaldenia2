using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using System.Collections.Generic;
using static GolemAsh;

namespace Server.Custom
{
	public class GolemCrystal : Item
	{

		public enum CrystalType
		{
			Citrine,
			Ruby,
			Amber,
			Tourmaline,
			Sapphire,
			Emerald,
			Amethyst,
			StarSapphire,
			Diamond
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public CrystalType Type { get; set; }

	

		private GolemAsh.AshType m_AshType;

		[CommandProperty(AccessLevel.GameMaster)]
		public GolemAsh.AshType AshType
		{
			get { return m_AshType; }
			set { m_AshType = value; InvalidateProperties(); }
		}
		private int m_SuccessChance;
		private int m_AshQuantity;
		private CreatureSpirit m_Spirit;

		[CommandProperty(AccessLevel.GameMaster)]
		public int SuccessChance { get { return m_SuccessChance; } set { m_SuccessChance = value; InvalidateProperties(); } }

	

		[CommandProperty(AccessLevel.GameMaster)]
		public int AshQuantity { get { return m_AshQuantity; } set { m_AshQuantity = value; InvalidateProperties(); } }

		[Constructable]
		public GolemCrystal(CrystalType type) : base(0x1F1C)
		{
			Type = type;
			Name = $"Cristal de {Type}";
			SuccessChance = GetSuccessChanceForType(type);
		}

		public GolemCrystal(Serial serial) : base(serial) { }

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add($"Chance de réussite: {SuccessChance}%");
		}

		private int GetSuccessChanceForType(CrystalType type)
		{
			switch (type)
			{
				case CrystalType.Citrine: return 75;
				case CrystalType.Ruby:
				case CrystalType.Amber:
				case CrystalType.Tourmaline: return 85;
				case CrystalType.Sapphire:
				case CrystalType.Emerald:
				case CrystalType.Amethyst: return 90;
				case CrystalType.StarSapphire: return 95;
				case CrystalType.Diamond: return 100;
				default: return 0;
			}
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
			writer.Write((int)Type);
			writer.Write(SuccessChance); 
			writer.Write((int)m_AshType);
			writer.Write(m_AshQuantity);
			writer.Write(m_Spirit);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Type = (CrystalType)reader.ReadInt();
			SuccessChance = reader.ReadInt(); 
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

				AddHtml(10, 12, 580, 20, $"<CENTER><BASEFONT COLOR=#FFFFFF>Création de Golem : {crystal.SuccessChance}% chance de réussite</BASEFONT></CENTER>", false, false);

				AddButton(20, 40, 4005, 4007, 1, GumpButtonType.Reply, 0);
				AddHtml(55, 40, 200, 20, $"<BASEFONT COLOR=#FFFFFF>Choisissez les cendres à utiliser: {crystal.AshQuantity} {GetAshTypeName(crystal.AshType)}</BASEFONT>", false, false);

				AddHtml(20, 70, 560, 100, $"<BASEFONT COLOR=#FFFFFF>Énergie : {GetEnergyForAshType(crystal.AshType)}<br>Pouvoir : {GetPowerForAshType(crystal.AshType)}<br><br>{GolemAsh.GetAshBonusDescription(crystal.AshType)}</BASEFONT>", false, false);

				AddButton(20, 180, 4005, 4007, 2, GumpButtonType.Reply, 0);
				AddHtml(55, 180, 200, 20, $"<BASEFONT COLOR=#FFFFFF>Choisissez l'esprit à utiliser: {(crystal.m_Spirit != null ? "Sélectionné" : "Non sélectionné")}</BASEFONT>", false, false);

				if (crystal.m_Spirit != null)
				{
					AddHtml(20, 210, 560, 100, $"<BASEFONT COLOR=#FFFFFF>STR: {crystal.m_Spirit.GetStrength()} DEX: {crystal.m_Spirit.GetDexterity()} INT: {crystal.m_Spirit.GetIntelligence()}<br>Armor: {crystal.m_Spirit.GetAR()}<br>Wrestling: {crystal.m_Spirit.GetSkillValue(SkillName.Wrestling)} Tactics: {crystal.m_Spirit.GetSkillValue(SkillName.Tactics)} MagicResist: {crystal.m_Spirit.GetSkillValue(SkillName.MagicResist)}</BASEFONT>", false, false);
				}

				AddButton(250, 320, 4005, 4007, 3, GumpButtonType.Reply, 0);
				AddHtml(285, 320, 200, 20, "<BASEFONT COLOR=#FFFFFF>Construire le Golem</BASEFONT>", false, false);
			}

			public override void OnResponse(NetState sender, RelayInfo info)
			{
				Mobile from = sender.Mobile;

				switch (info.ButtonID)
				{
					case 1: // Choisir les cendres
						from.Target = new InternalAshTarget(m_Crystal);
						from.SendMessage("Choisissez les cendres à utiliser.");
						break;
					case 2: // Choisir l'esprit
						from.Target = new InternalSpiritTarget(m_Crystal);
						from.SendMessage("Choisissez l'esprit à utiliser.");
						break;
					case 3: // Construire le Golem
						TryCreateGolem(from);
						break;
				}
			}

			private void TryCreateGolem(Mobile from)
			{
				if (m_Crystal.m_Spirit == null || m_Crystal.AshQuantity == 0)
				{
					from.SendMessage("Vous devez sélectionner un esprit et des cendres.");
					from.SendGump(new GolemCreationGump(from, m_Crystal));
					return;
				}

				if (m_Crystal.m_Spirit.Percentage < 100)
				{
					from.SendMessage("L'esprit doit être complet (100 sur 100) pour être utilisé.");
					from.SendGump(new GolemCreationGump(from, m_Crystal));
					return;
				}

				GolemAsh ash = from.Backpack.FindItemByType<GolemAsh>();
				if (ash == null || ash.Amount < m_Crystal.AshQuantity)
				{
					from.SendMessage("Vous n'avez pas assez de cendres.");
					from.SendGump(new GolemCreationGump(from, m_Crystal));
					return;
				}

				if (Utility.RandomDouble() * 100 < m_Crystal.SuccessChance)
				{
					GolemZyX golem = new GolemZyX(m_Crystal.m_Spirit, m_Crystal.AshType, m_Crystal.AshQuantity, from);
					golem.MoveToWorld(from.Location, from.Map);
					from.SendMessage("Vous avez créé un Golem avec succès!");
				}
				else
				{
					from.SendMessage("La création du Golem a échoué.");
				}

				// Consommer les ressources
				ash.Consume(m_Crystal.AshQuantity);
				m_Crystal.m_Spirit.Delete();
				m_Crystal.Delete();
			}

			private string GetAshTypeName(GolemAsh.AshType ashType)
			{
				switch (ashType)
				{
					case GolemAsh.AshType.Feu: return "Cendre de Feu";
					case GolemAsh.AshType.Eau: return "Cendre d'Eau";
					case GolemAsh.AshType.Glace: return "Cendre de Glace";
					case GolemAsh.AshType.Poison: return "Cendre de Poison";
					case GolemAsh.AshType.Sang: return "Cendre de Sang";
					case GolemAsh.AshType.Sylvestre: return "Cendre Sylvestre";
					case GolemAsh.AshType.Terre: return "Cendre de Terre";
					case GolemAsh.AshType.Vent: return "Cendre de Vent";
					default: return "Inconnu";
				}
			}

			private string GetEnergyForAshType(GolemAsh.AshType ashType)
			{
				// Implémentez cette méthode selon vos besoins
				return "À définir";
			}

			private string GetPowerForAshType(GolemAsh.AshType ashType)
			{
				// Implémentez cette méthode selon vos besoins
				return "À définir";
			}
		}

		private class InternalAshTarget : Target
		{
			private GolemCrystal m_Crystal;

			public InternalAshTarget(GolemCrystal crystal) : base(12, false, TargetFlags.None)
			{
				m_Crystal = crystal;
			}


			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is GolemAsh ash)
				{
					m_Crystal.AshType = ash.Type;
					m_Crystal.AshQuantity = ash.Amount;
					from.SendMessage($"Vous avez sélectionné {ash.Amount} {(ash.Type)}.");
				}
				else
				{
					from.SendMessage("Cela n'est pas des cendres valides.");
				}
				from.SendGump(new GolemCreationGump(from, m_Crystal));
			}
		}

		private string GetAshTypeName(GolemAsh.AshType ashType)
		{
			switch (ashType)
			{
				case GolemAsh.AshType.Feu: return "Cendre de Feu";
				case GolemAsh.AshType.Eau: return "Cendre d'Eau";
				case GolemAsh.AshType.Glace: return "Cendre de Glace";
				case GolemAsh.AshType.Poison: return "Cendre de Poison";
				case GolemAsh.AshType.Sang: return "Cendre de Sang";
				case GolemAsh.AshType.Sylvestre: return "Cendre Sylvestre";
				case GolemAsh.AshType.Terre: return "Cendre de Terre";
				case GolemAsh.AshType.Vent: return "Cendre de Vent";
				default: return "Inconnu";
			}
		}

		private class InternalSpiritTarget : Target
		{
			private GolemCrystal m_Crystal;

			public InternalSpiritTarget(GolemCrystal crystal) : base(12, false, TargetFlags.None)
			{
				m_Crystal = crystal;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is CreatureSpirit spirit)
				{
					if (spirit.Percentage < 100)
					{
						from.SendMessage("L'esprit doit être complet (100 sur 100) pour être utilisé.");
					}
					else
					{
						m_Crystal.m_Spirit = spirit;
						from.SendMessage("Vous avez sélectionné un esprit de créature.");
					}
				}
				else
				{
					from.SendMessage("Cela n'est pas un esprit de créature valide.");
				}

				from.SendGump(new GolemCreationGump(from, m_Crystal));
			}
		}
	}
}



