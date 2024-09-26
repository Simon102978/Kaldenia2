#region References
using Server.Engines.CityLoyalty;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Regions;
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Server.Items
{

    public class RouxPorter : ConditionTeleporter
    {

     

        [Constructable]
        public RouxPorter()
            : base()
        {





        }


        public RouxPorter(Serial serial)
            : base(serial)
        { }

       
         public override void DoTeleport(Mobile m)
        {
            base.DoTeleport(m);

            if (m is CustomPlayerMobile cp )
            {
                if (!cp.CheckRoux())
                {
                    cp.SendMessage("MOUAHAHAHA ! Te voila un roux-volutionnaire !! ");
                    cp.HairHue = 1530;
                    cp.FacialHairHue = 1530;
                }
            }
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

        






    }

}
