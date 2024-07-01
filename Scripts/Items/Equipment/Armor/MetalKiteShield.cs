using Server.Engines.Craft;

namespace Server.Items
{

    public class MetalKiteShield : BaseShield, IDyable
    {
        [Constructable]
        public MetalKiteShield()
            : base(0x1B74)
        {
            Weight = 7.0;
			Name = "Blason";
        }

        public MetalKiteShield(Serial serial)
            : base(serial)
        {
        }

		public override int BasePhysicalResistance => 5;
		public override int BaseFireResistance => 1;
		public override int BaseColdResistance => 1;
		public override int BasePoisonResistance => 0;
		public override int BaseEnergyResistance => 0;
		public override int InitMinHits => 100;
		public override int InitMaxHits => 125;
		public override int StrReq => 35;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;

		public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
                return false;

            Hue = sender.DyedHue;

            return true;
        }

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