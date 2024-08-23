using Server.Items;
using Server.Targeting;
using Server.Gumps;
using System;
using static Server.Engines.Craft.ConfirmSmeltGump;

namespace Server.Engines.Craft
{
	public enum SmeltResult
	{
		Success,
		Invalid,
		NoSkill
	}

	public class Resmelt
	{
		public static void Do(Mobile from, CraftSystem craftSystem, ITool tool)
		{
			int num = craftSystem.CanCraft(from, tool, null);

			if (num > 0 && num != 1044267)
			{
				from.SendGump(new CraftGump(from, craftSystem, tool, num));
			}
			else
			{
				from.Target = new InternalTarget(craftSystem, tool);
				from.SendLocalizedMessage(1044273); // Target an item to recycle.
			}
		}

		private class InternalTarget : Target, ISmeltTarget
		{
			private readonly CraftSystem m_CraftSystem;
			private readonly ITool m_Tool;

			public InternalTarget(CraftSystem craftSystem, ITool tool)
				: base(2, false, TargetFlags.None)
			{
				m_CraftSystem = craftSystem;
				m_Tool = tool;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				int num = m_CraftSystem.CanCraft(from, m_Tool, null);

				if (num > 0)
				{
					if (num == 1044267)
					{
						bool anvil, forge;

						DefBlacksmithy.CheckAnvilAndForge(from, 2, out anvil, out forge);

						if (!anvil)
							num = 1044266; // You must be near an anvil
						else if (!forge)
							num = 1044265; // You must be near a forge.
					}

					from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, num));
				}
				else
				{
					if (IsEpicOrLegendary(targeted))
					{
						from.SendGump(new ConfirmSmeltGump(from, this, targeted));
					}
					else
					{
						PerformSmelt(from, targeted);
					}
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

			public void PerformSmelt(Mobile from, object targeted)
			{
				SmeltResult result = SmeltResult.Invalid;
				bool isStoreBought = false;
				int message;

				if (targeted is BaseArmor)
				{
					result = Resmelt(from, (BaseArmor)targeted, ((BaseArmor)targeted).Resource);
					isStoreBought = !((BaseArmor)targeted).PlayerConstructed;
				}
				else if (targeted is BaseWeapon)
				{
					result = Resmelt(from, (BaseWeapon)targeted, ((BaseWeapon)targeted).Resource);
					isStoreBought = !((BaseWeapon)targeted).PlayerConstructed;
				}
				else if (targeted is BaseThrown)
				{
					result = Resmelt(from, (BaseThrown)targeted, ((BaseThrown)targeted).Resource);
					isStoreBought = !((BaseThrown)targeted).PlayerConstructed;
				}
				else if (targeted is BaseRanged)
				{
					result = Resmelt(from, (BaseRanged)targeted, ((BaseRanged)targeted).Resource);
					isStoreBought = !((BaseRanged)targeted).PlayerConstructed;
				}
				else if (targeted is DragonBardingDeed)
				{
					result = Resmelt(from, (DragonBardingDeed)targeted, ((DragonBardingDeed)targeted).Resource);
					isStoreBought = false;
				}

				switch (result)
				{
					default:
					case SmeltResult.Invalid:
						message = 1044272; break; // You can't melt that down into ingots.
					case SmeltResult.NoSkill:
						message = 1044269; break; // You have no idea how to work this metal.
					case SmeltResult.Success:
						message = isStoreBought ? 500418 : 1044270; break; // You melt the item down into ingots.
				}

				from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, message));
			}

