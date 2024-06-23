using Server.Engines.Craft;

namespace Server.Items
{
    public class SewingKit : BaseTool
    {
        [Constructable]
        public SewingKit()
            : base(0xF9D)
        {
            Weight = 2.0;
			Name = "Kit de couture";
		}

        [Constructable]
        public SewingKit(int uses)
            : base(uses, 0xF9D)
        {
            Weight = 2.0;
			Name = "Kit de couture";

		}

        public SewingKit(Serial serial)
            : base(serial)
        {
        }

        public override CraftSystem CraftSystem => DefTailoring.CraftSystem;
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
    }
}