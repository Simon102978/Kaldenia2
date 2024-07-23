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
	[Description("Consomme un de vos familiers apprivois�s pour vous soigner. Le montant de gu�rison d�pend du nombre de slots de contr�le du familier. N�cessite 75 en Taming et co�te 25 de stamina.")]
	public static void OnCommand(CommandEventArgs e)
	{
		Mobile from = e.Mobile;

		if (from.Skills[SkillName.AnimalTaming].Base < 75.0)
		{
			from.SendMessage("Vous avez besoin d'au moins 75 en Taming pour utiliser cette capacit�.");
			return;
		}

		if (from.Stam < 25)
		{
			from.SendMessage("Vous n'avez pas assez de stamina pour utiliser cette capacit�.");
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
					from.SendMessage("Vous devez cibler un de vos familiers apprivois�s.");
					return;
				}

				if (creature.IsDeadPet)
				{
					from.SendMessage("Vous ne pouvez pas cibler un familier mort.");
					return;
				}

				if (creature.Summoned)
				{
					from.SendMessage("Vous ne pouvez pas cibler une cr�ature invoqu�e.");
					return;
				}

				// Calculer le montant de gu�rison bas� sur le nombre de slots de contr�le
				int healAmount = creature.ControlSlots * 7;

				// Consommer le familier
				creature.Delete();

				// Soigner le joueur
				from.Heal(healAmount);

				// R�duire la stamina
				from.Stam -= 25;

				// Ajouter l'�mote
				from.PublicOverheadMessage(MessageType.Emote, 0x3B2, false, "* Sacrifie sa cr�ature *");

				from.SendMessage($"Vous consommez votre familier et r�cup�rez {healAmount} points de vie.");
			}
			else
			{
				from.SendMessage("Vous devez cibler un de vos familiers apprivois�s.");
			}
		}
	}
}
