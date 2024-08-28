using Server.Commands;
using Server.Mobiles;
using Server;
using System;
using Server.Engines.Craft;
using Server.Items;

public class TestQualiteChances
{
	private const int BATCH_SIZE = 1000;
	private const int MAX_ITERATIONS = 1000000;

	public static void Initialize()
	{
		CommandSystem.Register("TestQualiteChances", AccessLevel.Administrator, new CommandEventHandler(TestQualiteChances_OnCommand));
	}

	[Usage("TestQualiteChances <quality>")]
	[Description("Tests the chances of getting a specific quality item.")]
	public static void TestQualiteChances_OnCommand(CommandEventArgs e)
	{
		if (e.Mobile is CustomPlayerMobile pm)
		{
			if (e.Length == 1)
			{
				string arg = e.GetString(0).ToLower();
				if (arg == "exceptional" || arg == "epic" || arg == "legendary")
				{
					pm.SendMessage("Début du test de qualité. Veuillez patienter...");
					RunQualityTest(pm, arg);
				}
				else
				{
					pm.SendMessage("Qualité invalide. Utilisez: exceptional, epic, ou legendary.");
				}
			}
			else
			{
				pm.SendMessage("Usage: TestQualiteChances <quality>");
			}
		}
	}

	private static void RunQualityTest(CustomPlayerMobile pm, string targetQuality, int currentCount = 0)
	{
		Timer.DelayCall(TimeSpan.FromMilliseconds(50), () => // Petit délai pour réduire la charge
		{
			bool success = false;
			int count = currentCount;
			ItemQuality quality = ItemQuality.Normal;

			for (int i = 0; i < BATCH_SIZE && count < MAX_ITERATIONS && !success; i++)
			{
				count++;
				var craftItem = new CraftItem(typeof(RingmailGloves), "Armure Légère", "Gants d'anneaux"); 
				quality = craftItem.GetQuality(DefBlacksmithy.CraftSystem, 4, pm, 5);

				if (quality.ToString().ToLower() == targetQuality)
				{
					success = true;
				}
			}

			if (success)
			{
				pm.SendMessage($"Vous avez obtenu une qualité {targetQuality} après {count} essais.");
			}
			else if (count >= MAX_ITERATIONS)
			{
				pm.SendMessage($"Échec après {MAX_ITERATIONS} essais.");
			}
			else
			{
				if (count % 100000 == 0) // Feedback tous les 100,000 essais
				{
					pm.SendMessage($"Test en cours... {count} essais effectués.");
				}
				RunQualityTest(pm, targetQuality, count);
			}
		});
	}
}
