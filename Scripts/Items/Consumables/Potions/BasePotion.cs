using Server.Engines.Craft;
using System;
using System.Collections.Generic;

namespace Server.Items
{
    public enum PotionEffect
    {
		Nightsight,
		CureLesser,
		Cure,
		CureGreater,
		CureSuperior,
		AgilityLesser,
		Agility,
		AgilityGreater,
		AgilitySuperior,
		StrengthLesser,
		Strength,
		StrengthGreater,
		StrengthSuperior,
		PoisonLesser,
		Poison,
		PoisonGreater,
		PoisonDeadly,
		RefreshLesser,
		Refresh,
		RefreshGreater,
		RefreshSuperior,
		HealLesser,
		Heal,
		HealGreater,
		HealSuperior,
		ExplosionLesser,
		Explosion,
		ExplosionGreater,
		ExplosionSuperior,
		ConflagrationLesser,
		Conflagration,
		ConflagrationGreater,
		ConflagrationSuperior,
		MaskOfDeath,        // Mask of Death is not available in OSI but does exist in cliloc files
		MaskOfDeathGreater, // included in enumeration for compatability if later enabled by OSI
		ConfusionBlastLesser,
		ConfusionBlast,
		ConfusionBlastGreater,
		ConfusionBlastSuperior,
		Invisibility,
		Parasitic,
		Darkglow,
		Barrab,
		Kurak,
		Barako,
		Urali,
		Sakkhra,
		Shatter,
		Jukari,
		FearEssence,
		Shrink,
		ExplodingTarPotion,
		RefreshTotal,
		UltimeCure,
		Experience,
        Perfume,
		EtherealManifestation,
		IntelligenceLesser,
		Intelligence,
		IntelligenceGreater,
		IntelligenceSuperior,
		ManaLesser,
		Mana,
		ManaGreater,
		ManaSuperior




	}

    public abstract class BasePotion : Item, ICraftable, ICommodity
    {

		private int m_AlchemyLevel;
		private TimeSpan m_CustomDuration;

		[CommandProperty(AccessLevel.GameMaster)]
		public TimeSpan CustomDuration
		{
			get { return m_CustomDuration; }
			set
			{
				m_CustomDuration = value;
				InvalidateProperties();
			}
		}
		public int AlchemyLevel
		{
			get { return m_AlchemyLevel; }
			set { m_AlchemyLevel = value; }
		}
		private PotionEffect m_PotionEffect;

		public static TimeSpan DefaultDuration = TimeSpan.FromMinutes(5.0);
		public virtual TimeSpan MinimumDuration => TimeSpan.Zero;
		public virtual TimeSpan Duration
		{
			get
			{
				if (m_CustomDuration > TimeSpan.Zero)
				{
					return m_CustomDuration;
				}

				double minutes = 5 + (55 * m_AlchemyLevel / 100.0);
				TimeSpan calculatedDuration = TimeSpan.FromMinutes(minutes);

				if (calculatedDuration < DefaultDuration)
				{
					calculatedDuration = DefaultDuration;
				}

				return TimeSpan.FromTicks(Math.Max(calculatedDuration.Ticks, MinimumDuration.Ticks));
			}
		}

		public PotionEffect PotionEffect
        {
            get
            {
                return m_PotionEffect;
            }
            set
            {
                m_PotionEffect = value;
                InvalidateProperties();
            }
        }

        TextDefinition ICommodity.Description => LabelNumber;

        bool ICommodity.IsDeedable => true;

        public override int LabelNumber => 1041314 + (int)m_PotionEffect;

        public BasePotion(int itemID, PotionEffect effect)
            : base(itemID)
        {
            m_PotionEffect = effect;

            Stackable = true;
            Weight = 1.0;
        }

        public BasePotion(Serial serial)
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
                {
                    if (this is BaseExplosionPotion && Amount > 1)
                    {
                        BasePotion pot = (BasePotion)Activator.CreateInstance(GetType());

                        if (pot != null)
                        {
                            Amount--;

                            if (from.Backpack != null && !from.Backpack.Deleted)
                            {
                                from.Backpack.DropItem(pot);
                            }
                            else
                            {
                                pot.MoveToWorld(from.Location, from.Map);
                            }
                            pot.Drink(from);
                        }
                    }
                    else
                    {
                        Drink(from);
                    }
                }
                else
                {
                    from.SendLocalizedMessage(502172); // You must have a free hand to drink a potion.
                }
            }
            else
            {
                from.SendLocalizedMessage(502138); // That is too far away for you to use
            }
        }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(3); // version

			writer.Write(m_CustomDuration);
			writer.Write(m_AlchemyLevel);
			writer.Write((int)m_PotionEffect);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if (version >= 3)
			{
				m_CustomDuration = reader.ReadTimeSpan();
			}

			if (version >= 2)
			{
				m_AlchemyLevel = reader.ReadInt();
			}

			m_PotionEffect = (PotionEffect)reader.ReadInt();
		}


		public abstract void Drink(Mobile from);

		public static void PlayDrinkEffect(Mobile m)
		{
			m.RevealingAction();
			m.PlaySound(0x2D6);
			m.AddToBackpack(new Bottle());

			if (m.Body.IsHuman && !m.Mounted)
			{
				m.Animate(AnimationType.Eat, 0);
			}
		}

		public static int EnhancePotions(Mobile m)
        {
            int EP = AosAttributes.GetValue(m, AosAttribute.EnhancePotions);
            int skillBonus = m.Skills.Alchemy.Fixed / 330 * 10;

            if (EP > 50 && m.IsPlayer())
                EP = 50;

            return EP + skillBonus;
        }

        public static TimeSpan Scale(Mobile m, TimeSpan v)
        {
            double scalar = 1.0 + (0.01 * EnhancePotions(m));

            return TimeSpan.FromSeconds(v.TotalSeconds * scalar);
        }

        public static double Scale(Mobile m, double v)
        {
            double scalar = 1.0 + (0.01 * EnhancePotions(m));

            return v * scalar;
        }

        public static int Scale(Mobile m, int v)
        {
            return AOS.Scale(v, 100 + EnhancePotions(m));
        }

        public override bool WillStack(Mobile from, Item dropped)
        {
            return dropped is BasePotion && ((BasePotion)dropped).m_PotionEffect == m_PotionEffect && base.WillStack(from, dropped);
        }

		public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
		{
			if (craftSystem is DefAlchemy)
			{
				// Définir le niveau d'alchimie du créateur
				m_AlchemyLevel = (int)from.Skills[SkillName.Alchemy].Value;

				Container pack = from.Backpack;

				if (pack != null)
				{
					if ((int)PotionEffect >= (int)PotionEffect.Invisibility)
						return 1;

					List<PotionKeg> kegs = pack.FindItemsByType<PotionKeg>();

					for (int i = 0; i < kegs.Count; ++i)
					{
						PotionKeg keg = kegs[i];

						if (keg == null)
							continue;

						if (keg.Held <= 0 || keg.Held >= 100)
							continue;

						if (keg.Type != PotionEffect)
							continue;

						++keg.Held;

						Consume();
						from.AddToBackpack(new Bottle());

						return -1; // signal placed in keg
					}
				}
			}

			return 1;
		}
	}
}

