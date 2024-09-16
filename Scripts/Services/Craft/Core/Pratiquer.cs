using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server.Engines.Craft
{
	public class Pratiquer
	{
		public static DateTime NextAllowedTime = DateTime.MinValue;

		public Pratiquer()
		{
		}

		public static void Do(Mobile from, CraftSystem craftSystem, ITool tool)
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

					Type itemType = GetRequiredItemType(craftSystem);

					if (pack.ConsumeUpTo(itemType, 1) > 0)
					{
						StartCraftingProcess(craftSystem, from, tool);
					}
					else
					{
						string notice = "Vous n'avez pas assez de matériaux pour pratiquer.";
						from.SendGump(new CraftGump(from, craftSystem, tool, notice));
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Erreur dans Pratiquer.Do: {e.Message}");
			}
		}

		private static Type GetRequiredItemType(CraftSystem craftSystem)
		{
			if (craftSystem is DefTailoring)
				return typeof(Cloth);
			else if (craftSystem is DefCartography)
				return typeof(BlankMap);
			else if (craftSystem is DefCooking)
				return typeof(Dough);
			else if (craftSystem is DefInscription)
				return typeof(BlankScroll);
			else if (craftSystem is DefTinkering)
				return typeof(IronIngot);
			else if (craftSystem is DefAlchemy)
				return typeof(Bottle);
			else
				return craftSystem.CraftSubRes.GetAt(0).ItemType;
		}

		private static void StartCraftingProcess(CraftSystem craftSystem, Mobile from, ITool tool)
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
					bool toolBroken = false;

					if (from is PlayerMobile pm)
					{
						int chance = 100;

						if (chance < 5)
							chance = 5;

						if (chance > Utility.RandomMinMax(0, 100))
							tool.UsesRemaining--;

						if (tool.UsesRemaining < 1)
							toolBroken = true;
					}

					SkillName skillName = craftSystem.MainSkill;
					Skill skill = from.Skills[skillName];
					double minSkill = 0.0;
					double maxSkill = 100.0;

					if (SkillCheck.Mobile_SkillCheckLocation(from, skillName, minSkill, maxSkill))
					{
						//from.SendMessage($"Vous avez amélioré votre compétence en {craftSystem.MainSkill}.");

						if (from.SkillsTotal >= from.SkillsCap)
						{
							Skill skillToDecrease = FindSkillToDecrease(from, skill);

							if (skillToDecrease != null)
							{
								double decrease = 0.1;
								skillToDecrease.Base -= decrease;
							//	from.SendMessage($"Votre compétence en {skillToDecrease.Info.Name} a diminué de {decrease:F1} points pour compenser.");
							}
						}
					}
					else
					{
						//from.SendMessage("Vous avez pratiqué, mais n'avez pas amélioré votre compétence cette fois-ci.");
					}

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

		private static Skill FindSkillToDecrease(Mobile from, Skill skillToIncrease)
		{
			foreach (Skill skill in from.Skills)
			{
				if (skill != skillToIncrease && skill.Lock == SkillLock.Down && skill.Base > 0)
				{
					return skill;
				}
			}
			return null;
		}
	}
}