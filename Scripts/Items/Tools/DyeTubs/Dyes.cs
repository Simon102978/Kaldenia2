using Server.HuePickers;
using Server.Targeting;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	public class Dyes : Item
	{
		[Constructable]
		public Dyes() : base(0xFA9)
		{
			Weight = 3.0;
		}

		public Dyes(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		public override void OnDoubleClick(Mobile from)
		{
			from.SendLocalizedMessage(500856); // Select the dye tub to use the dyes on.
			from.Target = new InternalTarget();
		}

		private class InternalTarget : Target
		{
			public InternalTarget() : base(1, false, TargetFlags.None)
			{
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is DyeTub tub)
				{
					if (tub.Redyable)
					{
						if (tub.CustomHuePicker == null)
						{
							if (from is CustomPlayerMobile customFrom)
							{
								from.SendGump(new CustomDyeGump(customFrom, tub, tub.DyedHue, ApplyDye));
							}
							else
							{
								from.SendHuePicker(new InternalPicker(tub));
							}
						}
						else
						{
							from.SendGump(new CustomHuePickerGump(from, tub.CustomHuePicker, ApplyDye, tub));
						}
					}
					else if (tub is BlackDyeTub)
					{
						from.SendLocalizedMessage(1010092); // You can not use this on a black dye tub.
					}
					else
					{
						from.SendMessage("That dye tub may not be redyed.");
					}
				}
				else
				{
					from.SendLocalizedMessage(500857); // Use this on a dye tub.
				}
			}

			private static void ApplyDye(Mobile from, object state, int hue)
			{
				if (state is DyeTub tub)
				{
					tub.DyedHue = hue;
					tub.Hue = hue;
					from.SendLocalizedMessage(1042418, "#" + hue.ToString()); // You dye the tub ~1_COLOR~.
				}
			}

			private class InternalPicker : HuePicker
			{
				private readonly DyeTub m_Tub;
				private readonly Mobile m_From;

				public InternalPicker(DyeTub tub) : base(tub.ItemID)
				{
					m_Tub = tub;
				}

				public override void OnResponse(int hue)
				{
					m_Tub.DyedHue = hue;
					m_Tub.Hue = hue;
					m_From.SendLocalizedMessage(1042418, "#" + hue.ToString()); // You dye the tub ~1_COLOR~.			
				}
				}
		}
	}
}
