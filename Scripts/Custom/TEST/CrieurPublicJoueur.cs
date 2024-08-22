using System;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Commands;

namespace Server.Commands
{
	public class CrieurPublicCommand
	{
		private static Dictionary<string, int> ColorMap = new Dictionary<string, int>
		{
			{"rouge", 0x22},
			{"bleu", 0x55},
			{"vert", 0x44},
			{"jaune", 0x35},
			{"violet", 0x14},
			{"blanc", 0x3B2},
			{"noir", 0x3},
			{"gris", 0x3B1},
			{"orange", 0x2C},
			{"rose", 0x26},
			{"marron", 0x2A},
			{"cyan", 0x58},
			{"magenta", 0x16},
			{"indigo", 0x12},
			{"turquoise", 0x5A},
			{"lime", 0x3F},
			{"olive", 0x2E},
			{"corail", 0x21},
			{"saumon", 0x384},
			{"lavande", 0x13},
			{"or", 0x38},
			{"argent", 0x3BA},
			{"bronze", 0x29},
			{"azur", 0x5D},
			{"emeraude", 0x42},
			{"rubis", 0x23},
			{"saphir", 0x54},
			{"amethyste", 0x15}
		};

		public static void Initialize()
		{
			CommandSystem.Register("crier", AccessLevel.Player, new CommandEventHandler(Crier_OnCommand));
		}

		[Usage("Crier <message>")]
		[Description("Permet aux crieurs publics de diffuser un message. Incluez le nom d'une couleur à la fin pour changer la couleur du message.")]
		public static void Crier_OnCommand(CommandEventArgs e)
		{
			CustomPlayerMobile pm = e.Mobile as CustomPlayerMobile;

			if (pm == null || !pm.CrieurPublic)
			{
				e.Mobile.SendMessage("Vous n'êtes pas autorisé à utiliser cette commande.");
				return;
			}

			if (e.Length < 1)
			{
				e.Mobile.SendMessage("Usage: .crier <message> [couleur]");
				ListAvailableColors(pm);
				return;
			}

			string[] args = e.Arguments;
			string lastWord = args[args.Length - 1].ToLower();
			int color = 0x3B2; // Couleur par défaut (orange clair)
			string message;

			if (ColorMap.ContainsKey(lastWord))
			{
				color = ColorMap[lastWord];
				message = string.Join(" ", args, 0, args.Length - 1);
			}
			else
			{
				message = string.Join(" ", args);
			}

			BroadcastMessage(pm, message, color);
		}

		private static void BroadcastMessage(Mobile from, string message, int color)
		{
			foreach (NetState state in NetState.Instances)
			{
				Mobile m = state.Mobile;

				if (m != null && m.AccessLevel == AccessLevel.Player)
				{
					m.SendMessage(color, $"[Crieur Public] {from.Name}: {message}");
				}
			}

			from.SendMessage("Votre message a été diffusé.");
		}

		private static void ListAvailableColors(Mobile from)
		{
			from.SendMessage("Couleurs disponibles :");
			foreach (string colorName in ColorMap.Keys)
			{
				from.SendMessage(ColorMap[colorName], colorName);
			}
		}
	}
}
