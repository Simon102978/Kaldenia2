using System;

namespace Server.Items
{
    public class LesserHitsMaxBuffFood : BaseHitsMaxBuffFood
    {
        [Constructable]
        public LesserHitsMaxBuffFood() : base(BuffFoodEffect.HitsMaxLesser)
        {
			Name = "Plat réconfortant de constitution mineure";
		}

		public LesserHitsMaxBuffFood(Serial serial) : base(serial)
        {
        }

        public override int HitsMaxOffset => 5;
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