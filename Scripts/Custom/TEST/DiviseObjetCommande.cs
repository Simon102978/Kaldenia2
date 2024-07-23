using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Prompts;

namespace Server.Commands
{
	public class DiviseCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register("divise", AccessLevel.Player, new CommandEventHandler(Divise_OnCommand));
		}

		[Usage("Divise")]
		[Description("Divise un objet empilable en une quantité spécifiée.")]
		public static void Divise_OnCommand(CommandEventArgs e)
		{
			e.Mobile.SendMessage("Sélectionnez l'objet à diviser.");
			e.Mobile.Target = new DiviseTarget();
		}

		private class DiviseTarget : Target
		{
			public DiviseTarget() : base(-1, false, TargetFlags.None)
			{
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is Item item)
				{
					if (!item.Movable)
					{
						from.SendMessage("Cet objet ne peut pas être déplacé.");
						return;
					}

					if (item.Amount > 1)
					{
						from.SendMessage("Entrez la quantité que vous souhaitez prendre :");
						from.Prompt = new InternalPrompt(item);
					}
					else
					{
						if (!from.InRange(item.GetWorldLocation(), 1))
							from.SendLocalizedMessage(500446); // That is too far away.
						else
							from.SendMessage($"Vous prenez 1 {item.Name}.");
					}
				}
				else
				{
					from.SendMessage("Vous devez cibler un objet.");
				}
			}
		}

		private class InternalPrompt : Prompt
		{
			private Item m_Item;

			public InternalPrompt(Item item)
			{
				m_Item = item;
			}

			public override void OnResponse(Mobile from, string text)
			{
				if (!m_Item.Deleted && m_Item.IsAccessibleTo(from))
				{
					if (int.TryParse(text, out int amount))
					{
						if (amount < 1 || amount > m_Item.Amount)
						{
							from.SendMessage("Quantité invalide.");
						}
						else if (amount == m_Item.Amount)
						{
							from.SendMessage($"Vous prenez tous les {m_Item.Name}.");
							from.AddToBackpack(m_Item);
						}
						else
						{
							m_Item.Amount -= amount;
							Item newItem = (Item)Activator.CreateInstance(m_Item.GetType());
							newItem.Amount = amount;
							from.AddToBackpack(newItem);
							from.SendMessage($"Vous prenez {amount} {m_Item.Name}.");
						}
					}
					else
					{
						from.SendMessage("Quantité invalide. Veuillez entrer un nombre.");
					}
				}
			}
		}
	}
}
