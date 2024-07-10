#region References
using Server.Commands;
using Server.Items;
using Server.Network;
using System;
#endregion

namespace Server
{
	public enum TimeOfDay
	{
		Night,
		ScaleToDay,
		Day,
		ScaleToNight
	}

	public static class LightCycle
    {
        public const int DayLevel = 0;
        public const int NightLevel = 26; //12
        public const int DungeonLevel = 26; //26
        public const int JailLevel = 0; //9

        private static int _LevelOverride = int.MinValue;

        public static int LevelOverride
        {
            get { return _LevelOverride; }
            set
            {
                _LevelOverride = value;

				for (int i = 0; i < NetState.Instances.Count; ++i)
				{
					NetState ns = (NetState)NetState.Instances[i];
					Mobile m = ns.Mobile;

					if (m != null)
						m.CheckLightLevels(false);
				}
			}
		}

		public static void Initialize()
        {
            new LightCycleTimer(Clock.SecondsPerUOMinute).Start();

            EventSink.Login += OnLogin;

            CommandSystem.Register("GlobalLight", AccessLevel.GameMaster, Light_OnCommand);
        }

        public static void OnLogin(LoginEventArgs args)
        {
            Mobile m = args.Mobile;

            if (m != null)
                m.CheckLightLevels(true);
        }

		public static TimeOfDay GetTimeofDay()
		{
			
				int hours, minutes;
				Map map = Map.Felucca; // Utilisez la carte appropriée
				int x = 1000, y = 1000; // Utilisez des coordonnées représentatives

				Server.Items.Clock.GetTime(map, x, y, out hours, out minutes);

				switch (Map.Felucca.Season)
			{
				case 0: // Printemps
					{
						if (hours < 5)
							return TimeOfDay.Night;

						if (hours < 8)
							return TimeOfDay.ScaleToDay;

						if (hours < 19)
							return TimeOfDay.Day;

						if (hours < 23)
							return TimeOfDay.ScaleToNight;

						if (hours < 24)
							return TimeOfDay.Night;

						break;
					}
				case 1: // Été
					{
						if (hours < 5)
							return TimeOfDay.Night;

						if (hours < 8)
							return TimeOfDay.ScaleToDay;

						if (hours < 20)
							return TimeOfDay.Day;

						if (hours < 24)
							return TimeOfDay.ScaleToNight;

						break;
					}
				case 2: // Automne
					{
						if (hours < 5)
							return TimeOfDay.Night;

						if (hours < 8)
							return TimeOfDay.ScaleToDay;

						if (hours < 19)
							return TimeOfDay.Day;

						if (hours < 23)
							return TimeOfDay.ScaleToNight;

						if (hours < 24)
							return TimeOfDay.Night;

						break;
					}
				case 3: // Hiver
					{
						if (hours < 6)
							return TimeOfDay.Night;

						if (hours < 9)
							return TimeOfDay.ScaleToDay;

						if (hours < 18)
							return TimeOfDay.Day;

						if (hours < 22)
							return TimeOfDay.ScaleToNight;

						if (hours < 24)
							return TimeOfDay.Night;

						break;
					}
				case 4: // Abyss
					{
						if (hours < 7)
							return TimeOfDay.Night;

						if (hours < 10)
							return TimeOfDay.ScaleToDay;

						if (hours < 17)
							return TimeOfDay.Day;

						if (hours < 20)
							return TimeOfDay.ScaleToNight;

						if (hours < 24)
							return TimeOfDay.Night;

						break;
					}
			}

			return TimeOfDay.Night; // should never be
		}

