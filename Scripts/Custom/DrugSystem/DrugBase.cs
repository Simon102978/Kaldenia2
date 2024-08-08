using System;
using System.Collections.Generic;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public enum DrugPlantType
	{
		Shimyshisha,
		Shenyr,
		Amaesha,
		Astishys,
		Gwelalith,
		Frilar,
		Thomahar,
		Thiseth,
		Etherawin,
		Eralirid
	}

	public enum DrugType
	{
		Legere,
		Medium,
		Forte
	}

	public class DrugBase : Item
	{
		private DrugPlantType m_DrugPlant;
		private DrugType m_Type;

		[CommandProperty(AccessLevel.GameMaster)]
		public DrugPlantType DrugPlant
		{
			get { return m_DrugPlant; }
			set { m_DrugPlant = value; InvalidateProperties(); }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public DrugType Type
		{
			get { return m_Type; }
			set { m_Type = value; InvalidateProperties(); }
		}

		[Constructable]
		public DrugBase() : this(DrugPlantType.Shimyshisha, DrugType.Legere) { }

		[Constructable]
		public DrugBase(DrugPlantType drugPlant, DrugType type) : base(0x423A)
		{
			Stackable = true;
			Amount = 1;
			Weight = 0.1;
			m_DrugPlant = drugPlant;
			m_Type = type;
			Name = $"Drogue de {drugPlant}";
			Hue = GetHueForDrugType(drugPlant);
		}

		private int GetHueForDrugType(DrugPlantType type)
		{
			switch (type)
			{
				case DrugPlantType.Shimyshisha: return 0x7B8;
				case DrugPlantType.Shenyr: return 0x7C1;
				case DrugPlantType.Amaesha: return 0x7D0;
				case DrugPlantType.Astishys: return 0x7D9;
				case DrugPlantType.Gwelalith: return 0x7E2;
				case DrugPlantType.Frilar: return 0x7EB;
				case DrugPlantType.Thomahar: return 0x7F4;
				case DrugPlantType.Thiseth: return 0x7FD;
				case DrugPlantType.Etherawin: return 0x806;
				case DrugPlantType.Eralirid: return 0x80F;
				default: return 0;
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add($"Type: {m_Type}");
			list.Add($"Plante: {m_DrugPlant}");
		}

		public DrugBase(Serial serial) : base(serial) { }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
			writer.Write((int)m_DrugPlant);
			writer.Write((int)m_Type);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_DrugPlant = (DrugPlantType)reader.ReadInt();
			m_Type = (DrugType)reader.ReadInt();
		}

		public override void OnDoubleClick(Mobile from)
		{
			CustomPlayerMobile pm = from as CustomPlayerMobile;
			if (pm == null)
				return;

			if (pm.Hallucinating)
			{
				pm.SendMessage("Vous êtes déjà sous l'effet d'une drogue.");
				return;
			}

			this.Consume();
			pm.Hallucinating = true;
			pm.SendMessage("Vous consommez la drogue et sentez ses effets");

			ApplyDrugEffect(pm);

			int duration = 1 + (int)m_DrugPlant + (int)m_Type * 2;
			Timer.DelayCall(TimeSpan.FromMinutes(duration), () =>
			{
				pm.Hallucinating = false;
				pm.SendMessage("Les effets de la drogue se dissipent.");
			});

			pm.PlaySound(pm.Female ? 781 : 1052);
			pm.Emote("*consomme*");
			if (!pm.Mounted)
				pm.Animate(34, 5, 1, true, false, 0);
		}

		private void ApplyDrugEffect(CustomPlayerMobile pm)
		{
			switch (m_DrugPlant)
			{
				case DrugPlantType.Shimyshisha:
					ApplyShimyshishaEffect(pm);
					break;
				case DrugPlantType.Shenyr:
					ApplyShenyrEffect(pm);
					break;
				case DrugPlantType.Amaesha:
					ApplyAmaeshaEffect(pm);
					break;
				case DrugPlantType.Astishys:
					ApplyAstishysEffect(pm);
					break;
				case DrugPlantType.Gwelalith:
					ApplyGwelalithEffect(pm);
					break;
				case DrugPlantType.Frilar:
					ApplyFrilarEffect(pm);
					break;
				case DrugPlantType.Thomahar:
					ApplyThomaharEffect(pm);
					break;
				case DrugPlantType.Thiseth:
					ApplyThisethEffect(pm);
					break;
				case DrugPlantType.Etherawin:
					ApplyEtherawinEffect(pm);
					break;
				case DrugPlantType.Eralirid:
					ApplyEraliridEffect(pm);
					break;
				default:
					pm.SendMessage("Cette drogue n'a aucun effet connu.");
					break;
			}
			ApplyIntensityEffect(pm);
		}

		private void ApplyIntensityEffect(CustomPlayerMobile pm)
		{
			switch (m_Type)
			{
				case DrugType.Legere:
					ApplyLightEffect(pm);
					break;
				case DrugType.Medium:
					ApplyMediumEffect(pm);
					break;
				case DrugType.Forte:
					ApplyStrongEffect(pm);
					break;
			}
		}

		private void ApplyLightEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Vous ressentez un léger étourdissement et une envie de sauter.");

			int jumps = Utility.RandomMinMax(3, 4);
			Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromSeconds(1), jumps, () =>
			{
				if (pm.Alive)
				{
					pm.Animate(AnimationType.Attack, 0);
					pm.PlaySound(pm.Female ? 0x32F : 0x441);
					pm.Z += 20;
					Timer.DelayCall(TimeSpan.FromMilliseconds(250), () =>
					{
						pm.Z -= 20;
						pm.Stam = Math.Max(0, pm.Stam - 5);
					});
				}
			});
		}

		private void ApplyMediumEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Votre estomac se retourne et vous avez envie de vomir.");

			// Animation de vomissement
			pm.Animate(32, 5, 1, true, false, 0);

			// Créer une tache de vomissement
			Item vomitPool = new Static(0x122A);
			vomitPool.Name = "flaque de vomissement";
			vomitPool.Hue = 0x8A5; // Couleur jaunâtre
			vomitPool.Movable = false;
			vomitPool.MoveToWorld(pm.Location, pm.Map);

			// Faire disparaître la tache après un certain temps
			Timer.DelayCall(TimeSpan.FromMinutes(2), () =>
			{
				vomitPool.Delete();
			});
		}

		private void ApplyStrongEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Vous perdez le contrôle total de votre corps.");

			// Geler le joueur
			pm.Frozen = true;

			// Rendre le joueur invisible
			pm.Hidden = true;

			// Mettre le corps du joueur au sol
			pm.Animate(21, 5, 1, true, false, 0);

			// Programmer la fin des effets
			Timer.DelayCall(TimeSpan.FromMinutes(2), () =>
			{
				pm.Frozen = false;
				pm.Hidden = false;
				pm.Animate(22, 5, 1, true, false, 0); // Animation pour se relever
				pm.SendMessage("Vous reprenez lentement le contrôle de votre corps.");
			});
		}
		private void ApplyShimyshishaEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Vous vous sentez désorienté et confus.");
			ChangePlayerZPosition(pm);
			ApplyRandomParticleEffect(pm);
		}

		private void ApplyShenyrEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Votre vision se trouble et vous voyez des choses étranges.");
			ChangeNearbyItemsAppearance(pm);
			ShowHallucinationGump(pm);
		}

		private void ApplyAmaeshaEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Vous ressentez une douleur intense et votre corps tremble.");
			pm.Hits -= 20;
			ApplyRandomMovement(pm);
		}

		private void ApplyAstishysEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Vous vous sentez léger et flottant.");
			ChangePlayerZPosition(pm);
			ApplyRandomParticleEffect(pm);
		}

		private void ApplyGwelalithEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Votre esprit s'embrouille et vous perdez le sens de la réalité.");
			ShowHallucinationGump(pm);
			SendFakeMessage(pm);
		}

		private void ApplyFrilarEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Vous ressentez une intense paranoïa.");
			ChangePlayerGender(pm); 
			ChangeEnvironment(pm);
		}

		private void ApplyThomaharEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Vos sens s'intensifient de manière incontrôlable.");
			ApplyHallucinationEffects(pm);
			ModifyPlayerStats(pm);
		}

		private void ApplyThisethEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Vous perdez le contrôle de votre corps.");
			ApplyRandomMovement(pm);
			ChangePlayerAppearance(pm);
		}

		private void ApplyEtherawinEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Vous vous sentez détaché de la réalité.");
			ApplyDeathScreen(pm);
			ChangeEnvironment(pm);
		}

		private void ApplyEraliridEffect(CustomPlayerMobile pm)
		{
			pm.SendMessage("Votre corps semble se désintégrer.");
			ApplyRandomParticleEffect(pm);
			ChangePlayerAppearance(pm);
		}


		private static void ChangePlayerZPosition(CustomPlayerMobile pm)
		{
			int originalZ = pm.Z;
			int newZ = originalZ + Utility.RandomMinMax(-10, 10);

			// Vérifiez que le nouveau Z est valide
			if (newZ >= pm.Map.GetAverageZ(pm.X, pm.Y))
			{
				pm.Z = newZ;
				pm.SendMessage("Vous vous sentez " + (newZ > originalZ ? "flotter" : "couler") + "...");

				Timer.DelayCall(TimeSpan.FromSeconds(5), () =>
				{
					pm.Z = originalZ;
					pm.SendMessage("Vous revenez à votre position normale.");
				});
			}
			else
			{
				pm.SendMessage("Vous ressentez une étrange sensation de flottement.");
			}
		}

		private static void ApplyDeathScreen(CustomPlayerMobile pm)
		{
			pm.Send(new ToggleSpecialAbility(0x3EA, true));

			Timer.DelayCall(TimeSpan.FromSeconds(5), () =>
			{
				pm.Send(new ToggleSpecialAbility(0x3EA, false));
			});
		}

		private static void ChangeNearbyItemsAppearance(CustomPlayerMobile pm)
		{
			foreach (Item item in pm.GetItemsInRange(10))
			{
				if (item.Visible)
				{
					item.PublicOverheadMessage(MessageType.Regular, Utility.Random(2, 1200), false, item.Name);
				}
			}
		}

		private static void ApplyRandomParticleEffect(CustomPlayerMobile pm)
		{
			pm.FixedParticles(Utility.Random(0x373A, 0x377A), 10, 15, 5018, EffectLayer.Waist);
		}

		private static void PlayRandomHallucinationSound(CustomPlayerMobile pm)
		{
			pm.PlaySound(Utility.Random(0x1E4, 0x1E7));
		}

		private static void ModifyPlayerStats(CustomPlayerMobile pm)
		{
			pm.AddStatMod(new StatMod(StatType.Str, "DrugStr", Utility.RandomMinMax(-20, 21), TimeSpan.FromMinutes(1)));
			pm.AddStatMod(new StatMod(StatType.Dex, "DrugDex", Utility.RandomMinMax(-20, 21), TimeSpan.FromMinutes(1)));
			pm.AddStatMod(new StatMod(StatType.Int, "DrugInt", Utility.RandomMinMax(-20, 21), TimeSpan.FromMinutes(1)));
		}

		private static void ApplyRandomMovement(CustomPlayerMobile pm)
		{
			pm.Direction = (Direction)Utility.Random(8);
			pm.Move(pm.Direction);
		}

		private static void ChangePlayerGender(CustomPlayerMobile pm)
		{
			int originalBodyValue = pm.BodyValue;
			bool originalGender = pm.Female;

			// Changer le sexe et le corps
			pm.Female = !pm.Female;
			pm.BodyValue = pm.Female ? 401 : 400;

			pm.SendMessage("Vous vous sentez étrangement différent...");

			// Planifier le retour à l'état original
			Timer.DelayCall(TimeSpan.FromSeconds(30), () =>
			{
				pm.Female = originalGender;
				pm.BodyValue = originalBodyValue;
				pm.SendMessage("Vous vous sentez redevenir vous-même.");
			});
		}


		private static void ChangePlayerAppearance(CustomPlayerMobile pm)
		{
			pm.BodyMod = Utility.Random(400, 501);
			Timer.DelayCall(TimeSpan.FromSeconds(10), () => pm.BodyMod = 0);
		}

		private static void SendFakeMessage(CustomPlayerMobile pm)
		{
			string[] fakeMessages = new string[]
			{
				"Vous entendez des voix étranges...",
				"Le sol semble bouger sous vos pieds...",
				"Les couleurs semblent plus vives que d'habitude...",
				"Vous avez l'impression que quelqu'un vous observe...",
				"Le temps semble ralentir..."
			};
			pm.SendMessage(Utility.Random(0, 1201), fakeMessages[Utility.Random(fakeMessages.Length)]);
		}

		private static void ShowHallucinationGump(CustomPlayerMobile pm)
		{
			pm.CloseGump(typeof(HallucinationGump));
			pm.SendGump(new HallucinationGump());
		}

		private static void ChangeEnvironment(CustomPlayerMobile pm)
		{
			// Changement de luminosité personnelle
			int newLightLevel = Utility.RandomMinMax(0, 25);
			pm.NetState.Send(new PersonalLightLevel(pm, newLightLevel));

			// Changement de saison
			int newSeason = Utility.Random(5); // 0-4: printemps, été, automne, hiver, désert
			pm.NetState.Send(new SeasonChange(newSeason));

		
			// Planifier le retour à la normale
			Timer.DelayCall(TimeSpan.FromSeconds(30), () =>
			{
				pm.NetState.Send(new PersonalLightLevel(pm, -1)); // -1 pour réinitialiser à la valeur par défaut
				pm.NetState.Send(new SeasonChange((int)pm.Map.Season));
			});

			// Effet de tremblement de l'écran (si disponible dans votre version)
			if (pm.NetState.SupportsExpansion(Expansion.SA))
			{
				pm.NetState.Send(new ScreenEffect(ScreenEffectType.LightFlash));
			}
		}

		private static void ApplyHallucinationEffects(CustomPlayerMobile pm)
		{
			// Effet de distorsion de l'écran
			pm.NetState.Send(new ScreenEffect(ScreenEffectType.FadeIn));

			pm.NetState.Send(new ScreenEffect(ScreenEffectType.DarkFlash));

			// Effet de flou
			pm.NetState.Send(new ScreenEffect(ScreenEffectType.FadeOut));

			// Son d'ambiance aléatoire
			int[] ambientSounds = { 0x0, 0x1, 0x2, 0x14, 0x15, 0x16 }; // Ajoutez d'autres sons si nécessaire
			pm.NetState.Send(new PlaySound(ambientSounds[Utility.Random(ambientSounds.Length)], pm.Location));

			// Message hallucinatoire
			pm.SendMessage(Utility.RandomMinMax(0, 1200), "Le monde autour de vous semble se déformer...");
		}



		private class HallucinationGump : Gump
		{
			public HallucinationGump() : base(80, 80)
			{
				AddPage(0);
				AddBackground(0, 0, 200, 200, 9200);
				AddHtml(20, 20, 160, 160, "<center>Vous voyez des formes étranges danser devant vos yeux...</center>", false, false);
			}
		}

		private class GhostNPC : BaseCreature
		{
			public GhostNPC() : base(AIType.AI_Melee, FightMode.None, 10, 1, 0.2, 0.4)
			{
				Name = "Fantôme hallucinatoire";
				Body = 402;
				BaseSoundID = 0x482;

				SetStr(196, 225);
				SetDex(196, 225);
				SetInt(196, 225);

				SetHits(118, 135);

				SetDamage(8, 10);

				SetDamageType(ResistanceType.Physical, 100);

				SetResistance(ResistanceType.Physical, 35, 40);
				SetResistance(ResistanceType.Fire, 20, 30);
				SetResistance(ResistanceType.Cold, 50, 60);
				SetResistance(ResistanceType.Poison, 20, 30);
				SetResistance(ResistanceType.Energy, 30, 40);

				SetSkill(SkillName.MagicResist, 85.1, 100.0);
				SetSkill(SkillName.Tactics, 75.1, 90.0);
				SetSkill(SkillName.Wrestling, 80.1, 100.0);

				Fame = 4000;
				Karma = -4000;

				VirtualArmor = 38;

				Hue = -1;
			}

			public GhostNPC(Serial serial) : base(serial)
			{
			}

			public override void Serialize(GenericWriter writer)
			{
				base.Serialize(writer);
				writer.Write((int)0);
			}

			public override void Deserialize(GenericReader reader)
			{
				base.Deserialize(reader);
				int version = reader.ReadInt();
			}
		}
	}
}
