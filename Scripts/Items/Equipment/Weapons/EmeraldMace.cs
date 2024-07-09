namespace Server.Items
{
    public class EmeraudeMace : DiamantMace
    {
        public override int LabelNumber => 1073530; // Emeraude mace

        [Constructable]
        public EmeraudeMace()
        {
            WeaponAttributes.ResistPoisonBonus = 5;
        }

        public EmeraudeMace(Serial serial)
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