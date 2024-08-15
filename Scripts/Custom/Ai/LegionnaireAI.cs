using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Targets;

namespace Server.Mobiles
{
    public class LegionnaireAI : MeleeAI
	{
        public LegionnaireAI(BaseCreature m)
            : base(m)
        { }

	   

        public override void OnAggressiveAction(Mobile aggressor)
        {
            m_Mobile.ControlOrder = OrderType.Attack;
           

			base.OnAggressiveAction(aggressor);

        }

		public override void OnCurrentOrderChanged()
        {
            if (m_Mobile.Deleted )
            {
                return;
            }

            switch (m_Mobile.ControlOrder)
            {
                case OrderType.None:

                    m_Mobile.Home = m_Mobile.Location;
                    m_Mobile.CurrentSpeed = m_Mobile.PassiveSpeed;
                    m_Mobile.PlaySound(m_Mobile.GetIdleSound());
                    m_Mobile.Warmode = false;
                    m_Mobile.Combatant = null;
                    break;
                case OrderType.Attack:

                    m_Mobile.CurrentSpeed = m_Mobile.ActiveSpeed;
                    m_Mobile.PlaySound(m_Mobile.GetIdleSound());

                    m_Mobile.Warmode = true;
                    m_Mobile.Combatant = null;
                    break;
                case OrderType.Stay:

                    m_Mobile.CurrentSpeed = m_Mobile.PassiveSpeed;
                    m_Mobile.PlaySound(m_Mobile.GetIdleSound());
                    m_Mobile.Warmode = false;
                    m_Mobile.Combatant = null;
                    break;
                case OrderType.Stop:

                    m_Mobile.Home = m_Mobile.Location;
                    m_Mobile.CurrentSpeed = m_Mobile.PassiveSpeed;
                    m_Mobile.PlaySound(m_Mobile.GetIdleSound());
                    m_Mobile.Warmode = false;
                    m_Mobile.Combatant = null;
                    break;
                case OrderType.Follow:

                    m_Mobile.PlaySound(m_Mobile.GetIdleSound());

                    m_Mobile.Warmode = false;
                    m_Mobile.Combatant = null;
                    m_Mobile.AdjustSpeeds();

                    m_Mobile.CurrentSpeed = m_Mobile.ActiveSpeed;
                    break;
            }
        }

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            if (from.Alive && from is CustomPlayerMobile cp && from.InRange(m_Mobile, 14))
            {
               if (cp.GetTribeValue(TribeType.Legion) >= 100)
                {
                    list.Add(new InternalEntry(from, 6111, 14, m_Mobile, this, OrderType.Attack)); // Command: Kill
                    list.Add(new InternalEntry(from, 6108, 14, m_Mobile, this, OrderType.Follow)); // Command: Follow
                    list.Add(new InternalEntry(from, 6112, 14, m_Mobile, this, OrderType.Stop)); // Command: Stop
                    list.Add(new InternalEntry(from, 6114, 14, m_Mobile, this, OrderType.Stay)); // Command: Stay

                }
                else if ( cp.GetTribeValue(TribeType.Legion) >= 90)
                {
                    list.Add(new InternalEntry(from, 6108, 14, m_Mobile, this, OrderType.Follow)); // Command: Follow
                    list.Add(new InternalEntry(from, 6112, 14, m_Mobile, this, OrderType.Stop)); // Command: Stop
                    list.Add(new InternalEntry(from, 6114, 14, m_Mobile, this, OrderType.Stay)); // Command: Stay
                }
            }
        }

		 public override void BeginPickTarget(Mobile from, OrderType order)
        {
            if (m_Mobile.Deleted || !from.InRange(m_Mobile, 14) || from.Map != m_Mobile.Map)
            {
                return;
            }

			if (from is CustomPlayerMobile cp)
			{
				bool isOwner = cp.GetTribeValue(TribeType.Legion) >= 100;
				bool isFriend = (!isOwner && cp.GetTribeValue(TribeType.Legion) >= 90);

				if (!isOwner && !isFriend)
				{
					return;
				}
				if (isFriend && order != OrderType.Follow && order != OrderType.Stay && order != OrderType.Stop)
				{
					return;
				}

				if (from.Target == null)
				{
					if (order == OrderType.Transfer)
					{
						from.SendLocalizedMessage(502038); // Click on the person to transfer ownership to.
					}
					else if (order == OrderType.Friend)
					{
						from.SendLocalizedMessage(502020); // Click on the player whom you wish to make a co-owner.
					}
					else if (order == OrderType.Unfriend)
					{
						from.SendLocalizedMessage(1070948); // Click on the player whom you wish to remove as a co-owner.
					}

					from.Target = new AIControlMobileTarget(this, order);
				}
				else if (from.Target is AIControlMobileTarget)
				{
					AIControlMobileTarget t = (AIControlMobileTarget)from.Target;

					if (t.Order == order)
					{
						t.AddAI(this);
					}
				}
			}

           
        }

