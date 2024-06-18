using System;

namespace Server.Items
{
    public class HitsMaxBuffFood : BaseHitsMaxBuffFood
    {
        [Constructable]
        public HitsMaxBuffFood() : base(BuffFoodEffect.HitsMax)
        {
			Name = "Plat réconfortant de constitution";
		}

		public HitsMaxBuffFood(Serial serial) : base(serial)
        {
        }

        public override int HitsMaxOffset => 10;
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