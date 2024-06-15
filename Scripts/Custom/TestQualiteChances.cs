using Server.Mobiles;
using Server.Items;
using Server.Spells;
using Server.Commands;
using Server.Engines.Craft;

namespace Server.Scripts.Commands
{
    public class TestQualiteChances
    {
        public static void Initialize()
        {
            CommandSystem.Register("TestQualiteChances", AccessLevel.Administrator, new CommandEventHandler(TestQualiteChances_OnCommand));
        }

        public static void TestQualiteChances_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from is CustomPlayerMobile)
            {
                CustomPlayerMobile pm = (CustomPlayerMobile)from;

				string arg = e.GetString(0);

				var count = 0;
				bool success = false;
				var quality = ItemQuality.Normal;

				if (string.IsNullOrEmpty(arg))
				{
					pm.SendMessage("Vous devez entrer en paramètre la qualité désirée: [exceptional, epic, legendary]");
					return;
				}
				else if (arg.ToLower() == "legendary")
				{
					while(!success)
					{
						count++;
						var craftItem = new CraftItem(typeof(RingmailGloves), "Armure Légère", "Gants d’anneaux");
						quality = craftItem.GetQuality(DefBlacksmithy.CraftSystem, 4, pm, 5);

						if (quality == ItemQuality.Legendary)
							success = true;
					}
				}
				else if (arg.ToLower() == "epic")
				{
					while (!success)
					{
						count++;
						var craftItem = new CraftItem(typeof(RingmailGloves), "Armure Légère", "Gants d’anneaux");
						quality = craftItem.GetQuality(DefBlacksmithy.CraftSystem, 4, pm, 5);

						if (quality == ItemQuality.Epic)
							success = true;
					}
				}
				else if (arg.ToLower() == "exceptional")
				{
					while (!success)
					{
						count++;
						var craftItem = new CraftItem(typeof(RingmailGloves), "Armure Légère", "Gants d’anneaux");
						quality = craftItem.GetQuality(DefBlacksmithy.CraftSystem, 4, pm, 5);

						if (quality == ItemQuality.Exceptional)
							success = true;
					}
				}

				pm.SendMessage($"Vous avez obtenu une qualité {arg.ToLower()} après {count} essais.");
			}
        }
    }
}