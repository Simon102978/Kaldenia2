using Server.Engines.Craft;

namespace Server.Items
{

    [Flipable(0x1415, 0x1416)]
    public class PlateChest : BaseArmor
    {
        [Constructable]
        public PlateChest()
            : base(0x1415)
        {
            Weight = 10.0;
			Name = "Plastron Plaque";
		}

        public PlateChest(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance => 6;
        public override int BaseFireResistance => 3;
        public override int BaseColdResistance => 2;
        public override int BasePoisonResistance => 3;
        public override int BaseEnergyResistance => 2;
        public override int InitMinHits => 50;
        public override int InitMaxHits => 65;
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
