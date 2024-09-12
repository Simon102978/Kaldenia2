using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Commands;

namespace Server.Commands
{
	public class ResetColor
	{
		public static void Initialize()
		{
			CommandSystem.Register("ResetColor", AccessLevel.Administrator, new CommandEventHandler(ResetColor_OnCommand));
		}

		[Usage("ResetColor")]
		[Description("Resets the color of all resource-based items in the world.")]
		public static void ResetColor_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;
			int itemsChanged = 0;

			List<Item> allItems = new List<Item>(World.Items.Values);
			foreach (Item item in allItems)
			{
				CraftResource resource = GetItemResource(item);
				if (resource != CraftResource.None)
				{
					int newHue = GetResourceHue(resource);
					if (newHue != 0 && item.Hue != newHue)
					{
						item.Hue = newHue;
						itemsChanged++;
					}
				}
			}

			from.SendMessage($"Reset colors on {itemsChanged} items in the world.");
		}

		private static CraftResource GetItemResource(Item item)
		{
			CraftResource resource = CraftResource.None;

			try
			{
				resource = (CraftResource)item.GetType().GetProperty("Resource")?.GetValue(item, null);
			}
			catch
			{
				// Si une exception se produit, ignorez-la et continuez avec les vérifications spécifiques
			}

			if (resource != CraftResource.None)
				return resource;

			if (item is BaseArmor armor) return armor.Resource;
			if (item is BaseWeapon weapon) return weapon.Resource;
			if (item is BaseClothing clothing) return clothing.Resource;
			if (item is BaseIngot ingot) return ingot.Resource;
			if (item is BaseOre ore) return ore.Resource;
			if (item is BaseLeather leather) return leather.Resource;
			if (item is BaseHides hides) return hides.Resource;
			if (item is BaseBone bone) return bone.Resource;
			return CraftResource.None;
		}

		private static int GetResourceHue(CraftResource resource)
		{
			switch (resource)
			{
				// Leather resources
				case CraftResource.RegularLeather: return 0;
				case CraftResource.LupusLeather: return 1106;
				case CraftResource.ReptilienLeather: return 1438;
				case CraftResource.GeantLeather: return 1711;
				case CraftResource.OphidienLeather: return 2494;
				case CraftResource.ArachnideLeather: return 2143;
				case CraftResource.DragoniqueLeather: return 2146;
				case CraftResource.DemoniaqueLeather: return 2328;
				case CraftResource.AncienLeather: return 2337;

				// Bone resources
				case CraftResource.RegularBone: return 0;
				case CraftResource.LupusBone: return 1106;
				case CraftResource.ReptilienBone: return 1438;
				case CraftResource.GeantBone: return 1711;
				case CraftResource.OphidienBone: return 2494;
				case CraftResource.ArachnideBone: return 2143;
				case CraftResource.DragoniqueBone: return 2146;
				case CraftResource.DemoniaqueBone: return 2328;
				case CraftResource.AncienBone: return 2337;

				// Metal resources
				case CraftResource.Iron: return 0;
				case CraftResource.Bronze: return 1122;
				case CraftResource.Copper: return 1134;
				case CraftResource.Sonne: return 1124;
				case CraftResource.Argent: return 2500;
				case CraftResource.Boreale: return 2731;
				case CraftResource.Chrysteliar: return 1759;
				case CraftResource.Glacias: return 1365;
				case CraftResource.Lithiar: return 1448;
				case CraftResource.Acier: return 1102;
				//case CraftResource.Durian: return 1160;
				case CraftResource.Equilibrum: return 2212;
				case CraftResource.Gold: return 2886;
				case CraftResource.Jolinar: return 2205;
				case CraftResource.Justicium: return 2219;
				case CraftResource.Abyssium: return 1800;
				case CraftResource.Bloodirium: return 2299;
				case CraftResource.Herbrosite: return 2831;
				//case CraftResource.Khandarium: return 1746;
				case CraftResource.Mytheril: return 2432;
				case CraftResource.Sombralir: return 2856;
				case CraftResource.Draconyr: return 1411;
				case CraftResource.Heptazion: return 2130;
				case CraftResource.Oceanis: return 2591;
				case CraftResource.Brazium: return 1509;
				case CraftResource.Lunerium: return 2656;
				case CraftResource.Marinar: return 2246;
				case CraftResource.Nostalgium: return 1940;

				// AOS resources
				case CraftResource.DullCopper: return 0x973;
				case CraftResource.ShadowIron: return 0x966;
				case CraftResource.Agapite: return 1980;
				case CraftResource.Verite: return 2841;
				case CraftResource.Valorite: return 2867;

				default: return 0;
			}
		}

		
	}
}
