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
			Rubis,
			Ambre,
			Tourmaline,
			Saphire,
			Emeraude,
			Amethyste,
			SaphireEtoile,
			Diamant
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
		public int SuccessChance
		{
			get { return m_SuccessChance; }
			set { m_SuccessChance = value; InvalidateProperties(); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int AshQuantity
		{
			get { return m_AshQuantity; }
			set { m_AshQuantity = value; InvalidateProperties(); }
		}

		[Constructable]
		public GolemCrystal(CrystalType type) : base(0x1F1C)
		{
			Type = type;
			SuccessChance = GetSuccessChanceForType(type);
			UpdateName();
		}

		public GolemCrystal(Serial serial) : base(serial) { }

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add(1060658, "Chance de réussite\t{0}%", SuccessChance.ToString());
		}

		private void UpdateName()
		{
			Name = $"Cristal de {GetFrenchName(Type)}";
		}

		private string GetFrenchName(CrystalType type)
		{
			switch (type)
			{
				case CrystalType.Citrine: return "Citrine";
				case CrystalType.Rubis: return "Rubis";
				case CrystalType.Ambre: return "Ambre";
				case CrystalType.Tourmaline: return "Tourmaline";
				case CrystalType.Saphire: return "Saphir";
				case CrystalType.Emeraude: return "Émeraude";
				case CrystalType.Amethyste: return "Améthyste";
				case CrystalType.SaphireEtoile: return "Saphir Étoilé";
				case CrystalType.Diamant: return "Diamant";
				default: return "Inconnu";
			}
		}

		private int GetSuccessChanceForType(CrystalType type)
		{
			switch (type)
			{
				case CrystalType.Citrine: return 75;
				case CrystalType.Rubis:
				case CrystalType.Ambre:
				case CrystalType.Tourmaline: return 85;
				case CrystalType.Saphire:
				case CrystalType.Emeraude:
				case CrystalType.Amethyste: return 90;
				case CrystalType.SaphireEtoile: return 95;
				case CrystalType.Diamant: return 100;
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
			UpdateName();
		}



		public class GolemCreationGump : BaseProjectMGump
		{
			private Mobile m_From;
			private GolemCrystal m_Crystal;

			public GolemCreationGump(Mobile from, GolemCrystal crystal)
				: base("Création de Golem", 560, 622, false)
			{
				m_From = from;
				m_Crystal = crystal;

				int x = XBase;
				int y = YBase;

				AddSection(x - 10, y, 605, 240, "Informations");
				AddHtmlTexte(x + 10, y + 20, 300, $"Chance de réussite: {crystal.SuccessChance}%");

				// Section des cendres
				AddSection(x - 10, y + 245, 300, 300, "Cendres");
				AddButtonHtlml(x + 10, y + 265, 1, "Choisir les cendres", "#FFFFFF");
				if (crystal.AshQuantity > 0)
				{
					int energy = crystal.AshQuantity * 5;
					AddHtmlTexte(x + 10, y + 285, 280, $"Quantité: {crystal.AshQuantity} {GetAshTypeName(crystal.AshType)}");
					AddHtmlTexte(x + 10, y + 305, 280, $"Énergie: {energy}");
					AddHtmlTexte(x + 10, y + 325, 280, $"Pouvoir: {GetAshTypeName(crystal.AshType)}");
					AddHtmlTexte(x + 10, y + 345, 280, GolemAsh.GetAshBonusDescription(crystal.AshType));
				}

				// Section de l'esprit
				AddSection(x + 295, y + 245, 300, 300, "Esprit");
				AddButtonHtlml(x + 305, y + 265, 2, "Choisir l'esprit", "#FFFFFF");
				if (crystal.m_Spirit != null)
				{
					AddHtmlTexte(x + 305, y + 285, 140, $"STR: {crystal.m_Spirit.GetStrength()}");
					AddHtmlTexte(x + 445, y + 285, 140, $"Wrestling: {crystal.m_Spirit.GetSkillValue(SkillName.Wrestling):F1}");
					AddHtmlTexte(x + 305, y + 305, 140, $"DEX: {crystal.m_Spirit.GetDexterity()}");
					AddHtmlTexte(x + 445, y + 305, 140, $"Tactics: {crystal.m_Spirit.GetSkillValue(SkillName.Tactics):F1}");
					AddHtmlTexte(x + 305, y + 325, 140, $"INT: {crystal.m_Spirit.GetIntelligence()}");
					AddHtmlTexte(x + 445, y + 325, 140, $"Magic Resist: {crystal.m_Spirit.GetSkillValue(SkillName.MagicResist):F1}");
					AddHtmlTexte(x + 305, y + 345, 140, $"Armor: {crystal.m_Spirit.GetAR()}");
				}

				// Bouton de construction
				AddBackground(x - 10, y + 550, 605, 55, 9270);
				AddButtonHtlml(x + 150, y + 568, 3, "Construire le Golem", "#FFFFFF");
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



