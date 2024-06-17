using System;

namespace Server.Items
{
    public class GreaterStamMaxBuffFood : BaseStamMaxBuffFood
    {
        [Constructable]
        public GreaterStamMaxBuffFood() : base(BuffFoodEffect.StamMaxGreater)
        {
			Name = "Plat réconfortant d'endurance majeure";
		}

		public GreaterStamMaxBuffFood(Serial serial) : base(serial)
        {
        }

        public override int StamMaxOffset => 15;
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