using Server.Engines.Craft;

namespace Server.Items
{
    public class Earrings : BaseArmor, IRepairable
	{
		public CraftSystem RepairSystem => DefTinkering.CraftSystem;

		public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
        public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.All;

        public override int BasePhysicalResistance => Utility.RandomMinMax(0, 3);
        public override int BaseFireResistance => Utility.RandomMinMax(0, 3);
		public override int BaseColdResistance => Utility.RandomMinMax(0, 3);
		public override int BasePoisonResistance => Utility.RandomMinMax(0, 3);
		public override int BaseEnergyResistance => Utility.RandomMinMax(0, 3);

		public override int InitMinHits => 20;
        public override int InitMaxHits => 50;

        [Constructable]
        public Earrings()
            : base(0x4213)
        {
			Name = "Boucles d'oreilles pendantes";
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
