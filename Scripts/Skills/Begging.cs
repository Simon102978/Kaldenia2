using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Scripts.Commands
{
	public class Concentration
	{
		public static void Initialize()
		{
			CommandSystem.Register("Concentration", AccessLevel.Player, new CommandEventHandler(Concentration_OnCommand));
		}

		[Usage("Concentration")]
		[Description("Permet d'activer la concentration")]
		public static void Concentration_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;

			if (from is PlayerMobile player)
			{
				TimeSpan cooldown = OnUse(player);
				player.SendMessage($"Concentration activée. Cooldown: {cooldown.TotalSeconds} secondes.");
			}
		}

		private static DateTime[] _lastUseTime = new DateTime[0x1000];
		private static bool[] _concentrationActive = new bool[0x1000];

		public static TimeSpan OnUse(Mobile m)
		{
			if (m is PlayerMobile player)
			{
				double skill = player.Skills[SkillName.Concentration].Value;

				TimeSpan duration = TimeSpan.FromSeconds(skill * 3);

				_concentrationActive[m.Serial.Value] = true;
				_lastUseTime[m.Serial.Value] = DateTime.Now;

				player.SendMessage($"Vous êtes concentré. Vos lancés de sorts pendant {skill:F1} secondes pourraient se faire sans réactif ni mana.");

				Timer.DelayCall(duration, DeactivateConcentration, m);
			}

			return TimeSpan.FromSeconds(30.0);
		}

		private static void DeactivateConcentration(Mobile m)
		{
			_concentrationActive[m.Serial.Value] = false;
			m.SendMessage("Votre état de concentration est terminée.");
		}
	}
}
