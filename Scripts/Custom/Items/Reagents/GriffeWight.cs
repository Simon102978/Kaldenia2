using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class GriffeWight : BaseReagent
    {
        [Constructable]
        public GriffeWight() : this(1)
        {
        }

        [Constructable]
        public GriffeWight(int amount) : base(0xF85, amount)
        {
            Hue = 1926;
            Name = "Griffe de Wight";
			Stackable = true;
        }

        public GriffeWight(Serial serial) : base(serial)
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