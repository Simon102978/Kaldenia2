using System;
using System.Text;
using Server;
using Server.Network;
using Server.Multis;

namespace Server.Misc
{
	public class HouseSpeechFilter
	{
		public static void Initialize()
		{
			EventSink.Speech += OnSpeech;
		}

		public static void OnSpeech(SpeechEventArgs e)
		{
			Mobile speaker = e.Mobile;
			MessageType type = e.Type;
			string speech = e.Speech.Trim();

			// Convertir le message pour gérer les accents
			string convertedSpeech = RemoveDiacritics(speech);

			BaseHouse speakerHouse = BaseHouse.FindHouseAt(speaker);

			// Si le locuteur n'est pas dans une maison, permettre le comportement par défaut
			if (speakerHouse == null)
			{
				return; // Sortir de la méthode sans bloquer le message
			}

			// À partir d'ici, le code ne s'applique que si le locuteur est dans une maison

			// Vérifier si le message commence par "I wish" (insensible à la casse)
			if (speech.StartsWith("I wish", StringComparison.OrdinalIgnoreCase))
			{
				// Si c'est une commande "I wish", ne pas bloquer le message
				return;
			}

			if (type != MessageType.Regular && type != MessageType.Whisper)
				return;

			// Envoyer le message au locuteur lui-même
			speaker.Send(new UnicodeMessage(speaker.Serial, speaker.Body, type, e.Hue, 3, "FRA", speaker.Name, convertedSpeech));

			foreach (NetState state in NetState.Instances)
			{
				Mobile listener = state.Mobile;

				if (listener == null || listener == speaker)
					continue;

				BaseHouse listenerHouse = BaseHouse.FindHouseAt(listener);

				bool canHear = false;

				if (listenerHouse == speakerHouse) // L'auditeur est dans la même maison
				{
					canHear = true; // Peut entendre tous les types de messages
				}
				else if (type == MessageType.Regular) // Message normal
				{
					// L'auditeur est à l'extérieur, à 2 tiles de la maison
					canHear = speaker.GetDistanceToSqrt(listener) <= 2;
				}

				if (canHear)
				{
					listener.Send(new UnicodeMessage(speaker.Serial, speaker.Body, type, e.Hue, 3, "FRA", speaker.Name, convertedSpeech));
				}
			}

			e.Blocked = true; // Bloquer le message original seulement si le locuteur est dans une maison
		}

	

public static string RemoveDiacritics(string text)
		{
			var normalizedString = text.Normalize(NormalizationForm.FormD);
			var stringBuilder = new StringBuilder();

			foreach (var c in normalizedString)
			{
				var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}
	}
}
