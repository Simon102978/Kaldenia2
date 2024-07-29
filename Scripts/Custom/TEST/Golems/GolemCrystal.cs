using System;

using Server;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Items;
using static Server.Custom.GolemZyX;

namespace Server.Custom
{
	public class GolemCrystalCitrine : GolemCrystal
	{
		[Constructable]
		public GolemCrystalCitrine() : base(CrystalType.Citrine) { }
		public GolemCrystalCitrine(Serial serial) : base(serial) { }

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

	public class GolemCrystalRubis : GolemCrystal
	{
		[Constructable]
		public GolemCrystalRubis() : base(CrystalType.Rubis) { }
		public GolemCrystalRubis(Serial serial) : base(serial) { }

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

	public class GolemCrystalAmbre : GolemCrystal
	{
		[Constructable]
		public GolemCrystalAmbre() : base(CrystalType.Ambre) { }
		public GolemCrystalAmbre(Serial serial) : base(serial) { }

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

	public class GolemCrystalTourmaline : GolemCrystal
	{
		[Constructable]
		public GolemCrystalTourmaline() : base(CrystalType.Tourmaline) { }
		public GolemCrystalTourmaline(Serial serial) : base(serial) { }

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

	public class GolemCrystalSaphire : GolemCrystal
	{
		[Constructable]
		public GolemCrystalSaphire() : base(CrystalType.Saphire) { }
		public GolemCrystalSaphire(Serial serial) : base(serial) { }

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

	public class GolemCrystalEmeraude : GolemCrystal
	{
		[Constructable]
		public GolemCrystalEmeraude() : base(CrystalType.Emeraude) { }
		public GolemCrystalEmeraude(Serial serial) : base(serial) { }

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

	public class GolemCrystalAmethyste : GolemCrystal
	{
		[Constructable]
		public GolemCrystalAmethyste() : base(CrystalType.Amethyste) { }
		public GolemCrystalAmethyste(Serial serial) : base(serial) { }

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

	public class GolemCrystalSaphireEtoile : GolemCrystal
	{
		[Constructable]
		public GolemCrystalSaphireEtoile() : base(CrystalType.SaphireEtoile) { }
		public GolemCrystalSaphireEtoile(Serial serial) : base(serial) { }

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

	public class GolemCrystalDiamant : GolemCrystal
	{
		[Constructable]
		public GolemCrystalDiamant() : base(CrystalType.Diamant) { }
		public GolemCrystalDiamant(Serial serial) : base(serial) { }

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


	public class GolemCrystal : Item
	{
		private BaseGolemAsh m_Ash;
		private int m_AshQuantity;
		private CreatureSpirit m_Spirit;

