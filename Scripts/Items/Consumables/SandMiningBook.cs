using Server.Mobiles;

namespace Server.Items
{
    public class SandMiningBook : Item
    {
        public override int LabelNumber => 1153531;  // Find Glass-Quality Sand

        [Constructable]
        public SandMiningBook()
            : base(0xFBE)
        {
            Weight = 5.0;
			Name = "Connaissances Sable";
        }

        public SandMiningBook(Serial serial)
            : base(serial)
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
            PlayerMobile pm = from as PlayerMobile;

            if (pm == null)
            {
                return;
            }

            if (!IsChildOf(pm.Backpack))
            {
                pm.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else if (pm.Skills[SkillName.Mining].Base < 00.0)
           {
				pm.SendMessage("Vous devez avoir 50.0 dans votre skill de Mining"); // Only a Grandmaster Miner can learn from this book.
			}
            else if (pm.SandMining)
            {
                pm.SendLocalizedMessage(1080066); // You have already learned this information.
            }
            else
            {
                pm.SandMining = true;
                pm.SendLocalizedMessage(1111701); // You have learned how to mine fine sand.  Target sand areas when mining to look for fine sand.

                Delete();
            }
        }
    }
}