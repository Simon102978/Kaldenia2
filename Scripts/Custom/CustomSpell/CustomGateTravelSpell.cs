using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using System;

namespace Server.Spells.Seventh
{
    public class CustomGateTravelSpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Gate Travel", "Vas Rel Por",
            263,
            9032,
            Reagent.BlackPearl,
            Reagent.MandrakeRoot,
            Reagent.SulfurousAsh);


		public override MagicAptitudeRequirement[] AffinityRequirements { get { return new MagicAptitudeRequirement[] { new MagicAptitudeRequirement(MagieType.Cycle, 15) }; } }
     
        public CustomGateTravelSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }


        public override SpellCircle Circle => SpellCircle.Seventh;
        public override void OnCast()
        {
            if (Engines.VvV.VvVSigil.ExistsOn(Caster))
            {
                Caster.SendLocalizedMessage(1061632); // You can't do that while carrying the sigil.
            }
            else if (!SpellHelper.CheckTravel(Caster, TravelCheckType.GateFrom))
            {
            }
            else if (Caster.Criminal)
            {
                Caster.SendLocalizedMessage(1005561, "", 0x22); // Thou'rt a criminal and cannot escape so easily.
            }
            else if (SpellHelper.CheckCombat(Caster))
            {
                Caster.SendLocalizedMessage(1005564, "", 0x22); // Wouldst thou flee during the heat of battle??
            }
            else if (Engines.CityLoyalty.CityTradeSystem.HasTrade(Caster))
            {
                Caster.SendLocalizedMessage(1151733); // You cannot do that while carrying a Trade Order.
            }
            else if (CheckSequence())
            {
                if (Caster is CustomPlayerMobile cp && cp.TeleportStone != null) 
                {
                   cp.TeleportStone.CreateGate(cp);
                }
            }

            FinishSequence();





        }

        public override bool CheckCast()
        {
            if (Engines.VvV.VvVSigil.ExistsOn(Caster))
            {
                Caster.SendLocalizedMessage(1061632); // You can't do that while carrying the sigil.
                return false;
            }
            else if (Engines.CityLoyalty.CityTradeSystem.HasTrade(Caster))
            {
                Caster.SendLocalizedMessage(1151733); // You cannot do that while carrying a Trade Order.
                return false;
            }
            else if (Caster.Criminal)
            {
                Caster.SendLocalizedMessage(1005561, "", 0x22); // Thou'rt a criminal and cannot escape so easily.
                return false;
            }
            else if (SpellHelper.CheckCombat(Caster))
            {
                Caster.SendLocalizedMessage(1005564, "", 0x22); // Wouldst thou flee during the heat of battle??
                return false;
            }
            else if(Caster is CustomPlayerMobile cp && cp.TeleportStone == null)
            {
                Caster.SendMessage("Vous devez être lié à une pierre de Téléportation avant d'utiliser ce sortilège.");
                return false;
            }

            return SpellHelper.CheckTravel(Caster, TravelCheckType.GateFrom);
        }





    }
}