		[CommandProperty(AccessLevel.GameMaster)]
		public BaseGolemAsh Ash
		{
			get { return m_Ash; }
			set { m_Ash = value; InvalidateProperties(); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int AshQuantity
		{
			get { return m_AshQuantity; }
			set { m_AshQuantity = value; InvalidateProperties(); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public CreatureSpirit Spirit
		{
			get { return m_Spirit; }
			set { m_Spirit = value; InvalidateProperties(); }
		}

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

		private int m_SuccessChance;

		[CommandProperty(AccessLevel.GameMaster)]
		public int SuccessChance
		{
			get { return m_SuccessChance; }
			set { m_SuccessChance = value; InvalidateProperties(); }
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

			if (m_Ash != null)
			{
				list.Add($"Type de cendre: {m_Ash.GetType().Name.Replace("GolemCendre", "")}");
				list.Add($"Quantité de cendre: {m_AshQuantity}");
			}
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
			writer.Write(m_AshQuantity);
			writer.Write(m_Spirit);
			writer.Write(m_Ash);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Type = (CrystalType)reader.ReadInt();
			SuccessChance = reader.ReadInt();
			m_AshQuantity = reader.ReadInt();
			m_Spirit = reader.ReadItem() as CreatureSpirit;
			m_Ash = reader.ReadItem() as BaseGolemAsh;
			UpdateName();
		}

		public class GolemCreationGump : BaseProjectMGump
		{
			private Mobile m_From;
			private GolemCrystal m_Crystal;

			public GolemCreationGump(Mobile from, GolemCrystal crystal)
				: base("Création de Golem", 560, 450, false)
			{
				m_From = from;
				m_Crystal = crystal;

				int x = XBase;
				int y = YBase;

				AddBackground(x - 10, y, 605, 55, 9270);

				AddHtmlTexte(x + 10, y + 20, 595, $"<h3><center>Création de Golem: {crystal.SuccessChance}% Chance de réussite</center></h3>");

				string Cendredescription = "";

				if (crystal.AshQuantity > 0 && crystal.Ash != null)
				{
					int energy = crystal.AshQuantity * 5;

					Cendredescription = $"Quantité: {crystal.AshQuantity} {crystal.Ash.AshName}\n\n";
					Cendredescription += $"Énergie: {crystal.AshQuantity} cendre * 5 énergie = {energy}\n\n";
					Cendredescription += $"Pouvoir: {crystal.Ash.GetType().Name.Replace("GolemCendre", "")}\n";
					Cendredescription += GetAshBonusDescription(crystal.Ash);
				}

				// Section des cendres
				AddSection(x - 10, y + 60, 300, 300, "Cendres",Cendredescription);
			
				string Espritdescription = "";
				
				if (crystal.m_Spirit != null)
				{
					Espritdescription = $"Statistique: \nSTR: {crystal.m_Spirit.GetStrength()}" + $"\nDEX: {crystal.m_Spirit.GetDexterity()}" + $"\nINT: {crystal.m_Spirit.GetIntelligence()}" + $"\nArmor: {crystal.m_Spirit.GetAR()}\n\n" + "Compétences:\n";




				foreach (var skill in crystal.m_Spirit.Skills)
				{
					Espritdescription = Espritdescription + skill.Key + ": " + $"{skill.Value:F1}\n";
				}


				}

				AddSection(x + 295, y + 60, 300, 300, "Esprit", Espritdescription);

				// Bouton de construction
				AddSection(x - 10, y + 365, 605, 125,  "Commandes");
				AddButtonHtlml(x + 10, y + 400, 1, "Construire le Golem", "#FFFFFF");
				AddButtonHtlml(x + 10, y + 425, 2, "Choisir les cendres", "#FFFFFF");
				AddButtonHtlml(x + 10, y + 450, 3, "Choisir l'esprit", "#FFFFFF");
			}

			public override void OnResponse(NetState sender, RelayInfo info)
			{
				Mobile from = sender.Mobile;
				switch (info.ButtonID)
				{
					case 1: // Construire le Golem
						TryCreateGolem(from);
						break;
					case 2: // Choisir les cendres
						from.Target = new InternalAshTarget(m_Crystal);
						from.SendMessage("Choisissez les cendres à utiliser.");
						break;
					case 3: // Choisir l'esprit
						from.Target = new InternalSpiritTarget(m_Crystal);
						from.SendMessage("Choisissez l'esprit à utiliser.");
						break;
				}
			}

			private string GetAshBonusDescription(BaseGolemAsh ash)
			{
				if (ash is GolemCendreFeu)
					return "Bonus de 20 points à la STR\nBonus de 10 points à la Tactics\nPénalité: 4";
				else if (ash is GolemCendreEau)
					return "Bonus de 20 points à la DEX\nBonus de 10 points au Wrestling\nPénalité: 3";
				else if (ash is GolemCendreGlace)
					return "Attaque de paralysie\nBonus de 20 points au Wrestling\nPénalité: 2";
				else if (ash is GolemCendrePoison)
					return "Attaque de poison\nBonus de 20 points à la Tactics\nPénalité: 2";
				else if (ash is GolemCendreSang)
					return "Double armure\nBonus de 20 points au Wrestling\nPénalité: 2";
				else if (ash is GolemCendreSylvestre)
					return "Attaque de longue portée\nBonus de 20 points au Magic Resist\nPénalité: 3";
				else if (ash is GolemCendreTerre)
					return "Bonus de 20 points à la Tactics\nBonus de 20 points au Wrestling\nPénalité: 3";
				else if (ash is GolemCendreVent)
					return "Double DEX\nBonus de 20 points à la Tactics\nPénalité: 2";
				else
					return "Aucun bonus spécifique";
			}

			private void TryCreateGolem(Mobile from)
			{
				if (m_Crystal.Spirit == null || m_Crystal.AshQuantity == 0 || m_Crystal.Ash == null)
				{
					from.SendMessage("Vous devez sélectionner un esprit et des cendres.");
					from.SendGump(new GolemCreationGump(from, m_Crystal));
					return;
				}

				if (m_Crystal.Spirit.Percentage < 100)
				{
					from.SendMessage("L'esprit doit être complet (100 sur 100) pour être utilisé.");
					from.SendGump(new GolemCreationGump(from, m_Crystal));
					return;
				}

				BaseGolemAsh ash = from.Backpack.FindItemByType<BaseGolemAsh>();
				if (ash == null || ash.Amount < m_Crystal.AshQuantity)
				{
					from.SendMessage("Vous n'avez pas assez de cendres.");
					from.SendGump(new GolemCreationGump(from, m_Crystal));
					return;
				}

				if (Utility.RandomDouble() * 100 < m_Crystal.SuccessChance)
				{
					GolemAsh.AshType ashType = GolemAsh.GetAshTypeFromAsh(m_Crystal.Ash);
					GolemZyX golem = new GolemZyX(m_Crystal.Spirit, ashType, m_Crystal.AshQuantity, from); 
					ApplyAshBonuses(golem, m_Crystal.Ash, m_Crystal.AshQuantity, from.Skills[SkillName.AnimalTaming].Value);
					golem.MoveToWorld(from.Location, from.Map);
					from.SendMessage("Vous avez créé un Golem avec succès!");
				}
				else
				{
					from.SendMessage("La création du Golem a échoué.");
				}

				// Consommer les ressources
				m_Crystal.Ash.Consume(m_Crystal.AshQuantity);
				m_Crystal.Spirit.Delete();
				m_Crystal.Delete();
			}


			private void ApplyAshBonuses(GolemZyX golem, BaseGolemAsh ash, int ashQuantity, double animalTamingSkill)
			{
				int baseBonus = ashQuantity * 5;
				int skillBonus = (int)(animalTamingSkill * 0.1);

				if (ash is GolemCendreFeu)
				{
					golem.SetStr(golem.Str + baseBonus + 20);
					golem.Skills[SkillName.Tactics].Base += 10 + skillBonus;
					golem.SetDamageType(ResistanceType.Fire, 100);
					golem.SetResistance(ResistanceType.Fire, 50);
					golem.Penalty = 4;
				}
				if (ash is GolemCendreEau)
				{
					golem.SetDex(golem.Dex + baseBonus + 20);
					golem.Skills[SkillName.Wrestling].Base += 10 + skillBonus;
					golem.SetDamageType(ResistanceType.Cold, 100);
					golem.SetResistance(ResistanceType.Cold, 50);
					golem.Penalty = 3;
				}
				if (ash is GolemCendreGlace)
				{
					golem.Skills[SkillName.Wrestling].Base += 20 + skillBonus;
					golem.SetDamageType(ResistanceType.Cold, 100);
					golem.SetResistance(ResistanceType.Cold, 50);
					golem.Penalty = 2;
				}
				if (ash is GolemCendrePoison)
				{
					golem.Skills[SkillName.Tactics].Base += 20 + skillBonus;
					golem.SetDamageType(ResistanceType.Poison, 100);
					golem.SetResistance(ResistanceType.Poison, 50);
					golem.Penalty = 2;
				}
				if (ash is GolemCendreSang)
				{
					golem.Skills[SkillName.Wrestling].Base += 20 + skillBonus;
					golem.VirtualArmor *= 2;
					golem.SetDamageType(ResistanceType.Physical, 100);
					golem.Penalty = 2;
				}
				if (ash is GolemCendreSylvestre)
				{
					golem.Skills[SkillName.MagicResist].Base += 20 + skillBonus;
					golem.SetDamageType(ResistanceType.Energy, 100);
					golem.SetResistance(ResistanceType.Energy, 50);
					golem.Penalty = 3;
				}
				if (ash is GolemCendreTerre)
				{
					golem.Skills[SkillName.Tactics].Base += 20 + skillBonus;
					golem.Skills[SkillName.Wrestling].Base += 20 + skillBonus;
					golem.SetDamageType(ResistanceType.Physical, 100);
					golem.SetResistance(ResistanceType.Physical, 50);
					golem.Penalty = 3;
				}
				if (ash is GolemCendreVent)
				{
					golem.SetDex(golem.Dex * 2);
					golem.Skills[SkillName.Tactics].Base += 20 + skillBonus;
					golem.SetDamageType(ResistanceType.Energy, 100);
					golem.SetResistance(ResistanceType.Energy, 50);
					golem.Penalty = 2;
				}

				golem.SetHits(golem.HitsMax + (baseBonus * 2));
				golem.SetDamage(golem.DamageMin + (baseBonus / 10), golem.DamageMax + (baseBonus / 5));
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
					if (targeted is BaseGolemAsh ash)
					{
						m_Crystal.Ash = ash;
						m_Crystal.AshQuantity = ash.Amount;
						from.SendMessage($"Vous avez sélectionné {ash.Amount} {ash.AshName}.");
					}
					else
					{
						from.SendMessage("Cela n'est pas des cendres valides.");
					}
					from.SendGump(new GolemCreationGump(from, m_Crystal));
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
							m_Crystal.Spirit = spirit;
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
}



