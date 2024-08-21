using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Commands
{
	public class ViderSacCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register("vidersac", AccessLevel.Player, new CommandEventHandler(ViderSac_OnCommand));
		}

		[Usage("ViderSac")]
		[Description("Transfère le contenu d'un contenant à un autre.")]
		public static void ViderSac_OnCommand(CommandEventArgs e)
		{
			e.Mobile.SendMessage("Sélectionnez le contenant source.");
			e.Mobile.Target = new InternalSourceTarget();
		}

		private class InternalSourceTarget : Target
		{
			public InternalSourceTarget() : base(-1, false, TargetFlags.None)
			{
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is BaseContainer sourceContainer)
				{
					if (!sourceContainer.IsAccessibleTo(from))
					{
						from.SendMessage("Vous ne pouvez pas accéder à ce contenant.");
						return;
					}

					if (sourceContainer.LockByPlayer || sourceContainer.GmLocked )
					{
						from.SendMessage("Ce contenant est verrouillé.");
						return;
					}

					from.SendMessage("Sélectionnez le contenant de destination.");
					from.Target = new InternalDestTarget(sourceContainer);
				}
				else
				{
					from.SendMessage("Ceci n'est pas un contenant valide.");
				}
			}
		}

		private class InternalDestTarget : Target
		{
			private BaseContainer m_Source;

			public InternalDestTarget(BaseContainer source) : base(-1, false, TargetFlags.None)
			{
				m_Source = source;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is BaseContainer destContainer)
				{
					if (!destContainer.IsAccessibleTo(from))
					{
						from.SendMessage("Vous ne pouvez pas accéder à ce contenant.");
						return;
					}


					int itemsMoved = 0;
					Item[] items = m_Source.Items.ToArray();

					foreach (Item item in items)
					{
						if (item.Movable && destContainer.CheckHold(from, item, true, true))
						{
							m_Source.RemoveItem(item);
							destContainer.DropItem(item);
							itemsMoved++;
						}
					}

					from.SendMessage($"{itemsMoved} objets ont été transférés.");
				}
				else
				{
					from.SendMessage("Ceci n'est pas un contenant valide.");
				}
			}
		}
	}
}
