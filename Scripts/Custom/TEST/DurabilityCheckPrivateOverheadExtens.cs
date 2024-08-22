using Server;
using Server.Network;

public static class MobileExtensions
{
	public static void PrivateOverheadMessage(this Mobile m, MessageType type, int hue, bool ascii, string text)
	{
		if (m.NetState != null)
		{
			m.NetState.Send(new UnicodeMessage(m.Serial, m.Body, type, hue, 3, m.Language, m.Name, text));
		}
	}
}
