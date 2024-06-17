using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class PoussiereNecrotique : BaseReagent
    {
        [Constructable]
        public PoussiereNecrotique() : this(1)
        {
        }

        [Constructable]
        public PoussiereNecrotique(int amount) : base(0xF8F, amount)
        {
            Hue = 1668;
            Name = "Poussière Nécrotique";
			Stackable = true;
        }

        public PoussiereNecrotique(Serial serial) : base(serial)
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