﻿namespace Server.Items
{
    public class SangEnvouteFeu : Item, ICommodity
    {
        [Constructable]
        public SangEnvouteFeu()
            : this(1)
        {
        }

        [Constructable]
        public SangEnvouteFeu(int amount)
            : base(0x4077)
        {
            Stackable = true;
            Amount = amount;
			Name = "Sang Envouté Feu";
            Hue = 1797;
        }

        public SangEnvouteFeu(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;

        public override int LabelNumber => 1031702;// Medusa Blood
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