			private SmeltResult Resmelt(Mobile from, Item item, CraftResource resource)
			{
				try
				{
					if (CraftResources.GetType(resource) != CraftResourceType.Metal)
						return SmeltResult.Invalid;

					CraftResourceInfo info = CraftResources.GetInfo(resource);

					if (info == null || info.ResourceTypes.Length == 0)
						return SmeltResult.Invalid;

					CraftItem craftItem = m_CraftSystem.CraftItems.SearchFor(item.GetType());

					if (craftItem == null || craftItem.Resources.Count == 0)
						return SmeltResult.Invalid;

					CraftRes craftResource = craftItem.Resources.GetAt(0);

					if (craftResource.Amount < 2)
						return SmeltResult.Invalid; // Not enough metal to resmelt

					double difficulty = 0.0;

					switch (resource)
					{
						case CraftResource.Iron:
						case CraftResource.Bronze:
						case CraftResource.Copper:
							difficulty = 10.0;
							break;
						case CraftResource.Sonne:
						case CraftResource.Argent:
						case CraftResource.Boreale:
						case CraftResource.Chrysteliar:
						case CraftResource.Glacias:
						case CraftResource.Lithiar:
							difficulty = 30.0;
							break;
						case CraftResource.Acier:
						case CraftResource.Durian:
						case CraftResource.Equilibrum:
						case CraftResource.Gold:
						case CraftResource.Jolinar:
						case CraftResource.Justicium:
							difficulty = 50.0;
							break;
						case CraftResource.Abyssium:
						case CraftResource.Bloodirium:
						case CraftResource.Herbrosite:
						case CraftResource.Khandarium:
						case CraftResource.Mytheril:
						case CraftResource.Sombralir:
							difficulty = 60.0;
							break;
						case CraftResource.Draconyr:
						case CraftResource.Heptazion:
						case CraftResource.Oceanis:
						case CraftResource.Brazium:
						case CraftResource.Lunerium:
						case CraftResource.Marinar:
							difficulty = 80.0;
							break;
						case CraftResource.Nostalgium:
							difficulty = 100.0;
							break;
					}

					double skill = Math.Max(from.Skills[SkillName.Mining].Value, from.Skills[SkillName.Blacksmith].Value);

					if (difficulty > skill)
						return SmeltResult.NoSkill;

					Type resourceType = info.ResourceTypes[0];
					Item ingot = (Item)Activator.CreateInstance(resourceType);

					if (item is DragonBardingDeed || (item is BaseArmor && ((BaseArmor)item).PlayerConstructed) || (item is BaseWeapon && ((BaseWeapon)item).PlayerConstructed) || (item is BaseClothing && ((BaseClothing)item).PlayerConstructed))
						ingot.Amount = (int)(craftResource.Amount * .50);
					else
						ingot.Amount = 1;

					item.Delete();
					from.AddToBackpack(ingot);

					from.PlaySound(0x2A);
					from.PlaySound(0x240);
					return SmeltResult.Success;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.ToString());
				}

				return SmeltResult.Invalid;
			}
		}
	}

	public class ConfirmSmeltGump : Gump
	{
		private Mobile m_From;
		private ISmeltTarget m_Target;
		private object m_Targeted;

		public ConfirmSmeltGump(Mobile from, ISmeltTarget target, object targeted) : base(50, 50)
		{
			m_From = from;
			m_Target = target;
			m_Targeted = targeted;


			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage(0);
			AddBackground(0, 0, 240, 135, 9200);
			AddAlphaRegion(10, 10, 220, 115);

			AddHtml(10, 10, 220, 75, "<BASEFONT COLOR=#FFFFFF>Cet objet est épique ou légendaire. Êtes-vous sûr de vouloir le fondre ?</BASEFONT>", false, false);

			AddButton(40, 95, 4005, 4007, 1, GumpButtonType.Reply, 0);
			AddHtml(75, 95, 100, 35, "<BASEFONT COLOR=#FFFFFF>OUI</BASEFONT>", false, false);

			AddButton(135, 95, 4005, 4007, 0, GumpButtonType.Reply, 0);
			AddHtml(170, 95, 100, 35, "<BASEFONT COLOR=#FFFFFF>NON</BASEFONT>", false, false);
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			if (info.ButtonID == 1)
			{
				// L'utilisateur a confirmé, procédez au resmelt
				m_Target.PerformSmelt(m_From, m_Targeted);
			}
			else
			{
				// L'utilisateur a annulé, ne faites rien
				m_From.SendLocalizedMessage(500435); // You decide not to smelt the item.
			}
		}
		public interface ISmeltTarget
		{
			void PerformSmelt(Mobile from, object targeted);
		}
	}
}
