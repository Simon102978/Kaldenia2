using System;

namespace Server.Items
{
    public class GemologistsSatchel : Bag
    {
        public override int LabelNumber => 1113378;  // Gemologist's Satchel

        [Constructable]
        public GemologistsSatchel()
        {
            Hue = 1177;

            DropItem(new Ambre(Utility.RandomMinMax(10, 25)));
            DropItem(new Citrine(Utility.RandomMinMax(10, 25)));
            DropItem(new Rubis(Utility.RandomMinMax(10, 25)));
            DropItem(new Tourmaline(Utility.RandomMinMax(10, 25)));
            DropItem(new Amethyste(Utility.RandomMinMax(10, 25)));
            DropItem(new Emeraude(Utility.RandomMinMax(10, 25)));
            DropItem(new Sapphire(Utility.RandomMinMax(10, 25)));
            DropItem(new SaphirEtoile(Utility.RandomMinMax(10, 25)));
            DropItem(new Diamant(Utility.RandomMinMax(10, 25)));

            for (int i = 0; i < 5; i++)
            {
                Type type = SkillHandlers.Imbuing.IngredTypes[Utility.Random(SkillHandlers.Imbuing.IngredTypes.Length)];

                if (type != null)
                {
                    Item item = Loot.Construct(type);

                    if (item != null)
                    {
                        item.Amount = Utility.RandomMinMax(5, 12);
                        DropItem(item);
                    }
                }
            }
        }

        public GemologistsSatchel(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
