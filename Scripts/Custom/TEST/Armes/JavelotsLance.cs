using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class Javelot : BaseWeapon
	{
		private const int MaxJavelotCount = 100; // Nombre maximum de javelots

		[Constructable]
		public Javelot() : base(0xF62)
		{
			Weight = 4.0;
			Layer = Layer.OneHanded;
			Name = "Javelot de lancer";
			Hue = 0;

			WeaponAttributes.UseBestSkill = 1;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 15;

			JavelotCount = 50; // Valeur par défaut
		}

		public Javelot(Serial serial) : base(serial)
		{
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int JavelotCount { get; set; }

		public override WeaponAbility PrimaryAbility => WeaponAbility.ArmorIgnore;
		public override WeaponAbility SecondaryAbility => WeaponAbility.ParalyzingBlow;

		public override int StrengthReq => 30;
		public override int MinDamage => 12;
		public override int MaxDamage => 14;
		public override float Speed => 3.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 60;
		public override SkillName DefSkill => SkillName.Fencing;

		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;


		public override void OnHit(Mobile attacker, IDamageable defender, double damageBonus)
		{
			base.OnHit(attacker, defender, damageBonus);

			if (attacker.Player && defender is Mobile targetMobile)
			{
				int range = (int)attacker.GetDistanceToSqrt(targetMobile);

				if (range <= 4 && range >= 1)
				{
					attacker.MovingEffect(targetMobile, ItemID, 18, 1, false, false);

					if (JavelotCount > 0)
					{
						JavelotCount--;
					
					attacker.SendMessage("Il vous reste {0} javelots.", JavelotCount);
					InvalidateProperties();
					}
					else
					{
						attacker.SendMessage("Vous n'avez plus de javelots !");
					}
				}
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add("Javelots restants: " + JavelotCount);
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (IsChildOf(from.Backpack) || Parent == from)
			{
				from.SendMessage("Sélectionnez les javelots à ajouter.");
				from.Target = new InternalTarget(this);
			}
			else
			{
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
			}
		}

		private class InternalTarget : Target
		{
			private Javelot m_Javelot;

			public InternalTarget(Javelot javelot) : base(1, false, TargetFlags.None)
			{
				m_Javelot = javelot;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is Javelot targetJavelot && targetJavelot != m_Javelot)
				{
					int spaceLeft = MaxJavelotCount - m_Javelot.JavelotCount;
					if (spaceLeft > 0)
					{
						int toAdd = Math.Min(spaceLeft, targetJavelot.JavelotCount);
						m_Javelot.JavelotCount += toAdd;
						targetJavelot.JavelotCount -= toAdd;

						if (targetJavelot.JavelotCount <= 0)
							targetJavelot.Delete();

						from.SendMessage("Vous avez ajouté {0} javelots. Total: {1}", toAdd, m_Javelot.JavelotCount);
						m_Javelot.InvalidateProperties();
						if (!targetJavelot.Deleted)
							targetJavelot.InvalidateProperties();
					}
					else
					{
						from.SendMessage("Ce javelot ne peut pas contenir plus de projectiles.");
					}
				}
				else
				{
					from.SendMessage("Vous ne pouvez ajouter que des javelots.");
				}
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write(JavelotCount);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			JavelotCount = reader.ReadInt();
		}
	}
}
