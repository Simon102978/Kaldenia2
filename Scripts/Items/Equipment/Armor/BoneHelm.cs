namespace Server.Items
{
    [Flipable(0x1451, 0x1456)]
    public class BoneHelm : BaseArmor
    {
        public override int BasePhysicalResistance => 3;
        public override int BaseFireResistance => 3;
        public override int BaseColdResistance => 4;
        public override int BasePoisonResistance => 2;
        public override int BaseEnergyResistance => 4;
        public override int InitMinHits => 25;
        public override int InitMaxHits => 30;
        public override int StrReq => 20;
        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;
		public override CraftResource DefaultResource => CraftResource.RegularBone;
		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.Half;

		[Constructable]
        public BoneHelm()
            : base(0x1451)
        {
            Weight = 3.0;
			Name = "Casque Os";
		}

        public BoneHelm(Serial serial)
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
