using Server.Engines.Craft;

namespace Server.Items
{

    public class OrderShield : BaseShield
    {
        [Constructable]
        public OrderShield()
            : base(0x1BC4)
        {
            Weight = 7.0;
        }

        public OrderShield(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance => 6;
        public override int BaseFireResistance => 0;
        public override int BaseColdResistance => 0;
        public override int BasePoisonResistance => 0;
        public override int BaseEnergyResistance => 0;
        public override int InitMinHits => 100;
        public override int InitMaxHits => 125;
        public override int StrReq => 65;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;

		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);//version
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
