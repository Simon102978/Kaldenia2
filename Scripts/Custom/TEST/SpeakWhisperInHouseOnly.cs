using System;
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

			if (type != MessageType.Regular && type != MessageType.Whisper)
				return;

			BaseHouse speakerHouse = BaseHouse.FindHouseAt(speaker);

			foreach (NetState state in NetState.Instances)
			{
				Mobile listener = state.Mobile;

				if (listener == null || listener == speaker)
					continue;

				BaseHouse listenerHouse = BaseHouse.FindHouseAt(listener);

				bool canHear = false;

				if (speakerHouse != null)
				{
					if (type == MessageType.Whisper)
					{
						// Les chuchotements ne sont entendus que dans la même maison
						canHear = (speakerHouse == listenerHouse);
					}
					else // MessageType.Regular
					{
						// Les messages normaux sont entendus dans la maison et à proximité à l'extérieur
						canHear = (speakerHouse == listenerHouse) ||
								  (listenerHouse == null && speaker.InRange(listener, 2));
					}
				}
				else // Le locuteur est à l'extérieur
				{
					// Comportement normal pour l'extérieur
					canHear = speaker.InRange(listener, type == MessageType.Whisper ? 1 : 2);
				}

				if (canHear)
				{
					listener.Send(new AsciiMessage(speaker.Serial, speaker.Body, type, e.Hue, 3, speaker.Name, e.Speech));
				}
			}

			e.Blocked = true;
		}
	}
}
