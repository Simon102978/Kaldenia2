namespace Server.Items
{
    [Flipable(0x144f, 0x1454)]
    public class BoneChest : BaseArmor
    {
        public override int BasePhysicalResistance => 3;
        public override int BaseFireResistance => 3;
        public override int BaseColdResistance => 4;
        public override int BasePoisonResistance => 2;
        public override int BaseEnergyResistance => 4;
        public override int InitMinHits => 25;
        public override int InitMaxHits => 30;
        public override int StrReq => 50;
        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;
		public override CraftResource DefaultResource => CraftResource.RegularBone;

		public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.Half;

		[Constructable]
        public BoneChest()
            : base(0x144F)
        {
            Weight = 6.0;
			Name = "Plastron Os";
		}

        public BoneChest(Serial serial)
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
