using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;

namespace Server.Gumps
{
	public class TradeNPCGump : BaseProjectMGump
	{
		private PlayerMobile _player;
		private TradeNPC _trader;

		private Type _requiredResource; // Type of resource required for trade
		private List<(Type, int)> _offeredResources; // Type and quantity of resources offered in exchange
		private int _requiredQuantity; // Quantity of required resource

		public TradeNPCGump(PlayerMobile player, TradeNPC trader, Type requiredResource, int requiredQuantity, List<(Type, int)> offeredResources)
			: base("Marchand Itinérant", 100, 50, true)
		{
			_player = player;
			_trader = trader;
			_requiredResource = requiredResource;
			_requiredQuantity = requiredQuantity;
			_offeredResources = offeredResources;

			AddPage(0);
			AddBackground(0, 0, 300, 300, 0x13EC);

			AddHtml(20, 20, 260, 20, "<center><b>Offre d'échange</b></center>", false, false);

			int y = 60;
			int buttonID = 1;

			foreach (var (resource, quantity) in _offeredResources)
			{
				if (resource != null)
				{
					AddLabel(40, y, 0x486, $"Échangez {_requiredQuantity} {_requiredResource?.Name ?? "Ressource"} contre {quantity} {resource?.Name ?? "Ressource"}");
					AddButton(40, y + 20, 0xFA5, 0xFA7, buttonID, GumpButtonType.Reply, 0); // Accept button
					y += 40;
					buttonID++;
				}
			}
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			int buttonID = info.ButtonID - 1;

			if (buttonID >= 0 && buttonID < _offeredResources.Count)
			{
				var (selectedResource, selectedQuantity) = _offeredResources[buttonID];

				// Check if player has required resource
				if (_player.Backpack != null && _player.Backpack.ConsumeTotal(_requiredResource, _requiredQuantity))
				{
					// Give player the reward
					for (int i = 0; i < selectedQuantity; i++)
					{
						_player.Backpack.AddItem((Item)Activator.CreateInstance(selectedResource)); // Use Activator
					}
					_player.SendMessage("Vous avez échangé {0} {1} contre {2} {3}.", _requiredQuantity, _requiredResource.Name, selectedQuantity, selectedResource.Name);
				}
				else
				{
					_player.SendMessage("Vous n'avez pas assez de {0}.", _requiredResource.Name);
				}
			}
		}
	}
}
