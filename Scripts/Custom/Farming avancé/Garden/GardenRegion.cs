using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Regions
{
	public class GardenRegion : BaseRegion
	{
		public static readonly int HousePriority = DefaultPriority + 1;
		public static TimeSpan CombatHeatDelay = TimeSpan.FromSeconds(30.0);

		public GardenRegion(BaseGarden house)
			: base("Jardin", house.Map, HousePriority, GetArea(house))
		{
			Garden = house;
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public BaseGarden Garden { get; }


		public override bool AllowHousing(Mobile from, Point3D p)
		{
			return false;
		}

		public override void OnExit(Mobile m)
		{
			base.OnExit(m);

			BasketOfHerbs.CheckBonus(m);

			m.SendMessage("Vous sortez d'un jardin.");
		}

		public override void OnEnter(Mobile m)
		{
			Timer.DelayCall(TimeSpan.FromMilliseconds(500), () =>
			{
				m.SendEverything();
			});

			m.SendMessage("Vous entrez dans un jardin.");
		}

		private static Rectangle3D[] GetArea(BaseGarden house)
		{
			int x = house.X;
			int y = house.Y;

			Rectangle2D[] houseArea = house.Area;
			Rectangle3D[] area = new Rectangle3D[houseArea.Length];

			for (int i = 0; i < area.Length; i++)
			{
				Rectangle2D rect = houseArea[i];
				area[i] = ConvertTo3D(new Rectangle2D(x + rect.Start.X, y + rect.Start.Y, rect.Width, rect.Height));
			}

			

			return area;
		}
	}
}
