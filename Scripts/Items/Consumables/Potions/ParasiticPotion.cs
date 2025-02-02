namespace Server.Items
{
    public class ParasiticPotion : BasePoisonPotion
    {
        [Constructable]
        public ParasiticPotion()
            : base(PotionEffect.Parasitic)
        {
			Name = "Potion de poison parasitique";
			Hue = 0x17C;
        }

        public ParasiticPotion(Serial serial)
            : base(serial)
        {
        }

        public override Poison Poison => Poison.Parasitic;/* public override Poison Poison{ get{ return Poison.Darkglow; } }  MUST be restored when prerequisites are done */
        public override double MinPoisoningSkill => 75.0;
        public override double MaxPoisoningSkill => 100.0;
        public override int LabelNumber => 1072848;// Parasitic Poison
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