using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Engines.Craft
{
	public class Pratiquer
	{
		public static DateTime NextAllowedTime = DateTime.MinValue;

		public Pratiquer()
		{
		}

		public static void Do(Mobile from, CraftSystem craftSystem, BaseTool tool)
		{
			try
			{
				if (NextAllowedTime > DateTime.UtcNow)
				{
					from.SendGump(new CraftGump(from, craftSystem, tool, "Vous devez attendre avant de pratiquer à nouveau."));
					return;
				}

				Container pack = from.Backpack;

				if (pack != null)
				{
					if (craftSystem is DefBlacksmithy)
					{
						bool anvil, forge;
						DefBlacksmithy.CheckAnvilAndForge(from, 2, out anvil, out forge);

						if (!anvil || !forge)
						{
							from.SendGump(new CraftGump(from, craftSystem, tool, "Vous devez être près d'une forge et d'un enclume pour pouvoir pratiquer."));
							return;
						}
					}

					CraftContext context = craftSystem.GetContext(from);

					if (context == null)
						return;

					int index = context.LastResourceIndex;

					if (index < 0)
						index = 0;

					Type itemType = null;

					if (craftSystem is DefTailoring)
						itemType = typeof(Cloth);
					else if (craftSystem is DefCartography)
						itemType = typeof(BlankMap);
					else if (craftSystem is DefCooking)
						itemType = typeof(Dough);
					else if (craftSystem is DefInscription)
						itemType = typeof(BlankScroll);
					else if (craftSystem is DefTinkering)
						itemType = typeof(IronIngot);
					else if (craftSystem is DefAlchemy)
						itemType = typeof(Bottle);
					else
						itemType = craftSystem.CraftSubRes.GetAt(index).ItemType;

					if (pack.ConsumeUpTo(itemType, 1) > 0)
					{
						StartCraftingProcess(craftSystem, from, tool);
					}
					else
					{
						string notice = "";

						if (craftSystem is DefBlacksmithy)
							notice = "Vous n'avez pas assez de lingot.";
						else if (craftSystem is DefBowFletching)
							notice = "Vous n'avez pas assez de matériaux.";
						else if (craftSystem is DefCarpentry)
							notice = "Vous n'avez pas assez de planche.";
						else if (craftSystem is DefCartography)
							notice = "Vous n'avez pas assez de carte vierge.";
						else if (craftSystem is DefCooking)
							notice = "Vous n'avez pas assez de pâte.";
						else if (craftSystem is DefInscription)
							notice = "Vous n'avez pas assez de parchemin vierge.";
						else if (craftSystem is DefTailoring)
							notice = "Vous n'avez pas assez de tissu.";
						else if (craftSystem is DefTinkering)
							notice = "Vous n'avez pas assez de lingot.";
						else if (craftSystem is DefAlchemy)
							notice = "Vous n'avez pas assez de bouteille.";

						from.SendGump(new CraftGump(from, craftSystem, tool, notice));
					}
				}
			}
			catch
			{
			}
		}

		private static void StartCraftingProcess(CraftSystem craftSystem, Mobile from, BaseTool tool)
		{
			int count = 0;
			Timer timer = null;

			timer = Timer.DelayCall(TimeSpan.FromSeconds(1.4), TimeSpan.FromSeconds(1.4), () =>
			{
				if (craftSystem is DefBlacksmithy)
				{
					bool anvil, forge;
					DefBlacksmithy.CheckAnvilAndForge(from, 2, out anvil, out forge);

					if (!anvil || !forge)
					{
						from.SendGump(new CraftGump(from, craftSystem, tool, "Vous devez être près d'une forge et d'un enclume pour pouvoir pratiquer."));
						timer.Stop();
						return;
					}
				}

				craftSystem.PlayCraftEffect(from);

				if (count == 0)
				{
					NextAllowedTime = DateTime.UtcNow + TimeSpan.FromSeconds(10.0);
					from.SendGump(new CraftGump(from, craftSystem, tool, null));
				}
				else if (count == 3)
				{
					NextAllowedTime = DateTime.UtcNow;
					bool checkSkills = true;
					bool toolBroken = false;

					if (from is CustomPlayerMobile pm)
					{
						int chance = 20;

						if (chance < 5)
							chance = 5;

						if (chance > Utility.RandomMinMax(0, 100))
							tool.UsesRemaining--;

						if (tool.UsesRemaining < 1)
							toolBroken = true;
					}

					if (checkSkills)
						from.CheckSkill(craftSystem.MainSkill, 0.0, 100.0);

					if (toolBroken)
					{
						from.SendMessage("Vous avez brisé votre outil.");
						tool.Delete();
					}

					timer.Stop();
					return;
				}

				count++;
			});
		}
	}
}
