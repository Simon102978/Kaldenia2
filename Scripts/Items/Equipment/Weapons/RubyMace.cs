namespace Server.Items
{
    public class RubisMace : DiamantMace
    {
        public override int LabelNumber => 1073529; // Rubis mace

        [Constructable]
        public RubisMace()
        {
            Attributes.WeaponDamage = 5;
        }

        public RubisMace(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();
        }
    }
}