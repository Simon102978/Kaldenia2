using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Network;

public class PackCallCommand
{
	public static void Initialize()
	{
		CommandSystem.Register("meute", AccessLevel.Player, new CommandEventHandler(OnCommand));
	}

	[Usage("meute")]
	[Description("Téléporte tous vos familiers à vous. Nécessite 100 en Taming et coûte 25 de stamina.")]
	public static void OnCommand(CommandEventArgs e)
	{
		Mobile from = e.Mobile;

		if (from.Skills[SkillName.AnimalTaming].Base < 100.0)
		{
			from.SendMessage("Vous avez besoin d'au moins 100 en Taming pour utiliser cette capacité.");
			return;
		}

		if (from.Stam < 25)
		{
			from.SendMessage("Vous n'avez pas assez de stamina pour utiliser cette capacité.");
			return;
		}

		List<BaseCreature> pets = new List<BaseCreature>();

		foreach (Mobile m in World.Mobiles.Values)
		{
			if (m is BaseCreature creature)
			{
				if (creature.Controlled && creature.ControlMaster == from && !creature.IsDeadPet)
				{
					pets.Add(creature);
				}
			}
		}

		if (pets.Count == 0)
		{
			from.SendMessage("Vous n'avez aucun familier à appeler.");
			return;
		}

		foreach (BaseCreature pet in pets)
		{
			pet.MoveToWorld(from.Location, from.Map);
		}

		// Réduire la stamina
		from.Stam -= 25;

		// Ajouter l'émote
		from.PublicOverheadMessage(MessageType.Emote, 0x3B2, false, "* Appelle sa meute *");

		from.SendMessage($"Vous avez appelé {pets.Count} familier(s) à votre position.");
	}
}
