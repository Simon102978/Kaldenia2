using Server.Engines.Craft;

namespace Server.Items
{
    public class MortarPestlePoisoning : BaseTool
    {
        [Constructable]
        public MortarPestlePoisoning()
            : base(0xE9B)
        {
			Name = "Mortier et pilon (poison)";
			Hue = 77;
            Weight = 1.0;
        }

        [Constructable]
        public MortarPestlePoisoning(int uses)
            : base(uses, 0xE9B)
        {
			Name = "Mortier et pilon (poison)";
			Hue = 77;
			Weight = 1.0;
		}

        public MortarPestlePoisoning(Serial serial)
            : base(serial)
        {
        }

        public override CraftSystem CraftSystem => DefPoisoning.CraftSystem;
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