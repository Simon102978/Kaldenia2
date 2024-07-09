namespace Server.Items
{
    public class SsinsDagger : AssassinSpike
    {
        public override bool IsArtifact => true;
        [Constructable]
        public SsinsDagger()
        {
            WeaponAttributes.HitLeechMana = 16;
			Name = "Dague du roi Ssins";
			Hue = 2833;
			SetHue = 2833;

		}

		public SsinsDagger(Serial serial)
            : base(serial)
        {
        }
		public override SetItem SetID => SetItem.Assassin;
		public override int Pieces => 5;
		public override int LabelNumber => 1073519;// magekiller assassin spike
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