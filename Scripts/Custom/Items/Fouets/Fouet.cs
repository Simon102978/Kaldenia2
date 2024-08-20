using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Spells;
using Server.Gumps;

namespace Server.Items
{
	public abstract class Fouet : Item
	{
		private int m_MaxRange;

		[CommandProperty(AccessLevel.GameMaster)]
		public int MaxRange
		{
			get { return m_MaxRange; }
			set { m_MaxRange = value; }
		}
		[CommandProperty(AccessLevel.GameMaster)]
		public int FreezeDuration { get; set; } = Utility.RandomMinMax(2, 5); // Durée du gel en secondes

		[CommandProperty(AccessLevel.GameMaster)]
		public int BleedDuration { get; set; } = Utility.RandomMinMax(3, 7); // Durée du saignement en secondes
		public Fouet() : this(4)
		{
		}

		public Fouet(int maxRange) : base(5742)
		{
			Layer = Layer.OneHanded;
			Name = "fouet";
			Weight = 2.0;

			m_MaxRange = maxRange;
		}

		public Fouet(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)1); // version

			writer.Write((int)m_MaxRange);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 1:
					{
						m_MaxRange = reader.ReadInt();
						break;
					}
			}

			Layer = Layer.OneHanded;
		}

		public override bool CanEquip(Mobile from)
		{
			Item item = from.FindItemOnLayer(Layer.TwoHanded);

			if (item != null && item is BaseShield)
			{
				from.SendMessage("Vous ne pouvez pas équipper un fouet en ayant un bouclier à la main.");
				return false;
			}

			return true;
		}

		public virtual void Fouetage(Mobile from, Mobile receveur)
		{
			PlayerMobile pm = from as PlayerMobile;

			if (pm != null)
			{
				pm.RevealingAction();
				SpellHelper.Turn(from, receveur);

				double snooping = pm.Skills[SkillName.Snooping].Value;
				double stealing = pm.Skills[SkillName.Stealing].Value;

				double freezeChance = (snooping + stealing) / 400.0; // Max 50% chance at 100 in both skills
				double bleedChance = (snooping + stealing) / 285.0; // Max 70% chance at 100 in both skills

				if (receveur is BaseCreature bc && !(receveur is BaseHire))
				{
					// Appliquer l'effet de gel
					if (Utility.RandomDouble() < freezeChance)
					{
						bc.Frozen = true;
						Timer.DelayCall(TimeSpan.FromSeconds(FreezeDuration), () =>
						{
							if (bc != null && !bc.Deleted)
							{
								bc.Frozen = false;
							}
						});
						from.SendMessage("Vous gelez votre cible !");
						receveur.SendMessage("Vous êtes gelé !");
					}

					// Appliquer l'effet de saignement
					if (Utility.RandomDouble() < bleedChance)
					{
						ApplyBleedEffect(bc);
						from.SendMessage("Votre cible commence à saigner !");
						receveur.SendMessage("Vous commencez à saigner !");
					}
				}

				// Logique pour déranger le sort
				if (receveur.Spell != null)
				{
					int spellDisruptChance = (int)((snooping + stealing) / 15) * 2;
					if (receveur is PlayerMobile)
						spellDisruptChance -= (int)(receveur.Skills[SkillName.Wrestling].Value * 0.15);

					if (spellDisruptChance > Utility.Random(100))
					{
						receveur.Spell.OnCasterHurt();
						from.SendMessage("Vous dérangez votre cible !");
						Animation(from, receveur);
					}
				}

				// Logique pour voler ou faire tomber l'arme
				Item handOne = receveur.FindItemOnLayer(Layer.OneHanded);
				Item handTwo = receveur.FindItemOnLayer(Layer.TwoHanded);

				if (handTwo != null && handTwo is BaseArmor)
					handTwo = null;

				int chance_de_voler = (int)((snooping + stealing) / 20) * 2;
				int chance = (int)((snooping + stealing) / 15) * 2;

				if (handOne != null && handTwo == null)
				{
					if (chance_de_voler > Utility.Random(100))
					{
						if (from.AddToBackpack(handOne))
							from.SendMessage("Vous volez l'arme de votre cible !");
						else
						{
							handOne.MoveToWorld(from.Location);
							from.SendMessage("Vous faites tomber l'arme de votre cible !");
						}
						Animation(from, receveur);
					}
					else if (chance > Utility.Random(100))
					{
						handOne.MoveToWorld(receveur.Location);
						from.SendMessage("Vous faites tomber l'arme de votre cible !");
						Animation(from, receveur);
					}
					else
						from.SendMessage("Vous échouez à voler ou à faire tomber l'arme de votre cible !");
				}
				else if (handOne == null && handTwo != null)
				{
					if (chance > Utility.Random(100))
					{
						handTwo.MoveToWorld(receveur.Location);
						from.SendMessage("Vous faites tomber l'arme de votre cible !");
						Animation(from, receveur);
					}
					else
						from.SendMessage("Vous échouez à faire tomber l'arme de votre cible !");
				}
				else
				{
					from.SendMessage("Vous ne parvenez pas à fouetter cette cible!");
					receveur.PlaySound(0x238);
				}

				// Appliquer les dégâts
				if (from is PlayerMobile)
					AOS.Damage(receveur, from, 10, 100, 0, 0, 0, 0);

				// Animation finale
				Animation(from, receveur);
			}
		}


		private void ApplyBleedEffect(BaseCreature creature)
		{
			Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromSeconds(2), BleedDuration / 2, () =>
			{
				if (!creature.Deleted && creature.Alive)
				{
					creature.Damage(Utility.RandomMinMax(1, 5));
					creature.PlaySound(0x133);
					creature.FixedParticles(0x377A, 244, 25, 9950, 31, 0, EffectLayer.Waist);
				}
			});
		}

		public void Animation(Mobile from, Mobile receveur)
		{
			if (receveur is PlayerMobile)
			{
				PlayerMobile rec = (PlayerMobile)receveur;

			}

			receveur.PlaySound(0x145);
			receveur.SendMessage("Quelque chose vous heurte !");
			from.SendMessage("Vous fouetez votre cible !");
			receveur.RevealingAction();

			if (!receveur.Mounted && receveur.Body.IsHuman)
				receveur.Animate(20, 5, 1, true, false, 0);
		}

		public override void OnAosSingleClick(Mobile from)
		{
			LabelTo(from, Name);
			LabelTo(from, String.Format("[{0} mètres]", m_MaxRange));
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!from.CanBeginAction(typeof(Fouet)))
			{
				from.SendMessage("Vous devez attendre avant d'utiliser le fouet à nouveau.");
			}
			else if (from.Items.Contains(this))
			{
				from.Target = new TargetSystem(this);
				from.SendMessage("Qui voulez-vous fouetter ?");
			}
			else
				from.SendMessage("Vous devez avoir le fouet en main pour l'utiliser.");
		}

		private void Fouet_OnTick(object state)
		{
			Mobile from = (Mobile)state;

			if (from != null)
			{
				from.EndAction(typeof(Fouet));
			}
		}

		private class TargetSystem : Target
		{
			private Fouet m_Fouet;

			public TargetSystem(Fouet fouet) : base(12, false, TargetFlags.None)
			{
				m_Fouet = fouet;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (!from.CanBeginAction(typeof(Fouet)))
				{
					from.SendMessage("Vous devez attendre avant d'utiliser le fouet à nouveau.");
				}
				else if (!from.Items.Contains(m_Fouet))
				{
					from.SendMessage("Vous devez avoir le fouet en main pour l'utiliser.");
				}
				else if (targeted is Mobile)
				{
					Mobile m = (Mobile)targeted;

					if (m == from)
					{
						from.SendMessage("Vous ne pouvez pas vous foueter.");
					}
					else if (!from.InRange(m.Location, m_Fouet.MaxRange))
					{
						from.SendMessage("Votre fouet n'est pas assez long.");
					}
					else if (from is BaseCreature && !(from is BaseHire))
					{
						from.SendMessage("Vous ne pouvez pas fouetter cette cible.");
					}
					else
					{
						m_Fouet.Fouetage(from, m);

						if (!from.Mounted)
							from.Animate(9, 5, 1, true, false, 0);
						else
							from.Animate(26, 5, 1, true, false, 0);

						from.BeginAction(typeof(Fouet));

						double bonus = 0;


						Timer.DelayCall(TimeSpan.FromSeconds(12.0 - bonus), new TimerStateCallback(m_Fouet.Fouet_OnTick), from);
					}
				}
				else
				{
					from.SendMessage("Vous ne pouvez pas foueter cette cible.");
				}
			}
		}
	}
}