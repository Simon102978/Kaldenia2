using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class TamerTransformBrush : Item
	{
		[Constructable]
		public TamerTransformBrush() : base(0x1372)
		{
			Name = "Brosse de Transformation";
			Weight = 1.0;
		}

		public TamerTransformBrush(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
				return;
			}

			from.SendMessage("Choisissez la créature à transformer.");
			from.Target = new InternalTarget(this);
		}

		private class InternalTarget : Target
		{
			private TamerTransformBrush m_Brush;

			public InternalTarget(TamerTransformBrush brush) : base(10, false, TargetFlags.None)
			{
				m_Brush = brush;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is BaseCreature creature)
				{
					if (creature.Controlled && creature.ControlMaster == from)
					{
						

						List<int> availableBodyValues = GetAvailableBodyValues(creature.BodyValue);
						from.SendGump(new TransformGump(from, creature, availableBodyValues, m_Brush));
					}
					else
					{
						from.SendMessage("Vous ne pouvez transformer que vos propres créatures apprivoisées.");
					}
				}
				else
				{
					from.SendMessage("Vous ne pouvez transformer que des créatures apprivoisées.");
				}
			}
		}

		private static List<int> GetAvailableBodyValues(int currentBodyValue)
		{
			List<int> bodyValues = new List<int>();
			for (int i = -2; i <= 2; i++)
			{
				int newBodyValue = currentBodyValue + (i * 5);
				if (newBodyValue > 0)
				{
					bodyValues.Add(newBodyValue);
				}
			}
			return bodyValues;
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

	public class TransformGump : Gump
	{
		private Mobile m_From;
		private BaseCreature m_Creature;
		private List<int> m_BodyValues;
		private TamerTransformBrush m_Brush;

		public TransformGump(Mobile from, BaseCreature creature, List<int> bodyValues, TamerTransformBrush brush)
			: base(50, 50)
		{
			m_From = from;
			m_Creature = creature;
			m_BodyValues = bodyValues;
			m_Brush = brush;

			AddPage(0);

			AddBackground(0, 0, 450, 450, 5054);
			AddBackground(10, 10, 430, 430, 3000);

			AddHtmlLocalized(20, 30, 400, 25, 1154037, false, false); // Choose a new form:

			AddButton(20, 400, 4005, 4007, 0, GumpButtonType.Reply, 0);
			AddHtmlLocalized(55, 400, 200, 25, 1011012, false, false); // CANCEL

			AddPage(1);

			for (int i = 0; i < bodyValues.Count; i++)
			{
				int x = 30 + ((i % 2) * 200);
				int y = 85 + ((i / 2) * 100);

				AddRadio(x, y, 210, 211, false, i + 1);
				AddItem(x + 40, y, bodyValues[i]);
				AddLabel(x + 80, y, 0, $"Body ID: {bodyValues[i]}");
				AddLabel(x + 80, y + 20, 0, $"Taming requis: {(i + 1) * 20}");
			}

			AddButton(180, 400, 4005, 4007, 1, GumpButtonType.Reply, 0);
			AddHtmlLocalized(215, 400, 200, 25, 1011036, false, false); // OKAY
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;

			if (info.ButtonID == 0)
				return;

			int[] switches = info.Switches;
			if (switches.Length > 0)
			{
				int index = switches[0] - 1;
				if (index >= 0 && index < m_BodyValues.Count)
				{
					int requiredSkill = (index + 1) * 20;
					if (from.Skills[SkillName.AnimalTaming].Base >= requiredSkill)
					{
						m_Creature.BodyValue = m_BodyValues[index];
						from.SendMessage($"Vous avez transformé votre créature en Body ID {m_BodyValues[index]}.");
						m_Brush.Delete();
					}
					else
					{
						from.SendMessage($"Vous avez besoin de {requiredSkill} en Taming pour effectuer cette transformation.");
					}
				}
			}
		}
	}
}
