using System;

namespace Server.Items
{
    public class LesserStamMaxBuffFood : BaseStamMaxBuffFood
    {
        [Constructable]
        public LesserStamMaxBuffFood() : base(BuffFoodEffect.StamMaxLesser)
        {
			Name = "Plat réconfortant d'endurance mineure";
		}

		public LesserStamMaxBuffFood(Serial serial) : base(serial)
        {
        }

        public override int StamMaxOffset => 5;
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