using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Scripts.Custom
{
	public class LootEmote
	{
		public static void Initialize()
		{
			EventSink.CorpseLoot += OnCorpseLoot;
		}

		public static void OnCorpseLoot(CorpseLootEventArgs e)
		{
			if (e.Mobile is PlayerMobile looter && e.Corpse is Corpse corpse)
			{
				Mobile owner = corpse.Owner;

				if (owner is PlayerMobile || owner is BaseHire)
				{
					if (ShouldShowEmote(looter))
					{
						looter.Emote("*Fouille la carcasse*");
					}
				}
			}
		}

		private static bool ShouldShowEmote(PlayerMobile player)
		{
			Skill stealing = player.Skills[SkillName.Stealing];
			Skill snooping = player.Skills[SkillName.Snooping];

			// Si l'une des compétences est à 100 ou plus, ne pas afficher le message
			if (stealing.Base >= 100 || snooping.Base >= 100)
				return false;

			// Si l'une des compétences est à 0, afficher le message
			if (stealing.Base <= 0 || snooping.Base <= 0)
				return true;

			// Calculer la probabilité d'affichage basée sur la moyenne des deux compétences
			double averageSkill = (stealing.Base + snooping.Base) / 2;
			double showProbability = 1 - (averageSkill / 100);

			// Générer un nombre aléatoire entre 0 et 1
			double randomValue = Utility.RandomDouble();

			// Retourner true si le nombre aléatoire est inférieur à la probabilité d'affichage
			return randomValue < showProbability;
		}
	}
}
