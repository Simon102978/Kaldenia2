using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class FluideAstral : BaseReagent
    {
        [Constructable]
        public FluideAstral() : this(1)
        {
        }

        [Constructable]
        public FluideAstral(int amount) : base(0xF8D, amount)
        {
            Hue = 1160;
            Name = "Fluide Astral";
			Stackable = true;
        }

        public FluideAstral(Serial serial) : base(serial)
        {
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