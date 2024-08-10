using System;
using System.IO;
using System.Text;
using Server;
using Server.Commands;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;

namespace Server.Scripts.Commands
{
	public class MobGenCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register("MobGen", AccessLevel.GameMaster, new CommandEventHandler(MobGen_OnCommand));
			Console.WriteLine("MobGen command initialized successfully.");
		}

		[Usage("MobGen <ClassName>")]
		[Description("Permet de créer un script de créature qui sera sauvegardé sur le serveur dans : Scripts/Custom/MobGen.")]
		public static void MobGen_OnCommand(CommandEventArgs e)
		{
			if (!e.Mobile.IsStaff())
			{
				e.Mobile.SendMessage("Vous n'avez pas les permissions nécessaires pour utiliser cette commande.");
				return;
			}

			if (e.Length != 1)
			{
				e.Mobile.SendMessage("Format: MobGen <ClassName>");
				return;
			}

			string className = e.GetString(0);
			string serverPath = Server.Config.Get("ServUOPath", Core.BaseDirectory);
			string directoryPath = Path.Combine(serverPath, "Scripts","Custom","MobGen");
			string filePath = Path.Combine(directoryPath, $"{className}.cs");

			if (File.Exists(filePath))
			{
				e.Mobile.SendMessage($"Une créature avec le nom '{className}' existe déjà. S'il vous plaît modifier le nom");
				return;
			}

			e.Mobile.Target = new MobGenTarget(className, serverPath);
			e.Mobile.SendMessage("Quelle créature voulez vous scripter?");
		}

		private class MobGenTarget : Target
		{
			private string ClassName;
			private string ServerPath;

			public MobGenTarget(string classname, string serverPath)
				: base(-1, false, TargetFlags.None)
			{
				ClassName = classname;
				ServerPath = serverPath;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (!(targeted is BaseCreature creature))
				{
					from.SendMessage("Vous devez cibler une créature!");
					return;
				}

				try
				{
					string directoryPath = Path.Combine(Core.BaseDirectory, "Scripts", "Custom", "MobGen");
					Directory.CreateDirectory(directoryPath);
					string filePath = Path.Combine(directoryPath, $"{ClassName}.cs");

					StringBuilder sb = new StringBuilder();

					sb.AppendLine("using System;");
					sb.AppendLine("using Server;");
					sb.AppendLine("using Server.Items;");
					sb.AppendLine("using Server.Mobiles;");
					sb.AppendLine();
					sb.AppendLine("namespace Server.Mobiles");
					sb.AppendLine("{");
					sb.AppendLine($"\tpublic class {ClassName} : BaseCreature");
					sb.AppendLine("\t{");
					sb.AppendLine($"\t\tpublic {ClassName}() : base(AIType.{creature.AI}, FightMode.{creature.FightMode}, 10, 1, 0.2, 0.4)");
					sb.AppendLine("\t\t{");
					sb.AppendLine($"\t\t\tName = \"{creature.Name}\";");
					sb.AppendLine($"\t\t\tBody = {creature.Body};");
					sb.AppendLine($"\t\t\tBaseSoundID = {creature.BaseSoundID};");

					if (creature.Hue != 0)
						sb.AppendLine($"\t\t\tHue = {creature.Hue};");

					sb.AppendLine();
					sb.AppendLine($"\t\t\tSetStr({creature.Str});");
					sb.AppendLine($"\t\t\tSetDex({creature.Dex});");
					sb.AppendLine($"\t\t\tSetInt({creature.Int});");
					sb.AppendLine();
					sb.AppendLine($"\t\t\tSetHits({creature.HitsMax});");
					sb.AppendLine($"\t\t\tSetMana({creature.ManaMax});");
					sb.AppendLine($"\t\t\tSetStam({creature.StamMax});");
					sb.AppendLine();
					sb.AppendLine($"\t\t\tSetDamage({creature.DamageMin}, {creature.DamageMax});");
					sb.AppendLine();
					sb.AppendLine($"\t\t\tSetDamageType(ResistanceType.Physical, {creature.PhysicalDamage});");
					sb.AppendLine($"\t\t\tSetDamageType(ResistanceType.Fire, {creature.FireDamage});");
					sb.AppendLine($"\t\t\tSetDamageType(ResistanceType.Cold, {creature.ColdDamage});");
					sb.AppendLine($"\t\t\tSetDamageType(ResistanceType.Poison, {creature.PoisonDamage});");
					sb.AppendLine($"\t\t\tSetDamageType(ResistanceType.Energy, {creature.EnergyDamage});");
					sb.AppendLine();
					sb.AppendLine($"\t\t\tSetResistance(ResistanceType.Physical, {creature.PhysicalResistance});");
					sb.AppendLine($"\t\t\tSetResistance(ResistanceType.Fire, {creature.FireResistance});");
					sb.AppendLine($"\t\t\tSetResistance(ResistanceType.Cold, {creature.ColdResistance});");
					sb.AppendLine($"\t\t\tSetResistance(ResistanceType.Poison, {creature.PoisonResistance});");
					sb.AppendLine($"\t\t\tSetResistance(ResistanceType.Energy, {creature.EnergyResistance});");
					sb.AppendLine();
					sb.AppendLine($"\t\t\tSetSkill(SkillName.MagicResist, {creature.Skills[SkillName.MagicResist].Base});");
					sb.AppendLine($"\t\t\tSetSkill(SkillName.Tactics, {creature.Skills[SkillName.Tactics].Base});");
					sb.AppendLine($"\t\t\tSetSkill(SkillName.Wrestling, {creature.Skills[SkillName.Wrestling].Base});");
					sb.AppendLine();
					sb.AppendLine($"\t\t\tFame = {creature.Fame};");
					sb.AppendLine($"\t\t\tKarma = {creature.Karma};");
					sb.AppendLine();
					sb.AppendLine($"\t\t\tVirtualArmor = {creature.VirtualArmor};");

					if (creature.Tamable)
					{
						sb.AppendLine($"\t\t\tTamable = true;");
						sb.AppendLine($"\t\t\tControlSlots = {creature.ControlSlots};");
						sb.AppendLine($"\t\t\tMinTameSkill = {Math.Round(creature.MinTameSkill, 1).ToString(System.Globalization.CultureInfo.InvariantCulture)};");
					}

					if (creature.BodyValue == 400 || creature.BodyValue == 401)
					{
						sb.AppendLine("\n\t\t\t// Équipement");
						foreach (Item item in creature.Items)
						{
							if (item.Layer != Layer.Backpack)
							{
								sb.AppendLine($"\t\t\tAddItem(new {item.GetType().Name}");
								sb.AppendLine("\t\t\t{");
								if (item.Name != null)
									sb.AppendLine($"\t\t\t\tName = \"{item.Name}\",");
								if (item.Hue != 0)
									sb.AppendLine($"\t\t\t\tHue = {item.Hue},");
								if (item.LootType != LootType.Regular)
									sb.AppendLine($"\t\t\t\tLootType = LootType.{item.LootType},");
								sb.AppendLine($"\t\t\t\tLayer = Layer.{item.Layer},");
								if (item is BaseWeapon weapon)
								{
									sb.AppendLine($"\t\t\t\tQuality = ItemQuality.{weapon.Quality},");
									if (weapon.Resource != CraftResource.None)
										sb.AppendLine($"\t\t\t\tResource = CraftResource.{weapon.Resource},");
								}
								else if (item is BaseArmor armor)
								{
									sb.AppendLine($"\t\t\t\tQuality = ItemQuality.{armor.Quality},");
									if (armor.Resource != CraftResource.None)
										sb.AppendLine($"\t\t\t\tResource = CraftResource.{armor.Resource},");
								}
								sb.AppendLine("\t\t\t});");
							}
						}
					}

					sb.AppendLine("\t\t}");
					sb.AppendLine();
					sb.AppendLine($"\t\tpublic {ClassName}(Serial serial) : base(serial)");
					sb.AppendLine("\t\t{");
					sb.AppendLine("\t\t}");
					sb.AppendLine();
					sb.AppendLine("\t\tpublic override void Serialize(GenericWriter writer)");
					sb.AppendLine("\t\t{");
					sb.AppendLine("\t\t\tbase.Serialize(writer);");
					sb.AppendLine("\t\t\twriter.Write(0);");
					sb.AppendLine("\t\t}");
					sb.AppendLine();
					sb.AppendLine("\t\tpublic override void Deserialize(GenericReader reader)");
					sb.AppendLine("\t\t{");
					sb.AppendLine("\t\t\tbase.Deserialize(reader);");
					sb.AppendLine("\t\t\tint version = reader.ReadInt();");
					sb.AppendLine("\t\t}");
					sb.AppendLine("\t}");
					sb.AppendLine("}");

					File.WriteAllText(filePath, sb.ToString());

					from.SendMessage($"Script Créature généré et sauvegardé sur le serveur : {filePath}");
				}
				catch (Exception ex)
				{
					from.SendMessage($"Erreur lors de la génération du script : {ex.Message}");
				}
			}
		}
	}
}