		public static int ComputeLevelFor(Mobile from)
        {
            if (_LevelOverride > int.MinValue)
                return _LevelOverride;

            int hours, minutes;

			int level = NightLevel;


			Clock.GetTime(from.Map, from.X, from.Y, out hours, out minutes);

			/* OSI times:
            * 
            * Midnight ->  3:59 AM : Night
            *  4:00 AM -> 11:59 PM : Day
            * 
            * RunUO times:
            * 
            * 10:00 PM -> 11:59 PM : Scale to night
            * Midnight ->  3:59 AM : Night
            *  4:00 AM ->  5:59 AM : Scale to day
            *  6:00 AM ->  9:59 PM : Day
            */

			int season = Map.Felucca.Season;

			if (season == 0) // Printemps
			{
				if (hours < 5)
					level = NightLevel;

				else if (hours < 8)
					level = NightLevel + (((((hours - 5) * 60) + minutes) * (DayLevel - NightLevel)) / 180);

				else if (hours < 19)
					level = DayLevel;

				else if (hours < 23)
					level = DayLevel + (((((hours - 19) * 60) + minutes) * (NightLevel - DayLevel)) / 240);

				else if (hours < 24)
					level = NightLevel;
			}
			else if (season == 1) // Été
			{
				if (hours < 5)
					level = NightLevel;

				else if (hours < 8)
					level = NightLevel + (((((hours - 5) * 60) + minutes) * (DayLevel - NightLevel)) / 180);

				else if (hours < 20)
					level = DayLevel;

				else if (hours < 24)
					level = DayLevel + (((((hours - 20) * 60) + minutes) * (NightLevel - DayLevel)) / 240);
			}
			else if (season == 2) // Automne
			{
				if (hours < 5)
					level = NightLevel;

				else if (hours < 8)
					level = NightLevel + (((((hours - 5) * 60) + minutes) * (DayLevel - NightLevel)) / 180);

				else if (hours < 19)
					level = DayLevel;

				else if (hours < 23)
					level = DayLevel + (((((hours - 19) * 60) + minutes) * (NightLevel - DayLevel)) / 240);

				else if (hours < 24)
					level = NightLevel;
			}
			else if (season == 3) // Hiver
			{
				if (hours < 6)
					level = NightLevel;

				else if (hours < 9)
					level = NightLevel + (((((hours - 6) * 60) + minutes) * (DayLevel - NightLevel)) / 180);

				else if (hours < 18)
					level = DayLevel;

				else if (hours < 22)
					level = DayLevel + (((((hours - 18) * 60) + minutes) * (NightLevel - DayLevel)) / 240);

				else if (hours < 24)
					level = NightLevel;
			}
			else if (season == 4) // Abyss
			{
				if (hours < 7)
					level = NightLevel;

				else if (hours < 10)
					level = NightLevel + (((((hours - 7) * 60) + minutes) * (DayLevel - NightLevel)) / 180);

				else if (hours < 17)
					level = DayLevel;

				else if (hours < 20)
					level = DayLevel + (((((hours - 17) * 60) + minutes) * (NightLevel - DayLevel)) / 180);

				else if (hours < 24)
					level = NightLevel;
			}

			return level;
		}

		public static void CheckLightLevels()
        {
            int i = NetState.Instances.Count;

            while (--i >= 0)
            {
                if (i >= NetState.Instances.Count)
                    continue;

                NetState ns = NetState.Instances[i];

                if (ns == null)
                    continue;

                Mobile m = ns.Mobile;

                if (m != null)
                    m.CheckLightLevels(false);
            }
        }

        [Usage("GlobalLight <value>")]
        [Description("Sets the current global light level.")]
        private static void Light_OnCommand(CommandEventArgs e)
        {
            if (e.Length >= 1)
            {
                LevelOverride = e.GetInt32(0);
                e.Mobile.SendMessage("Global light level override has been changed to {0}.", _LevelOverride);
            }
            else
            {
                LevelOverride = int.MinValue;
                e.Mobile.SendMessage("Global light level override has been cleared.");
            }
        }

        public class NightSightTimer : Timer
        {
            private readonly Mobile m_Owner;

            public NightSightTimer(Mobile owner)
                : base(TimeSpan.FromMinutes(Utility.Random(15, 25)))
            {
                m_Owner = owner;

                Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                m_Owner.EndAction(typeof(LightCycle));
                m_Owner.LightLevel = 0;

                BuffInfo.RemoveBuff(m_Owner, BuffIcon.NightSight);
            }
        }

        private class LightCycleTimer : Timer
        {
            public LightCycleTimer(double interval)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(interval))
            {
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				for (int i = 0; i < NetState.Instances.Count; ++i)
				{
					NetState ns = (NetState)NetState.Instances[i];
					Mobile m = ns.Mobile;

					if (m != null)
						m.CheckLightLevels(false);
				}
			}
		}
	}
}