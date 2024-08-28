using System;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;
using static Server.Items.ConfirmationGump;

namespace Server.Items
{
	public interface IScissorable
	{
		bool Scissor(Mobile from, Scissors scissors);
	}

	[FlipableAttribute(0xf9f, 0xf9e)]
	public class Scissors : Item
	{
		[Constructable]
		public Scissors() : base(0xF9F)
		{
			Weight = 1.0;
			Layer = Layer.OneHanded;
			Name = "Ciseaux";
		}

		public Scissors(Serial serial) : base(serial)
		{
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

		public override void OnDoubleClick(Mobile from)
		{
			from.SendLocalizedMessage(502434); // What should I use these scissors on?
			from.Target = new InternalTarget(this);
		}

		private class InternalTarget : Target
		{
			private Scissors m_Item;

			public InternalTarget(Scissors item) : base(2, false, TargetFlags.None)
			{
				m_Item = item;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_Item.Deleted)
					return;

				/*if ( targeted is Item && !((Item)targeted).IsStandardLoot() )
				{
					from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
				}
				else */
				if (Core.AOS && targeted == from)
				{
					from.SendGump(new HairCutConfirmationGump(from, m_Item));
				}
				else if (Utility.RandomDouble() > .20 && (from.Direction & Direction.Running) != 0 &&
						 (Core.TickCount - from.LastMoveTime) < from.ComputeMovementSpeed(from.Direction))
				{
					from.SendLocalizedMessage(1063305); // Didn't your parents ever tell you not to run with scissors in your hand?!
				}
				else if (targeted is Item && !((Item)targeted).Movable)
				{
					if (targeted is IScissorable && (targeted is PlagueBeastInnard || targeted is PlagueBeastMutationCore))
					{
						IScissorable obj = (IScissorable)targeted;

						if (obj.Scissor(from, m_Item))
							from.PlaySound(0x248);
					}
				}

				else if (targeted is IScissorable obj)
				{
					if (IsEpicOrLegendary(targeted))
					{
						from.SendGump(new ConfirmationGump(from, m_Item, obj));
					}
					else
					{
						if (obj.Scissor(from, m_Item))
							from.PlaySound(0x248);
					}
				}
				else
				{
					from.SendLocalizedMessage(502440); // Scissors can not be used on that to produce anything.
				}
			}

			private bool IsEpicOrLegendary(object item)
			{
				if (item is IQuality qualityItem)
				{
					return qualityItem.Quality == ItemQuality.Epic || qualityItem.Quality == ItemQuality.Legendary;
				}
				return false;
			}
		}
	}

	public class ConfirmationGump : Gump
	{
		private Mobile m_From;
		private Scissors m_Scissors;
		private IScissorable m_Target;

		public ConfirmationGump(Mobile from, Scissors scissors, IScissorable target) : base(50, 50)
		{
			m_From = from;
			m_Scissors = scissors;
			m_Target = target;

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage(0);
			AddBackground(0, 0, 240, 135, 9200);
			AddAlphaRegion(10, 10, 220, 115);

			AddHtml(10, 10, 220, 75, "<BASEFONT COLOR=#FFFFFF>Cet objet est épique ou légendaire. Êtes-vous sûr de vouloir le découper ?", false, false);

			AddButton(40, 95, 4005, 4007, 1, GumpButtonType.Reply, 0);
			AddHtml(75, 95, 100, 35, "<BASEFONT COLOR=#FFFFFF>OUI</BASEFONT>", false, false);

			AddButton(135, 95, 4005, 4007, 0, GumpButtonType.Reply, 0);
			AddHtml(170, 95, 100, 35, "<BASEFONT COLOR=#FFFFFF>NON</BASEFONT>", false, false);
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			if (info.ButtonID == 1)
			{
				// L'utilisateur a confirmé, procédez au découpage
				if (m_Target.Scissor(m_From, m_Scissors))
					m_From.PlaySound(0x248);
			}
			// Si ButtonID == 0, l'utilisateur a annulé, ne faites rien
		}
		public class HairCutConfirmationGump : Gump
		{
			private Mobile m_From;
			private Scissors m_Scissors;

			public HairCutConfirmationGump(Mobile from, Scissors scissors) : base(50, 50)
			{
				m_From = from;
				m_Scissors = scissors;

				Closable = true;
				Disposable = true;
				Dragable = true;
				Resizable = false;

				AddPage(0);
				AddBackground(0, 0, 240, 135, 9200);
				AddAlphaRegion(10, 10, 220, 115);

				AddHtml(10, 10, 220, 75, "<BASEFONT COLOR=#FFFFFF>Êtes-vous sûr de vouloir vous couper les cheveux ?", false, false);

				AddButton(40, 95, 4005, 4007, 1, GumpButtonType.Reply, 0);
				AddHtml(75, 95, 100, 35, "<BASEFONT COLOR=#FFFFFF>OUI</BASEFONT>", false, false);

				AddButton(135, 95, 4005, 4007, 0, GumpButtonType.Reply, 0);
				AddHtml(170, 95, 100, 35, "<BASEFONT COLOR=#FFFFFF>NON</BASEFONT>", false, false);
			}

			public override void OnResponse(NetState sender, RelayInfo info)
			{
				if (info.ButtonID == 1)
				{
					// L'utilisateur a confirmé, procédez à la coupe de cheveux
					m_From.HairItemID = 0;
					m_From.FacialHairItemID = 0;
					m_From.SendLocalizedMessage(502943); // You cut your hair.
					m_From.PlaySound(0x248);
				}
				// Si ButtonID == 0, l'utilisateur a annulé, ne faites rien
			}
		}

	}
}
