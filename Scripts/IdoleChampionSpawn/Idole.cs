using Server.Items;
using Server.Mobiles;
using System;

namespace Server.Engines.Idole
{
    public class Idole : Item
    {
      
        [Constructable]
        public Idole( int ItemId, int hue)
            : base(ItemId)
        {
            Name = "Idole";
            Hue = hue;
            Movable = false;        
        }

      
        public Idole(Serial serial)
            : base(serial)
        {
        }
      
        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("Vous ressentez un drole d'aura entourant l'idole.");
        }




        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();  

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

        }
    }
}
