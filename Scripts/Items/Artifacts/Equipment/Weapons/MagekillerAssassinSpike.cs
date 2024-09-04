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

			SetSkillBonuses.SetValues(0, SkillName.Hiding, 30);

			SetSelfRepair = 3;

			SetAttributes.BonusDex = 12;

			SetPhysicalBonus = 5;
			SetFireBonus = 4;
			SetColdBonus = 3;
			SetPoisonBonus = 4;
			SetEnergyBonus = 4;

		}

		public SsinsDagger(Serial serial)
            : base(serial)
        {
        }

        public override void ResetWeapon()
        {
            WeaponAttributes.HitLeechMana = 16;
			Name = "Dague du roi Ssins";
			Hue = 2833;
			SetHue = 2833;

			SetSkillBonuses.SetValues(0, SkillName.Hiding, 30);

			SetSelfRepair = 3;

			SetAttributes.BonusDex = 12;

			SetPhysicalBonus = 5;
			SetFireBonus = 4;
			SetColdBonus = 3;
			SetPoisonBonus = 4;
			SetEnergyBonus = 4;
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