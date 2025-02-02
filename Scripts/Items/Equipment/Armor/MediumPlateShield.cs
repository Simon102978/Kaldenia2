namespace Server.Items
{
    [Flipable(0x4203, 0x4209)]
    public class MediumPlateShield : BaseShield
    {
        [Constructable]
        public MediumPlateShield()
            : base(0x4203)
        {
            Weight = 6.0;
			Name = "Rondache";
        }

        public MediumPlateShield(Serial serial)
            : base(serial)
        {
        }
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
		public override int BasePhysicalResistance => 4;
		public override int BaseFireResistance => 0;
		public override int BaseColdResistance => 4;
		public override int BasePoisonResistance => 0;
		public override int BaseEnergyResistance => 1;
		public override int InitMinHits => 50;
		public override int InitMaxHits => 65;
		public override int StrReq => 20;

		public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);//version
        }
    }
}
