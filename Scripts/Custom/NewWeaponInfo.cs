using System;
using Server.Items;
using Server.Commands;
using Server.Custom.Misc;
using System.Text;
using Server.Custom.Items.Spells;

namespace Server.Custom.Weapons
{
	class DPSCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register("DPS", AccessLevel.GameMaster, new CommandEventHandler(DPS_OnCommand));
		}

		[Usage("DPS")]
		[Description("Permet de connaitre le Damage Par Second d'une arme.")]
		public static void DPS_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;

			from.SendMessage(HueManager.GetHue(HueManagerList.Blue), "DPS: {0}", NewWeaponInfo.GetDPS((BaseWeapon)from.Weapon).ToString("0.00"));
		}
	}

	class NewWeaponInfo
	{
		public static double GetDPS(BaseWeapon weapon)
		{
			double dps = 0.0;

			if (weapon != null)
			{
				var minDamage = weapon.MinDamage;
				var maxDamage = weapon.MaxDamage;
				var speed = weapon.Speed;
				double dSpeed = Convert.ToDouble(speed.ToString());
				dps = ((double)minDamage + (double)maxDamage) / 2.0 * 1.0 / dSpeed;
			}

			return dps;
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
			else if (weapon is BaseThrust)
				return "Thrust";
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
			else if (weapon is BaseThrust)
				return typeof(Disarm);
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
