using Server.Engines.Craft;

namespace Server.Items
{
 
    public class PlateDo : BaseArmor
    {
        [Constructable]
        public PlateDo()
            : base(0x277D)
        {
            Weight = 10.0;
        }

        public PlateDo(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance => 6;
        public override int BaseFireResistance => 3;
        public override int BaseColdResistance => 2;
        public override int BasePoisonResistance => 3;
        public override int BaseEnergyResistance => 2;
        public override int InitMinHits => 60;
        public override int InitMaxHits => 70;
        public override int StrReq => 65;
        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
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
