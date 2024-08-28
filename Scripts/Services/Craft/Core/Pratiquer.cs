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

		public static void Do(Mobile from, CraftSystem craftSystem, ITool tool)
		{
			try
			{
				if (NextAllowedTime > DateTime.UtcNow)
				{
					from.SendGump(new CraftGump(from, craftSystem, tool, "Vous devez attendre avant de pratiquer � nouveau."));
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
							from.SendGump(new CraftGump(from, craftSystem, tool, "Vous devez �tre pr�s d'une forge et d'un enclume pour pouvoir pratiquer."));
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
						string notice = "Vous n'avez pas assez de mat�riaux pour pratiquer.";
						from.SendGump(new CraftGump(from, craftSystem, tool, notice));
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Erreur dans Pratiquer.Do: {e.Message}");
			}
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
						from.SendGump(new CraftGump(from, craftSystem, tool, "Vous devez �tre pr�s d'une forge et d'un enclume pour pouvoir pratiquer."));
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

                    Skill skill = from.Skills[craftSystem.MainSkill];
                    double oldValue = skill.Base;

                    if (AttemptSkillGain(oldValue))
                    {
                        double gainAmount = CalculateSkillGain(oldValue);
                        skill.Base += gainAmount;
                        from.SendMessage($"Votre comp�tence en {craftSystem.MainSkill} a augment� de {gainAmount:F1} points.");
                    }
                    else
                    {
                        from.SendMessage("Vous avez pratiqu�, mais n'avez pas am�lior� votre comp�tence cette fois-ci.");
                    }

                    if (toolBroken)
                    {
                        from.SendMessage("Vous avez bris� votre outil.");
                        tool.Delete();
                    }

                    timer.Stop();
                    return;
                }

                count++;
            });
        }

        private static bool AttemptSkillGain(double currentSkill)
        {
            int baseChance = 70; // 70% de chance de base pour gagner une comp�tence

            if (currentSkill < 50.0)
                baseChance += 20; // 90% de chance pour les comp�tences inf�rieures � 50
            else if (currentSkill < 70.0)
                baseChance += 10; // 80% de chance pour les comp�tences entre 50 et 70
            else if (currentSkill >= 90.0)
                baseChance -= 20; // 50% de chance pour les comp�tences sup�rieures � 90

            return Utility.RandomMinMax(1, 100) <= baseChance;
        }

        private static double CalculateSkillGain(double currentSkill)
        {
            if (currentSkill < 50.0)
                return Utility.RandomDouble() * 0.5;
            else if (currentSkill < 70.0)
                return Utility.RandomDouble() * 0.3;
            else if (currentSkill < 90.0)
                return Utility.RandomDouble() * 0.1;
            else
                return Utility.RandomDouble() * 0.05;
        }
    }
}
