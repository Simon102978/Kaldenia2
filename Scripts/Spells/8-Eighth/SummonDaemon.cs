using Server.Mobiles;
using System;

namespace Server.Spells.Eighth
{
    public class SummonDaemonSpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Summon Daemon", "Kal Vas Xen Corp",
            269,
            9050,
            false,
            Reagent.Bloodmoss,
            Reagent.MandrakeRoot,
            Reagent.SpidersSilk);

		public override MagicAptitudeRequirement[] AffinityRequirements { get { return new MagicAptitudeRequirement[] { new MagicAptitudeRequirement(MagieType.Mort, 16) }; } }
		public SummonDaemonSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle => SpellCircle.Eighth;
        public override bool CheckCast()
        {
            if (!base.CheckCast())
                return false;

            if ((Caster.Followers + 3) > Caster.FollowersMax)
            {
                Caster.SendLocalizedMessage(1049645); // You have too many followers to summon that creature.
                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            if (CheckSequence())
            {
                TimeSpan duration = TimeSpan.FromSeconds((2 * Caster.Skills.Magery.Fixed) / 4);

                BaseCreature m_Daemon = new SummonedDaemon();
                SpellHelper.Summon(m_Daemon, Caster, 0x216, duration, false, false);
                m_Daemon.FixedParticles(0x3728, 8, 20, 5042, EffectLayer.Head);
            }

            FinishSequence();
        }
    }
}
