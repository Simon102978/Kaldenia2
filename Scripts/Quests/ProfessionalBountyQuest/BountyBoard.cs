using Server.Engines.Quests;
using Server.Engines.VendorSearching;
using Server.Gumps;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server.Items
{
    public class ProfessionalBountyPalmierBoard : Item
    {
        [Constructable]
        public ProfessionalBountyPalmierBoard() : base(7774)
        {
            Movable = false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.HasGump(typeof(BountyPalmierBoardGump)))
                BaseGump.SendGump(new BountyPalmierBoardGump(from));
        }

        public ProfessionalBountyPalmierBoard(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class BountyPalmierBoardGump : BaseGump
    {
        public int Index { get; set; }

        private readonly int darkHue = 19686;
        private readonly int lightHue = 19884;

        public BountyPalmierBoardGump(Mobile from, int index = 0)
            : base(from as PlayerMobile, 20, 20)
        {
            Index = index;
        }

        public override void AddGumpLayout()
        {
            AddAlphaRegion(50, 50, 50, 50);
            AddImage(0, 0, 5400);

            AddHtmlLocalized(150, 37, 190, 16, CenterLoc, "#1116689", darkHue, false, false);   // WANTED FOR PIRACY
            AddHtmlLocalized(150, 320, 190, 16, CenterLoc, "#1116703", darkHue, false, false);  // WANTED DEAD OR ALIVE

            AddHtmlLocalized(180, 135, 200, 16, 1116704, lightHue, false, false); //Notice to all sailors
            AddHtmlLocalized(130, 150, 300, 16, 1116705, lightHue, false, false); //There be a bounty on these lowlifes!
            AddHtmlLocalized(150, 170, 300, 16, 1116706, lightHue, false, false); //See officers fer information.
            AddHtmlLocalized(195, 190, 300, 16, 1116707, lightHue, false, false); //********

            if (Index < 0)
                Index = 0;
            if (Index >= BountyQuestSpawner.Bounties.Count)
                Index = BountyQuestSpawner.Bounties.Count - 1;

            List<Mobile> mobs = new List<Mobile>(BountyQuestSpawner.Bounties.Keys);

            if (mobs.Count == 0)
                return;

            int y = 210;
            int idx = 0;

            for (int i = Index; i < mobs.Count; i++)
            {
                if (idx++ > 4)
                    break;

                Mobile mob = mobs[i];
                int toReward = 1000;

                BountyQuestSpawner.Bounties.TryGetValue(mob, out toReward);
                PirateCaptain capt = mob as PirateCaptain;

                if (capt == null)
                    continue;

                string args;

                if (User.NetState != null && User.NetState.IsEnhancedClient && VendorSearch.StringList != null)
                {
                    StringList strList = VendorSearch.StringList;

                    args = string.Format("{0} {1} {2}", strList.GetString(capt.Adjective), strList.GetString(capt.Noun), capt.PirateName > 0 ? strList.GetString(capt.PirateName) : capt.Name);

                    AddHtml(110, y, 400, 16, Color(C16232(lightHue), args), false, false);
                }
                else
                {
                    if (capt.PirateName > 0)
                        args = string.Format("#{0}\t#{1}\t#{2}", capt.Adjective, capt.Noun, capt.PirateName);
                    else
                        args = string.Format("#{0}\t#{1}\t{2}", capt.Adjective, capt.Noun, capt.Name);

                    AddHtmlLocalized(110, y, 400, 16, 1116690 + (idx - 1), args, lightHue, false, false); // ~1_val~ ~2_val~ ~3_val~
                }

                AddHtmlLocalized(280, y, 125, 16, 1116696 + (idx - 1), toReward.ToString(), lightHue, false, false); // Reward: ~1_val~

                y += 16;
            }

            AddButton(362, 115, 2084, 2084, 1 + Index, GumpButtonType.Reply, 0);
            AddButton(362, 342, 2085, 2085, 500 + Index, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(RelayInfo info)
        {
            if (info.ButtonID == 0)
                return;

            if (info.ButtonID < 500)
            {
                Index--;
                Refresh();
            }
            else
            {
                Index++;
                Refresh();
            }
        }
    }
}
