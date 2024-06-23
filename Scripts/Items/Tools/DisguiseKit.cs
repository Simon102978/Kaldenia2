#region References
using Server.Gumps;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Fifth;
#endregion

namespace Server.Items
{
    public class DisguiseKit : Item
    {
        [Constructable]
        public DisguiseKit()
            : base(0xE05)
        {
            Weight = 1.0;
        }

        public DisguiseKit(Serial serial)
            : base(serial)
        { }

        public override int LabelNumber => 1041078;  // a disguise kit

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            reader.ReadInt();
        }

        public bool ValidateUse(Mobile from)
        {
            PlayerMobile pm = from as PlayerMobile;

            if (!IsChildOf(from.Backpack))
            {
                // That must be in your pack for you to use it.
                from.SendLocalizedMessage(1042001);
            }
 /*           else if (pm == null || pm.NpcGuild != NpcGuild.ThievesGuild)
            {
                // Only Members of the thieves guild are trained to use this item.
                from.SendLocalizedMessage(501702);
            }
            else if (Stealing.SuspendOnMurder && pm.Kills > 0)
            {
                // You are currently suspended from the thieves guild.  They would frown upon your actions.
                from.SendLocalizedMessage(501703);
            }*/
            else if (!from.CanBeginAction(typeof(IncognitoSpell)))
            {
                // You cannot disguise yourself while incognitoed.
                from.SendLocalizedMessage(501704);
            }
            //else if (TransformationSpellHelper.UnderTransformation(from))
            //{
            //    // You cannot disguise yourself while in that form.
            //    from.SendLocalizedMessage(1061634);
            //}
            else if (from.BodyMod == 183 || from.BodyMod == 184)
            {
                // You cannot disguise yourself while wearing body paint
                from.SendLocalizedMessage(1040002);
            }
            //else if (!from.CanBeginAction(typeof(PolymorphSpell)) || from.IsBodyMod)
            //{
            //    // You cannot disguise yourself while polymorphed.
            //    from.SendLocalizedMessage(501705);
            //}
            else
            {
                return true;
            }

            return false;
        }

        public override void OnDoubleClick(Mobile from)
        {
    //        if (ValidateUse(from))
    //        {
				//if (from is CustomPlayerMobile)
				//{
				//	CustomPlayerMobile cm = (CustomPlayerMobile)from;

				//	if (cm.GetDeguisement() == null)
				//	{
				//		from.SendGump(new CustomDisguiseGump(cm, new Deguisement(cm)));
				//	}
				//	else
				//	{
				//		from.SendGump(new CustomDisguiseGump(cm, cm.GetDeguisement()));
				//	}					
				//}
    //        }
        }
    }
}
