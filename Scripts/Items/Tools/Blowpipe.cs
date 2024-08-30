using System.Diagnostics.Tracing;
using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable(0xE8A, 0xE89)]
    public class Blowpipe : BaseTool
    {
        public override CraftSystem CraftSystem => DefGlassblowing.CraftSystem;
        public override int LabelNumber => 1044609;  // Blow Pipe

        [Constructable]
        public Blowpipe()
            : base(0xE8A)
        {
			Name = "Tube à souffler le verre";

		}

		[Constructable]
        public Blowpipe(int uses)
            : base(uses, 0xE8A)
        {
            Weight = 1.0;
            Hue = 0x3B9;
			Name = "Tube à souffler le verre";
			Layer = Layer.TwoHanded;
			
        }

        public Blowpipe(Serial serial)
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
    }
}