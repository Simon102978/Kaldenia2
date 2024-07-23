using System;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;

public class WolfFeastCommand
{
	public static void Initialize()
	{
		CommandSystem.Register("loup", AccessLevel.Player, new CommandEventHandler(OnCommand));
	}

	[Usage("loup")]
	[Description("Consomme un de vos familiers apprivoisés pour vous soigner. Le montant de guérison dépend du nombre de slots de contrôle du familier. Nécessite 75 en Taming et coûte 25 de stamina.")]
	public static void OnCommand(CommandEventArgs e)
	{
		Mobile from = e.Mobile;

		if (from.Skills[SkillName.AnimalTaming].Base < 75.0)
		{
			from.SendMessage("Vous avez besoin d'au moins 75 en Taming pour utiliser cette capacité.");
			return;
		}

		if (from.Stam < 25)
		{
			from.SendMessage("Vous n'avez pas assez de stamina pour utiliser cette capacité.");
			return;
		}

		from.Target = new InternalTarget();
	}

	private class InternalTarget : Target
	{
		public InternalTarget() : base(3, false, TargetFlags.None)
		{
		}

		protected override void OnTarget(Mobile from, object targeted)
		{
			if (targeted is BaseCreature creature)
			{
				if (!creature.Controlled || creature.ControlMaster != from)
				{
					from.SendMessage("Vous devez cibler un de vos familiers apprivoisés.");
					return;
				}

				if (creature.IsDeadPet)
				{
					from.SendMessage("Vous ne pouvez pas cibler un familier mort.");
					return;
				}

				if (creature.Summoned)
				{
					from.SendMessage("Vous ne pouvez pas cibler une créature invoquée.");
					return;
				}

				// Calculer le montant de guérison basé sur le nombre de slots de contrôle
				int healAmount = creature.ControlSlots * 7;

				// Consommer le familier
				creature.Delete();

				// Soigner le joueur
				from.Heal(healAmount);

				// Réduire la stamina
				from.Stam -= 25;

				// Ajouter l'émote
				from.PublicOverheadMessage(MessageType.Emote, 0x3B2, false, "* Sacrifie sa créature *");

				from.SendMessage($"Vous consommez votre familier et récupérez {healAmount} points de vie.");
			}
			else
			{
				from.SendMessage("Vous devez cibler un de vos familiers apprivoisés.");
			}
		}
	}
}
