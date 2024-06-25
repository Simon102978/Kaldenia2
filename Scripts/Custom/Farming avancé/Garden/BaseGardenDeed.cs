using System; 
using Server.Network; 
using Server.Prompts; 
using Server.Items; 
using Server.Mobiles; 
using Server.Gumps;
using Server.Misc;
using Server.Multis;
using System.Security.Cryptography;

namespace Server.Items
{
    public abstract class BaseGardenDeed : Item
    {
      
        [Constructable]
        public BaseGardenDeed()
            : base(3720) //pitchfork
        {
            Name = "Outil placement de jardin";
            Hue = 1164;
            Weight = 50.0;
            LootType = LootType.Blessed;
        }

        public abstract BaseGarden GetGarden (Mobile from);
        


        public override void OnDoubleClick(Mobile from)
        {
                IPoint3D p = from.Location;
                Map map = from.Map;

                if (p == null || map == null)
                    return;

                if (IsChildOf(from.Backpack))
                {
                    Spells.SpellHelper.GetSurfaceTop(ref p);

                    BaseGarden Garden = GetGarden (from);


                    if (Garden.CouldGardenFit(p, map))
                    {
  
                        Garden.MoveToWorld(new Point3D(p), map);

                        Garden.UpdateRegion();

                        this.Delete();
                    }
                    else
                    {
                        from.SendMessage("Il est impossible de placer un jardin à cet endroit.");
                        Garden.Delete();
                    }
                  
                }
                else
                {
                    from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                }
              
            
        }

         public BaseGardenDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
