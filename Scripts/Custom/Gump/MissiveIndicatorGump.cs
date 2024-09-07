using Server.Gumps;
using Server;
using System.Reactive.Disposables;

public class MissiveIndicatorGump : Gump
{
	private Mobile m_Owner;
	private int m_Count;

	public MissiveIndicatorGump(Mobile owner, int count) : base(10, 10)
	{
		m_Owner = owner;
		m_Count = count;

		Closable = false;
		Disposable = false;
		Dragable = true;
		Resizable = false;

		AddPage(0);
		AddBackground(0, 0, 100, 40, 9270);
		AddLabel(10, 10, 32, $"Missives: {m_Count}");
	}

	public void Update(int newCount)
	{
		m_Count = newCount;
		Refresh();
	}

	private void Refresh()
	{
		Entries.Clear();
		AddPage(0);
		AddBackground(0, 0, 100, 40, 9270);
		AddLabel(10, 10, 32, $"Missives: {m_Count}");

		m_Owner.CloseGump(typeof(MissiveIndicatorGump));
		m_Owner.SendGump(this);
	}
}
