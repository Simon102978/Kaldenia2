using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Scripts.Custom
{
	public class MageryRangedWeaponRestriction
	{
		public static void Initialize()
		{
			EventSink.EquipRequest += OnEquipRequest;
		}

		public static void OnEquipRequest(EquipRequestEventArgs e)
		{
			if (e.Mobile is PlayerMobile player && e.Item is BaseRanged)
			{
				if (player.Skills.Magery.Base >= 40.0)
				{
					player.SendMessage("Vous ne pouvez pas équiper une arme à distance avec 40 ou plus en Magery.");
					e.Block = true;
				}
			}
		}
	}
}
