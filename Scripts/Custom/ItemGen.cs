using System;
using System.IO;
using Server;
using Server.Commands;
using Server.Targeting;
using Server.Items;
using Server.Mobiles.MannequinProperty;

namespace Server.Scripts.Commands
{
	public class GenScriptCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register("ItemGen", AccessLevel.GameMaster, new CommandEventHandler(ItemGen_OnCommand));
		}

		[Usage("GenScript <ClassName>")]
		[Description("Permet de créer un script d'item qui sera sauvegardé sur : Scripts/Custom/ItemGen.")]
		public static void ItemGen_OnCommand(CommandEventArgs e)
		{
			if (e.Length != 1)
			{
				e.Mobile.SendMessage("Format: ItemGen <ClassName>");
				return;
			}

			string className = e.GetString(0);
			string directoryPath = Path.Combine(Core.BaseDirectory, "Scripts", "Custom", "ItemGen");
			string filePath = Path.Combine(directoryPath, $"{className}.cs");

			if (File.Exists(filePath))
			{
				e.Mobile.SendMessage($"Un item avec le nom '{className}' existe déjà. S'il vous plaît, modifier le nom.");
				return;
			}


			e.Mobile.Target = new ItemGenTarget(e.GetString(0));
			e.Mobile.SendMessage("Quel item voulez-vous scripter?");
		}

		private class ItemGenTarget : Target
		{
			private string ClassName;

			public ItemGenTarget(string classname)
				: base(-1, false, TargetFlags.None)
			{
				ClassName = classname;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (!(targeted is Item item))
				{
					from.SendMessage("Vous devez cibler un item!");
					return;
				}

				try
				{
					string directoryPath = Path.Combine(Core.BaseDirectory, "Scripts", "Custom", "ItemGen");
					Directory.CreateDirectory(directoryPath);

					string filePath = Path.Combine(directoryPath, $"{ClassName}.cs");

					using (StreamWriter writer = new StreamWriter(filePath))
					{
						writer.WriteLine("using System;");
						writer.WriteLine("using Server.Items;");
						writer.WriteLine();
						writer.WriteLine("namespace Server.Items");
						writer.WriteLine("{");
						writer.WriteLine($"    public class {ClassName} : {item.GetType().Name}");
						writer.WriteLine("    {");
						writer.WriteLine("        [Constructable]");
						writer.WriteLine($"        public {ClassName}() : base({item.ItemID})");
						writer.WriteLine("        {");

						// Propriétés de base communes à tous les items
						if (!string.IsNullOrEmpty(item.Name))
							writer.WriteLine($"            Name = \"{item.Name}\";");
						if (item.Hue != 0)
							writer.WriteLine($"            Hue = {item.Hue};");
						if (item.Weight != 0)
							writer.WriteLine($"            Weight = {item.Weight};");

						// Écrire les propriétés de ressource pour tout type d'objet
						WriteResourceProperties(writer, item);

						// Propriétés spécifiques aux types d'items
						if (item is BaseArmor armor)
						{
							WriteArmorProperties(writer, armor);
						}
						else if (item is BaseWeapon weapon)
						{
							WriteWeaponProperties(writer, weapon);
						}
						else if (item is BaseJewel jewel)
						{
							WriteJewelProperties(writer, jewel);
						}

						// Propriétés communes à plusieurs types d'items
						if (item is IWearableDurability wearable)
						{
							WriteWearableProperties(writer, wearable);
						}

						// Autres propriétés générales
						if (item.LootType != LootType.Regular)
							writer.WriteLine($"            LootType = LootType.{item.LootType};");
						if (item.Insured)
							writer.WriteLine($"            Insured = true;");
						if (!item.Movable)
							writer.WriteLine($"            Movable = false;");
						if (item.Stackable)
						{
							writer.WriteLine($"            Stackable = true;");
							if (item.Amount > 1)
								writer.WriteLine($"            Amount = {item.Amount};");
						}

						writer.WriteLine("        }");
						writer.WriteLine();
						writer.WriteLine($"        public {ClassName}(Serial serial) : base(serial)");
						writer.WriteLine("        {");
						writer.WriteLine("        }");
						writer.WriteLine();
						writer.WriteLine("        public override void Serialize(GenericWriter writer)");
						writer.WriteLine("        {");
						writer.WriteLine("            base.Serialize(writer);");
						writer.WriteLine("            writer.Write(0); // version");
						writer.WriteLine("        }");
						writer.WriteLine();
						writer.WriteLine("        public override void Deserialize(GenericReader reader)");
						writer.WriteLine("        {");
						writer.WriteLine("            base.Deserialize(reader);");
						writer.WriteLine("            int version = reader.ReadInt();");
						writer.WriteLine("        }");
						writer.WriteLine("    }");
						writer.WriteLine("}");
					}

					from.SendMessage($"Script Item généré et sauvegardé sur {filePath}");
				}
				catch (Exception ex)
				{
					from.SendMessage($"Script en erreur: {ex.Message}");
				}
			}

			private void WriteResourceProperties(StreamWriter writer, Item item)
			{
				if (item is IResource resourceItem)
				{
					CraftResource resource = resourceItem.Resource;
					if (resource != CraftResource.None && resource != CraftResource.Iron)
					{
						writer.WriteLine($"            Resource = CraftResource.{resource};");
					}
				}
			}

			private void WriteArmorProperties(StreamWriter writer, BaseArmor armor)
			{
				writer.WriteLine($"            Layer = Layer.{armor.Layer};");
				if (armor.Quality != ItemQuality.Normal)
					writer.WriteLine($"            Quality = ArmorQuality.{armor.Quality};");
				

				if (armor.PhysicalResistance != 0) writer.WriteLine($"            PhysicalResistance = {armor.PhysicalResistance};");
				if (armor.FireResistance != 0) writer.WriteLine($"            FireResistance = {armor.FireResistance};");
				if (armor.ColdResistance != 0) writer.WriteLine($"            ColdResistance = {armor.ColdResistance};");
				if (armor.PoisonResistance != 0) writer.WriteLine($"            PoisonResistance = {armor.PoisonResistance};");
				if (armor.EnergyResistance != 0) writer.WriteLine($"            EnergyResistance = {armor.EnergyResistance};");

				WriteAosAttributes(writer, armor.Attributes);
				WriteAosArmorAttributes(writer, armor.ArmorAttributes);
				WriteAosSkillBonuses(writer, armor.SkillBonuses);
			}

			private void WriteWeaponProperties(StreamWriter writer, BaseWeapon weapon)
			{
				writer.WriteLine($"            Layer = Layer.{weapon.Layer};");
				if (weapon.Quality != ItemQuality.Normal)
					writer.WriteLine($"            Quality = WeaponQuality.{weapon.Quality};");
				

				writer.WriteLine($"            MinDamage = {weapon.MinDamage};");
				writer.WriteLine($"            MaxDamage = {weapon.MaxDamage};");
				writer.WriteLine($"            Speed = {weapon.Speed};");

				WriteAosAttributes(writer, weapon.Attributes);
				WriteAosWeaponAttributes(writer, weapon.WeaponAttributes);
				WriteAosElementAttributes(writer, weapon.AosElementDamages);
			}

			private void WriteJewelProperties(StreamWriter writer, BaseJewel jewel)
			{
				WriteAosAttributes(writer, jewel.Attributes);
				WriteAosElementAttributes(writer, jewel.Resistances);
				WriteAosSkillBonuses(writer, jewel.SkillBonuses);
			}

			private void WriteWearableProperties(StreamWriter writer, IWearableDurability wearable)
			{
				if (wearable.MaxHitPoints != 0)
					writer.WriteLine($"            MaxHitPoints = {wearable.MaxHitPoints};");
				if (wearable.HitPoints != 0)
					writer.WriteLine($"            HitPoints = {wearable.HitPoints};");
			}

			private void WriteAosAttributes(StreamWriter writer, AosAttributes attrs)
			{
				if (attrs.WeaponDamage != 0) writer.WriteLine($"            Attributes.WeaponDamage = {attrs.WeaponDamage};");
				if (attrs.WeaponSpeed != 0) writer.WriteLine($"            Attributes.WeaponSpeed = {attrs.WeaponSpeed};");
				if (attrs.SpellDamage != 0) writer.WriteLine($"            Attributes.SpellDamage = {attrs.SpellDamage};");
				if (attrs.CastRecovery != 0) writer.WriteLine($"            Attributes.CastRecovery = {attrs.CastRecovery};");
				if (attrs.CastSpeed != 0) writer.WriteLine($"            Attributes.CastSpeed = {attrs.CastSpeed};");
				if (attrs.AttackChance != 0) writer.WriteLine($"            Attributes.AttackChance = {attrs.AttackChance};");
				if (attrs.DefendChance != 0) writer.WriteLine($"            Attributes.DefendChance = {attrs.DefendChance};");
				if (attrs.BonusStr != 0) writer.WriteLine($"            Attributes.BonusStr = {attrs.BonusStr};");
				if (attrs.BonusDex != 0) writer.WriteLine($"            Attributes.BonusDex = {attrs.BonusDex};");
				if (attrs.BonusInt != 0) writer.WriteLine($"            Attributes.BonusInt = {attrs.BonusInt};");
				if (attrs.BonusHits != 0) writer.WriteLine($"            Attributes.BonusHits = {attrs.BonusHits};");
				if (attrs.BonusStam != 0) writer.WriteLine($"            Attributes.BonusStam = {attrs.BonusStam};");
				if (attrs.BonusMana != 0) writer.WriteLine($"            Attributes.BonusMana = {attrs.BonusMana};");
				if (attrs.RegenHits != 0) writer.WriteLine($"            Attributes.RegenHits = {attrs.RegenHits};");
				if (attrs.RegenStam != 0) writer.WriteLine($"            Attributes.RegenStam = {attrs.RegenStam};");
				if (attrs.RegenMana != 0) writer.WriteLine($"            Attributes.RegenMana = {attrs.RegenMana};");
				if (attrs.Luck != 0) writer.WriteLine($"            Attributes.Luck = {attrs.Luck};");
				if (attrs.EnhancePotions != 0) writer.WriteLine($"            Attributes.EnhancePotions = {attrs.EnhancePotions};");
				if (attrs.ReflectPhysical != 0) writer.WriteLine($"            Attributes.ReflectPhysical = {attrs.ReflectPhysical};");
				if (attrs.NightSight != 0) writer.WriteLine($"            Attributes.NightSight = {attrs.NightSight};");
			}

			private void WriteAosElementAttributes(StreamWriter writer, AosElementAttributes attrs)
			{
				if (attrs.Physical != 0) writer.WriteLine($"            Resistances.Physical = {attrs.Physical};");
				if (attrs.Fire != 0) writer.WriteLine($"            Resistances.Fire = {attrs.Fire};");
				if (attrs.Cold != 0) writer.WriteLine($"            Resistances.Cold = {attrs.Cold};");
				if (attrs.Poison != 0) writer.WriteLine($"            Resistances.Poison = {attrs.Poison};");
				if (attrs.Energy != 0) writer.WriteLine($"            Resistances.Energy = {attrs.Energy};");
			}

			private void WriteAosSkillBonuses(StreamWriter writer, AosSkillBonuses skillBonuses)
			{
				for (int i = 0; i < 5; i++)
				{
					if (skillBonuses.GetBonus(i) != 0)
					{
						writer.WriteLine($"            SkillBonuses.SetSkill({i}, SkillName.{skillBonuses.GetSkill(i)}, {skillBonuses.GetBonus(i)});");
					}
				}
			}

			private void WriteAosArmorAttributes(StreamWriter writer, AosArmorAttributes attrs)
			{
				if (attrs.LowerStatReq != 0) writer.WriteLine($"            ArmorAttributes.LowerStatReq = {attrs.LowerStatReq};");
				if (attrs.SelfRepair != 0) writer.WriteLine($"            ArmorAttributes.SelfRepair = {attrs.SelfRepair};");
				if (attrs.MageArmor != 0) writer.WriteLine($"            ArmorAttributes.MageArmor = {attrs.MageArmor};");
				if (attrs.DurabilityBonus != 0) writer.WriteLine($"            ArmorAttributes.DurabilityBonus = {attrs.DurabilityBonus};");
			}

			private void WriteAosWeaponAttributes(StreamWriter writer, AosWeaponAttributes attrs)
			{
				if (attrs.LowerStatReq != 0) writer.WriteLine($"            WeaponAttributes.LowerStatReq = {attrs.LowerStatReq};");
				if (attrs.SelfRepair != 0) writer.WriteLine($"            WeaponAttributes.SelfRepair = {attrs.SelfRepair};");
				if (attrs.HitLeechHits != 0) writer.WriteLine($"            WeaponAttributes.HitLeechHits = {attrs.HitLeechHits};");
				if (attrs.HitLeechStam != 0) writer.WriteLine($"            WeaponAttributes.HitLeechStam = {attrs.HitLeechStam};");
				if (attrs.HitLeechMana != 0) writer.WriteLine($"            WeaponAttributes.HitLeechMana = {attrs.HitLeechMana};");
				if (attrs.HitLowerAttack != 0) writer.WriteLine($"            WeaponAttributes.HitLowerAttack = {attrs.HitLowerAttack};");
				if (attrs.HitLowerDefend != 0) writer.WriteLine($"            WeaponAttributes.HitLowerDefend = {attrs.HitLowerDefend};");
				if (attrs.HitMagicArrow != 0) writer.WriteLine($"            WeaponAttributes.HitMagicArrow = {attrs.HitMagicArrow};");
				if (attrs.HitHarm != 0) writer.WriteLine($"            WeaponAttributes.HitHarm = {attrs.HitHarm};");
				if (attrs.HitFireball != 0) writer.WriteLine($"            WeaponAttributes.HitFireball = {attrs.HitFireball};");
				if (attrs.HitLightning != 0) writer.WriteLine($"            WeaponAttributes.HitLightning = {attrs.HitLightning};");
				if (attrs.HitDispel != 0) writer.WriteLine($"            WeaponAttributes.HitDispel = {attrs.HitDispel};");
				if (attrs.HitColdArea != 0) writer.WriteLine($"            WeaponAttributes.HitColdArea = {attrs.HitColdArea};");
				if (attrs.HitFireArea != 0) writer.WriteLine($"            WeaponAttributes.HitFireArea = {attrs.HitFireArea};");
				if (attrs.HitPoisonArea != 0) writer.WriteLine($"            WeaponAttributes.HitPoisonArea = {attrs.HitPoisonArea};");
				if (attrs.HitEnergyArea != 0) writer.WriteLine($"            WeaponAttributes.HitEnergyArea = {attrs.HitEnergyArea};");
				if (attrs.HitPhysicalArea != 0) writer.WriteLine($"            WeaponAttributes.HitPhysicalArea = {attrs.HitPhysicalArea};");
				if (attrs.ResistPhysicalBonus != 0) writer.WriteLine($"            WeaponAttributes.ResistPhysicalBonus = {attrs.ResistPhysicalBonus};");
				if (attrs.ResistFireBonus != 0) writer.WriteLine($"            WeaponAttributes.ResistFireBonus = {attrs.ResistFireBonus};");
				if (attrs.ResistColdBonus != 0) writer.WriteLine($"            WeaponAttributes.ResistColdBonus = {attrs.ResistColdBonus};");
				if (attrs.ResistPoisonBonus != 0) writer.WriteLine($"            WeaponAttributes.ResistPoisonBonus = {attrs.ResistPoisonBonus};");
				if (attrs.ResistEnergyBonus != 0) writer.WriteLine($"            WeaponAttributes.ResistEnergyBonus = {attrs.ResistEnergyBonus};");
				if (attrs.UseBestSkill != 0) writer.WriteLine($"            WeaponAttributes.UseBestSkill = {attrs.UseBestSkill};");
				if (attrs.MageWeapon != 0) writer.WriteLine($"            WeaponAttributes.MageWeapon = {attrs.MageWeapon};");
				if (attrs.DurabilityBonus != 0) writer.WriteLine($"            WeaponAttributes.DurabilityBonus = {attrs.DurabilityBonus};");
			}
		}
	}
}

