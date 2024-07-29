using Server.Mobiles;
using Server.Network;
using Server.Prompts;
using Server.Targeting;
using System;
using System.Collections.Generic;

namespace Server.Commands
{
    public class VisibilityList
    {
        public static void Initialize()
        {
            CommandSystem.Register("Vis", AccessLevel.Player, Vis_OnCommand);
            CommandSystem.Register("VisList", AccessLevel.Player, VisList_OnCommand);
            CommandSystem.Register("VisClear", AccessLevel.Player, VisClear_OnCommand);
        }

        [Usage("Vis")]
        [Description("Adds or removes a targeted player from your visibility list.  Anyone on your visibility list will be able to see you at all times, even when you're hidden.")]
        public static void Vis_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile is PlayerMobile)
            {
                e.Mobile.Target = new VisTarget();
                e.Mobile.SendMessage("Select person to add or remove from your visibility list.");
            }
        }

        [Usage("VisList")]
        [Description("Shows the names of everyone in your visibility list.")]
        public static void VisList_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)e.Mobile;
                List<Mobile> list = pm.VisibilityList;

                if (list.Count > 0)
                {
                    pm.SendMessage("You are visible to {0} mobile{1}:", list.Count, list.Count == 1 ? "" : "s");

                    for (int i = 0; i < list.Count; ++i)
                        pm.SendMessage("#{0}: {1}", i + 1, list[i].Name);
                }
                else
                {
                    pm.SendMessage("Your visibility list is empty.");
                }
            }
        }

        [Usage("VisClear")]
        [Description("Removes everyone from your visibility list.")]
        public static void VisClear_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)e.Mobile;
                List<Mobile> list = new List<Mobile>(pm.VisibilityList);

                pm.VisibilityList.Clear();
                pm.SendMessage("Your visibility list has been cleared.");

                for (int i = 0; i < list.Count; ++i)
                {
                    Mobile m = list[i];

                    if (!m.CanSee(pm) && Utility.InUpdateRange(m, pm))
                        m.Send(pm.RemovePacket);
                }
            }
        }

        private class VisTarget : Target
        {
            public VisTarget()
                : base(-1, false, TargetFlags.None)
            {
            }

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (from is PlayerMobile && targeted is Mobile)
				{
					PlayerMobile pm = (PlayerMobile)from;
					Mobile targ = (Mobile)targeted;

					Console.WriteLine($"Debug: OnTarget called. From: {from.Name}, Target: {targ.Name}");

					if (targ.AccessLevel <= from.AccessLevel)
					{
						List<Mobile> list = pm.VisibilityList;

						if (list.Contains(targ))
						{
							// Au lieu de retirer automatiquement, demandez confirmation
							from.SendMessage("This player is already in your visibility list. Do you want to remove them? (Y/N)");
							from.SendMessage("Type Y to remove, or N to keep them in the list.");
							from.Prompt = new RemoveConfirmPrompt(targ);
						}
						else
						{
							list.Add(targ);
							from.SendMessage("{0} has been added to your visibility list.", targ.Name);
							Console.WriteLine($"Debug: {targ.Name} added to {from.Name}'s visibility list.");

							// Mise à jour de la visibilité
							UpdateVisibility(from, targ);
						}
					}
					else
					{
						from.SendMessage("They can already see you!");
						Console.WriteLine($"Debug: {targ.Name} already has higher access level than {from.Name}.");
					}
				}
				else
				{
					from.SendMessage("Add only mobiles to your visibility list.");
					Console.WriteLine("Debug: Invalid target type.");
				}
			}

			private class RemoveConfirmPrompt : Prompt
			{
				private Mobile _target;

				public RemoveConfirmPrompt(Mobile target)
				{
					_target = target;
				}

				public override void OnResponse(Mobile from, string text)
				{
					if (from is PlayerMobile pm)
					{
						text = text.Trim().ToLower();
						if (text == "y" || text == "yes")
						{
							pm.VisibilityList.Remove(_target);
							from.SendMessage("{0} has been removed from your visibility list.", _target.Name);
							Console.WriteLine($"Debug: {_target.Name} removed from {from.Name}'s visibility list.");

							// Mise à jour de la visibilité
							UpdateVisibility(from, _target);
						}
						else
						{
							from.SendMessage("{0} remains in your visibility list.", _target.Name);
						}
					}
				}
			}

			private static void UpdateVisibility(Mobile from, Mobile targ)
			{
				if (Utility.InUpdateRange(targ, from))
				{
					NetState ns = targ.NetState;

					if (ns != null)
					{
						if (targ.CanSee(from))
						{
							ns.Send(new MobileIncoming(targ, from));
							ns.Send(from.OPLPacket);

							foreach (Item item in from.Items)
								ns.Send(item.OPLPacket);
						}
						else
						{
							ns.Send(from.RemovePacket);
						}
					}
				}
			}
		}
	}
}
