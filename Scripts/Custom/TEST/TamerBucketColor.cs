using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class TamerColorBucket : Item
	{
		[Constructable]
		public TamerColorBucket() : base(0x2004)
		{
			Name = "Seau de Coloration pour créature";
			Weight = 1.0;
		}

		public TamerColorBucket(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
				return;
			}

			from.SendGump(new CustomHuePickerGump(from, CustomHuePicker.LeatherDyeTub, new CustomHuePickerCallback(OnColorPicked), this));
		}

		public void OnColorPicked(Mobile from, object state, int hue)
		{
			TamerColorBucket bucket = (TamerColorBucket)state;
			from.SendMessage("Choisissez la créature à teindre.");
			from.Target = new InternalTarget(bucket, hue);
		}

		private class InternalTarget : Target
		{
			private TamerColorBucket m_Bucket;
			private int m_Hue;

			public InternalTarget(TamerColorBucket bucket, int hue) : base(10, false, TargetFlags.None)
			{
				m_Bucket = bucket;
				m_Hue = hue;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is BaseCreature creature)
				{
					if (creature.Controlled && creature.ControlMaster == from)
					{
						creature.Hue = m_Hue;
						from.SendMessage("Vous avez coloré votre créature.");
						m_Bucket.Delete();
					}
					else
					{
						from.SendMessage("Vous ne pouvez colorer que vos propres créatures apprivoisées.");
					}
				}
				else
				{
					from.SendMessage("Vous ne pouvez colorer que des créatures apprivoisées.");
				}
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}
