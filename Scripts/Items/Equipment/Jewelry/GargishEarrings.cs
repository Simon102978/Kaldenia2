namespace Server.Items
{
    public class Earrings : BaseArmor
    {
        public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;
        public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;

        public override int BasePhysicalResistance => 0;
        public override int BaseFireResistance => 0;
        public override int BaseColdResistance => 0;
        public override int BasePoisonResistance => 0;
        public override int BaseEnergyResistance => 0;

        public override int InitMinHits => 30;
        public override int InitMaxHits => 40;

        [Constructable]
        public Earrings()
            : base(0x4213)
        {
            Layer = Layer.Earrings;
        }

     //   public override int GetDurabilityBonus()
     //   {
     //       int bonus = Quality == ItemQuality.Exceptional ? 20 : 0;

       //     return bonus + ArmorAttributes.DurabilityBonus;
      //  }

   //     protected override void ApplyResourceResistances(CraftResource oldResource)
    //    {
     //   }

        public Earrings(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
