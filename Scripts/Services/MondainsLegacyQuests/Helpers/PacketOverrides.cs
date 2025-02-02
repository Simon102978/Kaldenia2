﻿using JournalCommand;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System;

namespace Server.Engines.Quests
{
    public sealed class HeritagePacket : Packet
    {
        public static readonly Packet Close = SetStatic(new HeritagePacket(false, 0xFF));
        public HeritagePacket(bool female, short type)
            : base(0xBF)
        {
            EnsureCapacity(7);

            m_Stream.Write((short)0x2A);
            m_Stream.Write((byte)(female ? 1 : 0));
            m_Stream.Write((byte)type);
        }
    }

    public class PacketOverrides
    {
        public static void Initialize()
        {
            Timer.DelayCall(TimeSpan.Zero, Override);
        }

        public static void Override()
        {
            PacketHandlers.RegisterEncoded(0x32, true, QuestButton);
            PacketHandlers.RegisterExtended(0x2A, true, HeritageTransform);
        }

        public static void QuestButton(NetState state, IEntity e, EncodedReader reader)
		{
			if (state.Mobile is CustomPlayerMobile pm)
				CJournalGump.OpenJournal(pm);
		}

		public static void HeritageTransform(NetState state, PacketReader reader)
        {
            Mobile m = state.Mobile;

            if (reader.Size == 5)
            {
                m.SendLocalizedMessage(1073645); // You may try this again later...	
            }
            else if (reader.Size == 15 && HeritageQuester.Check(m))
            {
                bool proceed = false;

                if (HeritageQuester.IsPending(m))
                {
                    proceed = true;

                    HeritageQuester quester = HeritageQuester.Pending(m);
                    m.Race = quester.Race;

                    quester.CheckCompleted(m, true); // removes done quests

                    m.SendLocalizedMessage(1073654); // You are now fully human.
                }
                else if (RaceChangeToken.IsPending(m))
                {
                    Race race = RaceChangeToken.GetPendingRace(m);

                    if (race != null)
                    {
                        m.Race = race;

                        proceed = true;
                        m.SendLocalizedMessage(1111914); // You have successfully changed your race.

                        RaceChangeToken.OnRaceChange(m);
                    }
                }

                if (proceed)
                {
                    m.Hue = reader.ReadUInt16();
                    m.HairItemID = reader.ReadUInt16();
                    m.HairHue = reader.ReadUInt16();
                    m.FacialHairItemID = reader.ReadUInt16();
                    m.FacialHairHue = reader.ReadUInt16();
                }
            }

            HeritageQuester.RemovePending(m);
            RaceChangeToken.RemovePending(m);
        }
    }
}
