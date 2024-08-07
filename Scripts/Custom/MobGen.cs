using System;
using System.IO;
using Server;
using Server.Commands;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Scripts.Commands
{
	public class MobGenCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register("MobGen", AccessLevel.GameMaster, new CommandEventHandler(MobGen_OnCommand));
		}

		[Usage("MobGen <ClassName>")]
		[Description("Permet de créer un script de créature qui sera sauvegardé sur : Scripts/Custom/MobGen.")]
		public static void MobGen_OnCommand(CommandEventArgs e)
		{
			if (e.Length != 1)
			{
				e.Mobile.SendMessage("Format: MobGen <ClassName>");
				return;
			}

			string className = e.GetString(0);
			string directoryPath = Path.Combine(Core.BaseDirectory, "Scripts", "Custom", "MobGen");
			string filePath = Path.Combine(directoryPath, $"{className}.cs");

			if (File.Exists(filePath))
			{
				e.Mobile.SendMessage($"Une créature avec le nom '{className}' existe déjà. S'il vous plaît modifier le nom");
				return;
			}

			e.Mobile.Target = new MobGenTarget(className);
			e.Mobile.SendMessage("Quelle créature voulez vous scripter?");
		}

		private class MobGenTarget : Target
		{
			private string ClassName;

			public MobGenTarget(string classname)
				: base(-1, false, TargetFlags.None)
			{
				ClassName = classname;
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

					// Le reste du code reste inchangé...

					from.SendMessage($"Script Créature généré et sauvegardé sur {filePath}");
				}
				catch (Exception ex)
				{
					from.SendMessage($"Script en erreur: {ex.Message}");
				}
			}
		}
	}
}
