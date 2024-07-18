using Server.Mobiles;

namespace Server.Items
{
    public class PerfumRemoval : BasePotion
    {
        [Constructable]
        public PerfumRemoval()
            : base(0xF06, PotionEffect.Nightsight)
        {
			Name = "Potion de retrait des parfums";

		}

        public PerfumRemoval(Serial serial)
            : base(serial)
        {
        }

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

        public override void Drink(Mobile from)
        {

          if (from is CustomPlayerMobile cp && cp.Perfume.Nom != null)
          {
            cp.Perfume = new Perfume();

            from.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
            from.PlaySound(0x1E3);

            PlayDrinkEffect(from);
            
            Consume();
          }
          else
          {
            from.SendMessage("Cette potion n'aura aucune effet sur vous.");
          }
        }
    }
}
