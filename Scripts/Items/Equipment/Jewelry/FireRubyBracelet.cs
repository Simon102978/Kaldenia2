namespace Server.Items
{
    public class FireRubisBracelet : GoldBracelet
    {
        [Constructable]
        public FireRubisBracelet()
            : base()
        {
            Weight = 1.0;

            BaseRunicTool.ApplyAttributesTo(this, true, 0, Utility.RandomMinMax(1, 4), 0, 100);

            if (Utility.Random(100) < 10)
                Attributes.RegenHits += 2;
            else
                Resistances.Fire += 10;
        }

        public FireRubisBracelet(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber => 1073454;// fire Rubis bracelet
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