using Server.Engines.Craft;
using System;

namespace Server.Items
{
    public enum BuffFoodEffect
    {
		HitsMaxLesser,
        HitsMax,
        HitsMaxGreater,
		ManaMaxLesser,
		ManaMax,
		ManaMaxGreater,
		StamMaxLesser,
		StamMax,
		StamMaxGreater,
	}

    public abstract class BaseBuffFood : Item, ICraftable
    {
        private BuffFoodEffect m_BuffFoodEffect;

        public BuffFoodEffect BuffFoodEffect
        {
            get
            {
                return m_BuffFoodEffect;
            }
            set
            {
                m_BuffFoodEffect = value;
                InvalidateProperties();
            }
        }

        public BaseBuffFood(int itemID, BuffFoodEffect effect)
            : base(itemID)
        {
            m_BuffFoodEffect = effect;

            Stackable = true;
            Weight = 1.0;
        }

        public BaseBuffFood(Serial serial)
            : base(serial)
        {
        }

        public virtual bool RequireFreeHand => true;

        public static bool HasFreeHand(Mobile m)
        {
            Item handOne = m.FindItemOnLayer(Layer.OneHanded);
            Item handTwo = m.FindItemOnLayer(Layer.TwoHanded);

            if (handTwo is BaseWeapon)
                handOne = handTwo;
            if (handTwo is BaseWeapon)
            {
                BaseWeapon wep = (BaseWeapon)handTwo;

                if (wep.Attributes.BalancedWeapon > 0)
                    return true;
            }

            return (handOne == null || handTwo == null);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (from.InRange(GetWorldLocation(), 1))
            {
                if (!RequireFreeHand || HasFreeHand(from))
                    Eat(from);
                else
                    from.SendMessage("Vous devez avoir les mains libres pour manger ceci.");
            }
            else
                from.SendLocalizedMessage(502138); // That is too far away for you to use
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1); // version

            writer.Write((int)m_BuffFoodEffect);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_BuffFoodEffect = (BuffFoodEffect)reader.ReadInt();
        }

        public abstract void Eat(Mobile from);

        public static void PlayDrinkEffect(Mobile m)
        {
            m.RevealingAction();
			m.PlaySound(Utility.Random(0x3A, 3));
			//m.AddToBackpack(new Bottle());

			if (m.Body.IsHuman && !m.Mounted)
            {
                m.Animate(AnimationType.Eat, 0);
            }
        }

        public static int EnhanceBuffFoods(Mobile m)
        {
            return m.Skills.Cooking.Fixed / 330 * 10;
        }

        public static TimeSpan Scale(Mobile m, TimeSpan v)
        {
            double scalar = 1.0 + (0.01 * EnhanceBuffFoods(m));

            return TimeSpan.FromSeconds(v.TotalSeconds * scalar);
        }

        public static double Scale(Mobile m, double v)
        {
            double scalar = 1.0 + (0.01 * EnhanceBuffFoods(m));

            return v * scalar;
        }

        public static int Scale(Mobile m, int v)
        {
            return AOS.Scale(v, 100 + EnhanceBuffFoods(m));
        }

        public override bool WillStack(Mobile from, Item dropped)
        {
            return dropped is BaseBuffFood && ((BaseBuffFood)dropped).m_BuffFoodEffect == m_BuffFoodEffect && base.WillStack(from, dropped);
        }

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
        {
            if (craftSystem is DefCooking)
            {
                
            }

            return 1;
        }
    }
}
