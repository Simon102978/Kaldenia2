namespace Server.Items
{
    public class PerfectEmeraudeRing : GoldRing
    {
        [Constructable]
        public PerfectEmeraudeRing()
            : base()
        {
            Weight = 1.0;

            BaseRunicTool.ApplyAttributesTo(this, true, 0, Utility.RandomMinMax(2, 4), 0, 100);

            if (Utility.RandomBool())
                Resistances.Poison += 10;
            else
                Attributes.SpellDamage += 5;
        }

        public PerfectEmeraudeRing(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1073459;// perfect Emeraude ring
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