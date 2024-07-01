using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefBlacksmithy), typeof(SmallPlateShield))]
    public class BronzeShield : BaseShield
    {
		public override int BasePhysicalResistance => 4;
		public override int BaseFireResistance => 0;
		public override int BaseColdResistance => 1;
		public override int BasePoisonResistance => 0;
		public override int BaseEnergyResistance => 1;
		public override int InitMinHits => 50;
		public override int InitMaxHits => 65;
		public override int StrReq => 20;
		public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;

		[Constructable]
        public BronzeShield()
            : base(0x1B72)
        {
            Weight = 6.0;
			Name = "Rondache résonnante";

		}

        public BronzeShield(Serial serial)
            : base(serial)
        {
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