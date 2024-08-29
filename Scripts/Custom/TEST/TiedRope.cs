using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class TiedRope : Item
	{
		private Mobile m_Tied;
		private Mobile m_Owner;
		private Timer m_FollowTimer;

		[Constructable]
		public TiedRope() : base(0x14F8)
		{
			Name = "Corde";
			Weight = 1.0;
		}

		public TiedRope(Serial serial) : base(serial) { }

		public Mobile Tied => m_Tied;
		public Mobile Owner => m_Owner;

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
				return;
			}

			if (m_Tied == null)
			{
				from.Target = new TieTarget(this);
				from.SendMessage("Qui voulez-vous attacher ?");
			}
			else
			{
				Untie(from);
			}
		}

		public void Tie(Mobile from, Mobile target)
		{
			if (target == from)
			{
				from.SendMessage("Vous ne pouvez pas vous attacher vous-même.");
				return;
			}

			if (!(target is PlayerMobile))
			{
				from.SendMessage("Vous ne pouvez attacher que d'autres joueurs.");
				return;
			}

			if (!target.Alive)
			{
				from.SendMessage("Vous ne pouvez attacher que des joueurs vivants.");
				return;
			}

			if (target is CustomPlayerMobile cp && cp.Vulnerability && cp.Alive)
			{
				// Le joueur est vulnérable, on l'attache directement
				ConfirmTie(from, target);
			}
			else
			{
				// Le joueur n'est pas vulnérable, on lui envoie le Gump de confirmation
				target.SendGump(new ConfirmTieGump(from, target, this));
			}
		}

		public void ConfirmTie(Mobile from, Mobile target)
		{
			m_Tied = target;
			m_Owner = from;
			target.SendMessage($"{from.Name} vous a attaché.");
			from.SendMessage($"Vous avez attaché {target.Name}.");
			target.Frozen = true;
			StartFollowing();
		}

		public void Untie(Mobile from)
		{
			if (m_Tied != null)
			{
				m_Tied.Frozen = false;
				m_Tied.SendMessage("Vous avez été libéré de vos liens.");
				from.SendMessage($"Vous avez détaché {m_Tied.Name}.");

				StopFollowing();

				m_Tied = null;
				m_Owner = null;
			}
			else
			{
				from.SendMessage("Il n'y a personne d'attaché à cette corde.");
			}
		}

		private void StartFollowing()
		{
			if (m_FollowTimer == null)
			{
				m_FollowTimer = Timer.DelayCall(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), new TimerCallback(CheckPosition));
			}
		}

		private void StopFollowing()
		{
			if (m_FollowTimer != null)
			{
				m_FollowTimer.Stop();
				m_FollowTimer = null;
			}
		}

		private void CheckPosition()
		{
			if (m_Tied != null && m_Owner != null && m_Tied.Map == m_Owner.Map)
			{
				Point3D newLocation = m_Owner.Location;
				newLocation.X--; // Place le joueur attaché juste derrière

				m_Tied.MoveToWorld(newLocation, m_Owner.Map);
			}
			else
			{
				StopFollowing();
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version

			writer.Write(m_Tied);
			writer.Write(m_Owner);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			m_Tied = reader.ReadMobile();
			m_Owner = reader.ReadMobile();

			if (m_Tied != null && m_Owner != null)
			{
				StartFollowing();
			}
		}

		public class TieTarget : Target
		{
			private TiedRope m_Rope;

			public TieTarget(TiedRope rope) : base(10, false, TargetFlags.None)
			{
				m_Rope = rope;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is Mobile mobile)
				{
					m_Rope.Tie(from, mobile);
				}
				else
				{
					from.SendMessage("Vous ne pouvez attacher que des joueurs.");
				}
			}
		}

		public class ConfirmTieGump : Gump
		{
			private Mobile m_From;
			private Mobile m_Target;
			private TiedRope m_Rope;

			public ConfirmTieGump(Mobile from, Mobile target, TiedRope rope) : base(50, 65)
			{
				m_From = from;
				m_Target = target;
				m_Rope = rope;

				Closable = true;
				Disposable = true;
				Dragable = true;
				Resizable = false;

				AddPage(0);
				AddBackground(0, 0, 240, 120, 9200);
				AddAlphaRegion(10, 10, 220, 100);

				AddHtml(10, 10, 220, 80, $"{from.Name} veut vous attacher. Acceptez-vous ?", false, false);

				AddButton(10, 90, 4005, 4007, 1, GumpButtonType.Reply, 0);
				AddHtmlLocalized(45, 90, 150, 20, 1011011, false, false); // CONTINUE

				AddButton(170, 90, 4005, 4007, 0, GumpButtonType.Reply, 0);
				AddHtmlLocalized(205, 90, 150, 20, 1011012, false, false); // CANCEL
			}

			public override void OnResponse(NetState sender, RelayInfo info)
			{
				Mobile from = sender.Mobile;

				if (info.ButtonID == 1)
				{
					m_Rope.ConfirmTie(m_From, m_Target);
				}
				else
				{
					m_From.SendMessage($"{m_Target.Name} a refusé d'être attaché.");
				}
			}
		}

		// Nouvelles méthodes pour gérer le fouillage et le déplacement d'objets
		public static bool IsTied(Mobile m)
		{
			foreach (Item item in World.Items.Values)
			{
				if (item is TiedRope rope && rope.Tied == m)
				{
					return true;
				}
			}
			return false;
		}

		public static Mobile TiedBy(Mobile m)
		{
			foreach (Item item in World.Items.Values)
			{
				if (item is TiedRope rope && rope.Tied == m)
				{
					return rope.Owner;
				}
			}
			return null;
		}

		public static bool CheckNonlocalDrop(Mobile from, Mobile tied, Item item, Item target)
		{
			if (IsTied(tied) && from == TiedBy(tied))
			{
				return true;
			}
			return false;
		}

		public static bool CheckNonlocalLift(Mobile from, Mobile tied, Item item)
		{
			if (IsTied(tied) && from == TiedBy(tied))
			{
				return true;
			}
			return false;
		}

		public static void OnTiedDoubleClick(Mobile from, Mobile tied)
		{
			if (IsTied(tied) && from == TiedBy(tied))
			{
				from.SendMessage("Vous fouillez le sac de " + tied.Name + ".");
				from.Target = new LootTarget(tied);
			}
		}

		private class LootTarget : Target
		{
			private Mobile m_Tied;

			public LootTarget(Mobile tied) : base(-1, false, TargetFlags.None)
			{
				m_Tied = tied;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is Item item && item.IsChildOf(m_Tied.Backpack))
				{
					if (from.AddToBackpack(item))
					{
						from.SendMessage("Vous avez pris " + item.Name + " du sac de " + m_Tied.Name + ".");
						m_Tied.SendMessage(from.Name + " a pris " + item.Name + " de votre sac.");
					}
					else
					{
						from.SendMessage("Vous n'avez pas assez de place dans votre sac pour prendre cet objet.");
					}
				}
				else
				{
					from.SendMessage("Cet objet n'est pas dans le sac de " + m_Tied.Name + ".");
				}
			}
		}
	}
}
