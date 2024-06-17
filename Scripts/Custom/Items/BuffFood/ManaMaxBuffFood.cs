using System;

namespace Server.Items
{
    public class ManaMaxBuffFood : BaseManaMaxBuffFood
    {
        [Constructable]
        public ManaMaxBuffFood() : base(BuffFoodEffect.ManaMax)
        {
			Name = "Plat réconfortant de sagesse";
		}

		public ManaMaxBuffFood(Serial serial) : base(serial)
        {
        }

        public override int ManaMaxOffset => 10;
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