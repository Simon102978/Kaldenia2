using System;

namespace Server.Items
{
    public class GreaterHitsMaxBuffFood : BaseHitsMaxBuffFood
    {
        [Constructable]
        public GreaterHitsMaxBuffFood() : base(BuffFoodEffect.HitsMaxGreater)
        {
			Name = "Plat réconfortant de constitution majeure";
		}

		public GreaterHitsMaxBuffFood(Serial serial) : base(serial)
        {
        }

        public override int HitsMaxOffset => 15;
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