using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Commands
{
	public class ReleverCommand
	{
		private static Dictionary<Serial, DateTime> CooldownTimes = new Dictionary<Serial, DateTime>();

		public static void Initialize()
		{
			CommandSystem.Register("relever", AccessLevel.Player, new CommandEventHandler(Relever_OnCommand));
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public static TimeSpan CooldownDuration { get; set; } = TimeSpan.FromMinutes(15);

		private static void Relever_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;

			if (from == null || !(from is CustomPlayerMobile))
				return;

			CustomPlayerMobile player = (CustomPlayerMobile)from;

			if (!player.Alive)
			{
				player.SendMessage("Vous devez être en vie pour utiliser cette commande.");
				return;
			}

			if (IsOnCooldown(player))
			{
				TimeSpan remainingTime = GetRemainingCooldown(player);
				player.SendMessage($"Vous devez attendre encore {remainingTime.Minutes} minutes et {remainingTime.Seconds} secondes avant de pouvoir utiliser cette commande.");
				return;
			}

			player.Target = new ReleverTarget(player);
			player.SendMessage("Qui voulez-vous relever ?");
		}

		private static bool IsOnCooldown(Mobile m)
		{
			return CooldownTimes.TryGetValue(m.Serial, out DateTime cooldownEnd) && cooldownEnd > DateTime.UtcNow;
		}

		private static TimeSpan GetRemainingCooldown(Mobile m)
		{
			if (CooldownTimes.TryGetValue(m.Serial, out DateTime cooldownEnd))
			{
				return cooldownEnd - DateTime.UtcNow;
			}
			return TimeSpan.Zero;
		}

		private static void SetCooldown(Mobile m)
		{
			CooldownTimes[m.Serial] = DateTime.UtcNow + CooldownDuration;
		}

		private class ReleverTarget : Target
		{
			private CustomPlayerMobile m_From;

			public ReleverTarget(CustomPlayerMobile from) : base(1, false, TargetFlags.None)
			{
				m_From = from;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (!(targeted is CustomPlayerMobile target))
				{
					m_From.SendMessage("Vous ne pouvez relever que des joueurs.");
					return;
				}

				if (!target.InRange(m_From.Location, 1))
				{
					m_From.SendMessage("La cible est trop loin.");
					return;
				}

				if (!target.Vulnerability)
				{
					m_From.SendMessage("Ce joueur n'est pas assommé.");
					return;
				}

				if (target.LastKiller != m_From)
				{
					m_From.SendMessage("Vous n'êtes pas la dernière personne à avoir assommé ce joueur.");
					return;
				}

				// Retirer le statut "assommé"
				target.Vulnerability = false;

				m_From.SendMessage($"Vous avez relevé {target.Name}.");
				target.SendMessage($"{m_From.Name} vous a relevé.");

				// Définir le temps de cooldown
				SetCooldown(m_From);
			}
		}
	}
}
