using System.Xml;
using Server.Mobiles;

namespace Server.Regions
{
    public class Mirage : BaseRegion
    {
        public Mirage(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
        {
        }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            return false;
        }


		public override void OnEnter(Mobile m)
		{
			base.OnEnter(m);

            m.SendMessage("Vous entrez dans la cité de Mirage !");

            if (m is BaseCreature bc )
            {
                bc.OnEnterCity();
            }
		}

        public override void OnExit(Mobile from)
        {
            base.OnExit(from);

            from.SendMessage("Vous sortez de Mirage.");
        }


    }
}