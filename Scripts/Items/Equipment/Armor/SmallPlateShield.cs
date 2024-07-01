namespace Server.Items
{
    [Flipable(0x4202, 0x420A)]
    public class SmallPlateShield : BaseShield
    {
        [Constructable]
        public SmallPlateShield()
            : base(0x4202)
        {
            Weight = 6.0;
			Name = "Targe";
        }

        public SmallPlateShield(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance => 3;
        public override int BaseFireResistance => 2;
        public override int BaseColdResistance => 3;
        public override int BasePoisonResistance => 0;
        public override int BaseEnergyResistance => 1;
		public override int InitMinHits => 40;
		public override int InitMaxHits => 50;
		public override int StrReq => 20;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;


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
