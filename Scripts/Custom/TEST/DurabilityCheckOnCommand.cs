using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Engines.Quests;

namespace Scripts.Custom
{
	public static class MobileExtensions
	{
		public static void PrivateOverheadMessage(this Mobile m, MessageType type, int hue, bool ascii, string text)
		{
			if (m.NetState != null)
			{
				m.NetState.Send(new UnicodeMessage(m.Serial, m.Body, type, hue, 3, m.Language, m.Name, text));
			}
		}
	}

	public class EquipmentDurabilitySystem : Timer
	{
		private const double DurabilityThreshold = 0.2; // 20%
		private static Dictionary<Serial, Dictionary<Serial, double>> lastNotifiedDurability = new Dictionary<Serial, Dictionary<Serial, double>>();

		public static void Initialize()
		{
			CommandSystem.Register("durabilite", AccessLevel.Player, new CommandEventHandler(OnCommand));
			new EquipmentDurabilitySystem().Start();
		}

		public EquipmentDurabilitySystem() : base(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10))
		{
		}

		protected override void OnTick()
		{
			foreach (NetState state in NetState.Instances)
			{
				Mobile m = state.Mobile;
				if (m != null && m.Player)
				{
					CheckPlayerEquipment(m);
					CheckPlayerHires(m);
				}
			}
		}

		[Usage("durabilite")]
		[Description("Affiche la durabilité de l'équipement du joueur et de ses BaseHire.")]
		public static void OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;
			ListEquipmentDurability(from);
		}

		private static void CheckPlayerEquipment(Mobile player)
		{
			if (!lastNotifiedDurability.ContainsKey(player.Serial))
			{
				lastNotifiedDurability[player.Serial] = new Dictionary<Serial, double>();
			}

			foreach (Item item in player.Items)
			{
				CheckItemDurability(player, item, player.Serial);
			}
		}

		private static void CheckPlayerHires(Mobile player)
		{
			foreach (Mobile m in World.Mobiles.Values)
			{
				if (m is BaseHire hire && hire.ControlMaster == player)
				{
					if (!lastNotifiedDurability.ContainsKey(hire.Serial))
					{
						lastNotifiedDurability[hire.Serial] = new Dictionary<Serial, double>();
					}

					foreach (Item item in hire.Items)
					{
						CheckItemDurability(player, item, hire.Serial, hire.Name);
					}
				}
			}
		}

		private static void CheckItemDurability(Mobile player, Item item, Serial ownerSerial, string ownerName = null)
		{
			if (item is IWearableDurability || item is BaseWeapon || item is BaseArmor || item is BaseClothing)
			{
				double durabilityPercentage = GetDurabilityPercentage(item);
				if (durabilityPercentage <= DurabilityThreshold)
				{
					if (!lastNotifiedDurability[ownerSerial].ContainsKey(item.Serial) ||
						durabilityPercentage < lastNotifiedDurability[ownerSerial][item.Serial])
					{
						string message = ownerName == null
							? $"{item.Name} est à {durabilityPercentage:P0} de durabilité!"
							: $"L'équipement {item.Name} de {ownerName} est à {durabilityPercentage:P0} de durabilité!";
						player.PrivateOverheadMessage(MessageType.Regular, 0x22, false, message);
						lastNotifiedDurability[ownerSerial][item.Serial] = durabilityPercentage;
					}
				}
				else if (lastNotifiedDurability[ownerSerial].ContainsKey(item.Serial))
				{
					lastNotifiedDurability[ownerSerial].Remove(item.Serial);
				}
			}
		}

		private static void ListEquipmentDurability(Mobile player)
		{
			player.SendMessage("Liste de l'équipement et sa durabilité :");
			ListMobileEquipmentDurability(player, player);

			foreach (Mobile m in World.Mobiles.Values)
			{
				if (m is BaseHire hire && hire.ControlMaster == player)
				{
					player.SendMessage($"Équipement de {hire.Name} :");
					ListMobileEquipmentDurability(player, hire);
				}
			}
		}

		private static void ListMobileEquipmentDurability(Mobile player, Mobile target)
		{
			foreach (Item item in target.Items)
			{
				if (item is IWearableDurability || item is BaseWeapon || item is BaseArmor || item is BaseClothing)
				{
					double durabilityPercentage = GetDurabilityPercentage(item);
					int currentHitPoints = GetCurrentHitPoints(item);
					int maxHitPoints = GetMaxHitPoints(item);
					string message = $"{item.Name}: {currentHitPoints}/{maxHitPoints} ({durabilityPercentage:P0})";
					if (durabilityPercentage <= DurabilityThreshold)
					{
						player.SendMessage(0x22, message); // Rouge pour les items à 20% ou moins
					}
					else
					{
						player.SendMessage(0x3B2, message); // Vert pour les items au-dessus de 20%
					}
				}
			}
		}

		private static double GetDurabilityPercentage(Item item)
		{
			if (item is IWearableDurability wearable)
			{
				return (double)wearable.HitPoints / wearable.MaxHitPoints;
			}
			else if (item is BaseWeapon weapon)
			{
				return (double)weapon.HitPoints / weapon.MaxHitPoints;
			}
			else if (item is BaseArmor armor)
			{
				return (double)armor.HitPoints / armor.MaxHitPoints;
			}
			else if (item is BaseClothing clothing)
			{
				return (double)clothing.HitPoints / clothing.MaxHitPoints;
			}
			return 1.0; // Si l'item n'a pas de durabilité, on considère qu'il est à 100%
		}

		private static int GetCurrentHitPoints(Item item)
		{
			if (item is IWearableDurability wearable)
			{
				return wearable.HitPoints;
			}
			else if (item is BaseWeapon weapon)
			{
				return weapon.HitPoints;
			}
			else if (item is BaseArmor armor)
			{
				return armor.HitPoints;
			}
			else if (item is BaseClothing clothing)
			{
				return clothing.HitPoints;
			}
			return 0;
		}

		private static int GetMaxHitPoints(Item item)
		{
			if (item is IWearableDurability wearable)
			{
				return wearable.MaxHitPoints;
			}
			else if (item is BaseWeapon weapon)
			{
				return weapon.MaxHitPoints;
			}
			else if (item is BaseArmor armor)
			{
				return armor.MaxHitPoints;
			}
			else if (item is BaseClothing clothing)
			{
				return clothing.MaxHitPoints;
			}
			return 0;
		}
	}
}
