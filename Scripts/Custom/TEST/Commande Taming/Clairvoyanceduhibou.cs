using System;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;

public class ClairvoyanceHibouCommand
{
	public static void Initialize()
	{
		CommandSystem.Register("hibou", AccessLevel.Player, new CommandEventHandler(OnCommand));
	}

	[Usage("hibou")]
	[Description("Consomme un de vos familiers hiboux apprivoisés pour restaurer 30 points de mana. Nécessite 90 en Taming et coûte 25 de stamina.")]
	public static void OnCommand(CommandEventArgs e)
	{
		Mobile from = e.Mobile;

		if (from.Skills[SkillName.AnimalTaming].Base < 90.0)
		{
			from.SendMessage("Vous avez besoin d'au moins 90 en Taming pour utiliser cette capacité.");
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

				// Calculer le montant de mana basé sur le nombre de slots de contrôle
				int ManaAmount = creature.ControlSlots * 7;

				// Consommer le familier
				creature.Delete();

				// Soigner le joueur
				from.Mana += ManaAmount;

				// Réduire la stamina
				from.Stam -= 25;

				// Ajouter l'émote
				from.PublicOverheadMessage(MessageType.Emote, 0x3B2, false, "* Sacrifie sa créature *");

				from.SendMessage($"Vous consommez votre familier et récupérez {ManaAmount} mana.");
			}
			else
			{
				from.SendMessage("Vous devez cibler un de vos familiers apprivoisés.");
			}
		}
	}
}
