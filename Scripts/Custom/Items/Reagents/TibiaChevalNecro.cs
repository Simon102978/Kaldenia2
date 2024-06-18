using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class TibiaChevalNecro : BaseReagent
    {
        [Constructable]
        public TibiaChevalNecro() : this(1)
        {
        }

        [Constructable]
        public TibiaChevalNecro(int amount) : base(0xF80, amount)
        {
            Hue = 1913;
            Name = "Tibia de cheval nécrosé";
			Stackable = true;
        }

        public TibiaChevalNecro(Serial serial) : base(serial)
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