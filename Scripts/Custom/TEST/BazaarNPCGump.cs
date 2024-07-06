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
			: base("Marchand du Bazaar", 360, 440, true) // Ajuster les dimensions du cadre initial
		{
			_player = player;
			_trader = trader;
			_requiredResource = requiredResource;
			_requiredQuantity = requiredQuantity;
			_requiredResourceArtID = requiredResourceArtID;
			_requiredResourceName = requiredResourceName;
			_offeredResources = offeredResources;

			AddPage(0);

			AddHtml(20, 5, 360, 20, "", false, false);

			int y = 95; // Remonter la première ressource de 10 pixels
			int buttonID = 1;

			foreach (var (resource, quantity, resourceArtID, resourceName) in _offeredResources)
			{
				if (resource != null)
				{
					// Draw rectangle around the entire entry, adjusted by 5 pixels to the right and 10 pixels shorter
					AddImageTiled(98, y, 340, 80, 0x2422); // Ajuster les dimensions du rectangle entourant chaque offre
					AddImageTiled(99, y + 1, 338, 78, 0x2430);

					// Display the offered item art and label
					AddItem(140, y + 15, resourceArtID);
					AddLabel(140, y + 60, 0x486, $"{resourceName} ({quantity})");

					// Display the accept button
					AddButton(250, y + 35, 0xFA5, 0xFA7, buttonID, GumpButtonType.Reply, 0);

					// Display the required item art and label
					AddItem(320, y + 15, _requiredResourceArtID);
					AddLabel(320, y + 60, 0x486, $"{_requiredResourceName} ({_requiredQuantity})");

					y += 90; // Augmenter y pour laisser de l'espace entre les entrées
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
