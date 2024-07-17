using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class SsinsEar : BaseReagent
    {
        [Constructable]
        public SsinsEar() : this(1)
        {
        }

        [Constructable]
        public SsinsEar(int amount) : base(0xF78, amount)
        {
            Hue = 2833;
            Name = "Oreille de Ssins";
			Stackable = true;
			Weight = 1;
			
		}

        public SsinsEar(Serial serial) : base(serial)
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