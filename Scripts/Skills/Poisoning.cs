using Server.Gumps;
using Server.Items;
using Server.Spells;
using Server.Targeting;
using System;
using System.Collections.Generic;

namespace Server.SkillHandlers
{
	public class Poisoning
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Poisoning].Callback = OnUse;
		}

		public static TimeSpan OnUse(Mobile m)
		{
			m.Target = new InternalTargetPoison();
			m.SendLocalizedMessage(502137); // Select the poison you wish to use
			return TimeSpan.FromSeconds(10.0);
		}

		private class InternalTargetPoison : Target
		{
			public InternalTargetPoison() : base(2, false, TargetFlags.None) { }

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is BasePoisonPotion potion)
				{
					from.SendGump(new PoisonOptionGump(from, potion));
				}
				else
				{
					from.SendLocalizedMessage(502139); // That is not a poison potion.
				}
			}
		}

		public class PoisonOptionGump : Gump
		{
			private readonly Mobile m_From;
			private readonly BasePoisonPotion m_Potion;

			public PoisonOptionGump(Mobile from, BasePoisonPotion potion) : base(50, 50)
			{
				m_From = from;
				m_Potion = potion;

				AddPage(0);
				AddBackground(0, 0, 200, 130, 9200);
				AddAlphaRegion(10, 10, 180, 110);

				AddHtml(20, 20, 160, 20, "<basefont color=#ffffff><CENTER>Menu Poison</CENTER></basefont>", false, false);
				AddButton(20, 50, 4005, 4007, 1, GumpButtonType.Reply, 0);
				AddHtml(55, 50, 120, 20, "<basefont color=#ffffff>Appliquer</basefont>", false, false);
				AddButton(20, 80, 4005, 4007, 2, GumpButtonType.Reply, 0);
				AddHtml(55, 80, 120, 20, "<basefont color=#ffffff>Lancer</basefont>", false, false);
			}

			public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
			{
				if (m_Potion.Deleted)
					return;

				switch (info.ButtonID)
				{
					case 1: // Apply to weapon
						m_From.SendLocalizedMessage(502142); // To what do you wish to apply the poison?
						m_From.Target = new InternalTarget(m_Potion);
						break;
					case 2: // Throw potion
						m_From.Target = new ThrowTarget(m_Potion);
						break;
				}
			}
		}

		private class InternalTarget : Target
		{
			private readonly BasePoisonPotion m_Potion;

			public InternalTarget(BasePoisonPotion potion) : base(2, false, TargetFlags.None)
			{
				m_Potion = potion;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_Potion.Deleted)
					return;

				bool startTimer = false;

				if (targeted is Food || targeted is FukiyaDarts || targeted is Shuriken)
				{
					startTimer = true;
				}
				else if (targeted is BaseWeapon weapon)
				{
					startTimer = (weapon.PrimaryAbility == WeaponAbility.InfectiousStrike || weapon.SecondaryAbility == WeaponAbility.InfectiousStrike);
				}

				if (startTimer)
				{
					new InternalTimer(from, (Item)targeted, m_Potion).Start();
					from.PlaySound(0x4F);
					m_Potion.Consume();
					from.AddToBackpack(new Bottle());
				}
				else // Target can't be poisoned
				{
					from.SendLocalizedMessage(1060204); // You cannot poison that! You can only poison infectious weapons, food or drink.
				}
			}
		}

		private class ThrowTarget : Target
		{
			private readonly BasePoisonPotion m_Potion;

			public ThrowTarget(BasePoisonPotion potion) : base(12, true, TargetFlags.None)
			{
				m_Potion = potion;
			}


			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_Potion.Deleted || m_Potion.Map == Map.Internal)
					return;

				if (targeted is IPoint3D p)
				{
					if (from.Map == null)
						return;

					SpellHelper.GetSurfaceTop(ref p);

					from.RevealingAction();

					Point3D targetLoc = new Point3D(p);

					Effects.SendMovingEffect(from, new Entity(Serial.Zero, targetLoc, from.Map),
						0xF0D, 7, 0, false, false, m_Potion.Hue, 0);

					Timer.DelayCall(TimeSpan.FromSeconds(1.5), () => Explode(from, targetLoc, from.Map));
				}
			}

			private void Explode(Mobile from, Point3D loc, Map map)
			{
				if (map == null)
					return;

				m_Potion.Consume();

				Effects.PlaySound(loc, map, 0x20C);

				bool skillCheck = from.CheckTargetSkill(SkillName.Poisoning, new object(), m_Potion.MinPoisoningSkill, m_Potion.MaxPoisoningSkill);

				for (int i = -1; i <= 1; i++)
				{
					for (int j = -1; j <= 1; j++)
					{
						Point3D p = new Point3D(loc.X + i, loc.Y + j, loc.Z);
						SpellHelper.AdjustField(ref p, map, 16, true);

						if (map.CanFit(p, 12, true, false))
						{
							if (skillCheck)
							{
								new PoisonFieldEffect(from, p, map, m_Potion.Poison);
							}
							else
							{
								// Créer un effet visuel sans empoisonnement
								Effects.SendLocationEffect(p, map, 0x3728, 30, 10, 2006, 0);
							}
						}
					}
				}

				if (skillCheck)
				{
					from.SendLocalizedMessage(1010517); // You apply the poison
					Misc.Titles.AwardKarma(from, -20, true);
				}
				else
				{
					from.SendLocalizedMessage(1010518); // You fail to apply a sufficient dose of poison

					// 5% de chance de s'empoisonner soi-même en cas d'échec
					if (from.Skills[SkillName.Poisoning].Base < 80.0 && Utility.Random(20) == 0)
					{
						from.SendLocalizedMessage(502148); // You make a grave mistake while applying the poison.
						from.ApplyPoison(from, m_Potion.Poison);
					}
				}
			}
		}

		private class InternalTimer : Timer
		{
			private readonly Mobile m_From;
			private readonly Item m_Target;
			private readonly Poison m_Poison;
			private readonly double m_MinSkill;
			private readonly double m_MaxSkill;

			public InternalTimer(Mobile from, Item target, BasePoisonPotion potion)
				: base(TimeSpan.FromSeconds(2.0))
			{
				m_From = from;
				m_Target = target;
				m_Poison = potion.Poison;
				m_MinSkill = potion.MinPoisoningSkill;
				m_MaxSkill = potion.MaxPoisoningSkill;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				if (m_From.CheckTargetSkill(SkillName.Poisoning, m_Target, m_MinSkill, m_MaxSkill))
				{
					if (m_Target is Food food)
					{
						food.Poison = m_Poison;
						food.Poisoner = m_From;
					}
					else if (m_Target is BaseWeapon weapon)
					{
						weapon.Poison = m_Poison;
						weapon.PoisonCharges = 18 - (m_Poison.RealLevel * 2);
					}
					else if (m_Target is FukiyaDarts darts)
					{
						darts.Poison = m_Poison;
						darts.PoisonCharges = Math.Min(18 - (m_Poison.RealLevel * 2), darts.UsesRemaining);
					}
					else if (m_Target is Shuriken shuriken)
					{
						shuriken.Poison = m_Poison;
						shuriken.PoisonCharges = Math.Min(18 - (m_Poison.RealLevel * 2), shuriken.UsesRemaining);
					}

					m_From.SendLocalizedMessage(1010517); // You apply the poison
					Misc.Titles.AwardKarma(m_From, -20, true);
				}
				else // Failed
				{
					if (m_From.Skills[SkillName.Poisoning].Base < 80.0 && Utility.Random(20) == 0)
					{
						m_From.SendLocalizedMessage(502148); // You make a grave mistake while applying the poison.
						m_From.ApplyPoison(m_From, m_Poison);
					}
					else
					{
						if (m_Target is BaseWeapon weapon)
						{
							if (weapon.Type == WeaponType.Slashing)
								m_From.SendLocalizedMessage(1010516); // You fail to apply a sufficient dose of poison on the blade
							else
								m_From.SendLocalizedMessage(1010518); // You fail to apply a sufficient dose of poison
						}
						else
						{
							m_From.SendLocalizedMessage(1010518); // You fail to apply a sufficient dose of poison
						}
					}
				}
			}
		}

		private class PoisonFieldEffect : Item
		{
			private Mobile m_Owner;
			private Poison m_Poison;
			private DateTime m_End;
			private Timer m_Timer;

			public PoisonFieldEffect(Mobile from, Point3D loc, Map map, Poison poison)
				: base(0x3728)
			{
				Movable = false;
				Hue = 2006;
				MoveToWorld(loc, map);

				m_Owner = from;
				m_Poison = poison;
				m_End = DateTime.UtcNow + TimeSpan.FromSeconds(10);

				m_Timer = Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromSeconds(1), OnTick);
			}

			// Ajout du constructeur de sérialisation
			public PoisonFieldEffect(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write(0); // version

				writer.Write(m_Owner);
				writer.Write(m_End);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();

				m_Owner = reader.ReadMobile();
				m_End = reader.ReadDateTime();

				// Redémarrer le timer après la désérialisation
				m_Timer = Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromSeconds(1), OnTick);
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();
				m_Timer?.Stop();
			}

			private void OnTick()
			{
				if (DateTime.UtcNow > m_End)
				{
					Delete();
					return;
				}

				List<Mobile> toPoisonList = new List<Mobile>();

				foreach (Mobile m in GetMobilesInRange(0))
				{
					if (m != m_Owner && m.Alive && !m.IsDeadBondedPet && m_Owner.CanBeHarmful(m, false))
					{
						toPoisonList.Add(m);
					}
				}

				foreach (Mobile m in toPoisonList)
				{
					if (m_Owner.CheckSkill(SkillName.Poisoning, 0, 100))
					{
						m.ApplyPoison(m_Owner, m_Poison);
						m.PlaySound(0x474);
					}
				}
			}
		}
	}
}
