namespace Server.Mobiles
{
    public class ArcherMageAI : MageAI
	{
        public ArcherMageAI (BaseCreature m)
            : base(m)
        { }


        public override void RunTo(IDamageable d)
        {

            IDamageable c = m_Mobile.Combatant;

            if (c == null || c.Deleted || !c.Alive || (c is Mobile && ((Mobile)c).IsDeadBondedPet))
            {
                m_Mobile.DebugSay("My combatant is deleted");
                Action = ActionType.Guard;
                return;
            }

            if (Core.TickCount - m_Mobile.LastMoveTime > 1000)
            {
                if (WalkMobileRange(c, 1, true, m_Mobile.RangeFight, m_Mobile.Weapon.MaxRange))
                {
                    if (!DirectionLocked)
                        m_Mobile.Direction = m_Mobile.GetDirectionTo(c.Location);
                }
                else
                {
                    m_Mobile.DebugSay("I am still not in range of {0}", c.Name);

                    if ((int)m_Mobile.GetDistanceToSqrt(c) > m_Mobile.RangePerception + 1)
                    {
                        m_Mobile.DebugSay("I have lost {0}", c.Name);

                        Action = ActionType.Guard;
                        return;
                    }
                }
            }

            if (!m_Mobile.Controlled && !m_Mobile.Summoned && m_Mobile.CheckCanFlee())
            {
                m_Mobile.DebugSay("I am going to flee from {0}", c.Name);
                Action = ActionType.Flee;
            }
        }


		
	}
}
