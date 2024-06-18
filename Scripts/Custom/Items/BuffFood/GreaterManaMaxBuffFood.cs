using System;

namespace Server.Items
{
    public class GreaterManaMaxBuffFood : BaseManaMaxBuffFood
    {
        [Constructable]
        public GreaterManaMaxBuffFood() : base(BuffFoodEffect.ManaMaxGreater)
        {
			Name = "Plat réconfortant de sagesse majeure";
		}

		public GreaterManaMaxBuffFood(Serial serial) : base(serial)
        {
        }

        public override int ManaMaxOffset => 15;
		public override TimeSpan Duration => TimeSpan.FromMinutes(5.0);
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