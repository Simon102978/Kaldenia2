using System.Xml;

namespace Server.Regions
{
    public class CreationRoom : BaseRegion
    {
        public CreationRoom(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
        {
        }

        public override bool AllowAutoClaim(Mobile from)
        {
            return false;
        }

        public override bool AllowBeneficial(Mobile from, Mobile target)
        {
            if (from.IsPlayer())
                from.SendLocalizedMessage(1115999); // You may not do that in this area.

            return (from.IsStaff());
        }

        public override bool AllowHarmful(Mobile from, IDamageable target)
        {
            if (from.Player)
                from.SendLocalizedMessage(1115999); // You may not do that in this area.

            return (from.IsStaff());
        }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            return false;
        }

        public override bool OnBeginSpellCast(Mobile from, ISpell s)
        {
            if (from.IsPlayer())
            {
                from.SendLocalizedMessage(502629); // You cannot cast spells here.
                return false;
            }

            return base.OnBeginSpellCast(from, s);
        }

        public override bool OnSkillUse(Mobile from, int Skill)
        {
            if (from.IsPlayer())
                from.SendLocalizedMessage(1116000); // You may not use that skill in this area.

            return (from.IsStaff());
        }

		public override void OnEnter(Mobile m)
		{
			base.OnEnter(m);

            m.SendMessage("Bienvenue sur Kaldenia ! Amusez-vous bien !");
		}

        public override void OnExit(Mobile from)
        {
            base.OnExit(from);

            from.SendMessage("Bonne aventure !");
        }

		public override bool OnCombatantChange(Mobile from, IDamageable Old, IDamageable New)
        {
            return (from.IsStaff());
        }
    }
}