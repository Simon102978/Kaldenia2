using System;
using Server.Items;
using Server.Mobiles;
using Server.Engines.CannedEvil;

namespace Server.Engines.Craft
{
	public class DefRuneCrafting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get{ return SkillName.Inscribe; }
		}


		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefRuneCrafting();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefRuneCrafting() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
		{
		}



		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x1F5 ); // magic

			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 9, 3, 1, true, false, 0 );

			//new InternalTimer( from ).Start();
		}

		// Delay to synchronize the sound with the hit on the anvil
		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 0.7 ) )
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.PlaySound( 0x2A );
			}
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				from.PlaySound( 65 ); // rune breaking
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				//from.PlaySound( 65 ); // rune breaking
				//if ( quality == 0 )
					//return 502785; // You were barely able to make this item.  It's quality is below average.
				//else if ( makersMark && quality == 2 )
					//return 1044156; // You create an exceptional quality item and affix your maker's mark.
				//else if ( quality == 2 )
					//return 1044155; // You create an exceptional quality item.
				//else				
					return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{

			#region Autres
			int index = AddCraft( typeof( BlankRune ), "Autres", "Rune Vierge", 45.0, 80.0, typeof(BlankScroll), "Parchemin Vierge", 10, "Vous n'avez pas suffisament de Parchemin Vierge");

			index = AddCraft(typeof(DisenchantRune), "Autres", "Rune de désenchantement", 45.0, 80.0, typeof(BlankScroll), "Parchemin Vierge", 10, "Vous n'avez pas suffisament de Parchemin Vierge");
			AddRes(index, typeof(Gold), "Or", 500, "Il vous manquent des pièces d'or.");
			#endregion

			#region Bijoux

			index = AddCraft(typeof(BonusDexRune), "Bijoux", "Dexterité", 85.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDex), "Sang Envouté Dexterité", 3, "Il vous manquent de sang envouté dexterité.");
			index = AddCraft(typeof(BonusIntRune), "Bijoux", "Intelligence", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteInt), "Sang Envouté Intelligence", 3, "Il vous manquent de sang envouté intelligence.");
			index = AddCraft(typeof(BonusStrRune), "Bijoux", "Force", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteForce), "Sang Envouté Force", 3, "Il vous manquent de sang envouté Force.");


			index = AddCraft(typeof(BonusHitRune), "Bijoux", "Vie", 85.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteWyvern), "Sang Envouté de Wvyern", 3, "Il vous manquent de sang envouté de Wvyern.");

			index = AddCraft(typeof(BonusManaRune), "Bijoux", "Mana", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteCentaur), "Sang Envouté de Centaur", 3, "Il vous manquent de sang envouté de Centaur.");

			index = AddCraft(typeof(BonusStamRune), "Bijoux", "Stamina", 80.5, 110.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteVegetal), "Sang Envouté Végétal", 3, "Il vous manquent de sang envouté Végétal.");

			index = AddCraft( typeof( RegenHitsRune ), "Bijoux", "Regeneration de vie", 80.5, 110.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDrake), "Sang Envouté de Drake", 3, "Il vous manquent de sang envouté de Drake.");

			index = AddCraft( typeof( RegenManaRune ), "Bijoux", "Regeneration de mana", 75.5, 115.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDragon), "Sang Envouté Dragon", 3, "Il vous manquent de sang envouté Dragon.");

			index = AddCraft( typeof( RegenStamRune ), "Bijoux", "Regeneration de stamina", 70.5, 99.5, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteSatyr), "Sang Envouté de Satyr", 3, "Il vous manquent de sang envouté de Satyr.");

			index = AddCraft(typeof(LowerManaCostRune), "Bijoux", "Reduction de mana", 75.5, 115.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteWyvern), "Sang Envouté de Wvyern", 3, "Il vous manquent de sang envouté de Wvyern.");

			index = AddCraft(typeof(LowerRegCostRune), "Bijoux", "Reduction de reactifs", 75.5, 115.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteCentaur), "Sang Envouté de Centaur", 3, "Il vous manquent de sang envouté de Centaur.");

			index = AddCraft(typeof(LowerAmmoCostRune), "Bijoux", "Reduction des munitions", 75.5, 115.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteCentaur), "Sang Envouté de Centaur", 3, "Il vous manquent de sang envouté de Centaur.");
			#endregion


			#region Armes

			index = AddCraft(typeof(BonusDexRune), "Armes", "Dexterité", 85.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDex), "Sang Envouté Dexterité", 3, "Il vous manquent de sang envouté dexterité.");
			index = AddCraft(typeof(BonusIntRune), "Armes", "Intelligence", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteInt), "Sang Envouté Intelligence", 3, "Il vous manquent de sang envouté intelligence.");
			index = AddCraft(typeof(BonusStrRune), "Armes", "Force", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteForce), "Sang Envouté Force", 3, "Il vous manquent de sang envouté Force.");


			index = AddCraft(typeof(BonusHitRune), "Armes", "Vie", 85.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteWyvern), "Sang Envouté de Wvyern", 3, "Il vous manquent de sang envouté de Wvyern.");

			index = AddCraft(typeof(BonusManaRune), "Armes", "Mana", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteCentaur), "Sang Envouté de Centaur", 3, "Il vous manquent de sang envouté de Centaur.");

			index = AddCraft(typeof(BonusStamRune), "Armes", "Stamina", 80.5, 110.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteVegetal), "Sang Envouté Végétal", 3, "Il vous manquent de sang envouté Végétal.");

			index = AddCraft( typeof( HitLeechLifeRune ), "Armes", "Vol de vie", 85.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteWyvern), "Sang Envouté de Wvyern", 3, "Il vous manquent de sang envouté de Wvyern.");
			index = AddCraft( typeof( HitLeechManaRune ), "Armes", "Vol de mana", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteCentaur), "Sang Envouté de Centaur", 3, "Il vous manquent de sang envouté de Centaur.");
			index = AddCraft( typeof( HitLeechStamRune ), "Armes", "Vol de stamina", 75.5, 115.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDrake), "Sang Envouté de Drake", 3, "Il vous manquent de sang envouté de Drake.");

			index = AddCraft( typeof( UseBestWepRune ), "Armes", "Meilleur arme", 65.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteLezard), "Sang Envouté Lezard", 3, "Il vous manquent de sang envouté de Lezard.");
			index = AddCraft( typeof( DamageIncRune ), "Armes", "Degat", 75.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteLezard), "Sang Envouté Lezard", 3, "Il vous manquent de sang envouté de Lezard.");
			index = AddCraft( typeof( SwingSpeedRune ), "Armes", "Vitesse de frappe", 75.5, 115.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteLezard), "Sang Envouté Lezard", 3, "Il vous manquent de sang envouté de Lezard.");

			index = AddCraft(typeof(CastSpeedRune), "Armes", "Vitesse des sorts", 75.5, 115.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDrake), "Sang Envouté de Drake", 3, "Il vous manquent de sang envouté de Drake.");

			index = AddCraft(typeof(CastRecoveryRune), "Armes", "Recuperation après un sort", 75.5, 115.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteSatyr), "Sang Envouté de Satyr", 3, "Il vous manquent de sang envouté de Satyr.");

			index = AddCraft(typeof(AttackChanceRune), "Armes", "Chance de touche", 75.5, 115.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDragon), "Sang Envouté Dragon", 3, "Il vous manquent de sang envouté Dragon.");

			index = AddCraft(typeof(SpellDamageRune), "Armes", "Puissance des sorts", 75.0, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDrake), "Sang Envouté de Drake", 3, "Il vous manquent de sang envouté de Drake.");

	//		index = AddCraft(typeof(SpellChannelRune), "Armes", "Canalisation Magique", 75.0, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
	//		AddRes(index, typeof(SangEnvouteDragon), "Sang Envouté Dragon", 3, "Il vous manquent de sang envouté Dragon.");

			index = AddCraft(typeof(SelfRepairRune), "Armes", "Auto-Réparation", 65.5, 95.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteLezard), "Sang Envouté Lezard", 3, "Il vous manquent de sang envouté de Lezard.");

			#endregion

			#region Livre de Sorts

			index = AddCraft(typeof(CastSpeedRune), "Livre de Sorts", "Vitesse des sorts", 75.5, 115.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDrake), "Sang Envouté de Drake", 3, "Il vous manquent de sang envouté de Drake.");

			index = AddCraft(typeof(CastRecoveryRune), "Livre de Sorts", "Recuperation après un sort", 75.5, 115.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteSatyr), "Sang Envouté de Satyr", 3, "Il vous manquent de sang envouté de Satyr.");

			index = AddCraft(typeof(SpellDamageRune), "Livre de Sorts", "Puissance des sorts", 75.0, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDrake), "Sang Envouté de Drake", 3, "Il vous manquent de sang envouté de Drake.");

			#endregion

			#region Armures

			index = AddCraft(typeof(BonusDexRune), "Armures", "Dexterité", 85.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDex), "Sang Envouté Dexterité", 3, "Il vous manquent de sang envouté dexterité.");
			index = AddCraft(typeof(BonusIntRune), "Armures", "Intelligence", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteInt), "Sang Envouté Intelligence", 3, "Il vous manquent de sang envouté intelligence.");
			index = AddCraft(typeof(BonusStrRune), "Armures", "Force", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteForce), "Sang Envouté Force", 3, "Il vous manquent de sang envouté Force.");


			index = AddCraft(typeof(BonusHitRune), "Armures", "Vie", 85.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteWyvern), "Sang Envouté de Wvyern", 3, "Il vous manquent de sang envouté de Wvyern.");

			index = AddCraft(typeof(BonusManaRune), "Armures", "Mana", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteCentaur), "Sang Envouté de Centaur", 3, "Il vous manquent de sang envouté de Centaur.");

			index = AddCraft(typeof(BonusStamRune), "Armures", "Stamina", 80.5, 110.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteVegetal), "Sang Envouté Végétal", 3, "Il vous manquent de sang envouté Végétal.");

			index = AddCraft(typeof(DefenceChanceRune), "Armures", "Defense", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDragon), "Sang Envouté Dragon", 3, "Il vous manquent de sang envouté Dragon.");

			index = AddCraft(typeof(SelfRepairRune), "Armures", "Auto-Réparation", 65.5, 95.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteLezard), "Sang Envouté Lezard", 3, "Il vous manquent de sang envouté de Lezard.");


			index = AddCraft(typeof(ResistColdRune), "Armures", "Resistance au froid", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteFroid), "Sang Envouté Froid", 3, "Il vous manquent de sang envouté Froid.");
			index = AddCraft(typeof(ResistEnergyRune), "Armures", "Resistance à l'énergie", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteEnergie), "Sang Envouté Energie", 3, "Il vous manquent de sang envouté Energie.");
			index = AddCraft(typeof(ResistFireRune), "Armures", "Resistance au feu", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteFeu), "Sang Envouté Feu", 3, "Il vous manquent de sang envouté feu.");
			index = AddCraft(typeof(ResistPhysRune), "Armures", "Resistance physique", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvoutePhysique), "Sang Envouté Physique", 3, "Il vous manquent de sang envouté physique.");
			index = AddCraft(typeof(ResistPoisRune), "Armures", "Resistance au poison", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvoutePoison), "Sang Envouté Poison", 3, "Il vous manquent de sang envouté Force.");
			#endregion


			#region Bouclier

			index = AddCraft(typeof(BonusDexRune), "Bouclier", "Dexterité", 85.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDex), "Sang Envouté Dexterité", 3, "Il vous manquent de sang envouté dexterité.");
			index = AddCraft(typeof(BonusIntRune), "Bouclier", "Intelligence", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteInt), "Sang Envouté Intelligence", 3, "Il vous manquent de sang envouté intelligence.");
			index = AddCraft(typeof(BonusStrRune), "Bouclier", "Force", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteForce), "Sang Envouté Force", 3, "Il vous manquent de sang envouté Force.");


			index = AddCraft(typeof(BonusHitRune), "Bouclier", "Vie", 85.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteWyvern), "Sang Envouté de Wvyern", 3, "Il vous manquent de sang envouté de Wvyern.");

			index = AddCraft(typeof(BonusManaRune), "Bouclier", "Mana", 80.5, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteCentaur), "Sang Envouté de Centaur", 3, "Il vous manquent de sang envouté de Centaur.");

			index = AddCraft(typeof(BonusStamRune), "Bouclier", "Stamina", 80.5, 110.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteVegetal), "Sang Envouté Végétal", 3, "Il vous manquent de sang envouté Végétal.");

			index = AddCraft(typeof(DefenceChanceRune), "Bouclier", "Defense", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteDragon), "Sang Envouté Dragon", 3, "Il vous manquent de sang envouté Dragon.");

		//	index = AddCraft(typeof(SpellChannelRune), "Bouclier", "Canalisation Magique", 75.0, 120.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
		//	AddRes(index, typeof(SangEnvouteDragon), "Sang Envouté Dragon", 3, "Il vous manquent de sang envouté Dragon.");


			index = AddCraft(typeof(ResistColdRune), "Bouclier", "Resistance au froid", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteFroid), "Sang Envouté Froid", 3, "Il vous manquent de sang envouté Froid.");
			index = AddCraft(typeof(ResistEnergyRune), "Bouclier", "Resistance à l'énergie", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteEnergie), "Sang Envouté Energie", 3, "Il vous manquent de sang envouté Energie.");
			index = AddCraft(typeof(ResistFireRune), "Bouclier", "Resistance au feu", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvouteFeu), "Sang Envouté Feu", 3, "Il vous manquent de sang envouté feu.");
			index = AddCraft(typeof(ResistPhysRune), "Bouclier", "Resistance physique", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvoutePhysique), "Sang Envouté Physique", 3, "Il vous manquent de sang envouté physique.");
			index = AddCraft(typeof(ResistPoisRune), "Bouclier", "Resistance au poison", 70.5, 100.0, typeof(BlankRune), "Rune vierge", 1, "Il vous faut une runes vierge.");
			AddRes(index, typeof(SangEnvoutePoison), "Sang Envouté Poison", 3, "Il vous manquent de sang envouté Force.");
			#endregion


			/*		index = AddCraft( typeof( ArachnidSlayer ), "Slayers", "Arachnid Slayer", 120, 120.5, typeof( ChampionSkull ), "Champion Skull's", 25, "You do not have enough Champion Skull's to Craft this." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
						AddRes( index, typeof ( GreenThorns ), "Green Thorn", 10, "You do not have Enough Green Thorns."  );
						AddRes( index, typeof ( OrangePetals ), "Orange Petals", 10, "You do not have enough Orange Petals to make that."  );
					index = AddCraft( typeof( DemonSlayer ), "Slayers", "Demon Slayer", 120, 120.5, typeof( ChampionSkull ), "Champion Skull's", 25, "You do not have enough Champion Skull's to Craft this." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
						AddRes( index, typeof ( GreenThorns ), "Green Thorn", 10, "You do not have Enough Green Thorns."  );
						AddRes( index, typeof ( OrangePetals ), "Orange Petals", 10, "You do not have enough Orange Petals to make that."  );
					index = AddCraft( typeof( ElementalSlayer ), "Slayers", "Elemental Slayer", 120, 120.5, typeof( ChampionSkull ), "Champion Skull's", 25, "You do not have enough Champion Skull's to Craft this." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
						AddRes( index, typeof ( GreenThorns ), "Green Thorn", 10, "You do not have Enough Green Thorns."  );
						AddRes( index, typeof ( OrangePetals ), "Orange Petals", 10, "You do not have enough Orange Petals to make that."  );
					index = AddCraft( typeof( FeySlayer ), "Slayers", "Fey Slayer", 120, 120, typeof( ChampionSkull ), "Champion Skull's", 25, "You do not have enough Champion Skull's to Craft this." );
						AddRes( index, typeof ( RoughStone ), "RoughStone", 1, "You do not have any Rough Stones to make that."  );
						AddRes( index, typeof ( GreenThorns ), "Green Thorn", 10, "You do not have Enough Green Thorns."  );
						AddRes( index, typeof ( OrangePetals ), "Orange Petals", 10, "You do not have enough Orange Petals to make that."  );
					index = AddCraft( typeof( RepondSlayer ), "Slayers", "Repond Slayer", 120, 120.5, typeof( ChampionSkull ), "Champion Skull's", 25, "You do not have enough Champion Skull's to Craft this." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
						AddRes( index, typeof ( GreenThorns ), "Green Thorn", 10, "You do not have Enough Green Thorns."  );
						AddRes( index, typeof ( OrangePetals ), "Orange Petals", 10, "You do not have enough Orange Petals to make that."  );
					index = AddCraft( typeof( ReptileSlayer ), "Slayers", "Reptile Slayer", 120, 120.5, typeof( ChampionSkull ), "Champion Skull's", 25, "You do not have enough Champion Skull's to Craft this." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
						AddRes( index, typeof ( GreenThorns ), "Green Thorn", 10, "You do not have Enough Green Thorns."  );
						AddRes( index, typeof ( OrangePetals ), "Orange Petals", 10, "You do not have enough Orange Petals to make that."  );
					index = AddCraft( typeof( UndeadSlayer ), "Slayers", "Undead Slayer", 120, 120.5, typeof( ChampionSkull ), "Champion Skull's", 25, "You do not have enough Champion Skull's to Craft this." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
						AddRes( index, typeof ( GreenThorns ), "Green Thorn", 10, "You do not have Enough Green Thorns."  );
						AddRes( index, typeof ( OrangePetals ), "Orange Petals", 10, "You do not have enough Orange Petals to make that."  );
					index = AddCraft( typeof( RemoveSlayer ), "Slayers", "Slayer Removal *all* wont break item", 120, 120.5, typeof( ChampionSkull ), "Champion Skull's", 3, "You do not have enough Champion Skull's to Craft this." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
						AddRes( index, typeof ( OilCloth ), "Oil Cloth", 30, "You do not have Enough Oil Cloths."  );
						AddRes( index, typeof ( Sand ), "Sand", 10, "You do not have enough Sand to make that."  );
		*/

			/*		index = AddCraft( typeof( HitColdAreaRune ), "Weapons Area", "Cold Area", 70.5, 105.0, typeof( BlackPearl ), "Black Pearl", 3, 1044361 );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
					index = AddCraft( typeof( HitEnergyAreaRune ), "Weapons Area", "Energy Area", 70.5, 100.0, typeof( PigIron ), "Pig Iron", 3, "You do not have enough pig iron to make that." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
					index = AddCraft( typeof( HitFireAreaRune ), "Weapons Area", "Fire Area", 70.5, 100.0, typeof( SulfurousAsh ), "Sulfurous Ash", 3, 1044367 );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
					index = AddCraft( typeof( HitPhysAreaRune ), "Weapons Area", "Physical Area", 70.5, 100.0, typeof( SpidersSilk ), "Spiders Silk", 3, 1044368 );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
					index = AddCraft( typeof( HitPoisonAreaRune ), "Weapons Area", "Poison Area", 70.5, 100.0, typeof( NoxCrystal ), "Nox Crystal", 3, "You do not have enough nox crystal to make that." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );

					index = AddCraft( typeof( HitDispelRune ), "Weapons Hit", "Hit Dispel", 60.5, 100.0, typeof( BatWing ), "Bat Wing", 3, "You do not have enough bat wing to make that." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
					index = AddCraft( typeof( HitLowerAttackRune ), "Weapons Hit", "Hit Lower Attack", 75.5, 102.0, typeof( DaemonBlood ), "Daemon Blood", 3, "You do not have enough daemon blood to make that." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
					index = AddCraft( typeof( HitLowerDefenseRune ), "Weapons Hit", "Hit Lower Defense", 65.5, 105.0, typeof( GraveDust ), "Grave Dust", 3, "You do not have enough grave dust to make that." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
					index = AddCraft( typeof( HitFireBallRune ), "Weapons Hit", "Hit Fireball", 60.5, 95.0, typeof( SulfurousAsh ), "Sulfurous Ash", 3, 1044367 );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
					index = AddCraft( typeof( HitHarmRune ), "Weapons Hit", "Hit Harm", 60.5, 95.0, typeof( BatWing ), "Bat Wing", 3, "You do not have enough bat wing to make that." );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
					index = AddCraft( typeof( HitLightningRune ), "Weapons Hit", "Hit Lightning", 60.5, 100.0, typeof( Garlic ), "Garlic", 3, "You do not have enough Garlic to make that" );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
					index = AddCraft( typeof( HitMagicArrowRune ), "Weapons Hit", "Hit Magic Arrow", 32.5, 100.0, typeof( BlackPearl ), "Black Pearl", 3, 1044361 );
						AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  ); */


			/*			index = AddCraft( typeof( EnhancePotsRune ), "Misc", "Enhance Potions", 30.0, 80.5, typeof( BlackPearl ), "Black Pearl", 3, 1044361 );
							AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
						index = AddCraft( typeof( LuckRune ), "Misc", "Luck", 30.5, 90.0, typeof( Garlic), "Garlic", 3, "You do not have enough Garlic to make that" );
							AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
						index = AddCraft( typeof( NightSightRune ), "Misc", "Night Sight", 30.5, 90.0, typeof( SpidersSilk ), "Spiders Silk", 3, 1044368 );
							AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );
						index = AddCraft( typeof( MageArmorRune ), "Misc", "Mage Armor/wep", 60.0, 95.0, typeof( Garlic ), "Garlic", 3, "You do not have enough Garlic to make that." );
							AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );*/

			/*			index = AddCraft( typeof( DurabilityBonusRune ), "Misc", "Fortifying rune / Durability Inc", 97.5, 100.0, typeof( ValoriteIngot ), "Valorite Ingots", 100, "You do not have enough Valorite Ingots to make that." );
					AddRes( index, typeof ( RoughStone ), "Rough Stone", 3, "You do not have any Rough Stones to make that."  );
				index = AddCraft( typeof( RepairItem ), "Misc", "Item Repair Rune Wont Break Item", 97.5, 100.0, typeof( ValoriteIngot ), "Valorite Ingots", 30, "You do not have enough Valorite Ingots to make that." );
					AddRes( index, typeof ( RoughStone ), "Rough Stone", 10, "You do not have any Rough Stones to make that."  );
					AddRes( index, typeof ( Lockpick ), "lockpick", 40, "You do not have enough Lock Picks to make that."  );


				index = AddCraft( typeof( ReflectPhysRune ), "Resistances / Reflect", "Reflect Physical Damage", 70.5, 105.0, typeof( PigIron ), "Pig Iron", 3, "You do not have enough pig iron to make that." );
					AddRes( index, typeof ( RoughStone ), "Rough Stone", 1, "You do not have any Rough Stones to make that."  );*/

		}

		public override int CanCraft(Mobile from, ITool tool, Type itemType)
		{
			int num = 0;

			if (tool == null || tool.Deleted || tool.UsesRemaining <= 0)
				return 1044038; // You have worn out your tool!
			else if (!tool.CheckAccessible(from, ref num))
				return num; // The tool must be on your person to use.

			return 0;
		}
	}
}

