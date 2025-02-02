using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefBlacksmithy), typeof(SmallPlateShield))]
    public class Buckler : BaseShield
    {
        public override int BasePhysicalResistance => 2;
        public override int BaseFireResistance => 2;
        public override int BaseColdResistance => 2;
        public override int BasePoisonResistance => 2;
        public override int BaseEnergyResistance => 2;
        public override int InitMinHits => 40;
        public override int InitMaxHits => 50;
        public override int StrReq => 20;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Cloth;

		[Constructable]
        public Buckler()
            : base(0x1B73)
        {
            Weight = 5.0;
			Name = "Bouclier";
        }

        public Buckler(Serial serial)
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