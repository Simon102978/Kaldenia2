using System;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class DisenchantRune : BaseRune
	{
		[Constructable]
		public DisenchantRune() : base()
		{
			Weight = 0.2;
			Name = "Rune de désenchantement";
			Hue = 1153; // Couleur violette
		}

		public override bool CanEnchant(Item item, Mobile from)
		{
			if (item is BaseWeapon || item is BaseArmor || item is BaseJewel ||
				item is BaseInstrument || item is Spellbook)
			{
				return true;
			}

			from.SendMessage("Cette rune ne peut être utilisée que sur des armes, armures, bijoux, instruments ou grimoires.");
			return false;
		}

		public override void Enchant(Item item, Mobile from)
		{
			if (item is BaseWeapon weapon)
			{
				ResetAttributes(weapon.Attributes);
				ResetAttributes(weapon.WeaponAttributes);
			}
			else if (item is BaseArmor armor)
			{
				ResetAttributes(armor.Attributes);
				ResetAttributes(armor.ArmorAttributes);
			}
			else if (item is BaseJewel jewel)
			{
				ResetAttributes(jewel.Attributes);
			}
		
			else if (item is Spellbook spellbook)
			{
				ResetAttributes(spellbook.Attributes);
			}

			item.Enchantement = 0;
			from.SendMessage("Tous les enchantements ont été retirés de l'objet.");
			from.PlaySound(0x1F5);
			this.Delete();
		}

		private void ResetAttributes(AosAttributes attributes)
		{
			attributes.RegenHits = 0;
			attributes.RegenStam = 0;
			attributes.RegenMana = 0;
			attributes.DefendChance = 0;
			attributes.AttackChance = 0;
			attributes.BonusStr = 0;
			attributes.BonusDex = 0;
			attributes.BonusInt = 0;
			attributes.BonusHits = 0;
			attributes.BonusStam = 0;
			attributes.BonusMana = 0;
			attributes.WeaponDamage = 0;
			attributes.WeaponSpeed = 0;
			attributes.SpellDamage = 0;
			attributes.CastRecovery = 0;
			attributes.CastSpeed = 0;
			attributes.LowerManaCost = 0;
			attributes.LowerRegCost = 0;
			attributes.ReflectPhysical = 0;
			attributes.EnhancePotions = 0;
			attributes.Luck = 0;
			attributes.SpellChanneling = 0;
			attributes.NightSight = 0;
		}

		private void ResetAttributes(AosWeaponAttributes attributes)
		{
			attributes.LowerStatReq = 0;
			attributes.SelfRepair = 0;
			attributes.HitLeechHits = 0;
			attributes.HitLeechStam = 0;
			attributes.HitLeechMana = 0;
			attributes.HitLowerAttack = 0;
			attributes.HitLowerDefend = 0;
			attributes.HitMagicArrow = 0;
			attributes.HitHarm = 0;
			attributes.HitFireball = 0;
			attributes.HitLightning = 0;
			attributes.HitDispel = 0;
			attributes.HitColdArea = 0;
			attributes.HitFireArea = 0;
			attributes.HitPoisonArea = 0;
			attributes.HitEnergyArea = 0;
			attributes.HitPhysicalArea = 0;
			attributes.ResistPhysicalBonus = 0;
			attributes.ResistFireBonus = 0;
			attributes.ResistColdBonus = 0;
			attributes.ResistPoisonBonus = 0;
			attributes.ResistEnergyBonus = 0;
			attributes.UseBestSkill = 0;
			attributes.MageWeapon = 0;
			attributes.DurabilityBonus = 0;
		}

		private void ResetAttributes(AosArmorAttributes attributes)
		{
			attributes.LowerStatReq = 0;
			attributes.SelfRepair = 0;
			attributes.MageArmor = 0;
			attributes.DurabilityBonus = 0;
		}

		public DisenchantRune(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}
