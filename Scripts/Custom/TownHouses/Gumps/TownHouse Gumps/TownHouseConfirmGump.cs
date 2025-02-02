#region References
using System;

using Server;
#endregion

namespace Knives.TownHouses
{
	public class TownHouseConfirmGump : GumpPlusLight
	{
		private readonly TownHouseSign c_Sign;
		private bool c_Items;

		public TownHouseConfirmGump(Mobile m, TownHouseSign sign)
			: base(m, 100, 100)
		{
			c_Sign = sign;
		}

		protected override void BuildGump()
		{
			var width = 200;
			var y = 0;

			AddHtml(
				0,
				y += 10,
				width,
				String.Format("<CENTER>{0} Cette maison?", c_Sign.RentByTime == TimeSpan.Zero ? "Acheter" : "Louer"));
			AddImage(width / 2 - 100, y + 2, 0x39);
			AddImage(width / 2 + 70, y + 2, 0x3B);

			if (c_Sign.RentByTime == TimeSpan.Zero)
			{
				AddHtml(0, y += 25, width, String.Format("<CENTER>{0}: {1}", "Prix", c_Sign.Free ? "Gratuit" : "" + c_Sign.Price));
			}
			else if (c_Sign.RecurRent)
			{
				AddHtml(0, y += 25, width, String.Format("<CENTER>{0}: {1}", "Recurent " + c_Sign.PriceType, c_Sign.Price));
			}
			else
			{
				AddHtml(0, y += 25, width, String.Format("<CENTER>{0}: {1}", "Un " + c_Sign.PriceTypeShort, c_Sign.Price));
			}

			if (c_Sign.KeepItems)
			{
				AddHtml(0, y += 20, width, "<CENTER>Prix des Items: " + c_Sign.ItemsPrice);
				AddButton(20, y, c_Items ? 0xD3 : 0xD2, "Items", Items);
			}

			AddHtml(0, y += 20, width, "<CENTER>Lockdowns: " + c_Sign.Locks);
			AddHtml(0, y += 20, width, "<CENTER>Secures: " + c_Sign.Secures);

			AddButton(10, y += 25, 0xFB1, 0xFB3, "Cancel", Cancel);
			AddButton(width - 40, y, 0xFB7, 0xFB9, "Confirme", Confirm);

			AddBackgroundZero(-25, -25, width + 80, y + 80, 0x24F4);
		}

		private void Items()
		{
			c_Items = !c_Items;

			NewGump();
		}

		private void Cancel()
		{ }

		private void Confirm()
		{
			c_Sign.Purchase(Owner, c_Items);
		}
	}
}