using System;

namespace Server.Items
{
    public class StamMaxBuffFood : BaseStamMaxBuffFood
    {
        [Constructable]
        public StamMaxBuffFood() : base(BuffFoodEffect.StamMax)
        {
			Name = "Plat réconfortant d'endurance";
		}

		public StamMaxBuffFood(Serial serial) : base(serial)
        {
        }

        public override int StamMaxOffset => 20;
		public override TimeSpan Duration => TimeSpan.FromMinutes(10.0);
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