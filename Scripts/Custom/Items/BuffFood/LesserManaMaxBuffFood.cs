using System;

namespace Server.Items
{
    public class LesserManaMaxBuffFood : BaseManaMaxBuffFood
    {
        [Constructable]
        public LesserManaMaxBuffFood() : base(BuffFoodEffect.ManaMaxLesser)
        {
			Name = "Plat réconfortant de sagesse mineure";
		}

		public LesserManaMaxBuffFood(Serial serial) : base(serial)
        {
        }

        public override int ManaMaxOffset => 5;
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