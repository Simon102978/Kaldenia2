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

			// Si l'une des comp�tences est � 100 ou plus, ne pas afficher le message
			if (stealing.Base >= 100 || snooping.Base >= 100)
				return false;

			// Si l'une des comp�tences est � 0, afficher le message
			if (stealing.Base <= 0 || snooping.Base <= 0)
				return true;

			// Calculer la probabilit� d'affichage bas�e sur la moyenne des deux comp�tences
			double averageSkill = (stealing.Base + snooping.Base) / 2;
			double showProbability = 1 - (averageSkill / 100);

			// G�n�rer un nombre al�atoire entre 0 et 1
			double randomValue = Utility.RandomDouble();

			// Retourner true si le nombre al�atoire est inf�rieur � la probabilit� d'affichage
			return randomValue < showProbability;
		}
	}
}
