using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Spells.Necromancy;
using Server.Targeting;

namespace Server.Spells.Fourth
{
    public class CustomRecallSpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Recall", "Kal Ort Por",
            239,
            9031,
            Reagent.BlackPearl,
            Reagent.Bloodmoss,
            Reagent.MandrakeRoot);

		public override MagicAptitudeRequirement[] AffinityRequirements { get { return new MagicAptitudeRequirement[] { new MagicAptitudeRequirement(MagieType.Arcane, 10) }; } }

        public CustomRecallSpell(Mobile caster, Item scroll)
             : base(caster, scroll, m_Info)
        {
        }
      
        public override SpellCircle Circle => SpellCircle.Fourth;

        public override void OnCast()
        {
            if (Engines.VvV.VvVSigil.ExistsOn(Caster))
            {
                Caster.SendLocalizedMessage(1061632); // You can't do that while carrying the sigil.
            }
            else if (!SpellHelper.CheckTravel(Caster, TravelCheckType.RecallFrom))
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
            else if (Misc.WeightOverloading.IsOverloaded(Caster))
            {
                Caster.SendLocalizedMessage(502359, "", 0x22); // Thou art too encumbered to move.
            }
            else if (Caster.Holding != null)
            {
                Caster.SendLocalizedMessage(1071955); // You cannot teleport while dragging an object.
            }
            else if (Engines.CityLoyalty.CityTradeSystem.HasTrade(Caster))
            {
                Caster.SendLocalizedMessage(1151733); // You cannot do that while carrying a Trade Order.
            }
            else if (CheckSequence())
            {
                if (Caster is CustomPlayerMobile cp && cp.TeleportStone != null) 
                {
                   cp.TeleportStone.Teleport(cp);
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
            else if (Misc.WeightOverloading.IsOverloaded(Caster))
            {
                Caster.SendLocalizedMessage(502359, "", 0x22); // Thou art too encumbered to move.
                return false;
            }
			else if (Caster.CheckPackage())
			{
				Caster.SendMessage("Vous ne pouvez pas vous teleporter avec un paquet.");
				return false;
			}
            else if(Caster is CustomPlayerMobile cp && cp.TeleportStone == null)
            {
                Caster.SendMessage("Vous devez être lié à une pierre de Téléportation avant d'utiliser ce sortilège.");
                return false;
            }
		

            return SpellHelper.CheckTravel(Caster, TravelCheckType.RecallFrom);
        }



    

    }
}
