using System;
using System.Collections;

namespace Server.Items
{
    public abstract class BaseManaMaxBuffFood : BaseBuffFood
	{
        public BaseManaMaxBuffFood(BuffFoodEffect effect)
            : base(0x3C11, effect)
        {
        }

        public BaseManaMaxBuffFood(Serial serial)
            : base(serial)
        {
        }

        public abstract int ManaMaxOffset { get; }
        public abstract TimeSpan Duration { get; }
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

		private static Hashtable m_Timers = new Hashtable();
		private static Hashtable m_Table = new Hashtable();

		public bool DoEffect(Mobile from)
		{
			if (IsActive(from))
			{
				from.SendLocalizedMessage(502173); // You are already under a similar effect.
				return false;
			}

			from.SendMessage($"La nourriture réconfortante de sagesse prend son effet sur vous pendant {Duration.TotalSeconds} seconde{(Duration.TotalSeconds > 1 ? "s" : "")}.");

			m_Table[from] = ManaMaxOffset;

			Timer t = new InternalTimer(from, DateTime.Now + Duration);
			m_Timers[from] = t;
			t.Start();
			from.Delta(MobileDelta.Mana);
			return true;
		}

		public static bool IsActive(Mobile m)
		{
			return m_Timers.ContainsKey(m);
		}

		public static int GetValue(Mobile m)
		{
			return m_Table.ContainsKey(m) ? (int)m_Table[m] : 0;
		}

		public static void Deactivate(Mobile m)
		{
			if (m == null)
				return;

			var t = m_Timers[m] as Timer;
			var mod = m_Table[m] as int?;

			if (t != null && mod != null)
			{
				t.Stop();
				m_Timers.Remove(m);
				m_Table.Remove(m);
				m.Delta(MobileDelta.Mana);
				m.SendMessage("La nourriture réconfortante de sagesse prend fin.");
			}
		}

		public class InternalTimer : Timer
		{
			private Mobile m_Mobile;
			private DateTime m_EndTime;

			public InternalTimer(Mobile m, DateTime endTime) : base(TimeSpan.Zero, TimeSpan.FromSeconds(2))
			{
				m_Mobile = m;
				m_EndTime = endTime;

				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if (DateTime.Now >= m_EndTime && m_Timers.Contains(m_Mobile) || m_Mobile == null || m_Mobile.Deleted || !m_Mobile.Alive)
				{
					Deactivate(m_Mobile);
					Stop();
				}
			}
		}

		public override void Eat(Mobile from)
		{
			if (DoEffect(from))
			{
				PlayDrinkEffect(from);
				Consume();
			}
		}
	}
}
