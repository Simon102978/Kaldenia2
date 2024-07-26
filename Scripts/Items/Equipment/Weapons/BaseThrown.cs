using System;

namespace Server.Items
{
	public abstract class BaseThrown : BaseRanged
	{
		private Mobile m_Thrower;
		private Mobile m_Target;
		private Point3D m_KillSave;

		public BaseThrown(int itemID)
			: base(itemID)
		{
		}

		public BaseThrown(Serial serial)
			: base(serial)
		{
		}

		public abstract int MinThrowRange { get; }

		public virtual int MaxThrowRange => MinThrowRange + 3;

		public override int DefMaxRange
		{
			get
			{
				int baseRange = MaxThrowRange;

				Mobile attacker = Parent as Mobile;

				if (attacker != null)
				{
					return (baseRange - 3) + ((attacker.Str - StrengthReq) / ((140 - StrengthReq) / 3));
				}
				else
				{
					return baseRange;
				}
			}
		}

		public override int EffectID => ItemID;

		public override Type AmmoType => null;

		public override Item Ammo => null;

		public override int DefHitSound => 0x5D3;
		public override int DefMissSound => 0x5D4;

		public override SkillName DefSkill => SkillName.Archery;

		public override WeaponAnimation DefAnimation => WeaponAnimation.Throwing;

		public override bool OnFired(Mobile attacker, IDamageable damageable)
		{
			m_Thrower = attacker;

			if (!attacker.InRange(damageable, 1))
			{
				attacker.MovingEffect(damageable, EffectID, 18, 1, false, false, Hue, 0);
			}

			// Réduire la durabilité de 1 à chaque attaque
			if (HitPoints > 0)
			{
				HitPoints--;
				InvalidateProperties(); // Mettre à jour l'affichage de l'arme

				if (HitPoints <= 0)
				{
					Delete();
					attacker.SendLocalizedMessage(1044038); // Vous avez détruit votre arme.
					return false;
				}
			}

			return true;
		}

		public override void OnHit(Mobile attacker, IDamageable damageable, double damageBonus)
		{
			m_KillSave = damageable.Location;

			if (!(WeaponAbility.GetCurrentAbility(attacker) is MysticArc))
				Timer.DelayCall(TimeSpan.FromMilliseconds(333.0), ThrowBack);

			base.OnHit(attacker, damageable, damageBonus);
		}

		public override void OnMiss(Mobile attacker, IDamageable damageable)
		{
			m_Target = damageable as Mobile;

			if (!(WeaponAbility.GetCurrentAbility(attacker) is MysticArc))
				Timer.DelayCall(TimeSpan.FromMilliseconds(333.0), ThrowBack);

			base.OnMiss(attacker, damageable);
		}

		public virtual void ThrowBack()
		{
			if (m_Target != null)
				m_Target.MovingEffect(m_Thrower, EffectID, 18, 1, false, false, Hue, 0);
			else if (m_Thrower != null)
				Effects.SendMovingParticles(new Entity(Serial.Zero, m_KillSave, m_Thrower.Map), m_Thrower, ItemID, 18, 0, false, false, Hue, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(1); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if (version == 0)
				InheritsItem = true;
		}

		#region Old Item Serialization Vars
		/* DO NOT USE! Only used in serialization of abyss reaver that originally derived from Item */
		public bool InheritsItem { get; protected set; }
		#endregion
	}
}
