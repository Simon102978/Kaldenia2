using Server.Engines.Craft;

namespace Server.Items
{
  
    [Flipable(0x1411, 0x141a)]
    public class PlateLegs : BaseArmor
    {
        [Constructable]
        public PlateLegs()
            : base(0x1411)
        {
            Weight = 7.0;
			Name = "Jambi�res Plaque";
		}

        public PlateLegs(Serial serial)
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
