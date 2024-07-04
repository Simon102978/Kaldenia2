using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Items; // Assurez-vous d'inclure cette directive pour accéder aux classes d'objets

namespace Server.Gumps
{
	public class BazaarNPCGump : BaseProjectMGump
	{
		private PlayerMobile _player;
		private BazaarNPC _trader;

		private Type _requiredResource;
		private int _requiredQuantity;
		private int _requiredResourceArtID;
		private string _requiredResourceName;

		private List<(Type, int, int, string)> _offeredResources;

		public BazaarNPCGump(PlayerMobile player, BazaarNPC trader, Type requiredResource, int requiredQuantity, int requiredResourceArtID, string requiredResourceName, List<(Type, int, int, string)> offeredResources)
			: base("Marchand Itinérant", 300, 350, true)
		{
			_player = player;
			_trader = trader;
			_requiredResource = requiredResource;
			_requiredQuantity = requiredQuantity;
			_requiredResourceArtID = requiredResourceArtID;
			_requiredResourceName = requiredResourceName;
			_offeredResources = offeredResources;

			AddPage(0);
			//AddBackground(0, 0, 400, 800, 0x4CC);

			AddHtml(20, 5, 360, 20, "", false, false);

			int y = 110;
			int buttonID = 1;

			foreach (var (resource, quantity, resourceArtID, resourceName) in _offeredResources)
			{
				if (resource != null)
				{
					AddItem(110, y, resourceArtID); // Display the offered item art
					AddLabel(110, y + 40, 0x486, $"{resourceName} ({quantity})");

					AddButton(220, y + 20, 0xFA5, 0xFA7, buttonID, GumpButtonType.Reply, 0); // Accept button

					AddItem(290, y, _requiredResourceArtID); // Display the required item art
					AddLabel(290, y + 40, 0x486, $"{_requiredResourceName} ({_requiredQuantity})"); // Display the required item name with custom name

					y += 60;
					buttonID++;
				}
			}
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			int buttonID = info.ButtonID - 1;

			if (buttonID >= 0 && buttonID < _offeredResources.Count)
			{
				var (selectedResource, selectedQuantity, _, selectedResourceName) = _offeredResources[buttonID];

				try
				{
					// Debug log to track the execution
					Console.WriteLine($"Button clicked: {buttonID}, Resource: {selectedResource}, Quantity: {selectedQuantity}");

					// Check if player has required resource
					if (_player.Backpack != null && _player.Backpack.ConsumeTotal(_requiredResource, _requiredQuantity))
					{
						// Give player the reward
						for (int i = 0; i < selectedQuantity; i++)
						{
							Item item = (Item)Activator.CreateInstance(selectedResource);
							if (item != null)
							{
								// Add item to backpack
								AddItemsToBackpack(_player, item.GetType(), 1);
							}
							else
							{
								Console.WriteLine("Failed to create instance of item.");
							}
						}
						_player.SendMessage("Vous avez échangé {0} {1} contre {2} {3}.", _requiredQuantity, _requiredResourceName, selectedQuantity, selectedResourceName);
					}
					else
					{
						_player.SendMessage("Vous n'avez pas assez de {0}.", _requiredResourceName);
					}
				}
				catch (Exception ex)
				{
					// Detailed error message
					Console.WriteLine($"Error during trade: {ex.Message}\nStack Trace: {ex.StackTrace}");
					_player.SendMessage("Une erreur s'est produite lors de l'échange. Veuillez réessayer.");
				}
			}
		}

		private void AddItemsToBackpack(PlayerMobile player, Type itemType, int quantity)
		{
			Item existingItem = player.Backpack.FindItemByType(itemType);

			if (existingItem != null)
			{
				// If the item already exists in the backpack, increase its amount
				existingItem.Amount += quantity;
			}
			else
			{
				// If the item doesn't exist, create a new item with the specified quantity
				Item newItem = (Item)Activator.CreateInstance(itemType);
				newItem.Amount = quantity;
				player.Backpack.AddItem(newItem); // Use AddItem to add the item to backpack
			}
		}
	}
}
