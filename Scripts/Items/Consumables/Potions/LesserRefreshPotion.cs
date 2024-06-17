namespace Server.Items
{
    public class LesserRefreshPotion : BaseRefreshPotion
    {
        [Constructable]
        public LesserRefreshPotion() : base(PotionEffect.RefreshLesser)
        {
			Name = "Potion de rafraichissement mineure";
        }

        public LesserRefreshPotion(Serial serial) : base(serial)
        {
        }

        public override double Refresh => 0.25;
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