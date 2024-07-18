using System;
using Server.Items;
using Server.Commands;
using Server.Custom.Misc;
using System.Text;
using Server.Spells;
using Server.Spells.Chivalry;
using Server.Spells.Necromancy;
using Server.Spells.Spellweaving;

namespace Server.Custom.Weapons
{
	class DPSCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register("DPS", AccessLevel.Player, new CommandEventHandler(DPS_OnCommand));
		}

		[Usage("DPS")]
		[Description("Permet de connaitre le Damage Par Second d'une arme en tenant compte des buffs.")]
		public static void DPS_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;

			double dps = NewWeaponInfo.GetDPS((BaseWeapon)from.Weapon, from);
			from.SendMessage(HueManager.GetHue(HueManagerList.Blue), "DPS: {0}", dps.ToString("0.00"));
		}
	}

	class NewWeaponInfo
	{
		public static double GetDPS(BaseWeapon weapon, Mobile from)
		{
			double dps = 0.0;

			if (weapon != null)
			{
				var minDamage = weapon.MinDamage;
				var maxDamage = weapon.MaxDamage;
				var speed = weapon.Speed;
				double dSpeed = Convert.ToDouble(speed.ToString());

				// Appliquer les modificateurs de dégâts des buffs
				double damageModifier = GetDamageModifier(from);

				dps = ((double)minDamage + (double)maxDamage) / 2.0 * damageModifier * 1.0 / dSpeed;
			}

			return dps;
		}

		private static double GetDamageModifier(Mobile from)
		{
			double modifier = 1.0;

			// Vérifier les buffs et appliquer les modificateurs
			if (DivineFurySpell.UnderEffect(from))
				modifier *= 1.1; // +10% de dégâts

			if (EnemyOfOneSpell.UnderEffect(from))
			{
				var context = EnemyOfOneSpell.GetContext(from);
				if (context != null)
					modifier *= (1 + (context.DamageScalar / 100.0));
			}

			if (BloodOathSpell.GetBloodOath(from) != null)
				modifier *= 1.1; // +10% de dégâts

			if (EssenceOfWindSpell.IsDebuffed(from))
			{
				// Notez que Essence of Wind est en fait un debuff qui réduit les dégâts
				// Nous allons supposer une réduction de 5% par niveau de Focus
				int focusLevel = EssenceOfWindSpell.GetFCMalus(from) - 1; // Le malus FC est FocusLevel + 1
				modifier *= (1 - (0.05 * focusLevel));
			}

			// Ajoutez d'autres vérifications de buffs ici

			return modifier;
		}

		//public static float GetSpeed(BaseWeapon weapon)
		//{
		//	var speed = 5.00f;

		//	if (weapon is Fists)
		//		speed = 2.0f;
		//	else if (weapon is BaseRanged)
		//		speed = 3.25f;
		//	else if (weapon.Layer == Layer.OneHanded)
		//		speed = 2.5f;
		//	else if (weapon.Layer == Layer.TwoHanded)
		//		speed = 4.0f;

		//	return speed;
		//}

		//public static int GetMinDamage(BaseWeapon weapon)
		//{
		//	double baseDamage = 1;

		//	if (weapon is TrainingSword)
		//		baseDamage = 1;
		//	else if (weapon is BaseTrainingWand)
		//		baseDamage = 1;
		//	else if (weapon is Fists)
		//		baseDamage = 9;
		//	else if (weapon is BaseRanged)
		//		baseDamage = 20;
		//	else if (weapon.Layer == Layer.OneHanded)
		//		baseDamage = 14;
		//	else if (weapon.Layer == Layer.TwoHanded)
		//		baseDamage = 25;

		//	if (baseDamage < 1)
		//		baseDamage = 1;

		//	return (int)baseDamage;
		//}

		//public static int GetMaxDamage(BaseWeapon weapon)
		//{
		//	return (int)(GetMinDamage(weapon) * 1.34);
		//}

		public static string GetWeaponAbilityNameByWeaponType(IWeapon weapon)
		{
			if (weapon == null)
				return String.Empty;

			var wa = GetWeaponAbilityTypeByWeaponType(weapon);

			if (wa == null)
				return String.Empty;

			return AddSpacesToSentence(wa.Name, true);
		}

		public static string AddSpacesToSentence(string text, bool preserveAcronyms)
		{
			if (string.IsNullOrWhiteSpace(text))
				return string.Empty;
			StringBuilder newText = new StringBuilder(text.Length * 2);
			newText.Append(text[0]);
			for (int i = 1; i < text.Length; i++)
			{
				if (char.IsUpper(text[i]))
					if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
						(preserveAcronyms && char.IsUpper(text[i - 1]) &&
						 i < text.Length - 1 && !char.IsUpper(text[i + 1])))
						newText.Append(' ');
				newText.Append(text[i]);
			}
			return newText.ToString();
		}

		public static string GetTypeName(BaseWeapon weapon)
		{
			if (weapon is BaseKnife)
				return "Knife";
			else if (weapon is BaseSword)
				return "Sword";
			else if (weapon is BaseAxe)
				return "Axe";
			else if (weapon is BaseSpear)
				return "Spear";
			else if (weapon is BasePoleArm)
				return "Pole arm";
			else if (weapon is BaseStaff)
				return "Staff";
			else if (weapon is BaseBashing)
				return "Bashing";
			else if (weapon is BaseBow)
				return "Bow";
			else if (weapon is BaseLongbow)
				return "Long Bow";
			else if (weapon is BaseCrossbow)
				return "Crossbow";
			else if (weapon is BaseHeavyCrossbow)
				return "Heavy Crossbow";
			else if (weapon is Fists)
				return "Fist";
			else if (weapon is BaseKatar)
				return "Katar";

			return string.Empty;
		}

		public static Type GetWeaponAbilityTypeByWeaponType(IWeapon iWeapon)
		{
			if (iWeapon == null || !(iWeapon is BaseWeapon))
				return null;

			var weapon = (BaseWeapon)iWeapon;

			if (weapon is BaseKnife)
				return typeof(InfectiousStrike);
			else if (weapon is BaseSword)
				return typeof(DoubleStrike);
			else if (weapon is BaseAxe)
				return typeof(WhirlwindAttack);
			else if (weapon is BaseSpear)
				return typeof(ArmorIgnore);
			else if (weapon is BasePoleArm || weapon is Lance)
				return typeof(Dismount);
			else if (weapon is BaseStaff)
				return typeof(Block);
			else if (weapon is BaseBashing)
				return typeof(CrushingBlow);
			else if (weapon is BaseBow)
				return typeof(MovingShot);
			else if (weapon is BaseLongbow)
				return typeof(MortalStrike);
			else if (weapon is BaseCrossbow)
				return typeof(SerpentArrow);
			else if (weapon is BaseHeavyCrossbow)
				return typeof(ArmorPierce);
			else if (weapon is Fists)
				return typeof(ParalyzingBlow);
			else if (weapon is BaseKatar)
				return typeof(ParalyzingBlow);

			return null;
		}

		public static bool CanActivateWeaponAbility(IWeapon weapon, WeaponAbility ability)
		{
			if (weapon == null || ability == null)
				return false;

			return ability.GetType() == GetWeaponAbilityTypeByWeaponType(weapon);
		}
	}
}
