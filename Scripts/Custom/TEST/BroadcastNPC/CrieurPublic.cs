using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using System.Collections.Generic;

public class CrieurPublic : BaseVendor
{
	private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();

	protected override List<SBInfo> SBInfos => m_SBInfos;

	[Constructable]
	public CrieurPublic() : base("le crieur public")
	{
		SetSkill(SkillName.Botanique, 36.0, 68.0);
	}

	public CrieurPublic(Serial serial) : base(serial)
	{
	}

	public override void InitSBInfo()
	{
	}

	public override void OnDoubleClick(Mobile from)
	{
		if (!from.InRange(this.Location, 3))
		{
			from.SendLocalizedMessage(500446); // That is too far away.
			return;
		}

		from.SendGump(new CrieurPublicGump(this));
	}

	public void BroadcastMessage(Mobile from, string message)
	{
		if (from.Backpack.ConsumeTotal(typeof(Gold), 100))
		{
			string fullMessage = $"{from.Name} annonce : {message}";
			this.Say(fullMessage);
			World.Broadcast(0x35, true, fullMessage);
			from.SendMessage("Votre message a été diffusé.");
			from.SendMessage("100 pièces d'or ont été retirées de votre sac.");
		}
		else
		{
			from.SendMessage("Vous n'avez pas assez d'or pour payer le crieur public.");
		}
	}

	public override void Serialize(GenericWriter writer)
	{
		base.Serialize(writer);
		writer.Write((int)0); // version
	}

	public override void Deserialize(GenericReader reader)
	{
		base.Deserialize(reader);
		int version = reader.ReadInt();
	}
}

public class CrieurPublicGump : Gump
{
	private CrieurPublic m_Crieur;

	public CrieurPublicGump(CrieurPublic crieur) : base(50, 50)
	{
		m_Crieur = crieur;

		AddPage(0);
		AddBackground(0, 0, 420, 320, 9270);
		AddAlphaRegion(10, 10, 400, 300);

		AddHtml(20, 20, 380, 60, "<BASEFONT COLOR=#FFFFFF>Entrez le message que vous souhaitez diffuser. Le coût est de 100 pièces d'or.</BASEFONT>", false, false);

		AddLabel(20, 90, 0xFFFFFF, "Message :"); // 0xFFFFFF est le code couleur pour le blanc

		AddBackground(20, 110, 380, 100, 9350);
		AddTextEntry(25, 115, 370, 90, 0, 0, "");

		AddButton(150, 280, 4005, 4007, 1, GumpButtonType.Reply, 0);
		AddHtml(190, 280, 100, 20, "<BASEFONT COLOR=#FFFFFF>Diffuser</BASEFONT>", false, false);

	}

	public override void OnResponse(NetState sender, RelayInfo info)
	{
		Mobile from = sender.Mobile;

		if (info.ButtonID == 1)
		{
			TextRelay entry = info.GetTextEntry(0);
			if (entry != null && !string.IsNullOrEmpty(entry.Text))
			{
				m_Crieur.BroadcastMessage(from, entry.Text);
			}
			else
			{
				from.SendMessage("Vous devez entrer un message à diffuser.");
			}
		}
	}
}
