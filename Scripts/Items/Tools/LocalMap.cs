using Server.Engines.Craft;

namespace Server.Items
{
    public class LocalMap : MapItem
    {
        [Constructable]
        public LocalMap()
        {
			SetDisplay(256, 104, 1900, 1740, 400, 400);
		}

        public LocalMap(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1015230;// local map
        public override void CraftInit(Mobile from, CraftItem craftitem)
        {
            double skillValue = from.Skills[SkillName.Cartography].Value;
            int dist = 64 + (int)(skillValue * 2);

            SetDisplay(from.X - dist, from.Y - dist, from.X + dist, from.Y + dist, 200, 200);
        }

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
}