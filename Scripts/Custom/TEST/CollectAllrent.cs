using Server;
using Server.Commands;

namespace Knives.TownHouses
{
	public class CollectRentsCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register("CollectAllRents", AccessLevel.Administrator, new CommandEventHandler(CollectAllRents_OnCommand));
		}

		[Usage("CollectAllRents")]
		[Description("Collects rent for all town houses and resets their timers.")]
		public static void CollectAllRents_OnCommand(CommandEventArgs e)
		{
			TownHouseSign.CollectAllRents();
			e.Mobile.SendMessage("All rents have been collected and timers reset.");
		}
	}
}
