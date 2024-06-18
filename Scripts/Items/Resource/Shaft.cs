using Server.Engines.Craft;
using System;

namespace Server.Items
{
    public class Shaft : Item, ICommodity
    {
        [Constructable]
        public Shaft()
            : this(5)
        {
        }

        [Constructable]
        public Shaft(int amount)
            : base(0x1BD4)
        {
            Stackable = true;
            Amount = 5;
        }

        public Shaft(Serial serial)
            : base(serial)
        {
        }

        public override double DefaultWeight => 0.1;
        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;
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

		public virtual int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
		{
			Amount = Amount * 5;

			return quality;
		}
	}
}