        public override void EndPickTarget(Mobile from, IDamageable target, OrderType order)
        {
            if (m_Mobile.Deleted || !from.InRange(m_Mobile, 14) || from.Map != m_Mobile.Map ||
                !from.CheckAlive())
            {
                return;
            }


			if (from is CustomPlayerMobile cp)
			{

				bool isOwner = cp.GetTribeValue(TribeType.Legion) >= 100;
				bool isFriend = (!isOwner && cp.GetTribeValue(TribeType.Legion) >= 90);

				if (!isOwner && !isFriend)
				{
					return;
				}
				if (isFriend && order != OrderType.Follow && order != OrderType.Stay && order != OrderType.Stop)
				{
					return;
				}

				if (order == OrderType.Attack)
				{
					if (target is BaseCreature)
					{
						BaseCreature bc = (BaseCreature)target;

						if (bc.IsScaryToPets && m_Mobile.IsScaredOfScaryThings)
						{
							m_Mobile.SayTo(from, "Your pet refuses to attack this creature!");
							return;
						}
					}
				}

				if (m_Mobile.CheckControlChance(from))
				{
					m_Mobile.ControlTarget = target;
					m_Mobile.ControlOrder = order;
				}
			}
        }


        public class InternalEntry : ContextMenuEntry
        {
            private readonly Mobile m_From;
            private readonly BaseCreature m_Mobile;
            private readonly BaseAI m_AI;
            private readonly OrderType m_Order;

            public InternalEntry(Mobile from, int number, int range, BaseCreature mobile, BaseAI ai, OrderType order)
                : base(number, range)
            {
                m_From = from;
                m_Mobile = mobile;
                m_AI = ai;
                m_Order = order;

                if (mobile.IsDeadPet && (order == OrderType.Guard || order == OrderType.Attack || order == OrderType.Transfer ||
                                         order == OrderType.Drop))
                {
                    Enabled = false;
                }
            }

            public override void OnClick()
            {
                if (!m_Mobile.Deleted && m_From.CheckAlive() && m_From is CustomPlayerMobile cp)
                {
                    if (m_From.Hidden)
                    {
                        m_From.RevealingAction();
                    }

                    if (m_Mobile.IsDeadPet && (m_Order == OrderType.Guard || m_Order == OrderType.Attack ||
                                               m_Order == OrderType.Transfer || m_Order == OrderType.Drop))
                    {
                        return;
                    }

                    bool isOwner = cp.GetTribeValue(TribeType.Legion) >= 100;
                    bool isFriend = (!isOwner && cp.GetTribeValue(TribeType.Legion) >= 90);

                    if (!isOwner && !isFriend)
                    {
                        return;
                    }
                    if (isFriend && m_Order != OrderType.Follow && m_Order != OrderType.Stay && m_Order != OrderType.Stop)
                    {
                        return;
                    }

                    switch (m_Order)
                    {
                        case OrderType.Follow:
                        case OrderType.Attack:

                            {
                             
                                    if (m_From.NetState != null && m_From.NetState.IsEnhancedClient)
                                    {
                                        m_AI.BeginPickTargetDelayed(m_From, m_Order);
                                    }
                                    else
                                    {
                                        m_AI.BeginPickTarget(m_From, m_Order);
                                    }
                                

                                break;
                            }
                        default:
                            {
                                if (m_Mobile.CheckControlChance(m_From))
                                {
                                    m_Mobile.ControlOrder = m_Order;
                                }

                                break;
                            }
                    }
                }
            }
        }



	}
}
