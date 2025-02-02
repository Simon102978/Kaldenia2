using Server.Engines.Craft;

namespace Server.Items
{
   
    public class ChaosShield : BaseShield
    {


        [Constructable]
        public ChaosShield()
            : base(0x1BC3)
        {
            Weight = 5.0;
			Name = "Targe d�cor�e";

		}

        public ChaosShield(Serial serial)
            : base(serial)
        {
        }
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;

		public override int BasePhysicalResistance => 5;
		public override int BaseFireResistance => 1;
		public override int BaseColdResistance => 1;
		public override int BasePoisonResistance => 0;
		public override int BaseEnergyResistance => 0;
		public override int InitMinHits => 100;
		public override int InitMaxHits => 125;
		public override int StrReq => 35;
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
