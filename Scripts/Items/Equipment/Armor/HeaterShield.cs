using Server.Engines.Craft;

namespace Server.Items
{

    public class HeaterShield : BaseShield
    {
        [Constructable]
        public HeaterShield()
            : base(0x1B76)
        {
            Weight = 8.0;
			Name = "Muraille";
        }

        public HeaterShield(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance => 6;
        public override int BaseFireResistance => 1;
        public override int BaseColdResistance => 0;
        public override int BasePoisonResistance => 1;
        public override int BaseEnergyResistance => 1;
		public override int InitMinHits => 110;
		public override int InitMaxHits => 140;
		public override int StrReq => 35;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;
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